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
        
        // For all text in database
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
            
            // Fulling TF dict
            for (int i = 0; i < actualWords.Length; i++)
            {   
                // TF for actual word in all texts
                double[] TFs = new double[filesAdresses.Length];

                // If word already exists, just add 1 to TF, else add it to dict
                if (TF.ContainsKey(actualWords[i].ToLower()))
                {
                    TF[actualWords[i]][TXTcounter]++;
                }
                else
                {
                    TF.Add(actualWords[i].ToLower(), TFs);
                    TF[actualWords[i]][TXTcounter]++;
                }
            }

            // Total of words in this text
            double total = actualWords.Length;

            // Dividing by total of words (This is final TF)
            for (int i = 0; i < actualWords.Length; i++)
            {
                // If TF is very close to 0 its taken as 0
                 TF[actualWords[i]][TXTcounter] /= total;
            }

            TXTcounter++;
        }
        return TF;
    }
        

     public static Dictionary<string, double[]> DF(Dictionary<string, double[]> TF)
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

        // Array of all words in data base
        string[] allWords = new string[words*filesAdresses.Length];

        int wordCounter = 0;
        foreach (var word in TF)
        {   
            string[] txtWords = TXTsContent[filesAdresses[TXTcounter]];

            for (int i = 0; i < txtWords.Length; i++)
            {
                
            }

        



        return iDF;
        
    }


    
}
