using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Model.UMDs
{

    public delegate void UmdDatabaseAddingUmdDelegate(UMD Umd);
    public delegate void UmdDatabaseAddedUmdDelegate(UMD Umd);

    public delegate void UmdDatabaseRemovingUmdDelegate(UMD Umd);
    public delegate void UmdDatabaseRemovedUmdDelegate(UMD Umd);

    public delegate void UmdDatabaseElementsChangingDelegate();
    public delegate void UmdDatabaseElementsChangedDelegate();

    class UmdDatabase
    {

        public static UmdDatabase Instance { get; } = new UmdDatabase();

        private Dictionary<int, UMD> umds = new Dictionary<int, UMD>();

        public event UmdDatabaseElementsChangingDelegate ElementsChanging;
        public event UmdDatabaseElementsChangedDelegate ElementsChanged;

        public event UmdDatabaseAddingUmdDelegate AddingUmd;
        public event UmdDatabaseAddedUmdDelegate AddedUmd;

        public event UmdDatabaseRemovingUmdDelegate RemovingUmd;
        public event UmdDatabaseRemovedUmdDelegate RemovedUmd;

        public IReadOnlyDictionary<int, UMD> UMDsWithKeys
        {
            get => umds;
        }

        public IReadOnlyList<UMD> UMDs
        {
            get => umds.Values.ToList();
        }

        public void Add(UMD umd)
        {

            // Validate argument
            if (umd == null)
                throw new ArgumentNullException();

            // Validate ID
            int id = umd.ID;
            if (id <= 0)
                throw new Exception();

            if (umds.ContainsKey(id))
                throw new Exception();

            // Add element
            ElementsChanging?.Invoke();
            AddingUmd?.Invoke(umd);

            umds.Add(umd.ID, umd);

            AddedUmd?.Invoke(umd);
            ElementsChanged?.Invoke();

        }

        public bool Remove(UMD umd)
        {

            // Validate argument
            if (umd == null)
                throw new ArgumentNullException();

            if (!umds.ContainsValue(umd))
                return false;

            // Remove element
            ElementsChanging?.Invoke();
            RemovingUmd?.Invoke(umd);

            umds.Remove(umd.ID);

            RemovedUmd?.Invoke(umd);
            ElementsChanged?.Invoke();

            return true;

        }

        public bool IsIdValidForUmd(int id, UMD umd)
        {
            if (!umds.TryGetValue(id, out UMD foundUmd))
                return true;
            if (foundUmd == umd)
                return true;
            return false;
        }

    }
}
