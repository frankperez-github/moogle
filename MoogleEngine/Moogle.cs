namespace MoogleEngine;

public class Moogle
{
    public static SearchResult Query(string query) {

        LoadTexts();
        string[] queryWords = SplitInWords(query);

        SearchItem[] items = new SearchItem[queryWords.Length];
        for (var i = 0; i < queryWords.Length; i++)
        {
            items = new SearchItem[] {
            new SearchItem(queryWords[i], "Lorem ipsum dolor sit amet", 0.9f)
        };
        
        }

        return new SearchResult(items, query);
    }

    public static void LoadTexts()
    {
        //Addresses of txts (GetFiles returns a string[])
        string[] filesAddress = Directory.GetFiles("../Content/", "*.txt");
        int TxtQuant = filesAddress.Length;

        //Dict to storage all content of txts, each text in a string[], each word as a string
        //and as key the file address
        Dictionary<string, string[]> TXTcontent = new Dictionary<string, string[]>();

        for (int i = 0; i < TxtQuant; i++)
        {   
            string content = File.ReadAllText(filesAddress[i]).ToLower();
            TXTcontent.Add(filesAddress[i], SplitInWords(content));
        }  
    } 

    public static string[] SplitInWords(string sentence)
    {
        //Normalizing text
        sentence = sentence.ToLower();
        sentence.Trim();

        string[] words;
        int wordsQuant = 0;
        int countWords = 0;
        int start = 0;
        int end = 0;

        //Counting total of words
        for(int i = 0; i < sentence.Length; i++)
        {
            if(sentence[i] == ' ' || sentence[i] == '.' || sentence[i] == ',' || sentence[i] == ';' || i == sentence.Length-1)
            {
                wordsQuant++;
            }
        }
        words = new string[wordsQuant];

        
        for(int i = 0; i < sentence.Length; i++)
        {
            //Spliting text in separeted words
            if (sentence[i] == ' ' || sentence[i] == '.' || sentence[i] == ',' || 
                sentence[i] == ';' || i == sentence.Length-1)
            {   
                
                end = i;//Does not include i position beacuase sentence[i] is a white space

                if(i == sentence.Length-1 && (int)(sentence[i]) >=  97 && (int)(sentence[i]) <=  122)
                {
                    //If last char of query is a letter, then is part of last word
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
                //If sentence[i] is a colon,semicolon... and next char is an empty space
                // next word start in second char from i
                if ( i <= sentence.Length-2)
                {
                    if(sentence[i+1] == ' ')
                    {
                        start = i+2;
                        i++;
                    }
                    
                    if(!char.IsLetter(sentence[i])) //If next char is a letter next word starts in i+1
                    {
                        start = i+1;
                    }
                }
            }
        }  

        return words;
    }
}

    