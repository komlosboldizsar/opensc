using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public delegate void RouterIdChangingDelegate(Router router, int oldValue, int newValue);
    public delegate void RouterIdChangedDelegate(Router router, int oldValue, int newValue);

    public delegate void RouterNameChangingDelegate(Router router, string oldName, string newName);
    public delegate void RouterNameChangedDelegate(Router router, string oldName, string newName);

    public abstract class Router : IModel
    {

        public virtual void Restored()
        {
            restoreInputSources();
            updateCrosspointRouterAssociations();
            updateAllCrosspoints();
        }

        public event RouterIdChangingDelegate IdChanging;
        public event RouterIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        public int id = 0;

        public int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                if (value == id)
                    return;
                int oldValue = id;
                IdChanging?.Invoke(this, oldValue, value);
                IdChangingPCN?.Invoke();
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                IdChangedPCN?.Invoke();
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!RouterDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public event RouterNameChangingDelegate NameChanging;
        public event RouterNameChangedDelegate NameChanged;
        public event ParameterlessChangeNotifierDelegate NameChangingPCN;
        public event ParameterlessChangeNotifierDelegate NameChangedPCN;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                ValidateName(value);
                if (value == name)
                    return;
                string oldName = name;
                NameChanging?.Invoke(this, oldName, value);
                NameChangingPCN?.Invoke();
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                NameChangedPCN?.Invoke();
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }

        private ObservableList<RouterInput> inputs = new ObservableList<RouterInput>();

        public ObservableList<RouterInput> Inputs
        {
            get { return inputs; }
        }

        [PersistAs("inputs")]
        [PersistAs("input", 1)]
        private RouterInput[] _inputs
        {
            get { return inputs.ToArray(); }
            set
            {
                if(value != null)
                    inputs.AddRange(value);
                updateInputIndices();
            }
        }

        public void AddInput()
        {
            int index = inputs.Count;
            inputs.Add(new RouterInput() {
                Index = index,
                Name = string.Format("Input #{0}", index + 1)
            });
        }

        public void RemoveInput(RouterInput input)
        {
            inputs.Remove(input);
            updateInputIndices();
        }

        private void updateInputIndices()
        {
            for (int i = 0; i < inputs.Count; i++)
                inputs[i].Index = i;
        }

        private ObservableList<RouterOutput> outputs = new ObservableList<RouterOutput>();

        public ObservableList<RouterOutput> Outputs
        {
            get { return outputs; }
        }

        [PersistAs("outputs")]
        [PersistAs("output", 1)]
        private RouterOutput[] _outputs
        {
            get { return outputs.ToArray(); }
            set
            {
                if(value != null)
                    outputs.AddRange(value);
                updateOutputIndices();
            }
        }

        public void AddOutput()
        {
            int index = outputs.Count;
            outputs.Add(new RouterOutput()
            {
                Index = index,
                Name = string.Format("Onuput #{0}", index + 1)
            });
        }

        public void RemoveOutput(RouterOutput output)
        {
            outputs.Remove(output);
            updateOutputIndices();
        }

        private void updateOutputIndices()
        {
            for (int i = 0; i < outputs.Count; i++)
                outputs[i].Index = i;
        }

        public bool UpdateCrosspoint(RouterOutput output, RouterInput input)
        {
            if (!outputs.Contains(output))
                return false;
            return setCrosspoint(output, input);
        }

        protected abstract bool setCrosspoint(RouterOutput output, RouterInput input);
        protected abstract void updateAllCrosspoints();

        private void updateCrosspointRouterAssociations()
        {
            foreach (RouterInput input in inputs)
                input.Router = this;
            foreach (RouterOutput output in outputs)
                output.Router = this;
        }

        private void restoreInputSources()
        {
            foreach (RouterInput input in inputs)
                input.RestoreSource();
        }

    }

}
