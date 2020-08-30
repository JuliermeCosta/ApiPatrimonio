using System;
using System.Globalization;

namespace ApiPatrimonio.Data.Extensions
{
    public static class ConversionExtension
    {
        public static object ToDBNull(this object objeto, bool stringEmptyToNull)
        {
            if (stringEmptyToNull && objeto?.GetType() == typeof(string))
            {
                return string.IsNullOrEmpty(objeto?.ToString()) ? DBNull.Value : objeto;
            }

            return objeto ?? DBNull.Value;
        }

        public static int ToInt(this object objeto, bool enableError = false)
        {
            try
            {
                bool converteu = int.TryParse(objeto?.ToString(), out int saida);

                if (!converteu && enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int? ToNullableInt(this object objeto, bool enableError = false)
        {
            try
            {
                int? saida = null;

                if (int.TryParse(objeto?.ToString(), out int teste))
                {
                    saida = teste;
                }
                else if (enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static long ToLong(this object objeto, bool enableError = false)
        {
            try
            {
                bool converteu = long.TryParse(objeto?.ToString(), out long saida);

                if (!converteu && enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static long? ToNullableLong(this object objeto, bool enableError = false)
        {
            try
            {
                long? saida = null;

                if (long.TryParse(objeto?.ToString(), out long teste))
                {
                    saida = teste;
                }
                else if (enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ToBool(this object objeto, bool enableError = false)
        {
            try
            {
                bool converteu = bool.TryParse(objeto?.ToString(), out bool saida);

                if (!converteu && enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool? ToNullableBool(this object objeto, bool enableError = false)
        {
            try
            {
                bool? saida = null;

                if (bool.TryParse(objeto?.ToString(), out bool teste))
                {
                    saida = teste;
                }
                else if (enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double ToDouble(this object objeto, bool enableError = false)
        {
            try
            {
                bool converteu = double.TryParse(objeto?.ToString(), out double saida);

                if (!converteu && enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double? ToNullableDouble(this object objeto, bool enableError = false)
        {
            try
            {
                double? saida = null;

                if (double.TryParse(objeto?.ToString(), out double teste))
                {
                    saida = teste;
                }
                else if (enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double ToDouble(this object objeto, CultureInfo culture, NumberStyles style = NumberStyles.None, bool enableError = false)
        {
            try
            {
                bool converteu = double.TryParse(objeto?.ToString(), style, culture, out double saida);

                if (!converteu && enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double? ToNullableDouble(this object objeto, CultureInfo culture, NumberStyles style = NumberStyles.None, bool enableError = false)
        {
            try
            {
                double? saida = null;

                if (double.TryParse(objeto?.ToString(), style, culture, out double teste))
                {
                    saida = teste;
                }
                else if (enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime ToDateTime(this object objeto, bool enableError = false)
        {
            try
            {
                bool converteu = DateTime.TryParse(objeto?.ToString(), out DateTime saida);

                if (!converteu && enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DateTime? ToNullableDateTime(this object objeto, bool enableError = false)
        {
            try
            {
                DateTime? saida = null;

                if (DateTime.TryParse(objeto?.ToString(), out DateTime teste))
                {
                    saida = teste;
                }
                else if (enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime ToDateTime(this object objeto, CultureInfo culture, DateTimeStyles style = DateTimeStyles.None, bool enableError = false)
        {
            try
            {
                bool converteu = DateTime.TryParse(objeto?.ToString(), culture, style, out DateTime saida);

                if (!converteu && enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime? ToNullableDateTime(this object objeto, CultureInfo culture, DateTimeStyles style = DateTimeStyles.None, bool enableError = false)
        {
            try
            {
                DateTime? saida = null;

                if (DateTime.TryParse(objeto?.ToString(), culture, style, out DateTime teste))
                {
                    saida = teste;
                }
                else if (enableError)
                {
                    throw new Exception("A conversão falhou!");
                }

                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
