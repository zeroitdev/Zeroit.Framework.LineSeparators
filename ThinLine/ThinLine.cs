// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="ThinLine.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Text;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.LineSeparators
{

    #region Helper Class For ZeroitThinLine
    /// <summary>
    /// Class Helpers1. This class cannot be inherited.
    /// </summary>
    internal sealed class Helpers1
    {

        /// <summary>
        /// Enum RoundingStyle
        /// </summary>
        public enum RoundingStyle : byte
        {
            /// <summary>
            /// All
            /// </summary>
            All,
            /// <summary>
            /// The top
            /// </summary>
            Top,
            /// <summary>
            /// The bottom
            /// </summary>
            Bottom,
            /// <summary>
            /// The left
            /// </summary>
            Left,
            /// <summary>
            /// The right
            /// </summary>
            Right,
            /// <summary>
            /// The top right
            /// </summary>
            TopRight,
            /// <summary>
            /// The bottom right
            /// </summary>
            BottomRight
        }

        /// <summary>
        /// Centers the string.
        /// </summary>
        /// <param name="G">The g.</param>
        /// <param name="T">The t.</param>
        /// <param name="F">The f.</param>
        /// <param name="C">The c.</param>
        /// <param name="R">The r.</param>
        public static void CenterString(Graphics G, string T, Font F, Color C, Rectangle R)
        {
            SizeF sizeF = G.MeasureString(T, F);
            using (SolidBrush solidBrush = new SolidBrush(C))
            {
                G.DrawString(T, F, solidBrush, checked(new Point((int)Math.Round(unchecked((double)R.Width / 2.0 - (double)(sizeF.Width / 2f))), (int)Math.Round(unchecked((double)R.Height / 2.0 - (double)(sizeF.Height / 2f))))));
            }
        }

        /// <summary>
        /// Colors from hexadecimal.
        /// </summary>
        /// <param name="Hex">The hexadecimal.</param>
        /// <returns>Color.</returns>
        public static Color ColorFromHex(string Hex)
        {
            return Color.FromArgb(checked((int)long.Parse(string.Format("FFFFFFFFFF{0}", Hex.Substring(1)), NumberStyles.HexNumber)));
        }

        /// <summary>
        /// Fulls the rectangle.
        /// </summary>
        /// <param name="S">The s.</param>
        /// <param name="Subtract">if set to <c>true</c> [subtract].</param>
        /// <returns>Rectangle.</returns>
        public static Rectangle FullRectangle(Size S, bool Subtract)
        {
            Rectangle result;
            if (Subtract)
            {
                result = checked(new Rectangle(0, 0, S.Width - 1, S.Height - 1));
            }
            else
            {
                result = new Rectangle(0, 0, S.Width, S.Height);
            }
            return result;
        }

        /// <summary>
        /// Rounds the rect.
        /// </summary>
        /// <param name="Rect">The rect.</param>
        /// <param name="Rounding">The rounding.</param>
        /// <param name="Style">The style.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath RoundRect(Rectangle Rect, int Rounding, Helpers1.RoundingStyle Style = Helpers1.RoundingStyle.All)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            checked
            {
                int num = Rounding * 2;
                graphicsPath.StartFigure();
                bool flag = Rounding == 0;
                GraphicsPath result;
                if (flag)
                {
                    graphicsPath.AddRectangle(Rect);
                    graphicsPath.CloseAllFigures();
                    result = graphicsPath;
                }
                else
                {
                    switch (Style)
                    {
                        case Helpers1.RoundingStyle.All:
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                            break;
                        case Helpers1.RoundingStyle.Top:
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                            graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                            break;
                        case Helpers1.RoundingStyle.Bottom:
                            graphicsPath.AddLine(new Point(Rect.X, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                            break;
                        case Helpers1.RoundingStyle.Left:
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                            graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                            break;
                        case Helpers1.RoundingStyle.Right:
                            graphicsPath.AddLine(new Point(Rect.X, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y));
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                            break;
                        case Helpers1.RoundingStyle.TopRight:
                            graphicsPath.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                            graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height - 1), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                            graphicsPath.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                            break;
                        case Helpers1.RoundingStyle.BottomRight:
                            graphicsPath.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                            graphicsPath.AddLine(new Point(Rect.X + Rect.Width - 1, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                            graphicsPath.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                            break;
                    }
                    graphicsPath.CloseAllFigures();
                    result = graphicsPath;
                }
                return result;
            }
        }
    }

    #endregion

    #region ZeroitThinLine

    /// <summary>
    /// A class collection for rendering a thin line.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitSeparatorThinLineDesigner))]
    public class ZeroitThinLine : Control
    {
        /// <summary>
        /// The g
        /// </summary>
        private Graphics G;
        /// <summary>
        /// The line color
        /// </summary>
        private Color lineColor = Color.Black;
        /// <summary>
        /// The line width
        /// </summary>
        private float lineWidth = 1f;

        /// <summary>
        /// The orientation
        /// </summary>
        private Orientation orientation = Orientation.Vertical;

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor
        {
            get { return lineColor; }
            set
            {
                lineColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        public float LineWidth
        {
            get { return lineWidth; }
            set
            {
                lineWidth = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitThinLine" /> class.
        /// </summary>
        public ZeroitThinLine()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            this.G = e.Graphics;
            this.G.SmoothingMode = SmoothingMode.HighQuality;
            this.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            using (Pen pen = new Pen(lineColor, lineWidth))
            {
                this.G.DrawLine(pen, new Point(0, 0), new Point(base.Width, 0));


            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Size = new Size(base.Width, 2);
        }




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
    /// Class ZeroitSeparatorThinLineDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitSeparatorThinLineDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitSeparatorThinLineSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitSeparatorThinLineSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitSeparatorThinLineSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitThinLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSeparatorThinLineSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitSeparatorThinLineSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitThinLine;

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
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        public float LineWidth
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


            items.Add(new DesignerActionPropertyItem("LineColor",
                                 "Line Color", "Appearance",
                                 "Sets the Line Color."));

            items.Add(new DesignerActionPropertyItem("LineWidth",
                                 "Line Width", "Appearance",
                                 "Sets the Line Width."));

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
