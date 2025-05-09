﻿using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace SimRegisPortal.Core.Helpers;

public static class GuidHelper
{
    private static readonly SequentialGuidValueGenerator Generator = new();

    public static Guid Generate()
    {
        return Generator.Next(null!);
    }
}
