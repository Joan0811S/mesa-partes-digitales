using System;

namespace ExpedientesEP
{
    // Funciones para leer y validar los datos ingresados y recuperados.
    public class clsValidaciones
    {
        public static string LeerTextoObligatorio(string mensaje)
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

        public static string LeerRespuestaSN(string mensaje)
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

        public static string LeerDNI()
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

        public static int LeerEnteroPositivo(string mensaje)
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

        public static int LeerEnteroEnRango(string mensaje, int minimo, int maximo)
        {
            string entrada;
            int valor = 0;
            do
            {
                entrada = LeerTextoObligatorio(mensaje);
                if (!EsSoloDigitos(entrada) || !int.TryParse(entrada, out valor) || valor < minimo || valor > maximo)
                {
                    // Entrada inválida: vuelve a solicitar la opción sin mostrar un mensaje adicional.
                    valor = minimo - 1;
                }
            } while (valor < minimo || valor > maximo);

            return valor;
        }

        public static string LeerFechaHora()
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

        public static bool FechaHoraValida(string fechaHora)
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

        public static bool EsSoloDigitos(string texto)
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
