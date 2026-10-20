# Registro de Expedientes - Avance EP

Prototipo de consola para una **mesa de partes digital**, desarrollado para el curso **Fundamentos de Programación (CIIN1205P)** dentro del proyecto *"Del archivador al algoritmo: un prototipo para la mesa de partes digital"*. El caso observado es la Mesa de Partes de la Municipalidad Distrital de Santiago de Cao.

Esta es la versión de **avance (EP)**: registra, lista, busca y ordena expedientes en memoria, usando arreglos paralelos y validaciones con cadenas.

## Integrantes

- Baltazar Mosqueira Nicolas
- Garcia Amaya Joan
- Joaquin Esteban Emanuel
- Lozano Ayala Dared
- Saravia Cordova Dereck

## Funcionalidades

| Opción | Descripción |
|--------|-------------|
| 1 | Registrar expediente |
| 2 | Listar expedientes |
| 3 | Buscar expediente por código |
| 4 | Ordenar expedientes por código |
| 0 | Salir |

### Datos que se guardan por expediente

- Código (formato `EXP001`, `EXP045`, `EXP120`)
- Remitente o institución
- DNI (8 dígitos, o "No corresponde" si el remitente no es persona natural)
- Tipo de documento o trámite
- Asunto
- Número de folios
- Fecha y hora de recepción (`dd/MM/aaaa HH:mm`)
- Estado (inicia siempre como `Recibido`)

### Registro de un expediente

1. El programa pregunta si el documento **ya tiene número de expediente**.
   - **Sí:** el usuario lo escribe y se valida que no esté duplicado.
   - **No:** el programa genera el siguiente código disponible (`EXPnnn`).
2. Se piden remitente, si es persona natural (y DNI en ese caso), tipo de documento, asunto, folios y fecha y hora.
3. El expediente se guarda con estado `Recibido`.

## Validaciones

- Los campos de texto son obligatorios (no se aceptan vacíos ni solo espacios).
- Las respuestas Sí/No solo aceptan `S` o `N`.
- El DNI debe tener exactamente 8 dígitos.
- Los folios deben ser un entero mayor que cero.
- El menú solo acepta números dentro del rango de opciones.
- La fecha y hora se valida por formato, rangos de mes, hora y minuto, y **días reales del mes** (incluye años bisiestos).
- No se permiten códigos de expediente duplicados.
- Se controla la capacidad máxima de **100 expedientes**.

## Técnicas aplicadas

- **Arreglos paralelos** (uno por campo) con una variable `cantidad` que indica cuántos registros hay.
- **Búsqueda secuencial** por código, sin distinguir mayúsculas de minúsculas.
- **Ordenamiento burbuja** por código, intercambiando todos los campos del expediente.
- **Modularidad:** cada tarea está en su propio subproceso o método (registrar, listar, buscar, ordenar, validar, leer datos).
- **Validación con cadenas:** recorrido carácter por carácter para comprobar dígitos, formato de fecha y recorte de espacios.

## Estructura del programa

| Módulo | Función |
|--------|---------|
| `RegistrarExpediente` | Captura y guarda un expediente nuevo |
| `ListarExpedientes` | Muestra todos los expedientes |
| `BuscarExpediente` / `BuscarPosicion` | Búsqueda secuencial por código |
| `OrdenarExpedientes` / `IntercambiarExpedientes` | Ordenamiento burbuja por código |
| `MostrarExpediente` | Imprime los datos de un expediente |
| `GenerarCodigo` | Genera códigos con formato `EXPnnn` |
| `LeerTextoObligatorio`, `LeerRespuestaSN`, `LeerDNI`, `LeerEnteroPositivo`, `LeerEnteroEnRango`, `LeerFechaHora` | Lectura con validación |
| `FechaHoraValida`, `EsSoloDigitos`, `Recortar` | Validaciones y utilidades de cadenas |

El diseño en pseudocódigo está en `ExpedientesEP.psc` (PSeInt) y cada subproceso corresponde a un método estático del programa en C# (`Program.cs`).

## Cómo ejecutarlo

### Versión C# (Visual Studio)

1. Abre la solución del proyecto en Visual Studio.
2. Ejecuta con `F5` (o `Ctrl + F5` para que la consola no se cierre).

También se puede ejecutar desde la terminal, dentro de la carpeta del proyecto:

```bash
dotnet run
```

### Versión pseudocódigo (PSeInt)

1. Abre `ExpedientesEP.psc` en PSeInt.
2. Pulsa **Ejecutar**.

> Nota: PSeInt trabaja con arreglos en base 1, mientras que C# lo hace en base 0.

## Limitaciones del avance EP

- Los datos solo viven **en memoria**: al cerrar el programa se pierden.
- Capacidad fija de 100 expedientes.
- El estado siempre inicia en `Recibido` y aún no se puede cambiar.

## Próximos pasos (EF)

- Búsqueda binaria sobre los expedientes ordenados.
- Función recursiva.
- Matriz resumen.
- Persistencia en archivo de texto.
- Control de versiones con Git y GitHub.
