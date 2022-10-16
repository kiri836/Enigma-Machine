using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Enigma_Machine
{
    class Program
    {
        public static class GlobalVariables
        {
            public static char[] LetterList =                                                                                                                                        { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            public static char[] RotorOneArrangement =            { 'J', 'G', 'D', 'Q', 'O', 'X', 'U', 'S', 'C', 'A', 'M', 'I', 'F', 'R', 'V', 'T', 'P', 'N', 'E', 'W', 'K', 'B', 'L', 'Z', 'Y', 'H' };
            public static char[] RotorTwoArrangement =                                                                                                                     { 'N', 'T', 'Z', 'P', 'S', 'F', 'B', 'O', 'K', 'M', 'W', 'R', 'C', 'J', 'D', 'I', 'V', 'L', 'A', 'E', 'Y', 'U', 'X', 'H', 'G', 'Q' };
            public static char[] RotorThreeArrangement =                                                                                                                             { 'J', 'V', 'I', 'U', 'B', 'H', 'T', 'C', 'D', 'Y', 'A', 'K', 'E', 'Q', 'Z', 'P', 'O', 'S', 'G', 'X', 'N', 'R', 'M', 'W', 'F', 'L' };
            public static List<char> StringToBeConverted = new List<char>();
        }
        static void Main(string[] args)
        {
            for (int p = 0; p < 1000; p++)
            {
                void Help(string RotorStart)
                {
                    while (RotorStart == "help")
                    {
                        Console.WriteLine("An enigma machine generally has 3 rotors that scrabble the letters given, they have starting positions that go from 0 - 25." +
                                "\n You need to supply that for the rotors, so for x, y and z think of a random number (0 - 25) and enter it following the format. Or if you are encrypting an already established code use that.");
                        Console.WriteLine("\n Enter the rotor starting points. Format: x,y,z . If you need more information input: help");
                        RotorStart = Console.ReadLine();
                    }

                }
                Console.WriteLine("Would you like to encrypt or decrypt? Enter encrypt or decrypt for which ever action you want to do.");
                string Dencrypt = Console.ReadLine();
                if (Dencrypt == "encrypt" || Dencrypt == "ENCRYPT")
                {
                    Console.WriteLine("Enter the rotor starting points. Format: x,y,z . If you need more information input: help");
                    string RotorStart = Console.ReadLine();
                    if (RotorStart == "help")
                    {
                        Help(RotorStart);
                    }
                    string[] sStartingPoints = RotorStart.Split(',');
                    List<int> StartingPoints = new List<int>();
                    for (int i = 0; i < 3; i++)
                    {
                        StartingPoints.Add(Convert.ToInt32(sStartingPoints[i]));
                    }
                    int RotorOne = StartingPoints[0];
                    int RotorTwo = StartingPoints[1];
                    int RotorThree = StartingPoints[2];
                    if (RotorOne > 25 || RotorOne < 0 || RotorTwo > 25 || RotorTwo < 0 || RotorThree > 25 || RotorThree < 0)
                    {
                        Console.WriteLine("Rotor starting points are either below 0 or above 25, program terminating.");
                        Console.ReadLine();
                        return;
                    }
                    Console.WriteLine("Rotor starting points set. Now enter the sentence/s or word/s that you would like to encrypt, do not include special characters or spaces.");
                    Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                    char[] MainInput = Console.ReadLine().ToCharArray();
                    string MainOutput = "Lol";
                    EncryptionProcess(MainInput, MainOutput, RotorOne, RotorTwo, RotorThree);

                }
                else if (Dencrypt == "decrypt" || Dencrypt == "DECRYPT")
                {
                    Console.WriteLine("Enter the rotor starting points. Format: x,y,z . If you need more information input: help");
                    string RotorStart = Console.ReadLine();
                    if (RotorStart == "help")
                    {
                        Help(RotorStart);
                    }
                    string[] sStartingPoints = RotorStart.Split(',');
                    List<int> StartingPoints = new List<int>();
                    for (int i = 0; i < 3; i++)
                    {
                        StartingPoints.Add(Convert.ToInt32(sStartingPoints[i]));
                    }
                    int RotorOne = StartingPoints[0];
                    int RotorTwo = StartingPoints[1];
                    int RotorThree = StartingPoints[2];
                    if (RotorOne > 25 || RotorOne < 0 || RotorTwo > 25 || RotorTwo < 0 || RotorThree > 25 || RotorThree < 0)
                    {
                        Console.WriteLine("Rotor starting points are either below 0 or above 25, program terminating.");
                        Console.ReadLine();
                        return;
                    }
                    Console.WriteLine("Rotor starting points set. Now enter the sentence/s or word/s that you would like to decrypt, they HAVE TO BE CAPITAL(or it won't work), enter the encrypted message exactly (no spaces or special characters).");
                    Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                    char[] MainInput = Console.ReadLine().ToCharArray();
                    string MainOutput = "Lol";
                    DecryptionProcess(MainInput, MainOutput, RotorOne, RotorTwo, RotorThree);
                }
                else
                {
                    Console.WriteLine("Wrong input, try again.");
                    return;
                }

                void EncryptionProcess(char[] MainInput, string MainOutput, int RotorOne, int RotorTwo, int RotorThree)
                {
                    for (int i = 0; i < MainInput.Count(); i++)
                    {
                        int x = Array.IndexOf(GlobalVariables.LetterList, MainInput[i]);
                        int y = x + RotorOne;
                        if (y > 25) { y = y - 26; }
                        int RotorOneOutput = Array.IndexOf(GlobalVariables.LetterList, GlobalVariables.RotorOneArrangement[y]);
                        y = RotorTwo + RotorOneOutput;
                        if (y > 25) { y = y - 26; }
                        int RotorTwoOutput = Array.IndexOf(GlobalVariables.LetterList, GlobalVariables.RotorTwoArrangement[y]);
                        y = RotorThree + RotorTwoOutput;
                        if (y > 25) { y = y - 26; }
                        GlobalVariables.StringToBeConverted.Add(GlobalVariables.RotorThreeArrangement[y]);

                        RotorOne++;
                        if (RotorOne > 25)
                        {
                            RotorOne = 0;
                            RotorTwo++;
                        }
                        else if (RotorTwo > 25)
                        {
                            RotorTwo = 0;
                            RotorThree++;
                        }
                        else if (RotorThree > 25)
                        {
                            RotorThree = 0;
                        }
                    }
                    MainOutput = string.Join("", GlobalVariables.StringToBeConverted);
                    Console.WriteLine("Encryption has completed here is completed encryption:" + MainOutput + "\nPress enter to restart the program or click the x at the top right to close.");
                    Console.ReadLine();
                }
                void DecryptionProcess(char[] MainInput, string MainOutput, int RotorOne, int RotorTwo, int RotorThree)
                {
                    for (int i = 0; i < MainInput.Count(); i++)
                    {

                        int RotorThreeOutput = Array.IndexOf(GlobalVariables.RotorThreeArrangement, MainInput[i]);
                        int y = -RotorThree + RotorThreeOutput;
                        if (y < 0) { y = y + 26; }
                        if (y > 25) { y = y - 26; }
                        int RotorTwoOutput = Array.IndexOf(GlobalVariables.RotorTwoArrangement, GlobalVariables.LetterList[y]);
                        y = -RotorTwo + RotorTwoOutput;
                        if (y < 0) { y = y + 26; }
                        if (y > 25) { y = y - 26; }
                        int RotorOneOutput = Array.IndexOf(GlobalVariables.RotorOneArrangement, GlobalVariables.LetterList[y]);
                        y = -RotorOne + RotorOneOutput;
                        if (y < 0) { y = y + 26; }
                        if (y > 25) { y = y - 26; }
                        GlobalVariables.StringToBeConverted.Add(GlobalVariables.LetterList[y]);

                        RotorOne++;
                        if (RotorOne > 25)
                        {
                            RotorOne = 0;
                            RotorTwo++;
                        }
                        else if (RotorTwo > 25)
                        {
                            RotorTwo = 0;
                            RotorThree++;
                        }
                        else if (RotorThree > 25)
                        {
                            RotorThree = 0;
                        }
                    }
                    MainOutput = string.Join("", GlobalVariables.StringToBeConverted);
                    Console.WriteLine("Decryption has completed here is completed decryption:" + MainOutput + "\nPress enter to restart the program or click the x at the top right to close.");
                    Console.ReadLine();
                }

            }
            
        }
    }
}
