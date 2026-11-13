using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Impacta.GestaoFrota.UI.Controllers;

public class HistoricoManutencaoController : Controller
{
    private readonly IHistoricoManutencaoAppService _appService;

    public HistoricoManutencaoController(IHistoricoManutencaoAppService appService)
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
        return View(new HistoricoManutencaoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(HistoricoManutencaoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            PopularListas();
            return View(viewModel);
        }

        if (_appService.Adicionar(viewModel))
        {
            TempData["SuccessMessage"] = "Histórico de manutenção cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Não foi possível cadastrar o histórico. Verifique as opções selecionadas.");
        PopularListas();
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var viewModel = _appService.ObterPorId(id);
        if (viewModel is null)
            return NotFound();

        PopularListas();
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, HistoricoManutencaoViewModel viewModel)
    {
        if (id != viewModel.IdHistorico)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            PopularListas();
            return View(viewModel);
        }

        if (_appService.Atualizar(viewModel))
        {
            TempData["SuccessMessage"] = "Histórico de manutenção atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Não foi possível atualizar o histórico. Verifique as opções selecionadas.");
        PopularListas();
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
            TempData["SuccessMessage"] = "Histórico de manutenção excluído com sucesso.";
        else
            TempData["ErrorMessage"] = "Não foi possível excluir o histórico de manutenção.";

        return RedirectToAction(nameof(Index));
    }

    private void PopularListas()
    {
        ViewBag.Locais = CriarLista(_appService.ObterLocais());
        ViewBag.TiposEvento = CriarLista(_appService.ObterTiposEvento());
        ViewBag.Funcionarios = CriarLista(_appService.ObterFuncionarios());
        ViewBag.Clientes = CriarLista(_appService.ObterClientes());
    }

    private static IEnumerable<SelectListItem> CriarLista(IEnumerable<HistoricoManutencaoOpcaoViewModel> opcoes)
    {
        return opcoes.Select(item => new SelectListItem(item.Nome, item.Id.ToString()));
    }
}
