using System;

namespace PassStorageService.CORE
{
    public class CreatePassService
    {
        public static Boolean CreatePass(string _websiteString, string _websitePass, string _encryptionKey, string _fileDir)
        {

            string uniqueId = CandyStore.PBKDF2Service(_websiteString, 10000);
            uniqueId = CandyStore.ToHexString(uniqueId);
            string encryptedPass = EncryptDecryptStore.Encrypt(_websitePass, _encryptionKey);

            PassObject passObject = new PassObject();   //Create new instance of PassObject

            try
            {
                passObject.SetUniqueId(uniqueId);
                passObject.SetEncryptedPass(encryptedPass);

                StorageHelper.WriteObjectToBlob(passObject, uniqueId, _fileDir);  //Store object

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
