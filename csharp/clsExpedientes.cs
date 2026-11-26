using System;

namespace ExpedientesEP
{
    // Operaciones de registro, consulta y ordenamiento sobre los ocho arreglos paralelos.
    public class clsExpedientes
    {
        public static int RegistrarExpediente(
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
            string respuesta = clsValidaciones.LeerRespuestaSN("¿El documento ya tiene número de expediente? (Si/No): ");
            string codigo;

            if (respuesta == "S")
            {
                int posicion;
                do
                {
                    codigo = clsValidaciones.LeerTextoObligatorio("Ingrese el número o código de expediente: ").ToUpper();
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

            string remitente = clsValidaciones.LeerTextoObligatorio("Nombre del remitente o institución: ");
            string esPersona = clsValidaciones.LeerRespuestaSN("¿El remitente es una persona real? (Si/No): ");
            string dni;

            if (esPersona == "S")
            {
                dni = clsValidaciones.LeerDNI();
            }
            else
            {
                dni = "No corresponde";
            }

            string tipoDocumento = clsValidaciones.LeerTextoObligatorio("Tipo de documento o trámite: ");
            string asunto = clsValidaciones.LeerTextoObligatorio("Asunto del documento: ");
            int numeroFolios = clsValidaciones.LeerEnteroPositivo("Número de folios: ");
            string fechaHora = clsValidaciones.LeerFechaHora();

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

        public static void ListarExpedientes(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int cantidad)
        {
            Console.WriteLine("\n--- EXPEDIENTES REGISTRADOS ---");

            if (cantidad == 0)
            {
                Console.WriteLine("Todavía no hay expedientes registrados.");
                return;
            }

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine("\nExpediente " + (i + 1));
                MostrarExpediente(codigos, remitentes, dnis, tiposDocumento,
                    asuntos, folios, fechasHoras, estados, i);
            }
        }

        public static void BuscarExpediente(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes para buscar.");
                return;
            }

            string codigo = clsValidaciones.LeerTextoObligatorio("Ingrese el código que desea buscar: ").ToUpper();
            int posicion = BuscarPosicion(codigos, cantidad, codigo);

            if (posicion == -1)
            {
                Console.WriteLine("No se encontró un expediente con ese código.");
            }
            else
            {
                MostrarExpediente(codigos, remitentes, dnis, tiposDocumento,
                    asuntos, folios, fechasHoras, estados, posicion);
            }
        }

        public static int BuscarPosicion(string[] codigos, int cantidad, string codigo)
        {
            for (int i = 0; i < cantidad; i++)
            {
                if (codigos[i] != null && codigos[i].ToUpper() == codigo.Trim().ToUpper())
                {
                    return i;
                }
            }

            return -1;
        }

        public static void OrdenarExpedientes(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes para ordenar.");
                return;
            }

            for (int i = 0; i < cantidad - 1; i++)
            {
                for (int j = 0; j < cantidad - 1 - i; j++)
                {
                    if (string.Compare(codigos[j], codigos[j + 1], StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        IntercambiarExpedientes(codigos, remitentes, dnis,
                            tiposDocumento, asuntos, folios, fechasHoras, estados, j, j + 1);
                    }
                }
            }

            Console.WriteLine("Expedientes ordenados por código.");
            ListarExpedientes(codigos, remitentes, dnis, tiposDocumento,
                asuntos, folios, fechasHoras, estados, cantidad);
        }

        private static void IntercambiarExpedientes(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int p1, int p2)
        {
            string texto;
            int numeroFolios;

            texto = codigos[p1]; codigos[p1] = codigos[p2]; codigos[p2] = texto;
            texto = remitentes[p1]; remitentes[p1] = remitentes[p2]; remitentes[p2] = texto;
            texto = dnis[p1]; dnis[p1] = dnis[p2]; dnis[p2] = texto;
            texto = tiposDocumento[p1]; tiposDocumento[p1] = tiposDocumento[p2]; tiposDocumento[p2] = texto;
            texto = asuntos[p1]; asuntos[p1] = asuntos[p2]; asuntos[p2] = texto;
            numeroFolios = folios[p1]; folios[p1] = folios[p2]; folios[p2] = numeroFolios;
            texto = fechasHoras[p1]; fechasHoras[p1] = fechasHoras[p2]; fechasHoras[p2] = texto;
            texto = estados[p1]; estados[p1] = estados[p2]; estados[p2] = texto;
        }

        private static void MostrarExpediente(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int posicion)
        {
            Console.WriteLine("Código: " + codigos[posicion]);
            Console.WriteLine("Remitente/institución: " + remitentes[posicion]);
            Console.WriteLine("DNI: " + dnis[posicion]);
            Console.WriteLine("Tipo de documento: " + tiposDocumento[posicion]);
            Console.WriteLine("Asunto: " + asuntos[posicion]);
            Console.WriteLine("Número de folios: " + folios[posicion]);
            Console.WriteLine("Fecha y hora de recepción: " + fechasHoras[posicion]);
            Console.WriteLine("Estado: " + estados[posicion]);
        }

        private static string GenerarCodigo(int numero)
        {
            if (numero < 10) return "EXP00" + numero;
            if (numero < 100) return "EXP0" + numero;
            return "EXP" + numero;
        }
    }
}
