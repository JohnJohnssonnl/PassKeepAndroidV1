using PassStorageService.CORE;
using Android;
using PassKeepAndroid;

namespace PassKeepAndroid.CORE
{
    class UIHandlingService
    {
        public static string CreatePass(string _encryptionKeyPlain, string _fileDir, string _websiteURL, string _websitePass)
        {
            if (_encryptionKeyPlain == "")
            {
                return "Please fill in the encryption key first";
            }

            string EncryptionKey = CandyStore.PBKDF2Service(_encryptionKeyPlain, 10000);

            try
            {
            if (CreatePassService.CreatePass(_websiteURL, _websitePass, EncryptionKey, _fileDir))
                {
                    return "Pass encryption completed";
                }
                else
                {
                    return "Pass encryption failed probably while writing its files";
                }
            }
            catch
            {
                return "Could not create key";
            }

        }
        public static string ReadPass(string _decryptionKeyPlain, string _fileDir, string _websiteURL)
        {
            string ret;

            if (_decryptionKeyPlain == "")
            {
                return "Please fill in the encryption key first";
            }

            string DecryptionKey = CandyStore.PBKDF2Service(_decryptionKeyPlain, 10000);

            try
            {
                ret = ReadPassService.ReadPass(_websiteURL, DecryptionKey, _fileDir);
            }
            catch
            {
                return "Could not read key";
            }

            return "Your pass: " + ret;
        }

        public static string WipeAllPasses(string _fileDir)
        {
            string ret;

            try
            {
                ret = StorageHelper.WipeDir(_fileDir);
            }
            catch
            {
                return "Wipe failed";
            }

            return ret;
        }
        public static string RemoveStorageSpecific(string _fileDir, string _websiteURL)
        {
            string ret;

            try
            {
                string uniqueId = CandyStore.PBKDF2Service(_websiteURL, 10000);
                uniqueId        = CandyStore.ToHexString(uniqueId);

                ret = StorageHelper.RemoveStorageSpecific(_fileDir, uniqueId);
            }
            catch
            {
                return "Removal failed";
            }

            return ret;
        }
    }
}