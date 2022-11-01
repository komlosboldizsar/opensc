using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    public class UmdTextCollection : ComponentCollection<Umd, UmdText, UmdTextCollection>
    {

        public UmdTextCollection(Umd owner) : base(owner)
        {
            int i = 0;
            foreach (UmdTextInfo textInfo in owner.TextInfo)
            {
                UmdText text = owner.CreateText();
                text.Info = textInfo;
                text.Index = i++;
                Add(text);
            }
        }

        protected override UmdText createInstance(string typeCode) => new();
        protected override string InstanceNameTemplate => "Text #{0}";

        internal void RestoredOwnFields()
        {
            int textCount = Count;
            int textInfoLength = owner.TextInfo.Length;
            if (textCount > textInfoLength)
                for (int i = textCount - 1; i >= textInfoLength; i--)
                    RemoveByKey(i);
            if (textCount < textInfoLength)
                for (int i = textCount; i < textInfoLength; i++)
                    Add(owner.CreateText());
        }

        internal void RestoreCustomRelations()
        {
            foreach (UmdText text in this)
                text.RestoreCustomRelations();
        }

    }
}
