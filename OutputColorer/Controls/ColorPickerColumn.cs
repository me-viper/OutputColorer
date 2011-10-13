using System;
using System.Drawing;
using System.Windows.Forms;

namespace Talk2Bits.OutputColorer.Controls
{
    public class ColorPickerColumn : DataGridViewColumn
    {
        public ColorPickerColumn() : this(Color.Black)
        {
        }

        public ColorPickerColumn(Color defaultColor) : base(new ColorPickerCell(defaultColor))
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(ColorPickerCell)))
                {
                    throw new InvalidCastException("ColorPicker expected.");
                }

                base.CellTemplate = value;
            }
        }
    }
}