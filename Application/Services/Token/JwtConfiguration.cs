﻿namespace Application.Services.Token
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;
    }
}
