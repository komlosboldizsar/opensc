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

    [DatabaseName("umds")]
    class UmdDatabase: DatabaseBase<UMD>
    {

        public static UmdDatabase Instance { get; } = new UmdDatabase();

    }
}
