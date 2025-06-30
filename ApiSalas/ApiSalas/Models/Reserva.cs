namespace ApiSalas.Models;

public class Reserva
{
    public int ReservaID { get; set; }
    public int SalaID { get; set; }
    public string? NombreUsuario { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public string? Estado { get; set; }
    

}
