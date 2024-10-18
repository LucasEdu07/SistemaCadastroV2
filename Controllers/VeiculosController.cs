using SistemaCadastro.Filters; // Adicione esta diretiva
using SistemaCadastro.Models;
using SistemaCadastro.Repo;
using SistemaCadastro.Repos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace SistemaCadastro.Controllers
{
    [Autorizacao] // Aplica o filtro de autorização
    public class VeiculosController : Controller
    {
        private VeiculosRepo _repositorio;
        private CombustivelRepo _combustivelRepo;
        private CorRepo _corRepo; // Adicionei o repositório de cores

        // Construtor que inicializa os repositórios
        public VeiculosController()
        {
            _repositorio = new VeiculosRepo(); // Usando o construtor padrão
            _combustivelRepo = new CombustivelRepo(); // Ajuste conforme necessário
            _corRepo = new CorRepo(); // Inicialize o repositório de cores
        }

        public ActionResult ObterVeiculos()
        {
            ModelState.Clear();
            return View(_repositorio.ObterVeiculos());
        }

        [HttpGet]
        public ActionResult IncluirVeiculo()
        {
            var tipoUsuario = Session["TipoUsuario"]?.ToString();
            if (tipoUsuario != "Administrador")
            {
                return new HttpUnauthorizedResult();
            }

            RecarregarDropdowns(); // Recarrega as listas de dropdown
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirVeiculo(Veiculos veiculoObj)
        {
            try
            {
                var tipoUsuario = Session["TipoUsuario"]?.ToString();
                if (tipoUsuario != "Administrador")
                {
                    return new HttpUnauthorizedResult();
                }

                if (ModelState.IsValid)
                {
                    if (_repositorio.IncluirVeiculo(veiculoObj))
                    {
                        ViewBag.Mensagem = "Veículo cadastrado com sucesso!";
                        return RedirectToAction("ObterVeiculos");
                    }
                }

                RecarregarDropdowns();
                return View(veiculoObj);
            }
            catch (SqlException sqlEx)
            {
                ModelState.AddModelError("", "Erro ao cadastrar veículo: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao cadastrar veículo: " + ex.Message);
            }

            RecarregarDropdowns();
            return View(veiculoObj);
        }

        [HttpGet]
        public ActionResult AtualizarVeiculo(int id)
        {
            var tipoUsuario = Session["TipoUsuario"]?.ToString();
            if (tipoUsuario != "Administrador")
            {
                return new HttpUnauthorizedResult();
            }

            var veiculo = _repositorio.ObterVeiculos().Find(t => t.VeiculosId == id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }

            RecarregarDropdowns();
            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizarVeiculo(Veiculos veiculoObj)
        {
            try
            {
                var tipoUsuario = Session["TipoUsuario"]?.ToString();
                if (tipoUsuario != "Administrador")
                {
                    return new HttpUnauthorizedResult();
                }

                if (ModelState.IsValid && _repositorio.AtualizarVeiculo(veiculoObj))
                {
                    ViewBag.Mensagem = "Veículo atualizado com sucesso!";
                    return RedirectToAction("ObterVeiculos");
                }

                ModelState.AddModelError("", "Erro ao atualizar veículo.");
            }
            catch (SqlException sqlEx)
            {
                ModelState.AddModelError("", "Erro ao atualizar veículo: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro: " + ex.Message);
            }

            RecarregarDropdowns();
            return View(veiculoObj);
        }

        public ActionResult ExcluirVeiculo(int id)
        {
            try
            {
                var tipoUsuario = Session["TipoUsuario"]?.ToString();
                if (tipoUsuario != "Administrador")
                {
                    return new HttpUnauthorizedResult();
                }

                if (_repositorio.ExcluirVeiculo(id))
                {
                    ViewBag.Mensagem = "Veículo excluído com sucesso!";
                }
                else
                {
                    ViewBag.Mensagem = "Erro ao excluir veículo.";
                }
            }
            catch (SqlException sqlEx)
            {
                ViewBag.Mensagem = "Erro ao excluir veículo: " + sqlEx.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro: " + ex.Message;
            }

            return RedirectToAction("ObterVeiculos");
        }

        private void RecarregarDropdowns()
        {
            ViewBag.CombustivelList = _combustivelRepo.GetAll().Select(c => new SelectListItem
            {
                Value = c.Nome, // Aqui, você deve usar o ID do combustível
                Text = c.Nome   // O texto mostrado ao usuário
            });

            // Carregar a lista de cores
            ViewBag.CorList = _corRepo.GetAll().Select(c => new SelectListItem
            {
                Value = c.NomeCor, // Use o ID da cor
                Text = c.NomeCor           // O texto mostrado ao usuário
            });

            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "ATIVO", Text = "ATIVO" },
                new SelectListItem { Value = "INATIVO", Text = "INATIVO" }
            };
        }
    }
}
