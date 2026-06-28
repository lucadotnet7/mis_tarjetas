<?php

    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        // atrapo los datos del formulario
        $documento = $_POST['documento'];
        $nombre_usuario = $_POST['usuario'];
        $passwordA = $_POST['passwordA'];
        $passwordB = $_POST['passwordB'];
        
        // Validación de campos obligatorios
        $errores = [];

        if(empty($documento)) {
            $errores[] = "El número de documento es obligatorio.";
        }
        if(empty($nombre_usuario)){
            $errores[] = "El nombre de usuario es obligatorio.";
        }
        if(empty($passwordA) ){
            $errores[] = "La contraseña es obligatoria.";
        }
        if($passwordA !== $passwordB){
            $errores[] = "Las contraseñas no coinciden.";
        }
        
        if(empty($errores)){
            // Conexión a la base de datos
            $conexion = mysqli_connect("db", "root", "root", "mi_banco_db");
            if(!$conexion){
                echo "Error al conectar a la base de datos: " . mysqli_connect_error();
            }
            else{
                // Buscar al usuario
                $query_id = "SELECT * FROM usuarios WHERE documento = '$documento'";
                $result_id = mysqli_query($conexion, $query_id);
                
                if(mysqli_num_rows($result_id) > 0 ) {
                    $registro = mysqli_fetch_assoc($result_id);
                    // creo el array assoc para acceder al campo nombre de usuario , se supone que si tiene nombre de usuario tambien va a tener password (validado antes por quien creo el usuario)

                    // Usamos empty() para atrapar NULL o strings vacíos
                    if(empty($registro['usuario'])){
                        // si el usuario tiene usuario , pregunto si es null para actualizarlo , si no es null entonces ya tiene usuario y password y no se puede actualizar
                        $query_update = "UPDATE usuarios SET usuario ='$nombre_usuario' , password = '$passwordA' WHERE documento = '$documento'";

                        if(mysqli_query($conexion, $query_update)){
                            // Cajita verde de éxito
                            echo "<div style='background-color: #d4edda; border: 1px solid #c3e6cb; padding: 20px; border-radius: 8px; max-width: 500px; text-align: center; font-family: Arial, sans-serif;'>";
                            echo "<h2 style='color: #155724; margin-top: 0;'>¡Activación Exitosa!</h2>";
                            echo "<p style='color: #155724;'>Tu usuario y contraseña se guardaron correctamente.</p>";
                            echo "<br>";
                            // Botón para ir al login
                            echo "<a href='../frontend/ingreso.html' style='background-color: #28a745; color: white; text-decoration: none; padding: 10px 20px; font-size: 16px; border-radius: 5px; display: inline-block;'>Ir a Iniciar Sesión</a>";
                            echo "</div>";
                        } else {
                            echo "<p style='color: red;'>Error al actualizar el usuario: " . mysqli_error($conexion) . "</p>";
                        }
                        
                    } else {
                        // si llego aca es por que el usuario ya tiene usuario y password y no se puede actualizar
                        // deberia iniciar sesion , por eso lo mando con el link a login.php
                        echo "<p style='color: red;'>El número de documento ya tiene un usuario registrado.</p>";
                        echo "<p style='color: red;'>Inicie sesión con su usuario y contraseña.</p>";
                        // Ojo acá: asegurate de que tu archivo de login se llame login.php y no ingreso.php o ingreso.html
                        echo "<p style='color: red;'>Hacé click en el siguiente enlace para iniciar sesión. <a href='../frontend/ingreso.html'>Iniciar sesión</a></p>";
                    }

                }
                else{
                    // si entro aca es por que no el documento no tiene cuenta de alta , debe ir al banco o pedirle a luquita que le haga el alta de cuenta
                    echo "<p style='color: red;'>El número de documento NO está registrado.</p>";
                    echo "<p style='color: red;'>Por favor, verifique el número de documento ingresado o acérquese a la entidad bancaria para abrir su cuenta.</p>";
                    echo  "Volver al <a href='../frontend/registro.html'>formulario de registro</a>";
                }
                
                // Siempre es buena práctica cerrar la conexión al final
                mysqli_close($conexion);
            }

        } else {
            // Mostrar los errores de validación de forma prolija
            echo "<div style='background-color: #ffe8e8; border: 1px solid #ff0000; padding: 20px; border-radius: 8px; max-width: 500px; font-family: Arial, sans-serif;'>";
            echo "<h3 style='color: #d80000; margin-top: 0;'>Por favor, revisá los siguientes datos:</h3>";
            
            // Abrimos una lista con viñetas para que quede ordenado
            echo "<ul style='color: #d80000; font-weight: bold;'>";
            foreach ($errores as $error) {
                echo "<li style='margin-bottom: 8px;'>$error</li>"; // El li ya hace el salto de línea automático
            }
            echo "</ul>";
            
            // Botón mágico para volver atrás sin perder lo tipeado
            echo "<br>";
            echo "<button onclick='history.back()' style='background-color: #d80000; color: white; border: none; padding: 10px 20px; font-size: 16px; border-radius: 5px; cursor: pointer;'>Volver al formulario</button>";
            
            echo "</div>";
        }
    }

?>