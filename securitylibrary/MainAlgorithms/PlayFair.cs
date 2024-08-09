using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {

        public string Decrypt(string cipherText, string key)
        {
            char[,] playFairMatrix = new char[5, 5];
            string decryptText = "";
            int counter1 = 0;
            int counter2 = 0;
            int truee = 1;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (counter1 < key.Length)
                    {
                        while (decryptText.Contains(key[counter1]))
                        {
                            counter1++;
                        }
                        if (!decryptText.Contains(key[counter1]))
                        {
                            decryptText += key[counter1];
                        }
                        if (key[counter1] == 'i' || key[counter1] == 'j')
                        {
                            if (truee == 1)
                            {
                                playFairMatrix[i, j] = 'i';
                                truee = 0;
                            }
                        }
                        else
                        {
                            playFairMatrix[i, j] = key[counter1];
                        }
                        counter1++;
                    }
                    else
                    {
                        while (decryptText.Contains((char)('a' + counter2)) || (char)('a' + counter2) == 'j')
                        {
                            counter2++;
                        }
                        if (!decryptText.Contains((char)('a' + counter2)))
                        {
                            decryptText += (char)('a' + counter2);
                            if ((char)('a' + counter2) == 'i' || (char)('a' + counter2) == 'j')
                            {
                                if (truee == 1)
                                {
                                    playFairMatrix[i, j] = 'i';
                                    truee = 0;
                                }
                                else
                                {
                                    counter2++;
                                    while (decryptText.Contains((char)('a' + counter2)) || (char)('a' + counter2) == 'i' || (char)('a' + counter2) == 'j')
                                    {
                                        counter2++;
                                    }
                                    if (!decryptText.Contains((char)('a' + counter2)))
                                    {
                                        playFairMatrix[i, j] = (char)('a' + counter2);
                                    }
                                }
                            }
                            else
                            {
                                playFairMatrix[i, j] = (char)('a' + counter2);
                            }
                            counter2++;
                        }
                    }
                }
            }

            string decryptText1 = "";
            cipherText = cipherText.ToLower();

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (i == cipherText.Length - 1)
                {
                    if (cipherText.Length < 25)
                    {
                        decryptText1 += cipherText[i];
                        decryptText1 += 'x';
                    }
                }
                else
                {
                    if (cipherText[i] == cipherText[i + 1])
                    {
                        decryptText1 += cipherText[i];
                        decryptText1 += 'x';
                    }
                    else
                    {
                        decryptText1 += cipherText[i];
                        i++;
                        decryptText1 += cipherText[i];
                    }
                }
            }

            string decryptText2 = "";
            for (int i = 0; i < decryptText1.Length; i++)
            {
                int decryptRow1, decryptCol1, decryptRow2, decryptCol2;
                int[] cell1 = findCoords(playFairMatrix, decryptText1[i]);
                i++;
                int[] cell2 = findCoords(playFairMatrix, decryptText1[i]);
                decryptRow1 = cell1[0];
                decryptCol1 = cell1[1];
                decryptRow2 = cell2[0];
                decryptCol2 = cell2[1];

                if (decryptRow1 == decryptRow2)
                {
                    if (decryptCol1 == 0)
                    {
                        decryptText2 += playFairMatrix[decryptRow1, 4];
                    }
                    else
                    {
                        decryptText2 += playFairMatrix[decryptRow1, decryptCol1 - 1];
                    }
                    if (decryptCol2 == 0)
                    {
                        decryptText2 += playFairMatrix[decryptRow2, 4];
                    }
                    else
                    {
                        decryptText2 += playFairMatrix[decryptRow1, decryptCol2 - 1];
                    }
                }
                else if (decryptCol1 == decryptCol2)
                {
                    if (decryptRow1 == 0)
                    {
                        decryptText2 += playFairMatrix[4, decryptCol1];
                    }
                    else
                    {
                        decryptText2 += playFairMatrix[decryptRow1 - 1, decryptCol1];
                    }
                    if (decryptRow2 == 0)
                    {
                        decryptText2 += playFairMatrix[4, decryptCol2];
                    }
                    else
                    {
                        decryptText2 += playFairMatrix[decryptRow2 - 1, decryptCol2];
                    }
                }
                else
                {
                    decryptText2 += playFairMatrix[decryptRow1, decryptCol2];
                    decryptText2 += playFairMatrix[decryptRow2, decryptCol1];
                }
            }

            string finalDencryptText = "";
            for (int i = 0; i < decryptText2.Length; i++)
            {
                if (decryptText2[i] == 'x')
                {
                    if (i % 2 != 0)
                    {
                        if (i != decryptText2.Length - 1)
                        {
                            if (decryptText2[i - 1] == decryptText2[i + 1])
                            {
                                i++;
                                finalDencryptText += decryptText2[i];
                            }
                            else
                            {
                                finalDencryptText += decryptText2[i];
                            }
                        }
                    }
                    else
                    {
                        finalDencryptText += decryptText2[i];
                    }
                }
                else
                {
                    finalDencryptText += decryptText2[i];
                }
            }

            return finalDencryptText;

        }

        public string Encrypt(string plainText, string key)
        {
            char[,] playFairMatrix = new char[5, 5];
            string encryptText = "";
            int counter1 = 0;
            int counter2 = 0;
            int truee = 1;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (counter1 < key.Length)
                    {
                        while (encryptText.Contains(key[counter1]))
                        {
                            counter1++;
                        }
                        if (!encryptText.Contains(key[counter1]))
                        {
                            encryptText += key[counter1];
                        }
                        if (key[counter1] == 'i' || key[counter1] == 'j')
                        {
                            if (truee == 1)
                            {
                                playFairMatrix[i, j] = 'i';
                                truee = 0;
                            }
                        }
                        else
                        {
                            playFairMatrix[i, j] = key[counter1];
                        }
                        counter1++;
                    }
                    else
                    {
                        while (encryptText.Contains((char)('a' + counter2)) || (char)('a' + counter2) == 'j')
                        {
                            counter2++;
                        }
                        if (!encryptText.Contains((char)('a' + counter2)))
                        {
                            encryptText += (char)('a' + counter2);
                            if ((char)('a' + counter2) == 'i' || (char)('a' + counter2) == 'j')
                            {
                                if (truee == 1)
                                {
                                    playFairMatrix[i, j] = 'i';
                                    truee = 0;
                                }
                                else
                                {
                                    counter2++;
                                    while (encryptText.Contains((char)('a' + counter2)) || (char)('a' + counter2) == 'i' || (char)('a' + counter2) == 'j')
                                    {
                                        counter2++;
                                    }
                                    if (!encryptText.Contains((char)('a' + counter2)))
                                    {
                                        playFairMatrix[i, j] = (char)('a' + counter2);
                                    }
                                }
                            }
                            else
                            {
                                playFairMatrix[i, j] = (char)('a' + counter2);
                            }
                            counter2++;
                        }
                    }
                }
            }

            string encryptText1 = "";
            plainText = plainText.ToLower();

            for (int i = 0; i < plainText.Length; i++)
            {
                if (i == plainText.Length - 1)
                {
                    if (plainText.Length < 25)
                    {
                        encryptText1 += plainText[i];
                        encryptText1 += 'x';
                    }
                }
                else
                {
                    if (plainText[i] == plainText[i + 1])
                    {
                        encryptText1 += plainText[i];
                        encryptText1 += 'x';
                    }
                    else
                    {
                        encryptText1 += plainText[i];
                        i++;
                        encryptText1 += plainText[i];
                    }
                }
            }

            string finalEncryptText = "";
            int encryptRow1, encryptCol1, encryptRow2, encryptCol2;

            for (int i = 0; i < encryptText1.Length; i++)
            {
                int[] temp1 = findCoords(playFairMatrix, encryptText1[i]);
                encryptRow1 = temp1[0];
                encryptCol1 = temp1[1];
                i++;
                int[] temp2 = findCoords(playFairMatrix, encryptText1[i]);
                encryptRow2 = temp2[0];
                encryptCol2 = temp2[1];

                if (temp1 != null && temp2 != null)
                {
                    if (encryptRow1 == encryptRow2)
                    {
                        finalEncryptText += playFairMatrix[encryptRow1, (encryptCol1 + 1) % 5];
                        finalEncryptText += playFairMatrix[encryptRow2, (encryptCol2 + 1) % 5];
                    }
                    else if (encryptCol1 == encryptCol2)
                    {
                        finalEncryptText += playFairMatrix[(encryptRow1 + 1) % 5, encryptCol1];
                        finalEncryptText += playFairMatrix[(encryptRow2 + 1) % 5, encryptCol2];
                    }
                    else
                    {
                        finalEncryptText += playFairMatrix[encryptRow1, encryptCol2];
                        finalEncryptText += playFairMatrix[encryptRow2, encryptCol1];
                    }
                }
            }
            return finalEncryptText;
        }


        private int[] findCoords(char[,] matrix, char c)
        {
            int[] coordinates = new int[2];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j] == c)
                    {
                        coordinates[0] = i;
                        coordinates[1] = j;
                        return coordinates;
                    }
                }
            }
            return coordinates;
        }

    }
}