// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="SeparatorLine.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.LineSeparators
{
    #region Line

    /// <summary>
    /// A class collection for rendering a line seperator.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitSeparatorLineDesigner))]
    public class ZeroitSeparatorLine : Control
    {

        #region " Drawing "


        /// <summary>
        /// The g
        /// </summary>
        private Graphics G;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSeparatorLine"/> class.
        /// </summary>
        public ZeroitSeparatorLine()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        /// <summary>
        /// The color line
        /// </summary>
        private Color colorLine = Color.White;
        /// <summary>
        /// The style
        /// </summary>
        private LineStyle style = LineStyle.Solid;

        /// <summary>
        /// Gets or sets the line style.
        /// </summary>
        /// <value>The line style.</value>
        public LineStyle Style
        {
            get { return style; }
            set
            {
                style = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Enum for setting line drawing style.
        /// </summary>
        public enum LineStyle
        {
            /// <summary>
            /// The custom
            /// </summary>
            Custom,
            /// <summary>
            /// The dash
            /// </summary>
            Dash,
            /// <summary>
            /// The dash dot
            /// </summary>
            DashDot,
            /// <summary>
            /// The dash dot dot
            /// </summary>
            DashDotDot,
            /// <summary>
            /// The dot
            /// </summary>
            Dot,
            /// <summary>
            /// The solid
            /// </summary>
            Solid
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color line.</value>
        public Color ColorLine
        {
            get { return colorLine; }
            set
            {
                colorLine = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            base.OnPaint(e);

            using (Pen P = new Pen(colorLine))
            {
                switch (style)
                {
                    case LineStyle.Custom:
                        P.DashStyle = DashStyle.Custom;
                        G.DrawLine(P, new Point(0, 0), new Point(Width, 0));
                        break;
                    case LineStyle.Dash:
                        P.DashStyle = DashStyle.Dash;
                        G.DrawLine(P, new Point(0, 0), new Point(Width, 0));
                        break;
                    case LineStyle.DashDot:
                        P.DashStyle = DashStyle.DashDot;
                        G.DrawLine(P, new Point(0, 0), new Point(Width, 0));
                        break;
                    case LineStyle.DashDotDot:
                        P.DashStyle = DashStyle.DashDot;
                        G.DrawLine(P, new Point(0, 0), new Point(Width, 0));
                        break;
                    case LineStyle.Dot:
                        P.DashStyle = DashStyle.Dot;
                        G.DrawLine(P, new Point(0, 0), new Point(Width, 0));
                        break;
                    case LineStyle.Solid:
                        P.DashStyle = DashStyle.Solid;
                        G.DrawLine(P, new Point(0, 0), new Point(Width, 0));
                        break;
                    default:
                        break;
                }

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
    /// Class ZeroitSeparatorLineDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitSeparatorLineDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitSeparatorLineSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitSeparatorLineSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitSeparatorLineSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitSeparatorLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSeparatorLineSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitSeparatorLineSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitSeparatorLine;

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
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public Zeroit.Framework.LineSeparators.ZeroitSeparatorLine.LineStyle Style
        {
            get
            {
                return colUserControl.Style;
            }
            set
            {
                GetPropertyByName("Style").SetValue(colUserControl, value);
            }
        }
        /// <summary>
        /// Gets or sets the color line.
        /// </summary>
        /// <value>The color line.</value>
        public Color ColorLine
        {
            get
            {
                return colUserControl.ColorLine;
            }
            set
            {
                GetPropertyByName("ColorLine").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("Style",
                                 "Style", "Appearance",
                                 "Choose Line Style."));
            items.Add(new DesignerActionPropertyItem("ColorLine",
                                 "Color Line", "Appearance",
                                 "Sets the line color."));


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
