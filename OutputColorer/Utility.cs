using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using Color = System.Windows.Media.Color;

namespace OutputColorer
{
    internal static class Utility
    {
        public static TService GetService<TService>() where TService : class
        {
            return Package.GetGlobalService(typeof(TService)) as TService;
        }

        public static TType GetService<TService, TType>() 
            where TService : class 
            where TType : class
        {
            var result = Package.GetGlobalService(typeof(TService)) as TType;

            if (result == null)
                throw new InvalidOperationException("Failed to get service.");

            return result;
        }

        public static Dictionary<string, FormatInfo> GetColorAndFontSettings()
        {
            var formats = new Dictionary<string, FormatInfo>();
            int hResult = VSConstants.S_OK;

            var cfStorage = GetService<SVsFontAndColorStorage, IVsFontAndColorStorage>();
            var editorCategory = new Guid("{A27B4E24-A735-4d1d-B8E7-9716E1E3D8E0}");
            hResult = cfStorage.OpenCategory(
                ref editorCategory,
                (uint)(__FCSTORAGEFLAGS.FCSF_READONLY | __FCSTORAGEFLAGS.FCSF_LOADDEFAULTS)
                );
            ErrorHandler.ThrowOnFailure(hResult);

            formats.Add(OutputClassifierDefinitions.Noise, GetColor(OutputClassifierDefinitions.Noise, cfStorage));
            formats.Add(OutputClassifierDefinitions.Error, GetColor(OutputClassifierDefinitions.Error, cfStorage));
            formats.Add(OutputClassifierDefinitions.Warning, GetColor(OutputClassifierDefinitions.Warning, cfStorage));
            formats.Add(OutputClassifierDefinitions.StackTrace, GetColor(OutputClassifierDefinitions.StackTrace, cfStorage));
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