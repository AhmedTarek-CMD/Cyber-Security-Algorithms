using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{

    public class DES : CryptographicTechnique
    {
        int[,] S1 = new int[4, 16] {{ 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                                    { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                                    { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                                    { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 }};

        int[,] S2 = new int[4, 16] {{ 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                                    { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                                    { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                                    { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 }};

        int[,] S3 = new int[4, 16] {{ 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                                    { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                                    { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                                    { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }};

        int[,] S4 = new int[4, 16] {{ 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                                    { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                                    { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                                    { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 }};

        int[,] S5 = new int[4, 16] {{ 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                                    { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                                    { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                                    { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 }};

        int[,] S6 = new int[4, 16] {{ 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                                    { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                                    { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                                    { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 }};

        int[,] S7 = new int[4, 16] {{ 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                                    { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                                    { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                                    { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 }};

        int[,] S8 = new int[4, 16] {{ 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                                    { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                                    { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                                    { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 }};

        int[,] P = new int[8, 4] {{ 16, 7, 20, 21 },
                                  { 29, 12, 28, 17 },
                                  { 1, 15, 23, 26 },
                                  { 5, 18, 31, 10 },
                                  { 2, 8, 24, 14 },
                                  { 32, 27, 3, 9 },
                                  { 19, 13, 30, 6 },
                                  { 22, 11, 4, 25 }};

        int[,] IP = new int[8, 8] {{ 58, 50, 42, 34, 26, 18, 10, 2 },
                                   { 60, 52, 44, 36, 28, 20, 12, 4 },
                                   { 62, 54, 46, 38, 30, 22, 14, 6 },
                                   { 64, 56, 48, 40, 32, 24, 16, 8 },
                                   { 57, 49, 41, 33, 25, 17, 9, 1 },
                                   { 59, 51, 43, 35, 27, 19, 11, 3 },
                                   { 61, 53, 45, 37, 29, 21, 13, 5 },
                                   { 63, 55, 47, 39, 31, 23, 15, 7 }};

        int[,] IP_1 = new int[8, 8] {{ 40, 8, 48, 16, 56, 24, 64, 32 },
                                    { 39, 7, 47, 15, 55, 23, 63, 31 },
                                    { 38, 6, 46, 14, 54, 22, 62, 30 },
                                    { 37, 5, 45, 13, 53, 21, 61, 29 },
                                    { 36, 4, 44, 12, 52, 20, 60, 28 },
                                    { 35, 3, 43, 11, 51, 19, 59, 27 },
                                    { 34, 2, 42, 10, 50, 18, 58, 26 },
                                    { 33, 1, 41, 9, 49, 17, 57, 25 }};

        int[,] PC_1 = new int[8, 7] {{ 57, 49, 41, 33, 25, 17, 9 },
                                     { 1, 58, 50, 42, 34, 26, 18 },
                                     { 10, 2, 59, 51, 43, 35, 27 },
                                     { 19, 11, 3, 60, 52, 44, 36 },
                                     { 63, 55, 47, 39, 31, 23, 15},
                                     { 7, 62, 54, 46, 38, 30, 22 },
                                     { 14, 6, 61, 53, 45, 37, 29 },
                                     { 21, 13, 5, 28, 20, 12, 4 }};

        int[,] PC_2 = new int[8, 6] {{ 14, 17, 11, 24, 1, 5 },
                                      { 3, 28, 15, 6, 21, 10 },
                                      { 23, 19, 12, 4, 26, 8 },
                                      { 16, 7, 27, 20, 13, 2 },
                                      { 41, 52, 31, 37, 47, 55},
                                      { 30, 40, 51, 45, 33, 48 },
                                      { 44, 49, 39, 56, 34, 53 },
                                      { 46, 42, 50, 36, 29, 32 }};

        int[,] E_bit = new int[8, 6] {{ 32, 1, 2, 3, 4, 5 },
                                      { 4, 5, 6, 7, 8, 9 },
                                      { 8, 9, 10, 11, 12, 13 },
                                      { 12, 13, 14, 15, 16, 17 },
                                      { 16, 17, 18, 19, 20, 21 },
                                      { 20, 21, 22, 23, 24, 25 },
                                      { 24, 25, 26, 27, 28, 29 },
                                      { 28, 29, 30, 31, 32, 1 }};

        List<string> sbox = new List<string>();
        string ebit_selection, XOR, premutation, tempSubBox, leftShift, DEES, leftMessage, rightMessage;
        int choice = 0;

        void Fun()
        {
            for (int i = 0; i < sbox.Count; i++)
            {
                string currentSBoxValue = sbox[i];
                string currSbox = currentSBoxValue[0].ToString() + currentSBoxValue[5];
                string currSbox1 = currentSBoxValue.Substring(1, 4);
                int Sbox_32 = Convert.ToInt32(currSbox, 2);
                int Sbox1_32 = Convert.ToInt32(currSbox1, 2);

                switch (i)
                {
                    case 0: choice = S1[Sbox_32, Sbox1_32]; 
                        break;
                    case 1: choice = S2[Sbox_32, Sbox1_32]; 
                        break;
                    case 2: choice = S3[Sbox_32, Sbox1_32]; 
                        break;
                    case 3: choice = S4[Sbox_32, Sbox1_32]; 
                        break;
                    case 4: choice = S5[Sbox_32, Sbox1_32]; 
                        break;
                    case 5: choice = S6[Sbox_32, Sbox1_32]; 
                        break;
                    case 6: choice = S7[Sbox_32, Sbox1_32]; 
                        break;
                    case 7: choice = S8[Sbox_32, Sbox1_32];
                        break;
                    default: 
                        throw new InvalidOperationException("Invalid index");
                }

                string binary = Convert.ToString(choice, 2);
                string paddedBinary = binary.PadLeft(4, '0');
                tempSubBox += paddedBinary;
            }
        }

        public override string Decrypt(string cipherText, string key)
        {

            string decryptCipherText = Convert.ToString(Convert.ToInt64(cipherText, 16), 2).PadLeft(64, '0');
            string decryptKey = Convert.ToString(Convert.ToInt64(key, 16), 2).PadLeft(64, '0');
            string leftMessage = "";
            string rightMessage = "";
            string every = "";
            string every1;
            int halfLength = decryptCipherText.Length / 2;
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            List<string> left = new List<string>();
            List<string> right = new List<string>();

            for (int i = 0; i < halfLength; i++)
            {
                leftMessage += decryptCipherText[i];
                rightMessage += decryptCipherText[i + halfLength];
            }
 
            for (int i = 0; i < 56; i++)
            {
                int halfIndex1 = i / 7;
                int halfIndex2 = i % 7;
                every += decryptKey[PC_1[halfIndex1, halfIndex2] - 1];
            }

            string newlist1 = every.Substring(0, 28);
            string newlist2 = every.Substring(28, 28);

            for (int i = 0; i <= 16; i++)
            {
                list1.Add(newlist1);
                list2.Add(newlist2);

                int shiftCount;
                if (i == 0 || i == 1 || i == 8 || i == 15)
                {
                    shiftCount = 1;
                }
                else
                {
                    shiftCount = 2;
                }

                newlist1 = LeftShift(newlist1, shiftCount);
                newlist2 = LeftShift(newlist2, shiftCount);
            }

            string IP1 = "";
            
            for (int i = 0; i < 64; i++)
            {
                int halfIndex1 = i / 8;
                int halfIndex2 = i % 8;
                IP1 += decryptCipherText[IP[halfIndex1, halfIndex2] - 1];
            }

            string newLeft = IP1.Substring(0, 32);
            string newRight = IP1.Substring(32, 32);
            List<string> oldK = new List<string>();

            for (int i = 0; i < list2.Count; i++)
            {
                oldK.Add(list1[i] + list2[i]);
            }

            List<string> newK = new List<string>();

            for (int i = 1; i < oldK.Count; i++)
            {
                every = "";
                every1 = oldK[i];

                for (int j = 0; j < 48; j++)
                {
                    int halfIndex1 = j / 6;
                    int halfIndex2 = j % 6;
                    every += every1[PC_2[halfIndex1, halfIndex2] - 1];
                }
                newK.Add(every);
            }

            left.Add(newLeft);
            right.Add(newRight);

            for (int i = 0; i < 16; i++)
            {
                left.Add(newRight);
                sbox.Clear();
                ebit_selection = "";
                leftShift = "";
                premutation = "";
                tempSubBox = "";

                for (int j = 0; j < 48; j++)
                {
                    int halfIndex1 = j / 6;
                    int halfIndex2 = j % 6;
                    ebit_selection += newRight[E_bit[halfIndex1, halfIndex2] - 1];
                }

                XOR = PerformXorOperation(newK[newK.Count - 1 - i], ebit_selection);

                for (int h = 0; h < XOR.Length; h += 6)
                {
                    DEES = XOR.Substring(h, Math.Min(6, XOR.Length - h));
                    sbox.Add(DEES);
                }

                Fun();

                for (int k = 0; k < 32; k++)
                {
                    int halfIndex1 = k / 4;
                    int halfIndex2 = k % 4;
                    premutation += tempSubBox[P[halfIndex1, halfIndex2] - 1];
                }

                for (int k = 0; k < premutation.Length; k++)
                {
                    leftShift += (premutation[k] ^ newLeft[k]);
                }

                newRight = leftShift;
                newLeft = left[i + 1];
                right.Add(newRight);
            }

            string leftRight = right[16] + left[16];
            string decryptPlainText = "";
            
            for (int i = 0; i < 64; i++)
            {
                int rowIndex = i / 8;
                int colIndex = i % 8;
                decryptPlainText += leftRight[IP_1[rowIndex, colIndex] - 1];
            }

            return "0x" + Convert.ToInt64(decryptPlainText, 2).ToString("X").PadLeft(16, '0');
        }

        public override string Encrypt(string plainText, string key)
        {

            string encryptPlainText = Convert.ToString(Convert.ToInt64(plainText, 16), 2).PadLeft(64, '0');
            string encryptKey = Convert.ToString(Convert.ToInt64(key, 16), 2).PadLeft(64, '0');
            string every = "";
            string every1;
            int halfLength = encryptPlainText.Length / 2;
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            List<string> left = new List<string>();
            List<string> right = new List<string>();

            for (int i = 0; i < halfLength; i++)
            {
                leftMessage += encryptPlainText[i];
                rightMessage += encryptPlainText[i + halfLength];
            }

            for (int i = 0; i < 56; i++)
            {
                int halfIndex1 = i / 7;
                int halfIndex2 = i % 7;
                every += encryptKey[PC_1[halfIndex1, halfIndex2] - 1];
            }

            string newList1 = every.Substring(0, 28);
            string newList2 = every.Substring(28, 28);

            for (int i = 0; i <= 16; i++)
            {
                list1.Add(newList1);
                list2.Add(newList2);

                int shiftCount;
                if (i == 0 || i == 1 || i == 8 || i == 15)
                {
                    shiftCount = 1;
                }
                else
                {
                    shiftCount = 2;
                }

                newList1 = LeftShift(newList1, shiftCount);
                newList2 = LeftShift(newList2, shiftCount);
            }

            string IP1 = "";

            for (int i = 0; i < 64; i++)
            {
                int halfIndex1 = i / 8;
                int halfIndex2 = i % 8;
                IP1 += encryptPlainText[IP[halfIndex1, halfIndex2] - 1];
            }

            string newLeft = IP1.Substring(0, 32);
            string newRight = IP1.Substring(32, 32);

            List<string> oldkey = new List<string>();

            for (int i = 0; i < list2.Count; i++)
            {
                oldkey.Add(list1[i] + list2[i]);
            }

            List<string> newkey = new List<string>();

            for (int k = 1; k < oldkey.Count; k++)
            {
                every = "";
                every1 = oldkey[k];
                
                for (int j = 0; j < 48; j++)
                {
                    int halfIndex1 = j / 6;
                    int halfIndex2 = j % 6;
                    every += every1[PC_2[halfIndex1, halfIndex2] - 1];
                }

                newkey.Add(every);
            }

            left.Add(newLeft);
            right.Add(newRight);

            for (int i = 0; i < 16; i++)
            {
                left.Add(newRight);
                sbox.Clear();
                ebit_selection = "";
                leftShift = "";
                premutation = "";
                tempSubBox = "";
                XOR = "";

                for (int j = 0; j < 48; j++)
                {
                    int halfIndex1 = j / 6;
                    int halfIndex2 = j % 6;
                    ebit_selection += newRight[E_bit[halfIndex1, halfIndex2] - 1];
                }

                for (int g = 0; g < ebit_selection.Length; g++)
                {
                    XOR = XOR + (newkey[i][g] ^ ebit_selection[g]).ToString();
                }
                
                for (int h = 0; h < XOR.Length; h += 6)
                {
                    DEES = XOR.Substring(h, Math.Min(6, XOR.Length - h));
                    sbox.Add(DEES);
                }
                
                Fun();

                for (int q = 0; q < 32; q++)
                {
                    int halfIndex1 = q / 4;
                    int halfIndex2 = q % 4;
                    premutation += tempSubBox[P[halfIndex1, halfIndex2] - 1];
                }

                for (int s = 0; s < premutation.Length; s++)
                {
                    leftShift += (premutation[s] ^ newLeft[s]);
                }

                newRight = leftShift;
                newLeft = left[i + 1];
                right.Add(newRight);
            }

            string leftRight = right[16] + left[16];
            string encryptCipherText = "";
            
            for (int i = 0; i < 64; i++)
            {
                int rowIndex = i / 8;
                int colIndex = i % 8;
                encryptCipherText += leftRight[IP_1[rowIndex, colIndex] - 1];
            }

            return "0x" + Convert.ToInt64(encryptCipherText, 2).ToString("X");
        }

        private string LeftShift(string binaryString, int shifts)
        {
            return binaryString.Substring(shifts) + binaryString.Substring(0, shifts);
        }

        private string PerformXorOperation(string key, string data)
        {
            StringBuilder xorResult = new StringBuilder();
            for (int g = 0; g < data.Length; g++)
            {
                xorResult.Append((key[g] ^ data[g]).ToString());
            }
            return xorResult.ToString();
        }
    }
}