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
        int totalTXTs = TXTsContent.Count();

        // List of texts' paths
        string[] filesAdresses = Directory.GetFiles("../Content/", "*.txt");


        // For each text computing TF to words
        for (int t = 0; t < totalTXTs; t++)
        {
            // Loading array of words of acual txt
            string[] actualWords = TXTsContent[filesAdresses[t]];
            
            // Fulling TF dict
            for (int i = 0; i < actualWords.Length; i++)
            {   
                // TF for actual word in all texts
                double[] TFs = new double[filesAdresses.Length];

                // If word already exists, just add 1 to TF, else add it to dict
                if (TF.ContainsKey(actualWords[i].ToLower()))
                {
                    TF[actualWords[i]][t] += (double)(1.00 / (double)actualWords.Length);
                }
                else
                {
                    TF.Add(actualWords[i].ToLower(), TFs);
                    TF[actualWords[i]][t] += (double)(1.00 / (double)actualWords.Length);
                }
            }
        }   
        return TF;
    }
        

    public static Dictionary<string, double> iDF(Dictionary<string, double[]> TF)
    {
        // Dictionary to storage iDF value of each word
        Dictionary<string, double> iDF = new Dictionary<string, double>();

        // For each word in TF dict if it has a TF value then appears in that txt
        for(int i = 0; i < TF.Count; i++)
        {
            // Storing TF values array of each word in TF dictionary
            double[] tf = TF[TF.ElementAt(i).Key];

            // For each text, if word have a TF value for this txt, add 1 to iDF
            for (int j = 0; j < tf.Length; j++)
            {
                if (tf[j] != 0)
                {
                    if (iDF.ContainsKey(TF.ElementAt(i).Key))
                    {
                        iDF[TF.ElementAt(i).Key]++;
                    }
                    else
                    {
                        iDF.Add(TF.ElementAt(i).Key, 1);
                    }
                }
                else //If has 0 TF in actual txt create that word in iDF if it doesn't exists
                {
                    if (!iDF.ContainsKey(TF.ElementAt(i).Key))
                    {
                        iDF.Add(TF.ElementAt(i).Key, 0);
                    }
                }
            }
            // Console.WriteLine(iDF[TF.ElementAt(i).Key]);
        }

        return iDF;   
    }


    
}
