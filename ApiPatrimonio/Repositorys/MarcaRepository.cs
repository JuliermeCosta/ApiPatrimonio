using ApiPatrimonio.Data.Extensions;
using ApiPatrimonio.Models;
using ApiPatrimonio.Repositorys.Base;
using ApiPatrimonio.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApiPatrimonio.Repositorys
{
    public class MarcaRepository : Repository, IRepository<Marca>
    {
        public void Delete(Marca entity)
        {
            try
            {
                base.ProcedureNonReturn("prDelMarca", new ParameterSql("@Id", entity.Id));
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao apagar dados no banco: {ex.Message}");
            }
        }

        public List<Marca> GetAll()
        {
            try
            {
                DataTable table = base.ProcedureWithReturn("prSelMarca");

                List<Marca> listaMarcas = new List<Marca>();

                foreach (DataRow row in table.Rows)
                {
                    listaMarcas.Add(ConvertDataRow(row));
                }

                return listaMarcas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public Marca GetById(int id)
        {
            try
            {
                DataTable table = base.ProcedureWithReturn("prSelMarca", new ParameterSql("@Id", id));

                Marca marca = table.Rows.Count > 0 ? ConvertDataRow(table.Rows[0]) : null;

                return marca;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public void Save(Marca entity)
        {
            try
            {
                bool novo = (entity.Id <= 0) || (GetById(entity.Id) == null);

                List<ParameterSql> parameters = new List<ParameterSql>() {
                    new ParameterSql("@Nome", entity.Nome)
                };

                if (novo)
                {
                    DataTable table = base.ProcedureWithReturn("prInsMarca", parameters.ToArray());

                    if (table != null)
                    {
                        int novoId = Convert.ToInt32(table.Rows[0]["Id"] ?? 0);

                        entity.Id = novoId;
                    }
                }
                else
                {
                    parameters.Add(new ParameterSql("@Id", entity.Id));

                    base.ProcedureNonReturn("prUpdMarca", parameters.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar dados no banco: {ex.Message}");
            }
        }

        private Marca ConvertDataRow(DataRow row)
        {
            try
            {
                Marca marca = new Marca()
                {
                    Id = row["Id"].ToInt(true),
                    Nome = row["Nome"].ToString(),
                    DataUltimaModificacao = row["DataUltimaModificacao"].ToDateTime(),
                    DataCriacao = row["DataCriacao"].ToDateTime()
                };

                return marca;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao converter dados para objeto: {ex.Message}");
            }
        }
    }
}
