namespace MoogleEngine;

public static class Moogle
{
    // Load_Texts();
    // Search method(It search the frequency of each word in a text and in all texts)
    // SearchResult recieves the query and returns (SearchItem[], query)

    public static SearchResult Query(string query) {

        LoadTexts();
        string[] queryWords = SplitSentences(query);

        //Hacer un metodo que separe el query en palabras y busque en la matriz contentMatrix
        //Ese metodo obtiene resultados que le pasa a items(el array de aqui abajo):

        SearchItem[] items = new SearchItem[] {

            new SearchItem(queryWords[0], "Lorem ipsum dolor sit amet", 0.9f),
            new SearchItem(queryWords[1], "Lorem ipsum dolor sit amet", 0.9f),
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

    public static string[] SplitSentences(string sentence)
    {
        int wordsQuant = 1;
        for(int i = 0; i < sentence.Length; i++)
        {
            if ((i != sentence.Length - 1 || i != 0) && (sentence[i] == ' ' || sentence[i] == ';' || sentence[i] == '.' || sentence[i] == ','))
            {
                //This conditional is implemented to be sure that 
                //there are not spaces at the beggining or end
                wordsQuant++;
            }
        }

        string[] words = new string[wordsQuant];
        int wordCount = 0;

        for(int i = 0; i < sentence.Length; i++)
        {   
            char[] currentWord = new char[17];
            currentWord[i] = sentence[i];
            if (sentence[i] == ' ')
            {
                words[wordCount] = new string(currentWord); //Convert char[] to string or change method bye;
                wordCount++;
                i++;
            }
            
        }
        return words;
    }
}

    