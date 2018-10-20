using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Menus
{
    public class MenuItem
    {

        public MenuItem(string text, Bitmap image, object tag, MenuClickHandler clickHandler)
        {
            this.text = text;
            this.image = image;
            this.tag = tag;
            this.clickHandler = clickHandler;
        }

        public MenuItem this[string id]
        {
            get { return GetChild(id, true); }
        }

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

        public delegate void MenuClickHandler(MenuItem menuItem, object tag);

        private MenuClickHandler clickHandler;

        public MenuClickHandler ClickHandler
        {
            get { return clickHandler; }
            set { clickHandler = value; }
        }

        public void HandleClick()
        {
            clickHandler?.Invoke(this, tag);
        }

        #region Children
        private Dictionary<string, MenuItem> children = new Dictionary<string, MenuItem>();

        public IReadOnlyList<MenuItem> Children
        {
            get => children.Values.ToList();
        }

        public delegate void ChildAddedDelegate(MenuItem parent, MenuItem child, string id);
        public delegate void ChildRemovedDelegate(MenuItem parent, MenuItem child, string id);
        public delegate void ChildrenChangedDelegate(MenuItem menuItem);

        public event ChildAddedDelegate ChildAdded;
        public event ChildRemovedDelegate ChildRemoved;
        public event ChildrenChangedDelegate ChildrenChanged;

        public MenuItem GetChild(string id, bool createIfNotExist = true)
        {
            if (!children.TryGetValue(id, out MenuItem child))
                child = AddChild(id, id, null, null, null);
            return child;
        }

        public MenuItem AddChild(string id, string text, Bitmap image, object tag, MenuClickHandler clickHandler)
        {
            MenuItem child = new MenuItem(text, image, tag, clickHandler);
            addChild(id, child);
            return child;
        }

        public void AddSeparator(string id)
        {
            addChild(id, new SeparatorMenuItem());
        }

        private void addChild(string id, MenuItem newMenuItem)
        {
            children.Add(id, newMenuItem);
            ChildAdded?.Invoke(this, newMenuItem, id);
            ChildrenChanged?.Invoke(this);
        }

        public MenuItem RemoveChild(string id)
        {
            if (!children.TryGetValue(id, out MenuItem childToRemove))
                return null;
            children.Remove(id);
            ChildRemoved?.Invoke(this, childToRemove, id);
            ChildrenChanged?.Invoke(this);
            return childToRemove;
        }
        #endregion

    }
}
