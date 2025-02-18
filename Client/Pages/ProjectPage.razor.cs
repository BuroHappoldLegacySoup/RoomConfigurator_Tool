using BH.oM.BuroHappoldData;
using BH.Tool.RoomConfigurator.Client.Global;
using BH.Tool.RoomConfigurator.oM;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BH.Tool.RoomConfigurator.Client.Pages
{
    public partial class ProjectPage : ComponentBase
    {
        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        protected override void OnInitialized()
        {
            SharedData.OnProjectSet += () => StateHasChanged();
        }

        /***************************************************/

        protected void OnProjectSelected()
        {
            StateHasChanged();
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected static Type thisType = typeof(Project);

        /***************************************************/
    }
}
