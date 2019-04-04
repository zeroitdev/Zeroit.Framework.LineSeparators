// ***********************************************************************
// Assembly         : Zeroit.Framework.LineSeparators
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="PrimitiveLine.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace Zeroit.Framework.LineSeparators
{
    #region ZeroitPrimitiveLine

    #region Graphic Extension

    #region Rectangle Edge Filter Enum

    /// <summary>
    /// Rectangle Edge Filter
    /// </summary>
    internal enum RectangleEdgeFilter
    {
        /// <summary>
        /// No Edge
        /// </summary>
        None = 0,
        /// <summary>
        /// Top Left
        /// </summary>
        TopLeft = 1,
        /// <summary>
        /// Top Right
        /// </summary>
        TopRight = 2,
        /// <summary>
        /// Bottom Left
        /// </summary>
        BottomLeft = 4,
        /// <summary>
        /// Bottom Right
        /// </summary>
        BottomRight = 8,
        /// <summary>
        /// All Edges
        /// </summary>
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    #endregion

    #region Font Metrics

    /// <summary>
    /// Class FontMetrics.
    /// </summary>
    public abstract class FontMetrics
    {
        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        public virtual int Height { get { return 0; } }
        /// <summary>
        /// Gets the ascent.
        /// </summary>
        /// <value>The ascent.</value>
        public virtual int Ascent { get { return 0; } }
        /// <summary>
        /// Gets the descent.
        /// </summary>
        /// <value>The descent.</value>
        public virtual int Descent { get { return 0; } }
        /// <summary>
        /// Gets the internal leading.
        /// </summary>
        /// <value>The internal leading.</value>
        public virtual int InternalLeading { get { return 0; } }
        /// <summary>
        /// Gets the external leading.
        /// </summary>
        /// <value>The external leading.</value>
        public virtual int ExternalLeading { get { return 0; } }
        /// <summary>
        /// Gets the average width of the character.
        /// </summary>
        /// <value>The average width of the character.</value>
        public virtual int AverageCharacterWidth { get { return 0; } }
        /// <summary>
        /// Gets the maximum width of the character.
        /// </summary>
        /// <value>The maximum width of the character.</value>
        public virtual int MaximumCharacterWidth { get { return 0; } }
        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public virtual int Weight { get { return 0; } }
        /// <summary>
        /// Gets the overhang.
        /// </summary>
        /// <value>The overhang.</value>
        public virtual int Overhang { get { return 0; } }
        /// <summary>
        /// Gets the digitized aspect x.
        /// </summary>
        /// <value>The digitized aspect x.</value>
        public virtual int DigitizedAspectX { get { return 0; } }
        /// <summary>
        /// Gets the digitized aspect y.
        /// </summary>
        /// <value>The digitized aspect y.</value>
        public virtual int DigitizedAspectY { get { return 0; } }
    }

    #endregion

    /// <summary>
    /// Class ZeroitLineGraphicsExtensions.
    /// </summary>
    internal static class ZeroitLineGraphicsExtensions
    {
        /// <summary>
        /// Creates a graphics path that represents a rounded rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="rectangle">Rectangle encapsulating the result</param>
        /// <param name="radius">Radius of the corners</param>
        /// <param name="filter">Edges to round</param>
        /// <returns>Graphics Path</returns>
        public static GraphicsPath GenerateRoundedRectangle(
        this Graphics graphics,
        RectangleF rectangle,
        float radius,
        RectangleEdgeFilter filter)
        {
            float diameter;
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0.0F || filter == RectangleEdgeFilter.None)
            {
                path.AddRectangle(rectangle);
                path.CloseFigure();
                return path;
            }
            else
            {
                if (radius >= (Math.Min(rectangle.Width, rectangle.Height)) / 2.0)
                    return graphics.GenerateCapsule(rectangle);
                diameter = radius * 2.0F;
                SizeF sizeF = new SizeF(diameter, diameter);
                RectangleF arc = new RectangleF(rectangle.Location, sizeF);
                if ((RectangleEdgeFilter.TopLeft & filter) == RectangleEdgeFilter.TopLeft)
                    path.AddArc(arc, 180, 90);
                else
                {
                    path.AddLine(arc.X, arc.Y + arc.Height, arc.X, arc.Y);
                    path.AddLine(arc.X, arc.Y, arc.X + arc.Width, arc.Y);
                }
                arc.X = rectangle.Right - diameter;
                if ((RectangleEdgeFilter.TopRight & filter) == RectangleEdgeFilter.TopRight)
                    path.AddArc(arc, 270, 90);
                else
                {
                    path.AddLine(arc.X, arc.Y, arc.X + arc.Width, arc.Y);
                    path.AddLine(arc.X + arc.Width, arc.Y + arc.Height, arc.X + arc.Width, arc.Y);
                }
                arc.Y = rectangle.Bottom - diameter;
                if ((RectangleEdgeFilter.BottomRight & filter) == RectangleEdgeFilter.BottomRight)
                    path.AddArc(arc, 0, 90);
                else
                {
                    path.AddLine(arc.X + arc.Width, arc.Y, arc.X + arc.Width, arc.Y + arc.Height);
                    path.AddLine(arc.X, arc.Y + arc.Height, arc.X + arc.Width, arc.Y + arc.Height);
                }
                arc.X = rectangle.Left;
                if ((RectangleEdgeFilter.BottomLeft & filter) == RectangleEdgeFilter.BottomLeft)
                    path.AddArc(arc, 90, 90);
                else
                {
                    path.AddLine(arc.X + arc.Width, arc.Y + arc.Height, arc.X, arc.Y + arc.Height);
                    path.AddLine(arc.X, arc.Y + arc.Height, arc.X, arc.Y);
                }
                path.CloseFigure();
            }
            return path;
        }

        /// <summary>
        /// Creates a graphics path that represents a capsule
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="rectangle">Rectangle encapsulating the result</param>
        /// <returns>Graphics Path</returns>
        public static GraphicsPath GenerateCapsule(
                this Graphics graphics,
                RectangleF rectangle)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new GraphicsPath();
            try
            {
                if (rectangle.Width > rectangle.Height)
                {
                    diameter = rectangle.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(rectangle.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = rectangle.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (rectangle.Width < rectangle.Height)
                {
                    diameter = rectangle.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(rectangle.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = rectangle.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else path.AddEllipse(rectangle);
            }
            catch { path.AddEllipse(rectangle); }
            finally { path.CloseFigure(); }
            return path;
        }

        /// <summary>
        /// Draws a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="pen">Edge Pen</param>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="radius">Corner Radius</param>
        /// <param name="filter">Corners to round</param>
        public static void DrawRoundedRectangle(
                this Graphics graphics,
                Pen pen,
                float x,
                float y,
                float width,
                float height,
                float radius,
                RectangleEdgeFilter filter)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius, filter);
            SmoothingMode old = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawPath(pen, path);
            graphics.SmoothingMode = old;
        }

        /// <summary>
        /// Draws a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="pen">Edge Pen</param>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="radius">Corner Radius</param>
        public static void DrawRoundedRectangle(
                this Graphics graphics,
                Pen pen,
                float x,
                float y,
                float width,
                float height,
                float radius)
        {
            graphics.DrawRoundedRectangle(
                    pen,
                    x,
                    y,
                    width,
                    height,
                    radius,
                    RectangleEdgeFilter.All);
        }

        /// <summary>
        /// Draws a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="pen">Edge Pen</param>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="radius">Corner Radius</param>
        public static void DrawRoundedRectangle(
                this Graphics graphics,
                Pen pen,
                int x,
                int y,
                int width,
                int height,
                int radius)
        {
            graphics.DrawRoundedRectangle(
                    pen,
                    Convert.ToSingle(x),
                    Convert.ToSingle(y),
                    Convert.ToSingle(width),
                    Convert.ToSingle(height),
                    Convert.ToSingle(radius));
        }

        /// <summary>
        /// Draws a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="pen">Edge Pen</param>
        /// <param name="rectangle">Rectangle</param>
        /// <param name="radius">Corner Radius</param>
        /// <param name="filter">Corners to round</param>
        public static void DrawRoundedRectangle(
            this Graphics graphics,
            Pen pen,
            System.Drawing.Rectangle rectangle,
            int radius,
            RectangleEdgeFilter filter)
        {
            graphics.DrawRoundedRectangle(
                pen,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                radius,
                filter);
        }

        /// <summary>
        /// Draws a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="pen">Edge Pen</param>
        /// <param name="rectangle">Rectangle</param>
        /// <param name="radius">Corner Radius</param>
        public static void DrawRoundedRectangle(
            this Graphics graphics,
            Pen pen,
            System.Drawing.Rectangle rectangle,
            int radius)
        {
            graphics.DrawRoundedRectangle(
                pen,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                radius,
                RectangleEdgeFilter.All);
        }

        /// <summary>
        /// Draws a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="pen">Edge Pen</param>
        /// <param name="rectangle">Rectangle</param>
        /// <param name="radius">Corner Radius</param>
        /// <param name="filter">Corners to Round</param>
        public static void DrawRoundedRectangle(
            this Graphics graphics,
            Pen pen,
            RectangleF rectangle,
            int radius,
            RectangleEdgeFilter filter)
        {
            graphics.DrawRoundedRectangle(
                pen,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                radius,
                filter);
        }

        /// <summary>
        /// Draws a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="pen">Edge Pen</param>
        /// <param name="rectangle">Rectangle</param>
        /// <param name="radius">Corner Radius</param>
        public static void DrawRoundedRectangle(
            this Graphics graphics,
            Pen pen,
            RectangleF rectangle,
            int radius)
        {
            graphics.DrawRoundedRectangle(
                pen,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                radius,
                RectangleEdgeFilter.All);
        }

        /// <summary>
        /// Fills a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="brush">Fill Brush</param>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="radius">Radius</param>
        /// <param name="filter">Corners to Round</param>
        public static void FillRoundedRectangle(
                this Graphics graphics,
                Brush brush,
                float x,
                float y,
                float width,
                float height,
                float radius,
                RectangleEdgeFilter filter)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius, filter);
            SmoothingMode old = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillPath(brush, path);
            graphics.SmoothingMode = old;
        }

        /// <summary>
        /// Fills a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="brush">Fill Brush</param>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="radius">Radius</param>
        public static void FillRoundedRectangle(
                this Graphics graphics,
                Brush brush,
                float x,
                float y,
                float width,
                float height,
                float radius)
        {
            graphics.FillRoundedRectangle(
                    brush,
                    x,
                    y,
                    width,
                    height,
                    radius,
                    RectangleEdgeFilter.All);
        }

        /// <summary>
        /// Fills a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="brush">Fill Brush</param>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="radius">Radius</param>
        public static void FillRoundedRectangle(
                this Graphics graphics,
                Brush brush,
                int x,
                int y,
                int width,
                int height,
                int radius)
        {
            graphics.FillRoundedRectangle(
                    brush,
                    Convert.ToSingle(x),
                    Convert.ToSingle(y),
                    Convert.ToSingle(width),
                    Convert.ToSingle(height),
                    Convert.ToSingle(radius));
        }

        /// <summary>
        /// Fills a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="brush">Fill Brush</param>
        /// <param name="rectangle">Rectangle</param>
        /// <param name="radius">Corner Radius</param>
        /// <param name="filter">Corners to Round</param>
        public static void FillRoundedRectangle(
            this Graphics graphics,
            Brush brush,
            System.Drawing.Rectangle rectangle,
            int radius,
            RectangleEdgeFilter filter)
        {
            graphics.FillRoundedRectangle(
                brush,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                radius,
                filter);
        }

        /// <summary>
        /// Fills a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="brush">Fill Brush</param>
        /// <param name="rectangle">Rectangle</param>
        /// <param name="radius">Corner Radius</param>
        public static void FillRoundedRectangle(
            this Graphics graphics,
            Brush brush,
            System.Drawing.Rectangle rectangle,
            int radius)
        {
            graphics.FillRoundedRectangle(
                brush,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                radius,
                RectangleEdgeFilter.All);
        }

        /// <summary>
        /// Fills a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="brush">Fill Brush</param>
        /// <param name="rectangle">Rectangle</param>
        /// <param name="radius">Corner Radius</param>
        /// <param name="filter">Corners to Round</param>
        public static void FillRoundedRectangle(
            this Graphics graphics,
            Brush brush,
            RectangleF rectangle,
            int radius,
            RectangleEdgeFilter filter)
        {
            graphics.FillRoundedRectangle(
                brush,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                radius,
                filter);
        }

        /// <summary>
        /// Fills a Rounded Rectangle
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="brush">Fill Brush</param>
        /// <param name="rectangle">Rectangle</param>
        /// <param name="radius">Corner Radius</param>
        public static void FillRoundedRectangle(
            this Graphics graphics,
            Brush brush,
            RectangleF rectangle,
            int radius)
        {
            graphics.FillRoundedRectangle(
                brush,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                radius,
                RectangleEdgeFilter.All);
        }

        /// <summary>
        /// Calculates Font Metrics
        /// </summary>
        /// <param name="graphics">Extension Reference</param>
        /// <param name="font">Font</param>
        /// <returns>Font Metrics</returns>
        public static FontMetrics GetFontMetrics(
            this Graphics graphics,
            Font font)
        {
            return FontMetricsImpl.GetFontMetrics(graphics, font);
        }

        /// <summary>
        /// Class FontMetricsImpl.
        /// </summary>
        /// <seealso cref="Zeroit.Framework.LineSeparators.FontMetrics" />
        private class FontMetricsImpl : FontMetrics
        {
            /// <summary>
            /// Struct TEXTMETRIC
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct TEXTMETRIC
            {
                /// <summary>
                /// The tm height
                /// </summary>
                public int tmHeight;
                /// <summary>
                /// The tm ascent
                /// </summary>
                public int tmAscent;
                /// <summary>
                /// The tm descent
                /// </summary>
                public int tmDescent;
                /// <summary>
                /// The tm internal leading
                /// </summary>
                public int tmInternalLeading;
                /// <summary>
                /// The tm external leading
                /// </summary>
                public int tmExternalLeading;
                /// <summary>
                /// The tm ave character width
                /// </summary>
                public int tmAveCharWidth;
                /// <summary>
                /// The tm maximum character width
                /// </summary>
                public int tmMaxCharWidth;
                /// <summary>
                /// The tm weight
                /// </summary>
                public int tmWeight;
                /// <summary>
                /// The tm overhang
                /// </summary>
                public int tmOverhang;
                /// <summary>
                /// The tm digitized aspect x
                /// </summary>
                public int tmDigitizedAspectX;
                /// <summary>
                /// The tm digitized aspect y
                /// </summary>
                public int tmDigitizedAspectY;
                /// <summary>
                /// The tm first character
                /// </summary>
                public char tmFirstChar;
                /// <summary>
                /// The tm last character
                /// </summary>
                public char tmLastChar;
                /// <summary>
                /// The tm default character
                /// </summary>
                public char tmDefaultChar;
                /// <summary>
                /// The tm break character
                /// </summary>
                public char tmBreakChar;
                /// <summary>
                /// The tm italic
                /// </summary>
                public byte tmItalic;
                /// <summary>
                /// The tm underlined
                /// </summary>
                public byte tmUnderlined;
                /// <summary>
                /// The tm struck out
                /// </summary>
                public byte tmStruckOut;
                /// <summary>
                /// The tm pitch and family
                /// </summary>
                public byte tmPitchAndFamily;
                /// <summary>
                /// The tm character set
                /// </summary>
                public byte tmCharSet;
            }
            /// <summary>
            /// Selects the object.
            /// </summary>
            /// <param name="hdc">The HDC.</param>
            /// <param name="hgdiobj">The hgdiobj.</param>
            /// <returns>IntPtr.</returns>
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
            /// <summary>
            /// Gets the text metrics.
            /// </summary>
            /// <param name="hdc">The HDC.</param>
            /// <param name="lptm">The LPTM.</param>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
            public static extern bool GetTextMetrics(IntPtr hdc, out TEXTMETRIC lptm);
            /// <summary>
            /// Deletes the object.
            /// </summary>
            /// <param name="hdc">The HDC.</param>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
            public static extern bool DeleteObject(IntPtr hdc);
            /// <summary>
            /// Generates the text metrics.
            /// </summary>
            /// <param name="graphics">The graphics.</param>
            /// <param name="font">The font.</param>
            /// <returns>TEXTMETRIC.</returns>
            private TEXTMETRIC GenerateTextMetrics(
                Graphics graphics,
                Font font)
            {
                IntPtr hDC = IntPtr.Zero;
                TEXTMETRIC textMetric;
                IntPtr hFont = IntPtr.Zero;
                try
                {
                    hDC = graphics.GetHdc();
                    hFont = font.ToHfont();
                    IntPtr hFontDefault = SelectObject(hDC, hFont);
                    bool result = GetTextMetrics(hDC, out textMetric);
                    SelectObject(hDC, hFontDefault);
                }
                finally
                {
                    if (hFont != IntPtr.Zero) DeleteObject(hFont);
                    if (hDC != IntPtr.Zero) graphics.ReleaseHdc(hDC);
                }
                return textMetric;
            }
            /// <summary>
            /// The metrics
            /// </summary>
            private TEXTMETRIC metrics;
            /// <summary>
            /// Gets the height.
            /// </summary>
            /// <value>The height.</value>
            public override int Height { get { return this.metrics.tmHeight; } }
            /// <summary>
            /// Gets the ascent.
            /// </summary>
            /// <value>The ascent.</value>
            public override int Ascent { get { return this.metrics.tmAscent; } }
            /// <summary>
            /// Gets the descent.
            /// </summary>
            /// <value>The descent.</value>
            public override int Descent { get { return this.metrics.tmDescent; } }
            /// <summary>
            /// Gets the internal leading.
            /// </summary>
            /// <value>The internal leading.</value>
            public override int InternalLeading { get { return this.metrics.tmInternalLeading; } }
            /// <summary>
            /// Gets the external leading.
            /// </summary>
            /// <value>The external leading.</value>
            public override int ExternalLeading { get { return this.metrics.tmExternalLeading; } }
            /// <summary>
            /// Gets the average width of the character.
            /// </summary>
            /// <value>The average width of the character.</value>
            public override int AverageCharacterWidth { get { return this.metrics.tmAveCharWidth; } }
            /// <summary>
            /// Gets the maximum width of the character.
            /// </summary>
            /// <value>The maximum width of the character.</value>
            public override int MaximumCharacterWidth { get { return this.metrics.tmMaxCharWidth; } }
            /// <summary>
            /// Gets the weight.
            /// </summary>
            /// <value>The weight.</value>
            public override int Weight { get { return this.metrics.tmWeight; } }
            /// <summary>
            /// Gets the overhang.
            /// </summary>
            /// <value>The overhang.</value>
            public override int Overhang { get { return this.metrics.tmOverhang; } }
            /// <summary>
            /// Gets the digitized aspect x.
            /// </summary>
            /// <value>The digitized aspect x.</value>
            public override int DigitizedAspectX { get { return this.metrics.tmDigitizedAspectX; } }
            /// <summary>
            /// Gets the digitized aspect y.
            /// </summary>
            /// <value>The digitized aspect y.</value>
            public override int DigitizedAspectY { get { return this.metrics.tmDigitizedAspectY; } }
            /// <summary>
            /// Initializes a new instance of the <see cref="FontMetricsImpl"/> class.
            /// </summary>
            /// <param name="graphics">The graphics.</param>
            /// <param name="font">The font.</param>
            private FontMetricsImpl(Graphics graphics, Font font)
            {
                this.metrics = this.GenerateTextMetrics(graphics, font);
            }
            /// <summary>
            /// Gets the font metrics.
            /// </summary>
            /// <param name="graphics">The graphics.</param>
            /// <param name="font">The font.</param>
            /// <returns>FontMetrics.</returns>
            public static FontMetrics GetFontMetrics(
                Graphics graphics,
                Font font)
            {
                return new FontMetricsImpl(graphics, font);
            }
        }
    }

    #endregion

    #region IShape
    /// <summary>
    /// Interface IShape
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Gets the number of points in the shape.
        /// </summary>
        /// <value>The point count.</value>
        int PointCount { get; }
        /// <summary>
        /// Gets the point at the index <paramref name="i" />.
        /// </summary>
        /// <param name="i">Index of the point to return.</param>
        /// <returns>Point at the index or Point.Empty if it doesn't exist</returns>
        Point GetPoint(int i);
        /// <summary>
        /// Sets the point at the specified index <paramref name="i" />.
        /// </summary>
        /// <param name="i">Index of the point to set.</param>
        /// <param name="p">Point value.</param>
        void SetPoint(int i, Point p);

        /// <summary>
        /// Raised when the number of points changes in the shape.
        /// </summary>
        event EventHandler PointCountChanged;
    }
    #endregion

    #region Line
    /// <summary>
    /// A class collection for rendering a line control.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="Zeroit.Framework.LineSeparators.IShape" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    //[Designer(typeof(ShapeControlDesigner))]
    [Designer(typeof(ZeroitPrimitiveLineDesigner))]
    public class ZeroitPrimitiveLine : Control, IShape, INotifyPropertyChanged
    {

        #region Fields

        /// <summary>
        /// The p1
        /// </summary>
        private Point _p1;
        /// <summary>
        /// The p2
        /// </summary>
        private Point _p2;
        /// <summary>
        /// The pen width
        /// </summary>
        private float _penWidth = 1.0f;
        /// <summary>
        /// The line color
        /// </summary>
        private Color _lineColor = Color.Black;
        /// <summary>
        /// The edge offset
        /// </summary>
        private int _edgeOffset = 5;
        /// <summary>
        /// The opacity
        /// </summary>
        private float _opacity = 1.0f;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the start point of the line in control coordinates.
        /// </summary>
        /// <value>The start point.</value>
        [Browsable(false), Category("Layout"), Description("Start point of the line in control coordinates.")]
        public Point StartPoint
        {
            get { return _p1; }
            set
            {
                if (value != _p1)
                {
                    _p1 = value;
                    OnPropertyChanged("StartPoint");
                    RecalcSize();
                }
            }
        }

        /// <summary>
        /// Gets or sets the end point of the line in control coordinates.
        /// </summary>
        /// <value>The end point.</value>
        [Browsable(false), Category("Layout"), Description("End point of the line in control coordinates.")]
        public Point EndPoint
        {
            get { return _p2; }
            set
            {
                if (value != _p2)
                {
                    _p2 = value;
                    OnPropertyChanged("EndPoint");
                    RecalcSize();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        [Category("Appearance"), Description("Color of the line drawn."), DefaultValue(typeof(Color), "Black")]
        public Color LineColor
        {
            get { return _lineColor; }
            set
            {
                if (value != _lineColor)
                {
                    _lineColor = value;
                    OnPropertyChanged("LineColor");
                    InvokeInvalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        [Category("Appearance"), Description("Width of the line drawn."), DefaultValue(1.0f)]
        public float LineWidth
        {
            get { return _penWidth; }
            set
            {
                if (value != _penWidth)
                {
                    _penWidth = value;
                    OnPropertyChanged("LineWidth");
                    InvokeInvalidate();
                }
            }
        }

        /// <summary>
        /// Gets the number of points in the shape.
        /// </summary>
        /// <value>The point count.</value>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int PointCount
        {
            get { return 2; }
        }

        /// <summary>
        /// Gets or sets the opacity of the line.
        /// </summary>
        /// <value>The opacity.</value>
        [Category("Appearance"), Description("Sets the opacity of the line color."), DefaultValue(1.0f)]
        public float Opacity
        {
            get { return _opacity; }
            set
            {
                if (value != _opacity)
                {
                    if (value > 1.0f)
                        value = 1.0f;
                    if (value < 0.0f)
                        value = 0.0f;
                    _opacity = value;
                    OnPropertyChanged("Opacity");
                    InvokeInvalidate();
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the number of points changes in the shape.
        /// </summary>
        public event EventHandler PointCountChanged;
        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitPrimitiveLine"/> class.
        /// </summary>
        public ZeroitPrimitiveLine()
        {
            _p1 = new Point(5, 5);
            _p2 = new Point(45, 45);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the point at the index <paramref name="i" />.
        /// </summary>
        /// <param name="i">Index of the point to return.</param>
        /// <returns>Point at the index or Point.Empty if it doesn't exist</returns>
        public Point GetPoint(int i)
        {
            if (i == 0)
                return _p1;
            else if (i == 1)
                return _p2;
            else
                return Point.Empty;
        }

        /// <summary>
        /// Sets the point at the specified index <paramref name="i" />.
        /// </summary>
        /// <param name="i">Index of the point to set.</param>
        /// <param name="p">Point value.</param>
        public void SetPoint(int i, Point p)
        {
            if (i == 0)
                StartPoint = p;
            else if (i == 1)
                EndPoint = p;
        }

        #endregion

        #region Protected Methods


        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        /// <value>The default size.</value>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(50, 50);
            }
        }

        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        /// <value>The create parameters.</value>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x20;

                return cp;
            }
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            InvokeInvalidate();
            base.OnResize(e);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color opacityColor = Color.FromArgb((int)(255 * _opacity), _lineColor);

            using (Pen p = new Pen(opacityColor, _penWidth))
            {
                e.Graphics.DrawLine(p, _p1, _p2);
            }


        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Recalcs the size.
        /// </summary>
        private void RecalcSize()
        {
            AdjustTopEdge();
            AdjustBottomEdge();
            AdjustLeftEdge();
            AdjustRightEdge();

            InvokeInvalidate();

            base.OnResize(EventArgs.Empty);
        }

        /// <summary>
        /// Invokes the invalidate.
        /// </summary>
        private void InvokeInvalidate()
        {
            if (!IsHandleCreated)
                return;

            Rectangle rect = new Rectangle(Location, Size);

            try
            {
                //RecreateHandle(); Invalidate();
                this.Invoke((MethodInvoker)delegate { Parent.Invalidate(rect, true); });
            }
            catch { }
        }

        /// <summary>
        /// Adjusts the top edge.
        /// </summary>
        private void AdjustTopEdge()
        {
            //Find the top most point
            int minY = Math.Min(_p1.Y, _p2.Y);
            bool useP1 = false;

            if (_p1.Y < _p2.Y)
                useP1 = true;

            int adjust = minY - _edgeOffset;

            Top += adjust;

            if (useP1)
            {
                _p1.Y = _edgeOffset;
                _p2.Y -= adjust;
                Height -= adjust;
            }
            else
            {
                _p2.Y = _edgeOffset;
                _p1.Y -= adjust;
                Height -= adjust;
            }
        }

        /// <summary>
        /// Adjusts the bottom edge.
        /// </summary>
        private void AdjustBottomEdge()
        {
            int maxY = Math.Max(_p1.Y, _p2.Y);

            int height = maxY + _edgeOffset;

            Height = height;
        }

        /// <summary>
        /// Adjusts the left edge.
        /// </summary>
        private void AdjustLeftEdge()
        {
            int minX = Math.Min(_p1.X, _p2.X);
            bool useP1 = false;

            if (_p1.X < _p2.X)
                useP1 = true;

            int adjust = minX - _edgeOffset;

            Left += adjust;

            if (useP1)
            {
                _p1.X = _edgeOffset;
                _p2.X -= adjust;
                Width -= adjust;
            }
            else
            {
                _p2.X = _edgeOffset;
                _p1.X -= adjust;
                Width -= adjust;
            }
        }

        /// <summary>
        /// Adjusts the right edge.
        /// </summary>
        private void AdjustRightEdge()
        {
            int maxX = Math.Max(_p1.X, _p2.X);

            int width = maxX + _edgeOffset;

            Width = width;
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
    #endregion

    #region PointGlyph
    /// <summary>
    /// Class PointGlyph.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.Behavior.Glyph" />
    class PointGlyph : Glyph
    {

        #region Fields

        /// <summary>
        /// The shape
        /// </summary>
        private IShape _shape;
        /// <summary>
        /// The point index
        /// </summary>
        private int _pointIdx;
        /// <summary>
        /// The behavior SVC
        /// </summary>
        private BehaviorService _behaviorSvc;
        /// <summary>
        /// The base control
        /// </summary>
        private Control _baseControl;
        /// <summary>
        /// The glyph size
        /// </summary>
        private int _glyphSize = 10;
        /// <summary>
        /// The glyph fill color
        /// </summary>
        private Color _glyphFillColor = Color.White;
        /// <summary>
        /// The glyph outline color
        /// </summary>
        private Color _glyphOutlineColor = Color.Black;
        /// <summary>
        /// The glyph corner radius
        /// </summary>
        private int _glyphCornerRadius = 4;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.
        /// </summary>
        /// <value>The bounds.</value>
        public override Rectangle Bounds
        {
            get
            {
                Point p = _shape.GetPoint(_pointIdx);

                p = _behaviorSvc.MapAdornerWindowPoint(_baseControl.Handle, p);

                int x = p.X - (_glyphSize / 2);
                int y = p.Y - (_glyphSize / 2);

                return new Rectangle(x, y, _glyphSize, _glyphSize);
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Initializes a new instance of the <see cref="PointGlyph"/> class.
        /// </summary>
        /// <param name="behaviorSvc">The behavior SVC.</param>
        /// <param name="shape">The shape.</param>
        /// <param name="pointIdx">Index of the point.</param>
        /// <param name="baseControl">The base control.</param>
        public PointGlyph(BehaviorService behaviorSvc, IShape shape, int pointIdx, Control baseControl)
            : base(new ShapeGlyphBehavior(shape, pointIdx))
        {
            _shape = shape;
            _pointIdx = pointIdx;
            _behaviorSvc = behaviorSvc;
            _baseControl = baseControl;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Provides hit test logic.
        /// </summary>
        /// <param name="p">A point to hit-test.</param>
        /// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated with <paramref name="p" />; otherwise, null.</returns>
        public override Cursor GetHitTest(Point p)
        {
            Rectangle hitBounds = Bounds;
            hitBounds.Inflate(4, 4);

            if (hitBounds.Contains(p))
                return Cursors.Hand;

            return null;
        }

        /// <summary>
        /// Provides paint logic.
        /// </summary>
        /// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        public override void Paint(PaintEventArgs pe)
        {
            Rectangle glyphRect = Bounds;

            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //First draw the fill...
            using (SolidBrush sb = new SolidBrush(_glyphFillColor))
            {
                pe.Graphics.FillRoundedRectangle(sb, glyphRect, _glyphCornerRadius);
            }

            //And then  the outline
            using (Pen p = new Pen(_glyphOutlineColor))
            {
                pe.Graphics.DrawRoundedRectangle(p, glyphRect, _glyphCornerRadius);
            }
        }

        #endregion

        #region Private Methods



        #endregion

    }
    #endregion

    #region ShapeControlDesigner
    /// <summary>
    /// Class ShapeControlDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    class ShapeControlDesigner : ControlDesigner
    {

        #region Fields

        /// <summary>
        /// The shape adorner
        /// </summary>
        private Adorner _shapeAdorner;

        /// <summary>
        /// The rem properties
        /// </summary>
        private string[] _remProperties = new string[] { "AccessibleDescription", "AccessibleRole",
            "AccessibleName", "BackColor", "BackgroundImage", "BackgroundImageLayout", "Cursor",
            "Font", "ForeColor", "RightToLeft", "Text", "UseWaitCursor", "AllowDrop", "Enabled",
            "ImeMode", "Anchor", "Dock", "Margin", "Padding", "MaximumSize", "MinimumSize",
            "CausesValidation" };

        /// <summary>
        /// The selection SVC
        /// </summary>
        private ISelectionService _selectionSvc;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> will allow snapline alignment during a drag operation.
        /// </summary>
        /// <value><c>true</c> if [participates with snap lines]; otherwise, <c>false</c>.</value>
        public override bool ParticipatesWithSnapLines
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        /// <value>The selection rules.</value>
        public override SelectionRules SelectionRules
        {
            get
            {
                return System.Windows.Forms.Design.SelectionRules.Moveable |
                    System.Windows.Forms.Design.SelectionRules.Visible;
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            BehaviorService b = BehaviorService;

            if (b != null && b.Adorners.Contains(_shapeAdorner))
                b.Adorners.Remove(_shapeAdorner);

            base.Dispose(disposing);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate the designer with. This component must always be an instance of, or derive from, <see cref="T:System.Windows.Forms.Control" />.</param>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            _selectionSvc = GetService(typeof(ISelectionService)) as ISelectionService;

            _selectionSvc.SelectionChanged += new EventHandler(SelectionSvc_SelectionChanged);

            Control.Resize += new EventHandler(Control_Resize);

            IShape shape = Control as IShape;

            if (shape != null)
                shape.PointCountChanged += new EventHandler(Shape_PointCountChanged);

            RecreateAdorner();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Adjusts the set of properties the component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.
        /// </summary>
        /// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> containing the properties for the class of the component.</param>
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            foreach (string propName in _remProperties)
                properties.Remove(propName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the PointCountChanged event of the Shape control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Shape_PointCountChanged(object sender, EventArgs e)
        {
            RecreateAdorner();
        }

        /// <summary>
        /// Handles the Resize event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Control_Resize(object sender, EventArgs e)
        {
            BehaviorService.SyncSelection();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the SelectionSvc control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelectionSvc_SelectionChanged(object sender, EventArgs e)
        {
            if (_selectionSvc.PrimarySelection == Control && _selectionSvc.SelectionCount == 1)
                _shapeAdorner.Enabled = true;
            else
                _shapeAdorner.Enabled = false;
        }

        /// <summary>
        /// Recreates the adorner.
        /// </summary>
        private void RecreateAdorner()
        {
            if (_shapeAdorner != null)
            {
                _shapeAdorner.Glyphs.Clear();
            }
            else
            {
                _shapeAdorner = new Adorner();
                BehaviorService.Adorners.Add(_shapeAdorner);
            }

            IShape shape = Control as IShape;

            if (shape == null)
                return;

            for (int i = 0; i < shape.PointCount; i++)
            {
                _shapeAdorner.Glyphs.Add(new PointGlyph(BehaviorService, shape, i, Control));
            }

        }

        #endregion

    }
    #endregion

    #region ShapeGlyphBehavior
    /// <summary>
    /// Class ShapeGlyphBehavior.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.Behavior.Behavior" />
    class ShapeGlyphBehavior : Behavior
    {

        #region Fields

        /// <summary>
        /// The drag start
        /// </summary>
        private Point _dragStart = Point.Empty;
        /// <summary>
        /// The shape
        /// </summary>
        private IShape _shape;
        /// <summary>
        /// The point index
        /// </summary>
        private int _pointIdx;
        /// <summary>
        /// The dragging
        /// </summary>
        private bool _dragging = false;


        #endregion

        #region Properties



        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeGlyphBehavior"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="pointIdx">Index of the point.</param>
        public ShapeGlyphBehavior(IShape shape, int pointIdx)
        {
            _shape = shape;
            _pointIdx = pointIdx;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when any mouse-down message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.
        /// </summary>
        /// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
        /// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons" /> value indicating which button was clicked.</param>
        /// <param name="mouseLoc">The location at which the click occurred.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseDown(Glyph g, System.Windows.Forms.MouseButtons button, Point mouseLoc)
        {
            if ((button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                _dragStart = mouseLoc;
                _dragging = true;
            }
            return true;
        }

        /// <summary>
        /// Called when any mouse-up message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.
        /// </summary>
        /// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
        /// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons" /> value indicating which button was clicked.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseUp(Glyph g, System.Windows.Forms.MouseButtons button)
        {
            if ((button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                _dragging = false;
            }
            return true;
        }

        /// <summary>
        /// Called when any mouse-move message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.
        /// </summary>
        /// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
        /// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons" /> value indicating which button was clicked.</param>
        /// <param name="mouseLoc">The location at which the move occurred.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseMove(Glyph g, System.Windows.Forms.MouseButtons button, Point mouseLoc)
        {
            if (_dragging)
            {
                int xDiff = mouseLoc.X - _dragStart.X;
                int yDiff = mouseLoc.Y - _dragStart.Y;

                Point p = _shape.GetPoint(_pointIdx);

                if (xDiff == 0 && yDiff == 0)
                    return true;

                p.X += xDiff;
                p.Y += yDiff;
                _dragStart = mouseLoc;

                _shape.SetPoint(_pointIdx, p);
            }

            return true;
        }

        /// <summary>
        /// Called when any mouse-leave message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.
        /// </summary>
        /// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseLeave(Glyph g)
        {
            _dragging = false;
            return true;
        }

        #endregion

        #region Private Methods



        #endregion

    }
    #endregion


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitPrimitiveLineDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitPrimitiveLineDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitPrimitiveLineDesigner : System.Windows.Forms.Design.ControlDesigner
    {

        #region Fields

        /// <summary>
        /// The shape adorner
        /// </summary>
        private Adorner _shapeAdorner;

        /// <summary>
        /// The rem properties
        /// </summary>
        private string[] _remProperties = new string[] { "AccessibleDescription", "AccessibleRole",
            "AccessibleName", "BackColor", "BackgroundImage", "BackgroundImageLayout", "Cursor",
            "Font", "ForeColor", "RightToLeft", "Text", "UseWaitCursor", "AllowDrop", "Enabled",
            "ImeMode", "Anchor", "Dock", "Margin", "Padding", "MaximumSize", "MinimumSize",
            "CausesValidation" };

        /// <summary>
        /// The selection SVC
        /// </summary>
        private ISelectionService _selectionSvc;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> will allow snapline alignment during a drag operation.
        /// </summary>
        /// <value><c>true</c> if [participates with snap lines]; otherwise, <c>false</c>.</value>
        public override bool ParticipatesWithSnapLines
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        /// <value>The selection rules.</value>
        public override SelectionRules SelectionRules
        {
            get
            {
                return System.Windows.Forms.Design.SelectionRules.Moveable |
                    System.Windows.Forms.Design.SelectionRules.Visible;
            }
        }

        #endregion

        #region Construction / Deconstruction

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            BehaviorService b = BehaviorService;

            if (b != null && b.Adorners.Contains(_shapeAdorner))
                b.Adorners.Remove(_shapeAdorner);

            base.Dispose(disposing);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate the designer with. This component must always be an instance of, or derive from, <see cref="T:System.Windows.Forms.Control" />.</param>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            _selectionSvc = GetService(typeof(ISelectionService)) as ISelectionService;

            _selectionSvc.SelectionChanged += new EventHandler(SelectionSvc_SelectionChanged);

            Control.Resize += new EventHandler(Control_Resize);

            IShape shape = Control as IShape;

            if (shape != null)
                shape.PointCountChanged += new EventHandler(Shape_PointCountChanged);

            RecreateAdorner();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Adjusts the set of properties the component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.
        /// </summary>
        /// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> containing the properties for the class of the component.</param>
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            foreach (string propName in _remProperties)
                properties.Remove(propName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the PointCountChanged event of the Shape control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Shape_PointCountChanged(object sender, EventArgs e)
        {
            RecreateAdorner();
        }

        /// <summary>
        /// Handles the Resize event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Control_Resize(object sender, EventArgs e)
        {
            BehaviorService.SyncSelection();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the SelectionSvc control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelectionSvc_SelectionChanged(object sender, EventArgs e)
        {
            if (_selectionSvc.PrimarySelection == Control && _selectionSvc.SelectionCount == 1)
                _shapeAdorner.Enabled = true;
            else
                _shapeAdorner.Enabled = false;
        }

        /// <summary>
        /// Recreates the adorner.
        /// </summary>
        private void RecreateAdorner()
        {
            if (_shapeAdorner != null)
            {
                _shapeAdorner.Glyphs.Clear();
            }
            else
            {
                _shapeAdorner = new Adorner();
                BehaviorService.Adorners.Add(_shapeAdorner);
            }

            IShape shape = Control as IShape;

            if (shape == null)
                return;

            for (int i = 0; i < shape.PointCount; i++)
            {
                _shapeAdorner.Glyphs.Add(new PointGlyph(BehaviorService, shape, i, Control));
            }

        }

        #endregion


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
                    actionLists.Add(new ZeroitPrimitiveLineSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitPrimitiveLineSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitPrimitiveLineSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitPrimitiveLine colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitPrimitiveLineSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitPrimitiveLineSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitPrimitiveLine;

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
        /// Gets or sets the start point.
        /// </summary>
        /// <value>The start point.</value>
        public Point StartPoint
        {
            get
            {
                return colUserControl.StartPoint;
            }
            set
            {
                GetPropertyByName("StartPoint").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        /// <value>The end point.</value>
        public Point EndPoint
        {
            get
            {
                return colUserControl.EndPoint;
            }
            set
            {
                GetPropertyByName("EndPoint").SetValue(colUserControl, value);
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

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>The opacity.</value>
        public float Opacity
        {
            get
            {
                return colUserControl.Opacity;
            }
            set
            {
                GetPropertyByName("Opacity").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("StartPoint",
                                 "Start Point", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("EndPoint",
                                 "End Point", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("LineColor",
                                 "Line Color", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("LineWidth",
                                 "Line Width", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("Opacity",
                                 "Opacity", "Appearance",
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
