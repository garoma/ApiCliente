using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Domain.Entities;
using Domain.Interfaces;
using Application.Services;
using Shared.DTOs;

namespace Tests
{
    public class ClienteServiceTests
    {
        private readonly ClienteService _clienteService;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public ClienteServiceTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarListaDeClientes()
        {
            // Arrange
            var clientesEsperados = new List<Cliente>
            {
                new Cliente ("João", "joao@gmail.com"),
                new Cliente ("Maria","joao@gmail.com")
            };

            _clienteRepositoryMock
                .Setup(repo => repo.ObterTodosAsync())
                .ReturnsAsync(clientesEsperados);

            // Act
            var resultado = await _clienteService.ListarAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Equal("João", resultado[0].Nome);
        }

        [Fact]
        public async Task AdicionarAsync_DeveChamarRepositorioComCliente()
        {
            // Arrange
            var novoCliente = new Cliente ("Pedro", "pedro@gmail.com");
            var novoClienteDTO = new ClienteDTO { Nome =  "Pedro", Email = "pedro@gmail.com" };

            _clienteRepositoryMock
                .Setup(repo => repo.AdicionarAsync(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            // Act
            await _clienteService.CriarAsync(novoClienteDTO);

            // Assert
            _clienteRepositoryMock.Verify(repo => repo.AdicionarAsync(It.Is<Cliente>(c => c.Nome == "Pedro")), Times.Once);
        }
    }
}
