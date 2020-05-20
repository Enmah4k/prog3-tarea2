using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tarea2.Models;

namespace Tarea2.Controllers
{
    public class ConsultarRNCController : Controller
    {
        private readonly ILogger<ConsultarRNCController> _logger;

        public ConsultarRNCController(ILogger<ConsultarRNCController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index(){
            return View(new ConsultarRNCModel());
        }

        [HttpPost]
        public IActionResult Index(ConsultarRNCModel c_rnc, string buscar)
        {
            if (buscar == "Buscar"){
                c_rnc.BotonPresionado = true;
                string rnc = c_rnc.RNC;

                string url = "http://adamix.net/gastosrd/api.php?act=GetContribuyentes&rnc="+rnc;

                var json = new WebClient().DownloadString(url);

                if (json != "0"){
                    c_rnc.Success = true;

                    dynamic data = JsonConvert.DeserializeObject(json);

                    c_rnc.Nombre = data["RGE_NOMBRE"];
                    c_rnc.NComercial = data["NOMBRE_COMERCIAL"];
                    c_rnc.Categoria = data["CATEGORIA"];
                    c_rnc.Regimen = data["REGIMEN_PAGOS"];
                    c_rnc.Estatus  = data["ESTATUS"];
                }else{
                    c_rnc.Success = false;
                }
            }

            return View(c_rnc);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
