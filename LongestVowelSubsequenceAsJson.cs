using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class LongestVowelSubsequence
{
    /// <summary>
    /// Verilen bir kelime listesindeki her kelimenin ardışık en uzun sesli harf alt dizisini bulur
    /// ve sonuçları JSON formatında döndürür.
    /// </summary>
    /// <param name="words">İşlenecek kelime listesi.</param>
    /// <returns>Kelime, en uzun sesli harf dizisi ve uzunluğunu içeren JSON string.</returns>
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
    // Sesli harfleri kolayca kontrol etmek için küme olarak tanımla.
        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

    // Her kelime için sonuçları tutacak listeyi oluştur.
        var results = new List<object>();

    // Girdi listesindeki her kelimeyi sırayla işle.
        foreach (var word in words)
        {
            string longestSequence = "";
            string currentSequence = "";

            // Kelime null veya boş değilse işle.
            if (!string.IsNullOrEmpty(word))
            {
                // Kelimedeki her karakteri sırayla işle.
                foreach (char c in word.ToLower())
                {
                    // Karakter sesli harf mi kontrol et.
                    if (vowels.Contains(c))
                    {
                        // Sesli harfi mevcut diziye ekle.
                        currentSequence += c;
                    }
                    else
                    {
                        // Karakter sesli harf değilse, ardışık sesli harf dizisi bozuldu.
                        // Mevcut dizi, şimdiye kadar bulunan en uzun dizi mi kontrol et.
                        if (currentSequence.Length > longestSequence.Length)
                        {
                            longestSequence = currentSequence;
                        }
                        // Sonraki sesli harfler için mevcut diziyi sıfırla.
                        currentSequence = "";
                    }
                }
                
                // Döngüden sonra, kelimenin sonunda biten diziler için son bir kontrol yap.
                if (currentSequence.Length > longestSequence.Length)
                {
                    longestSequence = currentSequence;
                }
            }

            // Mevcut kelimenin sonucu için anonim nesne oluştur.
            var result = new
            {
                word = word,
                sequence = longestSequence,
                length = longestSequence.Length
            };

            // Sonuç nesnesini listeye ekle.
            results.Add(result);
        }

    // Sonuç nesneleri listesini JSON string'e çevir.
        return JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = false });
    }
}
