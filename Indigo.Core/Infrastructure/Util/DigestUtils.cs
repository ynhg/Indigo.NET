using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Indigo.Infrastructure.Util
{
    public static class DigestUtils
    {
        public static byte[] Md5(Stream inputStream)
        {
            return GetMd5Provider().ComputeHash(inputStream);
        }

        public static byte[] Md5(byte[] data)
        {
            return GetMd5Provider().ComputeHash(data);
        }

        public static byte[] Md5(string data)
        {
            return GetMd5Provider().ComputeHash(GetBytes(data));
        }

        public static string Md5Hex(Stream inputStream)
        {
            return GetHexString(Md5(inputStream));
        }

        public static string Md5Hex(byte[] data)
        {
            return GetHexString(Md5(data));
        }

        public static string Md5Hex(string data)
        {
            return GetHexString(Md5(data));
        }

        public static byte[] Sha1(Stream inputStream)
        {
            return GetSha1Provider().ComputeHash(inputStream);
        }

        public static byte[] Sha1(byte[] data)
        {
            return GetSha1Provider().ComputeHash(data);
        }

        public static byte[] Sha1(string data)
        {
            return GetSha1Provider().ComputeHash(GetBytes(data));
        }

        public static string Sha1Hex(Stream inputStream)
        {
            return GetHexString(Sha1(inputStream));
        }

        public static string Sha1Hex(byte[] data)
        {
            return GetHexString(Sha1(data));
        }

        public static string Sha1Hex(string data)
        {
            return GetHexString(Sha1(data));
        }

        public static byte[] Sha256(Stream inputStream)
        {
            return GetSha256Provider().ComputeHash(inputStream);
        }

        public static byte[] Sha256(byte[] data)
        {
            return GetSha256Provider().ComputeHash(data);
        }

        public static byte[] Sha256(string data)
        {
            return GetSha256Provider().ComputeHash(GetBytes(data));
        }

        public static string Sha256Hex(Stream inputStream)
        {
            return GetHexString(Sha256(inputStream));
        }

        public static string Sha256Hex(byte[] data)
        {
            return GetHexString(Sha256(data));
        }

        public static string Sha256Hex(string data)
        {
            return GetHexString(Sha256(data));
        }

        public static byte[] Sha384(Stream inputStream)
        {
            return GetSha384Provider().ComputeHash(inputStream);
        }

        public static byte[] Sha384(byte[] data)
        {
            return GetSha384Provider().ComputeHash(data);
        }

        public static byte[] Sha384(string data)
        {
            return GetSha384Provider().ComputeHash(GetBytes(data));
        }

        public static string Sha384Hex(Stream inputStream)
        {
            return GetHexString(Sha384(inputStream));
        }

        public static string Sha384Hex(byte[] data)
        {
            return GetHexString(Sha384(data));
        }

        public static string Sha384Hex(string data)
        {
            return GetHexString(Sha384(data));
        }

        public static byte[] Sha512(Stream inputStream)
        {
            return GetSha512Provider().ComputeHash(inputStream);
        }

        public static byte[] Sha512(byte[] data)
        {
            return GetSha512Provider().ComputeHash(data);
        }

        public static byte[] Sha512(string data)
        {
            return GetSha512Provider().ComputeHash(GetBytes(data));
        }

        public static string Sha512Hex(Stream inputStream)
        {
            return GetHexString(Sha512(inputStream));
        }

        public static string Sha512Hex(byte[] data)
        {
            return GetHexString(Sha512(data));
        }

        public static string Sha512Hex(string data)
        {
            return GetHexString(Sha512(data));
        }

        private static HashAlgorithm GetMd5Provider()
        {
            return new MD5CryptoServiceProvider();
        }

        private static HashAlgorithm GetSha1Provider()
        {
            return new SHA1CryptoServiceProvider();
        }

        private static HashAlgorithm GetSha256Provider()
        {
            return new SHA256CryptoServiceProvider();
        }

        private static HashAlgorithm GetSha384Provider()
        {
            return new SHA384CryptoServiceProvider();
        }

        private static HashAlgorithm GetSha512Provider()
        {
            return new SHA512CryptoServiceProvider();
        }

        private static byte[] GetBytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        private static string GetHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (int b in data)
            {
                sb.AppendFormat("{0:x2}", b);
            }

            return sb.ToString();
        }
    }
}
