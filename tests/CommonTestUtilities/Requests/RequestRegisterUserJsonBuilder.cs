using Bogus;
using Tropical.Comunication.Requests;

namespace CommonTestUtilities.Requests
{
    public static class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength) {
            //lib bogus cria um faker<T>
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Name, (f) => f.Person.FullName)
                .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
                
            // devo consultar a documentação para verificar os tipos disponíveis
        }
        public static RequestRegisterUserJson Build()
        {
            //lib bogus cria um faker<T>
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Name, (f) => f.Person.FullName)
                .RuleFor(user => user.Password, (f) => f.Internet.Password(6));
            // devo consultar a documentação para verificar os tipos disponíveis
        }


    }
}
