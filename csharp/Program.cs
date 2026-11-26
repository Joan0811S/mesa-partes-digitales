using System;

namespace ExpedientesEP
{
    internal class Program
    {
        private const int CapacidadMaxima = 100;
        private const string RutaArchivo = "expedientes.txt";

        static void Main(string[] args)
        {
            string[] codigos = new string[CapacidadMaxima];
            string[] remitentes = new string[CapacidadMaxima];
            string[] dnis = new string[CapacidadMaxima];
            string[] tiposDocumento = new string[CapacidadMaxima];
            string[] asuntos = new string[CapacidadMaxima];
            int[] folios = new int[CapacidadMaxima];
            string[] fechasHoras = new string[CapacidadMaxima];
            string[] estados = new string[CapacidadMaxima];

            int cantidad = 0;
            int opcion;

            do
            {
                Console.WriteLine("\n===== REGISTRO DE EXPEDIENTES - MESA DE PARTES DIGITAL =====");
                Console.WriteLine("1. Registrar expediente");
                Console.WriteLine("2. Listar expedientes");
                Console.WriteLine("3. Buscar expediente por código");
                Console.WriteLine("4. Ordenar expedientes por código");
                Console.WriteLine("5. Guardar expedientes en archivo");
                Console.WriteLine("6. Cargar expedientes desde archivo");
                Console.WriteLine("0. Salir");

                opcion = clsValidaciones.LeerEnteroEnRango("Seleccione una opción (0-6): ", 0, 6);

                switch (opcion)
                {
                    case 1:
                        cantidad = clsExpedientes.RegistrarExpediente(codigos, remitentes, dnis,
                            tiposDocumento, asuntos, folios, fechasHoras, estados,
                            cantidad, CapacidadMaxima);
                        break;
                    case 2:
                        clsExpedientes.ListarExpedientes(codigos, remitentes, dnis, tiposDocumento,
                            asuntos, folios, fechasHoras, estados, cantidad);
                        break;
                    case 3:
                        clsExpedientes.BuscarExpediente(codigos, remitentes, dnis, tiposDocumento,
                            asuntos, folios, fechasHoras, estados, cantidad);
                        break;
                    case 4:
                        clsExpedientes.OrdenarExpedientes(codigos, remitentes, dnis, tiposDocumento,
                            asuntos, folios, fechasHoras, estados, cantidad);
                        break;
                    case 5:
                        clsArchivos.GuardarExpedientes(codigos, remitentes, dnis, tiposDocumento,
                            asuntos, folios, fechasHoras, estados, cantidad, RutaArchivo);
                        break;
                    case 6:
                        Console.WriteLine("La carga reemplazará los registros que están actualmente en memoria.");
                        string respuestaCarga = clsValidaciones.LeerRespuestaSN("¿Desea continuar? (Si/No): ");
                        if (respuestaCarga == "S")
                        {
                            cantidad = clsArchivos.CargarExpedientes(codigos, remitentes, dnis,
                                tiposDocumento, asuntos, folios, fechasHoras, estados,
                                cantidad, CapacidadMaxima, RutaArchivo);
                        }
                        else
                        {
                            Console.WriteLine("Carga cancelada. Los registros en memoria se mantienen.");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Programa finalizado. Recuerde guardar los cambios con la opción 5 antes de salir.");
                        break;
                }
            } while (opcion != 0);
        }
    }
}
