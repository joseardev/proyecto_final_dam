package com.tarea3.myapplication.tickets.ui.clases

import java.time.LocalDate
import java.util.Date

data class Ticket(
    val ID: Int?,
    val ESTADO: String,
    val FECHA: LocalDate?,
    val USUARIO_SOLICITANTE: String,
    val TIPO_AVISO: String,
    val ORIGEN_AVISO: String,
    val URGENTE: Boolean,
    val DETALLES: String,
    val OBSERVACIONES: String
)
data class TicketConImagen(
    val ID: Int,
    val NUMERO_LINEA: Int,
    val DETALLES: String,
    val ESTADO: String?,
    val IMAGEN: String?
)
