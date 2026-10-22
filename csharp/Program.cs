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
                Console.WriteLine("===== REGISTRO DE EXPEDIENTES =====");
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

            Console.WriteLine("--- REGISTRO DE EXPEDIENTE ---");
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
        
        static void ListarExpedientes(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int cantidad)
        {
            Console.WriteLine("--- EXPEDIENTES REGISTRADOS ---");

            if (cantidad == 0)
            {
                Console.WriteLine("Todavía no hay expedientes registrados.");
                return;
            }

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine("Expediente " + (i + 1));
                MostrarExpediente(codigos, remitentes, dnis, tiposDocumento,
                    asuntos, folios, fechasHoras, estados, i);
            }
        }
        
        static void BuscarExpediente(
            string[] codigos, string[] remitentes, string[] dnis,
            string[] tiposDocumento, string[] asuntos, int[] folios,
            string[] fechasHoras, string[] estados, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes para buscar.");
                return;
            }

            string codigo = LeerTextoObligatorio("Ingrese el código que desea buscar: ").ToUpper();
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
        
        static int BuscarPosicion(string[] codigos, int cantidad, string codigo)
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
        
        static void OrdenarExpedientes(
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
        
        static void IntercambiarExpedientes(
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
        
        static void MostrarExpediente(
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

        static string GenerarCodigo(int numero)
        {
            if (numero < 10) return "EXP00" + numero;
            if (numero < 100) return "EXP0" + numero;
            return "EXP" + numero;
        }

        static string LeerTextoObligatorio(string mensaje)
        {
            string texto;
            do
            {
                Console.Write(mensaje);
                texto = (Console.ReadLine() ?? "").Trim();
                if (texto.Length == 0)
                {
                    Console.WriteLine("Este dato es obligatorio.");
                }
            } while (texto.Length == 0);

            return texto;
        }

        static string LeerRespuestaSN(string mensaje)
        {
            string respuesta;
            do
            {
                respuesta = LeerTextoObligatorio(mensaje).ToUpper();

                if (respuesta == "SI" || respuesta == "SÍ")
                {
                    respuesta = "S";
                }
                else if (respuesta == "NO")
                {
                    respuesta = "N";
                }

                if (respuesta != "S" && respuesta != "N")
                {
                    Console.WriteLine("Responda Si/No (o S/N).");
                }
            } while (respuesta != "S" && respuesta != "N");

            return respuesta;
        }

        static string LeerDNI()
        {
            string dni;
            do
            {
                dni = LeerTextoObligatorio("DNI de 8 dígitos: ");
                if (dni.Length != 8 || !EsSoloDigitos(dni))
                {
                    Console.WriteLine("El DNI debe contener exactamente 8 dígitos.");
                }
            } while (dni.Length != 8 || !EsSoloDigitos(dni));

            return dni;
        }

        static int LeerEnteroPositivo(string mensaje)
        {
            string entrada;
            int valor = 0;
            do
            {
                entrada = LeerTextoObligatorio(mensaje);
                if (!EsSoloDigitos(entrada) || !int.TryParse(entrada, out valor) || valor <= 0)
                {
                    Console.WriteLine("Ingrese un número entero mayor que cero.");
                    valor = 0;
                }
            } while (valor <= 0);

            return valor;
        }

        static int LeerEnteroEnRango(string mensaje, int minimo, int maximo)
        {
            string entrada;
            int valor = 0;
            do
            {
                entrada = LeerTextoObligatorio(mensaje);
                if (!EsSoloDigitos(entrada) || !int.TryParse(entrada, out valor) || valor < minimo || valor > maximo)
                {
                    Console.WriteLine("Ingrese un número entre " + minimo + " y " + maximo + ".");
                    valor = minimo - 1;
                }
            } while (valor < minimo || valor > maximo);

            return valor;
        }

        static string LeerFechaHora()
        {
            string fechaHora;
            do
            {
                fechaHora = LeerTextoObligatorio("Fecha y hora de recepción (dd/MM/aaaa HH:mm): ");
                if (!FechaHoraValida(fechaHora))
                {
                    Console.WriteLine("Fecha u hora inválida. Use dd/MM/aaaa HH:mm, por ejemplo 09/07/2026 08:45.");
                }
            } while (!FechaHoraValida(fechaHora));

            return fechaHora;
        }

        static bool FechaHoraValida(string fechaHora)
        {
            if (fechaHora.Length != 16 || fechaHora[2] != '/' || fechaHora[5] != '/' ||
                fechaHora[10] != ' ' || fechaHora[13] != ':')
            {
                return false;
            }

            string diaTexto = fechaHora.Substring(0, 2);
            string mesTexto = fechaHora.Substring(3, 2);
            string anioTexto = fechaHora.Substring(6, 4);
            string horaTexto = fechaHora.Substring(11, 2);
            string minutoTexto = fechaHora.Substring(14, 2);

            if (!EsSoloDigitos(diaTexto) || !EsSoloDigitos(mesTexto) ||
                !EsSoloDigitos(anioTexto) || !EsSoloDigitos(horaTexto) ||
                !EsSoloDigitos(minutoTexto))
            {
                return false;
            }

            int dia = int.Parse(diaTexto);
            int mes = int.Parse(mesTexto);
            int anio = int.Parse(anioTexto);
            int hora = int.Parse(horaTexto);
            int minuto = int.Parse(minutoTexto);

            if (anio < 1 || mes < 1 || mes > 12 || hora < 0 || hora > 23 || minuto < 0 || minuto > 59)
            {
                return false;
            }

            int diasMes;
            if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
            {
                diasMes = 30;
            }
            else if (mes == 2)
            {
                bool bisiesto = (anio % 400 == 0) || (anio % 4 == 0 && anio % 100 != 0);
                diasMes = bisiesto ? 29 : 28;
            }
            else
            {
                diasMes = 31;
            }

            return dia >= 1 && dia <= diasMes;
        }

        static bool EsSoloDigitos(string texto)
        {
            if (texto.Length == 0) return false;

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] < '0' || texto[i] > '9') return false;
            }

            return true;
        }
    }
}
