using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            string repeatingKey = "";
            string repeatingText = "";

            for (int i = 0; i < cipherText.Length; i++)
            {
                int key = ((cipherText[i] - plainText[i]) + 26) % 26;
                repeatingKey += (char)('a' + key);
            }

            repeatingText += repeatingKey[0];

            for (int i = 1; i < repeatingKey.Length; i++)
            {
                if (plainText == Decrypt(cipherText, repeatingText))
                {
                    return repeatingText;
                }
                repeatingText += repeatingKey[i];
            }

            repeatingKey = repeatingKey.ToLower();
            return repeatingKey;

        }


        public string Decrypt(string cipherText, string key)
        {
            string repeatingPlainText = "";
            cipherText = cipherText.ToLower();
            char[,] alphabetMatrix = new char[26, 26];

            int decryptCounter = 0;
            while (key.Length < cipherText.Length)
            {
                key += key[decryptCounter % key.Length];
                decryptCounter++;
            }

            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    alphabetMatrix[i, j] = (char)((i + j) % 26 + 'a');
                }
            }

            for (int i = 0; i < cipherText.Length; i++)
            {
                int index = key[i] - 'a';
                int countIndex = 0;
                while (alphabetMatrix[index, countIndex] != cipherText[i])
                {
                    countIndex++;
                }
                repeatingPlainText += (char)(countIndex + 'a');
            }
            return repeatingPlainText;
        }

        public string Encrypt(string plainText, string key)
        {
            string repeatingCipherText = "";
            char[,] alphabetMatrix = new char[26, 26];

            int encryptCounter = 0;
            while (key.Length < plainText.Length)
            {
                key += key[encryptCounter % key.Length];
                encryptCounter++;
            }

            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    alphabetMatrix[i, j] = (char)((i + j) % 26 + 'a');
                }
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                int index1 = key[i] - 'a';
                int index2 = plainText[i] - 'a';
                repeatingCipherText += alphabetMatrix[index1, index2];
            }
            return repeatingCipherText;
        }
    }
}

