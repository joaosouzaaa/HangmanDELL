using HangmanDELL.API.Arguments;
using HangmanDELL.API.Interfaces.Services;

namespace HangmanDELL.API.Services;

public sealed class LetterGuesserService : ILetterGuesserService
{
    public RightLetterResultArgument IsRightLetter(RightLetterArgument rightLetter)
    {
        var rightLetterResult = new RightLetterResultArgument()
        {
            IsSuccess = false,
        };

        char[] guessedWord = rightLetter.GuessProgress.ToCharArray();

        for (int i = 0; i < rightLetter.WordToGuess.Length; i++)
        {
            if (rightLetter.WordToGuess[i] == rightLetter.Letter)
            {
                guessedWord[i] = rightLetter.Letter;
                rightLetterResult.IsSuccess = true;
            }
        }

        rightLetterResult.GuessedWord = new string(guessedWord);

        return rightLetterResult;
    }
}
