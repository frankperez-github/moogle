namespace MoogleEngine;

public static class Moogle
{

    public static SearchResult Query(string query) {

        LoadTexts();
        string[] queryWords = SplitSentences(query);

        //Hacer un metodo que separe el query en palabras y busque en la matriz contentMatrix
        //Ese metodo obtiene resultados que le pasa a items(el array de aqui abajo):

        SearchItem[] items = new SearchItem[] {

            new SearchItem(queryWords[0], "Lorem ipsum dolor sit amet", 0.9f),
            new SearchItem(queryWords[1], "Lorem ipsum dolor sit amet", 0.9f),
            new SearchItem(queryWords[2], "Lorem ipsum dolor sit amet", 0.9f),
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
            string content = File.ReadAllText(filesAddress[i]).ToLower();
            contentMatrix[i] = content;
        }
    } 

          public static string[] SplitSentences(string sentence)
        {
            sentence = sentence.ToLower();
            //Queda pendiente quitar espacios al final y al inicio del query
            string[] words;
            int wordsQuant = 0;
            int countWords = 0;
            int start = 0;
            int end = 0;

            //Counting total of words
            for(int i = 0; i < sentence.Length; i++)
            {
                if(sentence[i] == ' ' || i == sentence.Length-1)
                {
                    wordsQuant++;
                }
            }
            words = new string[wordsQuant];

            //Walking trought the sentence if there is space then
            //until there, is a word
            for(int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == ' ' || sentence[i] == '.' || sentence[i] == ',' || sentence[i] == ';' || i == sentence.Length-1)
                {   
                    end = i;
                    if(i == sentence.Length-1)
                    {
                        end = i+1;
                    }
                    char[] newWord = new char[end-start];
                    int tmpcount = 0;
                    
                    for(int j = start; j < end; j++)
                    {
                        newWord[tmpcount] = sentence[j];
                        tmpcount++;
                    }

                    if (countWords == wordsQuant)
                    {
                        break;
                    }
                    words[countWords] = string.Join("", newWord);
                    countWords++;
                    start = i+1;
                }
            }  

            return words;
        }
}

    