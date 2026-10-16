using Microsoft.AspNetCore.Mvc;

using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class ClienteController : Controller
    {
        private static readonly string[] EstadosBrasileiros =
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG",
            "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        private readonly IClienteAppService _clienteAppService;

        public ClienteController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
        {
            page = Math.Max(page, 1);
            pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

            var clientes = _clienteAppService.ObterTodos()
                .OrderBy(cliente => cliente.IdPessoa)
                .ToList();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var termo = search.Trim();
                var documento = new string(termo.Where(char.IsAsciiDigit).ToArray());
                clientes = clientes
                    .Where(cliente => cliente.Nome.Contains(termo, StringComparison.CurrentCultureIgnoreCase) ||
                                      cliente.Cidade.Contains(termo, StringComparison.CurrentCultureIgnoreCase) ||
                                      cliente.Estado.Contains(termo, StringComparison.CurrentCultureIgnoreCase) ||
                                      (documento.Length > 0 && cliente.Documento.Contains(documento, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            var totalPaginas = Math.Max((int)Math.Ceiling(clientes.Count / (double)pageSize), 1);
            page = Math.Min(page, totalPaginas);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = clientes.Count;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Search = search;

            return View(clientes.Skip((page - 1) * pageSize).Take(pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ClienteViewModel();
            PrepararListas();
            return IsAjaxRequest() ? PartialView("_CreateModal", viewModel) : PartialView("_CreateModal", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClienteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = ObterErrosModelState() });

                PrepararListas();
                return PartialView("_CreateModal", viewModel);
            }

            if (_clienteAppService.Adicionar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Cliente cadastrado com sucesso." });

                TempData["SuccessMessage"] = "Cliente cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            const string errorMessage = "Não foi possível cadastrar o cliente. Verifique se o CPF ou CNPJ já está cadastrado.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMessage });

            ModelState.AddModelError(string.Empty, errorMessage);
            PrepararListas();
            return PartialView("_CreateModal", viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _clienteAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            PrepararListas();
            return IsAjaxRequest() ? PartialView("_EditModal", viewModel) : PartialView("_EditModal", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClienteViewModel viewModel)
        {
            if (id != viewModel.IdPessoa)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = ObterErrosModelState() });

                PrepararListas();
                return PartialView("_EditModal", viewModel);
            }

            if (_clienteAppService.Atualizar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Cliente atualizado com sucesso." });

                TempData["SuccessMessage"] = "Cliente atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            const string errorMessage = "Não foi possível atualizar o cliente. Verifique se o CPF ou CNPJ já está cadastrado para outro cliente.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMessage });

            ModelState.AddModelError(string.Empty, errorMessage);
            PrepararListas();
            return PartialView("_EditModal", viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _clienteAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            return PartialView("_DeleteModal", viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_clienteAppService.Remover(id))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Cliente excluído com sucesso." });

                TempData["SuccessMessage"] = "Cliente removido com sucesso.";
            }
            else
            {
                const string errorMessage = "Não foi possível remover o cliente. Verifique se ele está vinculado a históricos, reservas ou benefícios.";
                if (IsAjaxRequest())
                    return Json(new { success = false, message = errorMessage });

                TempData["ErrorMessage"] = errorMessage;
            }

            return RedirectToAction(nameof(Index));
        }

        private void PrepararListas()
        {
            ViewBag.Estados = EstadosBrasileiros.Select(estado => new SelectListItem(estado, estado));
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
