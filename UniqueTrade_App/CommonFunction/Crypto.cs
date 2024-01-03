using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using System.Web.Mvc;

namespace UniqueTrade_App.CommanFunction
{
    public class Crypto
    {
        public static string Encrypt(string stringvalue, System.Text.Encoding encoding)
        {
            Byte[] stringBytes = encoding.GetBytes(stringvalue);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }
        public static string Decrypt(string hexvalue, System.Text.Encoding encoding)
        {
            int CharsLength = hexvalue.Length;
            byte[] bytesarray = new byte[CharsLength / 2];
            for (int i = 0; i < CharsLength; i += 2)
            {
                bytesarray[i / 2] = Convert.ToByte(hexvalue.Substring(i, 2), 16);
            }
            return encoding.GetString(bytesarray);
        }

        //static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
        //public static string Encrypt(string originalString)
        //{
        //    if (String.IsNullOrEmpty(originalString))
        //    {
        //        throw new ArgumentNullException
        //               ("The string which needs to be encrypted can not be null.");
        //    }
        //    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        //    MemoryStream memoryStream = new MemoryStream();
        //    CryptoStream cryptoStream = new CryptoStream(memoryStream,
        //        cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
        //    StreamWriter writer = new StreamWriter(cryptoStream);
        //    writer.Write(originalString);
        //    writer.Flush();
        //    cryptoStream.FlushFinalBlock();
        //    writer.Flush();
        //    return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        //}

        //public static string Decrypt(string cryptedString)
        //{
        //    if (String.IsNullOrEmpty(cryptedString))
        //    {
        //        throw new ArgumentNullException
        //           ("The string which needs to be decrypted can not be null.");
        //    }
        //    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        //    MemoryStream memoryStream = new MemoryStream
        //            (Convert.FromBase64String(cryptedString));
        //    CryptoStream cryptoStream = new CryptoStream(memoryStream,
        //        cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
        //    StreamReader reader = new StreamReader(cryptoStream);
        //    return reader.ReadToEnd();
        //}
    }
}