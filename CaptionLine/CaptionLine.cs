// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="CaptionLine.cs" company="Zeroit Dev Technologies">
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
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.LineSeparators
{
    #region Caption Line

    #region Control

    /// <summary>
    /// A class collection for rendering a nice shaded line separator.
    /// <br></br>
    /// Can have an aligned text caption.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [DefaultProperty("Caption")]
    [ToolboxBitmap(typeof(System.Windows.Forms.GroupBox))]
    [Designer(typeof(ZeroitCaptionLineDesigner))]
    public class ZeroitCaptionLine : System.Windows.Forms.UserControl
    {

        #region Private Fields

        /// <summary>
        /// The components
        /// </summary>
        private System.ComponentModel.Container components = null;
        /// <summary>
        /// The caption
        /// </summary>
        private string _Caption = "Insert";
        /// <summary>
        /// The caption margin space
        /// </summary>
        private int _CaptionMarginSpace = 16;
        /// <summary>
        /// The caption padding
        /// </summary>
        private int _CaptionPadding = 2;
        /// <summary>
        /// The line vertical align
        /// </summary>
        private LineVerticalAlign _LineVerticalAlign = LineVerticalAlign.Middle;
        /// <summary>
        /// The caption orizontal align
        /// </summary>
        private CaptionOrizontalAlign _CaptionOrizontalAlign = CaptionOrizontalAlign.Left;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitCaptionLine"/> class.
        /// </summary>
        public ZeroitCaptionLine()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);


        }


        #endregion

        #region Public Properties


        /// <summary>
        /// The caption text displayed on the line.
        /// If the caption is "" (the default) the line is not broken
        /// </summary>
        /// <value>The caption.</value>
        [Category("Appearance")]
        [DefaultValue("")]
        [Description("The caption text displayed on the line. If the caption is \"\" (the default) the line is not broken")]
        public string Caption
        {
            get { return _Caption; }
            set
            {
                _Caption = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The distance in pixels form the control margin to caption text
        /// </summary>
        /// <value>The caption margin space.</value>
        [Category("Appearance")]
        [DefaultValue(16)]
        [Description("The distance in pixels form the control margin to caption text")]
        public int CaptionMarginSpace
        {
            get { return _CaptionMarginSpace; }
            set
            {
                _CaptionMarginSpace = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The space in pixels arrownd text caption
        /// </summary>
        /// <value>The caption padding.</value>
        [Category("Appearance")]
        [DefaultValue(2)]
        [Description("The space in pixels arrownd text caption")]
        public int CaptionPadding
        {
            get { return _CaptionPadding; }
            set
            {
                _CaptionPadding = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The vertical alignement of the line within the space of the control
        /// </summary>
        /// <value>The line vertical align.</value>
        [Category("Appearance")]
        [DefaultValue(LineVerticalAlign.Middle)]
        [Description("The vertical alignement of the line within the space of the control")]
        public LineVerticalAlign LineVerticalAlign
        {
            get { return _LineVerticalAlign; }
            set
            {
                _LineVerticalAlign = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Tell where the text caption is aligned in the control
        /// </summary>
        /// <value>The caption orizontal align.</value>
        [Category("Appearance")]
        [DefaultValue(CaptionOrizontalAlign.Left)]
        [Description("Tell where the text caption is aligned in the control")]
        public CaptionOrizontalAlign CaptionOrizontalAlign
        {
            get { return _CaptionOrizontalAlign; }
            set
            {
                _CaptionOrizontalAlign = value;
                Invalidate();
            }
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
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // NiceLine
            // 
            this.Name = "NiceLine";
            this.Size = new System.Drawing.Size(100, this.Font.Height);
        }
        #endregion

        //		protected override CreateParams CreateParams
        //		{
        //			get
        //			{
        //				CreateParams cp = base.CreateParams;
        //				cp.ExStyle |= 0x20;
        //				return cp;
        //			}
        //		}
        //
        //		protected override void OnMove(EventArgs e)
        //		{
        //			RecreateHandle();
        //		}
        //
        //		protected override void OnPaintBackground(PaintEventArgs e)
        //		{
        //			// do nothing
        //		}

        #endregion

        #region Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            base.OnPaint(e);

            int ym;
            switch (LineVerticalAlign)
            {
                case LineVerticalAlign.Top:
                    ym = 0;
                    break;
                case LineVerticalAlign.Middle:
                    ym = Convert.ToInt32(Math.Ceiling((decimal)Size.Height / 2)) - 1;
                    break;
                case LineVerticalAlign.Bottom:
                    ym = Size.Height - 2;
                    break;
                default:
                    ym = 0;
                    break;
            }

            SizeF captionSizeF = e.Graphics.MeasureString(Caption, this.Font, this.Width - CaptionMarginSpace * 2, StringFormat.GenericDefault);
            int captionLength = Convert.ToInt32(captionSizeF.Width);

            int beforeCaption;
            int afterCaption;

            if (Caption == "")
            {
                beforeCaption = CaptionMarginSpace;
                afterCaption = CaptionMarginSpace;
            }
            else
            {
                switch (CaptionOrizontalAlign)
                {
                    case CaptionOrizontalAlign.Left:
                        beforeCaption = CaptionMarginSpace;
                        afterCaption = CaptionMarginSpace + CaptionPadding * 2 + captionLength;
                        break;
                    case CaptionOrizontalAlign.Center:
                        beforeCaption = (Width - captionLength) / 2 - CaptionPadding;
                        afterCaption = (Width - captionLength) / 2 + captionLength + CaptionPadding;
                        break;
                    case CaptionOrizontalAlign.Right:
                        beforeCaption = Width - CaptionMarginSpace * 2 - captionLength;
                        afterCaption = Width - CaptionMarginSpace;
                        break;
                    default:
                        beforeCaption = CaptionMarginSpace;
                        afterCaption = CaptionMarginSpace;
                        break;
                }
            }

            // ------- 
            // |      ...caption...
            e.Graphics.DrawLines(new Pen(Color.DimGray, 1),
                new Point[] {
                                new Point(0, ym + 1),
                                new Point(0, ym),
                                new Point(beforeCaption, ym)
                            }
                );

            //                  -------
            //	      ...caption... 
            e.Graphics.DrawLines(new Pen(Color.DimGray, 1),
                new Point[] {
                                new Point(afterCaption, ym),
                                new Point(this.Width, ym)
                            }
                );

            //        ...caption...
            // -------
            e.Graphics.DrawLines(new Pen(Color.White, 1),
                new Point[] {
                                new Point(0, ym + 1),
                                new Point(beforeCaption, ym + 1)
                            }
                );

            //        ...caption...       |
            //                  -------
            e.Graphics.DrawLines(new Pen(Color.White, 1),
                new Point[] {
                                new Point(afterCaption, ym + 1),
                                new Point(this.Width, ym + 1),
                                new Point(this.Width, ym)
                            }
                );

            //        ...caption...
            if (Caption != "")
            {
                e.Graphics.DrawString(Caption, this.Font, new SolidBrush(this.ForeColor), beforeCaption + CaptionPadding, 1);
            }

            //			e.Graphics.DrawLines(new Pen(Color.Red, 1), 
            //				new Point[] { 
            //								new Point(0, 0), 
            //								new Point(this.Width-1, 0), 
            //								new Point(this.Width-1, this.Height-1),
            //								new Point(0, this.Height-1),
            //								new Point(0, 0)
            //							} 
            //				);
        }

        /// <summary>
        /// Handles the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            this.Height = this.Font.Height + 2;
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnFontChanged(System.EventArgs e)
        {
            this.OnResize(e);
            base.OnFontChanged(e);
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
    /// Class ZeroitCaptionLineDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitCaptionLineDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitCaptionLineSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitCaptionLineSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitCaptionLineSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitCaptionLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitCaptionLineSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitCaptionLineSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitCaptionLine;

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
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string Caption
        {
            get
            {
                return colUserControl.Caption;
            }
            set
            {
                GetPropertyByName("Caption").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the caption margin space.
        /// </summary>
        /// <value>The caption margin space.</value>
        public int CaptionMarginSpace
        {
            get
            {
                return colUserControl.CaptionMarginSpace;
            }
            set
            {
                GetPropertyByName("CaptionMarginSpace").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the caption padding.
        /// </summary>
        /// <value>The caption padding.</value>
        public int CaptionPadding
        {
            get
            {
                return colUserControl.CaptionPadding;
            }
            set
            {
                GetPropertyByName("CaptionPadding").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the line vertical align.
        /// </summary>
        /// <value>The line vertical align.</value>
        public LineVerticalAlign LineVerticalAlign
        {
            get
            {
                return colUserControl.LineVerticalAlign;
            }
            set
            {
                GetPropertyByName("LineVerticalAlign").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the caption orizontal align.
        /// </summary>
        /// <value>The caption orizontal align.</value>
        public CaptionOrizontalAlign CaptionOrizontalAlign
        {
            get
            {
                return colUserControl.CaptionOrizontalAlign;
            }
            set
            {
                GetPropertyByName("CaptionOrizontalAlign").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("Caption",
                                 "Caption", "Appearance",
                                 "Sets the caption of the the control."));

            items.Add(new DesignerActionPropertyItem("CaptionMarginSpace",
                                 "Caption Margin Space", "Appearance",
                                 "Sets the caption margin space of the control."));

            items.Add(new DesignerActionPropertyItem("CaptionPadding",
                                 "Caption Padding", "Appearance",
                                 "Sets the padding of the control."));

            items.Add(new DesignerActionPropertyItem("LineVerticalAlign",
                                 "Line Vertical Align", "Appearance",
                                 "Sets the vertical line alignment."));

            items.Add(new DesignerActionPropertyItem("CaptionOrizontalAlign",
                                 "Caption Orizontal Align", "Appearance",
                                 "Sets the caption horizontal alignment."));

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

    #region Public Enums

    /// <summary>
    /// Enum for setting the vertical alignment of <c><see cref="ZeroitCaptionLine" /></c>
    /// </summary>
    public enum LineVerticalAlign
    {
        /// <summary>
        /// The top
        /// </summary>
        Top,
        /// <summary>
        /// The middle
        /// </summary>
        Middle,
        /// <summary>
        /// The bottom
        /// </summary>
        Bottom
    }

    /// <summary>
    /// Enum for setting the caption alignment of <c><see cref="ZeroitCaptionLine" /></c>
    /// </summary>
    public enum CaptionOrizontalAlign
    {
        /// <summary>
        /// The left
        /// </summary>
        Left,
        /// <summary>
        /// The center
        /// </summary>
        Center,
        /// <summary>
        /// The right
        /// </summary>
        Right
    }

    #endregion

    #endregion
}
