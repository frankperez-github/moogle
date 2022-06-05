namespace MoogleEngine;

public static class Moogle
{
    // Load_Texts();
    // Search method(It search the frequency of each word in a text and in all texts)
    // SearchResult recieves the query and returns (SearchItem[], query)

    

    public static SearchResult Query(string query) {

        LoadTexts();
        SearchItem[] items = new SearchItem[] {
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.9f),
        };

        return new SearchResult(items, query);
    }

    public static void LoadTexts()
        {
            //Addresses of txts
            string[] filesAddress = Directory.GetFiles("../Content/", "*.txt");
            int TxtQuant = filesAddress.Length;

            //Matrix to storage all content of txts, each one in a string
            string[] contentMatrix = new string[TxtQuant];

            //Content of txts
            for (int i = 0; i < TxtQuant; i++)
            {   
                string content = File.ReadAllText(filesAddress[i]);
                contentMatrix[i] = content;
            }
        } 
}

    