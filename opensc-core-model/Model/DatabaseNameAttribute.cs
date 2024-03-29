﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public DatabaseNameAttribute(string Name)
        {
            this.Name = Name;
        }

    }
}
