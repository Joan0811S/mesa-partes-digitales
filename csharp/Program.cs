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
        
        static int RegistrarExpediente(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int cantidad, int capacidadMaxima)
        {
            if (cantidad >= capacidadMaxima)
            {
                Console.WriteLine("No se puede registrar: se alcanzó la capacidad máxima.");
                return cantidad;
            }

            Console.WriteLine("\n--- REGISTRO DE EXPEDIENTE ---");
            string respuesta = LeerRespuestaSN("¿El documento ya tiene número de expediente? (Si/No): ");
            string codigo;

            if (respuesta == "S")
            {
                int posicion;
                do
                {
                    codigo = LeerTextoObligatorio("Ingrese el número o código de expediente: ").ToUpper();
                    posicion = BuscarPosicion(codigos, cantidad, codigo);
                    if (posicion >= 0)
                    {
                        Console.WriteLine("Ese código ya existe. Ingrese el número correcto sin duplicarlo.");
                    }
                } while (posicion >= 0);
            }
            else
            {
                int numero = cantidad + 1;
                codigo = GenerarCodigo(numero);

                while (BuscarPosicion(codigos, cantidad, codigo) >= 0)
                {
                    numero++;
                    codigo = GenerarCodigo(numero);
                }

                Console.WriteLine("Número de expediente generado: " + codigo);
            }

            string remitente = LeerTextoObligatorio("Nombre del remitente o institución: ");
            string esPersona = LeerRespuestaSN("¿El remitente es una persona real? (Si/No): ");
            string dni;

            if (esPersona == "S")
            {
                dni = LeerDNI();
            }
            else
            {
                dni = "No corresponde";
            }

            string tipoDocumento = LeerTextoObligatorio("Tipo de documento o trámite: ");
            string asunto = LeerTextoObligatorio("Asunto del documento: ");
            int numeroFolios = LeerEnteroPositivo("Número de folios: ");
            string fechaHora = LeerFechaHora();

            codigos[cantidad] = codigo;
            remitentes[cantidad] = remitente;
            dnis[cantidad] = dni;
            tiposDocumento[cantidad] = tipoDocumento;
            asuntos[cantidad] = asunto;
            folios[cantidad] = numeroFolios;
            fechasHoras[cantidad] = fechaHora;
            estados[cantidad] = "Recibido";

            cantidad++;
            Console.WriteLine("Expediente registrado correctamente. Estado inicial: Recibido.");
            return cantidad;
        }
    }
}
