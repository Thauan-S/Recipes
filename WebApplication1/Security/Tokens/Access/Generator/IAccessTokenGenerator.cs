namespace WebApplication1.Security.Tokens.Access.Generator
{
    public interface IAccessTokenGenerator
    {
        public string Generate(Guid userIdentifier);
    }
}
