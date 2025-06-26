******************************
Pequeña API REST a modo de aprendizaje
***************
Objetivos:

✅ GET /api/salas → Listar todas las salas.
✅ POST /api/salas → Agregar una sala (usa sp_AgregarSala).
✅ GET /api/reservas/{salaId}/{fecha} → Listar reservas de una sala en un día (usa sp_ListarReservasPorSala).
✅ POST /api/reservas → Reservar sala (usa sp_ReservarSala).
✅ DELETE /api/reservas/{reservaId} o un POST → Cancelar reserva (usa sp_CancelarReserva).