using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

/// <summary>
/// A class containing a method to find the largest increasing subarray.
/// </summary>
public static class SubarrayFinder
{
    /// <summary>
    /// Finds the subsequence with the largest sum among the consecutively increasing subsequences in a list of integers.
    /// The found subsequence is returned in JSON format.
    /// </summary>
    /// <param name="numbers">The list of integers to analyze.</param>
    /// <returns>A JSON string representing the largest increasing subarray.</returns>
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
        // Handle the edge case of a null or empty list.
        if (numbers == null || numbers.Count == 0)
        {
            return JsonSerializer.Serialize(new List<int>());
        }

        // Initialize variables to track the current and maximum subsequences and their sums.
        List<int> currentSubsequence = new List<int> { numbers[0] };
        List<int> maxSubsequence = new List<int> { numbers[0] };
        long currentSum = numbers[0];
        long maxSum = numbers[0];

        // Iterate through the list starting from the second element.
        for (int i = 1; i < numbers.Count; i++)
        {
            // If the current number is greater than the previous one, it's part of the increasing sequence.
            if (numbers[i] > numbers[i - 1])
            {
                currentSubsequence.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                // The increasing sequence is broken. Compare the current sequence's sum with the maximum sum found so far.
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSubsequence = new List<int>(currentSubsequence); // Create a new list to store the winning sequence.
                }

                // Reset the current sequence and sum for the new subarray.
                currentSubsequence = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

        // After the loop, perform one final check for the last subsequence.
        if (currentSum > maxSum)
        {
            maxSubsequence = currentSubsequence;
        }

        // Serialize the final result list to a JSON string.
        return JsonSerializer.Serialize(maxSubsequence);
    }
}
