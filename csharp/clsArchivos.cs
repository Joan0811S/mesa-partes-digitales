using System;
using System.IO;

namespace ExpedientesEP
{
    // Guardado y recuperación secuencial de expedientes en expedientes.txt.
    public class clsArchivos
    {
        public static void GuardarExpedientes(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int cantidad, string rutaArchivo)
        {
            if (cantidad <= 0)
            {
                Console.WriteLine("No hay expedientes para guardar. El archivo existente no fue modificado.");
                return;
            }

            try
            {
                string rutaCompleta = Path.GetFullPath(rutaArchivo);
                string? carpeta = Path.GetDirectoryName(rutaCompleta);

                if (!string.IsNullOrEmpty(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                using (StreamWriter archivo = new StreamWriter(rutaCompleta, false))
                {
                    for (int i = 0; i < cantidad; i++)
                    {
                        // Cada expediente ocupa ocho líneas, una por cada campo.
                        archivo.WriteLine(codigos[i]);
                        archivo.WriteLine(remitentes[i]);
                        archivo.WriteLine(dnis[i]);
                        archivo.WriteLine(tiposDocumento[i]);
                        archivo.WriteLine(asuntos[i]);
                        archivo.WriteLine(folios[i]);
                        archivo.WriteLine(fechasHoras[i]);
                        archivo.WriteLine(estados[i]);
                    }
                }

                Console.WriteLine("Se guardaron " + cantidad + " expedientes correctamente.");
                Console.WriteLine("Ubicación del archivo: " + rutaCompleta);
            }
            catch (IOException ex)
            {
                Console.WriteLine("No se pudo guardar el archivo. Detalle: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("No hay permisos para guardar el archivo. Detalle: " + ex.Message);
            }
        }

        public static int CargarExpedientes(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int cantidadActual,
            int capacidadMaxima, string rutaArchivo)
        {
            string rutaCompleta;
                try
                {
                    rutaCompleta = Path.GetFullPath(rutaArchivo);
                }
                catch (Exception ex) when (ex is ArgumentException || ex is NotSupportedException || ex is PathTooLongException)
                {
                    Console.WriteLine("La ruta del archivo no es válida. Se conservan los registros en memoria. Detalle: " + ex.Message);
                    return cantidadActual;
                }

                if (!File.Exists(rutaCompleta))
            {
                Console.WriteLine("No se encontró el archivo. Se conservan los registros que están en memoria.");
                return cantidadActual;
            }

            // Los arreglos temporales evitan perder los datos actuales si falla la lectura.
            string[] codigosCargados = new string[capacidadMaxima];
            string[] remitentesCargados = new string[capacidadMaxima];
            string[] dnisCargados = new string[capacidadMaxima];
            string[] tiposCargados = new string[capacidadMaxima];
            string[] asuntosCargados = new string[capacidadMaxima];
            int[] foliosCargados = new int[capacidadMaxima];
            string[] fechasCargadas = new string[capacidadMaxima];
            string[] estadosCargados = new string[capacidadMaxima];

            int cantidadCargada = 0;
            int omitidos = 0;

            try
            {
                using (StreamReader archivo = new StreamReader(rutaCompleta))
                {
                    while (!archivo.EndOfStream)
                    {
                        string codigo = archivo.ReadLine() ?? "";
                        string remitente = archivo.ReadLine() ?? "";
                        string dni = archivo.ReadLine() ?? "";
                        string tipoDocumento = archivo.ReadLine() ?? "";
                        string asunto = archivo.ReadLine() ?? "";
                        string foliosTexto = archivo.ReadLine() ?? "";
                        string fechaHora = archivo.ReadLine() ?? "";
                        string estado = archivo.ReadLine() ?? "";

                        int numeroFolios = 0;
                        bool dniValido = dni == "No corresponde" ||
                            (dni.Length == 8 && clsValidaciones.EsSoloDigitos(dni));
                        bool registroValido = !string.IsNullOrWhiteSpace(codigo) &&
                            !string.IsNullOrWhiteSpace(remitente) &&
                            dniValido &&
                            !string.IsNullOrWhiteSpace(tipoDocumento) &&
                            !string.IsNullOrWhiteSpace(asunto) &&
                            int.TryParse(foliosTexto, out numeroFolios) && numeroFolios > 0 &&
                            clsValidaciones.FechaHoraValida(fechaHora) &&
                            !string.IsNullOrWhiteSpace(estado);

                        codigo = codigo.Trim().ToUpper();
                        if (!registroValido ||
                            clsExpedientes.BuscarPosicion(codigosCargados, cantidadCargada, codigo) >= 0 ||
                            cantidadCargada >= capacidadMaxima)
                        {
                            omitidos++;
                            continue;
                        }

                        codigosCargados[cantidadCargada] = codigo;
                        remitentesCargados[cantidadCargada] = remitente;
                        dnisCargados[cantidadCargada] = dni;
                        tiposCargados[cantidadCargada] = tipoDocumento;
                        asuntosCargados[cantidadCargada] = asunto;
                        foliosCargados[cantidadCargada] = numeroFolios;
                        fechasCargadas[cantidadCargada] = fechaHora;
                        estadosCargados[cantidadCargada] = estado;
                        cantidadCargada++;
                    }
                }

                // Solo se reemplaza el contenido en memoria cuando la lectura termina correctamente.
                Array.Copy(codigosCargados, codigos, capacidadMaxima);
                Array.Copy(remitentesCargados, remitentes, capacidadMaxima);
                Array.Copy(dnisCargados, dnis, capacidadMaxima);
                Array.Copy(tiposCargados, tiposDocumento, capacidadMaxima);
                Array.Copy(asuntosCargados, asuntos, capacidadMaxima);
                Array.Copy(foliosCargados, folios, capacidadMaxima);
                Array.Copy(fechasCargadas, fechasHoras, capacidadMaxima);
                Array.Copy(estadosCargados, estados, capacidadMaxima);

                Console.WriteLine("Se cargaron " + cantidadCargada + " expedientes desde el archivo.");
                if (omitidos > 0)
                {
                    Console.WriteLine("Se omitieron " + omitidos + " registros incompletos, inválidos o duplicados.");
                }
                Console.WriteLine("Archivo leído: " + rutaCompleta);
                return cantidadCargada;
            }
            catch (IOException ex)
            {
                Console.WriteLine("No se pudo leer el archivo. Se conservan los registros en memoria. Detalle: " + ex.Message);
                return cantidadActual;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("No hay permisos para leer el archivo. Se conservan los registros en memoria. Detalle: " + ex.Message);
                return cantidadActual;
            }
        }
    }
}
