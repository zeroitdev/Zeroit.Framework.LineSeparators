// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="GroupBoxLine.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.LineSeparators
{
    #region GroupBoxLine

    /// <summary>
    /// A class collection for rendering Line groupbox.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.GroupBox" />

    #region Old Designer Code
    //public class GroupBoxLineDesigner : ParentControlDesigner
    //{
    //    protected override void PostFilterProperties(System.Collections.IDictionary id)
    //    {
    //        id.Remove("FlatStyle");
    //        base.PostFilterProperties(id);
    //    }
    //    protected override void OnPaintAdornments(System.Windows.Forms.PaintEventArgs pe)
    //    {
    //        this.DrawBorder(pe.Graphics);
    //        base.OnPaintAdornments(pe);
    //    }

    //    private void DrawBorder(Graphics graphics)
    //    {
    //        Control control1 = base.Control;
    //        Rectangle rectangle1 = control1.ClientRectangle;
    //        Color borderColor;
    //        Color backColor = control1.BackColor;

    //        if (((double)backColor.GetBrightness()) < 0.5)
    //        {
    //            borderColor = ControlPaint.Light(control1.BackColor);
    //        }
    //        else
    //        {
    //            borderColor = ControlPaint.Dark(control1.BackColor);
    //        }
    //        Pen pen = new Pen(borderColor);
    //        pen.DashPattern = new float[] { 3, 1 };
    //        rectangle1.Width--;
    //        rectangle1.Height--;
    //        graphics.DrawRectangle(pen, rectangle1);
    //        pen.Dispose();
    //    }
    //} 
    #endregion

    [Designer(typeof(ZeroitGroupBoxLineDesigner))]
    [ToolboxBitmap(typeof(ZeroitGroupBoxLine), @"Toolbox_GroupBoxLine.bmp")]
    public class ZeroitGroupBoxLine : System.Windows.Forms.GroupBox
    {
        #region Private Fields
        /// <summary>
        /// Required designer variable.
        /// </summary>

        protected Color lineColor;
        /// <summary>
        /// The text color
        /// </summary>
        protected Color textColor;
        /// <summary>
        /// The components
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

        #region Public Properties

        // Add the 'LineColor' property

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        [Description("The line color."),
        Category("Appearance")]
        public Color LineColor
        {
            get
            {
                return this.lineColor;
            }
            set
            {
                this.lineColor = value;
                Invalidate();
            }
        }

        // Add the 'TextColor' property        
        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        [Description("The text color."),
        Category("Appearance")]
        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.textColor = value;
                Invalidate();
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitGroupBoxLine" /> class.
        /// </summary>
        public ZeroitGroupBoxLine()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);


            this.textColor = Color.LightGray;
            this.lineColor = Color.FromArgb(208, 208, 191);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(this.textColor);
            Pen pen = new Pen(this.lineColor);
            SizeF sizef = g.MeasureString(this.Text, this.Font);
            PointF pointf = new PointF(0, 0);

            g.DrawString(this.Text, this.Font, brush, pointf);
            g.DrawLine(pen, sizef.Width, sizef.Height / 2, this.Width, sizef.Height / 2);

            brush.Dispose();
            pen.Dispose();
        }

        #endregion

        #region Designer Generated Code

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
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
    /// Class ZeroitGroupBoxLineDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitGroupBoxLineDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;


        /// <summary>
        /// Posts the filter properties.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected override void PostFilterProperties(System.Collections.IDictionary id)
        {
            id.Remove("FlatStyle");
            base.PostFilterProperties(id);
        }
        /// <summary>
        /// Receives a call when the control that the designer is managing has painted its surface so the designer can paint any additional adornments on top of the control.
        /// </summary>
        /// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> the designer can use to draw on the control.</param>
        protected override void OnPaintAdornments(System.Windows.Forms.PaintEventArgs pe)
        {

            this.DrawBorder(pe.Graphics);
            base.OnPaintAdornments(pe);
        }

        /// <summary>
        /// Draws the border.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        private void DrawBorder(Graphics graphics)
        {
            Control control1 = base.Control;
            Rectangle rectangle1 = control1.ClientRectangle;
            Color borderColor;
            Color backColor = control1.BackColor;

            if (((double)backColor.GetBrightness()) < 0.5)
            {
                borderColor = ControlPaint.Light(control1.BackColor);
            }
            else
            {
                borderColor = ControlPaint.Dark(control1.BackColor);
            }
            Pen pen = new Pen(borderColor);
            pen.DashPattern = new float[] { 3, 1 };
            rectangle1.Width--;
            rectangle1.Height--;
            graphics.DrawRectangle(pen, rectangle1);
            pen.Dispose();
        }


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
                    actionLists.Add(new ZeroitGroupBoxLineSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitGroupBoxLineSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitGroupBoxLineSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitGroupBoxLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitGroupBoxLineSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitGroupBoxLineSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitGroupBoxLine;

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
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get
            {
                return colUserControl.TextColor;
            }
            set
            {
                GetPropertyByName("TextColor").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("LineColor",
                                 "Line Color", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("TextColor",
                                 "Text Color", "Appearance",
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
}
