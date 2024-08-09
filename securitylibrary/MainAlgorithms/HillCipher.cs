using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{

    public class HillCipher : ICryptographicTechnique<string, string>, ICryptographicTechnique<List<int>, List<int>>
    {

        static int sqrt(int n)
        {
            int cur = 1, ans = 1;
            while (cur * cur <= n)
            {
                ans = cur;
                cur++;
            }
            return ans;
        }

        static int power(int b, int p)
        {
            int ret = 1;
            while (p > 0)
            {
                ret *= b;
                p--;
            }
            return ret;
        }
        static bool list_eq(List<int> a, List<int> b)
        {
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (b[i] != a[i]) return false;
            }
            return true;
        }
        static List<int> get_key(List<int> p, List<int> c)
        {
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    for (int k = 0; k < 26; k++)
                    {
                        for (int l = 0; l < 26; l++)
                        {
                            List<int> key = new List<int>();
                            key.Add(i);
                            key.Add(j);
                            key.Add(k);
                            key.Add(l);

                            HillCipher obj = new HillCipher();
                            List<int> cipher = obj.Encrypt(p, key);
                            if (list_eq(cipher, c))
                            {
                                return key;
                            }
                        }
                    }
                }
            }
            throw new InvalidAnlysisException();
        }
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            List<int> ret = get_key(plainText, cipherText);
            return ret;
        }
        public string Analyse(string plainText, string cipherText)
        {
            List<int> plain_text = new List<int>();
            for (int i = 0; i < plainText.Count(); i++) plain_text.Add(plainText[i]);
            List<int> cipher_text = new List<int>();
            for (int i = 0; i < cipherText.Count(); i++) cipher_text.Add(cipherText[i]);
            List<int> key = get_key(plain_text, cipher_text);
            string ret = "";
            for (int i = 0; i < key.Count; i++) ret += (char)('a' + key[i]);
            return ret;
        }

        static int[,] mat_inv(int[,] m)
        {
            int n = m.GetLength(0);
            int d = get_det(m, n);
            int[,] ret = new int[n, n];
            int di = mod_inv(d, 10);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int s = 1;
                    if ((i + j) % 2 == 1)
                    {
                        s = -1;
                    }
                    int min = get_min(m, i, j);
                    ret[j, i] = s * min * di % 10;
                }
            }

            return ret;
        }

        static int get_det(int[,] m, int n)
        {
            if (n == 1)
            {
                return m[0, 0];
            }

            int d = 0;
            int s = 1;

            for (int i = 0; i < n; i++)
            {
                int[,] part = new int[n - 1, n - 1];

                for (int j = 1; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (k < i)
                        {
                            part[j - 1, k] = m[j, k];
                        }
                        else if (k > i)
                        {
                            part[j - 1, k - 1] = m[j, k];
                        }
                    }
                }

                d += s * m[0, i] * get_det(part, n - 1);
                s *= -1;
            }

            return d;
        }

        static int get_min(int[,] m, int r, int c)
        {
            int n = m.GetLength(0);
            int[,] ret = new int[n - 1, n - 1];
            int p = 0;
            int q = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != r && j != c)
                    {
                        ret[p, q] = m[i, j];
                        q++;

                        if (q == n - 1)
                        {
                            q = 0;
                            p++;
                        }
                    }
                }
            }

            return get_det(ret, n - 1);
        }

        static int mod_inv(int x, int mod)
        {
            x %= mod;

            for (int i = 1; i < mod; i++)
            {
                if (x * i % mod == 1)
                {
                    return i;
                }
            }
            return 1;
        }

        int gcd(int a, int b)
        {
            if (a < 0) a = -a;
            if (b < 0) b = -b;
            if (b == 0)
            {
                return a;
            }
            return gcd(b, a % b);
        }

        int get_b(int[,] mat, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (mat[i, j] < 0 || mat[i, j] >= 26)
                    {
                        return 0;
                    }
                }
            }

            int d = get_det(mat, n);

            if (d == 0)
            {
                return 0;
            }

            if (gcd(d, 26) != 1)
            {
                return 0;
            }

            for (int i = 1; i < 26; i++)
            {
                int mul = i * d;
                while (mul < 0) mul += 26;
                mul %= 26;
                if (mul == 1)
                {
                    return i;
                }
            }

            return 0;
        }

        static int[,] transpose(int[,] m)
        {
            int r = m.GetLength(0);
            int c = m.GetLength(1);

            int[,] ret = new int[c, r];

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    ret[j, i] = m[i, j];
                }
            }

            return ret;
        }

        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            List<int> ret = new List<int>();
            int sq = sqrt(key.Count);
            int[,] key_mat = list_to_matrix(key, sq, sq);

            int b = get_b(key_mat, sq, sq);
            if (b == 0)
            {
                throw new InvalidAnlysisException();
            }

            int[,] inv_key_mat = mat_inv(key_mat);
            int d = get_det(key_mat, sq);
            if (sq == 2)
            {
                for (int i = 0; i < sq; i++)
                {
                    for (int j = 0; j < sq; j++)
                    {
                        inv_key_mat[i, j] = 1 / d * inv_key_mat[i, j];
                    }
                }
            }
            else if (sq == 3)
            {
                for (int i = 0; i < sq; i++)
                {
                    for (int j = 0; j < sq; j++)
                    {
                        inv_key_mat[i, j] = b * power(-1, i + j) * get_min(key_mat, i, j);
                        while (inv_key_mat[i, j] < 0)
                        {
                            inv_key_mat[i, j] += 26;
                        }
                        inv_key_mat[i, j] %= 26;
                    }
                }


                inv_key_mat = transpose(inv_key_mat);
            }


            int cur = 0;
            while (cur < cipherText.Count)
            {
                int[,] cur_mat = new int[sq, 1];
                for (int i = 0; i < sq; i++)
                {
                    if (cur < cipherText.Count)
                    {
                        cur_mat[i, 0] = cipherText[cur];
                    }
                    else
                    {
                        cur_mat[i, 0] = 0;
                    }
                    cur++;
                }

                int[,] res = mat_mul(inv_key_mat, cur_mat);
                for (int i = 0; i < sq; i++)
                {
                    ret.Add(res[i, 0]);
                }
            }

           

            return ret;
        }
        public string Decrypt(string cipherText, string key)
        {
            List<int> cipher_text_list = new List<int>();
            for (int i = 0; i < cipherText.Length; i++)
            {
                cipher_text_list.Add(i - 'a');
            }
            List<int> key_list = new List<int>();
            for (int i = 0; i < key.Length; i++)
            {
                key_list.Add(key[i] - 'a');
            }
            List<int> ret = new List<int>();
            int sq = sqrt(key_list.Count);
            int[,] key_mat = list_to_matrix(key_list, sq, sq);

           

            int b = get_b(key_mat, sq, sq);
            if (b == 0)
            {
                throw new InvalidAnlysisException();
            }

            int[,] inv_key_mat = mat_inv(key_mat);
            int d = get_det(key_mat, sq);
            if (sq == 2)
            {
                for (int i = 0; i < sq; i++)
                {
                    for (int j = 0; j < sq; j++)
                    {
                        inv_key_mat[i, j] = 1 / d * inv_key_mat[i, j];
                    }
                }
            }
            else if (sq == 3)
            {
                for (int i = 0; i < sq; i++)
                {
                    for (int j = 0; j < sq; j++)
                    {
                        inv_key_mat[i, j] = b * power(-1, i + j) * get_min(key_mat, i, j);
                        while (inv_key_mat[i, j] < 0)
                        {
                            inv_key_mat[i, j] += 26;
                        }
                        inv_key_mat[i, j] %= 26;
                    }
                }


                inv_key_mat = transpose(inv_key_mat);
            }

            

            int cur = 0;
            while (cur < cipher_text_list.Count)
            {
                int[,] cur_mat = new int[sq, 1];
                for (int i = 0; i < sq; i++)
                {
                    if (cur < cipher_text_list.Count)
                    {
                        cur_mat[i, 0] = cipher_text_list[cur];
                    }
                    else
                    {
                        cur_mat[i, 0] = 0;
                    }
                    cur++;
                }

                int[,] res = mat_mul(inv_key_mat, cur_mat);
                for (int i = 0; i < sq; i++)
                {
                    ret.Add(res[i, 0]);
                }
            }

            

            String r = "";
            for (int i = 0; i < ret.Count; i++)
            {
                r += (char)(ret[i] + 'a');
            }
            return r;
        }

        static int[,] mat_mul(int[,] x, int[,] y)
        {
            int rows1 = x.GetLength(0);
            int cols1 = x.GetLength(1);
            int cols2 = y.GetLength(1);

            int[,] ret = new int[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    for (int k = 0; k < cols1; k++)
                    {
                        int a = x[i, k], b = y[k, j];
                        int mul = (a * b);
                        while (mul < 0)
                        {
                            mul += 26;
                        }
                        ret[i, j] = (ret[i, j] + mul) % 26;
                    }
                }
            }

            return ret;
        }

        int[,] list_to_matrix(List<int> l, int n, int m)
        {
            int[,] ret = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int cur = i * m + j;
                    if (cur < l.Count)
                    {
                        ret[i, j] = l[cur];
                    }
                    else
                    {
                        ret[i, j] = 0;
                    }
                }
            }

            return ret;
        }
        int[,] list_to_matrix_flipped(List<int> l, int n, int m)
        {
            int[,] ret = new int[n, m];

            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    int cur = j * n + i;
                    if (cur < l.Count)
                    {
                        ret[i, j] = l[cur];
                    }
                    else
                    {
                        ret[i, j] = 0;
                    }
                    cur++;
                }
            }
            return ret;
        }

        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            List<int> ret = new List<int>();
            int sq = sqrt(key.Count);
            int[,] key_mat = list_to_matrix(key, sq, sq);

            int cur = 0;
            while (cur < plainText.Count)
            {
                int[,] cur_mat = new int[sq, 1];
                for (int i = 0; i < sq; i++)
                {
                    if (cur < plainText.Count)
                    {
                        cur_mat[i, 0] = plainText[cur];
                    }
                    else
                    {
                        cur_mat[i, 0] = 0;
                    }
                    cur++;
                }

                int[,] multiplied_mat = mat_mul(key_mat, cur_mat);

                for (int i = 0; i < sq; i++)
                {
                    ret.Add(multiplied_mat[i, 0]);
                }
            }

            return ret;
        }
        public string Encrypt(string plainText, string key)
        {
            List<int> plain_text_list = new List<int>();
            List<int> key_list = new List<int>();
            foreach (char ch in plainText) { plain_text_list.Add(ch - 'a'); }
            foreach (char ch in key) { key_list.Add(ch - 'a'); }
            string ret = "";
            int sq = sqrt(key_list.Count);

            int[,] key_mat = list_to_matrix(key_list, sq, sq);

            int cur = 0;
            while (cur < plain_text_list.Count)
            {
                int[,] cur_mat = new int[sq, 1];
                for (int i = 0; i < sq; i++)
                {
                    if (cur < plain_text_list.Count)
                    {
                        cur_mat[i, 0] = plainText[cur];
                    }
                    else
                    {
                        cur_mat[i, 0] = 0;
                    }
                    cur++;
                }

                int[,] multiplied_mat = mat_mul(key_mat, cur_mat);

                for (int i = 0; i < sq; i++)
                {
                    ret += (char)('a' + multiplied_mat[i, 0]);
                }
            }

            return ret;
        }

        public List<int> Analyse3By3Key(List<int> plain3, List<int> cipher3)
        {
            int sq = sqrt(plain3.Count);
            int[,] p = list_to_matrix_flipped(plain3, sq, sq);
            int b = get_b(p, sq, sq);
            if (b == 0)
            {
                throw new InvalidAnlysisException();
            }
            int[,] inv = mat_inv(p);
            int d = get_det(p, sq);
            for (int i = 0; i < sq; i++)
            {
                for (int j = 0; j < sq; j++)
                {
                    inv[i, j] = b * power(-1, i + j) * get_min(p, i, j);
                    while (inv[i, j] < 0)
                    {
                        inv[i, j] += 26;
                    }
                    inv[i, j] %= 26;
                }
            }
            inv = transpose(inv);
            int[,] c = list_to_matrix_flipped(cipher3, sq, sq);
            int[,] res = mat_mul(c, inv);
            List<int> ret = new List<int>();
            for (int i = 0; i < sq; i++)
            {
                for (int j = 0; j < sq; j++)
                {
                    ret.Add(res[i, j]);
                }
            }
            return ret;
        }

        public string Analyse3By3Key(string plain3, string cipher3)
        {
            List<int> p = new List<int>();
            for (int i = 0; i < plain3.Length; i++) p.Add(plain3[i]);
            List<int> c = new List<int>();
            for (int i = 0; i < cipher3.Length; i++) c.Add(cipher3[i]);


            int sq = sqrt(p.Count);
            int[,] p_mat = list_to_matrix_flipped(p, sq, sq);
            int b = get_b(p_mat, sq, sq);
            if (b == 0)
            {
                throw new InvalidAnlysisException();
            }
            int[,] inv = mat_inv(p_mat);
            int d = get_det(p_mat, sq);
            for (int i = 0; i < sq; i++)
            {
                for (int j = 0; j < sq; j++)
                {
                    inv[i, j] = b * power(-1, i + j) * get_min(p_mat, i, j);
                    while (inv[i, j] < 0)
                    {
                        inv[i, j] += 26;
                    }
                    inv[i, j] %= 26;
                }
            }
            inv = transpose(inv);
            int[,] c_mat = list_to_matrix_flipped(c, sq, sq);
            int[,] res = mat_mul(c_mat, inv);
            List<int> ret = new List<int>();
            for (int i = 0; i < sq; i++)
            {
                for (int j = 0; j < sq; j++)
                {
                    ret.Add(res[i, j]);
                }
            }
            string r = "";
            for (int i = 0; i < ret.Count; i++) r += (char)(ret[i] + 'a');
            return r;
        }
    }
}