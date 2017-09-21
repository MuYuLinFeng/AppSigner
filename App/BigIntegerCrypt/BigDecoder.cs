using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BigIntegerCrypt
{
    public class BigDecoder
    {
        private string saveFilePath;
        public string Decrypt(string KeyFilePath, string EncryptedFilePath)
        {
            FileInfo file = new FileInfo(EncryptedFilePath);
            FileInfo key = new FileInfo(KeyFilePath);
            saveFilePath = file.FullName.Replace("encrypted", "decrypted");
            SetRegularExpressions();
            EncryptedFileLength = file.Length;
            
            RSAKeyPair? decryptKey = null;

            using (var fs = key.OpenRead())
            {
                BinaryFormatter bf = new BinaryFormatter();
                decryptKey = bf.Deserialize(fs) as RSAKeyPair?;
            }

            string originalFileName;
            if (!ExtractFileName(file.Name, out originalFileName))
            {
                return "";
            }
            string originalExtension = ExtractExtension(originalFileName);

            if (decryptKey != null)
            {
                Queue<byte> buffer = new Queue<byte>();
                RSAKeyPair keyPair = decryptKey.Value;
                RSAByteEncrypter encypter = new RSAByteEncrypter();
                var map = encypter.CreateByteMap(keyPair);

                using (var inStream = file.OpenRead())
                {
                    using (var reader = new BinaryReader(inStream))
                    {
                        using (var outStream = File.OpenWrite(saveFilePath))
                        {
                            using (var writer = new BinaryWriter(outStream))
                            {
                                for (long i = 0; i < EncryptedFileLength; i++)
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
                }
                return saveFilePath;
            }
            return null;
        }

        private long EncryptedFileLength;

        private Regex encryptExtensionRegex;
        private Regex fileExtensionRegex;
        private void SetRegularExpressions()
        {
            encryptExtensionRegex = new Regex(@"(.*)\.encrypted");
            fileExtensionRegex = new Regex(@".*\.([^.]*)");
        }

        private bool ExtractFileName(string encryptedFileName, out string originalFileName)
        {
            if (encryptExtensionRegex.IsMatch(encryptedFileName))
            {
                var catched = encryptExtensionRegex.Match(encryptedFileName);
                originalFileName = catched?.Groups?[1]?.Value;
                return true;
            }
            else
            {
                originalFileName = string.Empty;
                return false;
            }
        }

        private string ExtractExtension(string fileName)
        {
            return fileExtensionRegex.Match(fileName).Groups?[1]?.Value;
        }
    }
}
