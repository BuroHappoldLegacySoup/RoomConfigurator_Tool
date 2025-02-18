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

using BH.oM.Base;
using BH.Tool.RoomConfigurator.Server.BHoM;
using BH.Tool.RoomConfigurator.oM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BH.oM.BuroHappoldData;

namespace BH.Tool.RoomConfigurator.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        /***************************************************/
        /**** API Methods                               ****/
        /***************************************************/

        [HttpGet()]
        public IEnumerable<ProjectReference> Get()
        {
            try
            {
                return BHDataAdapter.PullProjectIds(HttpContext.UserName());
            }
            catch (Exception e)
            {
                List<ProjectReference> result = new List<ProjectReference> { new ProjectReference { ProjectId = e.Message, Name = e.StackTrace } };

                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    result.Add(new ProjectReference { ProjectId = e.Message, Name = e.StackTrace });
                }

                return result;
            }
        }

        /***************************************************/

        [HttpGet("details")]
        [Produces("application/bhom")] // Use this if you want the data to be serialised with BHoM when sent to client (some BHoM objects do not serialise properly with the default Blazor serialiser)
        public Project Get(string projectId)
        {
            return BHDataAdapter.PullProject(projectId, HttpContext.UserName());
        }

        /***************************************************/
    }
}
