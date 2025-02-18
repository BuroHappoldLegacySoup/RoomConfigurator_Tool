/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2023, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using BH.Adapter.BuroHappoldData;
using BH.Adapter.SQL;
using BH.Engine.Base;
using BH.oM.Adapters.SQL;
using BH.oM.Base;
using BH.oM.Base.Debugging;
using BH.oM.BHoMAnalytics;
using BH.oM.BuroHappoldData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Tool.RoomConfigurator_Tool.Server.BHoM
{
    public static class Analytics
    {
        /***************************************************/
        /**** Public Enum                               ****/
        /***************************************************/

        public enum Caller
        {
            Undefined,
            PushCaller,
            PullCaller,
            RemoveCaller
        }


        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static void RecordEntry(Type objectType, Caller caller, string database, string userName, string projectId, List<Event> events)
        {
            try
            {
                WebUsageEntry entry = new WebUsageEntry
                {
                    Toolkit = "RoomConfigurator_Tool",
                    ItemName = objectType.Name,
                    ItemType = ItemType.Type,
                    FullName = objectType.ToText(true),
                    CallerName = caller.ToString(),
                    TimeStamp = DateTime.UtcNow,
                    Website = "", //TODO: define the website link here
                    Database = database,
                    UserName = userName,
                    ProjectID = projectId,
                    BHoMVersion = m_BHoMVersion,
                    ErrorCount = events.Count(x => x.Type == EventType.Error)
                };

                m_Adapter.Push(new List<object> { entry }, "", BH.oM.Adapter.PushType.AdapterDefault, m_PushConfig);
            }
            catch { }
        }


        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/



        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        private static SqlAdapter m_Adapter = new SqlAdapter("SQL-BHOM01", "BHoMAnalytics_Dev");
        private static PushConfig m_PushConfig = new PushConfig { Table = "WebUsageData" };
        private static string m_BHoMVersion = BH.Engine.Base.Query.BHoMVersion();

        /***************************************************/
    }
}
