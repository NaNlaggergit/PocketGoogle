using Avalonia.Media.TextFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PocketGoogle;
public class Indexer : IIndexer
{
    public Dictionary<string, Dictionary<int, List<int>>> Words = new Dictionary<string, Dictionary<int, List<int>>>();
    public void Add(int id, string documentText)
	{
        MatchCollection matches = Regex.Matches(documentText, @"\b\w");
        var bufferWords=documentText.Split(new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < bufferWords.Length; i++)
		{
            Dictionary<int, List<int>> idWords = new Dictionary<int, List<int>>();
            idWords.Add(id, new List<int>());
			idWords[id].Add(matches[i].Index);
            if (Words.ContainsKey(bufferWords[i]))
			{
				if (Words[bufferWords[i]].ContainsKey(id))
				{
                    Words[bufferWords[i]][id].Add(matches[i].Index);
                    continue;
                }
				Words[bufferWords[i]].Add(id, new List<int>() { matches[i].Index });
				continue;
			}
            Words.Add(bufferWords[i],idWords);
		}
		//throw new NotImplementedException();
	}

	public List<int> GetIds(string word)
	{
		if (!Words.ContainsKey(word))
			return new List<int>();
		List<int> listId = new List<int>(Words[word].Keys);
		return listId;
		//throw new NotImplementedException();
	}

	public List<int> GetPositions(int id, string word)
	{
		if (!(Words.ContainsKey(word) && Words[word].ContainsKey(id)))
            return new List<int>();
        return (Words[word][id]);
        //throw new NotImplementedException();
    }

	public void Remove(int id)
	{
		foreach(KeyValuePair<string, Dictionary<int, List<int>>> kvp in Words)
		{
			if (kvp.Value.ContainsKey(id))
				kvp.Value.Remove(id);
		}
        //throw new NotImplementedException();
    }
}