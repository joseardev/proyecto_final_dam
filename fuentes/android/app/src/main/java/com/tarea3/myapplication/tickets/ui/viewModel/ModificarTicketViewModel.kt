package com.tarea3.myapplication.tickets.ui.viewModel

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.tarea3.myapplication.tickets.ui.clases.SocketRepository
import com.tarea3.myapplication.tickets.ui.clases.TicketConImagen
import com.tarea3.myapplication.tickets.ui.clases.UserSession
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class ModificarTicketViewModel : ViewModel(){

    private val repository: SocketRepository
    init {
        repository = SocketRepository()
    }
    // LiveData to hold the TicketConImagen
    private val _ticketConImagen = MutableLiveData<TicketConImagen?>()
    val ticketConImagen: MutableLiveData<TicketConImagen?> = _ticketConImagen

    // Método para solicitar tickets al servidor
    fun obtenerLineasTickets(ticketId: Int) {
        viewModelScope.launch(Dispatchers.IO) {
            try {
                val tickets = repository.getTicketsLineas(ticketId)
                withContext(Dispatchers.Main) {
                    // Update the LiveData which the UI can observe
                    _ticketConImagen.value = tickets
                }
            } catch (e: Exception) {
                // Handle any exceptions here
                withContext(Dispatchers.Main) {
                    // You might want to update the UI or LiveData with error information
                }
            }
        }
    }
}