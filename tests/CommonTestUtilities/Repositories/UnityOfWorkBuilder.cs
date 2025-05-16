using Moq;
using Tropical.Domain.Repositories;

namespace CommonTestUtilities.Repositories
{
    public static class UnityOfWorkBuilder
    {
        public static IUnityOfWork Build()
        {
            var mock= new Mock<IUnityOfWork>();// criando o mock do repositório
            return mock.Object; // retornando o mock do repositório
        }
    }
}
