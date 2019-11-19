using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public class Label : INotifyPropertyChanged
    {
        public Label()
        { }

        public Label(string text, RouterInput routerInput)
        {
            this.text = text;
            this.RouterInput = routerInput;
        }

        public void Restored()
        { }

        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();
                if (value == text)
                    return;
                string oldText = text;
                text = value;
                TextChanged?.Invoke(this, oldText, value);
                PropertyChanged?.Invoke(nameof(Text));
            }
        }

        public delegate void TextChangedDelegate(Label input, string oldText, string newText);
        public event TextChangedDelegate TextChanged;

        public RouterInput RouterInput { get; internal set; }

        // "Temp foreign key"
        public int _routerId;
        public int _routerInputIndex;

        public void RestoreRouterInput()
        {
            RouterInput = RouterDatabase.Instance.GetTById(_routerId).Inputs[_routerInputIndex];
        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;
        #endregion

    }
}
