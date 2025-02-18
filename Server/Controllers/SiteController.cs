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

using BH.oM.Base.Debugging;
using BH.oM.BuroHappoldData;
using BH.oM.Test.UnitTests;
using BH.Tool.RoomConfigurator.oM;
using BH.Tool.RoomConfigurator.Server.BHoM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Tool.RoomConfigurator.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        /***************************************************/
        /**** API Methods                               ****/
        /***************************************************/

        [HttpGet("details")]
        [Produces("application/bhom")]
        public List<Site> Get(string projectId, bool isTestData)
        {
            return BHDataAdapter.PullSites(projectId, isTestData, HttpContext.UserName());
        }

        /***************************************************/

        // The site will probably be pushed by the toolkit adapter alongside the data you're saving but 
        // - this is a good example on how to handle a post query on the server
        // - can be used in case you need to save sites separately
        [HttpPost]
        [Consumes("application/bhom")]
        public PostResult Post(Site site)
        {
            if (site == null)
                return new PostResult { Success = false, Errors = new List<string> { "The data received is null" } };
            else
            {
                TestModeFragment testFragment = site.Fragments.OfType<TestModeFragment>().FirstOrDefault();
                bool isTestData = testFragment == null ? false : testFragment.IsTestMode;
                return BHDataAdapter.PushSites(site, isTestData, HttpContext.UserName());
            }
        }

        /***************************************************/
    }
}
