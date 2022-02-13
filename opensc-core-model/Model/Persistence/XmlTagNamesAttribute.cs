using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{

    [AttributeUsage(AttributeTargets.Class)]
    public class XmlTagNamesAttribute: Attribute
    {

        public string RootTag { get; private set; } = "root";

        public string ItemTag { get; private set; } = "item";

        public XmlTagNamesAttribute(string RootTag, string ItemTag)
        {
            this.RootTag = RootTag;
            this.ItemTag = ItemTag;
        }

    }

}
