using System;

using Microsoft.VisualStudio.Shell;

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
    }
}