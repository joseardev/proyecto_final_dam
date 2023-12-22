package com.tarea3.myapplication.tickets.ui.view

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import com.tarea3.myapplication.tickets.ui.viewModel.LoginViewModel
import com.tarea3.myapplication.databinding.ActivityLoginBinding

class LoginActivity : AppCompatActivity() {
    private lateinit var binding: ActivityLoginBinding
    private lateinit var viewModel: LoginViewModel

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // Inflar la vista utilizando el enlace de datos
        binding = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Inicializar el ViewModel para la actividad
        viewModel = ViewModelProvider(this).get(LoginViewModel::class.java)

        // Configurar el botón de inicio de sesión para responder al clic
        binding.loginButton.setOnClickListener {
            val username = binding.username.text.toString()
            val password = binding.password.text.toString()

            // Llamar al método de inicio de sesión en el ViewModel
            viewModel.login(username, password)
        }

        // Observar los resultados del inicio de sesión desde el ViewModel
        viewModel.loginResult.observe(this, Observer { success ->
            if (success) {
                // Iniciar sesión exitosa, abrir MainActivity
                abrirMainActivity()
            } else {
                // Error de inicio de sesión
                mostrarMensajeError()
            }
        })
    }

    // Método para abrir la actividad principal (MainActivity)
    private fun abrirMainActivity() {
        val intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
        finish() // Finaliza LoginActivity para evitar volver atrás
    }

    // Método para mostrar un mensaje de error (por ejemplo, con Toast)
    private fun mostrarMensajeError() {
        Toast.makeText(this, "Error de inicio de sesión", Toast.LENGTH_SHORT).show()
    }
}
