// Guids.cs
// MUST match guids.h
using System;

namespace Talk2Bits.OutputColorerInstaller
{
    static class GuidList
    {
        public const string guidOutputColorerInstallerPkgString = "8ea955ed-fd1d-4b69-8afe-09d0eb2ac656";
        public const string guidOutputColorerInstallerCmdSetString = "b744e2e5-7794-483a-aeee-7f92890452c2";

        public static readonly Guid guidOutputColorerInstallerCmdSet = new Guid(guidOutputColorerInstallerCmdSetString);
    };
}