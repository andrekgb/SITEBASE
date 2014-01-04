using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Util
{
    /// <summary>
    /// Calculos de tempo.
    /// </summary>
    public class Tempo
    {
        #region "Metodos"

        /// <summary>
        /// Retorna quanto tempo decorreu após uma determinada data.
        /// </summary>
        /// <returns></returns>
        public string calcularTempoDecorrido(DateTime data)
        {
            TimeSpan diff = DateTime.Now - data;

            double segundos = Math.Truncate(diff.TotalSeconds);
            double minutos = Math.Truncate(diff.TotalMinutes);
            double horas = Math.Truncate(diff.TotalHours);
            double dias = Math.Truncate(diff.TotalDays);

            if (segundos < 10)
                return "Neste instante";
            else if (segundos < 60)
                return segundos.ToString() + " segundos atrás";
            else if (minutos == 1)
                return "1 minuto atrás";
            else if (minutos < 60)
                return minutos.ToString() + " minutos atrás";
            else if (horas == 1)
                return "1 hora atrás";
            else if (horas < 24)
                return horas.ToString() + " horas atrás";
            else if (dias == 1)
                return "1 dia atrás";
            else if (dias < 7)
                return dias.ToString() + " dias atrás";
            else if (dias < 14)
                return "1 semana atrás";
            else if (dias < 30)
                return Math.Truncate(dias / 7).ToString() + " semanas atrás";
            else if (dias < 60)
                return "1 mês atrás";
            else if (dias < 365)
                return Math.Truncate(dias / 30).ToString() + " meses atrás";
            else if (dias < 730)
                return "1 ano atrás";
            else
                return Math.Truncate(dias / 365).ToString() + " anos atrás";
        }

        #endregion
    }
}
