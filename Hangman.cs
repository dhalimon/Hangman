using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hangman
{
    class Hangman
    {
        static List<string> LoadListOfWords()
        {
            StreamReader sr = null;
            try
            {
                sr  = new StreamReader("ListOfWords.txt");
            }
            catch(Exception)
            {
                Console.WriteLine("Cannot load text file with list of words!");
            }

            List<string> ListOfWords = new List<string>();
            string word;
            while ((word = sr.ReadLine()) != null)
            {
                ListOfWords.Add(word);
            }

            return ListOfWords;
        }
        static void Main(string[] args)
        {
            
            Random rnd = new Random();
            List<string> listOfWords = LoadListOfWords();
            string wordToGuess = listOfWords[rnd.Next(0, listOfWords.Count - 1)];
            string wordToGuessUpper = wordToGuess.ToUpper();
           
            StringBuilder wordToGuessDashed = new StringBuilder();

            for(int i = 0; i < wordToGuess.Length; i++)
            {
                wordToGuessDashed.Append('-');
            }

            string usedLetters = String.Empty;
            bool won = false;
            int numberOfFails = 0;
            string input = String.Empty;
            char guess;

            while(numberOfFails < 10 && !won)
            {
                Console.Write("Enter a letter: ");
                input = Console.ReadLine().ToUpper();
                guess = input[0];

                if(!Char.IsLetter(guess))
                {
                    Console.WriteLine($"{guess} is not a letter!");
                    continue;
                }

                if(usedLetters.Contains(guess))
                {
                    Console.WriteLine($"You've already used {guess}!");
                    continue;
                }

                if(wordToGuessUpper.Contains(guess))
                {
                    for(int j = 0; j < wordToGuessUpper.Length; j++)
                    {
                        if(wordToGuessUpper[j] == guess )
                        {
                            wordToGuessDashed[j] = guess;
                        }
                    }
                    Console.WriteLine(wordToGuessDashed.ToString());
                }
                else
                {
                    numberOfFails++;
                    usedLetters += guess;
                    Console.WriteLine(wordToGuessDashed.ToString());
                    if(numberOfFails < 10)
                    {
                        Console.WriteLine($"Used letters: {usedLetters}");
                    }
                }

                if(wordToGuessDashed.ToString().Equals(wordToGuessUpper))
                {
                    won = true;
                }
            }

            if(won)
            {
                Console.WriteLine("You won!");
            }
            else
            {
                Console.WriteLine($"You lost! Correct answer is: {wordToGuessUpper}");
            }
            Console.Write("Hit enter to exit...");
            Console.ReadLine();
        }
    }
}
