package com.tarea3.myapplication.tickets.ui.clases

import android.util.Log
import com.google.gson.Gson
import com.google.gson.JsonSyntaxException
import com.google.gson.reflect.TypeToken
import java.io.BufferedReader
import java.io.BufferedWriter
import java.io.IOException
import java.io.InputStreamReader
import java.io.OutputStreamWriter
import java.io.PrintWriter
import java.net.InetAddress
import java.net.Socket
import java.net.UnknownHostException


class SocketRepository {
    private var resultado: String? = null
    @Throws(IOException::class)
    fun connectSocket(mensaje: String): String? {
        try {
            val serverAddr = InetAddress.getByName(ip)
            Log.i("TCP", "Conectando...")
            val socket = Socket(serverAddr, puerto)
            var out: PrintWriter? = null
            var `in`: BufferedReader? = null
            try {
                Log.i("TCP", "C: Sending: '$mensaje'")

                //   avisos.setText("Enviando: '" + message + "'");
                out =
                    PrintWriter(BufferedWriter(OutputStreamWriter(socket.getOutputStream())), true)
                `in` = BufferedReader(InputStreamReader(socket.getInputStream()))
                //   resultado=in.readLine();
                out.println(mensaje)
                // permitir recibir los mensajes del servidor, no enviando más mensajes desde android
                socket.shutdownOutput()
                val total = StringBuilder()
                var line: String?
                if (`in` != null) {
                    while (`in`.readLine().also { line = it } != null) {
                        total.append(line)
                    }
                    resultado = total.toString()
                }
                Log.i("TAG", "resultado")
                Log.i("TCP", "C: Sent.")
                Log.i("TCP", "C: Done.")
            } catch (e: Exception) {
                Log.e("TCP", "S: Error", e)
            } finally {
            }
        } catch (e: UnknownHostException) {
            Log.e("TCP", "C: UnknownHostException", e)
            e.printStackTrace()
        } catch (e: IOException) {
            Log.e("TCP", "C: IOException", e)
            e.printStackTrace()
        }
        return resultado
    }

    // Solicitar la lista de tickets
    fun getTickets(usuario: String): List<Ticket> {
        val requestMessage = "GET_TICKETS:$usuario"
        val receivedData = connectSocket(requestMessage)
        return try {
            // Deserializa la respuesta a una lista de tickets
            val tickets = Gson().fromJson(receivedData, Array<Ticket>::class.java).toList()
            tickets
        } catch (e: JsonSyntaxException) {
            // Manejar error de deserialización
            listOf()
        }
    }
    fun createTicket(ticket: Ticket): String {
        val requestMessage = "CREATE_TICKET:${Gson().toJson(ticket)}"
        val receivedData = connectSocket(requestMessage)
        return receivedData ?: "Error al crear ticket"
    }
    fun login(username: String, password: String): Usuario? {
        val loginRequest = "LOGIN:$username:$password"
        val response = connectSocket(loginRequest)
        return if (response != null) {
            try {
                Gson().fromJson(response, Usuario::class.java)
            } catch (e: JsonSyntaxException) {
                null // En caso de un error de deserialización
            }
        } else {
            null
        }
    }

    fun getTicketsLineas(id: Int): TicketConImagen? {
        val requestMessage = "GET_DETAILS:$id"
        val receivedData = connectSocket(requestMessage)

            // Deserializa la respuesta a una lista de tickets
        val resultado = Gson().fromJson<List<TicketConImagen>>(receivedData, object : TypeToken<List<TicketConImagen>>() {}.type)
        return resultado.firstOrNull()


    }


    companion object {
        private const val TAG = "SocketRepository"
        private const val ip = "172.20.10.3"
        private const val puerto = 1234
    }
}
