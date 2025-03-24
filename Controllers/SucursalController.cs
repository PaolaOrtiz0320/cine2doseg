//EXAMEN SEGUNDO SEGUIMIENTO
//Taller Avanzado de Desarrollo de Software
//Integrantes:
//Moedano Alcántara María Fernanda
//Ortiz Velazquez Gabriela Paola 
using System;
using System.Web.Http;
using apiRESTCine.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace apiRESTCine.Controllers
{
    public class SucursalController : ApiController
    {
        // Endpoint GET para ejecutar vwRptSucursales
        // Ruta: cine/sucursal/vwrptsucursales
        [HttpGet]
        [Route("cine/sucursal/vwrptsucursales")]
        public clsApiStatus vwRptSucursales()
        {
            clsApiStatus response = new clsApiStatus();
            try
            {
                clsSucursal objSucursal = new clsSucursal();
                var ds = objSucursal.vwRptSucursales();

                string jsonData = JsonConvert.SerializeObject(ds.Tables[0]);
                response.statusExec = true;
                response.msg = "Consulta exitosa.";
                response.datos = JObject.Parse("{\"sucursales\": " + jsonData + "}");
            }
            catch (Exception ex)
            {
                response.statusExec = false;
                response.ban = -1;
                response.msg = "Error al ejecutar vwRptSucursales: " + ex.Message;
            }
            return response;
        }

        // Endpoint POST para ejecutar spInsSucursales
        // Ruta: cine/sucursal/spinssucursales
        [HttpPost]
        [Route("cine/sucursal/spinssucursales")]
        public clsApiStatus spInsSucursales([FromBody] clsSucursal sucursal)
        {
            clsApiStatus response = new clsApiStatus();
            try
            {
                // Se utiliza el constructor con parámetros para inicializar el objeto
                clsSucursal objSucursal = new clsSucursal(sucursal.clave, sucursal.nombre, sucursal.direccion, sucursal.url, sucursal.logo);
                var ds = objSucursal.spInsSucursales();

                string jsonData = JsonConvert.SerializeObject(ds.Tables[0]);
                response.statusExec = true;
                response.msg = "Inserción ejecutada.";
                response.datos = JObject.Parse("{\"resultado\": " + jsonData + "}");
            }
            catch (Exception ex)
            {
                response.statusExec = false;
                response.ban = -1;
                response.msg = "Error al ejecutar spInsSucursales: " + ex.Message;
            }
            return response;
        }

        // Endpoint DELETE para ejecutar spEliminarSucursal
        // Ruta: cine/sucursal/spdelsucursal/{clave}
        [HttpDelete]
        [Route("cine/sucursal/spdelsucursal/{clave}")]
        public clsApiStatus spDelSucursal(string clave)
        {
            clsApiStatus response = new clsApiStatus();
            try
            {
                clsSucursal objSucursal = new clsSucursal();
                objSucursal.clave = clave;
                var ds = objSucursal.spDelSucursal();

                string jsonData = JsonConvert.SerializeObject(ds.Tables[0]);
                response.statusExec = true;
                response.msg = "Eliminación ejecutada.";
                response.datos = JObject.Parse("{\"resultado\": " + jsonData + "}");
            }
            catch (Exception ex)
            {
                response.statusExec = false;
                response.ban = -1;
                response.msg = "Error al ejecutar spDelSucursal: " + ex.Message;
            }
            return response;
        }
    }
}
