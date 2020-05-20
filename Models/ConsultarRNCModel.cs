using System;

namespace Tarea2.Models
{
    public class ConsultarRNCModel
    {
        public string RNC { get; set; }
        public string Nombre { get; set; }
        public string NComercial { get; set; }
        public string Categoria { get; set; }
        public string Regimen { get; set; }
        public string Estatus { get; set; }

        public bool Success { get; set; }
    }
}