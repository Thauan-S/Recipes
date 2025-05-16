//using Microsoft.EntityFrameworkCore;
//using Moq;
//using WebApplication1.Context;
//using WebApplication1.Dto;
//using WebApplication1.Models;
//using WebApplication1.Services.Client;
//using Xunit;

//namespace WebApplication1.Tests.ServicesTest
//{
//    public class ClientServiceTests
//    {
//        private readonly Mock<AppDbContext> _mockContext;
//        private readonly ClientService _clientService;

//        public ClientServiceTests()
//        {
//            // Mockar o DbSet de Clients
//            var mockDbSet = new Mock<DbSet<ClientModel>>();
//            _mockContext = new Mock<AppDbContext>();
//            _mockContext.Setup(m => m.Clients).Returns(mockDbSet.Object);

//            // Inicializar o serviço
//            _clientService = new ClientService(_mockContext.Object);
//        }

//        [Fact]
//        public  void Create_ShouldCreateClient_WhenValidDto()
//        {
//            // Arrange
//            var clientDto = new ClientDto
//            {
//                Id = Guid.NewGuid(),
//                Name = "John Doe",
//                Email = "john.doe@example.com",
//                Password = "password123",
//                Phone = "123456789"
//            };

//            // Act
//            var result =  _clientService.Create(clientDto).Result;

//            // Assert
//            Assert.True(result.Status);
//            Assert.Equal("Cliente foi criado com sucesso", result.Message);
//        }
//        [Theory]
//        [InlineData("arara")]
//        [InlineData("osso")]
//        [InlineData("reviver")]
//        public void ShouldVerifyIfStringIsAPalindric(String a)
//        {
//            // Arrange
              

//            // Act
//            var result = a.Reverse().ToString();

//            // Assert

//            Assert.Equal("a", result);
//        }

//        [Fact]
//        public void  Create_ShouldReturnError_WhenExceptionOccurs()
//        {
//            // Arrange
//            var clientDto = new ClientDto
//            {
//                Id = Guid.NewGuid(),
//                Name = "Jane Doe",
//                Email = "jane.doe@example.com",
//                Password = "password456",
//                Phone = "987654321"
//            };

//            // Simular uma exceção durante o processo de salvamento
//            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
//                .ThrowsAsync(new Exception("Database error"));

//            // Act
//            var result =  _clientService.Create(clientDto).Result;

//            // Assert
//            Assert.True (result.Status);
//            Assert.Equal("Database error", result.Message);
//        }
//    }
//}
