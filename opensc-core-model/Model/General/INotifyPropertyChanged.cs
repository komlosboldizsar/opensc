using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{

    public delegate void PropertyChangedDelegate(string propertyName);

    public interface INotifyPropertyChanged
    {

        protected PropertyChangedDelegate _PropertyChanged { get; set; }

        public event PropertyChangedDelegate PropertyChanged
        {
            add => _PropertyChanged += value;
            remove => _PropertyChanged -= value;
        }

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "") =>  _PropertyChanged?.Invoke(propertyName);

    }

}
