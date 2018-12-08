using PassStorageService.CORE;
using System;

namespace PassKeepAndroid.CORE
{
    class UIHandlingService
    {
        public static string CreatePass(string _encryptionKeyPlain, 
                                        string _fileDir, 
                                        string _websiteURL, 
                                        string _websitePass, 
                                        string _numOfIterations)
        {
            int NumOfIterations = _numOfIterations == ""? 0:Convert.ToInt32(_numOfIterations);  //avoid exceptions when empty

            if (NumOfIterations < CandyStore.GetParameters().MinNumOfIterationsParm)
            {
                return "Safety: Number of iterations must exceed" + Convert.ToString(CandyStore.GetParameters().MinNumOfIterationsParm);
            }

            if (_encryptionKeyPlain == "")
            {
                return "Please fill in the encryption key first";
            }
            if (_websiteURL == "")
            {
                return "Please fill in the website URL";
            }

            if (_websitePass == "")
            {
                return "Please fill in the website pass";
            }

            string EncryptionKey = CandyStore.PBKDF2Service(_encryptionKeyPlain, NumOfIterations);

            try
            {
            if (CreatePassService.CreatePass(_websiteURL, _websitePass, EncryptionKey, _fileDir, NumOfIterations))
                {
                    return "Pass encryption completed for website:" + _websiteURL;
                }
                else
                {
                    return "Pass encryption failed (did you allow storage permissions for this app?)";
                }
            }
            catch
            {
                return "Could not create key";
            }

        }
        public static string ReadPass(  string _decryptionKeyPlain, 
                                        string _fileDir, 
                                        string _websiteURL, 
                                        string _numOfIterations)
        {
            string ret;
            int NumOfIterations = _numOfIterations == "" ? 0 : Convert.ToInt32(_numOfIterations);   //avoid exceptions when empty

            if (_decryptionKeyPlain == "")
            {
                return "Please fill in the encryption key first";
            }
            if (NumOfIterations < CandyStore.GetParameters().MinNumOfIterationsParm)
            {
                return "Safety: Number of iterations must exceed" + Convert.ToString(CandyStore.GetParameters().MinNumOfIterationsParm);
            }

            string DecryptionKey = CandyStore.PBKDF2Service(_decryptionKeyPlain, NumOfIterations);

            try
            {
                ret = ReadPassService.ReadPass(_websiteURL, DecryptionKey, _fileDir, NumOfIterations);
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
                string uniqueId = CandyStore.PBKDF2Service(_websiteURL, CandyStore.GetParameters().MinNumOfIterationsParm);
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