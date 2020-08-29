using ApiPatrimonio.Data.Extension;
using ApiPatrimonio.Models;
using ApiPatrimonio.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ApiPatrimonio.Repository
{
    public class PatrimonioRepository : Repository, IRepository<Patrimonio>
    {
        public void Delete(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@Id", id));
                base.ProcedureSemRetorno("prDelPatrimonio");
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

        public List<Patrimonio> GetAll()
        {
            try
            {
                DataTable table = base.ProcedureComRetorno("prSelPatrimonio");

                List<Patrimonio> listaPatrimonios = new List<Patrimonio>();

                foreach (DataRow row in table.Rows)
                {
                    listaPatrimonios.Add(ConvertDataRow(row));
                }

                return listaPatrimonios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public Patrimonio GetById(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@Id", id));
                DataTable table = base.ProcedureComRetorno("prSelPatrimonio");

                Patrimonio patrimonio = table.Rows.Count > 0 ? ConvertDataRow(table.Rows[0]) : null;

                return patrimonio;
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

        public void Save(Patrimonio entity)
        {
            try
            {
                bool novo = (entity.Id <= 0) || (GetById(entity.Id) == null);

                base.Parameters.Add(new SqlParameter("@Nome", entity.Nome));
                base.Parameters.Add(new SqlParameter("@MarcaId", entity.MarcaId));
                base.Parameters.Add(new SqlParameter("@Descricao", entity.Descricao));

                if (novo)
                {
                    base.Parameters.Add(new SqlParameter("@NumeroTombo", entity.NumeroTombo));

                    DataTable table = base.ProcedureComRetorno("prInsPatrimonio");

                    if (table != null)
                    {
                        int novoId = Convert.ToInt32(table.Rows[0]["Id"] ?? 0);

                        entity.Id = novoId;
                    }
                }
                else
                {
                    base.Parameters.Add(new SqlParameter("@Id", entity.Id));

                    base.ProcedureSemRetorno("prUpdPatrimonio");
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

        private Patrimonio ConvertDataRow(DataRow row)
        {
            try
            {
                Patrimonio patrimonio = new Patrimonio()
                {
                    Id = row["Id"].ToInt(true),
                    Descricao = row["Descricao"].ToString(),
                    Nome = row["Nome"].ToString(),
                    NumeroTombo = row["NumeroTombo"].ToNullableInt(),
                    UltimaModificacao = row["UltimaModificacao"].ToDateTime(),
                    MarcaId = row["MarcaId"].ToInt()
                };

                return patrimonio;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao converter dados para objeto: {ex.Message}");
            }
        }
    }
}
