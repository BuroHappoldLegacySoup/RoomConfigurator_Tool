using BH.Tool.RoomConfigurator.oM;
using BH.Tool.RoomConfigurator.Client.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BH.oM.BuroHappoldData;

namespace BH.Tool.RoomConfigurator.Client.Global
{
    public static partial class SharedData
    {
        /***************************************************/
        /**** Main Layout                               ****/
        /***************************************************/

        public static event Action OnGlobalMessage;

        public static event Action OnNavigationRefreshRequest;

        /***************************************************/

        public static HttpClient Http { get; set; } = null; 

        public static IDialogService Dialog { get; set; } = null;

        public static bool IsTestMode { get; private set; } = false;

        public static string Username { get; set; } = "";

        public static string GlobalMessage { get;  private set; } = "";

        public static string ToolTitle { get; } = "Add Tool Title Here"; // TODO: replace with the actual tool title.


        /***************************************************/

        public static async Task SetTestMode(bool isTestMode)
        {
            IsTestMode = isTestMode;
            await SetProject(null);
        }

        /***************************************************/

        public static void SetGlobalMessage(string message)
        {
            GlobalMessage = message;
            OnGlobalMessage?.Invoke();
        }


        /***************************************************/
        /**** Utility Methods                           ****/
        /***************************************************/

        public static void RequestNavigationRefresh()
        {
            OnNavigationRefreshRequest?.Invoke();
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/



        /***************************************************/
    }
}
