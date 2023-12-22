package com.tarea3.myapplication.tickets.ui.view

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.tarea3.myapplication.R
import com.tarea3.myapplication.databinding.FragmentCrearTickestBinding
import com.tarea3.myapplication.databinding.FragmentListaTicketsBinding
import com.tarea3.myapplication.databinding.FragmentModificarTicketBinding

class ModificarTicketFragment : Fragment() {
    private var _binding: FragmentModificarTicketBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        // Inflate the layout for this fragment
        _binding = FragmentModificarTicketBinding.inflate(layoutInflater, container, false)
        // Inflate the layout for this fragment
        return binding.root
    }


}