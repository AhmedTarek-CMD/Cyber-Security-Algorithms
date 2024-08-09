using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            string letters = "abcdefghijklmnopqrstuvwxyz";
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();

            char[] keys = new char[26];
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] = ' ';
            }

            foreach (char letter in plainText)
            {
                if (char.IsLetter(letter))
                {
                    int index = plainText.IndexOf(letter);
                    keys[letters.IndexOf(letter)] = cipherText[index];
                }
            }

            int keyIndex = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == ' ')
                {
                    while (keys.Contains(letters[keyIndex]) || cipherText.Contains(letters[keyIndex]))
                    {
                        keyIndex++;
                    }
                    keys[i] = letters[keyIndex++];
                }
            }

            return new string(keys);
        }

        public string Decrypt(string cipherText, string key)
        {
            key = key.ToUpper();
            char[] decryptionKey = new char[26];

            for (int i = 0; i < 26; i++)
            {
                decryptionKey[i] = key[i];
            }

            string decryptedText = "";

            foreach (char c in cipherText)
            {
                char decryptedChar = (char)(Array.IndexOf(decryptionKey, char.ToUpper(c)) + 'A');
                decryptedText += char.ToLower(decryptedChar);
            }
            return decryptedText;
        }

        public string Encrypt(string plainText, string key)
        {
            key = key.ToUpper();
            char[] encryptionKey = new char[26];

            for (int i = 0; i < 26; i++)
            {
                encryptionKey[i] = key[i];
            }

            string encryptedText = "";

            foreach (char c in plainText)
            {
                char encryptedChar = encryptionKey[char.ToUpper(c) - 'A'];
                encryptedText += char.ToLower(encryptedChar);
            }
            return encryptedText;
        }







        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	=
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        /// 

        public string AnalyseUsingCharFrequency(string cipher)
        {
            string englishAlphabet = "ETAOINSRHLUMFDCPGWYBVKXQJZ";
            int[] monoalphabeticCipherFrequency = new int[26];

            foreach (char c in cipher)
            {
                int i = c - 'A';
                monoalphabeticCipherFrequency[i]++;
            }

            char[] mapping = new char[26];

            for (int i = 0; i < 26; i++)
            {
                int index = 0;
                for (int j = 0; j < 26; j++)
                {
                    if (monoalphabeticCipherFrequency[j] > monoalphabeticCipherFrequency[index])
                    {
                        index = j;
                    }
                }
                char monoalphabeticCipherLetter = (char)(index + 'A');
                mapping[monoalphabeticCipherLetter - 'A'] = englishAlphabet[i];
                monoalphabeticCipherFrequency[index] = -1;
            }

            string key = "";

            foreach (char c in cipher)
            {
                int j = c - 'A';
                key += mapping[j];
            }

            return key.ToLower();
        }
    }
}