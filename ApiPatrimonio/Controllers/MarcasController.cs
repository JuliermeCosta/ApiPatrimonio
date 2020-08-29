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
        public IActionResult Get()
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
        public IActionResult Get([FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}")] int id)
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
        public IActionResult GetPatrimonios([FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}")] int id)
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
        public IActionResult Post([FromBody] Marca marca)
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
        public IActionResult Put(
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


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute, Range(1, int.MaxValue, ErrorMessage = "Digite um valor entre {1} e {2}"), Required(ErrorMessage = "Campo {0} requerido!")] int id)
        {
            try
            {
                bool naoExiste = _repositoryMarca.GetById(id) == null;

                if (naoExiste)
                {
                    return NotFound();
                }

                _repositoryMarca.Delete(id);

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
