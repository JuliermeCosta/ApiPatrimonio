using Microsoft.Extensions.Configuration;
using ApiPatrimonio.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ApiPatrimonio.Repositorys.Base
{
    /// <summary>
    /// As classes para respositório devem ser herdeiras dessa classe
    /// </summary>
    public abstract class Repository
    {
        //String de conexão no appsettings.json
        private readonly string _stringConnection = ConfigurationManager.AppSetting.GetConnectionString("Db_Patrimonios");

        private DataTable DatabaseAccess(string procedure, bool comRetorno, bool usarProcedure, params ParameterSql[] parametros)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(_stringConnection))
            {
                try
                {
                    SqlCommand command = new SqlCommand(procedure, connection)
                    {
                        CommandType = usarProcedure ? CommandType.StoredProcedure : CommandType.Text
                    };

                    if (parametros != null)
                    {
                        foreach (ParameterSql parametro in parametros)
                        {
                            command.Parameters.Add(new SqlParameter(parametro.Parameter, parametro.Value));
                        }
                    }

                    command.Connection.Open();

                    if (comRetorno)
                    {
                        using (SqlDataReader sdr = command.ExecuteReader())
                        {
                            table.Load(sdr);
                        }
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return table;
        }

        public void ProcedureNonReturn(string procedure)
        {
            try
            {
                DatabaseAccess(procedure, false, true, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcedureNonReturn(string procedure, params ParameterSql[] parametros)
        {
            try
            {
                DatabaseAccess(procedure, false, true, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ProcedureWithReturn(string procedure)
        {
            try
            {
                return DatabaseAccess(procedure, true, true, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ProcedureWithReturn(string procedure, params ParameterSql[] parametros)
        {
            try
            {
                return DatabaseAccess(procedure, true, true, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SqlNonReturn(string sql)
        {
            try
            {
                DatabaseAccess(sql, false, false, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SqlNonReturn(string sql, params ParameterSql[] parametros)
        {
            try
            {
                DatabaseAccess(sql, false, false, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SqlWithReturn(string sql)
        {
            try
            {
                return DatabaseAccess(sql, true, false, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SqlWithReturn(string sql, params ParameterSql[] parametros)
        {
            try
            {
                return DatabaseAccess(sql, true, false, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
