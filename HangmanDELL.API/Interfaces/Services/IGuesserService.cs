﻿using HangmanDELL.API.Arguments;
using HangmanDELL.API.DataTransferObjects.History;

namespace HangmanDELL.API.Interfaces.Services;

public interface IGuesserService
{
    Task<HistoryResponse?> GuessWordAsync(GuessWordArgument guessWord);
}
