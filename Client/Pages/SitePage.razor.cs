using Azure.Core;
using BH.Engine.Base;
using BH.Engine.BuroHappoldData;
using BH.Engine.Reflection;
using BH.oM.Base;
using BH.oM.BuroHappoldData;
using BH.Tool.SoftwareName.Client.Components;
using BH.Tool.SoftwareName.Client.Global;
using BH.Tool.SoftwareName.Engine;
using BH.Tool.SoftwareName.oM;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BH.Tool.SoftwareName.Client.Pages
{
    public partial class SitePage : ComponentBase
    {
        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        protected override void OnInitialized()
        {
            SharedData.OnSiteSet += () => StateHasChanged();
        }

        /***************************************************/

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (SharedData.Site != null)
                SharedData.RequestNavigationRefresh();
        }

        /***************************************************/

        protected async void OnSiteSelected(Guid siteId = default(Guid))
        {
            if (siteId != default(Guid))
            {
                Site site = SharedData.Sites.Where(x => x.BHoM_Guid == siteId).FirstOrDefault();
                if (site != null)
                    await SharedData.SetSite(site);
            }
            
            StateHasChanged();
        }

        /***************************************************/

        protected async Task OnNewEntryClick()
        {
            Site newEntry = new Site { Name = "New Entry" };
            newEntry.Fragments.Add(new Creator { Name = SharedData.Username });
            newEntry.Project = new ProjectReference { ProjectId = SharedData.ProjectId, Name = SharedData.ProjectName };

            await SharedData.AddSite(newEntry);
        }

        /***************************************************/

        protected string DeleteTooltipText()
        {
            if (SharedData.Site == null)
                return "You need to select a site first";
            else if (SharedData.Username != SharedData.Site.Creator())
                return "The building can only be deleted by its creator";
            else if (SharedData.Site.IsSaved())
                return "The building cannot be deleted once it has been saved in the database as there might be data attached to it.";
            else
                return "";
        }

        /***************************************************/

        protected string SitePropertyName(ISiteProperties siteProperties)
        {
            return SitePropertyName(siteProperties.GetType());
        }

        /***************************************************/

        protected string SitePropertyName(Type type)
        {
            return type.Name.Replace("Site", "").Replace("Details", "");
        }

        /***************************************************/

        protected async Task DuplicateCurrent()
        {

            Site newEntry = SharedData.Site.DeepClone();
            newEntry.BHoM_Guid = new Guid();
            newEntry.Fragments = new FragmentSet { new Creator { Name = SharedData.Username } };

            await SharedData.AddSite(newEntry);
        }

        /***************************************************/

        protected void DeleteCurrent()
        {
            IDialogReference dialog = DialogService.Show<DeleteSiteDialog>("Delete site", new DialogParameters { }, new DialogOptions { DisableBackdropClick = true });
        }

        /***************************************************/

        protected async void AddTab()
        {
            var result = await DialogService.Show<SitePropertiesDialog>().Result;

            if (!result.Canceled)
            {
                Type selectedType = (Type)(result.Data);
                if (selectedType != null)
                {
                    SharedData.Site.Properties.Add(Activator.CreateInstance(selectedType) as ISiteProperties);
                    StateHasChanged();
                }
            }
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected static Type thisType = typeof(Site);

        protected static List<Type> m_DisciplineTypes = typeof(ISiteProperties).Subtypes();
        protected static string m_SelectedSiteProperties = null;

        /***************************************************/
    }
}
