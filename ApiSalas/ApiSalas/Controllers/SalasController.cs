using Microsoft.AspNetCore.Mvc;
using ApiSalas.Models;

namespace ApiSalas.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SalasController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Sala>> GetSalas()
    {
        List<Sala> salas = new List<Sala>
        {
            new Sala { SalaID = 1, Nombre = "Sala Alfa", Capacidad = 10, Ubicacion = "Planta Baja" },
            new Sala { SalaID = 2, Nombre = "Sala Beta", Capacidad = 20, Ubicacion = "Primer Piso" }
        };
        return Ok(salas);
    }


}