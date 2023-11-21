using HangmanDELL.API.Services;

namespace UnitTests.ServicesTests;
public sealed class QueryWordsServiceTests
{
    private readonly QueryWordsService _queryWordsService;

    public QueryWordsServiceTests()
    {
        _queryWordsService = new QueryWordsService();
    }

    [Fact]
    public void GetRandomWord_SuccessfulScenario_ReturnsRandomWord()
    {
        var randomWordResult = _queryWordsService.GetRandomWord();

        Assert.NotNull(randomWordResult);
        Assert.NotEmpty(randomWordResult);
    }
}
