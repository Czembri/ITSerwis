using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ItSerwis_Merge_v2
{
    public class Encryptor : IEncryptor
    {
        /// <summary>
        /// method that encrypts user login and password - encrypted data is stored in database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string EncryptData(string data)
        {
            MD5 md5Hash = MD5.Create();

            byte[] HashedData = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < HashedData.Length; i++)
            {
                sBuilder.Append(HashedData[i].ToString("x2"));
            }
            var encrypted = sBuilder.ToString();
            return encrypted;

        }
    }

}
