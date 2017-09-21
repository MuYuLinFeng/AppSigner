using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace BigIntegerCrypt
{
    public class BigEncoder
    {
        public Hashtable createKey(FileInfo file)
        {
            Random r = new Random();
            int seed = r.Next(350000) + 258;
            Queue<byte> buffer = new Queue<byte>();
            RSAByteEncrypter encypter = new RSAByteEncrypter();
            string decryptKeyFilePath = $"{file.FullName}.public.key";
            string encryptKeyFilePath = $"{file.FullName}.private.key";

            var keys = encypter.GenerateKeyPairs(seed);
            var encryptKey = keys[0];
            var decryptKey = keys[1];
            System.Diagnostics.Debug.WriteLine("======encryptKey====="+ keys[0].value);
            System.Diagnostics.Debug.WriteLine("======decryptKey====="+ keys[1].value);

            using (var fs = File.OpenWrite(decryptKeyFilePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, decryptKey);
            }

            using (var fs = File.OpenWrite(encryptKeyFilePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, encryptKey);
            }
            Hashtable ht = new Hashtable();
            ht.Add("public", decryptKeyFilePath);
            ht.Add("private", encryptKeyFilePath);
            return ht;
        }
        public string Encrypt(FileInfo encryptFile, FileInfo keyFile)
        {
            string encryptedFilePath = $"{encryptFile.FullName}.encrypted";
            SelectedFileLength = encryptFile.Length;
            RSAByteEncrypter encypter = new RSAByteEncrypter();
            RSAKeyPair? encryptKey = null;
            using (var fs = keyFile.OpenRead())
            {
                BinaryFormatter bf = new BinaryFormatter();
                encryptKey = bf.Deserialize(fs) as RSAKeyPair?;
            }

            if (encryptKey != null)
            {
                Queue<byte> buffer = new Queue<byte>();
                RSAKeyPair keyPair = encryptKey.Value;
                var map = encypter.CreateByteMap(keyPair);

                using (var inStream = encryptFile.OpenRead())
                {
                    using (var reader = new BinaryReader(inStream))
                    {
                        using (var outStream = File.OpenWrite(encryptedFilePath))
                        {
                            using (var writer = new BinaryWriter(outStream))
                            {
                                for (long i = 0; i < SelectedFileLength; i++)
                                {
                                    byte b = reader.ReadByte();
                                    buffer.Enqueue(map[b]);
                                    while (buffer.Count > 0)
                                        writer.Write(buffer.Dequeue());
                                    buffer.Clear();
                                }
                                while (buffer.Count > 0)
                                    writer.Write(buffer.Dequeue());
                            }
                        }
                    }
                    return encryptedFilePath;
                }
            }
            return null;
        }

        public static long GetUpdateTick(long length)
        {
            if (length < 1024) // 1KB 
                return 16;  // 16 bytes
            else if (length < 1048576) // 1MB 
                return 4096;    // 1 KB
            else if (length < 1048576 * 1024)   // 1GB 
                return 1048576 / 8;    // 128 KB
            else
                return 1048576 / 4;   // 256 KB
        }

        private void SetFolderPath(string path)
        {
            SelectedFolderPath = path;
            Files = Array.ConvertAll(Directory.GetFiles(SelectedFolderPath), (filePath) => new FileInfo(filePath));
        }

        public IEnumerable<FileInfo> Files { get; set; }
        public string SelectedFolderPath { get; set; }
        private EncryptionState State { get; set; }
        private long SelectedFileLength { get; set; }
    }
}
