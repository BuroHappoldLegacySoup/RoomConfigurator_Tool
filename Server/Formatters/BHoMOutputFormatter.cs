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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using BH.oM.Base.Attributes;
using System.ComponentModel;

namespace BH.Tool.SoftwareName.Server.Formatters
{
    public class BHoMOutputFormatter : OutputFormatter
    {
        public BHoMOutputFormatter()
        {
            this.SupportedMediaTypes.Clear();

            //Look for specific media type declared with Content-Type header in request  
            this.SupportedMediaTypes.Add("application/bhom");
        }

        protected override bool CanWriteType(Type type)
        {
            return true; // As long as the media type was set to bhom, we trust the user to know it is compatible
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            try
            {
                string json = BH.Tool.SoftwareName.Engine.Convert.ToJson(context.Object);
                await response.WriteAsync(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /*******************************************/
    }
}


