using Impacta.GestaoFrota.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly IConsultaAppService _consultaAppService;

        public ConsultasController(IConsultaAppService consultaAppService)
        {
            _consultaAppService = consultaAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VeiculosDisponiveis() => View(_consultaAppService.ObterVeiculosDisponiveis());

        public IActionResult VeiculosEmManutencao() => View(_consultaAppService.ObterVeiculosEmManutencao());

        public IActionResult ReservasAprovadas() => View(_consultaAppService.ObterReservasAprovadas());

        public IActionResult QuilometragemAtualFrota() => View(_consultaAppService.ObterQuilometragemAtualFrota());

        public IActionResult ValorTotalGastoPorTipoEvento() => View(_consultaAppService.ObterValorTotalPorTipoEvento());

        public IActionResult BeneficiariosVeiculos() => View(_consultaAppService.ObterBeneficiariosVeiculos());

        public IActionResult RankingClientesComMaisReservas() => View(_consultaAppService.ObterRankingClientesComMaisReservas());
    }
}
