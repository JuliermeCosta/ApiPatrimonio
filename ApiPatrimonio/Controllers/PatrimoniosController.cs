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
    public class PatrimoniosController : ControllerBase
    {
        private readonly IRepository<Patrimonio> _repositoryPatrimonio;
        private readonly IRepository<Marca> _repositoryMarca;

        public PatrimoniosController(IRepository<Patrimonio> repositoryPatrimonio, IRepository<Marca> repositoryMarca)
        {
            _repositoryPatrimonio = repositoryPatrimonio;
            _repositoryMarca = repositoryMarca;
        }

        [HttpGet]
        public IActionResult Get([FromQuery, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}")] int? marcaId)
        {
            try
            {
                List<Patrimonio> listaPatrimonios = _repositoryPatrimonio.GetAll();

                if (marcaId.HasValue)
                {
                    listaPatrimonios = listaPatrimonios.Where(x => x.MarcaId == marcaId.Value).ToList();
                }

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

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}")] int id)
        {
            try
            {
                Patrimonio patrimonio = _repositoryPatrimonio.GetById(id);

                if (patrimonio == null)
                {
                    return NotFound();
                }

                return Ok(patrimonio);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Patrimonio patrimonio, [FromQuery] bool validarMarca, [FromQuery] bool validarNome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(patrimonio.Nome))
                {
                    return BadRequest("Necessário informar o nome!");
                }

                if (patrimonio.MarcaId <= 0)
                {
                    return BadRequest($"A MarcaId deve ser entre 1 e {int.MaxValue}");
                }

                if (validarMarca)
                {
                    bool naoExisteMarca = _repositoryMarca.GetById(patrimonio.MarcaId) == null;

                    if (naoExisteMarca)
                    {
                        return BadRequest("Não existe uma marca com esse Id!");
                    }
                }

                if (validarNome)
                {
                    bool existeNome = _repositoryPatrimonio.GetAll().Where(x => x.Nome == patrimonio.Nome).Any();

                    if (existeNome)
                    {
                        return BadRequest("Já existe um patrimônio com esse nome!");
                    }
                }

                Patrimonio novoPatrimonio = new Patrimonio(patrimonio.Nome, patrimonio.Descricao, patrimonio.MarcaId);

                _repositoryPatrimonio.Save(novoPatrimonio);

                string location = $"{this.RouteData.Values["controller"]}/{novoPatrimonio.Id}";

                return Created(location, null);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("lote")]
        public IActionResult Post([FromBody] List<Patrimonio> listaPatrimonios, [FromQuery] bool validarMarca, [FromQuery] bool validarNome)
        {
            try
            {
                List<string> listaSucesso = new List<string>();
                List<string> listaErros = new List<string>();

                int nomesInvalidos = 0;
                int marcasInvalidas = 0;
                int marcaNaoExiste = 0;
                int nomeJaExistente = 0;

                foreach (Patrimonio patrimonio in listaPatrimonios)
                {
                    if (string.IsNullOrWhiteSpace(patrimonio.Nome))
                    {
                        nomesInvalidos++;
                    }

                    if (patrimonio.MarcaId <= 0)
                    {
                        marcasInvalidas++;
                    }

                    if (validarMarca)
                    {
                        bool naoExisteMarca = _repositoryMarca.GetById(patrimonio.MarcaId) == null;

                        if (naoExisteMarca)
                        {
                            marcaNaoExiste++;
                        }
                    }

                    if (validarNome)
                    {
                        bool existeNome = _repositoryPatrimonio.GetAll().Where(x => x.Nome == patrimonio.Nome).Any();

                        if (existeNome)
                        {
                            nomeJaExistente++;
                        }
                    }

                    Patrimonio novoPatrimonio = new Patrimonio(patrimonio.Nome, patrimonio.Descricao, patrimonio.MarcaId);

                    _repositoryPatrimonio.Save(novoPatrimonio);

                    listaSucesso.Add($"{this.RouteData.Values["controller"]}/{novoPatrimonio.Id}");
                }

                if (nomesInvalidos > 0)
                {
                    listaErros.Add($"Nomes em branco: {nomesInvalidos}");
                }

                if (marcasInvalidas > 0)
                {
                    listaErros.Add($"Marcas inválidas: {nomesInvalidos}");
                }

                if (marcaNaoExiste > 0)
                {
                    listaErros.Add($"Marcas com IDs não existentes: {nomesInvalidos}");
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

        [HttpPut("{id}")]
        public IActionResult Put(
            [FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}"), Required(ErrorMessage = "Campo {0} requerido!")] int id,
            [FromBody] Patrimonio patrimonio,
            [FromQuery] bool validarMarca,
            [FromQuery] bool validarNome
            )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(patrimonio.Nome))
                {
                    return BadRequest("Necessário informar o nome!");
                }

                if (patrimonio.MarcaId <= 0)
                {
                    return BadRequest($"A MarcaId deve ser entre 1 e {int.MaxValue}");
                }

                if (validarMarca)
                {
                    bool naoExisteMarca = _repositoryMarca.GetById(patrimonio.MarcaId) == null;

                    if (naoExisteMarca)
                    {
                        return BadRequest("Não existe uma marca com esse Id!");
                    }
                }

                if (validarNome)
                {
                    bool existeNome = _repositoryPatrimonio.GetAll().Where(x => x.Nome == patrimonio.Nome).Any();

                    if (existeNome)
                    {
                        return BadRequest("Já existe um patrimônio com esse nome!");
                    }
                }

                Patrimonio novoPatrimonio = new Patrimonio(id, patrimonio.Nome, patrimonio.Descricao, patrimonio.MarcaId);

                bool naoExiste = _repositoryPatrimonio.GetById(id) == null;

                if (naoExiste)
                {
                    return NotFound();
                }

                _repositoryPatrimonio.Save(novoPatrimonio);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}"), Required(ErrorMessage = "Campo {0} requerido!")] int id)
        {
            try
            {
                Patrimonio patrimonio = _repositoryPatrimonio.GetById(id);

                if (patrimonio == null)
                {
                    return NotFound();
                }

                _repositoryPatrimonio.Delete(patrimonio);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
