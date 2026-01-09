using FCG.Application.DTOs;
using FCG.Application.Services;
using FCG.Domain.Interfaces;
using FCG.Domain.Models;
using FluentAssertions;
using Moq;

namespace FCG.Tests
{
    public class PromocaoServiceTests
    {
        private readonly Mock<IPromocaoRepository> _promocaoRepoMock;
        private readonly Mock<IJogoRepository> _jogoRepoMock;
        private readonly PromocaoService _service;

        public PromocaoServiceTests()
        {
            _promocaoRepoMock = new Mock<IPromocaoRepository>();
            _jogoRepoMock = new Mock<IJogoRepository>();
            _service = new PromocaoService(_promocaoRepoMock.Object, _jogoRepoMock.Object);
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarFalse_QuandoPromocaoNaoExiste()
        {
            _promocaoRepoMock.Setup(r => r.Delete(6)).ReturnsAsync(false);

            var resultado = await _service.DeleteAsync(6);

            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarTrue_QuandoPromocaoExiste()
        {
            _promocaoRepoMock.Setup(r => r.Delete(6)).ReturnsAsync(true);

            var resultado = await _service.DeleteAsync(6);

            resultado.Should().BeTrue();
        }

        [Fact]
        public async Task CreateAsync_DeveLancarExcecao_SeJogoNaoExiste()
        {
            _jogoRepoMock.Setup(r => r.GetById(1)).ReturnsAsync((Jogo)null);

            var dto = new PromocaoDTO { DescontoPercentual = 10 };

            Func<Task> acao = async () => await _service.CreateAsync(1, dto);

            await acao.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Jogo não encontrado");
        }

        [Fact]
        public async Task GetByJogoAsync_DeveRetornarListaDeDTOs()
        {
            var promocoes = new List<Promocao>
        {
            new Promocao { Id = 1, JogoId = 1, Jogo = new Jogo { Titulo = "Jogo A" }, DescontoPercentual = 10, DataInicio = DateTime.Today, DataFim = DateTime.Today.AddDays(5) },
            new Promocao { Id = 2, JogoId = 1, Jogo = new Jogo { Titulo = "Jogo A" }, DescontoPercentual = 20, DataInicio = DateTime.Today, DataFim = DateTime.Today.AddDays(10) }
        };

            _promocaoRepoMock.Setup(r => r.GetByJogoId(1)).ReturnsAsync(promocoes);

            var resultado = await _service.GetByJogoAsync(1);

            resultado.Should().HaveCount(2);
            resultado.First().TituloJogo.Should().Be("Jogo A");
        }
    }

}
