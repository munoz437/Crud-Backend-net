using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using api_empresa.Models;
namespace api_empresa.Controllers{
    [Route("api/[controller]")]
    public class PuestosController : Controller {
       
        private Conexion dbConexion;

        
        public PuestosController() { 
            dbConexion = Conectar.Create();
        }

        //Get para mostrar todos los valores
        //Get api/Puestos
        [HttpGet]
        public ActionResult Get() {
            return Ok(dbConexion.Puestos.ToArray());
        }

        //Get para mostrar un valor
        //Método asíncrono
        //Get api/Puestos/1
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) {
            var Puestos = await dbConexion.Puestos.FindAsync(id);
            if (Puestos != null) {
                return Ok(Puestos);
            } else {
                return NotFound();
            }
        }

        //Post para insertar datos
        //Post api/Puestos
        // {
        // "id_empleado": 1,
        // "codigo": "E001",
        // "nombres": "Mario Haroldo",
        // "apellidos": "Sinay Gaytan",
        // "direccion": "Antigua",
        // "telefono": "30319066",
        // "fecha_nacimiento": "11/03/2000 00:00:00",
        // "id_puesto": 3
        // [HttpPost]
        // public async Task<ActionResult> Post([FromBody] Puestos Puestos) {
        //     if (ModelState.IsValid){
        //         dbConexion.Puestos.Add(Puestos);
        //         await dbConexion.SaveChangesAsync();
        //         return Ok();
        //     } else {
        //         return BadRequest();
        //     }
        // }

        //update
        //Put api/Puestos/1
        // {
        // "id_empleado": 1,
        // "codigo": "E001",
        // "nombres": "Mario Haroldo",
        // "apellidos": "Sinay Gaytan",
        // "direccion": "Antigua",
        // "telefono": "30319066",
        // "fecha_nacimiento": "11/03/2000 00:00:00",
        // "id_puesto": 3
        // public async Task<ActionResult> Put([FromBody] Puestos Puestos){
        //     var v_Puestos = dbConexion.Puestos.SingleOrDefault(c => c.id_empleado == Puestos.id_empleado);
        //     if (v_Puestos != null && ModelState.IsValid){
        //         dbConexion.Entry(v_Puestos).CurrentValues.SetValues(Puestos);
        //         await dbConexion.SaveChangesAsync();
        //         return Ok();
        //     } else {
        //         return BadRequest();
        //     }
        // }


        //Get para mostrar un valor
        //Método asíncrono
        //Delete api/Puestos/1
        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(int id) {
        //     var Puestos = dbConexion.Puestos.SingleOrDefault(c => c.id_empleado == id);
        //     if (Puestos != null) {
        //         dbConexion.Puestos.Remove(Puestos);
        //         await dbConexion.SaveChangesAsync();
        //         return Ok();
        //     } else {
        //         return NotFound();
        //     }
        // }
  }
}