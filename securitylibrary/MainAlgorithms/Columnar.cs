using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public int FindCells(string plainText)
        {
            int fillCells = 0;
            for (int i = 4; i < 8; i++)
            {
                if (plainText.Length % i == 0)
                {
                    fillCells = i;
                }
            }
            return fillCells;
        }

        public List<int> Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int fillCells = FindCells(plainText);
            int fillRows = cipherText.Length / fillCells;
            List<int> key = new List<int>(fillCells);

            for (int i = 0; i < fillCells; i++)
            {
                bool columnMatchFound = false;
                for (int j = 0; j < cipherText.Length; j += fillRows)
                {
                    if (plainText[i] == cipherText[j])
                    {
                        int currentPlainTextIndex = i;
                        bool columnMatch = true;

                        for (int k = 0; k < fillRows; k++)
                        {
                            if (plainText[currentPlainTextIndex] != cipherText[j + k])
                            {
                                columnMatch = false;
                                break;
                            }
                            currentPlainTextIndex += fillCells;
                            if (currentPlainTextIndex >= plainText.Length)
                            {
                                break;
                            }
                        }

                        if (columnMatch)
                        {
                            columnMatchFound = true;
                            key.Add((j / fillRows) + 1);
                            break; 
                        }
                    }
                }

                if (!columnMatchFound)
                {
                    key.Add(0);
                    key.Add(0);
                }
            }

            return key;
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            int rowCount = (cipherText.Length + key.Count - 1) / key.Count;
            char[,] decryptionMatrix = new char[rowCount, key.Count];
            int index = 0;
            for (int col = 0; col < key.Count; col++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    int originalCol = key.IndexOf(col + 1);
                    if (index < cipherText.Length)
                    {
                        decryptionMatrix[row, originalCol] = cipherText[index++];
                    }
                    else
                    {
                        decryptionMatrix[row, originalCol] = 'x';
                    }
                }
            }

            string plaintext = "";
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < key.Count; col++)
                {
                    if (decryptionMatrix[row, col] != 'x')
                    {
                        plaintext += (decryptionMatrix[row, col]);
                    }
                }
            }

            return plaintext.ToLower();
        }

        public string Encrypt(string plainText, List<int> key)
        {
            int rowCount = (plainText.Length + key.Count - 1) / key.Count;
            char[,] encryptionMatrix = new char[rowCount, key.Count];
            int index = 0;
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < key.Count; col++)
                {
                    if (index < plainText.Length)
                        encryptionMatrix[row, col] = plainText[index++];
                    else
                        encryptionMatrix[row, col] = 'x'; 
                }
            }

            string ciphertext = "";
            Dictionary<int, int> columnsMapping = new Dictionary<int, int>();
            for (int i = 1; i <= key.Count; i++)
            {
                columnsMapping.Add(key[i - 1], i);
            }
            var sortedMappedColumns = columnsMapping.OrderBy(x => x.Key);
            foreach (var pair in sortedMappedColumns)
            {
                int mappedColIndex = pair.Value;
                for (int row = 0; row < rowCount; row++)
                {
                    ciphertext += char.ToUpper(encryptionMatrix[row, mappedColIndex - 1]);
                }
            }
            return ciphertext;
        }
    }
}
