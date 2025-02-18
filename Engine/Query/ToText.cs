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

using BH.Engine.Base;
using BH.oM.Base;
using BH.oM.Base.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BH.Tool.RoomConfigurator_Tool.Engine
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static string ToText(this double number)
        {
            double abs = Math.Abs(number);

            if (abs < 0.005)
                return number.ToString("N3", CultureInfo.GetCultureInfo("en-GB").NumberFormat);
            else if (abs < 1)
                return number.ToString("N2", CultureInfo.GetCultureInfo("en-GB").NumberFormat);
            else if (abs < 10)
                return number.ToString("N1", CultureInfo.GetCultureInfo("en-GB").NumberFormat);
            else
                return number.ToString("N0", CultureInfo.GetCultureInfo("en-GB").NumberFormat);
        }

        /***************************************************/
        
        public static string ToText(this PropertyInfo property)
        {
            if (property == null)
                return "null";

            DisplayTextAttribute[] attributes = property.GetCustomAttributes(typeof(DisplayTextAttribute), false) as DisplayTextAttribute[];

            if (attributes != null && attributes.Count() > 0)
                return attributes.First().Text;
            else
                return property.Name;
        }

        /***************************************************/
    }
}
