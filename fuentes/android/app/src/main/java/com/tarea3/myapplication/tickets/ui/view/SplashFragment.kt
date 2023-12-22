package com.tarea3.myapplication.tickets.ui.view

import android.os.Bundle
import android.os.Handler
import android.os.Looper
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.fragment.findNavController
import com.tarea3.myapplication.R


class SplashFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inicializa tu layout para el SplashFragment aquí
        return inflater.inflate(R.layout.fragment_splash, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        // Suponiendo que tienes una animación o tarea de carga que esperar,
        // cuando esté listo para navegar al siguiente fragmento:
        goToNextScreen()
    }

    private fun goToNextScreen() {
        // Aquí realizas la navegación después de que tu lógica esté completa
        // Por ejemplo, después de un delay que simula la carga o la animación del splash.
        Handler(Looper.getMainLooper()).postDelayed({
            findNavController().navigate(R.id.action_splashFragment_to_listaTicketsFragment)
        }, 3000) // Cambia 3000 (3 segundos) por la duración que necesites
    }

}
