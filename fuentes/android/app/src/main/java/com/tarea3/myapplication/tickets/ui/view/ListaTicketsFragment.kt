package com.tarea3.myapplication.tickets.ui.view

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.tarea3.myapplication.R
import com.tarea3.myapplication.databinding.FragmentListaTicketsBinding
import com.tarea3.myapplication.tickets.ui.adapter.TicketsAdapter
import com.tarea3.myapplication.tickets.ui.viewModel.ListaTicketsViewModel

class ListaTicketsFragment: Fragment() {

    private var _binding: FragmentListaTicketsBinding? = null
    private val binding get() = _binding!!

    // Inicializando ViewModel
    private lateinit var viewModel: ListaTicketsViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        // Inflate the layout for this fragment
        _binding = FragmentListaTicketsBinding.inflate(layoutInflater, container, false)

        // Initialize ViewModel
        viewModel = ViewModelProvider(this).get(ListaTicketsViewModel::class.java)

        val ticketsAdapter = TicketsAdapter()
        binding.recyclerView.adapter = ticketsAdapter

        // Aquí agregamos el LinearLayoutManager al RecyclerView
        binding.recyclerView.layoutManager = LinearLayoutManager(context)

        viewModel.ticketsLiveData.observe(viewLifecycleOwner) { tickets ->
            ticketsAdapter.setTickets(tickets)
        }

        viewModel.fetchTickets()

        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.fabAddTicket.setOnClickListener {
            // Aquí manejas el clic en el FAB
            // Por ejemplo, navegar a otro fragmento para agregar tickets
            findNavController().navigate(R.id.action_listaTicketsFragment_to_crearTickestFragment)


        }
    }
    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}
