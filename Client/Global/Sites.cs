using BH.Tool.RoomConfigurator_Tool.oM;
using BH.Tool.RoomConfigurator_Tool.Client.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BH.oM.BuroHappoldData;
using BH.Tool.RoomConfigurator_Tool.Client.Extensions;

namespace BH.Tool.RoomConfigurator_Tool.Client.Global
{
    public static partial class SharedData
    {
        /***************************************************/
        /**** Sites                                     ****/
        /***************************************************/

        public static event Action OnSiteSet;

        public static List<Site> Sites { get; set; } = new List<Site>();

        public static Site Site { get; private set; } = null;

        /***************************************************/

        public static async Task SetSite(Site site, bool checkForUnsaved = true)
        {
            if (checkForUnsaved && false /* TODO: Replace false with checks for unsaved data. The simplest is to have a 'IsSaved' property/fragment on the related objects */)
            {
                var parameters = new DialogParameters();
                parameters.Add("ContentText", "Some of the data related to this site currently have unsaved changes. Switching the active site will cause the loss of those changes. Are you sure you want to continue ?");

                IDialogReference reference = Dialog.Show<ConfirmationDialog>("Warning: Unsaved data", parameters);
                DialogResult result = await reference.Result;

                if (result.Canceled)
                {
                    OnSiteSet?.Invoke();
                    return;
                }
            }

            Site = site;
            OnSiteSet?.Invoke();

            if (site == null)
            {
                // TODO: reset data related to the site
            }
            else
            {
                SetGlobalMessage("Recovering the data related to the site from the database.");
                // TODO: Recover the data related to the side
                // TODO: mark it as saved using MarkAsSaved()
                SetGlobalMessage(""); ;
            }

            // TODO: Set the site related data to the first available
        }

        /***************************************************/

        public static async Task AddSite(Site site)
        {
            Sites.Add(site);
            await SetSite(site);
        }

        /***************************************************/

        public static async Task DeleteCurrentSite()
        {
            Sites.Remove(Site);
            await SetSite(null);
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
