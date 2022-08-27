namespace MoogleEngine;

public class Moogle
{
    public static SearchResult Query(string query, Dictionary<string, double[]> TF, Dictionary<string, double> iDF) {

        // Looking for search operators
        (bool, string[]) nonPresent = operators.nonPresent(query);
        foreach (var word in nonPresent.Item2)
        {
            Console.WriteLine(word);
        }


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

            


            // Search of query
            double queryTF = 0;
            double queryiDF = 0;

            foreach(var word in queryWords)
            {
                // TF of each word in query
                try
                {
                    queryTF += TF[word][i];
                }
                catch (KeyNotFoundException)
                {
                    queryTF += 0;
                }

                // iDF of each word in query
                try
                {
                    queryiDF += iDF[word];
                }
                catch (KeyNotFoundException)
                {
                    queryiDF += 0;
                }
            }


            // OPERATORS ACTIONS

            // ! operator
            if (nonPresent.Item1)
            {
                foreach (var word in nonPresent.Item2)
                {
                    try
                    {
                        queryTF -= TF[word][i];
                    }
                    catch (KeyNotFoundException)
                    {
                        // Do nothing because word desn't exists in data base
                    }
                    try
                    {
                        queryiDF -= iDF[word]; // If word is not in dictionary, its iDF is 0.0001 to be sure that vector's length is not 0
                    }
                    catch (KeyNotFoundException)
                    {
                        // Do nothing because word desn't exists in data base
                    }
                }
            }
            
            //Similarity between query and each txt using cosine similarity
            // The bigger Cos(angule) is best match            
            double TXTvectorLength = Math.Sqrt(Math.Pow(queryTF, 2) + Math.Pow(queryiDF, 2)); //Length of vector of txt
            double anguleCos = (queryTF / TXTvectorLength);

            // If TF of query in text is 0 discard that txt as match
            if(queryTF == 0)
            {
                Match[i].Item1 = 0;   //Score of txt
            }
            else
            {
                Match[i].Item1 = anguleCos; //Score of txt
                validMatches++;
            }

            Match[i].Item2 = filesAdresses[i]; //Adress of txt
        }

        // Results of search are just valid matches
        SearchItem[] items = new SearchItem[validMatches];
        int count = 0;
        
        //In case of not results founded
        if (validMatches == 0)
        {
            SearchItem[] emptySearch = {new SearchItem("No matches founded", "...", 0)};
            return new SearchResult(emptySearch);
        }

        // Fulling items to be returned
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
        
        // Sorting items by Cos(angule)
        var sortedMatches = from item in items orderby item.Score descending select item;
        var results = sortedMatches.ToArray();
            
        return new SearchResult(results, query);
    }

}

    