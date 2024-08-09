using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary.DiffieHellman
{


    public class DiffieHellman
    {
        public  int power(int f, int s, int sf)
        {
            int result = 1;

            while (s > 0)
            {
                if (s % 2 == 1)
                {
                    result = (result * f) % sf;
                }

                f = (f * f) % sf;
                s /= 2;
            }

            return result;
        }

        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            List<int> keyList = new List<int>();
            int publicKey1, publicKey2;
            int privateKey1, privateKey2;

            publicKey1 = power(alpha, xa, q);
            publicKey2 = power(alpha, xb, q);
            privateKey1 = power(publicKey2, xa, q);
            privateKey2 = power(publicKey1, xb, q);

            keyList.Add(privateKey1);
            keyList.Add(privateKey2);

            return keyList;
        }
    }
}