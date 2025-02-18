using BH.Tool.RoomConfigurator.Client.Components;
using BH.Tool.RoomConfigurator.Client.Global;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using System.Linq;

namespace BH.Tool.RoomConfigurator.Client.Shared
{
    public partial class NavMenu : ComponentBase
    {
        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SharedData.OnNavigationRefreshRequest += () =>
            {
                StateHasChanged();
            };
        }

        /***************************************************/

        protected override bool ShouldRender()
        {
            bool valueChanged = false;

            // Can be used to define areas of the menu that are only avaialble under specific conditions
            m_IsDataReady = SharedData.ProjectId?.Length > 0; // TODO: replace with actual checks relevant to your case

            return base.ShouldRender() || valueChanged;
        }

        /***************************************************/

        protected void SaveData()
        {
            DialogService.Show<SaveDialog>("Save assessment", new DialogParameters { }, new DialogOptions { DisableBackdropClick = true });
        }

        /***************************************************/

        protected void PrintResults()
        {
            DialogService.Show<PrintDialog>("Print results", new DialogParameters { }, new DialogOptions { DisableBackdropClick = true });
        }



        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        private static bool m_IsDataReady = false;

        /***************************************************/
    }
}
