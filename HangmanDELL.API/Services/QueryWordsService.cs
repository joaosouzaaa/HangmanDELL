using HangmanDELL.API.Interfaces.Services;
using System.Xml;

namespace HangmanDELL.API.Services;

public sealed class QueryWordsService : IQueryWordsService
{
    public string GetRandomWord()
    {
        const string wordListFileName = "words.xml";
        string wordListFilePath = Path.Combine(Directory.GetCurrentDirectory(), wordListFileName);
        var wordList = new List<string>();

        var xmlDocument = new XmlDocument();
        xmlDocument.Load(wordListFilePath);

        XmlNodeList wordNodes = xmlDocument.SelectNodes("//hangman/word_list/word")!;

        foreach (XmlNode node in wordNodes)
        {
            wordList.Add(node.InnerText);
        }

        var random = new Random();
        int randomIndex = random.Next(wordList.Count);

        return wordList[randomIndex];
    }
}
