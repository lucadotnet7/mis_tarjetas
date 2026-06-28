<?php
    session_start();
    
    $documento = $_SESSION['documento'];
    $nombre_usuario = $_SESSION['usuario'];

    echo "<div style='background-color: #e8fff4; border: 1px solid #77ff00; padding: 20px; border-radius: 8px; max-width: 500px; text-align: center; font-family: Arial, sans-serif; display: flex; flex-direction: column; align-items: center; margin: 150px auto;'>";
    echo "<h1>Bienvenido a tu Home Banking, " . $nombre_usuario . "</h1>";
    echo "</div>";
?>