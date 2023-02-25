using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica.Models;
using Microsoft.EntityFrameworkCore;
namespace practica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;

        public equiposController(equiposContext equipoContexto) {
            _equiposContext= equipoContexto;
        }
        //Método de obtención de datos
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<equipos> listadoequipo = (from e in _equiposContext.equipos select e).ToList();

            if(listadoequipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoequipo);
        }
        //Método de búsqueda de ID
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContext.equipos
                               where e.id == id
                               select e).FirstOrDefault();

            if (equipo == null) return NotFound();
            return Ok(equipo);
        }

        //Metodo de búsqueda
        [HttpGet]
        [Route("find/{filtro}")]
        public IActionResult Get(string filtro) 
        { 
            List<equipos> equipo = (from e in _equiposContext.equipos
                                    where e.nombre.Contains(filtro)
                                    select e).ToList();
            if (equipo == null) return NotFound();
            return Ok(equipo);
        }

        //Método de añadir
        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] equipos equipo) {
            try
            {
                _equiposContext.Add(equipo);
                _equiposContext.SaveChanges();
                return Ok(equipo);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método de actualizar datos
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult actualizarEquipo(int id, [FromBody] equipos updateDevice) {
            
            equipos? equipo = (from e in _equiposContext.equipos
                               where e.id == id select e).FirstOrDefault();

            if(equipo == null) return NotFound();

            equipo.nombre = updateDevice.nombre;
            equipo.costo = updateDevice.costo;

            _equiposContext.Entry(equipo).State = EntityState.Modified; 
            _equiposContext.SaveChanges();
            return Ok(equipo);
        }

        //Métodos de eliminar por ID
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            equipos? equipo = (from e in _equiposContext.equipos
                               where e.id == id
                               select e).FirstOrDefault();

            if (equipo == null) return NotFound();

            _equiposContext.equipos.Attach(equipo);
            _equiposContext.equipos.Remove(equipo);
            _equiposContext.SaveChanges();
            return Ok(equipo);
        }
    }

    

}
