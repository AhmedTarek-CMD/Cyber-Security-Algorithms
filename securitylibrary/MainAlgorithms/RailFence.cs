using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int row = 0;
            bool analysisFlag = false;

            foreach (char c in plainText)
            {
                if (analysisFlag == false)
                {
                    if (c == cipherText[1])
                    {
                        if (row == 1)
                        {
                            row++;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        row++;
                    }
                }
                else
                {
                    break;
                }
            }

            return row;
        }

        public string Decrypt(string cipherText, int key)
        {
            int columnCells = (cipherText.Length + key - 1) / key;
            char[,] railFenceMatrix = new char[key, columnCells];
            int matrixIndex = 0;
            string railFencePlainText = "";

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < columnCells; j++)
                {
                    if (matrixIndex < cipherText.Length)
                    {
                        railFenceMatrix[i, j] = cipherText[matrixIndex];
                        matrixIndex++;
                    }
                }
            }

            for (int j = 0; j < columnCells; j++)
            {
                for (int i = 0; i < key; i++)
                {
                    railFencePlainText += railFenceMatrix[i, j];
                }
            }

            return railFencePlainText;
        }

        public string Encrypt(string plainText, int key)
        {
            int columnCells = (plainText.Length + key - 1) / key;
            char[,] railFenceMatrix = new char[key, columnCells];
            int matrixIndex = 0;
            string railFenceCipherText = "";

            for (int i = 0; i < columnCells; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    if (matrixIndex == plainText.Length)
                    {
                        break;
                    }
                    railFenceMatrix[j, i] = plainText[matrixIndex];
                    matrixIndex++;
                }
            }

            for (int j = 0; j < key; j++)
            {
                for (int i = 0; i < columnCells; i++)
                {
                    railFenceCipherText += railFenceMatrix[j, i];
                }
            }

            return railFenceCipherText;
        }
    }
}
