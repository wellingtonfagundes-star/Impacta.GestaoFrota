using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioAppService _funcionarioAppService;

        public FuncionarioController(IFuncionarioAppService funcionarioAppService)
        {
            _funcionarioAppService = funcionarioAppService;
        }

        public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
        {
            page = Math.Max(page, 1);
            pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

            var funcionarios = _funcionarioAppService.ObterTodos()
                .OrderBy(funcionario => funcionario.IdFuncionario)
                .ToList();

            if (!string.IsNullOrWhiteSpace(search))
            {
                funcionarios = funcionarios
                    .Where(funcionario => funcionario.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                          (funcionario.Matricula?.Contains(search, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                                          funcionario.Cpf.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                          funcionario.Cidade.Contains(search, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            var totalPaginas = Math.Max((int)Math.Ceiling(funcionarios.Count / (double)pageSize), 1);
            page = Math.Min(page, totalPaginas);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = funcionarios.Count;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Search = search;

            return View(funcionarios.Skip((page - 1) * pageSize).Take(pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new FuncionarioViewModel();
            return IsAjaxRequest() ? PartialView("_CreateModal", viewModel) : View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FuncionarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = ObterErrosModelState() });

                return View(viewModel);
            }

            if (_funcionarioAppService.Adicionar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Funcionário cadastrado com sucesso." });

                TempData["SuccessMessage"] = "Funcionário cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            const string errorMessage = "Não foi possível cadastrar o funcionário. Verifique se o CPF já está cadastrado.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMessage });

            ModelState.AddModelError(string.Empty, errorMessage);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _funcionarioAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            return IsAjaxRequest() ? PartialView("_EditModal", viewModel) : View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FuncionarioViewModel viewModel)
        {
            if (id != viewModel.IdFuncionario)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = ObterErrosModelState() });

                return View(viewModel);
            }

            if (_funcionarioAppService.Atualizar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Funcionário atualizado com sucesso." });

                TempData["SuccessMessage"] = "Funcionário atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            const string errorMessage = "Não foi possível atualizar o funcionário. Verifique se o CPF já está cadastrado para outro funcionário.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMessage });

            ModelState.AddModelError(string.Empty, errorMessage);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _funcionarioAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            return IsAjaxRequest() ? PartialView("_DeleteModal", viewModel) : View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_funcionarioAppService.Remover(id))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Funcionário excluído com sucesso." });

                TempData["SuccessMessage"] = "Funcionário removido com sucesso.";
            }
            else
            {
                const string errorMessage = "Não foi possível remover o funcionário. Verifique se ele está vinculado a históricos ou reservas.";
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
