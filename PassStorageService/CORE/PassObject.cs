using System;

namespace PassStorageService.CORE
{
    [Serializable]
    public class PassObject
    {
        string uniqueId;
        string encryptedPass;

        public void SetUniqueId(string _uniqueId)
        {
            uniqueId = _uniqueId;
        }
        public void SetEncryptedPass(string _encryptedPass)
        {
            encryptedPass = _encryptedPass;
        }
        public string GetEncryptedPass()
        {
            return encryptedPass;
        }
    }
}
