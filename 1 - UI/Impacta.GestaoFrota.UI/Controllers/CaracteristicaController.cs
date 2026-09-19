using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class CaracteristicaController : Controller
    {
        private readonly ICaracteristicaAppService _caracteristicaAppService;

        public CaracteristicaController(ICaracteristicaAppService caracteristicaAppService)
        {
            _caracteristicaAppService = caracteristicaAppService;
        }

        public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
        {
            page = Math.Max(page, 1);
            pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

            var todas = _caracteristicaAppService.ObterTodos().ToList();

            // Aplicar filtro de pesquisa
            if (!string.IsNullOrWhiteSpace(search))
            {
                todas = todas
                    .Where(c => c.Descricao.Contains(search, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            var totalPaginas = Math.Max((int)Math.Ceiling(todas.Count / (double)pageSize), 1);
            page = Math.Min(page, totalPaginas);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = todas.Count;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Search = search;

            return View(todas.Skip((page - 1) * pageSize).Take(pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (IsAjaxRequest())
                return PartialView("_CreateModal", new CaracteristicaViewModel());

            return View(new CaracteristicaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CaracteristicaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    var errorMessage = string.Join(", ", errors.Select(e => e.ErrorMessage));
                    return Json(new { success = false, message = errorMessage });
                }

                return IsAjaxRequest()
                    ? PartialView("_CreateModal", viewModel)
                    : View(viewModel);
            }

            if (_caracteristicaAppService.Adicionar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Característica cadastrada com sucesso." });

                TempData["SuccessMessage"] = "Característica cadastrada com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            var errorMsg = "Não foi possível cadastrar a característica.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMsg });

            ModelState.AddModelError(string.Empty, errorMsg);
            return IsAjaxRequest()
                ? PartialView("_CreateModal", viewModel)
                : View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _caracteristicaAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            if (IsAjaxRequest())
                return PartialView("_EditModal", viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CaracteristicaViewModel viewModel)
        {
            if (id != viewModel.IdCaracteristica)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    var errorMessage = string.Join(", ", errors.Select(e => e.ErrorMessage));
                    return Json(new { success = false, message = errorMessage });
                }

                return IsAjaxRequest()
                    ? PartialView("_EditModal", viewModel)
                    : View(viewModel);
            }

            if (_caracteristicaAppService.Atualizar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Característica atualizada com sucesso." });

                TempData["SuccessMessage"] = "Característica atualizada com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            var errorMsg = "Não foi possível atualizar a característica.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMsg });

            ModelState.AddModelError(string.Empty, errorMsg);
            return IsAjaxRequest()
                ? PartialView("_EditModal", viewModel)
                : View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _caracteristicaAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            if (IsAjaxRequest())
                return PartialView("_DeleteModal", viewModel);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_caracteristicaAppService.Remover(id))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Característica excluída com sucesso." });

                TempData["SuccessMessage"] = "Característica removida com sucesso.";
            }
            else
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = "Não foi possível excluir a característica." });

                TempData["ErrorMessage"] = "Não foi possível remover a característica.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IsAjaxRequest()
        {
            return string.Equals(
                Request.Headers["X-Requested-With"].ToString(),
                "XMLHttpRequest",
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
