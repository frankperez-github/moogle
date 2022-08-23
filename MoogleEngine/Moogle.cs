namespace MoogleEngine;

public class Moogle
{
    public static SearchResult Query(string query, Dictionary<string, double[]> TF, Dictionary<string, double> iDF) {

        // Proccesing query
        string[] queryWords = preSearch.SplitInWords(query);

        // Texts in Database
        string[] filesAdresses = Directory.GetFiles("../Content/", "*.txt");

        // Array for txt's values for similarity and its adress
        (double, string)[] Match = new (double, string)[filesAdresses.Length];

        // Real matches
        int validMatches = 0;

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

            // The lowest angule is best match

            // If TF of query in text is 0 discard that txt as match
            
            if(queryTF == 0)
            {
                Match[i].Item1 = 0;   
            }
            else
            {
                Match[i].Item1 = angule;
                validMatches++;
            }
            Match[i].Item2 = filesAdresses[i];
        }

        // Results of search
        SearchItem[] items = new SearchItem[validMatches];
        int count = 0;

        foreach (var txt in Match)
        {
            // txt.Item1 es adress of txt, 
            // txt.Item2 is score of txt

            // Showing all matches except the ones that have 0 as TF for query
            if(txt.Item1 != 0)
            {
                items[count] = new SearchItem(txt.Item2.Split("../Content/")[1], "Lorem ipsum dolor sit amet", txt.Item1);
                count++;
            }
        }

        // Sorting items by angule
        var sortedMatches = from item in items orderby item.Score ascending select item;
        var results = sortedMatches.ToArray();
            
        return new SearchResult(results, query);
    }

}

    