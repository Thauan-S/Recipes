using Bogus;
using CommonTestUtilities.Cryptography;
using Tropical.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public static class UserBuilder
    {                   // nomeacao com user e password
        public static (User user, string password) Build() // retorna 2 params , mas devo nomear 
        {
            var passwordEncripter = PasswordEncrypterBuilder.Build();
            var password = new Faker().Internet.Password();
            var user = new Faker<User>()
                .RuleFor(user => user.Id, () => 1)
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, () => passwordEncripter.Encrypt(password));
            return (user, password);
        }

    }
}
