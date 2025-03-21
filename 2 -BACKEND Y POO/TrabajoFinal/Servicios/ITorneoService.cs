﻿using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;
using Microsoft.AspNetCore.Mvc;


namespace TrabajoFinal.Servicios
{
    public interface ITorneoService
    {
        int CrearTorneo(RequestCrearTorneo torneo);
        void AgregarSeriesATorneoExistente(int torneoId, List<int> seriesIds);
        Torneo? ObtenerTorneoPorId(int id);
        IEnumerable<Torneo> ObtenerTodosLosTorneos();
        string ObtenerFaseTorneoId(int id);
        bool ActualizarTorneo(Torneo torneo);
        bool TorneoExiste(int torneoId);
        bool AvanzarFase(int torneoId);
        bool AvanzarRonda(int torneoId);
        bool EliminarTorneo(int id);
    }
}
