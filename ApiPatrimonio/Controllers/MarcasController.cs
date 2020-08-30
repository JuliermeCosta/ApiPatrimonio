using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApiPatrimonio.Models;
using ApiPatrimonio.Repositorys.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPatrimonio.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IRepository<Patrimonio> _repositoryPatrimonio;
        private readonly IRepository<Marca> _repositoryMarca;

        public MarcasController(IRepository<Patrimonio> repositoryPatrimonio, IRepository<Marca> repositoryMarca)
        {
            _repositoryPatrimonio = repositoryPatrimonio;
            _repositoryMarca = repositoryMarca;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {
                List<Marca> listaMarcas = _repositoryMarca.GetAll();

                if (!listaMarcas.Any())
                {
                    return NoContent();
                }

                return Ok(listaMarcas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId([FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}")] int id)
        {
            try
            {
                Marca marca = _repositoryMarca.GetById(id);

                if (marca == null)
                {
                    return NotFound();
                }

                return Ok(marca);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/patrimonios")]
        public IActionResult ListarPatrimonios([FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}")] int id)
        {
            try
            {
                Marca marca = _repositoryMarca.GetById(id);

                if (marca == null)
                {
                    return NotFound();
                }

                List<Patrimonio> listaPatrimonios = _repositoryPatrimonio.GetAll().Where(x => x.MarcaId == marca.Id).ToList();

                if (!listaPatrimonios.Any())
                {
                    return NoContent();
                }

                return Ok(listaPatrimonios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] Marca marca)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(marca.Nome))
                {
                    return BadRequest("Necessário informar o nome!");
                }
                else if (NomeJaExiste(marca.Nome))
                {
                    return BadRequest("O nome já existe em outra marca!");
                }

                Marca novaMarca = new Marca(marca.Nome);

                _repositoryMarca.Save(novaMarca);

                string location = $"{this.RouteData.Values["controller"]}/{novaMarca.Id}";

                return Created(location, null);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(
            [FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}"), Required(ErrorMessage = "Campo {0} requerido!")] int id,
            [FromBody] Marca marca
            )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(marca.Nome))
                {
                    return BadRequest("Necessário informar o nome!");
                }
                else if (NomeJaExiste(marca.Nome))
                {
                    return BadRequest("O nome já existe em outra marca!");
                }

                Marca novaMarca = new Marca(id, marca.Nome);

                bool naoExiste = _repositoryMarca.GetById(id) == null;

                if (naoExiste)
                {
                    return NotFound();
                }

                _repositoryMarca.Save(novaMarca);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("lote")]
        public IActionResult AtualizarEmLote([FromBody] List<Marca> listaMarcas, [FromQuery] bool validarMarca, [FromQuery] bool validarNome)
        {
            try
            {
                List<string> listaSucesso = new List<string>();
                List<string> listaErros = new List<string>();

                int nomesInvalidos = 0;
                int nomeJaExistente = 0;

                foreach (Marca marca in listaMarcas)
                {
                    if (string.IsNullOrWhiteSpace(marca.Nome))
                    {
                        nomesInvalidos++;
                    }
                    else if (NomeJaExiste(marca.Nome))
                    {
                        nomeJaExistente++;
                    }

                    Marca novaMarca = new Marca(marca.Nome);

                    _repositoryMarca.Save(novaMarca);

                    listaSucesso.Add($"{this.RouteData.Values["controller"]}/{novaMarca.Id}");
                }

                if (nomesInvalidos > 0)
                {
                    listaErros.Add($"Nomes em branco: {nomesInvalidos}");
                }

                if (nomeJaExistente > 0)
                {
                    listaErros.Add($"Nomes já existentes: {nomesInvalidos}");
                }

                if (!listaSucesso.Any())
                {
                    return BadRequest(listaErros);
                }

                if (listaErros.Any())
                {
                    listaSucesso.AddRange(listaErros);
                }

                return Created(this.RouteData.Values["controller"].ToString(), listaSucesso);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Apagar([FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}"), Required(ErrorMessage = "Campo {0} requerido!")] int id)
        {
            try
            {
                Marca marca = _repositoryMarca.GetById(id);

                if (marca == null)
                {
                    return NotFound();
                }

                _repositoryMarca.Delete(marca);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private bool NomeJaExiste(string nome)
        {
            try
            {
                bool existe = !string.IsNullOrWhiteSpace(nome) && _repositoryMarca.GetAll().Where(x => x.Nome == nome).Any();

                return existe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
