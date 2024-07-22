using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class VnPayLibrary
    {
        private SortedList<string, string> requestData = new SortedList<string, string>(new VnPayCompare());
        private SortedList<string, string> responseData = new SortedList<string, string>(new VnPayCompare());

        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                requestData.Add(key, value);
            }
        }

        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                responseData.Add(key, value);
            }
        }

        public string GetResponseData(string key)
        {
            return responseData.ContainsKey(key) ? responseData[key] : null;
        }

        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in requestData)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    data.Append(kv.Key + "=" + kv.Value + "&");
                }
            }
            data.Remove(data.Length - 1, 1); // remove the last '&'

            string rawData = data.ToString();
            string vnp_SecureHash = HmacSHA512(vnp_HashSecret, rawData);
            string queryString = rawData + "&vnp_SecureHash=" + vnp_SecureHash;

            return baseUrl + "?" + queryString;
        }

        public bool ValidateSignature(string inputHash, string secretKey)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in responseData)
            {
                if (!string.IsNullOrEmpty(kv.Value) && kv.Key.StartsWith("vnp_"))
                {
                    data.Append(kv.Key + "=" + kv.Value + "&");
                }
            }
            data.Remove(data.Length - 1, 1); // remove the last '&'

            string rawData = data.ToString();
            string myChecksum = HmacSHA512(secretKey, rawData);

            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

        private string HmacSHA512(string key, string inputData)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }
    }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            return string.CompareOrdinal(x, y);
        }
    }
}