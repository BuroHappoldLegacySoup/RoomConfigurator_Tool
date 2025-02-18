
using BH.Engine.Base;
using BH.Tool.RoomConfigurator_Tool.oM;
using BH.Tool.RoomConfigurator_Tool.Client.Extensions;
using BH.Tool.RoomConfigurator_Tool.Client.Global;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BH.Tool.RoomConfigurator_Tool.Client.Components
{
    public partial class SaveDialog : ComponentBase
    {
        /***************************************************/
        /**** Parameters                                ****/
        /***************************************************/

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }



        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        protected override void OnInitialized()
        {
            base.OnInitialized();

            m_InvalidItems = new List<InvalidItem>(); //TODO: check if your data is valid before sending it to the server and record any problem.
        }

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
                MudDialog.Close();
            }
            else
            {
                m_Confirmed = true;

                if (SharedData.ProjectId.Length == 0)
                {
                    m_Errors = new List<string> { "You need to select a project first" };
                }
                else
                {
                    PostResult result = null;

                    try
                    {
                        //TODO: Replace with what you actually want to save
                        HttpResponseMessage response = await Http.PostAsBHoMAsync("api/Example", SharedData.Project);

                        result = await response.Content.ReadFromJsonAsync<PostResult>();

                        if (!result.Success)
                            m_Errors = result.Errors.ToList();
                        else
                        {
                            m_Errors = new List<string>();
                            // TODO: Tag your object as saved (use property or fragment)
                        }                          
                    }
                    catch (Exception e)
                    {
                        m_Errors = new List<string> { "Failed to send the assessment to the database: " + e.Message };
                    } 
                }

                this.StateHasChanged();
            }
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected bool m_Confirmed = false;

        protected List<InvalidItem> m_InvalidItems = new List<InvalidItem>();

        protected List<string> m_Errors = new List<string>();


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected class InvalidItem
        {
            public string Name { get; set; } = "";

            public string ErrorDescription { get; set; } = "";

            public string ToText()
            {
                return Name + " : " + ErrorDescription;
            }
        }



        /***************************************************/
    }
}
