using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            int n = p * q;
            int cipher = Power(M, e, n);
            return cipher;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            int n = p * q;
            int eculer = (p - 1) * (q - 1);
            long d = GetMultiplicativeInverse(e, eculer);
            return Power(C, d, n);
        }

        public int Power(int b, long e, int m)
        {
            int result = 1;
            b %= m;
            while (e > 0)
            {
                if (e % 2 == 1)
                {
                    result = divide_the_number(result, b, m);
                }
                e /= 2;
                b = divide_the_number(b, b, m);
            }
            return result;
        }

        public int divide_the_number(int a, int b, int m)
        {
            long x = (long)a * b;
            long result = x % m;
            return (int)result;
        }

        int gcd(int a, int b)
        {
            if (b == 0) return a;
            return gcd(b, a % b);
        }

        List<int> eGCD(int r0, int r1)
        {
            int x0, y0;
            List<int> ret = new List<int>();

            int q, x1, y1;
            x0 = y1 = 1;
            x1 = y0 = 0;

            while (r1 != 0)
            {
                q = r0 / r1;

                int next_r = r0 - q * r1;

                r0 = r1;
                r1 = next_r;

                int next_x = x0 - q * x1;

                x0 = x1;
                x1 = next_x;

                int next_y = y0 - q * y1;

                y0 = y1;
                y1 = next_y;
            }

            ret.Add(r0);
            ret.Add(x0);
            ret.Add(y0);

            return ret;
        }

        int raise_x_over_bar(int x, int x_unit, int bar, bool or_equal)
        {
            if (x > bar || (x == bar && or_equal))
            {
                return x;
            }

            int add = (or_equal ? 1 : 0);
            int shift = (bar - x + x_unit - add) / x_unit;

            x += shift * x_unit;

            return x;
        }

        public int GetMultiplicativeInverse(int number, int baseN)
        {
            if (gcd(number, baseN) != 1)
            {
                return -1;
            }

            List<int> ret = eGCD(number, baseN);
            int x_unit = baseN;

            ret[1] = raise_x_over_bar(ret[1], x_unit, 0, true);

            return ret[1];
        }
    }
}
