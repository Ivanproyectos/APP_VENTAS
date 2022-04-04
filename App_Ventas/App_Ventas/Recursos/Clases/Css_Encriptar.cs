using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;

namespace App_Ventas.Recursos.Clases
{
    public class Css_Encriptar
    {
        
        public static string Encriptar(string dato)
        {
            MD5 md5 = MD5.Create();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(dato);
            byte[] result = md5.ComputeHash(data);
            dato = BitConverter.ToString(result).Replace("-", ""); 

            return dato;
        }

        static string keyx = "A!9HHhi%XjjYY4YP2@Nob009X";
        private static string EncryptionKey = "$d#THDRARC";
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        public static string Encrypt(string texto)
        {
            texto = (DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year) + texto;
                          
            string returnstring = "";
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(texto);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                returnstring = Convert.ToBase64String(ms.ToArray());


                return returnstring;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string Decrypt(string stringToDecrypt)
        {

            Byte[] inputByteArray = new Byte[stringToDecrypt.Length];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;


                return encoding.GetString(ms.ToArray()).Replace((DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year).ToString(),"");
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
         
    }
}
