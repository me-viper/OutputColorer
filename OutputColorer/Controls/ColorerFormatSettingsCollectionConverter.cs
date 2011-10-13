using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Talk2Bits.OutputColorer.Controls
{
    public class ColorerFormatSettingsCollectionConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var result = new StringBuilder();

            using (var xw = XmlWriter.Create(new StringWriter(result), new XmlWriterSettings { CloseOutput = true }))
            {
                var sr = new DataContractSerializer(typeof(Collection<ColorerFormatSetting>));
                sr.WriteObject(xw, value);
            }

            return result.ToString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Collection<ColorerFormatSetting> result; 

            using (var xr = XmlReader.Create(new StringReader(value.ToString()), new XmlReaderSettings { CloseInput = true }))
            {
                var sr = new DataContractSerializer(typeof(Collection<ColorerFormatSetting>));
                result = (Collection<ColorerFormatSetting>)sr.ReadObject(xr);
            }

            return result;
        }
    }
}