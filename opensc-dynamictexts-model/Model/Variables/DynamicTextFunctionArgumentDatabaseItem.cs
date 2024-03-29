﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public class DynamicTextFunctionArgumentDatabaseItem<TModel> : DynamicTextFunctionArgumentBase
        where TModel : class, IModel
    {

        private readonly DatabaseBase<TModel> database;

        public DynamicTextFunctionArgumentDatabaseItem(DatabaseBase<TModel> database) : base(typeof(TModel), DynamicTextFunctionArgumentType.Integer)
        {
            this.database = database;
        }

        public override object GetObjectByKey(object key, object[] previousArgumentObjects) => database.GetTById((int)key);

    }

}
