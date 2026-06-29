
// Aca atrapamos el form que va a estar esuchando todo el tiempo
const formRegistro = document.getElementById('formRegistro');

// Si existe formRegistro (es decir, si estamos en registro.html), ejecuta esto:
if (formRegistro) {

    /*Aca usamos un metodo para que cuando el usuario
    aprete el boton de enviar los datos `submit`
    frene y hagamos un control de los datos que ingreo antes de enviarlos
    a backend. */
    formRegistro.addEventListener('submit', function(evento) {

        // Usamos el metodo para frenar la accion
        evento.preventDefault();

        const passA = document.getElementById('passA').value;
        const passB = document.getElementById('passB').value;
        const tipoDoc = document.getElementById('tipoDoc').value;
        const documento = document.getElementById('documento').value;
        const usuario = document.getElementById('usuario').value;

        // 1ra validacion: Vemos si las contraseñas coinciden.
        if (passA !== passB) {
            alert('Las contraseñas no coinciden.');
            return;
        }

        // 2da Validación: Documento (Dependiendo de si es DNI o Pasaporte)
        if (tipoDoc === "DNI") {

            // Verificamos si escribió letras (isNaN da true si NO es un número)
            if (isNaN(documento)) {
                alert('El DNI debe contener únicamente números (sin puntos ni letras).');
                return;
            }

            // Verificamos el largo (entre 7 y 8 caracteres)
            if (documento.length < 7 || documento.length > 8) {
                alert('El DNI debe tener entre 7 y 8 números.');
                return;
            }

        } else if (tipoDoc === "PASAPORTE") {

            // Como el pasaporte puede tener letras, solo validamos que no esté casi vacío
            if (documento.length < 5) {
                alert('El número de pasaporte ingresado es demasiado corto.');
                return;
            }
        }

        formRegistro.submit();
    });
}


const formIngreso = document.getElementById('formIngreso');

if (formIngreso) {

    formIngreso.addEventListener('submit', function(evento) {

        evento.preventDefault();

        const tipoDoc = document.getElementById('tipoDoc').value;
        const documento = document.getElementById('documento').value;

        if (tipoDoc === "DNI") {

            // Verificamos si escribió letras (isNaN da true si NO es un número)
            if (isNaN(documento)) {
                alert('El DNI debe contener únicamente números (sin puntos ni letras).');
                return;
            }

            // Verificamos el largo (entre 7 y 8 caracteres)
            if (documento.length < 7 || documento.length > 8) {
                alert('El DNI debe tener entre 7 y 8 números.');
                return;
            }

        } else if (tipoDoc === "PASAPORTE") {

            // Como el pasaporte puede tener letras, solo validamos que no esté casi vacío
            if (documento.length < 5) {
                alert('El número de pasaporte ingresado es demasiado corto.');
                return;
            }
        }

        formIngreso.submit();
    });
}