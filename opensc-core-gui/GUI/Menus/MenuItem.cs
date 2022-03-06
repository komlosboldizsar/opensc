using OpenSC.Model.Settings;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenSC.GUI.Menus
{
    public class MenuItem
    {

        public MenuItem(MenuItemGroup parentGroup, string text, Bitmap image, object tag, MenuClickHandler clickHandler)
        {
            this.ParentGroup = parentGroup;
            this.text = text;
            this.image = image;
            this.tag = tag;
            this.clickHandler = clickHandler;
        }

        public MenuItemGroup this[string id] => GetGroup(id, true);
        public MenuItemGroup ParentGroup { get; private set; }

        #region Property: Text
        public delegate void TextChangedDelegate(MenuItem menuItem, string oldText, string newText);
        public event TextChangedDelegate TextChanged;

        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                string oldText = text;
                if (oldText == value)
                    return;
                text = value;
                TextChanged?.Invoke(this, oldText, value);
            }
        }
        #endregion

        #region Property: Image
        public delegate void ImageChangedDelegate(MenuItem menuItem, Bitmap oldImage, Bitmap newImage);
        public event ImageChangedDelegate ImageChanged;

        private Bitmap image;

        public Bitmap Image
        {
            get { return image; }
            set
            {
                Bitmap oldImage = image;
                if (oldImage == value)
                    return;
                image = value;
                ImageChanged?.Invoke(this, oldImage, value);
            }
        }
        #endregion

        #region Property: Tag
        public delegate void TagChangedDelegate(MenuItem menuItem, object oldTag, object newTag);
        public event TagChangedDelegate TagChanged;

        private object tag;

        public object Tag
        {
            get { return tag; }
            set
            {
                object oldTag = image;
                if (oldTag == value)
                    return;
                tag = value;
                TagChanged?.Invoke(this, oldTag, value);
            }
        }
        #endregion

        #region Property: Weight
        public delegate void WeightChangedDelegate(MenuItem menuItem, int oldWeight, int newWeight);
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

        #region Property: BoundCheckedSetting
        public delegate void BoundCheckedSettingChangedDelegate(MenuItem menuItem, Setting<bool> oldSetting, Setting<bool> newSetting);
        public event BoundCheckedSettingChangedDelegate BoundCheckedSettingChanged;

        private Setting<bool> boundCheckedSetting;

        public Setting<bool> BoundCheckedSetting
        {
            get => boundCheckedSetting;
            set
            {
                Setting<bool> oldBoundCheckedSetting = boundCheckedSetting;
                if (boundCheckedSetting == value)
                    return;
                boundCheckedSetting = value;
                BoundCheckedSettingChanged?.Invoke(this, oldBoundCheckedSetting, value);
            }
        }
        #endregion

        #region Click handler
        public delegate void MenuClickHandler(MenuItem menuItem, object tag);
        private MenuClickHandler clickHandler;

        public MenuClickHandler ClickHandler
        {
            get => clickHandler;
            set => clickHandler = value;
        }

        public void HandleClick() => clickHandler?.Invoke(this, tag);
        #endregion

        #region Groups
        private Dictionary<string, MenuItemGroup> groups = new Dictionary<string, MenuItemGroup>();
        public IReadOnlyList<MenuItemGroup> Groups => groups.Values.ToList();

        public delegate void GroupAddedDelegate(MenuItem menu, MenuItemGroup group, string id);
        public delegate void GroupRemovedDelegate(MenuItem menu, MenuItemGroup group, string id);
        public delegate void GroupChangedDelegate(MenuItem menu);

        public event GroupAddedDelegate GroupAdded;
        public event GroupRemovedDelegate GroupRemoved;
        public event GroupChangedDelegate MenuChanged;

        public MenuItemGroup GetGroup(string id, bool createIfNotExist = true)
        {
            if (!groups.TryGetValue(id, out MenuItemGroup child))
                child = AddGroup(id, id, null, null, null);
            return child;
        }

        public MenuItemGroup AddGroup(string id, string text, Bitmap image, object tag, MenuClickHandler clickHandler)
        {
            MenuItemGroup group = new MenuItemGroup();
            addGroup(id, group);
            return group;
        }

        private void addGroup(string id, MenuItemGroup newMenuItem)
        {
            groups.Add(id, newMenuItem);
            GroupAdded?.Invoke(this, newMenuItem, id);
            MenuChanged?.Invoke(this);
        }

        public MenuItemGroup RemoveGroup(string id)
        {
            if (!groups.TryGetValue(id, out MenuItemGroup groupToRemove))
                return null;
            groups.Remove(id);
            GroupRemoved?.Invoke(this, groupToRemove, id);
            MenuChanged?.Invoke(this);
            return groupToRemove;
        }
        #endregion

    }
}
