namespace MoogleEngine;

public class Moogle
{
    public static SearchResult Query(string query) {

        string[] queryWords = Search.SplitInWords(query);

        SearchItem[] items = new SearchItem[queryWords.Length];
        int count = 0;
        foreach (var word in queryWords)
        {
            items[count] = new SearchItem(queryWords[count], "Lorem ipsum dolor sit amet", 0.9f);
            count++;
        }
            
        return new SearchResult(items, query);
    }

}

    