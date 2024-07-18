using System;
using System.Text;

namespace HDB.DataNew
{
    public class RC4
    {
        private byte[] SOriginal = new byte[256];
        private byte[] S = new byte[256];
        private int i;
        private int j;

        public RC4(Guid key)
        {
            Initialize(key);
        }

        private void Initialize(Guid key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.ToString());

            for (int ti = 0; ti < 256; ++ti)
            {
                SOriginal[ti] = (byte)ti;
            }

            int tj = 0;
            int keyLen = keyBytes.Length;
            for (int ti = 0; ti < 256; ++ti)
            {
                int t = SOriginal[ti];
                tj = (tj + t + keyBytes[ti % keyLen]) & 255;
                SOriginal[ti] = SOriginal[tj];
                SOriginal[tj] = (byte)t;
            }
        }

        public void Crypt(byte[] bytes)
        {
            i = 0;
            j = 0;
            Array.Clear(S, 0, S.Length);
            Array.Copy(SOriginal, S, SOriginal.Length);
            int ti = 0;
            int len = bytes.Length;
            while (ti < len)
            {
                bytes[ti++] ^= Next();
            }
        }

        private byte Next()
        {
            i = (i + 1) & 255;
            j = (j + S[i]) & 255;
            int t = S[i];
            S[i] = S[j];
            S[j] = (byte)t;
            return S[(t + S[i]) & 255];
        }
    }
}
