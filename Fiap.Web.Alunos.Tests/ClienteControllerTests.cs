using AutoMapper;
using Fiap.Web.Alunos.Controllers;
using Fiap.Web.Alunos.Data.Contexts;
using Fiap.Web.Alunos.Models;
using Fiap.Web.Alunos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Fiap.Web.Alunos.Tests
{
    public class ClienteControllerTests
    {
        // Mocks dos serviços e do mapper
        private readonly Mock<IClienteService> _mockClienteService;
        private readonly Mock<IRepresentanteService> _mockRepresentanteService;
        private readonly Mock<IMapper> _mockMapper;

        // Controlador que será testado
        private readonly ClienteController _controller;

        public ClienteControllerTests()
        {
            // Inicializa os mocks
            _mockClienteService = new Mock<IClienteService>();
            _mockRepresentanteService = new Mock<IRepresentanteService>();
            _mockMapper = new Mock<IMapper>();

            // Inicializa o controller com os serviços e mapper mockados
            _controller = new ClienteController(_mockMapper.Object, _mockClienteService.Object, _mockRepresentanteService.Object);
        }

        // Método para criar e configurar um DbSet mock para ClienteModel
        private DbSet<ClienteModel> MockDbSet()
        {
            // Lista de clientes para simular dados no banco de dados
            var data = new List<ClienteModel>
            {
                new ClienteModel { ClienteId = 1, Nome = "Cliente 1" },
                new ClienteModel { ClienteId = 2, Nome = "Cliente 2" }
            }.AsQueryable();

            // Cria o mock do DbSet
            var mockSet = new Mock<DbSet<ClienteModel>>();

            // Configura o comportamento do mock DbSet para simular uma consulta ao banco de dados
            mockSet.As<IQueryable<ClienteModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ClienteModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ClienteModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ClienteModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Retorna o DbSet mock
            return mockSet.Object;
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfClients()
        {
            // Arrange
            var clientes = new List<ClienteModel>
            {
                new ClienteModel { ClienteId = 1, Nome = "Cliente 1" },
                new ClienteModel { ClienteId = 2, Nome = "Cliente 2" }
            };
            _mockClienteService.Setup(s => s.ListarClientes()).Returns(clientes);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ClienteModel>>(viewResult.Model);
            Assert.Equal(2, clientes.Count);
        }


        
        [Fact]
        public void Index_ReturnsEmptyList_WhenNoClientsExist()
        {
            _mockClienteService.Setup(s => s.ListarClientes()).Returns(new List<ClienteModel>());
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ClienteModel>>(viewResult.Model);
            Assert.Empty(model);
        }

        
        [Fact]
        public void Index_ThrowsException_WhenDatabaseFails()
        {
            _mockClienteService.Setup(s => s.ListarClientes()).Throws(new System.Exception("Database error"));
            Assert.Throws<System.Exception>(() => _controller.Index());
        }
        
    }
}