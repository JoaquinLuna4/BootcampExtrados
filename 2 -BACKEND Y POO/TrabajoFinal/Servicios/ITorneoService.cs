using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;
using Microsoft.AspNetCore.Mvc;


namespace TrabajoFinal.Servicios
{
    public interface ITorneoService
    {
        int CrearTorneo(RequestCrearTorneo torneo);
        Torneo? ObtenerTorneoPorId(int id);
        IEnumerable<Torneo> ObtenerTodosLosTorneos();
        string ObtenerFaseTorneoId(int id);
        bool ActualizarTorneo(Torneo torneo);
        bool AvanzarFase(int torneoId);
        bool EliminarTorneo(int id);
    }
}
