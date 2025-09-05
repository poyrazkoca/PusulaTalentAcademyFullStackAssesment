using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class SubarrayFinder
{
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
    // null veya boş liste durumunu kontrol ettim.
        if (numbers == null || numbers.Count == 0)
        {
            return JsonSerializer.Serialize(new List<int>());
        }

    // mevcut ve maksimum alt dizileri ve bunların toplamlarını takip etmek için değişkenleri başlattım.
        List<int> currentSubsequence = new List<int> { numbers[0] };
        List<int> maxSubsequence = new List<int> { numbers[0] };
        long currentSum = numbers[0];
        long maxSum = numbers[0];

    // listenin ikinci elemanından başlayarak döngüye soktum.
        for (int i = 1; i < numbers.Count; i++)
        {
            // eğer mevcut sayı bir önceki sayıdan büyükse, artan dizinin parçası olarak algılattım.
            if (numbers[i] > numbers[i - 1])
            {
                currentSubsequence.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                // artan dizi bozulunca mevcut dizinin toplamını şimdiye kadar bulunan maksimum toplam ile karşılaştırdım.
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSubsequence = new List<int>(currentSubsequence); // kazanan diziyi saklamak için yeni bir liste oluşturdum.
                }

                // yeni alt dizi için mevcut dizi ve toplamı sıfırladım.
                currentSubsequence = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

    // döngüden sonra, son alt dizi için son bir kontrol yaptım.
        if (currentSum > maxSum)
        {
            maxSubsequence = currentSubsequence;
        }

    // sonuç listesini JSON formatında string'e çevirdim.
        return JsonSerializer.Serialize(maxSubsequence);
    }
}
