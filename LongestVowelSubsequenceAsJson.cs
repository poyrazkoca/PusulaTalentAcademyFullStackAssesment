using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class LongestVowelSubsequence
{
    /// <summary>
    /// Finds the longest consecutive vowel subsequence in each word of a given list
    /// and returns the results in a JSON string.
    /// </summary>
    /// <param name="words">A list of strings to process.</param>
    /// <returns>A JSON string containing the word, the longest vowel sequence, and its length.</returns>
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        // Define the set of vowels for easy lookup.
        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

        // Create a list to hold the results for each word.
        var results = new List<object>();

        // Iterate through each word in the input list.
        foreach (var word in words)
        {
            string longestSequence = "";
            string currentSequence = "";

            // Check if the word is not null or empty before processing.
            if (!string.IsNullOrEmpty(word))
            {
                // Iterate through each character in the word.
                foreach (char c in word.ToLower())
                {
                    // Check if the character is a vowel.
                    if (vowels.Contains(c))
                    {
                        // Append the vowel to the current sequence.
                        currentSequence += c;
                    }
                    else
                    {
                        // If the character is not a vowel, the sequence of consecutive vowels is broken.
                        // Check if the current sequence is the longest found so far for this word.
                        if (currentSequence.Length > longestSequence.Length)
                        {
                            longestSequence = currentSequence;
                        }
                        // Reset the current sequence for the next set of vowels.
                        currentSequence = "";
                    }
                }
                
                // After the loop, a final check is needed for sequences ending at the end of the word.
                if (currentSequence.Length > longestSequence.Length)
                {
                    longestSequence = currentSequence;
                }
            }

            // Create an anonymous object for the current word's result.
            var result = new
            {
                word = word,
                sequence = longestSequence,
                length = longestSequence.Length
            };

            // Add the result object to the list.
            results.Add(result);
        }

        // Serialize the list of result objects into a JSON string.
        // Use JsonSerializerOptions for pretty printing if needed, but the default is concise.
        return JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = false });
    }
}
