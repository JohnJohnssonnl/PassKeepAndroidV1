namespace PassStorageService.CORE
{
    public class ReadPassService
    {
        public static string ReadPass(string _websiteString, string _decryptionKey, string _fileDir, int _numOfIterations)
        {
            string ret = "";
            string uniqueId = CandyStore.PBKDF2Service(_websiteString, _numOfIterations);
            uniqueId        = CandyStore.ToHexString(uniqueId);


            try
            {
                PassObject PassObject = StorageHelper.ReadBlobToObject(uniqueId, _fileDir);

                ret = EncryptDecryptStore.Decrypt(PassObject.GetEncryptedPass(), _decryptionKey);
            }
            catch
            {
                ret = "Cannot read, does this website exist as pass?";
            }

            return ret;
        }
    }
}
