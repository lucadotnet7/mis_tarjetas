<?php
    session_start();
    
    // valido que halla llegado a esta pagina con la sesion iniciada , si no es asi lo mando al login
    if (!isset($_SESSION['documento'])) {
        header("Location: ../ingreso.html");
        exit;
    }
    // extraigo los datos de la sesion para usarlos en la consulta a la base de datos    
    $documento = $_SESSION['documento'];
    $nombre_usuario = $_SESSION['usuario'];
    // conexion a la base de datos para traer los resúmenes de la cuenta del usuario logueado
    $conexion = mysqli_connect("db" , "root" , "root" , "mi_banco_db" );

    if(!$conexion){
        echo "ERROR AL CONECTAR LA BASE DE DATOS !  " ;
        header("Location: ../frontend/ingreso.html");
        exit;
    } else{
        // realizo la consulta con los join para traer los resúmenes de la cuenta del usuario logueado , ordenados por fecha de vencimiento descendente
        $query = "SELECT * FROM liquidaciones l join tarjetas t on l.num_cuenta = t.num_cuenta JOIN usuarios u ON t.dni_titular = u.documento WHERE u.documento = '$documento' ORDER BY l.fecha_vencimiento DESC";
        
        $resultado = mysqli_query($conexion, $query);
        $bandera = 0;
        // adentro del while formateo el resultado a un array assoc para poder acceder a los campos de la tabla y mostrarlo en pantalla
        while($registro = mysqli_fetch_assoc($resultado)){
            $bandera += 1;
            if($bandera == 1){
                echo "<h2>⭐ RESUMEN ACTUAL DESTACADO ⭐</h2>";
            } elseif ($bandera == 2) {
                echo "<h2>📚 HISTORIAL DE RESÚMENES ANTERIORES</h2>";
            }
            echo "Numero de cuenta: " . $registro['num_cuenta'] . "<br>";
            echo "Periodo del resumen: " . $registro['periodo'] . "<br>";
            echo "Fecha de vencimiento: " . $registro['fecha_vencimiento'] . "<br>";
            echo "Monto a pagar: $" . $registro['total_a_pagar'] . "<br>";
            echo "Pago mínimo: " . $registro['pago_minimo'] . "<br>";
            echo "<hr>"; // Una línea para separar cada resumen
        }
        if($bandera == 0){
            echo "<h2>NO HAY RESUMENES EMITIDOS PARA " . strtoupper($nombre_usuario) . "</h2>";
        }
    }


    echo "<div style='background-color: #e8fff4; border: 1px solid #77ff00; padding: 20px; border-radius: 8px; max-width: 500px; text-align: center; font-family: Arial, sans-serif; display: flex; flex-direction: column; align-items: center; margin: 150px auto;'>";
    echo "<h1>Bienvenido a tu Home Banking, " . $nombre_usuario . "</h1>";
    
    // Botón de cerrar sesión (es un simple enlace que viaja por GET hacia salir.php)
    echo "<br><br>";
    echo "<a href='salir.php' style='background-color: #d80000; color: white; text-decoration: none; padding: 10px 20px; border-radius: 5px; font-weight: bold;'>Cerrar Sesión</a>";
    
    echo "</div>";
    mysqli_close($conexion);
?>