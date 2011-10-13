using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Talk2Bits.OutputColorer.Controls
{
    public class ColorPickerCell : DataGridViewTextBoxCell
    {
        public ColorPickerCell() : this(Color.White)
        {
        }

        public ColorPickerCell(Color defaultColor)
        {
            Value = defaultColor;
        }        

        protected override void OnClick(DataGridViewCellEventArgs e)
        {
            var cd = new ColorDialog();
            cd.SolidColorOnly = false;

            if (Value != null)
                cd.Color = (Color)Value;

            cd.ShowDialog();

            Value = cd.Color;
        }

        public override Type ValueType
        {
            get { return typeof(Color); }
        }

        public override object DefaultNewRowValue
        {
            get { return Color.White; }
        }

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates elementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            formattedValue = null;

            base.Paint(
                graphics,
                clipBounds,
                cellBounds,
                rowIndex,
                elementState,
                value,
                formattedValue,
                errorText,
                cellStyle,
                advancedBorderStyle,
                paintParts
                );


            var colorBoxRect = new Rectangle();
            var textBoxRect = new RectangleF();

            GetDisplayLayout(cellBounds, ref colorBoxRect, ref textBoxRect);

            // Draw the cell background, if specified.
            if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
            {
                SolidBrush cellBackground;

                if (value != null && value.GetType() == typeof(Color))
                {
                    cellBackground = new SolidBrush((Color)value);
                }
                else
                {
                    cellBackground = new SolidBrush(cellStyle.BackColor);
                }

                graphics.FillRectangle(cellBackground, colorBoxRect);
                graphics.DrawRectangle(Pens.Black, colorBoxRect);
                var lclcolor = (Color)value;
                graphics.DrawString(lclcolor.Name, cellStyle.Font, Brushes.Black, textBoxRect);

                cellBackground.Dispose();
            }
        }

        public override object ParseFormattedValue(
            object formattedValue,
            DataGridViewCellStyle cellStyle,
            TypeConverter formattedValueTypeConverter,
            TypeConverter valueTypeConverter)
        {
            int result;
            
            if (int.TryParse(formattedValue.ToString(), NumberStyles.HexNumber, null, out result))
            {
                //Hex number
                return base.ParseFormattedValue(
                    "0x" + formattedValue, 
                    cellStyle, 
                    formattedValueTypeConverter, 
                    valueTypeConverter
                    );
            }
            
            return base.ParseFormattedValue(
                formattedValue, 
                cellStyle, 
                formattedValueTypeConverter, 
                valueTypeConverter
                );
        }

        protected virtual void GetDisplayLayout(
            Rectangle cellRect, 
            ref Rectangle colorBoxRect, 
            ref RectangleF textBoxRect)
        {
            const int distanceFromEdge = 2;

            colorBoxRect.X = cellRect.X + distanceFromEdge;
            colorBoxRect.Y = cellRect.Y + 1;
            colorBoxRect.Size = new Size((int)(1.5 * 17), cellRect.Height - (2 * distanceFromEdge));

            // The text occupies the middle portion.
            textBoxRect = RectangleF.FromLTRB(
                colorBoxRect.X + colorBoxRect.Width + 5,
                colorBoxRect.Y + 2,
                cellRect.X + cellRect.Width - distanceFromEdge,
                colorBoxRect.Y + colorBoxRect.Height
                );
        }
    }
}