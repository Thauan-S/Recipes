﻿namespace Tropical.Domain.Security.Tokens
{
    public interface IRefreshTokenGenerator
    {
        string Generate();
    }
}
