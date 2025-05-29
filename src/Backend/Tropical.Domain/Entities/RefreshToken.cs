namespace Tropical.Domain.Entities
{
    public class RefreshToken:EntityBase
    {   // para ter controle sobre o próprietário daquele token
        //revogar o acesso ao token em diferentes dispositivos
        //
        public required string Value { get; set; } = string.Empty;
        public required long UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
