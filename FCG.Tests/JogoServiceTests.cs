using Moq;
using FluentAssertions;
using FCG.Application.Services;
using FCG.Application.DTOs;
using FCG.Domain.Interfaces;
using FCG.Domain.Models;
using FCG.Application.Interfaces.Mappers;

namespace FCG.Tests
{
    public class JogoServiceTests
    {
        private readonly Mock<IJogoRepository> _jogoRepoMock;
        private readonly Mock<IJogoMapper> _jogoMapperMock;
        private readonly JogoService _service;

        public JogoServiceTests()
        {
            _jogoRepoMock = new Mock<IJogoRepository>();
            _jogoMapperMock = new Mock<IJogoMapper>();
            _service = new JogoService(_jogoRepoMock.Object, _jogoMapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_DeveLancarExcecao_SeJogoNaoExiste()
        {
            _jogoRepoMock.Setup(r => r.GetById(1)).ReturnsAsync((Jogo)null);

            Func<Task> acao = async () => await _service.GetByIdAsync(1);

            await acao.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Jogo não encontrado");
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarDTO_SeJogoExiste()
        {
            var jogo = new Jogo { Id = 1, Titulo = "Jogo X" };
            var dto = new JogoDTO { Id = 1, Titulo = "Jogo X" };

            _jogoRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(jogo);
            _jogoMapperMock.Setup(m => m.ToDto(jogo)).Returns(dto);

            var resultado = await _service.GetByIdAsync(1);

            resultado.Should().NotBeNull();
            resultado.Titulo.Should().Be("Jogo X");
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarListaDeDTOs()
        {
            var jogos = new List<Jogo>
            {
                new Jogo { Id = 1, Titulo = "A" },
                new Jogo { Id = 2, Titulo = "B" }
            };

            _jogoRepoMock.Setup(r => r.GetAll()).ReturnsAsync(jogos);
            _jogoMapperMock.Setup(m => m.ToDto(It.IsAny<Jogo>()))
                .Returns<Jogo>(j => new JogoDTO { Id = j.Id, Titulo = j.Titulo });

            var resultado = await _service.GetAllAsync();

            resultado.Should().HaveCount(2);
            resultado.First().Titulo.Should().Be("A");
        }

        [Fact]
        public async Task CreateAsync_DeveChamarAddComEntidadeConvertida()
        {
            var dto = new JogoDTO { Titulo = "Novo Jogo" };
            var jogo = new Jogo { Titulo = "Novo Jogo" };

            _jogoMapperMock.Setup(m => m.ToEntity(dto)).Returns(jogo);

            await _service.CreateAsync(dto);

            _jogoRepoMock.Verify(r => r.Add(jogo), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarExcecao_SeJogoNaoExiste()
        {
            _jogoRepoMock.Setup(r => r.GetById(1)).ReturnsAsync((Jogo)null);

            var dto = new JogoDTO { Id = 1, Titulo = "Atualizado" };

            Func<Task> acao = async () => await _service.UpdateAsync(dto);

            await acao.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Jogo não encontrado");
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarCampos_SeJogoExiste()
        {
            var dto = new JogoDTO
            {
                Id = 1,
                Titulo = "Novo Título",
                Descricao = "Nova descrição",
                Genero = "Ação",
                Preco = 99.99m,
                DataLancamento = DateTime.Today
            };

            var jogo = new Jogo { Id = 1 };

            _jogoRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(jogo);

            await _service.UpdateAsync(dto);

            jogo.Titulo.Should().Be("Novo Título");
            jogo.Descricao.Should().Be("Nova descrição");
            jogo.Genero.Should().Be("Ação");
            jogo.Preco.Should().Be(99.99m);
            jogo.DataLancamento.Should().Be(DateTime.Today);
            jogo.AtualizadoEm.Should().NotBeNull();

            _jogoRepoMock.Verify(r => r.Update(jogo), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarFalse_SeJogoNaoExiste()
        {
            _jogoRepoMock.Setup(r => r.GetById(1)).ReturnsAsync((Jogo)null);

            var resultado = await _service.DeleteAsync(1);

            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarTrue_SeJogoExiste()
        {
            var jogo = new Jogo { Id = 1 };
            _jogoRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(jogo);
            _jogoRepoMock.Setup(r => r.Delete(1)).ReturnsAsync(true);

            var resultado = await _service.DeleteAsync(1);

            resultado.Should().BeTrue();
        }
    }
}