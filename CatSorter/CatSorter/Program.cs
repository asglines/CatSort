using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        // Sample list of words
        List<string> words = new List<string>
        {
            "Mountain", "Electric", "Velocity", "Shadow", "Fortress",
            "Whisper", "Galaxy", "Chronicle", "Horizon", "Nebula"
        };

        // Show words before sorting
        Console.WriteLine("Here is the list of words:");
        foreach (var word in words)
        {
            Console.WriteLine($"- {word}");
        }

        Console.Write("\nWould you like to sort the list? (y + Enter): ");
        string input = Console.ReadLine()?.Trim().ToLower();

        if (input == "y")
        {
            CatWalkAnimation(); // Run animation first
            Console.WriteLine("\nThe cat walked on the keyboard and typed nonsense:\n");

            // Create scrambled words and sorting scores
            var scrambledWords = words.Select(w => new
            {
                Original = w,
                Scrambled = ScrambleWord(w),
                Score = GetSortScore(w)
            }).ToList();

            // Show what the cat "typed" and their scores
            Console.WriteLine($"{"Original",-15} {"Scrambled",-15} {"Score",-10}");
            Console.WriteLine(new string('-', 45));

            foreach (var item in scrambledWords)
            {
                Console.WriteLine($"{item.Original,-15} {item.Scrambled,-15} {item.Score,-10}");
            }

            Console.WriteLine("\nSorting the nonsense...\n");

            // Sort based on the generated score
            var sortedWords = scrambledWords.OrderBy(w => w.Score).ToList();

            // Print sorted results
            Console.WriteLine($"{"Sort Order",-10} {"Original",-15} {"Scrambled",-15} {"Score",-10}");
            Console.WriteLine(new string('-', 55));

            int rank = 1;
            foreach (var item in sortedWords)
            {
                Console.WriteLine($"{rank++,-10} {item.Original,-15} {item.Scrambled,-15} {item.Score,-10}");
            }
        }
        else
        {
            Console.WriteLine("\nOkay, no sorting. Goodbye!");
        }
    }

    static string ScrambleWord(string word)
    {
        Random rand = new Random(word.GetHashCode()); // Ensure repeatability
        char[] letters = word.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            if (rand.Next(0, 3) == 0) // 1 in 3 chance to change a letter
            {
                letters[i] = GetRandomAdjacentChar(letters[i]);
            }
        }
        return new string(letters);
    }

    static char GetRandomAdjacentChar(char c)
    {
        string keyboard = "`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./";
        int index = keyboard.IndexOf(char.ToLower(c));
        if (index == -1) return c; // If not found, return original

        Random rand = new Random();
        int offset = rand.Next(-1, 2); // Choose a nearby key
        int newIndex = Math.Max(0, Math.Min(index + offset, keyboard.Length - 1));
        return keyboard[newIndex];
    }

    static int GetSortScore(string word)
    {
        return word.Sum(c => c); // Sum ASCII values for deterministic score
    }

    static void CatWalkAnimation()
    {
        string[] catFrames = {
            @" /\_/\  
( o.o )  
~(  )~  ",  // Normal
            @" /\_/\  
( -.- )  
~(  )~  ",  // Sleepy
            @" /\_/\  
( >.< )  
~(  )~  ",  // Excited
            @" /\_/\  
( O_O )  
~(  )~  "   // Shocked
        };

        int startX = 5;
        int endX = startX + 15; // Moves back and forth in a 15-char range
        int delay = 100; // Faster movement
        bool movingRight = true;

        for (int cycle = 0; cycle < 4; cycle++) // Move back and forth twice
        {
            for (int step = 0; step <= (endX - startX); step++)
            {
                Console.Clear(); // Clear screen

                int posX = movingRight ? startX + step : endX - step;
                string frame = catFrames[step % catFrames.Length]; // Cycle through cat faces

                // Print cat at new position
                Console.SetCursorPosition(posX, 5);
                Console.WriteLine(frame.Replace("\n", "\n" + new string(' ', posX)));

                Thread.Sleep(delay);
            }
            movingRight = !movingRight; // Reverse direction
        }

        Console.Clear(); // Final clear
    }
}
