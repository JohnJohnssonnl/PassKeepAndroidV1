using System;
using System.Text;

namespace PassStorageService.CORE
{
    public class StringToEncryptedED25519
    {
        public static string CreateEncryption(string _input)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(_input);

            byte[] publicKey = Ed25519.PublicKey(bytes);

            return StringToEncryptedED25519.ConvertToPubAddressChunked(publicKey);
        }

        public static string ConvertToPubAddressChunked(byte[] array)
        {
            int arrayLength = array.Length;
            string ret = "";
            int numOfCopyInt;
            byte[] subArray = new byte[8];  //Max 8 bytes

            for (int I = 0; I < arrayLength; I += 8)
            {
                numOfCopyInt = arrayLength - I > 7 ? 8 : arrayLength - I;
                subArray = new byte[numOfCopyInt];
                Array.Copy(array, I, subArray, 0, numOfCopyInt);
                ret += StringToEncryptedED25519.ConvertToPubAddress(subArray);
            }

            return ret;
        }

        public static string ConvertToPubAddress(byte[] array)
        {
            const string ALPHABET = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
            string retString = string.Empty;
            System.Numerics.BigInteger encodeSize = ALPHABET.Length;
            System.Numerics.BigInteger arrayToInt = 0;
            for (int i = 0; i < array.Length; ++i)
            {
                arrayToInt = arrayToInt * 256 + array[i];
            }
            while (arrayToInt > 0)
            {
                int rem = (int)(arrayToInt % encodeSize);
                arrayToInt /= encodeSize;
                retString = ALPHABET[rem] + retString;
            }
            for (int i = 0; i < array.Length && array[i] == 0; ++i)
                retString = ALPHABET[0] + retString;

            return retString;
        }
        public static byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }
        public static int GetHexVal(char hex)
        {
            int val = (int)hex;

            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }
}
