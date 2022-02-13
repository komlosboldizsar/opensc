using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenSC.GUI.Menus
{
    public class MenuItemGroup
    {

        public MenuItem this[string id] => GetItem(id, true);

        #region Property: Weight
        public delegate void WeightChangedDelegate(MenuItemGroup menuItemGroup, int oldWeight, int newWeight);
        public event WeightChangedDelegate WeightChanged;

        private int weight = 50;

        public int Weight
        {
            get { return weight; }
            set
            {
                int oldWeight = weight;
                if (oldWeight == value)
                    return;
                weight = value;
                WeightChanged?.Invoke(this, oldWeight, value);
            }
        }
        #endregion

        #region Items
        private Dictionary<string, MenuItem> items = new Dictionary<string, MenuItem>();
        public IReadOnlyList<MenuItem> Items => items.Values.ToList();

        public delegate void ItemAddedDelegate(MenuItemGroup group, MenuItem item, string id);
        public delegate void ItemRemovedDelegate(MenuItemGroup group, MenuItem item, string id);
        public delegate void GroupChangedDelegate(MenuItemGroup item);

        public event ItemAddedDelegate ItemAdded;
        public event ItemRemovedDelegate ItemRemoved;
        public event GroupChangedDelegate ItemChanged;

        public MenuItem GetItem(string id, bool createIfNotExist = true)
        {
            if (!items.TryGetValue(id, out MenuItem item))
                item = AddItem(id, id, null, null, null);
            return item;
        }

        public MenuItem AddItem(string id, string text, Bitmap image, object tag, MenuItem.MenuClickHandler clickHandler)
        {
            MenuItem child = new MenuItem(this, text, image, tag, clickHandler);
            addItem(id, child);
            return child;
        }

        private void addItem(string id, MenuItem newMenuItem)
        {
            items.Add(id, newMenuItem);
            ItemAdded?.Invoke(this, newMenuItem, id);
            ItemChanged?.Invoke(this);
        }

        public MenuItem RemoveItem(string id)
        {
            if (!items.TryGetValue(id, out MenuItem itemToRemove))
                return null;
            items.Remove(id);
            ItemRemoved?.Invoke(this, itemToRemove, id);
            ItemChanged?.Invoke(this);
            return itemToRemove;
        }
        #endregion

    }
}
