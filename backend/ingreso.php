<?php
    session_start();

    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        $tipo_doc = $_POST['tipo_doc'];
        $documento = $_POST['documento'];
        $nombre_usuario = $_POST['usuario'];
        $password = $_POST['password'];

        // Validación de campos obligatorios
        $errores = [];

        if(empty($tipo_doc)) {
            $errores[] = "El tipo de documento es obligatorio.";
        }
        if(empty($documento)) {
            $errores[] = "El número de documento es obligatorio.";
        }
        if(empty($nombre_usuario)){
            $errores[] = "El nombre de usuario es obligatorio.";
        }
        if(empty($password)){
            $errores[] = "La contraseña es obligatoria.";
        }


        // si no hay campos vacios , entonces busco en la base de datos si el usuario existe y si la contraseña es correcta
        if(empty($errores)){
            $conexion = mysqli_connect("db", "root","root", "mi_banco_db");
            // no hubo errores , abro la conexion
            if($conexion){
                // guardo la consulta comparando los 4 campos del formulario
                $query = "SELECT * FROM usuarios WHERE '$tipo_doc' = tipo_doc AND  '$documento'  = documento AND  '$nombre_usuario' = usuario AND '$password' = password";
                $resultado = mysqli_query($conexion, $query);
                // si trajo una coincidencia , entonces guardo en la sesion el usuario y el documento para poder usarlo en otras paginas
                if(mysqli_num_rows($resultado) == 1){
                    $registro = mysqli_fetch_assoc($resultado);
                    $_SESSION['usuario'] = $registro['usuario'];
                    $_SESSION['documento'] = $registro['documento'];
                    $_SESSION['apellido'] = $registro['apellido'];
                // atrape los datos de la sesion en memoria de la pagina y redirijo a la pagina de resumen.php
                    header("Location: ../backend/resumen.php");  
                    exit();
                    // si no entro es por que no encontro coincidencias , entonces muestro un mensaje de error y un boton para volver al formulario de ingreso 
                } else {
                    echo "<div style='background-color: #ffe8e8; border: 1px solid #ff0000; padding: 25px; border-radius: 5px; max-width: 600px; display: flex; flex-direction: column; align-items: center; margin: 150px auto; font-family: Arial, sans-serif; font-size: 30px;'>";
                    echo "<p style='color: #d80000; margin: 0; font-weight: bold;'>Credenciales incorrectas.</p>";
                    echo "<p style='color: #d80000; margin: 5px 0 0 0; font-size: 14px;'>Por favor, verificá tus datos e intentá nuevamente.</p>";
                    echo "<br><button onclick='history.back()' style='background-color: #d80000; color: white; border: none; padding: 8px 15px; cursor: pointer;'>Volver</button>";
                    echo "</div>";
                }
            } else {
                echo "Error al conectar a la base de datos: " . mysqli_connect_error();
            }
            mysqli_close($conexion);

        } else{
            echo "<div style='background-color: #ffe8e8; border: 1px solid #ff0000; padding: 20px; border-radius: 8px; max-width: 500px;'>";
            echo "<ul style='color: #d80000; font-weight: bold;'>";
            foreach ($errores as $error) {
                echo "<li style='margin-bottom: 8px;'>$error</li>";
                }
            echo "</ul>";
            echo "<button onclick='history.back()' style='background-color: #d80000; color: white; border: none; padding: 10px 20px; cursor: pointer;'>Volver</button>";
            echo "</div>";
            }

        
    }


?>