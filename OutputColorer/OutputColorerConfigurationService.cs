using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using Color = System.Windows.Media.Color;

namespace OutputColorer
{
    [ComVisible(true)]
    [Guid("B066E361-98CD-4FEE-AAD2-1B01A330EFBC")]
    internal interface IOutputColorerConfigurationService
    {
        FormatInfo GetFontAndColor(string classificationType);
    }
    
    internal sealed class OutputColorerConfigurationService : IOutputColorerConfigurationService
    {
        private Dictionary<string, FormatInfo> _fontsAndColors;

        public OutputColorerConfigurationService()
        {
            _fontsAndColors = GetColorAndFontSettings();
        }
                
        public FormatInfo GetFontAndColor(string classificationType)
        {
            if (_fontsAndColors == null || !_fontsAndColors.ContainsKey(classificationType))
                return new FormatInfo();

            return _fontsAndColors[classificationType];
        }

        private Dictionary<string, FormatInfo> GetColorAndFontSettings()
        {
            var formats = new Dictionary<string, FormatInfo>();
            int hResult = VSConstants.S_OK;

            var cfStorage = Utility.GetService<SVsFontAndColorStorage, IVsFontAndColorStorage>();
            
            if (cfStorage == null)
                throw new InvalidOperationException("Couldn't initialize SVsFontAndColorStorage service.");

            var editorCategory = new Guid("{A27B4E24-A735-4d1d-B8E7-9716E1E3D8E0}");
            hResult = cfStorage.OpenCategory(
                ref editorCategory,
                (uint)(__FCSTORAGEFLAGS.FCSF_READONLY)
                );
            ErrorHandler.ThrowOnFailure(hResult);

            formats.Add(OutputClassifierDefinitions.Noise, GetColor(OutputClassifierDefinitions.Noise, cfStorage));
            formats.Add(OutputClassifierDefinitions.Error, GetColor(OutputClassifierDefinitions.Error, cfStorage));
            formats.Add(OutputClassifierDefinitions.Warning, GetColor(OutputClassifierDefinitions.Warning, cfStorage));
            formats.Add(OutputClassifierDefinitions.Delimiter, GetColor(OutputClassifierDefinitions.Delimiter, cfStorage));
            formats.Add(OutputClassifierDefinitions.Success, GetColor(OutputClassifierDefinitions.Success, cfStorage));

            hResult = cfStorage.CloseCategory();
            ErrorHandler.ThrowOnFailure(hResult);

            return formats;
        }

        private static FormatInfo GetColor(string itemName, IVsFontAndColorStorage storage)
        {
            var result = new FormatInfo();

            try
            {
                var hResult = VSConstants.S_OK;

                var colorableItemInfos = new ColorableItemInfo[1];
                hResult = storage.GetItem(itemName, colorableItemInfos);
                ErrorHandler.ThrowOnFailure(hResult);

                var cii = colorableItemInfos[0];

                if (cii.bForegroundValid == 1)
                {
                    if (!IsAutomaticColor(storage, cii.crForeground))
                    {
                        var color = ColorTranslator.FromWin32((int)cii.crForeground);
                        result.ForegroundColor = Color.FromRgb(color.R, color.G, color.B);
                    }
                }

                if (cii.bBackgroundValid == 1)
                {
                    if (!IsAutomaticColor(storage, cii.crBackground))
                    {
                        var color = ColorTranslator.FromWin32((int)cii.crBackground);
                        result.BackGroundColor = Color.FromRgb(color.R, color.G, color.B);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex);
            }

            return result;
        }

        private static bool IsAutomaticColor(IVsFontAndColorStorage storage, uint color)
        {
            var util = (IVsFontAndColorUtilities)storage;

            var colorType = (int)__VSCOLORTYPE.CT_INVALID;
            int hResult = util.GetColorType(color, out colorType);
            ErrorHandler.ThrowOnFailure(hResult);

            return (__VSCOLORTYPE)colorType == __VSCOLORTYPE.CT_AUTOMATIC;
        }
    }
}