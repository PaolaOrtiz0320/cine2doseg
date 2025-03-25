//EXAMEN SEGUNDO SEGUIMIENTO
//Taller Avanzado de Desarrollo de Software
//Integrantes:
//Moedano Alcántara María Fernanda
//Ortiz Velazquez Gabriela Paola 
//Soto Cadena Alan

using Newtonsoft.Json.Linq;

namespace apiRESTCine.Models
{
    public class clsApiStatus
    {
        public bool statusExec { get; set; }
        public int ban { get; set; }
        public string msg { get; set; }
        public object datos { get; set; }
    }
}
