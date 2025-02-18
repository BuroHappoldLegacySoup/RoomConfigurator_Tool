using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using MudBlazor;
using System.Linq;
using BH.Tool.RoomConfigurator.Client.Global;
using BH.oM.Base;
using System;
using BH.Tool.RoomConfigurator.oM;

namespace BH.Tool.RoomConfigurator.Client.Pages
{
    public partial class HomePage : ComponentBase
    {
        /***************************************************/
        /**** Override Methods                          ****/
        /***************************************************/

        protected override void OnInitialized()
        {
            base.OnInitialized();

        }

        
        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        protected async void NavigateTo(string path)
        {
            string alertMessage = "";

            switch (path)
            {
                //TODO: prevent access to specific pages until some conditions are met
                default:
                    break;
            }

            if (string.IsNullOrEmpty(alertMessage))
                Nav.NavigateTo(path);
            else
            {
                await DialogService.ShowMessageBox("Link currently disabled", alertMessage);
                StateHasChanged();
            }
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected List<GuideCard> guides = new List<GuideCard>
        {
            new GuideCard
            {
                Title = "Overview",
                Link = Links.OverviewGuide,
                Description = "Start here to get a general idea of how to use this website."
            },
            // TODO: Add additional cards here
            new GuideCard
            {
                Title = "Frequently asked questions",
                Link = Links.FAQGuide,
                Description = "Got a question about the tool or a suggestion to improve it? Click here!"
            },
        };


        /***************************************************/
        /**** Class Definitions                         ****/
        /***************************************************/

        protected class GuideCard
        {
            public string Title { get; set; }
            public string Link { get; set; }
            public string Description { get; set; }
        }

        /***************************************************/
    }
}
