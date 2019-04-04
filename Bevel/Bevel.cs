// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="Bevel.cs" company="Zeroit Dev Technologies">
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
    #region Bevel Line

    #region Control    
    /// <summary>
    /// A class collection for rendering a line.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitBevelLineDesigner))]
    public class ZeroitBevelLine : System.Windows.Forms.Control
    {
        #region Events
        /// <summary>
        /// Occurs when [orientation changed].
        /// </summary>
        public event EventHandler OrientationChanged;
        #endregion

        #region Private Properties
        /// <summary>
        /// The bevel line width
        /// </summary>
        private int bevelLineWidth;
        /// <summary>
        /// The top line color
        /// </summary>
        private Color topLineColor;
        /// <summary>
        /// The bottom line color
        /// </summary>
        private Color bottomLineColor;
        /// <summary>
        /// The blend
        /// </summary>
        private bool blend;
        /// <summary>
        /// The angle
        /// </summary>
        private int angle;
        /// <summary>
        /// The orientation
        /// </summary>
        private Orientation orientation;
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitBevelLine" /> class.
        /// </summary>
        public ZeroitBevelLine()
        {
            this.SetStyle(ControlStyles.UserPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint, true);

            bevelLineWidth = 1;
            topLineColor = SystemColors.ControlDark;
            bottomLineColor = SystemColors.ControlLightLight;
            orientation = Orientation.Horizontal;
            blend = false;
            angle = 90;
        }

        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the width of each line.
        /// </summary>
        /// <value>The width of each line.</value>
        [Description("The width of each line."), DefaultValue(1)]
        public int BevelLineWidth
        {
            get
            {
                return bevelLineWidth;
            }
            set
            {
                bevelLineWidth = value;
                OnResize(null);
            }
        }

        /// <summary>
        /// Gets or sets the color of the top line.
        /// </summary>
        /// <value>The color of the top line.</value>
        [Description(""), DefaultValue(typeof(Color), "ControlDark")]
        public Color TopLineColor
        {
            get
            {
                return topLineColor;
            }
            set
            {
                topLineColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the bottom line.
        /// </summary>
        /// <value>The color of the bottom line.</value>
        [Description(""), DefaultValue(typeof(Color), "ControlLightLight")]
        public Color BottomLineColor
        {
            get
            {
                return bottomLineColor;
            }
            set
            {
                bottomLineColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the orientation.Default value is Horizontal.
        /// </summary>
        /// <value>The orientation.</value>
        [Description(""), DefaultValue(Orientation.Horizontal)]
        public System.Windows.Forms.Orientation Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                orientation = value;
                if (orientation == Orientation.Horizontal)
                {
                    this.Width = this.Height;
                    Angle = 90;
                }
                else
                {
                    this.Height = this.Width;
                    Angle = 0;
                }

                OnResize(null);

                if (OrientationChanged != null)
                {
                    OrientationChanged(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the two colors will be blended together.
        /// </summary>
        /// <value><c>true</c> if two colors will be blended together; otherwise, <c>false</c>.</value>
        [Description("If true then the two colors will be blended together."), DefaultValue(false)]
        public bool Blend
        {
            get
            {
                return blend;
            }
            set
            {
                blend = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        /// <value>The angle.</value>
        [Description(""), Browsable(false)]
        public int Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                this.Invalidate();

            }
        }

        #endregion

        #region Overriden Functions / Events
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            Graphics g = e.Graphics;

            SolidBrush topBrush = new SolidBrush(topLineColor);
            SolidBrush bottomBrush = new SolidBrush(bottomLineColor);

            Rectangle blendRect;
            Rectangle topRect;
            Rectangle bottomRect;

            if (orientation == Orientation.Horizontal)
            {
                if (blend)
                {
                    blendRect = new Rectangle(0, 0, this.Width, this.Height);
                    g.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(blendRect, topLineColor, bottomLineColor, angle, false), blendRect);
                }
                else
                {
                    topRect = new Rectangle(0, 0, this.Width, bevelLineWidth);
                    bottomRect = new Rectangle(0, bevelLineWidth, this.Width, bevelLineWidth * 2);
                    g.FillRectangle(topBrush, topRect);
                    g.FillRectangle(bottomBrush, bottomRect);
                }
            }
            else
            {
                if (blend)
                {
                    blendRect = new Rectangle(0, 0, this.Width, this.Height);
                    g.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(blendRect, topLineColor, bottomLineColor, angle, false), blendRect);
                }
                else
                {
                    topRect = new Rectangle(0, 0, bevelLineWidth, this.Height);
                    bottomRect = new Rectangle(bevelLineWidth, 0, bevelLineWidth * 2, this.Height);
                    g.FillRectangle(topBrush, topRect);
                    g.FillRectangle(bottomBrush, bottomRect);
                }
            }
        }

        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    //base.OnPaintBackground (pevent);
        //}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if (orientation == Orientation.Horizontal)
            {
                this.Height = bevelLineWidth * 2;
            }
            else
            {
                this.Width = bevelLineWidth * 2;
            }
            this.Invalidate();
        }

        #endregion

        #region Hidden Properties / Events        
        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>The color of the back.</value>
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        [Browsable(false)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets the background image displayed in the control.
        /// </summary>
        /// <value>The background image.</value>
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        #endregion




        #region Transparency


        #region Include in Paint

        /// <summary>
        /// Transes the in paint.
        /// </summary>
        /// <param name="g">The g.</param>
        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        /// <summary>
        /// The allow transparency
        /// </summary>
        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [allow transparency].
        /// </summary>
        /// <value><c>true</c> if [allow transparency]; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// Makes the transparent.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="g">The g.</param>
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
    #endregion

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitBevelLineDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitBevelLineDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitBevelLineSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitBevelLineSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitBevelLineSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitBevelLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitBevelLineSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitBevelLineSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitBevelLine;

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
        /// Gets or sets the color of the top line.
        /// </summary>
        /// <value>The color of the top line.</value>
        public Color TopLineColor
        {
            get
            {
                return colUserControl.TopLineColor;
            }
            set
            {
                GetPropertyByName("TopLineColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the bottom line.
        /// </summary>
        /// <value>The color of the bottom line.</value>
        public Color BottomLineColor
        {
            get
            {
                return colUserControl.BottomLineColor;
            }
            set
            {
                GetPropertyByName("BottomLineColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the bevel line.
        /// </summary>
        /// <value>The width of the bevel line.</value>
        public int BevelLineWidth
        {
            get
            {
                return colUserControl.BevelLineWidth;
            }
            set
            {
                GetPropertyByName("BevelLineWidth").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public System.Windows.Forms.Orientation Orientation
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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitBevelLineSmartTagActionList"/> is blend.
        /// </summary>
        /// <value><c>true</c> if blend; otherwise, <c>false</c>.</value>
        public bool Blend
        {
            get
            {
                return colUserControl.Blend;
            }
            set
            {
                GetPropertyByName("Blend").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        /// <value>The angle.</value>
        public int Angle
        {
            get
            {
                return colUserControl.Angle;
            }
            set
            {
                GetPropertyByName("Angle").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("TopLineColor",
                                 "Top Line Color", "Appearance",
                                 "Sets the top line color."));

            items.Add(new DesignerActionPropertyItem("BottomLineColor",
                                 "Bottom Line Color", "Appearance",
                                 "Sets the bottom line color."));

            items.Add(new DesignerActionPropertyItem("BevelLineWidth",
                                 "Bevel Line Width", "Appearance",
                                 "Sets the width of the line."));

            items.Add(new DesignerActionPropertyItem("Orientation",
                                 "Orientation", "Appearance",
                                 "Sets the orientation. Horizontal or Vertical."));

            items.Add(new DesignerActionPropertyItem("Blend",
                                 "Blend", "Appearance",
                                 "Set to enable blend."));

            items.Add(new DesignerActionPropertyItem("Angle",
                                 "Angle", "Appearance",
                                 "Sets the blend angle."));

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
