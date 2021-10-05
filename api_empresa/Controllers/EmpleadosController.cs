using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using api_empresa.Models;
namespace api_empresa.Controllers{
    [Route("api/[controller]")]
    public class EmpleadosController : Controller {
        //establecer conexión con el aplicativo realizado para la conexión con la base de datos.
        private Conexion dbConexion;

        
        public EmpleadosController() { 
            dbConexion = Conectar.Create();
        }

        //Get para mostrar todos los valores
        //Get api/Empleados
        [HttpGet]
        public ActionResult Get() {
            // return Ok(dbConexion.Empleados.ToArray());
             var query = from a in dbConexion.Empleados
                        join s in dbConexion.Puestos
                        on a.id_puesto equals s.id_puesto
                        select new Empleados{ id_empleado=a.id_empleado, codigo=a.codigo,nombres= a.nombres, apellidos = a.apellidos,
                            direccion = a.direccion, telefono = a.telefono, fecha_nacimiento = a.fecha_nacimiento,id_puesto=a.id_puesto,puesto= s };
            return Ok(query.ToList());
        }

        //Get para mostrar un valor
        //Método asíncrono
        //Get api/Empleados/1
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) {
            // var empleados = await dbConexion.Empleados.FindAsync(id);
             var empleados =  from a in dbConexion.Empleados
                        join s in dbConexion.Puestos
                        on a.id_puesto equals s.id_puesto
                        where a.id_empleado==id
                        select new Empleados{ id_empleado=a.id_empleado, codigo=a.codigo,nombres= a.nombres, apellidos = a.apellidos,
                            direccion = a.direccion, telefono = a.telefono, fecha_nacimiento = a.fecha_nacimiento,id_puesto=a.id_puesto,puesto= s };
           
            
            if (empleados != null) {
                return Ok(empleados);
            } else {
                return NotFound();
            }
        }

        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleados empleados) {
            if (ModelState.IsValid){
                dbConexion.Empleados.Add(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();
            } else {
                return BadRequest();
            }
        }

        
        public async Task<ActionResult> Put([FromBody] Empleados empleados){
            var v_empleados = dbConexion.Empleados.SingleOrDefault(c => c.id_empleado == empleados.id_empleado);
            if (v_empleados != null && ModelState.IsValid){
                dbConexion.Entry(v_empleados).CurrentValues.SetValues(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();
            } else {
                return BadRequest();
            }
        }


        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var empleados = dbConexion.Empleados.SingleOrDefault(c => c.id_empleado == id);
            if (empleados != null) {
                dbConexion.Empleados.Remove(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();
            } else {
                return NotFound();
            }
        }
  }
}