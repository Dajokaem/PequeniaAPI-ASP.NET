namespace ApiSalas.Models;

public class Reserva
{
    public int ReservaID { get; set; }
    public int SalaID { get; set; }
    public string NombreUsuario { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public string Estado { get; set; }
    

}
