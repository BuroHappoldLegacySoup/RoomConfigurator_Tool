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
using BH.oM.Base;
using BH.oM.Base.Debugging;
using BH.oM.BuroHappoldData;
using BH.Tool.SoftwareName.oM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Tool.SoftwareName.Server.BHoM
{
    public static class BHDataAdapter
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static List<ProjectReference> PullProjectIds(string userName)
        {
            BH.Engine.Base.Compute.ClearCurrentEvents();

            // Pull the data with the SQL Adapter
            List<ProjectReference> results = m_Adapter.Pull(new ProjectReferencesRequest()).OfType<ProjectReference>().OrderBy(x => x.ProjectId).ToList();

            // Record action for Analytics
            Analytics.RecordEntry(typeof(ProjectReference), Analytics.Caller.PullCaller, "BuroHappoldData", userName, "", BH.Engine.Base.Query.CurrentEvents());

            // Return the ordered list of project references
            return results.OrderBy(x => x.ProjectId).ToList();
        }

        /***************************************************/

        public static Project PullProject(string projectId, string userName)
        {
            BH.Engine.Base.Compute.ClearCurrentEvents();

            Project result = m_Adapter.Pull(new ProjectByNumberRequest { ProjectNumber = projectId }).OfType<Project>().LastOrDefault();

            // Record action for Analytics
            Analytics.RecordEntry(typeof(Project), Analytics.Caller.PullCaller, "BuroHappoldData", userName, projectId, BH.Engine.Base.Query.CurrentEvents());

            return result;
        }

        /***************************************************/

        public static List<Site> PullSites(string projectId, bool isTestData, string userName)
        {
            BH.Engine.Base.Compute.ClearCurrentEvents();

            // Pull the buildings
            BuroHappoldDataAdapter bhAdapter = isTestData ? m_TestAdapter : m_Adapter;
            List<Site> buildings = bhAdapter.Pull(new SiteRequest { ProjectNumber = projectId }).OfType<Site>().ToList();

            // Record action for Analytics
            Analytics.RecordEntry(typeof(Site), Analytics.Caller.PullCaller, bhAdapter.DatabaseName, userName, projectId, BH.Engine.Base.Query.CurrentEvents());

            return buildings;
        }

        /***************************************************/

        public static PostResult PushSites(Site site, bool isTestData, string userName)
        {
            BH.Engine.Base.Compute.ClearCurrentEvents();

            // Pull the buildings
            BuroHappoldDataAdapter bhAdapter = isTestData ? m_TestAdapter : m_Adapter;
            
            try
            {
                bhAdapter.Push(new List<object> { site });
            }
            catch (Exception e)
            {
                BH.Engine.Base.Compute.RecordError("Failed to send the site to the database: " + e.Message);
            }

            // Record action for Analytics
            List<Event> events = BH.Engine.Base.Query.CurrentEvents().Where(x => x.Type == EventType.Error).ToList();
            Analytics.RecordEntry(typeof(Site), Analytics.Caller.PushCaller, bhAdapter.DatabaseName, userName, site.Project?.ProjectId, events);

            return new PostResult
            {
                //Events = events,
                Errors = events.Select(x => x.Message).ToList(),
                Success = events.Count == 0
            };
        }

        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        private static BuroHappoldDataAdapter m_Adapter = new BuroHappoldDataAdapter("SQL-BHOM01", "BuroHappoldData");
        private static BuroHappoldDataAdapter m_TestAdapter = new BuroHappoldDataAdapter("SQL-BHOM01", "BuroHappoldData_Test");


        /***************************************************/
    }
}
