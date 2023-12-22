package com.tarea3.myapplication.tickets.ui.clases

object UserSession {
    var usuario: Usuario? = null
    fun iniciarSesion(usuario: Usuario) {
        this.usuario = usuario
    }

    fun cerrarSesion() {
        usuario = null
    }

    val isSesionIniciada: Boolean
        get() = usuario != null
}
