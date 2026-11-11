using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class ReservaVeiculoController : Controller
    {
        private readonly IReservaVeiculoAppService _appService;

        public ReservaVeiculoController(IReservaVeiculoAppService appService)
        {
            _appService = appService;
        }

        public IActionResult Index()
        {
            return View(_appService.ObterTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopularListas();
            return View(new ReservaVeiculoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReservaVeiculoViewModel viewModel)
        {
            viewModel.IdReserva = 0;
            ValidarReserva(viewModel);
            if (!ModelState.IsValid)
            {
                PopularListas(viewModel);
                return View(viewModel);
            }

            if (_appService.Adicionar(viewModel))
            {
                TempData["SuccessMessage"] = "Reserva de veículo cadastrada com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Não foi possível cadastrar a reserva. Verifique as opções selecionadas.");
            PopularListas(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _appService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            PopularListas(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReservaVeiculoViewModel viewModel)
        {
            if (id != viewModel.IdReserva)
                return BadRequest();

            ValidarReserva(viewModel);
            ValidarDevolucao(viewModel);
            if (!ModelState.IsValid)
            {
                PopularListas(viewModel);
                return View(viewModel);
            }

            if (_appService.Atualizar(viewModel))
            {
                TempData["SuccessMessage"] = "Reserva de veículo atualizada com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Não foi possível atualizar a reserva. Verifique as opções e os dados da devolução.");
            PopularListas(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _appService.ObterPorId(id);
            return viewModel is null ? NotFound() : View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_appService.Remover(id))
            {
                TempData["SuccessMessage"] = "Reserva de veículo excluída com sucesso.";
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível excluir a reserva de veículo.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopularListas(ReservaVeiculoViewModel? viewModel = null)
        {
            ViewBag.Veiculos = CriarLista(_appService.ObterVeiculos());
            ViewBag.Locais = CriarLista(_appService.ObterLocais());
            ViewBag.Pessoas = CriarLista(_appService.ObterPessoas());
            ViewBag.Funcionarios = CriarLista(_appService.ObterFuncionarios());

            if (viewModel is not null)
                viewModel.LocalRetornoNome = _appService.ObterLocais()
                    .SingleOrDefault(item => item.Id == viewModel.IdLocalRetorno)?.Nome ?? string.Empty;
        }

        private static IEnumerable<SelectListItem> CriarLista(IEnumerable<ReservaVeiculoOpcaoViewModel> opcoes)
        {
            return opcoes.Select(item => new SelectListItem(item.Nome, item.Id.ToString()));
        }

        private void ValidarReserva(ReservaVeiculoViewModel viewModel)
        {
            if (viewModel.KmEstimada < 0)
                ModelState.AddModelError(nameof(viewModel.KmEstimada), "A quilometragem estimada não pode ser negativa.");
        }

        private void ValidarDevolucao(ReservaVeiculoViewModel viewModel)
        {
            if (!viewModel.DataRetorno.HasValue)
            {
                viewModel.IdLocalRetorno = null;
                viewModel.KmEntregue = null;
                viewModel.Combustivel = null;
                viewModel.Lataria = null;
                viewModel.Pneus = null;
                viewModel.ObsRetorno = null;
                return;
            }

            if (!viewModel.IdLocalRetorno.HasValue || viewModel.IdLocalRetorno <= 0)
                ModelState.AddModelError(nameof(viewModel.IdLocalRetorno), "Selecione o local de retorno.");
            if (!viewModel.KmEntregue.HasValue || viewModel.KmEntregue < 0)
                ModelState.AddModelError(nameof(viewModel.KmEntregue), "Informe a quilometragem entregue.");
            if (string.IsNullOrWhiteSpace(viewModel.Combustivel))
                ModelState.AddModelError(nameof(viewModel.Combustivel), "Informe o combustível.");
            if (string.IsNullOrWhiteSpace(viewModel.Lataria))
                ModelState.AddModelError(nameof(viewModel.Lataria), "Informe a condição da lataria.");
            if (string.IsNullOrWhiteSpace(viewModel.Pneus))
                ModelState.AddModelError(nameof(viewModel.Pneus), "Informe a condição dos pneus.");
        }
    }
}
