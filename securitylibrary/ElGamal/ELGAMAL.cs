using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        /// 

        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            List<long> encryptList = new List<long>();
            long a, b, c, d, e = 1, f = 1;

            a = alpha % q;
            b = m % q;
            c = y % q;
            d = k % (q - 1);

            while (d > 0)
            {
                if (d % 2 == 1)
                {
                    e = (e * a) % q;
                    f = (f * c) % q;
                }
                a = (a * a) % q;
                c = (c * c) % q;
                d /= 2;
            }

            f = (f * b) % q;

            encryptList.Add(e);
            encryptList.Add(f);

            return encryptList;
        }
     
        public int Decrypt(int c1, int c2, int x, int q)
        {
            int a, b, e, f, g1, g2, h = 0, j = 1, k = 1, plainText;
            a = c1 % q;
            b = x % (q - 1);
            g1 = g2 = q;

            while (b > 0)
            {
                if (b % 2 == 1)
                {
                    k = (k * a) % q;
                }
                a = (a * a) % q;
                b /= 2;
            }

            int l;

            l = k % g1;

            for (int i = 0; l > 1; i++)
            {
                f = l / g2;
                e = g2;
                g2 = l % g2;
                l = e;
                e = h;
                h = j - f * h;
                j = e;
            }

            plainText = (c2 * j) % q;

            return plainText;
        }
    }
}
