package com.tarea3.myapplication.tickets.ui.adapter

import android.content.Intent
import android.os.Handler
import android.os.Looper
import android.view.LayoutInflater
import android.view.ViewGroup
import android.view.animation.AnimationUtils
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import com.tarea3.myapplication.tickets.ui.view.ModificarTicketActivity
import com.tarea3.myapplication.R
import com.tarea3.myapplication.databinding.ItemTicketBinding
import com.tarea3.myapplication.tickets.ui.clases.Ticket

class TicketsAdapter : RecyclerView.Adapter<TicketsAdapter.TicketViewHolder>() {

    private var tickets: List<Ticket> = listOf()

    class TicketViewHolder(private val binding: ItemTicketBinding) : RecyclerView.ViewHolder(binding.root) {
        fun bind(ticket: Ticket) {
           //binding.tvTicketFecha.text = ticket.FECHA
            binding.tvTicketEstado.text = ticket.ESTADO
            binding.tvTipoAviso.text = ticket.TIPO_AVISO
            binding.tvDetalles.text = ticket.OBSERVACIONES

            binding.root.setOnClickListener {
                // Solo inicia la actividad si el estado del ticket es "Resuelto"
                if (ticket.ESTADO == "Resuelto") {
                    val intent = Intent(binding.root.context, ModificarTicketActivity::class.java)
                    intent.putExtra("EXTRA_TICKET_ID", ticket.ID)
                    binding.root.context.startActivity(intent)
                } else {
                    // Opcionalmente, puedes mostrar un mensaje si el ticket no está resuelto
                    Toast.makeText(binding.root.context, "El ticket no está resuelto", Toast.LENGTH_SHORT).show()
                }
            }
            if (ticket.ESTADO == "Resuelto"){
                binding.cardView.setBackgroundResource(R.color.colorFinalizado)
            }
            if (ticket.ESTADO == "EnCurso"){
                binding.cardView.setBackgroundResource(R.color.colorEnCurso)
            }

            // Cambiar el fondo del CardView basado en la urgencia
            if (ticket.URGENTE && ticket.ESTADO  == "Pendiente") {
               // binding.cardView.setBackgroundResource(R.color.colorUrgente)
                /*val animation = AnimationUtils.loadAnimation(itemView.context, R.anim.fade_in)
                itemView.startAnimation(animation)*/
                // Retrasar la animación por 1 segundo (1000 milisegundos)
                Handler(Looper.getMainLooper()).postDelayed({
                    val animation = AnimationUtils.loadAnimation(binding.root.context, R.anim.fade_in)
                    binding.root.startAnimation(animation)
                    binding.cardView.setBackgroundResource(R.color.colorUrgente)
                }, 900)
            }

        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): TicketViewHolder {
        val binding = ItemTicketBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return TicketViewHolder(binding)
    }

    override fun getItemCount(): Int = tickets.size

    override fun onBindViewHolder(holder: TicketViewHolder, position: Int) {
        holder.bind(tickets[position])
    }

    fun setTickets(tickets: List<Ticket>) {
        this.tickets = tickets
        notifyDataSetChanged()
    }
}



