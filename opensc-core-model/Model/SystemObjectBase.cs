using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public abstract class SystemObjectBase : ISystemObject, INotifyPropertyChanged
    {

        public event PropertyChangedTwoValuesDelegate<SystemObjectBase, string> GlobalIdChanged;

        private string globalId;
        public string GlobalID
        {
            get => globalId;
            protected set => this.setProperty(ref globalId, value, GlobalIdChanged);
        }

        PropertyChangedDelegate INotifyPropertyChanged._PropertyChanged { get; set; }

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "") => ((INotifyPropertyChanged)this).RaisePropertyChanged(propertyName);

    }

}
