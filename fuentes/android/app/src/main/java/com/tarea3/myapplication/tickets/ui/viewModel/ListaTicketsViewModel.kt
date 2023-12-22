package com.tarea3.myapplication.tickets.ui.viewModel

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.tarea3.myapplication.tickets.ui.clases.SocketRepository
import com.tarea3.myapplication.tickets.ui.clases.Ticket
import com.tarea3.myapplication.tickets.ui.clases.UserSession
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext


class ListaTicketsViewModel : ViewModel() {
    private val repository: SocketRepository

    // Suponiendo que cada ticket es simplemente un String.
    // En un escenario real, podrías tener una clase Ticket con varios campos.
    var tickets = MutableLiveData<List<String>>()
    var errorMessage = MutableLiveData<String>()
    val ticketsLiveData = MutableLiveData<List<Ticket>>()
    init {
        repository = SocketRepository()
    }

    // Método para solicitar tickets al servidor
    fun fetchTickets() {
        viewModelScope.launch(Dispatchers.IO) {
            if (UserSession.isSesionIniciada) {
                val usuarioActual = UserSession.usuario
                usuarioActual?.let {
                    val nombre = it.USUARIO
                    val tipoPerfil = it.PERFIL
                }
            val tickets = repository.getTickets(usuarioActual!!.USUARIO)
            withContext(Dispatchers.Main) {
                // Actualizar la interfaz de usuario aquí
                ticketsLiveData.postValue(tickets)
            }

        }
    }
    }
}
