using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class LocalAssistenciaController : Controller
    {
        private readonly ILocalAssistenciaAppService _localAssistenciaAppService;

        public LocalAssistenciaController(ILocalAssistenciaAppService localAssistenciaAppService)
        {
            _localAssistenciaAppService = localAssistenciaAppService;
        }

        public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
        {
            page = Math.Max(page, 1);
            pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

            var locais = _localAssistenciaAppService.ObterTodos().ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                locais = locais
                    .Where(local => local.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                    local.Endereco.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                    local.Telefone.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                    local.DescrServico.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                    local.NomeContato.Contains(search, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            var totalPaginas = Math.Max((int)Math.Ceiling(locais.Count / (double)pageSize), 1);
            page = Math.Min(page, totalPaginas);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = locais.Count;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Search = search;

            return View(locais.Skip((page - 1) * pageSize).Take(pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new LocalAssistenciaViewModel();
            return IsAjaxRequest() ? PartialView("_CreateModal", viewModel) : View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LocalAssistenciaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = ObterErrosModelState() });

                return View(viewModel);
            }

            if (_localAssistenciaAppService.Adicionar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Local de assistência cadastrado com sucesso." });

                TempData["SuccessMessage"] = "Local de assistência cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            const string errorMessage = "Não foi possível cadastrar o local de assistência.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMessage });

            ModelState.AddModelError(string.Empty, errorMessage);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _localAssistenciaAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            return IsAjaxRequest() ? PartialView("_EditModal", viewModel) : View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LocalAssistenciaViewModel viewModel)
        {
            if (id != viewModel.IdLocal)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = ObterErrosModelState() });

                return View(viewModel);
            }

            if (_localAssistenciaAppService.Atualizar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Local de assistência atualizado com sucesso." });

                TempData["SuccessMessage"] = "Local de assistência atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            const string errorMessage = "Não foi possível atualizar o local de assistência.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMessage });

            ModelState.AddModelError(string.Empty, errorMessage);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _localAssistenciaAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            return IsAjaxRequest() ? PartialView("_DeleteModal", viewModel) : View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_localAssistenciaAppService.Remover(id))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Local de assistência excluído com sucesso." });

                TempData["SuccessMessage"] = "Local de assistência removido com sucesso.";
            }
            else
            {
                const string errorMessage = "Não foi possível remover o local de assistência. Verifique se ele está vinculado a históricos, reservas ou retornos.";
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
}
