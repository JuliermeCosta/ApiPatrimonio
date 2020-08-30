namespace ApiPatrimonio.Repositorys.Base
{
    /// <summary>
    /// Classe auxiliar para inclusão de parâmetros nas consultas
    /// </summary>
    public sealed class ParameterSql
    {
        /// <summary>
        /// Parâmetro conforme está no SQL
        /// </summary>
        public string Parameter { get; private set; }

        /// <summary>
        /// Objeto correspondente ao parâmetro
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Classe auxiliar para inclusão de parâmetros nas consultas
        /// </summary>
        /// <param name="parameterSQL">Parâmetro conforme está no SQL</param>
        /// <param name="value">Objeto correspondente ao parâmetro</param>
        public ParameterSql(string parameterSQL, object value)
        {
            Parameter = parameterSQL;
            Value = value;
        }
    }
}
