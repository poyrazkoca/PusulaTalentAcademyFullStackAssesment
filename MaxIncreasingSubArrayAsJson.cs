using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

/// <summary>
/// En büyük artan alt diziyi bulan metodu içeren sınıf.
/// </summary>
public static class SubarrayFinder
{
    /// <summary>
    /// Bir tamsayı listesindeki ardışık artan alt dizilerden toplamı en büyük olanı bulur.
    /// Bulunan alt dizi JSON formatında döndürülür.
    /// </summary>
    /// <param name="numbers">Analiz edilecek tamsayı listesi.</param>
    /// <returns>En büyük artan alt diziyi JSON formatında döndüren string.</returns>
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
    // Null veya boş liste durumunu kontrol et.
        if (numbers == null || numbers.Count == 0)
        {
            return JsonSerializer.Serialize(new List<int>());
        }

    // Mevcut ve maksimum alt dizileri ve bunların toplamlarını takip etmek için değişkenleri başlat.
        List<int> currentSubsequence = new List<int> { numbers[0] };
        List<int> maxSubsequence = new List<int> { numbers[0] };
        long currentSum = numbers[0];
        long maxSum = numbers[0];

    // Listenin ikinci elemanından başlayarak döngüye gir.
        for (int i = 1; i < numbers.Count; i++)
        {
            // Eğer mevcut sayı bir önceki sayıdan büyükse, artan dizinin parçasıdır.
            if (numbers[i] > numbers[i - 1])
            {
                currentSubsequence.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                // Artan dizi bozuldu. Mevcut dizinin toplamını şimdiye kadar bulunan maksimum toplam ile karşılaştır.
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSubsequence = new List<int>(currentSubsequence); // Create a new list to store the winning sequence.
                }

                // Yeni alt dizi için mevcut dizi ve toplamı sıfırla.
                currentSubsequence = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

    // Döngüden sonra, son alt dizi için son bir kontrol yap.
        if (currentSum > maxSum)
        {
            maxSubsequence = currentSubsequence;
        }

    // Sonuç listesini JSON formatında string'e çevir.
        return JsonSerializer.Serialize(maxSubsequence);
    }
}
