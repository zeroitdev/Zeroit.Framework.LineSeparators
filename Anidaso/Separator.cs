// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="Separator.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zeroit.Framework.LineSeparators.Helper;

namespace Zeroit.Framework.LineSeparators
{
    /// <summary>
    /// A class collection for rendering a line.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [Designer(typeof(ZeroitAnidasoLineDesigner))]
    //[ProvideProperty("Zeroit.Framework.LineSeparators", typeof(Control))]
	public class ZeroitAnidasoLine : UserControl
	{
        /// <summary>
        /// The vertical
        /// </summary>
        private bool vertical;

        /// <summary>
        /// The i container
        /// </summary>
        private IContainer iContainer;

        /// <summary>
        /// The picture box1
        /// </summary>
        private PictureBox pictureBox1;

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor
		{
			get
			{
				return this.pictureBox1.BackColor;
			}
			set
			{
				this.pictureBox1.BackColor = value;
			}
		}

        /// <summary>
        /// Gets or sets the line thickness.
        /// </summary>
        /// <value>The line thickness.</value>
        public int LineThickness
		{
			get
			{
				if (this.Vertical)
				{
					return this.pictureBox1.Width;
				}
				return this.pictureBox1.Height;
			}
			set
			{
				if (this.Vertical)
				{
					this.pictureBox1.Width = value;
					return;
				}
				this.pictureBox1.Height = value;
			}
		}

        /// <summary>
        /// Gets or sets the transparency.
        /// </summary>
        /// <value>The transparency.</value>
        public int Transparency
		{
			get
			{
				return this.pictureBox1.BackColor.A;
			}
			set
			{
				PictureBox pictureBox = this.pictureBox1;
				byte r = this.pictureBox1.BackColor.R;
				byte g = this.pictureBox1.BackColor.G;
				Color backColor = this.pictureBox1.BackColor;
				pictureBox.BackColor = Color.FromArgb(value, (int)r, (int)g, (int)backColor.B);
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitAnidasoLine" /> is vertical or horizontal.
        /// </summary>
        /// <value><c>true</c> if vertical; otherwise, <c>false</c>.</value>
        public bool Vertical
		{
			get
			{
				return this.vertical;
			}
			set
			{
				int num = 0;
				int num1 = 0;
				int num2;
				if (value == this.vertical)
				{
					do
					{
						if (num != num1)
						{
							break;
						}
						num1 = 1;
						num2 = num;
						num = 1;
					}
					while (1 <= num2);
				}
				else
				{
					this.vertical = value;
					int height = this.pictureBox1.Height;
					int width = this.pictureBox1.Width;
					this.pictureBox1.Height = width;
					this.pictureBox1.Width = height;
					this.OnResize(new EventArgs());
				}
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAnidasoLine" /> class.
        /// </summary>
        public ZeroitAnidasoLine()
		{
			this.InitializeComponent();
			//this.OnResize(new EventArgs());
			AnidasoCustomControl.initializeComponent(this);
		}

        /// <summary>
        /// Handles the BackColorChanged event of the AnidasoSeparator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.Exception">Invalid Value</exception>
        private void AnidasoSeparator_BackColorChanged(object sender, EventArgs e)
		{
			int num = 0;
			int num1 = 0;
			int num2;
			if (this.BackColor != Color.Transparent)
			{
				throw new Exception("Invalid Value");
			}
			do
			{
				if (num != num1)
				{
					break;
				}
				num1 = 1;
				num2 = num;
				num = 1;
			}
			while (1 <= num2);
		}

        /// <summary>
        /// Handles the Load event of the AnidasoSeparator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AnidasoSeparator_Load(object sender, EventArgs e)
		{
			int num = 0;
			int num1 = 0;
			int num2;
			if (!base.DesignMode)
			{
				do
				{
					if (num != num1)
					{
						break;
					}
					num1 = 1;
					num2 = num;
					num = 1;
				}
				while (1 <= num2);
			}
			else
			{
				AnidasoCustomControl.initializeComponent(this);
			}
		}

        /// <summary>
        /// Handles the Resize event of the AnidasoSeparator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AnidasoSeparator_Resize(object sender, EventArgs e)
		{
			if (this.Vertical)
			{
				this.pictureBox1.Top = 0;
				this.pictureBox1.Height = base.Height;
				this.pictureBox1.Left = base.Width / 2 - this.pictureBox1.Width / 2;
				return;
			}
			this.pictureBox1.Left = 0;
			this.pictureBox1.Width = base.Width;
			this.pictureBox1.Top = base.Height / 2 - this.pictureBox1.Height / 2;
		}

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
		{
			if (disposing && this.iContainer != null)
			{
				this.iContainer.Dispose();
			}
			base.Dispose(disposing);
		}

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
		{
			this.pictureBox1 = new PictureBox();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.BackColor = Color.DimGray;
			this.pictureBox1.Location = new Point(0, 15);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(639, 1);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.pictureBox1);
			base.Name = "AnidasoSeparator";
			base.Size = new System.Drawing.Size(639, 35);
			base.Load += new EventHandler(this.AnidasoSeparator_Load);
			base.BackColorChanged += new EventHandler(this.AnidasoSeparator_BackColorChanged);
			base.Resize += new EventHandler(this.AnidasoSeparator_Resize);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitAnidasoLineDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitAnidasoLineDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitAnidasoLineDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitAnidasoLineSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }

        #region Zeroit Filter (Remove Properties)
        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("AllowDrop");
            //Properties.Remove("FlatStyle");
            //Properties.Remove("ForeColor");
            //Properties.Remove("ImageIndex");
            //Properties.Remove("ImageList");
        }
        #endregion

    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitAnidasoLineSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitAnidasoLineSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitAnidasoLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAnidasoLineSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitAnidasoLineSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitAnidasoLine;

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
        /// Gets or sets the line thickness.
        /// </summary>
        /// <value>The line thickness.</value>
        public int LineThickness
        {
            get
            {
                return colUserControl.LineThickness;
            }
            set
            {
                GetPropertyByName("LineThickness").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets the transparency.
        /// </summary>
        /// <value>The transparency.</value>
        public int Transparency
        {
            get
            {
                return colUserControl.Transparency;
            }
            set
            {
                GetPropertyByName("Transparency").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitAnidasoLineSmartTagActionList"/> is vertical.
        /// </summary>
        /// <value><c>true</c> if vertical; otherwise, <c>false</c>.</value>
        public bool Vertical
        {
            get
            {
                return colUserControl.Vertical;
            }
            set
            {
                GetPropertyByName("Vertical").SetValue(colUserControl, value);
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


            items.Add(new DesignerActionPropertyItem("Vertical",
                "Vertical", "Appearance",
                "Sets the orientation."));

            items.Add(new DesignerActionPropertyItem("LineColor",
                                 "Line Color", "Appearance",
                                 "Sets the line color."));

            items.Add(new DesignerActionPropertyItem("LineThickness",
                                 "Line Thickness", "Appearance",
                                 "Sets the line thickness."));

            items.Add(new DesignerActionPropertyItem("Transparency",
                                 "Transparency", "Appearance",
                                 "Sets the transparency value."));


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

}