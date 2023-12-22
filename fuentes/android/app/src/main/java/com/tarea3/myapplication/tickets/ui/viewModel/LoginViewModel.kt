package com.tarea3.myapplication.tickets.ui.viewModel

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.tarea3.myapplication.tickets.ui.clases.SocketRepository
import com.tarea3.myapplication.tickets.ui.clases.UserSession
import com.tarea3.myapplication.tickets.ui.clases.Usuario
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class LoginViewModel : ViewModel() {

    // Aquí puedes agregar funciones para manejar el inicio de sesión,
    // por ejemplo, validar las credenciales, hacer una solicitud de red, etc.

    private val _loginResult = MutableLiveData<Boolean>()
    val loginResult: LiveData<Boolean> = _loginResult

    fun login(username: String, password: String) {
        viewModelScope.launch(Dispatchers.IO) {
            // Aquí, llama a tu lógica de autenticación
            // Por ejemplo, podrías llamar a tu SocketRepository
            val response = SocketRepository().login(username, password)
            // Ejemplo de uso
            if (response!!.PERFIL.isNotEmpty()){
                val usuarioLogueado = Usuario(response.USUARIO, response.PERFIL)
                UserSession.iniciarSesion(usuarioLogueado)
            }
            _loginResult.postValue(response != null && response.PERFIL.isNotEmpty())
        }
    }
}