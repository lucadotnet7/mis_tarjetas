# Mis Tarjetas

Portal web de consulta de liquidaciones de tarjetas de crédito para la entidad ficticia **Progra3card**. Permite a los clientes del banco activar su cuenta web, iniciar sesión y consultar el resumen de sus tarjetas. Incluye además un **backoffice de administración** por consola para que el banco gestione usuarios, tarjetas y liquidaciones.

---

## Tecnologías utilizadas

| Capa | Tecnología |
|---|---|
| Frontend | HTML5, Tailwind CSS, JavaScript |
| Backend | PHP 8.2, MySQLi |
| Backoffice | C# .NET 10 (aplicación de consola) |
| Base de datos | MySQL 8.0 |

---

## Estructura del proyecto

```
mis_tarjetas/
├── frontend/          # Interfaz web del cliente
│   ├── ingreso.html
│   ├── registro.html
│   ├── script.js
│   └── styles.css
├── backend/           # Lógica del servidor en PHP
│   ├── ingreso.php
│   ├── altas.php
│   ├── resumen.php
│   └── salir.php
├── backoffice/        # Aplicación de consola en C# para administración
│   ├── controllers/
│   ├── models/
│   ├── views/
│   └── Program.cs
├── bd.sql             # Script de creación de la base de datos y datos de prueba
├── Dockerfile
└── docker-compose.yml
```

---

## ¿Qué hace cada capa?

### FRONTEND

Contiene las páginas HTML que ve el cliente del banco. Está construido con **Tailwind CSS** para el diseño y **JavaScript puro** para la validación de formularios del lado del cliente.

- **`ingreso.html`** — Formulario de login. El usuario ingresa su tipo de documento, número de documento, usuario web y contraseña.
- **`registro.html`** — Formulario de activación de cuenta web. Un cliente que ya existe en la base de datos (cargado por el banco) puede crear aquí sus credenciales de acceso.
- **`script.js`** — Valida ambos formularios antes de enviarlos al servidor: verifica que el DNI sea numérico y tenga entre 7 y 8 dígitos, que el pasaporte tenga al menos 5 caracteres y que las contraseñas coincidan en el registro.

### BACKEND

Contiene los scripts **PHP** que procesan los formularios, acceden a la base de datos MySQL y gestionan la sesión del usuario.

- **`altas.php`** — Recibe el formulario de activación. Verifica que el documento exista en la base de datos (dado de alta previamente por el banco), que los datos personales coincidan y que el usuario no tenga credenciales ya asignadas. Si todo es válido, guarda el usuario y contraseña en la tabla `usuarios`.
- **`ingreso.php`** — Recibe el formulario de login. Valida las credenciales contra la base de datos y, si son correctas, abre una sesión PHP y redirige a `resumen.php`.
- **`resumen.php`** — Página protegida por sesión. Consulta la base de datos con un JOIN entre `liquidaciones`, `tarjetas` y `usuarios` para mostrar el resumen de tarjeta del cliente logueado, destacando el más reciente.
- **`salir.php`** — Destruye la sesión activa y redirige al login.

### BACKOFFICE

Aplicación de **consola en C#** con arquitectura **MVC** que permite al personal del banco administrar los datos del sistema sin pasar por la web.

**Menú principal:**
```
1. Agregar usuario      → Registra un nuevo cliente y le asigna una tarjeta
2. Agregar liquidación  → Carga una nueva liquidación a una tarjeta existente
3. Listar tarjetas      → Muestra todas las tarjetas del sistema
4. Detalle de tarjeta   → Muestra datos completos de tarjeta y titular por número de cuenta
5. Eliminar tarjeta     → Elimina una tarjeta y sus liquidaciones asociadas (CASCADE)
6. Salir
```

La arquitectura separa responsabilidades en tres carpetas:
- **`models/`** — Clases de datos (`Usuario`, `Tarjeta`, `Liquidacion`, `UsuarioTarjeta`) y sus servicios con acceso a la base de datos.
- **`controllers/`** — Intermediarios que conectan la vista con el servicio correspondiente.
- **`views/`** — Pantallas de consola (`MainMenu`, `UsuarioView`, `TarjetaView`, `LiquidacionView`) que muestran información y reciben input del operador.

---

## Pasos para correr la aplicación

### Requisitos previos

- XAMPP, MAMP or Docker para correr PHP
- [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado (solo para el backoffice).

---

### 1. Levantar la aplicación web (Frontend + Backend + Base de datos)

- Ubicar el proyecto en la carpeta **htdocs** del servidor de xampp.
- Ingresar desde el navegador a http://localhost:8080/mis_tarjetas/frontend/ingreso.html

---

### 2. Inicializar la base de datos

Accedé a **phpMyAdmin** en http://localhost:8081 con:
- **Usuario:** `root`
- **Contraseña:** `root`

Luego importá el archivo `bd.sql` ubicado en la raíz del proyecto:

1. Clic en **"Importar"** en el menú superior.
2. Seleccioná el archivo `bd.sql`.
3. Clic en **"Continuar"**.

Esto crea la base de datos `mi_banco_db` con las tablas `usuarios`, `tarjetas` y `liquidaciones`, y carga datos de prueba.

---

### 3. Correr el Backoffice (consola C#)

Para inicializar el proyecto Backoffice ejecute el siguiente comando

```bash
cd backoffice
dotnet run
```

> El backoffice se conecta a la base de datos en `localhost:3307`. Asegurate de que la base de datos esté corriendo.

---

## Guía de uso paso a paso

### Como operador del banco (Backoffice)

> El banco primero debe cargar al cliente en el sistema antes de que pueda activar su cuenta web.

1. Ejecutar el backoffice con `dotnet run` desde la carpeta `backoffice/`.
2. Seleccionar la opción **`1. Agregar usuario`**.
3. Ingresar los datos del cliente (tipo de doc, documento, nombre, apellido, fecha de nacimiento, email).
4. El sistema pedirá luego los datos de la tarjeta (número de tarjeta, banco emisor).
5. Para cargar un resumen mensual, seleccionar la opción **`2. Agregar liquidación`** e ingresar el número de cuenta, período, fecha de vencimiento, total a pagar y pago mínimo.

---

### Como cliente (Aplicación Web)

**Activar cuenta web (primer acceso):**

1. Ir a http://localhost:8080/frontend/registro.html
2. Completar el formulario con los mismos datos personales que el banco cargó en el backoffice (tipo de doc, documento, nombre, apellido, fecha de nacimiento, email).
3. Elegir un nombre de usuario web y una contraseña.
4. Hacer clic en **"Activar Mi Cuenta Web"**.
5. Si los datos coinciden, la cuenta queda activada y se puede iniciar sesión.

**Iniciar sesión y consultar liquidaciones:**

1. Ir a http://localhost:8080/frontend/ingreso.html
2. Ingresar tipo de documento, número de documento, usuario web y contraseña.
3. Hacer clic en **"Ingresar"**.
4. El sistema muestra el resumen actual destacado y el historial de liquidaciones anteriores.
5. Para salir, hacer clic en **"Cerrar Sesión"**.

---

## Usuarios de prueba (datos de seed)

Estos usuarios ya están cargados por el script `bd.sql` con credenciales activas:

| Nombre | Tipo Doc | Documento | Usuario Web | Contraseña |
|---|---|---|---|---|
| Carlos Gómez | DNI | 20123456 | carlos85 | clave123 |
| Ana Martínez | DNI | 30987654 | anamar | clave123 |
| Lucía Rodríguez | DNI | 40111222 | *(pendiente activación web)* | — |

> Lucía Rodríguez existe en la base de datos pero aún no activó su cuenta web. Podés probar el flujo de activación en `registro.html` con sus datos.

---

## Detener la aplicación de consola

```bash
cd backoffice
ctrl + c
```

## Integrantes del proyecto

| Nombre | Apellido | Legajo |
|---|---|---|
| Luca | Cañas | 38593 |
| Julian | Gentili | 38527 |
| Ivan | Godoy | 38524 |
