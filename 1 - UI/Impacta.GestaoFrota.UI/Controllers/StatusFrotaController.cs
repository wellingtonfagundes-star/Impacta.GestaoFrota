using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers;

public class StatusFrotaController : Controller
{
    private readonly IStatusFrotaAppService _statusFrotaAppService;

    public StatusFrotaController(IStatusFrotaAppService statusFrotaAppService)
    {
        _statusFrotaAppService = statusFrotaAppService;
    }

    public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
    {
        page = Math.Max(page, 1);
        pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

        var statusFrotas = _statusFrotaAppService.ObterTodos().ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            statusFrotas = statusFrotas
                .Where(status => status.NomeStatus.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                 (status.Descricao?.Contains(search, StringComparison.CurrentCultureIgnoreCase) ?? false))
                .ToList();
        }

        var totalPaginas = Math.Max((int)Math.Ceiling(statusFrotas.Count / (double)pageSize), 1);
        page = Math.Min(page, totalPaginas);

        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalRegistros = statusFrotas.Count;
        ViewBag.TotalPaginas = totalPaginas;
        ViewBag.Search = search;

        return View(statusFrotas.Skip((page - 1) * pageSize).Take(pageSize));
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new StatusFrotaViewModel();
        return IsAjaxRequest() ? PartialView("_CreateModal", viewModel) : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(StatusFrotaViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            if (IsAjaxRequest())
                return Json(new { success = false, message = ObterErrosModelState() });

            return View(viewModel);
        }

        if (_statusFrotaAppService.Adicionar(viewModel))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Status da frota cadastrado com sucesso." });

            TempData["SuccessMessage"] = "Status da frota cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        const string errorMessage = "Não foi possível cadastrar o status da frota.";
        if (IsAjaxRequest())
            return Json(new { success = false, message = errorMessage });

        ModelState.AddModelError(string.Empty, errorMessage);
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var viewModel = _statusFrotaAppService.ObterPorId(id);
        if (viewModel is null)
            return NotFound();

        return IsAjaxRequest() ? PartialView("_EditModal", viewModel) : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, StatusFrotaViewModel viewModel)
    {
        if (id != viewModel.IdStatusFrota)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            if (IsAjaxRequest())
                return Json(new { success = false, message = ObterErrosModelState() });

            return View(viewModel);
        }

        if (_statusFrotaAppService.Atualizar(viewModel))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Status da frota atualizado com sucesso." });

            TempData["SuccessMessage"] = "Status da frota atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        const string errorMessage = "Não foi possível atualizar o status da frota.";
        if (IsAjaxRequest())
            return Json(new { success = false, message = errorMessage });

        ModelState.AddModelError(string.Empty, errorMessage);
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var viewModel = _statusFrotaAppService.ObterPorId(id);
        if (viewModel is null)
            return NotFound();

        return IsAjaxRequest() ? PartialView("_DeleteModal", viewModel) : View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (_statusFrotaAppService.Remover(id))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Status da frota excluído com sucesso." });

            TempData["SuccessMessage"] = "Status da frota removido com sucesso.";
        }
        else
        {
            const string errorMessage = "Não foi possível remover o status da frota. Verifique se ele está sendo utilizado por algum veículo.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMessage });

            TempData["ErrorMessage"] = errorMessage;
        }

        return RedirectToAction(nameof(Index));
    }

    private string ObterErrosModelState()
    {
        return string.Join(", ", ModelState.Values.SelectMany(value => value.Errors).Select(error => error.ErrorMessage));
    }

    private bool IsAjaxRequest()
    {
        return string.Equals(
            Request.Headers["X-Requested-With"].ToString(),
            "XMLHttpRequest",
            StringComparison.OrdinalIgnoreCase);
    }
}
