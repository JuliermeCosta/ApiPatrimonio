using ApiPatrimonio.Data.Extension;
using ApiPatrimonio.Models;
using ApiPatrimonio.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ApiPatrimonio.Repository
{
    public class MarcaRepository : Repository, IRepository<Marca>
    {
        public void Delete(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@Id", id));
                base.ProcedureSemRetorno("prDelMarca");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao apagar dados no banco: {ex.Message}");
            }
            finally
            {
                base.Parameters.Clear();
            }
        }

        public List<Marca> GetAll()
        {
            try
            {
                DataTable table = base.ProcedureComRetorno("prSelMarca");

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
                base.Parameters.Add(new SqlParameter("@Id", id));
                DataTable table = base.ProcedureComRetorno("prSelMarca");

                Marca marca = table.Rows.Count > 0 ? ConvertDataRow(table.Rows[0]) : null;

                return marca;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
            finally
            {
                base.Parameters.Clear();
            }
        }

        public void Save(Marca entity)
        {
            try
            {
                bool novo = (entity.Id <= 0) || (GetById(entity.Id) == null);

                base.Parameters.Add(new SqlParameter("@Nome", entity.Nome));

                if (novo)
                {
                    DataTable table = base.ProcedureComRetorno("prInsMarca");

                    if (table != null)
                    {
                        int novoId = Convert.ToInt32(table.Rows[0]["Id"] ?? 0);

                        entity.Id = novoId;
                    }
                }
                else
                {
                    base.Parameters.Add(new SqlParameter("@Id", entity.Id));

                    base.ProcedureSemRetorno("prUpdMarca");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar dados no banco: {ex.Message}");
            }
            finally
            {
                base.Parameters.Clear();
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
                    UltimaModificacao = row["UltimaModificacao"].ToDateTime(),
                };

                return marca;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao converter dados para objeto: {ex.Message}");
            }
            finally
            {
                base.Parameters.Clear();
            }
        }
    }
}
