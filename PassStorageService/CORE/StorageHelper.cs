using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PassStorageService.CORE
{
    public class StorageHelper
    {
        public static string RemoveStorageSpecific(string _fileDir, string _websiteUniqueID)
        {
            //Write to wallet file
            string filePath = _fileDir + "/PASSKEEP/STORE/" + _websiteUniqueID + ".bin";

            if (!File.Exists(filePath))
            {
                throw new Exception("File does not exist");
            }

            //Delete storage folder
            try
            {
                File.Delete(filePath);
                
                return "Removal completed";
            }
            catch
            {
                return "Removal failed";
            }
        }

        public static string WipeDir(string _fileDir)
        {
            //Write to wallet file
            String FileFolder = _fileDir + "/PASSKEEP/STORE/";
            

            if (!Directory.Exists(FileFolder))
            {
                //Create directory if it does not exist
                return "Folder does not exist, no wipe could be performed";
            }

            //Delete storage folder
            try
            {
                var list = Directory.GetFiles(FileFolder, "*");

                if (list.Length > 0)
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        File.Delete(list[i]);
                    }
                }

                return "Wipe completed";
            }
            catch
            {
                return "Wipe failed";
            }
        }

        public static void WriteObjectToBlob(PassObject _objectToBlob, string _saveAsUniqueId,string _fileDir)
        {
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, _objectToBlob);
            byte[] BlobData = memorystream.ToArray();

            WriteBin(BlobData, _saveAsUniqueId, _fileDir);
        }

        public static PassObject ReadBlobToObject(string _readAsUniqueId, string _fileDir)
        {
            //Read from bin
            string filePath = _fileDir + "/PASSKEEP/STORE/" + _readAsUniqueId + ".bin";

            if (!File.Exists(filePath))
            {
                throw new Exception("Filepath does not exist");
            }

            byte[] BlobData = ReadBin(filePath);

            MemoryStream memorystreamd = new MemoryStream(BlobData);
            BinaryFormatter bfd = new BinaryFormatter();
            PassObject deserializedobject = bfd.Deserialize(memorystreamd) as PassObject;

            memorystreamd.Close();

            return deserializedobject;
        }

        public static void WriteBin(byte[] _storage, string _saveAsUniqueId, string _fileDir)
        {
            //Write to wallet file
            String FileFolder = _fileDir + "/PASSKEEP/STORE/";
            String FilePath = FileFolder + _saveAsUniqueId + ".bin";

            if (!Directory.Exists(FileFolder))
            {
                //Create directory if it does not exist
                Directory.CreateDirectory(FileFolder);
            }

            BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create));

            writer.Write(_storage);  //Write binaries
            writer.Close();
        }
        public static byte[] ReadBin(String _filePath)
        {
            StreamReader sr = new StreamReader(_filePath);
            var bytes = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                sr.BaseStream.CopyTo(memstream);
                bytes = memstream.ToArray();
            }

            return bytes;
        }
    }
}
