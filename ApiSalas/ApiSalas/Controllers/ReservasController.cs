using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ApiSalas.Models;

namespace ApiSalas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly string? _connection;
    public ReservasController(IConfiguration configuration)
    {
        _connection = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet]
    public ActionResult<List<Reserva>> GetReservasPorSalaYFecha([FromQuery] int salaID, [FromQuery] DateTime fecha)
    {
        List<Reserva> reservas = new List<Reserva>();
        using (SqlConnection conn = new SqlConnection(_connection))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("sp_ListarReservasPorSala", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SalaID", salaID);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservas.Add(new Reserva
                        {
                            ReservaID = reader.GetInt32(0),
                            SalaID = reader.GetInt32(1),
                            NombreUsuario = reader.GetString(2),
                            Fecha = reader.GetDateTime(3),
                            HoraInicio = reader.GetTimeSpan(4),
                            HoraFin = reader.GetTimeSpan(5),
                            Estado = reader.IsDBNull(6) ? "Activa" : reader.GetString(6)
                        });
                    }

                }
            }
            conn.Close();
        }
        return Ok(reservas);
    }

    [HttpPost]
    public IActionResult AgregarReserva([FromBody] Reserva nuevaReserva)
    {
        using (SqlConnection conn = new SqlConnection(_connection))
        {
            conn.Open();

            using (SqlCommand cmd = new SqlCommand("sp_ReservarSala", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SalaID", nuevaReserva.SalaID);
                cmd.Parameters.AddWithValue("@NombreUsuario", nuevaReserva.NombreUsuario);
                cmd.Parameters.AddWithValue("@Fecha", nuevaReserva.Fecha);
                cmd.Parameters.AddWithValue("@HoraInicio", nuevaReserva.HoraInicio);
                cmd.Parameters.AddWithValue("@HoraFin", nuevaReserva.HoraFin);



                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        return Created("", nuevaReserva);
    }
    [HttpPatch("{ReservaID}")]
    public IActionResult CancelarReserva(int ReservaID)
    {
        using (SqlConnection conn = new SqlConnection(_connection))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("sp_CancelarReserva", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReservaID", ReservaID);

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        return Ok(ReservaID);
    }
}