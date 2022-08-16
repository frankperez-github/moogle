public class preSearch
{
    // Auxiliar methods
    public static Dictionary<string, string[]> LoadTexts()
    {
        // Addresses of txts (GetFiles returns a string[])
        string[] filesAdresses = Directory.GetFiles("../Content/", "*.txt");
        int TxtQuant = filesAdresses.Length; //Quantity of txts

        // Dictionary to storage all content of txts, each text as an array of "words"
        // and as key the file address
        Dictionary<string, string[]> TXTcontent = new Dictionary<string, string[]>();
        
        for (int i = 0; i < TxtQuant; i++)
        {   
            // Reading each line
            string content = File.ReadAllText(filesAdresses[i]).ToLower();
            // Spliting each line in words
            TXTcontent.Add(filesAdresses[i], SplitInWords(content));
        }      
        return TXTcontent;
    } 
 
    public static string[] SplitInWords(string sentence)
    {
        // Normalizing text
        sentence = sentence.ToLower();
        char[] separators = { ' ', ',', '.', ';', ':'};
        string[] words = sentence.Split(separators,StringSplitOptions.RemoveEmptyEntries);
        
        return words;
    }


    // Principal Methods
    public static Dictionary<string, double[]> TF()
    // This method compute TF of all words in all texts and storage it in a dict  <word, TF values> pairs
    {
        // Here I will storage TF for all words
        // In a dict that contains all words and their TF value for each text
        Dictionary<string, double[]> TF = new Dictionary<string, double[]>();

        // Loading all txts to a dict
        // key: textAdress, value: words in text
        Dictionary<string, string[] > TXTsContent = LoadTexts();

        // List of texts' paths
        string[] filesAdresses = Directory.GetFiles("../Content/", "*.txt");

        // Calculating TF to each word
        int TXTcounter = 0;

        // For each text computing TF to words
        foreach (var text in TXTsContent)
        {
            // Loading array of words of acual txt
            string[] actualWords = TXTsContent[filesAdresses[TXTcounter]];
            int total = actualWords.Length;

            for (int i = 0; i < actualWords.Length; i++)
            {
                // This is an array to storage TF values of a word in each text
                // It is new for each word
                double[] TFs = new double[filesAdresses.Length];

                for (int j = 0; i < actualWords.Length; i++)
                {   
                    // For each repetition in text, word's TF gets incremented
                    if(actualWords[j] == actualWords[i] && j!=i)
                    {
                        TFs[TXTcounter]++;
                    }
                }
                
                // Term Frequency is word's repetiton / total of words in text
                TFs[TXTcounter] = (double)(TFs[TXTcounter]/total);

                TXTcounter++;
                TF.Add(actualWords[i], TFs);
            }
        }
        return TF;
    }
        

     public static Dictionary<string, double[]> iDF()
    // This method compute iDF of all words in all texts, very similar to TF     <word, iDF value> pairs
    {
        Dictionary<string, double[]> iDF = new Dictionary<string, double[]>();

        // Loading all txts to a dict
        // key: textAdress, value: words in text
        Dictionary<string, string[] > TXTsContent = LoadTexts();

        // List of texts' paths
        string[] filesAdresses = Directory.GetFiles("../Content/", "*.txt");

        int TXTcounter = 0;
        long words = 0;
        foreach(var text in TXTsContent)
        {
            string[] textWords = TXTsContent[filesAdresses[TXTcounter]];
            // Counting total of words in data base
            words += textWords.Length;
        }

        // Array of all words in data base
        string[] allWords = new string[words*filesAdresses.Length];



        return iDF;
        
    }


    
}
