using ApiPatrimonio.Data.Extensions;
using ApiPatrimonio.Models;
using ApiPatrimonio.Repositorys.Base;
using ApiPatrimonio.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApiPatrimonio.Repositorys
{
    public class PatrimonioRepository : Repository, IRepository<Patrimonio>
    {
        public void Delete(Patrimonio entity)
        {
            try
            {
                base.ProcedureNonReturn("prDelPatrimonio", new ParameterSql("@Id", entity.Id));
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao apagar dados no banco: {ex.Message}");
            }
        }

        public List<Patrimonio> GetAll()
        {
            try
            {
                DataTable table = base.ProcedureWithReturn("prSelPatrimonio");

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
                DataTable table = base.ProcedureWithReturn("prSelPatrimonio", new ParameterSql("@Id", id));

                Patrimonio patrimonio = table.Rows.Count > 0 ? ConvertDataRow(table.Rows[0]) : null;

                return patrimonio;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public void Save(Patrimonio entity)
        {
            try
            {
                bool novo = (entity.Id <= 0) || (GetById(entity.Id) == null);

                List<ParameterSql> parameters = new List<ParameterSql>
                {
                    new ParameterSql("@Nome", entity.Nome),
                    new ParameterSql("@MarcaId", entity.MarcaId),
                    new ParameterSql("@Descricao", entity.Descricao)
                };

                if (novo)
                {
                    parameters.Add(new ParameterSql("@NumeroTombo", entity.NumeroTombo));

                    DataTable table = base.ProcedureWithReturn("prInsPatrimonio", parameters.ToArray());

                    if (table != null)
                    {
                        int novoId = Convert.ToInt32(table.Rows[0]["Id"] ?? 0);

                        entity.Id = novoId;
                    }
                }
                else
                {
                    parameters.Add(new ParameterSql("@Id", entity.Id));

                    base.ProcedureNonReturn("prUpdPatrimonio", parameters.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar dados no banco: {ex.Message}");
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
                    DataUltimaModificacao = row["DataUltimaModificacao"].ToDateTime(),
                    DataCriacao = row["DataCriacao"].ToDateTime(),
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
