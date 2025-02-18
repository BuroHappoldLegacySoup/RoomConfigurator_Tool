using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BH.Tool.RoomConfigurator_Tool.Client.Components
{
    public partial class PrintDialog : ComponentBase
    {
        /***************************************************/
        /**** Parameters                                ****/
        /***************************************************/

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }



        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        protected void Close()
        {
            MudDialog.Close();
        }

        /***************************************************/

        protected void CreateExcelFile()
        {

        }

        /***************************************************/

        protected void CreatePowerPointFile()
        {

        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/


        /***************************************************/
    }
}
