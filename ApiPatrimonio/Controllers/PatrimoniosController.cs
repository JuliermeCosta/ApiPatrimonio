using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApiPatrimonio.Models;
using ApiPatrimonio.Repository.Interfaces;
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
                bool naoExiste = _repositoryPatrimonio.GetById(id) == null;

                if (naoExiste)
                {
                    return NotFound();
                }

                _repositoryPatrimonio.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
