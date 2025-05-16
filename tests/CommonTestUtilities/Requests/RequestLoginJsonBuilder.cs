using Bogus;
using Tropical.Comunication.Requests;

namespace CommonTestUtilities.Requests
{
    public static class RequestLoginJsonBuilder
    {
       
        public static RequestLoginJson Build()
        {//lib bogus
            return new Faker<RequestLoginJson>()
            .RuleFor(r => r.Email, (f) => f.Internet.Email())
            .RuleFor(r => r.Password, (f) => f.Internet.Password());

            
        }
    }
}
