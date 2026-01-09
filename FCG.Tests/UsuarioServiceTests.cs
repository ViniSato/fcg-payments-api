using Moq;
using FluentAssertions;
using FCG.Application.Services;
using FCG.Application.DTOs;
using FCG.Domain.Interfaces;
using FCG.Domain.Models;
using FCG.Application.Interfaces.Mappers;
using FCG.Application.Interfaces.Services.Auth;

namespace FCG.Tests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IUsuarioMapper> _usuarioMapperMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _usuarioRepoMock = new Mock<IUsuarioRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _usuarioMapperMock = new Mock<IUsuarioMapper>();
            _service = new UsuarioService(
                _usuarioRepoMock.Object,
                _passwordHasherMock.Object,
                _usuarioMapperMock.Object
            );
        }

        [Fact]
        public async Task GetUsuarioByIdAsync_DeveLancarExcecao_SeUsuarioNaoExiste()
        {
            _usuarioRepoMock.Setup(r => r.GetById(1)).ReturnsAsync((Usuario)null);

            Func<Task> acao = async () => await _service.GetUsuarioByIdAsync(1);

            await acao.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Usuário não encontrado");
        }

        [Fact]
        public async Task GetUsuarioByIdAsync_DeveRetornarDTO_SeUsuarioExiste()
        {
            var usuario = new Usuario { Id = 1, Nome = "Vinícius" };
            var dto = new UsuarioDTO { Id = 1, Nome = "Vinícius" };

            _usuarioRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(usuario);
            _usuarioMapperMock.Setup(m => m.ToDto(usuario)).Returns(dto);

            var resultado = await _service.GetUsuarioByIdAsync(1);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("Vinícius");
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarListaDeDTOs()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "A" },
                new Usuario { Id = 2, Nome = "B" }
            };

            _usuarioRepoMock.Setup(r => r.GetAll()).ReturnsAsync(usuarios);
            _usuarioMapperMock.Setup(m => m.ToDto(It.IsAny<Usuario>()))
                .Returns<Usuario>(u => new UsuarioDTO { Id = u.Id, Nome = u.Nome });

            var resultado = await _service.GetAllAsync();

            resultado.Should().HaveCount(2);
            resultado.First().Nome.Should().Be("A");
        }

        [Fact]
        public async Task CreateUsuarioAsync_DeveHashSenhaECriarUsuario()
        {
            var dto = new UsuarioDTO { Nome = "Novo", SenhaHash = "123" };
            var usuario = new Usuario { Nome = "Novo", SenhaHash = "hashed" };

            _passwordHasherMock.Setup(h => h.Hash("123")).Returns("hashed");
            _usuarioMapperMock.Setup(m => m.ToEntity(It.Is<UsuarioDTO>(d => d.SenhaHash == "hashed")))
                .Returns(usuario);

            await _service.CreateUsuarioAsync(dto);

            dto.SenhaHash.Should().Be("hashed");
            _usuarioRepoMock.Verify(r => r.Add(usuario), Times.Once);
        }

        [Fact]
        public async Task UpdateUsuarioAsync_DeveAtualizarUsuario_SeExiste()
        {
            var dto = new UsuarioDTO { Id = 1, Nome = "Atualizado", Email = "email", SenhaHash = "nova" };
            var usuario = new Usuario { Id = 1, Nome = "Antigo", Email = "old", SenhaHash = "velha" };

            _usuarioRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(usuario);
            _passwordHasherMock.Setup(h => h.Hash("nova")).Returns("nova-hash");

            await _service.UpdateUsuarioAsync(dto);

            usuario.Nome.Should().Be("Atualizado");
            usuario.Email.Should().Be("email");
            usuario.SenhaHash.Should().Be("nova-hash");
            usuario.AtualizadoEm.Should().NotBeNull();

            _usuarioRepoMock.Verify(r => r.Update(usuario), Times.Once);
        }

        [Fact]
        public async Task DeleteUsuarioAsync_DeveRetornarFalse_SeUsuarioNaoExiste()
        {
            _usuarioRepoMock.Setup(r => r.GetById(1)).ReturnsAsync((Usuario)null);

            var resultado = await _service.DeleteUsuarioAsync(1);

            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteUsuarioAsync_DeveRetornarTrue_SeUsuarioExiste()
        {
            var usuario = new Usuario { Id = 1 };
            _usuarioRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(usuario);
            _usuarioRepoMock.Setup(r => r.Delete(1)).ReturnsAsync(true);

            var resultado = await _service.DeleteUsuarioAsync(1);

            resultado.Should().BeTrue();
        }
    }
}