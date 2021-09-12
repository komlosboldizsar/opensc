using OpenSC.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules
{

    class ModuleLoader
    {

        private const string LOG_TAG = "ModuleLoader";

        public void LoadModules()
        {
            lookupForModules();
            findDependencies();
            runDfs();
            markModulesToInit();
            initModules();
        }

        private List<ModuleData> loadedModules = new List<ModuleData>();
        private Dictionary<string, ModuleData> loadedModulesByName = new Dictionary<string, ModuleData>();
        private Dictionary<Type, ModuleData> loadedModulesByType = new Dictionary<Type, ModuleData>();

        private static readonly Type MODULE_DESCRIPTOR_TYPE = typeof(IModule);
        private static readonly object[] EMPTY_ARRAY = new object[] { };
        private static readonly Type[] EMPTY_TYPE_ARRAY = new Type[] { };
        private static readonly ModuleData UNKNOWN_MODULE = new ModuleData();

        private void lookupForModules()
        {
            string moduleDirectoryPath = Directory.GetCurrentDirectory();
            DirectoryInfo moduleDirectory = new DirectoryInfo(moduleDirectoryPath);
            foreach (FileInfo fileInfo in moduleDirectory.GetFiles())
            {
                if (fileInfo.Extension == ".dll")
                {
                    Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                    IEnumerable<TypeInfo> moduleDescriptorTypeInfos = assembly.DefinedTypes.Where(ti => ti.ImplementedInterfaces.Contains(MODULE_DESCRIPTOR_TYPE));
                    foreach (TypeInfo moduleDescriptorTypeInfo in moduleDescriptorTypeInfos)
                    {
                        ModuleAttribute moduleAttribute = moduleDescriptorTypeInfo.GetCustomAttribute<ModuleAttribute>();
                        if (moduleAttribute == null)
                            continue;
                        ConstructorInfo constuctorInfo = moduleDescriptorTypeInfo.GetConstructor(EMPTY_TYPE_ARRAY);
                        if (constuctorInfo == null)
                            continue;
                        IModule moduleDescriptor = constuctorInfo.Invoke(EMPTY_ARRAY) as IModule;
                        if (moduleDescriptor == null)
                            continue;
                        ModuleData moduleData = new ModuleData()
                        {
                            Name = moduleAttribute.Name,
                            Path = fileInfo.FullName,
                            Assembly = assembly,
                            Instance = moduleDescriptor,
                            Type = moduleDescriptorTypeInfo.AsType()
                        };
                        loadedModules.Add(moduleData);
                        loadedModulesByName.Add(moduleData.Name, moduleData);
                        loadedModulesByType.Add(moduleData.Type, moduleData);
                        LogDispatcher.I(LOG_TAG, "Found module [{0}] in [{1}].", moduleData.Name, moduleData.Path);
                    }
                }
            }
        }

        private void findDependencies()
        {
            foreach (ModuleData moduleData in loadedModules)
            {
                IEnumerable<DependsOnModuleAttribute> dependencyAttributes = moduleData.Type.GetCustomAttributes<DependsOnModuleAttribute>();
                foreach (DependsOnModuleAttribute dependencyAttribute in dependencyAttributes)
                {
                    if (!loadedModulesByType.TryGetValue(dependencyAttribute.Module, out ModuleData dependencyModuleData))
                        dependencyModuleData = UNKNOWN_MODULE;
                    moduleData.DependsOn.Add(dependencyModuleData);
                    dependencyModuleData?.IsDependencyFor?.Add(moduleData);
                }
            }
        }

        private void runDfs()
        {
            // http://cs.bme.hu/bsz2/dfs.pdf
            foreach (ModuleData node in loadedModules)
            {
                node.DfsDepthIndex = null;
                node.DfsFinishIndex = null;
                node.DfsPrecedingNode = null;
            }
            ModuleData currentNode = loadedModules.FirstOrDefault(); // "a"
            if (currentNode == null)
                return;
            int maxDepthIndex = 1; // "D"
            int maxFinishIndex = 0; // "F"
            currentNode.DfsDepthIndex = 1;
            while (true)
            {
                ModuleData childNode = currentNode.DependsOn.FirstOrDefault(n => (n.DfsDepthIndex == null));
                if (childNode != null)
                {
                    childNode.DfsDepthIndex = ++maxDepthIndex;
                    childNode.DfsPrecedingNode = currentNode;
                    if ((childNode.DfsDepthIndex < currentNode.DfsDepthIndex) && childNode.DfsFinishIndex == null)
                        throw new CyclicDependencyException();
                    currentNode = childNode;
                }
                else
                {
                    maxFinishIndex++;
                    currentNode.DfsFinishIndex = maxFinishIndex;
                    if (currentNode.DfsPrecedingNode != null)
                    {
                        currentNode = currentNode.DfsPrecedingNode;
                    }
                    else
                    {
                        currentNode = loadedModules.FirstOrDefault(n => (n.DfsDepthIndex == null));
                        if (currentNode != null)
                            currentNode.DfsDepthIndex = ++maxDepthIndex;
                        else
                            break;
                    }
                }
            }
        }

        private void markModulesToInit()
        {
            foreach (ModuleData moduleData in loadedModules)
                moduleData.ToInit = true;
            // Check if all dependencies are loaded
            foreach (ModuleData moduleData in loadedModules)
                foreach (ModuleData dependencyModuleData in moduleData.DependsOn)
                    if (dependencyModuleData == UNKNOWN_MODULE)
                        moduleDependencyMissing(moduleData);
        }

        private void initModules()
        {
            foreach (ModuleData moduleData in loadedModules)
            {
                if (moduleData.ToInit)
                {
                    moduleData.Instance.Initialize();
                    LogDispatcher.I(LOG_TAG, "Initializing module [{0}]...", moduleData.Name);
                }
            }
        }

        private void moduleDependencyMissing(ModuleData moduleData)
        {
            moduleData.ToInit = false;
            foreach (ModuleData dependingModuleData in moduleData.IsDependencyFor)
                moduleDependencyMissing(dependingModuleData);
        }

        private class ModuleData
        {
            public string Name;
            public string Path;
            public Assembly Assembly;
            public Type Type;
            public IModule Instance;
            public bool ToInit;
            // Dependencies
            public List<ModuleData> DependsOn = new List<ModuleData>(); // edges to
            public List<ModuleData> IsDependencyFor = new List<ModuleData>(); // edges from
            // For DFS algorithm
            public int? DfsDepthIndex; // "d"
            public int? DfsFinishIndex; // "f"
            public ModuleData DfsPrecedingNode; // "m"
        }

        public class CyclicDependencyException : Exception
        {
            public CyclicDependencyException()
            { }
        }

    }

}
