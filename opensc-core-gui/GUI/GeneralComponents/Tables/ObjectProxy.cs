using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public abstract class ObjectProxy<TOriginal> : INotifyPropertyChanged
            where TOriginal : class
    {

        private TOriginal original;

        public TOriginal Original
        {
            get => original;
            init
            {
                original = value;
                INotifyPropertyChanged originalCasted = original as INotifyPropertyChanged;
                if (originalCasted != null)
                    originalCasted.PropertyChanged += originalsPropertyChanged;
            }
        }

        public ObjectProxy(TOriginal original = null, Dictionary<string, string> propertyBindings = null)
        {
            Original = original;
            this.propertyBindings = propertyBindings;
        }

        private Dictionary<string, string> propertyBindings;

        private void originalsPropertyChanged(string originalPropertyName)
        {
            if ((propertyBindings != null) && propertyBindings.TryGetValue(originalPropertyName, out string proxyPropertyName))
                RaisePropertyChanged(proxyPropertyName);
        }

        #region INotifyPropertyChanged
        PropertyChangedDelegate INotifyPropertyChanged._PropertyChanged { get; set; }
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => ((INotifyPropertyChanged)this).RaisePropertyChanged(propertyName);
        #endregion

    }

}
