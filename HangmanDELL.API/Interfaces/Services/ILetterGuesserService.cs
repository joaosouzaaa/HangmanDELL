using HangmanDELL.API.Arguments;

namespace HangmanDELL.API.Interfaces.Services;

public interface ILetterGuesserService
{
    RightLetterResultArgument IsRightLetter(RightLetterArgument rightLetter);
}
