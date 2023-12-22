package com.tarea3.myapplication.tickets.ui.view

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.activity.viewModels
import com.tarea3.myapplication.databinding.ActivityModificarTicketBinding // Import the generated binding class
import com.tarea3.myapplication.tickets.ui.viewModel.ModificarTicketViewModel

class ModificarTicketActivity : AppCompatActivity() {

    private lateinit var binding: ActivityModificarTicketBinding
    private val viewModel: ModificarTicketViewModel by viewModels()
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityModificarTicketBinding.inflate(layoutInflater)
        val view = binding.root
        setContentView(view)

        // Get the Intent that started this activity and extract the ticket ID
        var ticketId = intent.getIntExtra("EXTRA_TICKET_ID", 0) // Use a default value if no ID was passed

        //ticketId = 13
        viewModel.obtenerLineasTickets(ticketId)

        binding.btnVolver.setOnClickListener {
            finish();
        }

        // Observe the LiveData within the ViewModel
        viewModel.ticketConImagen.observe(this) { ticketConImagen ->
            // Here you can update your UI based on the ticketConImagen
            if (ticketConImagen != null) {
                // Update the UI with the ticket details
                // e.g., binding.ticketImageView.setImageBitmap(ticketConImagen.image)
                binding.tvNumeroLinea.text = ticketConImagen.NUMERO_LINEA.toString()
               binding.tvEstado.text =  ticketConImagen.ESTADO
                binding.tvDetalles.text = ticketConImagen.DETALLES
                binding.tvTicketId.text =ticketConImagen.ID.toString()
            } else {
                // Handle the case where ticketConImagen is null
                // e.g., show an error message
            }
        }

    }
}
