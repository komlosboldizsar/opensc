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

        public int DividerWidth { get; private set; }

        public DataGridViewCellStyle CellStyle { get; private set; }

        public delegate void CellInitializerMethodDelegate(T item, DataGridViewCell cell);
        public CellInitializerMethodDelegate InitializerMethod;

        public delegate void CellUpdaterMethodDelegate(T item, DataGridViewCell cell);
        public CellUpdaterMethodDelegate UpdaterMethod { get; private set; }

        public delegate object[] CellDropDownPopulatorMethodDelegate(T item, DataGridViewCell cell);
        public CellDropDownPopulatorMethodDelegate DropDownPopulatorMethod;

        public delegate void CellContentClickHandlerMethodDelegate(T item, DataGridViewCell cell, DataGridViewCellEventArgs eventArgs);
        public CellContentClickHandlerMethodDelegate ContentClickHandlerMethod { get; private set; }

        public delegate void CellDoubleClickHandlerMethodDelegate(T item, DataGridViewCell cell, DataGridViewCellEventArgs eventArgs);
        public CellDoubleClickHandlerMethodDelegate DoubleClickHandlerMethod { get; private set; }

        public delegate void CellEndEditHandlerMethodDelegate(T item, DataGridViewCell cell, DataGridViewCellEventArgs eventArgs);
        public CellEndEditHandlerMethodDelegate EndEditHandlerMethod { get; private set; }

        public string[] ChangeEvents { get; private set; }

        public delegate void CellUpdaterMethodInvokerDelegate();
        public delegate void ExternalUpdateEventSubscriberMethodDelegate(T item, CellUpdaterMethodInvokerDelegate updateMethodInvoker);
        public ExternalUpdateEventSubscriberMethodDelegate ExternalUpdateEventSubscriberMethod { get; private set; }

        public bool TextEditable { get; private set; }

        public string ButtonText { get; private set; }

        public Image ButtonImage { get; set; }

        public Padding ButtonImagePadding { get; set; }

        public bool IconShown { get; set; }

        public Color IconColor { get; set; }

        public DataGridViewSmallIconCell.IconTypes IconType { get; set; }

        public Padding IconPadding { get; set; }

        public CustomDataGridViewColumnDescriptor(
                DataGridViewColumnType type,
                string header,
                int width,
                int dividerWidth,
                DataGridViewCellStyle cellStyle,
                CellInitializerMethodDelegate initializerMethod,
                CellUpdaterMethodDelegate updaterMethod,
                CellDropDownPopulatorMethodDelegate dropDownPopulatorMethod,
                CellContentClickHandlerMethodDelegate contentClickHandlerMethod,
                CellDoubleClickHandlerMethodDelegate doubleClickHandlerMethod,
                CellEndEditHandlerMethodDelegate endEditHandlerMethod,
                string[] changeEvents,
                ExternalUpdateEventSubscriberMethodDelegate externalUpdateEventSubscriberMethod,
                bool textEditable,
                string buttonText,
                Image buttonImage,
                Padding buttonImagePadding,
                bool iconShown,
                Color iconColor,
                DataGridViewSmallIconCell.IconTypes iconType,
                Padding iconPadding)
        {
            Type = type;
            Header = header;
            Width = width;
            DividerWidth = dividerWidth;
            CellStyle = cellStyle;
            InitializerMethod = initializerMethod;
            UpdaterMethod = updaterMethod;
            DropDownPopulatorMethod = dropDownPopulatorMethod;
            ContentClickHandlerMethod = contentClickHandlerMethod;
            DoubleClickHandlerMethod = doubleClickHandlerMethod;
            EndEditHandlerMethod = endEditHandlerMethod;
            ChangeEvents = changeEvents;
            ExternalUpdateEventSubscriberMethod = externalUpdateEventSubscriberMethod;
            TextEditable = textEditable;
            ButtonText = buttonText;
            ButtonImage = buttonImage;
            ButtonImagePadding = buttonImagePadding;
            IconShown = iconShown;
            IconColor = iconColor;
            IconType = iconType;
            IconPadding = iconPadding;
        }

    }
}
