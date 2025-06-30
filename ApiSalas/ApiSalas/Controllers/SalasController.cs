using Microsoft.AspNetCore.Mvc;
using ApiSalas.Models;
using System.Data;
using System.Data.SqlClient;


namespace ApiSalas.Controllers;



[ApiController]
[Route("api/[Controller]")]
public class SalasController : ControllerBase
{
    private readonly string? _connectionString;


    public SalasController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }



    [HttpGet]
    public ActionResult<List<Sala>> GetSalas()
    {
        List<Sala> salas = new List<Sala>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("sp_ListarSalas", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salas.Add(new Sala
                        {
                            SalaID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Capacidad = reader.GetInt32(2),
                            Ubicacion = reader.GetString(3)
                        });
                    }
                }
            }
        }
        return Ok(salas);
    }

    [HttpPost]
    public IActionResult AgregarSala([FromBody] Sala nuevaSala)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            using (SqlCommand cmd = new SqlCommand("sp_AgregarSala", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", nuevaSala.Nombre);
                cmd.Parameters.AddWithValue("@Capacidad", nuevaSala.Capacidad);
                cmd.Parameters.AddWithValue("@Ubicacion", nuevaSala.Ubicacion);

                cmd.ExecuteNonQuery();
            }
        }

        return Created("", nuevaSala);
    }


}