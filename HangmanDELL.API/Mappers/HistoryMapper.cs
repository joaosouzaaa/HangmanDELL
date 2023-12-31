﻿using HangmanDELL.API.DataTransferObjects.History;
using HangmanDELL.API.Entities;
using HangmanDELL.API.Interfaces.Mappers;

namespace HangmanDELL.API.Mappers;

public sealed class HistoryMapper : IHistoryMapper
{
    private readonly IGuessMapper _guessMapper;

    public HistoryMapper(IGuessMapper guessMapper)
    {
        _guessMapper = guessMapper;
    }

    public HistoryResponse DomainToResponse(History history)
    {
        var guessResponseList = _guessMapper.DomainListToResponseList(history.Guesses);

        return new HistoryResponse()
        {
            Guesses = guessResponseList,
            NumberOfLives = history.NumberOfLives,
            WordProgress = history.WordProgress,
            IsFinalResult = false
        };
    }
}
