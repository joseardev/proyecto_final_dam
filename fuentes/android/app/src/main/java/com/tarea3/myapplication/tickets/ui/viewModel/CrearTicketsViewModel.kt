package com.tarea3.myapplication.tickets.ui.viewModel

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.tarea3.myapplication.tickets.ui.clases.SocketRepository
import com.tarea3.myapplication.tickets.ui.clases.Ticket
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import kotlin.random.Random


class CrearTicketsViewModel : ViewModel() {
    private val repository: SocketRepository

    // Suponiendo que cada ticket es simplemente un String.
    // En un escenario real, podrías tener una clase Ticket con varios campos.
    var tickets = MutableLiveData<List<String>>()
    private val _ticketCreado = MutableLiveData<Boolean>()
    val ticketCreado: LiveData<Boolean> = _ticketCreado
    init {
        repository = SocketRepository()
    }

    fun crearTickets(
        estado: String,
        usuarioSolicitante: String,
        tipoAviso: String,
        origen:String,
        urgente: Boolean,
        observaciones: String
    ) {
        viewModelScope.launch(Dispatchers.IO) {
            // Crear el objeto Ticket utilizando los argumentos
            val ticket = Ticket(
                ID = null, // Asegúrate de manejar posibles excepciones aquí
                ESTADO = estado,
                null,
                USUARIO_SOLICITANTE = usuarioSolicitante,
                TIPO_AVISO = tipoAviso,
                ORIGEN_AVISO = origen,
                URGENTE = urgente,
                "",
                OBSERVACIONES = observaciones
            )

            // Pasar este objeto al método createTicket del repositorio
            val result = repository.createTicket(ticket)

            withContext(Dispatchers.Main) {
                // Actualizar la interfaz de usuario aquí basado en el resultado
                if (result.isNotEmpty()) _ticketCreado.value = true

            }
        }
    }

    fun generateRandomId(): Int {
        return Random.nextInt(1, 10000)
    }

    fun generateRandomFecha(): String {
        return "2023-${Random.nextInt(1, 12)}-${Random.nextInt(1, 28)}"
    }

    fun <T> chooseRandomFromList(list: List<T>): T {
        return list[Random.nextInt(list.size)]
    }
}
