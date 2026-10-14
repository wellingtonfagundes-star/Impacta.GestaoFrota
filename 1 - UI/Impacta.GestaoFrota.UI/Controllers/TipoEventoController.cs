using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers;

public class TipoEventoController : Controller
{
    private readonly ITipoEventoAppService _tipoEventoAppService;

    public TipoEventoController(ITipoEventoAppService tipoEventoAppService)
    {
        _tipoEventoAppService = tipoEventoAppService;
    }

    public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
    {
        page = Math.Max(page, 1);
        pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

        var tiposEvento = _tipoEventoAppService.ObterTodos().ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            tiposEvento = tiposEvento
                .Where(tipo => tipo.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                               tipo.Descricao.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                               (tipo.InfoAdicional?.Contains(search, StringComparison.CurrentCultureIgnoreCase) ?? false))
                .ToList();
        }

        var totalPaginas = Math.Max((int)Math.Ceiling(tiposEvento.Count / (double)pageSize), 1);
        page = Math.Min(page, totalPaginas);

        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalRegistros = tiposEvento.Count;
        ViewBag.TotalPaginas = totalPaginas;
        ViewBag.Search = search;

        return View(tiposEvento.Skip((page - 1) * pageSize).Take(pageSize));
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new TipoEventoViewModel();
        return IsAjaxRequest() ? PartialView("_CreateModal", viewModel) : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TipoEventoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            if (IsAjaxRequest())
                return Json(new { success = false, message = ObterErrosModelState() });
            return View(viewModel);
        }

        if (_tipoEventoAppService.Adicionar(viewModel))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Tipo de evento cadastrado com sucesso." });

            TempData["SuccessMessage"] = "Tipo de evento cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        const string errorMessage = "Não foi possível cadastrar o tipo de evento.";
        if (IsAjaxRequest())
            return Json(new { success = false, message = errorMessage });

        ModelState.AddModelError(string.Empty, errorMessage);
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var viewModel = _tipoEventoAppService.ObterPorId(id);
        if (viewModel is null)
            return NotFound();

        return IsAjaxRequest() ? PartialView("_EditModal", viewModel) : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, TipoEventoViewModel viewModel)
    {
        if (id != viewModel.IdTipoevento)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            if (IsAjaxRequest())
                return Json(new { success = false, message = ObterErrosModelState() });
            return View(viewModel);
        }

        if (_tipoEventoAppService.Atualizar(viewModel))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Tipo de evento atualizado com sucesso." });

            TempData["SuccessMessage"] = "Tipo de evento atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        const string errorMessage = "Não foi possível atualizar o tipo de evento.";
        if (IsAjaxRequest())
            return Json(new { success = false, message = errorMessage });

        ModelState.AddModelError(string.Empty, errorMessage);
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var viewModel = _tipoEventoAppService.ObterPorId(id);
        if (viewModel is null)
            return NotFound();

        return IsAjaxRequest() ? PartialView("_DeleteModal", viewModel) : View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (_tipoEventoAppService.Remover(id))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Tipo de evento excluído com sucesso." });

            TempData["SuccessMessage"] = "Tipo de evento removido com sucesso.";
        }
        else
        {
            const string errorMessage = "Não foi possível excluir o tipo de evento. Verifique se existem históricos vinculados a ele.";
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
