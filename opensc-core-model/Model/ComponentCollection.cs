using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    public abstract class ComponentCollection<TParent, TComponent, TCollection> :
        ObservableValueDictionary<int, TComponent>,
        IComponentCollection<TParent>,
        IInstantiatorCollection<TComponent>
        where TComponent : IComponent<TParent, TComponent, TCollection>
        where TCollection : ComponentCollection<TParent, TComponent, TCollection>
    {

        public ComponentCollection(TParent owner)
            : base(rp => rp.Index)
            => this.owner = owner;

        protected readonly TParent owner;

        public void ParentRemoved()
        {
            foreach (TComponent component in this)
                component.Removed();
        }

        public override void Add(TComponent component)
        {
            base.Add(component);
            component.IndexChanged += componentIndexChanged;
            component.AssignParent(owner, (TCollection)this);
        }

        public override bool Remove(TComponent port)
        {
            if (!base.RemoveValue(port))
                return false;
            port.IndexChanged -= componentIndexChanged;
            port.Removed();
            return true;
        }

        public void CheckKey(TComponent port, int newIndex)
            => ThrowIfKeyCantBeUsedForItem(newIndex, port);

        private void componentIndexChanged(TComponent port, int oldValue, int newValue)
            => ChangeKey(oldValue, newValue);


        public virtual TComponent CreateEmptyInstance(string typeCode = null)
            => createInstance(typeCode);

        public virtual TComponent CreateAndAddNextInstance(string typeCode = null)
        {
            TComponent port = createInstance(typeCode);
            int index = underlying.Keys.Max() + 1;
            port.Index = index;
            port.Name = string.Format(InstanceNameTemplate, index);
            Add(port);
            return port;
        }

        protected abstract TComponent createInstance(string typeCode);
        protected virtual string InstanceNameTemplate => "#{0}";

        public virtual Type GetType(string typeCode) => typeof(TComponent);

    }
}
