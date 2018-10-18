using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    public interface IModel
    {
        int ID { get; set; }
        void Restored();
    }
}
