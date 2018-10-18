using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public class CustomDataGridViewColumnDescriptor<T>
    {

        public DataGridViewColumnType Type { get; private set; }

        public string Header { get; private set; }

        public int Width { get; private set; }

        public delegate void CellInitializerMethodDelegate(T item, DataGridViewCell cell);
        public CellInitializerMethodDelegate InitializerMethod;

        public delegate void CellUpdaterMethodDelegate(T item, DataGridViewCell cell);
        public CellUpdaterMethodDelegate UpdaterMethod { get; private set; }

        public delegate void CellContentClickHandlerMethodDelegate(T item, DataGridViewCell cell, DataGridViewCellEventArgs eventArgs);
        public CellContentClickHandlerMethodDelegate ContentClickHandlerMethod { get; private set; }

        public delegate void CellEndEditHandlerMethodDelegate(T item, DataGridViewCell cell, DataGridViewCellEventArgs eventArgs);
        public CellEndEditHandlerMethodDelegate EndEditHandlerMethod { get; private set; }

        public string[] ChangeEvents { get; private set; }

        public bool TextEditable { get; private set; }

        public string ButtonText { get; private set; }

        public Image ButtonImage { get; set; }

        public Padding ButtonImagePadding { get; set; }

        public CustomDataGridViewColumnDescriptor(
                DataGridViewColumnType type,
                string header,
                int width,
                CellInitializerMethodDelegate initializerMethod,
                CellUpdaterMethodDelegate updaterMethod,
                CellContentClickHandlerMethodDelegate contentClickHandlerMethod,
                CellEndEditHandlerMethodDelegate endEditHandlerMethod,
                string[] changeEvents,
                bool textEditable,
                string buttonText,
                Image buttonImage,
                Padding buttonImagePadding)
        {
            Type = type;
            Header = header;
            Width = width;
            InitializerMethod = initializerMethod;
            UpdaterMethod = updaterMethod;
            ContentClickHandlerMethod = contentClickHandlerMethod;
            EndEditHandlerMethod = endEditHandlerMethod;
            ChangeEvents = changeEvents;
            TextEditable = textEditable;
            ButtonText = buttonText;
            ButtonImage = buttonImage;
            ButtonImagePadding = buttonImagePadding;
        }

    }
}
