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
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;


namespace BH.Tool.RoomConfigurator_Tool.Engine
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static T FromJson<T>(string json)
        {
            try
            {
                return (T)BH.Engine.Serialiser.Convert.FromJson(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(T);
            }
        }

        /***************************************************/

        public static List<T> FromJsonArray<T>(string json)
        {
            try
            {
                return BH.Engine.Serialiser.Convert.FromJsonArray(json).OfType<T>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<T>();
            }
        }

        /***************************************************/

        // This improves the Core BHoM method with some performance enhancement
        // If it is ported to the code BHoM, than this method can be simplifed to a simple try-catch
        public static string ToJson(this object obj)
        {
            try
            {
                if (obj == null)
                    return "";

                if (obj is string)
                    return "{ \"_t\": \"System.String\", \"_v\": " + BsonExtensionMethods.ToJson<string>(obj as string) + "}";

                if (obj is IEnumerable && !(obj is IObject) && !(obj is IDictionary))
                    return ToJsonArray((obj as IEnumerable).OfType<object>());

                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.CanonicalExtendedJson };
                BsonDocument doc = BH.Engine.Serialiser.Convert.ToBson(obj);

                // Remove the IBHoMObject properties that have a default value -> Reduces the bandwith usage
                if (obj is BHoMObject)
                {
                    IBHoMObject bhom = obj as IBHoMObject;
                    if (bhom.Name.Length == 0)
                        doc.Remove("Name");
                    if (bhom.Fragments.Count == 0)
                        doc.Remove("Fragments");
                    if (bhom.Tags.Count == 0)
                        doc.Remove("Tags");
                    if (bhom.CustomData.Count == 0)
                        doc.Remove("CustomData");

                    doc.Remove("_bhomVersion");
                }

                return doc.ToJson(jsonWriterSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }


        /*******************************************/

        public static string ToJsonArray(this IEnumerable<object> objs)
        {
            try
            {
                List<string> allLines = new List<string>();
                string json = "";

                // Join all individually serialised objects with a comma and a newline
                json = string.Join(",\n", objs.Where(c => c != null).Select(obj => ToJson(obj)));

                // Put between square brackets, to make a valid JSON array.
                json = "[" + json + "]";

                return json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "[]";
            }
        }

        /*******************************************/

    }
}
