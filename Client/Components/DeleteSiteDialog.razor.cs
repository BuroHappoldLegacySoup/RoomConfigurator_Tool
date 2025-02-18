using BH.Tool.RoomConfigurator.Client.Global;
using BH.Tool.RoomConfigurator.oM;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static System.Net.WebRequestMethods;
using System.Net.Http;

namespace BH.Tool.RoomConfigurator.Client.Components
{
    public partial class DeleteSiteDialog : ComponentBase
    {
        /***************************************************/
        /**** Parameters                                ****/
        /***************************************************/

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }



        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/


        protected void Cancel()
        {
            MudDialog.Cancel();
        }

        /***************************************************/

        protected async void Confirm()
        {
            if (m_Confirmed)
            {
                MudDialog.Close(DialogResult.Ok(m_Success));
            }
            else
            {
                await SharedData.DeleteCurrentSite();

                m_Confirmed = true;

                this.StateHasChanged();
            }
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected bool m_Confirmed = false;
        protected bool m_Success = false;

        protected string m_ErrorMessage = "";


        /***************************************************/
    }
}
