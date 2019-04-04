// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="Divider.cs" company="Zeroit Dev Technologies">
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
    #region ZeroitDivider

    #region Control    
    /// <summary>
    /// A class collection for rendering a line divider.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.LineSeparators.ThemeControl154" />
    [Designer(typeof(ZeroitDividerDesigner))]
    public class ZeroitFadeLine : ThemeControl154
    {

        /// <summary>
        /// The orientation
        /// </summary>
        private Orientation _Orientation;

        /// <summary>
        /// The blend1
        /// </summary>
        private Color blend1 = Color.Transparent;
        /// <summary>
        /// The blend2
        /// </summary>
        private Color blend2 = Color.LightGray;
        /// <summary>
        /// The blend3
        /// </summary>
        private Color blend3 = Color.LightGray;
        /// <summary>
        /// The blend4
        /// </summary>
        private Color blend4 = Color.Transparent;
        /// <summary>
        /// The blend5
        /// </summary>
        private Color blend5 = Color.Transparent;
        /// <summary>
        /// The blend6
        /// </summary>
        private Color blend6 = Color.FromArgb(144, 144, 144);
        /// <summary>
        /// The blend7
        /// </summary>
        private Color blend7 = Color.FromArgb(160, 160, 160);
        /// <summary>
        /// The blend8
        /// </summary>
        private Color blend8 = Color.FromArgb(156, 156, 156);
        /// <summary>
        /// The blend9
        /// </summary>
        private Color blend9 = Color.Transparent;

        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend1.</value>
        public Color Blend1
        {
            get { return blend1; }
            set
            {
                blend1 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend2.</value>
        public Color Blend2
        {
            get { return blend2; }
            set
            {
                blend2 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend3.</value>
        public Color Blend3
        {
            get { return blend3; }
            set
            {
                blend3 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend4.</value>
        public Color Blend4
        {
            get { return blend4; }
            set
            {
                blend4 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend5.</value>
        public Color Blend5
        {
            get { return blend5; }
            set
            {
                blend5 = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend6.</value>
        public Color Blend6
        {
            get { return blend6; }
            set
            {
                blend6 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend7.</value>
        public Color Blend7
        {
            get { return blend7; }
            set
            {
                blend7 = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend8.</value>
        public Color Blend8
        {
            get { return blend8; }
            set
            {
                blend8 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color blend.
        /// </summary>
        /// <value>The blend9.</value>
        public Color Blend9
        {
            get { return blend9; }
            set
            {
                blend9 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the orientation. Either vertical or horizontal.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation
        {
            get { return _Orientation; }
            set
            {
                _Orientation = value;
                if (value == System.Windows.Forms.Orientation.Vertical)
                {
                    LockHeight = 0;
                    LockWidth = 14;
                }
                else
                {
                    LockHeight = 14;
                    LockWidth = 0;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitFadeLine" /> class.
        /// </summary>
        public ZeroitFadeLine()
        {
            Transparent = true;
            BackColor = Color.Transparent;
            LockHeight = 14;
        }

        /// <summary>
        /// Colors the hook.
        /// </summary>
        protected override void ColorHook()
        {
        }

        /// <summary>
        /// Paints the hook.
        /// </summary>
        protected override void PaintHook()
        {
            TransInPaint(G);
            //G.Clear(BackColor);
            ColorBlend BL1 = new ColorBlend();
            ColorBlend BL2 = new ColorBlend();
            BL1.Positions = new float[] {
                    0.0F,
                    0.15F,
                    0.85F,
                    1.0F};
            BL2.Positions = new float[] {
                    0.0F,
                    0.15F,
                    0.5F,
                    0.85F,
                    1.0F};
            BL1.Colors = new Color[] {
                    blend1,
                    blend2,
                    blend3,
                    blend4};
            BL2.Colors = new Color[] {
                    blend5,
                    blend6,
                    blend7,
                    blend8,
                    blend9};
            if (_Orientation == System.Windows.Forms.Orientation.Vertical)
            {
                DrawGradient(BL1, 6, 0, 1, Height);
                DrawGradient(BL2, 7, 0, 1, Height);
            }
            else
            {
                DrawGradient(BL1, 0, 6, Width, 1, 0.0F);
                DrawGradient(BL2, 0, 7, Width, 1, 0.0F);
            }
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
    #endregion


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitDividerDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitDividerDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitDividerSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitDividerSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitDividerSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitFadeLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitDividerSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitDividerSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitFadeLine;

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
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation
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
        /// Gets or sets the blend1.
        /// </summary>
        /// <value>The blend1.</value>
        public Color Blend1
        {
            get
            {
                return colUserControl.Blend1;
            }
            set
            {
                GetPropertyByName("Blend1").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the blend2.
        /// </summary>
        /// <value>The blend2.</value>
        public Color Blend2
        {
            get
            {
                return colUserControl.Blend2;
            }
            set
            {
                GetPropertyByName("Blend2").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the blend3.
        /// </summary>
        /// <value>The blend3.</value>
        public Color Blend3
        {
            get
            {
                return colUserControl.Blend3;
            }
            set
            {
                GetPropertyByName("Blend3").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the blend4.
        /// </summary>
        /// <value>The blend4.</value>
        public Color Blend4
        {
            get
            {
                return colUserControl.Blend4;
            }
            set
            {
                GetPropertyByName("Blend4").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the blend5.
        /// </summary>
        /// <value>The blend5.</value>
        public Color Blend5
        {
            get
            {
                return colUserControl.Blend5;
            }
            set
            {
                GetPropertyByName("Blend5").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the blend6.
        /// </summary>
        /// <value>The blend6.</value>
        public Color Blend6
        {
            get
            {
                return colUserControl.Blend6;
            }
            set
            {
                GetPropertyByName("Blend6").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the blend7.
        /// </summary>
        /// <value>The blend7.</value>
        public Color Blend7
        {
            get
            {
                return colUserControl.Blend7;
            }
            set
            {
                GetPropertyByName("Blend7").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the blend8.
        /// </summary>
        /// <value>The blend8.</value>
        public Color Blend8
        {
            get
            {
                return colUserControl.Blend8;
            }
            set
            {
                GetPropertyByName("Blend8").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the blend9.
        /// </summary>
        /// <value>The blend9.</value>
        public Color Blend9
        {
            get
            {
                return colUserControl.Blend9;
            }
            set
            {
                GetPropertyByName("Blend9").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("Orientation",
                                 "Orientation", "Appearance",
                                 "Sets the orientation of the line."));

            items.Add(new DesignerActionPropertyItem("Blend1",
                                 "Blend1", "Appearance",
                                 "Sets the blend."));

            items.Add(new DesignerActionPropertyItem("Blend2",
                                 "Blend2", "Appearance",
                                 "Sets the blend."));

            items.Add(new DesignerActionPropertyItem("Blend3",
                                 "Blend3", "Appearance",
                                 "Sets the blend."));

            items.Add(new DesignerActionPropertyItem("Blend4",
                                 "Blend4", "Appearance",
                                 "Sets the blend."));

            items.Add(new DesignerActionPropertyItem("Blend5",
                                 "Blend5", "Appearance",
                                 "Sets the blend."));

            items.Add(new DesignerActionPropertyItem("Blend6",
                                 "Blend6", "Appearance",
                                 "Sets the blend."));

            items.Add(new DesignerActionPropertyItem("Blend7",
                                 "Blend7", "Appearance",
                                 "Sets the blend."));

            items.Add(new DesignerActionPropertyItem("Blend8",
                                 "Blend8", "Appearance",
                                 "Sets the blend."));

            items.Add(new DesignerActionPropertyItem("Blend9",
                                 "Blend9", "Appearance",
                                 "Sets the blend."));


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
