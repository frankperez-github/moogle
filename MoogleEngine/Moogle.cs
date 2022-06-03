namespace MoogleEngine;

public static class Moogle
{
    // Load Texts
    // Search method(It search the frequency of each word in a text and in all texts)
    // SearchResult recieves the query and returns (SearchItem[], query)





    public static SearchResult Query(string query) {
        // Modifique este método para responder a la búsqueda

        SearchItem[] items = new SearchItem[7] {
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.9f),
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.5f),
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.1f),
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.9f),
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.5f),
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.1f),
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.9f),
        };

        return new SearchResult(items, query);
    }
    public static bool loadTexts(){

        

        return true;
    }
}