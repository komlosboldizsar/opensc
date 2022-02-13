using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public class DynamicTextFunctionArgumentRegisterItem<TKey, TObject> : DynamicTextFunctionArgumentBase
        where TObject : class
    {

        private readonly ObjectRegisterBase<TKey, TObject> register;

        public DynamicTextFunctionArgumentRegisterItem(ObjectRegisterBase<TKey, TObject> register)
            : base(typeof(TObject), getKeyFromArgumentType())
            => this.register = register;

        public override object GetObjectByKey(object key, object[] previousArgumentObjects) => register[(TKey)key];

        private static DynamicTextFunctionArgumentType getKeyFromArgumentType()
        {
            if (typeof(TKey) == typeof(int))
                return DynamicTextFunctionArgumentType.Integer;
            return DynamicTextFunctionArgumentType.String;
        }

    }

}
