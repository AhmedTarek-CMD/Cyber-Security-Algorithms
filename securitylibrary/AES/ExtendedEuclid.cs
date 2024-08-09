using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        int gcd(int a, int b)
        {
            if (b == 0) return a;
            return gcd(b, a % b);
        }


        // returns [gcd, x, y]
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

        int raise_x_over_bar(int x, int y, int x_unit, int y_unit, int bar, bool or_equal)
        {
            if (x > bar || (x == bar && or_equal))
            {
                return x;
            }

            int add = (or_equal ? 1 : 0);

            int shift = (bar - x + x_unit - add) / x_unit;
            x += shift * x_unit;
            y -= shift * y_unit;
            return x;
        }

        public int GetMultiplicativeInverse(int number, int baseN)
        {
            if (gcd(number, baseN) != 1)
            {
                return -1;
            }

            List<int> ret = eGCD(number, baseN);

            int x_unit = baseN, y_unit = number;

            ret[1] = raise_x_over_bar(ret[1], ret[2], x_unit, y_unit, 0, true);

            return ret[1];
        }
    }
}
