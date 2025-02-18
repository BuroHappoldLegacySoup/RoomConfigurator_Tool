using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using MudBlazor;
using BH.Tool.RoomConfigurator_Tool.Client.Global;

namespace BH.Tool.RoomConfigurator_Tool.Client.Pages
{
    public partial class AboutPage : ComponentBase
    {
        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected List<UsefulLink> links = new List<UsefulLink>
        {
            new UsefulLink
            {
                Name = "Design documents",
                Link = "",
                Description = "Sets of document covering the development process of this tool (e.g.User requirements, mockups, development roadmap, ...)."
            },
            new UsefulLink
            {
                Name = "Testing procedure",
                Link = "",
                Description = "Detailed description of the procedure followed to make sure the tool is functioning correctly."
            },
            new UsefulLink
            {
                Name = "Issue list",
                Link = "",
                Description = "List of known bugs or requested features awaiting development. Feel free to raise your own issue there if it is not already in the list."
            },
            new UsefulLink
            {
                Name = "Source code",
                Link = "",
                Description = "GitHub repository containing all the code making this tool a reality."
            },
            new UsefulLink
            {
                Name = "BHoM website",
                Link = "https://bhom.xyz/",
                Description = "The framework powering this tool."
            }
        };

        /***************************************************/

        protected List<Tool> tools = new List<Tool>
        {
            new Tool
            {
                Name = "BHoM",
                Link = "https://bhom.xyz/",
                Version = BH.Engine.Base.Query.BHoMVersion(),
                Description = "Framework for object definitions, serialisation, versioning, adapters, analytics and method libraries."
            },
            new Tool
            {
                Name = "SQL Toolkit",
                Link = "https://github.com/BHoM/SQL_Toolkit",
                Version = BH.Engine.Base.Query.BHoMVersion(),
                Description = "Adapter to the SQL database."
            },
            //TODO: Add the list of toolkits used by this tool
            new Tool
            {
                Name = "Blazor",
                Link = "https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor",
                Version = "v6.0.1",
                Description = "Web framework for building interactive web UIs using C# instead of JavaScript."
            },
            new Tool
            {
                Name = "MudBlazor",
                Link = "https://mudblazor.com/",
                Version = "v6.0.2",
                Description = "Component library for Blazor."
            }
        };

        /***************************************************/

        protected List<Version> versions = new List<Version>
        {
            new Version { Number = "v0.0", BHoMVersion="v6.1", Type = VersionType.Alpha, ReleaseDate = new DateTime(2023,3, 24), MainUpdates="Initial version" } // TODO: Correct the BHoM version and data
            // TODO: Add new versions each time you update the tool
        };

        /***************************************************/

        protected Dictionary<VersionType, Color> TypeColor = new Dictionary<VersionType, Color>
        {
            {VersionType.Alpha, Color.Warning},
            {VersionType.Beta, Color.Info},
            {VersionType.Release, Color.Success},
            {VersionType.Patch, Color.Default}
        };


        /***************************************************/
        /**** Class Definitions                         ****/
        /***************************************************/

        protected class UsefulLink
        {
            public string Name { get; set; }
            public string Link { get; set; }
            public string Description { get; set; }
        }

        /***************************************************/

        protected class Tool
        {
            public string Name { get; set; }
            public string Link { get; set; }
            public string Version { get; set; }
            public string Description { get; set; }
        }

        /***************************************************/

        protected enum VersionType
        {
            Alpha,
            Beta,
            Release,
            Patch
        }

        /***************************************************/

        protected class Version
        {
            public string Number { get; set; }
            public string BHoMVersion { get; set; }
            public VersionType Type { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string MainUpdates { get; set; }
        }

        /***************************************************/
    }
}
