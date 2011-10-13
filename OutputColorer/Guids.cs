// Guids.cs
// MUST match guids.h
using System;

namespace Talk2Bits.OutputColorer
{
    internal static class GuidList
    {
        public const string GuidOutputColorerInstallerPkgString = "0875BBE6-451F-48BE-8F8C-E9EA43609D1A";
        public const string GuidOutputColorerInstallerCmdSetString = "36DEADC2-538C-4DC0-A5C5-4CAC15DAE4D8";
        public const string GuidOptionsPageString = "80E49DA6-5165-49FF-A07E-B57727CF4F79";

        public static readonly Guid GuidOutputColorerInstallerCmdSet = new Guid(GuidOutputColorerInstallerCmdSetString);
        public static readonly Guid GuidOptionsPage = new Guid(GuidOptionsPageString);
    };
}