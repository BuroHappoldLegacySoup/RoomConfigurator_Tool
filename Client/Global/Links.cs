using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BH.Tool.RoomConfigurator.Client.Global
{
    public static class Links
    {
        /***************************************************/

        public static string OverviewGuide { get; set; } = ""; // TODO: Add http link to document. A possible place to store that document is on SharePoint to facilitate contribution from non-developers

        public static string ProjectGuide { get; set; } = "";

        public static string SiteGuide { get; set; } = "";

        /**** TODO: Add other guides here ****/

        public static string FAQGuide { get; set; } = ""; // TODO: Add http link to document

        /***************************************************/
    }
}

