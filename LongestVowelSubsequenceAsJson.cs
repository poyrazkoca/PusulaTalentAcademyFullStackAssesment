using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class LongestVowelSubsequence
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
    // sesli harfleri kolayca kontrol etmek için küme olarak tanımladım.
        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

    // her kelime için sonuçları tutacak listeyi oluşturdum.
        var results = new List<object>();

    // girdi listesindeki her kelimeyi sırayla işledim.
        foreach (var word in words)
        {
            string longestSequence = "";
            string currentSequence = "";

            // kelime null veya boş değilse işledim.
            if (!string.IsNullOrEmpty(word))
            {
                // kelimedeki her karakteri sırayla işledim.
                foreach (char c in word.ToLower())
                {
                    // karakter sesli harf mi kontrol ettim.
                    if (vowels.Contains(c))
                    {
                        // sesli harfi mevcut diziye ekledim.
                        currentSequence += c;
                    }
                    else
                    {
                        // karakter sesli harf değilse, ardışık sesli harf dizisini bozdum.
                        // mevcut dizi, şimdiye kadar bulunan en uzun dizi mi kontrol ettim.
                        if (currentSequence.Length > longestSequence.Length)
                        {
                            longestSequence = currentSequence;
                        }
                        // sonraki sesli harfler için mevcut diziyi sıfırladım.
                        currentSequence = "";
                    }
                }
                
                // döngüden sonra, kelimenin sonunda biten diziler için son bir kontrol yaptım.
                if (currentSequence.Length > longestSequence.Length)
                {
                    longestSequence = currentSequence;
                }
            }

            // mevcut kelimenin sonucu için anonim nesne oluşturdum.
            var result = new
            {
                word = word,
                sequence = longestSequence,
                length = longestSequence.Length
            };

            // sonuç nesnesini listeye ekledim.
            results.Add(result);
        }

    // sonuç nesneleri listesini JSON string'e çevirdim.
        return JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = false });
    }
}
