using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    public delegate void ParameterlessChangeNotifierDelegate();
    public interface IModel
    {
        int ID { get; set; }
        void Restored();
        void Removed();
        void StartUpdate();
        void StopUpdate();
    }
}
