using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{
    public class ObjectBase : INotifyPropertyChanged
    {
        public event PropertyChangedDelegate PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(propertyName);
    }
}
