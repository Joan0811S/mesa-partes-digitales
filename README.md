# Mesa de Partes Digital — Grupo 9

Proyecto de consola en C# y pseudocódigo PSeInt para registrar, validar, buscar, ordenar y almacenar expedientes usando ocho arreglos paralelos.

## Estructura del proyecto C#

```text
csharp/
├── Program.cs
├── clsExpedientes.cs
├── clsValidaciones.cs
├── clsArchivos.cs
└── csharp.csproj
```

- `Program.cs`: contiene `Main`, declara los ocho arreglos paralelos y presenta el menú.
- `clsExpedientes.cs`: contiene el registro, listado, búsqueda secuencial, ordenamiento Burbuja y presentación de expedientes.
- `clsValidaciones.cs`: contiene las funciones para leer y validar textos, respuestas S/N, DNI, números y fecha/hora.
- `clsArchivos.cs`: contiene `GuardarExpedientes` y `CargarExpedientes`, que realizan la persistencia secuencial.
- No se crea una clase `Expediente`: los campos se mantienen en los ocho arreglos paralelos definidos para el proyecto.

## Requisitos

- .NET 10 SDK
- Visual Studio con soporte para .NET 10 o terminal con `dotnet`

## Ejecución

Desde la carpeta `csharp`:

```bash
dotnet run
```

## Opciones del menú

1. Registrar expediente
2. Listar expedientes
3. Buscar expediente por código
4. Ordenar expedientes por código
5. Guardar expedientes en archivo
6. Cargar expedientes desde archivo
0. Salir

La opción 5 crea o reemplaza `expedientes.txt` en el directorio de trabajo, escribiendo ocho líneas por cada expediente. La opción 6 lee el archivo, valida los registros y solicita confirmación porque reemplaza los arreglos que están actualmente en memoria. El programa no guarda automáticamente al salir.

## Archivo de datos

`expedientes.txt` es un archivo de texto secuencial y no debe publicarse cuando contenga datos personales reales. Para pruebas y sustentación se deben usar datos ficticios o anonimizados. El archivo está excluido del control de versiones mediante `.gitignore`.

## Pseudocódigo

`ExpedientesEP.psc` contiene el algoritmo principal y los subprocesos equivalentes a las responsabilidades de las clases C#. PSeInt utiliza subprocesos para la modularidad; no se divide en clases como C#.

## Publicación en GitHub

Agregar o actualizar estos elementos en el repositorio:

- `csharp/Program.cs` (actualizado)
- `csharp/clsExpedientes.cs` (nuevo)
- `csharp/clsValidaciones.cs` (nuevo)
- `csharp/clsArchivos.cs` (nuevo)
- `csharp/csharp.csproj` (conservar)
- `ExpedientesEP.psc` (actualizado)
- `README.md` (actualizado)
- `.gitignore` (nuevo o actualizado)

No subir carpetas `bin`, `obj`, `.vs` ni el archivo `expedientes.txt`.


- Búsqueda binaria sobre los expedientes ordenados.
- Función recursiva.
- Matriz resumen.
- Persistencia en archivo de texto.
- Control de versiones con Git y GitHub.
