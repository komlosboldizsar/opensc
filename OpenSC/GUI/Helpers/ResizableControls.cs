using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Helpers
{

    // original by: @source https://www.codeproject.com/Tips/178587/Draggable-WinForms-Controls-2
    public static class ResizableControls
    {

        private static int RESIZE_ACCURACY = 3;
        public static void Resizable(this Control control,
            ResizingDirection resizableVertical = (ResizingDirection.ResizingForward | ResizingDirection.ResizingReverse),
            ResizingDirection resizableHorizontal = (ResizingDirection.ResizingForward | ResizingDirection.ResizingReverse))
        {

            if (resizables.ContainsKey(control))
                return;

            ResizingData data = new ResizingData()
            {
                verticalAllowed = resizableVertical,
                horizontalAllowed = resizableHorizontal,
                verticalCurrent = ResizingDirection.NoResizing,
                horizontalCurrent = ResizingDirection.NoResizing,
                minWidth = null,
                maxWidth = null,
                minHeight = null,
                maxHeight = null,
                originalLocation = control.Location,
                originalSize = control.Size,
                originalCursor = control.Cursor,
                resizingCursor = false
            };
            resizables.Add(control, data);

            control.MouseDown += new MouseEventHandler(control_MouseDown);
            control.MouseUp += new MouseEventHandler(control_MouseUp);
            control.MouseMove += new MouseEventHandler(control_MouseMove);

        }

        public static void ResizableMinMaxSize(this Control control, int? minWidth, int maxWidth, int? minHeight, int? maxHeight)
        {
            if (!resizables.TryGetValue(control, out ResizingData data))
                return;
            data.minWidth = minWidth;
            data.minHeight = minHeight;
            data.maxWidth = maxWidth;
            data.maxHeight = maxHeight;
        }

        public static void NotResizeable(this Control control)
        {
            if (!resizables.ContainsKey(control))
                return;
            control.MouseDown -= control_MouseDown;
            control.MouseUp -= control_MouseUp;
            control.MouseMove -= control_MouseMove;
            resizables.Remove(control);
        }
        static void control_MouseDown(object sender, MouseEventArgs e)
        {
            Control typedSender = (Control)sender;
            PossibleResizingDirection prd = getPossibleResizingDirection(typedSender, e);
            if ((prd.vertical != ResizingDirection.NoResizing) || (prd.horizontal != ResizingDirection.NoResizing))
            {
                mouseOffset = new Size(e.Location);
                ResizingData resizingData = resizables[typedSender];
                resizingData.originalLocation = typedSender.Location;
                resizingData.originalSize = typedSender.Size;
                resizingData.verticalCurrent = prd.vertical;
                resizingData.horizontalCurrent = prd.horizontal;
            }
            
        }
        static void control_MouseUp(object sender, MouseEventArgs e)
        {
            Control typedSender = (Control)sender;
            ResizingData resizingData = resizables[typedSender];
            resizingData.originalLocation = typedSender.Location;
            resizingData.originalSize = typedSender.Size;
            resizingData.verticalCurrent = ResizingDirection.NoResizing;
            resizingData.horizontalCurrent = ResizingDirection.NoResizing;
        }
        static void control_MouseMove(object sender, MouseEventArgs e)
        {

            Control typedSender = (Control)sender;
            ResizingData resizingData = resizables[typedSender];

            if ((resizingData.verticalCurrent == ResizingDirection.NoResizing) && (resizingData.horizontalCurrent == ResizingDirection.NoResizing))
            { // Cursor and start

                Cursor newCursor = null;
                PossibleResizingDirection prd = getPossibleResizingDirection(typedSender, e);
                if ((prd.vertical == prd.horizontal) && (prd.vertical != ResizingDirection.NoResizing))
                    newCursor = Cursors.SizeNWSE;
                else if ((prd.vertical != prd.horizontal) && (prd.vertical != ResizingDirection.NoResizing) && (prd.horizontal != ResizingDirection.NoResizing))
                    newCursor = Cursors.SizeNESW;
                else if (prd.vertical != ResizingDirection.NoResizing)
                    newCursor = Cursors.SizeNS;
                else if (prd.horizontal != ResizingDirection.NoResizing)
                    newCursor = Cursors.SizeWE;

                if (newCursor != null)
                {
                    if (!resizingData.resizingCursor)
                        resizingData.originalCursor = typedSender.Cursor;
                    resizingData.resizingCursor = true;
                    typedSender.Cursor = newCursor;
                }
                else
                {
                    resizingData.resizingCursor = false;
                    typedSender.Cursor = resizingData.originalCursor;
                }

            }
            else
            { // Resizing

                Point newMouseOffset = (e.Location - mouseOffset) + new Size(typedSender.Location - new Size(resizingData.originalLocation));
                SizeChange scH = getNewSize(resizingData.horizontalCurrent, resizingData.originalSize.Width, newMouseOffset.X, resizingData.minWidth, resizingData.maxWidth);
                SizeChange scV = getNewSize(resizingData.verticalCurrent, resizingData.originalSize.Height, newMouseOffset.Y, resizingData.minHeight, resizingData.maxHeight);
                typedSender.Width = scH.newSize;
                typedSender.Height = scV.newSize;
                typedSender.Location = resizingData.originalLocation + new Size(scH.locationDelta, scV.locationDelta);

            }

        }

        #region Getting new size for one dimension
        private static SizeChange getNewSize(ResizingDirection direction, int originalSize, int mouseOffset, int? min, int? max)
        {

            SizeChange result = new SizeChange()
            {
                newSize = originalSize,
                sizeChange = 0,
                locationDelta = 0
            };

            int _newSize = 0;
            switch (direction)
            {
                case ResizingDirection.ResizingForward:
                    _newSize = originalSize + mouseOffset;
                    if (((min == null) || (min <= _newSize)) && ((max == null) || (max >= _newSize)))
                    {
                        result.newSize = _newSize;
                        result.sizeChange = mouseOffset;
                    }
                    else
                    {
                        if ((min != null) && (_newSize < min))
                        {
                            result.newSize = (int)min;
                            result.sizeChange = result.newSize - originalSize;
                        }
                        else if ((max != null) && (_newSize > min))
                        {
                            result.newSize = (int)max;
                            result.sizeChange = result.newSize - originalSize;
                        }
                    }
                    break;
                case ResizingDirection.ResizingReverse:
                    _newSize = originalSize - mouseOffset;
                    if (((min == null) || (min <= _newSize)) && ((max == null) || (max >= _newSize)))
                    {
                        result.newSize = _newSize;
                        result.sizeChange = -mouseOffset;
                        result.locationDelta = mouseOffset;
                    }
                    else
                    {
                        if ((min != null) && (_newSize < min))
                        {
                            result.newSize = (int)min;
                            result.sizeChange = result.newSize - originalSize;
                            result.locationDelta = -result.sizeChange;
                        }
                        else if ((max != null) && (_newSize > min))
                        {
                            result.newSize = (int)max;
                            result.sizeChange = result.newSize - originalSize;
                            result.locationDelta = -result.sizeChange;
                        }
                    }
                    break;
                case ResizingDirection.NoResizing:
                default:
                    break;
            }

            return result;

        }

        private class SizeChange
        {
            public int newSize;
            public int sizeChange;
            public int locationDelta;
        }
        #endregion

        #region Data store
        private static Dictionary<Control, ResizingData> resizables = new Dictionary<Control, ResizingData>();

        private static Size mouseOffset;

        [Flags]
        public enum ResizingDirection
        {
            NoResizing = 0,
            ResizingForward = 1,
            ResizingReverse = 2
        }
        private class ResizingData
        {
            public ResizingDirection verticalAllowed;
            public ResizingDirection horizontalAllowed;
            public ResizingDirection verticalCurrent;
            public ResizingDirection horizontalCurrent;
            public int? minWidth;
            public int? maxWidth;
            public int? minHeight;
            public int? maxHeight;
            public Point originalLocation;
            public Size originalSize;
            public Cursor originalCursor;
            public bool resizingCursor;
        }
        #endregion

        #region Determining resizing diretion and mode
        private static PossibleResizingDirection getPossibleResizingDirection(Control control, MouseEventArgs e)
        {

            PossibleResizingDirection result = new PossibleResizingDirection()
            {
                vertical = ResizingDirection.NoResizing,
                horizontal = ResizingDirection.NoResizing
            };

            if (!resizables.TryGetValue(control, out ResizingData resizingData))
                return result;

            int diffLeft = e.Location.X;
            int diffRight = Math.Abs(control.Width - 1 - e.Location.X);
            int diffTop = e.Location.Y;
            int diffBottom = Math.Abs(control.Height - 1 - e.Location.Y);

            if (diffLeft <= RESIZE_ACCURACY)
                result.horizontal = ResizingDirection.ResizingReverse;
            else if (diffRight <= RESIZE_ACCURACY)
                result.horizontal = ResizingDirection.ResizingForward;

            if (diffTop <= RESIZE_ACCURACY)
                result.vertical = ResizingDirection.ResizingReverse;
            else if (diffBottom <= RESIZE_ACCURACY)
                result.vertical = ResizingDirection.ResizingForward;

            result.vertical &= resizingData.verticalAllowed;
            result.horizontal &= resizingData.horizontalAllowed;

            return result;

        }
        private class PossibleResizingDirection
        {
            public ResizingDirection vertical;
            public ResizingDirection horizontal;
        }
        #endregion

    }

}
