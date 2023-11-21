using HangmanDELL.API.Interfaces.Services;
using System.Xml;

namespace HangmanDELL.API.Services;

public sealed class QueryWordsService : IQueryWordsService
{
    public string GetRandomWord()
    {
        var wordList = GetWordListFromXml();

        var random = new Random();
        int randomIndex = random.Next(wordList.Count);

        return wordList[randomIndex];
    }

    private List<string> GetWordListFromXml()
    {
        const string xmlContent = @"
        <hangman>
        	<word_list>
        		<word>DELL</word>
        		<word>DELIVER</word>
        		<word>TECHNOLOGY</word>
        		<word>CUSTOMER</word>
        		<word>CLOUD</word>
        		<word>COMPUTER</word>
        		<word>SERVER</word>
        		<word>ADVANCED</word>
        		<word>STORAGE</word>
        		<word>SOLLUTIONS</word>
        		<word>COMMITMENT</word>
        		<word>DIVERSITY</word>
        		<word>ENGAGEMENT</word>
        		<word>COMMUNITY</word>
        		<word>MILESTONE</word>
        	</word_list>
        </hangman>";

        var wordList = new List<string>();
        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlContent);

        XmlNodeList wordNodes = xmlDocument.SelectNodes("//hangman/word_list/word")!;

        foreach (XmlNode node in wordNodes)
        {
            wordList.Add(node.InnerText);
        }

        return wordList;
    }
}
