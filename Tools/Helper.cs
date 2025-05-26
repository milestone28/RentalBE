

using Tools.Extensions;
using Tools.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Tools
{
    public static class Helper
    {
        /// New line
        public static string newLine = "\r\n";

        /// Convert object model to string
        public static string toJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// Convert string to object model
        public static T toOBJECT<T>(this string strobj)
        {
            return (T)JsonConvert.DeserializeObject<T>(strobj);
        }

        /// Return object string description
         public static string toDescription(this object val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// Replace unwanted character from the string
        public static string fixMe(string varmsgtxt)
        {
            string result = "";
            if ((varmsgtxt == null) || Regex.Replace(varmsgtxt, @"\s+", "") == "")
            {
                result = "";
            }
            else
            {
                result = varmsgtxt.Replace("'", "''");
                result = result.Replace(@"\", @"\\");
                result = result.Replace("~", "");
                result = result.Replace("`", "");
            }
            return result;
        }

        /// Check if email is valid
        public static bool isValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Check if string is alpha numeric
        public static bool isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
            return rg.IsMatch(strToCheck);
        }

        // Compare old entry and new entry and get the changes
        public static List<change_log> getChanges(object oldEntry, object newEntry)
        {
            List<change_log> logs = new List<change_log>();

            var oldType = oldEntry.GetType();
            var newType = newEntry.GetType();
            if (oldType != newType)
            {
                return logs; //Types don't match, cannot log changes
            }

            var oldProperties = oldType.GetProperties();
            var newProperties = newType.GetProperties();

            var dateChanged = DateTime.Now;
            var tmpprm = oldProperties.Where(x => Attribute.IsDefined(x, typeof(LoggingPrimaryKey))).First().GetValue(oldEntry);
            var primaryKey = tmpprm.ToString();

            var className = oldEntry.GetType().Name;

            foreach (var oldProperty in oldProperties)
            {
                var matchingProperty = newProperties.Where(x => !Attribute.IsDefined(x, typeof(IgnoreLogging))
                                                                && x.Name == oldProperty.Name
                                                                && x.PropertyType == oldProperty.PropertyType)
                                                    .FirstOrDefault();
                if (matchingProperty == null)
                {
                    continue; //If we don't find a matching property, move on to the next property.
                }

                string oldValue = "";
                var newValue = "";

                try
                {
                    oldValue = oldProperty.GetValue(oldEntry).ToString();
                }
                catch
                {
                    oldValue = "";
                }

                try
                {
                    newValue = matchingProperty.GetValue(newEntry).ToString();
                }
                catch
                {
                    newValue = "";
                }

                if (matchingProperty != null && oldValue != newValue)
                {
                    logs.Add(new change_log()
                    {
                        PrimaryKey = primaryKey,
                        DateChanged = dateChanged,
                        ClassName = className,
                        PropertyName = matchingProperty.Name,
                        OldValue = oldValue, // oldProperty.GetValue(oldEntry).ToString(),
                        NewValue = newValue // matchingProperty.GetValue(newEntry).ToString()
                    });
                }
            }
            return logs;
        }

        // Check role claim
        public static bool checkRoleClaim(string whattocheck, ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                bool result = false;

                Claim clm = claims.Where(x => x.Type.Equals(ClaimTypes.Role) && x.Value.Equals(whattocheck)).FirstOrDefault();
                if (clm != null) result = true;

                return result;
            }
            return false;
        }

        // Check if valid IPv4
        public static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;
            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}
