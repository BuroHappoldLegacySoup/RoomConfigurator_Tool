using BH.Engine.Base;
using BH.oM.Base;
using BH.Tool.RoomConfigurator.Client.Global;
using BH.Tool.RoomConfigurator.oM;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BH.Tool.RoomConfigurator.Client.Shared
{
    public partial class MainLayout
    {

        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        protected override void OnInitialized()
        {
            currentTheme = lightTheme;
            SharedData.Http = Http;
            SharedData.Dialog = DialogService;

            SharedData.OnGlobalMessage += () => StateHasChanged();

            JSRuntime.InvokeAsync<string>("setTitle", new object[] { SharedData.ToolTitle });

            NavManager.NavigateTo("/home");

            // Forces the type dictionary to initalise for serialisation's sake.
            List<string> loadedTypes = BH.Engine.Base.Query.BHoMTypeDictionary().Keys.ToList();


            // TODO: Pre-load some datasets
            // Use SharedData.SetGlobalMessage to display a loading message. Set the message back to an empty string when finished
        }

        /***************************************************/

        void ToggleTheme()
        {
            if (currentTheme == lightTheme)
            {
                currentTheme = darkTheme;
            }
            else
            {
                currentTheme = lightTheme;
            }
        }

        /***************************************************/

        void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
        }

        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        bool _drawerOpen = true;
        MudTheme currentTheme = new MudTheme();

        MudTheme lightTheme = new MudTheme()
        {
            Palette = new PaletteLight()
            {
                Black = "#272c34",
                AppbarBackground = "#000000",
                Primary = "#c4d600",
                Success = "#6CC24E",
                Warning = "#EB671C",
                Error = "#D50032",
                Info = "#00A9E0"
            }
        };

        MudTheme darkTheme = new MudTheme()
        {
            Palette = new PaletteDark()
            {
                Black = "#27272f",
                Primary = "#c4d600",
                Success = "#5d822d",
                Warning = "#d06a13",
                Error = "#bc204b",
                Info = "#006da8",
                Background = "#32333d",
                BackgroundGrey = "#27272f",
                Surface = "#373740",
                DrawerBackground = "#27272f",
                DrawerText = "rgba(255,255,255, 0.50)",
                DrawerIcon = "rgba(255,255,255, 0.50)",
                AppbarBackground = "#000000",
                AppbarText = "rgba(255,255,255, 0.70)",
                TextPrimary = "rgba(255,255,255, 0.70)",
                TextSecondary = "rgba(255,255,255, 0.50)",
                ActionDefault = "#adadb1",
                ActionDisabled = "rgba(255,255,255, 0.26)",
                ActionDisabledBackground = "rgba(255,255,255, 0.12)",
                Divider = "rgba(255,255,255, 0.12)",
                DividerLight = "rgba(255,255,255, 0.06)",
                TableLines = "rgba(255,255,255, 0.12)",
                LinesDefault = "rgba(255,255,255, 0.12)",
                LinesInputs = "rgba(255,255,255, 0.3)",
                TextDisabled = "rgba(255,255,255, 0.2)"
            }
        };

        /***************************************************/
    }
}
