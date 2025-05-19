using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public static class Hasher
    {
        #region "variables"
        private const int saltLengthLimit = 32;
        private const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        private const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMBERS = "123456789";
        private const string SPECIALS = @"!@#$%^&*?_";
        #endregion

        #region "public methods"
        public static string createMd5(string salt, string tohash)
        {
            string hash = "";
            string concatenated = "";
            try
            {
                concatenated = salt + tohash;
                using (MD5 md5Hash = MD5.Create())
                {
                    hash = GetMd5Hash(md5Hash, concatenated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hash;
        }

        public static string createRequesttoken(string concatenated)
        {
            string hash = "";
            try
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    hash = GetMd5Hash(md5Hash, concatenated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hash;
        }

        public static string createSha1(string tohash)
        {
            byte[] data = Encoding.UTF8.GetBytes(tohash);
            HashAlgorithm hash = new SHA1Managed();

            byte[] hashBytes = hash.ComputeHash(data);
            StringBuilder hashValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            Array.ForEach<byte>(hashBytes, delegate (byte b) { hashValue.Append(b.ToString("X2")); });

            // return hexadecimal string
            return hashValue.ToString().ToLower();
        }

        public static byte[] getSalt()
        {
            return GetSalt(saltLengthLimit);
        }

        public static string generatePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize)
        {
            char[] _password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            System.Random _random = new Random();
            int counter;

            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE;
            if (useUppercase) charSet += UPPER_CAES;
            if (useNumbers) charSet += NUMBERS;
            if (useSpecial) charSet += SPECIALS;
            for (counter = 0; counter < passwordSize; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }
            return String.Join(null, _password);
        }
        #endregion

        #region "private methods"
        private static void Sort(JObject jObj)
        {
            var props = jObj.Properties().ToList();
            foreach (var prop in props)
            {
                prop.Remove();
            }

            foreach (var prop in props.OrderBy(p => p.Name))
            {
                jObj.Add(prop);
                if (prop.Value is JObject)
                    Sort((JObject)prop.Value);
            }
        }
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }
        #endregion
    }
}
