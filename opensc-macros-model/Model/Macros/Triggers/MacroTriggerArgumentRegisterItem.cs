using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public abstract class MacroTriggerArgumentRegisterItem<TKey, TObject> : MacroTriggerArgumentBase
        where TObject : class
    {

        private readonly ObjectRegisterBase<TKey, TObject> register;

        public MacroTriggerArgumentRegisterItem(ObjectRegisterBase<TKey, TObject> register) : base(typeof(TObject), MacroArgumentKeyType.String)
            => this.register = register;

        public override object GetObjectByKey(string key, object[] previousArgumentObjects) => register[StringKeyToTypedKey(key)];
        public override string GetKeyByObject(object obj) => TypedKeyToStringKey(register.GetKey(obj as TObject));
        protected abstract TKey StringKeyToTypedKey(string stringKey);
        protected string TypedKeyToStringKey(TKey typedKey) => typedKey?.ToString() ?? "";
        public override IEnumerable<object> GetPossibilities(object[] previousArgumentValues) => register;

    }

}
