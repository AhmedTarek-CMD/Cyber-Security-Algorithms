using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {

        public string Encrypt(string plainText, int key)
        {
            string word = "";
            foreach (char c in plainText)
            {
                char upper = char.ToUpper(c);
                int charNumber = upper - 'A';
                int ceacerCipherEncryption = (charNumber + key) % 26;
                char convert = (char)(ceacerCipherEncryption + 'A');
                word += convert;
            }
            return word;
        }

        public string Decrypt(string cipherText, int key)
        {
            string word = "";
            foreach (char c in cipherText)
            {
                char upper = char.ToUpper(c);
                int charPosition = upper + 'A';
                int ceaserCipherDecryption = (charPosition - key) % 26;
                char convert = (char)(ceaserCipherDecryption + 'A');
                word += convert;
            }
            return word;
        }

        public int Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            int ceaserKey = 0;

            for (int i = 0; i < plainText.Length; i++)
            {
                int diffPC = (cipherText[i] - plainText[i] + 26) % 26; 
                if (diffPC != 0)
                {
                    ceaserKey = diffPC;
                    break;
                }
            }
            return ceaserKey;
        }
    }
}