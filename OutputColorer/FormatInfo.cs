using System;
using System.Windows.Media;

namespace OutputColorer
{
    internal sealed class FormatInfo
    {
        public Color? ForegroundColor { get; set; }
        public Color? BackGroundColor { get; set; }
        public bool? IsBold { get; set; }
    }
}