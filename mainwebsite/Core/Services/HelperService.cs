using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaroonVillage.Core.Services
{
    public interface IDbHelperService
    {
        //Database GetNamedDatabase(string namedDatabase);
        //Database GetDefaultDatabase();
    }

    public class CoreHelperService : IDbHelperService
    {
        public const string PipeDelimeter = "|";
        //private static readonly Lazy<Database> database = new Lazy<Database>(() => DatabaseFactory.CreateDatabase("MaroonVillageDb"));
        //private static readonly Database database = DatabaseFactory.CreateDatabase("MaroonVillageDb");
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="namedDatabase"></param>
        ///// <returns></returns>
        //public static Database GetNamedDatabase(string namedDatabase)
        //{
        //    return DatabaseFactory.CreateDatabase(namedDatabase);

        //}// end method: GetNamedDatabase

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public static Database GetDefaultDatabase()
        //{
        //    return database;

        //}// end method: GetNamedDatabase

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //Database IDbHelperService.GetDefaultDatabase()
        //{
        //    return GetDefaultDatabase();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="namedDatabase"></param>
        ///// <returns></returns>
        //Database IDbHelperService.GetNamedDatabase(string namedDatabase)
        //{
        //    return GetNamedDatabase(namedDatabase);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes
                                             (typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetEnumDefaultValue(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DefaultValueAttribute[])fi.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            return attributes.Any() ? (int)attributes[0].Value : 0;
        }

        public static string BuildSeparatedList(string[] tokens, string delim = PipeDelimeter)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (string s in tokens)
            {
                sb.Append(s);
                sb.Append(PipeDelimeter);
            }

            return sb.ToString().TrimEnd(delim.ToCharArray());
        }


        #region Common Static Helper Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static bool GetBoolean(string inputValue)
        {
            var returnValue = false;
            if (!string.IsNullOrEmpty(inputValue))
                returnValue = !(inputValue.Equals("false", StringComparison.OrdinalIgnoreCase) || inputValue.Equals("0", StringComparison.OrdinalIgnoreCase) || inputValue.Equals("n", StringComparison.OrdinalIgnoreCase));
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GetStringWithMaxLength(object inputValue, int maxLength)
        {
            var result = Convert.ToString(inputValue);
            var returnLength = result.Length > maxLength ? maxLength : result.Length;

            return inputValue != null ? result.Substring(0, returnLength) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valuetoParse"></param>
        /// <returns></returns>
        public static bool ParseBoolean(string valuetoParse)
        {
            var tempBool = false;
            if (!string.IsNullOrEmpty(valuetoParse))
                bool.TryParse(valuetoParse, out tempBool);

            return tempBool;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valuetoParse"></param>
        /// <returns></returns>
        public static float ParseFloat(string valuetoParse)
        {
            var tempFloat = 0f;
            if (!string.IsNullOrEmpty(valuetoParse))
                float.TryParse(valuetoParse, out tempFloat);

            return tempFloat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueToParse"></param>
        /// <returns></returns>
        public static int ParseInt(string valueToParse)
        {
            var tempInt = 0;
            if (!string.IsNullOrEmpty(valueToParse))
                int.TryParse(valueToParse, out tempInt);

            return tempInt;
        }

        public static long ParseLong(string valuetoParse)
        {
            var tempLong = 0L;
            if (!string.IsNullOrEmpty(valuetoParse))
                long.TryParse(valuetoParse, out tempLong);

            return tempLong;
        }

        /// <summary>
        ///     Allows for returning a value other than 0 when the string to be parsed is null
        /// </summary>
        /// <param name="valueToParse"></param>
        /// <param name="optionalReturn"></param>
        /// <returns></returns>
        public static int ParseIntSetBaseReturn(string valueToParse, int optionalReturn)
        {
            int tempInt;
            if (optionalReturn != 0 && valueToParse == null)
            {
                tempInt = optionalReturn;
            }
            else
            {
                int.TryParse(valueToParse, out tempInt);
            }

            return tempInt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueToParse"></param>
        /// <returns></returns>
        public static DateTime ParseDate(string valueToParse)
        {
            var tempDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(valueToParse))
                DateTime.TryParse(valueToParse, out tempDate);

            return tempDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueToParse"></param>
        /// <returns></returns>
        public static string ParseString(object valueToParse)
        {
            return valueToParse != null && valueToParse != DBNull.Value && !valueToParse.Equals(DBNull.Value) ? Convert.ToString(valueToParse) : null;
        }
        #endregion


    }

    public static class EnumExtension
    {
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
            //or
            //throw new ArgumentException("Not found.", "description");

        }

    }
}
