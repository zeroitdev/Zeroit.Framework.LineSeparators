// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="HV.cs" company="Zeroit Dev Technologies">
//    This program is for creating Line/Seperator controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.LineSeparators
{
    #region HVLine

    #region Control

    /// <summary>
    /// A class collection for rendering a horizontal and vertical line.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitHVLineDesigner))]
    public class ZeroitHVLine : Control
    {

        #region Private Fields
        /// <summary>
        /// The linetype
        /// </summary>
        private LineType _linetype = LineType.Horizontal;
        /// <summary>
        /// The line color
        /// </summary>
        private Color lineColor = Color.DarkGray;
        /// <summary>
        /// The line color1
        /// </summary>
        private Color lineColor1 = Color.WhiteSmoke;
        /// <summary>
        /// The line width
        /// </summary>
        private int lineWidth = 1;
        /// <summary>
        /// The dash cap
        /// </summary>
        private DashCap dashCap = DashCap.Flat;
        /// <summary>
        /// The dash style
        /// </summary>
        private DashStyle dashStyle = DashStyle.Solid;
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the dash cap.
        /// </summary>
        /// <value>The dash cap.</value>
        public DashCap DashCap
        {
            get { return dashCap; }
            set
            {
                dashCap = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The dash style.</value>
        public DashStyle DashStyle
        {
            get { return dashStyle; }
            set
            {
                dashStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor
        {
            get
            {
                return lineColor;
            }

            set
            {
                lineColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the line color.
        /// </summary>
        /// <value>The line color1.</value>
        public Color LineColor1
        {
            get
            {
                return lineColor1;
            }

            set
            {
                lineColor1 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        public int LineWidth
        {
            get { return lineWidth; }
            set
            {
                lineWidth = value;
                Invalidate();
            }
        }

        #region Smoothing Mode

        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.HighQuality;

        /// <summary>
        /// Gets or sets the smoothing.
        /// </summary>
        /// <value>The smoothing.</value>
        public SmoothingMode Smoothing
        {
            get { return smoothing; }
            set
            {
                smoothing = value;
                Invalidate();
            }
        }

        #endregion

        /// <summary>
        /// Enumeration for setting the orientation of <c><see cref="ZeroitHVLine" /></c> control;
        /// </summary>
        public enum LineType
        {
            /// <summary>
            /// The horizontal
            /// </summary>
            Horizontal,
            /// <summary>
            /// The vertical
            /// </summary>
            Vertical
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public LineType Orientation
        {
            get { return _linetype; }
            set
            {
                _linetype = value;
                Invalidate();
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitHVLine" /> class.
        /// </summary>
        public ZeroitHVLine()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);


        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Draw a Horizontal Rule
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        private void DrawhorizontalRule(PaintEventArgs e, int x, int y, int width)
        {
            if (width < 0) { width = 0; }
            Graphics grfx = e.Graphics;
            grfx.SmoothingMode = smoothing;
            //Pen penGray = new Pen(Color.FromKnownColor(System.Drawing.KnownColor.ControlDark), 1);
            Pen penGray = new Pen(lineColor, lineWidth);
            Pen penWhite = new Pen(lineColor1, lineWidth);

            penGray.DashStyle = dashStyle;
            penWhite.DashStyle = dashStyle;

            penGray.DashCap = dashCap;
            penWhite.DashStyle = dashStyle;

            grfx.DrawLine(penGray, x, y, x + width, y);
            grfx.DrawLine(penWhite, x, y + 1, x + width, y + 1);
        }

        /// <summary>
        /// Draw a Vertical Rule
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="height">The height.</param>
        private void DrawverticalRule(PaintEventArgs e, int x, int y, int height)
        {
            if (height < 0) { height = 0; }
            Graphics grfx = e.Graphics;
            grfx.SmoothingMode = smoothing;
            //Pen penGray = new Pen(Color.FromKnownColor(System.Drawing.KnownColor.ControlDark), 1);
            Pen penGray = new Pen(lineColor, lineWidth);
            Pen penWhite = new Pen(lineColor1, lineWidth);

            penGray.DashStyle = dashStyle;
            penWhite.DashStyle = dashStyle;

            penGray.DashCap = dashCap;
            penWhite.DashStyle = dashStyle;

            grfx.DrawLine(penGray, x, y, x, y + height);
            grfx.DrawLine(penWhite, x + 1, y, x + 1, y + height);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            base.OnPaint(e);

            switch (_linetype)
            {
                case LineType.Horizontal:
                    DrawhorizontalRule(e, 0, lineWidth, Width);
                    break;
                case LineType.Vertical:
                    DrawverticalRule(e, lineWidth, 0, Height);
                    break;
                default:
                    break;
            }
            

        }

        #endregion

        
        #region Transparency


        #region Include in Paint

        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;

                Invalidate();
            }
        }

        #endregion

        #region Method

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        private static void MakeTransparent(Control control, Graphics g)
        {
            var parent = control.Parent;
            if (parent == null) return;
            var bounds = control.Bounds;
            var siblings = parent.Controls;
            int index = siblings.IndexOf(control);
            Bitmap behind = null;
            for (int i = siblings.Count - 1; i > index; i--)
            {
                var c = siblings[i];
                if (!c.Bounds.IntersectsWith(bounds)) continue;
                if (behind == null)
                    behind = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                c.DrawToBitmap(behind, c.Bounds);
            }
            if (behind == null) return;
            g.DrawImage(behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel);
            behind.Dispose();
        }

        #endregion


        #endregion



    }


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitHVLineDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitHVLineDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new ZeroitHVLineSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitHVLineSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitHVLineSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitHVLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitHVLineSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitHVLineSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitHVLine;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="System.ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the dash cap.
        /// </summary>
        /// <value>The dash cap.</value>
        public DashCap DashCap
        {
            get
            {
                return colUserControl.DashCap;
            }
            set
            {
                GetPropertyByName("DashCap").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The dash style.</value>
        public DashStyle DashStyle
        {
            get
            {
                return colUserControl.DashStyle;
            }
            set
            {
                GetPropertyByName("DashStyle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor
        {
            get
            {
                return colUserControl.LineColor;
            }
            set
            {
                GetPropertyByName("LineColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the line color1.
        /// </summary>
        /// <value>The line color1.</value>
        public Color LineColor1
        {
            get
            {
                return colUserControl.LineColor1;
            }
            set
            {
                GetPropertyByName("LineColor1").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        public int LineWidth
        {
            get
            {
                return colUserControl.LineWidth;
            }
            set
            {
                GetPropertyByName("LineWidth").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets the smoothing.
        /// </summary>
        /// <value>The smoothing.</value>
        public SmoothingMode Smoothing
        {
            get
            {
                return colUserControl.Smoothing;
            }
            set
            {
                GetPropertyByName("Smoothing").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Zeroit.Framework.LineSeparators.ZeroitHVLine.LineType Orientation
        {
            get
            {
                return colUserControl.Orientation;
            }
            set
            {
                GetPropertyByName("Orientation").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("DashCap",
                                 "Dash Cap", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("DashStyle",
                                 "Dash Style", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("LineColor",
                                 "Line Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("LineColor1",
                                 "Line Color1", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("LineWidth",
                                 "Line Width", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("Smoothing",
                                 "Smoothing", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("Orientation",
                                 "Orientation", "Appearance",
                                 "Type few characters to filter Cities."));

            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Product: ");
            location.Append(colUserControl.ProductName);
            StringBuilder size = new StringBuilder("Version: ");
            size.Append(colUserControl.ProductVersion);
            items.Add(new DesignerActionTextItem(location.ToString(),
                             "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(),
                             "Information"));

            return items;
        }

        #endregion




    }

    #endregion

    #endregion


    #endregion

    #endregion
}
