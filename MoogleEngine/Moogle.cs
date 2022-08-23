namespace MoogleEngine;

public class Moogle
{
    public static SearchResult Query(string query, Dictionary<string, double[]> TF, Dictionary<string, double> iDF) {

        // Proccesing query
        string[] queryWords = preSearch.SplitInWords(query);

        // Texts in Database
        string[] filesAdresses = Directory.GetFiles("../Content/", "*.txt");

        // Array for txt's values for similarity
        double[] simil = new double[filesAdresses.Length];

        // Looking best match in all txt
        for(int i = 0; i < filesAdresses.Length; i++)
        {
            double queryTF = 0;
            double queryiDF = 0;
            foreach(var word in queryWords)
            {
                // TF of each word in query
                queryTF += TF[word][i];
                // iDF of each word in query
                queryiDF += iDF[word];
            }
            
            //Similarity between query and each txt using cosine similarity
            double TXTvectorLength = Math.Sqrt(Math.Pow(queryTF, 2) + Math.Pow(queryiDF, 2)); //Length of vector of txt
            double anguleCos = (queryTF + queryiDF) / (Math.Sqrt(2) * TXTvectorLength);
            double angule = Math.Acos(anguleCos); //Radians

            // If TF of query in text is 0 discard that txt as match
            if(queryTF == 0)
            {
                simil[i] = 0;    
            }
            else
            {
                simil[i] = angule;
            }
        }

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

    