// Guids.cs
// MUST match guids.h
using System;

namespace Talk2Bits.OutputColorerInstaller
{
    internal static class GuidList
    {
        public const string guidOutputColorerInstallerPkgString = "0875BBE6-451F-48BE-8F8C-E9EA43609D1A";
        public const string guidOutputColorerInstallerCmdSetString = "36DEADC2-538C-4DC0-A5C5-4CAC15DAE4D8";

        public static readonly Guid guidOutputColorerInstallerCmdSet = new Guid(guidOutputColorerInstallerCmdSetString);
    };
}