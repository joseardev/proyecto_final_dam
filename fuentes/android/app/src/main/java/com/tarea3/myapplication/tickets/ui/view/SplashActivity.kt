package com.tarea3.myapplication.tickets.ui.view

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import com.tarea3.myapplication.R

class SplashActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_splash)

        // Iniciar LoginActivity después de 2 segundos
        Handler(Looper.getMainLooper()).postDelayed({
            // Intención para iniciar LoginActivity
            val intent = Intent(this, LoginActivity::class.java)
            startActivity(intent)
            // Finaliza la actividad actual para que el usuario no pueda volver a ella
            finish()
        }, 2000) // 2000 ms = 2 segundos
    }
}