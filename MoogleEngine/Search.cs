public class Search
{
    private string[] files = {};
    
    public Dictionary<string, string[]> LoadTexts()
    {
        //Addresses of txts (GetFiles returns a string[])
        string[] filesAdresses = Directory.GetFiles("../Content/", "*.txt");
        this.files = filesAdresses;
        int TxtQuant = filesAdresses.Length;

        //Dictionary to storage all content of txts, each text as an array of "words"
        //and as key the file address
        Dictionary<string, string[]> TXTcontent = new Dictionary<string, string[]>();
        
        for (int i = 0; i < TxtQuant; i++)
        {   
            string content = File.ReadAllText(filesAdresses[i]).ToLower();
            TXTcontent.Add(filesAdresses[i], SplitInWords(content));
        }      
        return TXTcontent;
    } 

    
    public static string[] SplitInWords(string sentence)
    {
        //Normalizing text
        sentence = sentence.ToLower();
        char[] separators = { ' ', ',', '.', ';', ':'};
        string[] words = sentence.Split(separators,StringSplitOptions.RemoveEmptyEntries);
        
        return words;
    }


    public Dictionary<string, int[]> TF()
    //This method compute TF of all words in all texts and storage it in a dict.
    {
        //Here I will storage TF for all words
        Dictionary<string, int[]> tf = new Dictionary<string, int[]>();

        // Loading all txts to a dict
        Dictionary<string, string[] > TXTsContent = LoadTexts();
        //List of texts' paths
        string[] filesAdresses = Directory.GetFiles("../Content/", "*.txt");

        //Calculating TF to each word
        int TXTcounter = 0;
        foreach (var text in TXTsContent)
        {
            int[] TF = new int[files.Length];
            string[] actualWords = TXTsContent[this.files[TXTcounter]];
            for (int i = 0; i < actualWords.Length; i++)
            {
                    for (int j = 0; i < actualWords.Length; i++)
                    {
                        if(actualWords[j] == actualWords[i] && j!=i)
                        {
                            TF[TXTcounter]++;
                        }
                    }
                    TXTcounter++;
                    tf.Add(actualWords[i], TF);
            }
        }
        return tf;
    }
        
}
