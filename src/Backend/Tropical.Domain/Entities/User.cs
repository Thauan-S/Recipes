﻿namespace Tropical.Domain.Entities
{
    public class User:EntityBase
    {
        public Guid UserId { get; set; }=Guid.NewGuid();
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }=string.Empty ;
    }
}
