using HangmanDELL.API.Entities;
using HangmanDELL.API.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class GuessMapperTests
{
    private readonly GuessMapper _guessMapper;

    public GuessMapperTests()
    {
        _guessMapper = new GuessMapper();
    }

    [Fact]
    public void DomainListToResponseList_SucessfulScenario()
    {
        // A
        var guessList = new List<Guess>()
        {
            GuessBuilder.NewObject().DomainBuild(),
            GuessBuilder.NewObject().DomainBuild(),
            GuessBuilder.NewObject().DomainBuild()
        };

        // A
        var guessResponseListResult = _guessMapper.DomainListToResponseList(guessList);

        // A
        Assert.Equal(guessResponseListResult.Count, guessList.Count);
    }
}
