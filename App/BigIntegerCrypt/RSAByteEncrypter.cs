using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigIntegerCrypt
{
    public class RSAByteEncrypter
    {
        #region Constant
        private static readonly int Prime = 257;
        #endregion

        #region For Singleton
        private static RSAByteEncrypter encrypter;
        public static RSAByteEncrypter Encrypter
        {
            get
            {
                if (encrypter == null)
                    encrypter = new RSAByteEncrypter();
                return encrypter;
            }
        }
        #endregion

        #region Constructor
        public RSAByteEncrypter() { }
        #endregion

        #region Generate Keys
        public RSAKeyPair[] GenerateKeyPairs(int n)
        {
            return RSAEncrypter.Instance.GenerateKeyPairs(n, Prime);
        }
        #endregion

        #region Encrypt / Decrypt
        private byte ConvertByte(ref RSAKeyPair keyPair, byte b)
        {
            return Convert.ToByte((int)(RSAEncrypter.Instance.Encrypt(keyPair, b) % Prime));
        }
        #endregion

        #region CreateMap
        public IDictionary<byte, byte> CreateByteMap(RSAKeyPair keyPair)
        {
            Dictionary<byte, byte> map = new Dictionary<byte, byte>();
            for (int i = 0; i < 256; i++)
            {
                byte idx = Convert.ToByte(i);
                byte b = ConvertByte(ref keyPair, idx);
                map.Add(idx, b);
                if (i % 16 == 0)
                {
                    Task.Delay(1);
                }
            }
            return map;
        }
        #endregion
    }
}
