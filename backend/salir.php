<?php
    session_start();

    session_destroy(); // Rompe la pulsera VIP y borra la memoria
    header("Location: ../frontend/ingreso.html"); // Lo manda de vuelta a la puerta
    exit;

?>