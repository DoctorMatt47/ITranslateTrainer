﻿namespace ITranslateTrainer.Application.Tests.Responses;

public record GetTestResponse(
    int Id,
    string String,
    IEnumerable<GetOptionResponse> Options);

public record GetOptionResponse(
    int Id,
    string String,
    bool IsChosen,
    bool IsCorrect);