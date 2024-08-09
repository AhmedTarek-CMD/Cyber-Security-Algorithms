using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            string key = "";

            for (int i = 0; i < cipherText.Length; i++)
            {
                int add = (cipherText[i] - plainText[i] + 26) % 26;
                key += (char)('a' + add);
            }

            string possibleKey = key[0].ToString();
            for (int i = 1; i < key.Length; i++)
            {
                if (plainText == Decrypt(cipherText, possibleKey))
                {
                    return possibleKey;
                }
                possibleKey += key[i];
            }

            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            string autoKeyPlainText = "";
            cipherText = cipherText.ToLower();

            for (int i = 0; i < autoKeyPlainText.Length; i++)
            {
                int index = (cipherText[i] - key[i] + 26) % 26;
                key += (char)('a' + index);
            }

            for (int i = 0; i < cipherText.Length; i++)
            {
                char plainChar = (char)((cipherText[i] - key[i] + 26) % 26 + 'a');
                autoKeyPlainText += plainChar;
                key += plainChar; 
            }

            return autoKeyPlainText;
        }

        public string Encrypt(string plainText, string key)
        {
            string autoKeyCipherText = "";
            char[,] alphabetMatrix = new char[26, 26];
            string newKey = key + plainText.Substring(0, plainText.Length - key.Length);

            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    alphabetMatrix[i, j] = (char)('a' + (i + j) % 26);
                }
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                int keyIndex = newKey[i] - 'a';
                int plainIndex = plainText[i] - 'a';

                autoKeyCipherText += alphabetMatrix[plainIndex, keyIndex];
            }

            return autoKeyCipherText;
        }
    }
}
