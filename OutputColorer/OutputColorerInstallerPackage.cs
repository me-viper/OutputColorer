using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using Talk2Bits.OutputColorer.Controls;

namespace Talk2Bits.OutputColorer
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideOptionPageAttribute(typeof(OutputColorerOptionsPage), "Output Colorer", "General", 113, 114, true)]
    [ProvideProfileAttribute(typeof(OutputColorerOptionsPage), "Output Colorer", "Color settings", 113, 115, true, DescriptionResourceID = 101)]    
    [Guid(GuidList.GuidOutputColorerInstallerPkgString)]
    [ProvideAutoLoad("{adfc4e60-0397-11d1-9f4e-00a0c911004f}")] // VSConstants.UICONTEXT_SolutionBuilding
    [ProvideBindingPath]
    public sealed class OutputColorerInstallerPackage : Package, IVsUpdateSolutionEvents2
    {
        private uint _solutionBuildEventsCookie;
        private IVsSolutionBuildManager2 _buildManager;
        private List<string> _buildSummary = new List<string>();

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public OutputColorerInstallerPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));            
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Get solution build manager
            _buildManager = ServiceProvider.GlobalProvider.GetService(typeof(SVsSolutionBuildManager)) as IVsSolutionBuildManager2;
            
            if (_buildManager != null)
                _buildManager.AdviseUpdateSolutionEvents(this, out _solutionBuildEventsCookie);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (_buildManager != null)
                    _buildManager.UnadviseUpdateSolutionEvents(_solutionBuildEventsCookie);
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        int IVsUpdateSolutionEvents.UpdateSolution_Begin(ref int pfCancelUpdate)
        {
            ((IVsUpdateSolutionEvents2)this).UpdateSolution_Begin(ref pfCancelUpdate);
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents.UpdateSolution_Done(int fSucceeded, int fModified, int fCancelCommand)
        {
            ((IVsUpdateSolutionEvents2)this).UpdateSolution_Done(fSucceeded, fModified, fCancelCommand);
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents.UpdateSolution_StartUpdate(ref int pfCancelUpdate)
        {
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents.UpdateSolution_Cancel()
        {
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents.OnActiveProjectCfgChange(IVsHierarchy pIVsHierarchy)
        {
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents2.UpdateSolution_Begin(ref int pfCancelUpdate)
        {
            _buildSummary.Clear();
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents2.UpdateSolution_Done(int fSucceeded, int fModified, int fCancelCommand)
        {
            var dte = (DTE2)GetService(typeof(DTE));
            var panes = dte.ToolWindows.OutputWindow.OutputWindowPanes;

            OutputWindowPane buildPane;

            try
            {
                buildPane = panes.Item("Build");
            }
            catch (ArgumentException)
            {
                Logger.Instance.Trace("Couldn't find Build output pane.");
                return VSConstants.S_OK;
            }

            foreach (var entry in _buildSummary)
                buildPane.OutputString(entry + Environment.NewLine);

            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents2.UpdateSolution_StartUpdate(ref int pfCancelUpdate)
        {
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents2.UpdateSolution_Cancel()
        {
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents2.OnActiveProjectCfgChange(IVsHierarchy pIVsHierarchy)
        {
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents2.UpdateProjectCfg_Begin(
            IVsHierarchy pHierProj, 
            IVsCfg pCfgProj, 
            IVsCfg pCfgSln, 
            uint dwAction, 
            ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        int IVsUpdateSolutionEvents2.UpdateProjectCfg_Done(
            IVsHierarchy pHierProj, 
            IVsCfg pCfgProj, 
            IVsCfg pCfgSln, 
            uint dwAction, 
            int fSuccess, 
            int fCancel)
        {
            object o;
            pHierProj.GetProperty((uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_Name, out o);
            var projectName = o as string;
            
            _buildSummary.Add(
                string.Format(
                    "{0} Build completed: Project: {1} {2} {0}", 
                    string.Concat(Enumerable.Repeat("-", 5)),
                    projectName, 
                    fSuccess == 1 ? "succeeded" : "failed"
                    )
                );

            return VSConstants.S_OK;
        }
    }
}
