using BH.Tool.SoftwareName.oM;
using BH.Tool.SoftwareName.Engine;
using BH.Tool.SoftwareName.Client.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BH.oM.BuroHappoldData;
using BH.oM.Environment.Elements;
using BH.Tool.SoftwareName.Client.Extensions;

namespace BH.Tool.SoftwareName.Client.Global
{
    public static partial class SharedData
    {
        /***************************************************/
        /**** Project                                   ****/
        /***************************************************/

        public static event Action OnProjectSet;

        public static Project Project { get; private set; } = null;

        public static string ProjectId => Project?.ProjectId;

        public static string ProjectName => Project?.Name;

        /***************************************************/

        public static async Task SetProject(Project project, bool checkForUnsaved = true)
        {
            if (checkForUnsaved && (Sites.Any(x => !x.IsSaved() || false)) /* TODO: Replace false with checks for unsaved data. The simplest is to have a 'IsSaved' property/fragment on the related objects */)
            {
                var parameters = new DialogParameters();
                parameters.Add("ContentText", "Some of the data related to this project currently have unsaved changes. Switching the active project will cause the loss of those changes. Are you sure you want to continue ?");

                IDialogReference reference = Dialog.Show<ConfirmationDialog>("Warning: Unsaved data", parameters);
                DialogResult result = await reference.Result;

                if (result.Canceled)
                {
                    OnProjectSet?.Invoke();
                    return;
                }
            }

            Project = project;
            OnProjectSet?.Invoke();

            // Load data related to this project 
            if (string.IsNullOrEmpty(project?.ProjectId))
            {
                Sites = new List<Site>();
            }
            else
            {
                SetGlobalMessage("Recovering the list of sites from the database.");
                Sites = await Http.GetFromBHoMListAsync<Site>($"api/Site/details?projectId={project.ProjectId}&&isTestData={IsTestMode.ToString().ToLower()}");
                Sites.ForEach(x => x.MarkAsSaved());
                SetGlobalMessage("");
            }

            if (Sites.Count == 1)
                await SetSite(Sites.First(), false);
            else
                await SetSite(null, false);

            
            RequestNavigationRefresh(); // This asks the menu to update the avility of its links 
        }


        /***************************************************/
        /**** Utility Methods                           ****/
        /***************************************************/


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/


        /***************************************************/
    }
}
