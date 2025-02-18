using BH.oM.BuroHappoldData;
using BH.Tool.RoomConfigurator_Tool.Client.Extensions;
using BH.Tool.RoomConfigurator_Tool.Client.Global;
using BH.Tool.RoomConfigurator_Tool.oM;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BH.Tool.RoomConfigurator_Tool.Client.Components
{
    public partial class ProjectSelector : ComponentBase
    {
        /***************************************************/
        /**** Public Properties                         ****/
        /***************************************************/

        [Parameter]
        public bool OnlyProject { get; set; } = false;

        [Parameter]
        public EventCallback ProjectSelected { get; set; }

        [Parameter]
        public EventCallback SiteSelected { get; set; }


        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        protected override async Task OnInitializedAsync()
        {
            if (projectReferences.Count() == 0)
            {
                ProjectReference[] projects = await Http.GetFromJsonAsync<ProjectReference[]>($"api/Project");
                projectReferences = projects.ToList();
            }

            selectedProjectRef = new ProjectReference { ProjectId = SharedData.ProjectId, Name = SharedData.ProjectName };

            SharedData.OnProjectSet += () =>
            {
                selectedProjectRef = new ProjectReference { ProjectId = SharedData.ProjectId, Name = SharedData.ProjectName };
                StateHasChanged();
            };

            SharedData.OnSiteSet += () => StateHasChanged();
        }

        /***************************************************/

        protected async Task OnProjectSelected(ProjectReference projectRef)
        {
            selectedProjectRef = projectRef;
            string projectId = projectRef?.ProjectId;

            if (projectId == SharedData.ProjectId)
                return;

            if (string.IsNullOrWhiteSpace(projectRef?.ProjectId))
            {
                await SharedData.SetProject(null);
            }
            else if (useUnlistedProject)
            {
                await SharedData.SetProject(new Project { ProjectId = projectId, Name = projectRef.Name });
            }
            else
            {
                SharedData.SetGlobalMessage("Recovering the project details from the database.");
                Project project = await Http.GetFromBHoMAsync<Project>($"api/Project/details?projectId={projectId}");
                await SharedData.SetProject(project);
                SharedData.SetGlobalMessage("");
            }

            await ProjectSelected.InvokeAsync();
        }

        /***************************************************/

        protected async Task OnSiteSelected(string siteName)
        {
            Site site = SharedData.Sites.FirstOrDefault(x => x?.Name == siteName);
            await SharedData.SetSite(site);
            await SiteSelected.InvokeAsync();
        }

        /***************************************************/

        protected void OnUseUnlistedChanged(bool check)
        {
            useUnlistedProject = check;
        }

        /***************************************************/

        protected async void OnUnlistedProjectIdChanged(string projectId)
        {
            selectedProjectRef.ProjectId = projectId;
            await OnProjectSelected(selectedProjectRef);
        }

        /***************************************************/

        protected string Title()
        {
            string title = "1. Select a project";
            if (!OnlyProject)
                title += " and a site";
            return title;
        }

        /***************************************************/

        public static async Task<IEnumerable<ProjectReference>> MatchingProjectsById(string value)
        {
            if (string.IsNullOrEmpty(value))
                return projectReferences;
            else
                return Enumerable.ToList(await Task.FromResult(projectReferences.Where(x => x.ProjectId.Contains(value, StringComparison.InvariantCultureIgnoreCase)))); ;
        }

        /***************************************************/

        public static async Task<IEnumerable<ProjectReference>> MatchingProjectsByName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return projectReferences;
            else
                return Enumerable.ToList(await Task.FromResult(projectReferences.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)))); ;
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected static ProjectReference selectedProjectRef = null;

        protected static List<ProjectReference> projectReferences { get; set; } = new List<ProjectReference>();

        protected static bool useUnlistedProject = false;

        /***************************************************/
    }
}
