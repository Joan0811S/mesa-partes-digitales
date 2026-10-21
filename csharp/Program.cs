using System;

namespace ExpedientesEP
{
    internal class Program
    {
        const int CapacidadMaxima = 100;

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
                Console.WriteLine("\n===== REGISTRO DE EXPEDIENTES - AVANCE EP =====");
                Console.WriteLine("1. Registrar expediente");
                Console.WriteLine("2. Listar expedientes");
                Console.WriteLine("3. Buscar expediente por código");
                Console.WriteLine("4. Ordenar expedientes por código");
                Console.WriteLine("0. Salir");

                opcion = LeerEnteroEnRango("Seleccione una opción (0-4): ", 0, 4);

                switch (opcion)
                {
                    case 1:
                        cantidad = RegistrarExpediente(codigos, remitentes, dnis,
                            tiposDocumento, asuntos, folios, fechasHoras, estados,
                            cantidad, CapacidadMaxima);
                        break;
                    case 2:
                        ListarExpedientes(codigos, remitentes, dnis, tiposDocumento,
                            asuntos, folios, fechasHoras, estados, cantidad);
                        break;
                    case 3:
                        BuscarExpediente(codigos, remitentes, dnis, tiposDocumento,
                            asuntos, folios, fechasHoras, estados, cantidad);
                        break;
                    case 4:
                        OrdenarExpedientes(codigos, remitentes, dnis, tiposDocumento,
                            asuntos, folios, fechasHoras, estados, cantidad);
                        break;
                    case 0:
                        Console.WriteLine("Programa finalizado. Los registros de la EP solo estaban en memoria.");
                        break;
                }
            } while (opcion != 0);
        }
    }
}
