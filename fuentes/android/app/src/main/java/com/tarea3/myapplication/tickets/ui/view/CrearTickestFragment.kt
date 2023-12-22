package com.tarea3.myapplication.tickets.ui.view

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import com.tarea3.myapplication.databinding.FragmentCrearTickestBinding
import com.tarea3.myapplication.tickets.ui.clases.UserSession
import com.tarea3.myapplication.tickets.ui.viewModel.CrearTicketsViewModel

class CrearTickestFragment : Fragment() {
    private var _binding: FragmentCrearTickestBinding? = null
    private val binding get() = _binding!!
    private lateinit var viewModel: CrearTicketsViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        // Inicializar el ViewModel para este fragmento
        viewModel = ViewModelProvider(this).get(CrearTicketsViewModel::class.java)

        // Inflar la vista utilizando el enlace de datos
        _binding = FragmentCrearTickestBinding.inflate(layoutInflater, container, false)

        // Retorna la vista raíz del fragmento
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        // Configurar el botón "Volver Atrás" para retroceder en la navegación
        binding.btnVolverAtras.setOnClickListener {
            findNavController().popBackStack()
        }

        // Configurar el botón "Crear Tickets"
        binding.creartickets.setOnClickListener {
            // Obtener los valores de los elementos de la interfaz de usuario
            val estado = "Pendiente" // Valor del estado (posiblemente hardcodeado)
            val usuarioSolicitante = UserSession.usuario!!.USUARIO // Usuario solicitante desde la sesión
            val origen = "APP" // Origen del aviso (posiblemente hardcodeado)
            val tipoAviso = binding.spTipoAviso.selectedItem.toString() // Tipo de aviso seleccionado
            val urgente = binding.cbUrgente.isChecked // Estado del CheckBox "Urgente"
            val observaciones = binding.etDetalles.text.toString() // Detalles del aviso

            // Llamar al método del ViewModel para crear tickets con los valores obtenidos
            viewModel.crearTickets(estado, usuarioSolicitante, tipoAviso, origen, urgente, observaciones)
        }

        // Observar si el ticket se creó con éxito
        viewModel.ticketCreado.observe(viewLifecycleOwner) { creado ->
            if (creado) {
                // Si el ticket se creó con éxito, navegar hacia atrás en la pila de navegación
                findNavController().popBackStack()
            }
        }
    }
}
