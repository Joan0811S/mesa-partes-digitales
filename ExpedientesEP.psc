Algoritmo ExpedientesEP
	Definir capacidadMaxima, cantidad, eleccion Como Entero
	Definir respuestaCarga Como Caracter
	Definir codigos, remitentes, dnis, tiposDocumento, asuntos, fechasHoras, estados Como Caracter
	Definir folios Como Entero

	capacidadMaxima <- 100
	Dimension codigos[100]
	Dimension remitentes[100]
	Dimension dnis[100]
	Dimension tiposDocumento[100]
	Dimension asuntos[100]
	Dimension folios[100]
	Dimension fechasHoras[100]
	Dimension estados[100]

	cantidad <- 0

	Repetir
		Escribir ""
		Escribir "===== REGISTRO DE EXPEDIENTES - MESA DE PARTES DIGITAL ====="
		Escribir "1. Registrar expediente"
		Escribir "2. Listar expedientes"
		Escribir "3. Buscar expediente por codigo"
		Escribir "4. Ordenar expedientes por codigo"
		Escribir "5. Guardar expedientes en archivo"
		Escribir "6. Cargar expedientes desde archivo"
		Escribir "0. Salir"

		eleccion <- LeerEnteroEnRango("Seleccione una opcion del 0 al 6 > ", 0, 6)

		Segun eleccion Hacer
			1:
				cantidad <- RegistrarExpediente(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad, capacidadMaxima)
			2:
				ListarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
			3:
				BuscarExpediente(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
			4:
				OrdenarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
			5:
				GuardarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
			6:
				respuestaCarga <- LeerRespuestaSN("La carga reemplazara los registros en memoria. Desea continuar? (S/N): ")
				Si respuestaCarga = "S" Entonces
					cantidad <- CargarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, capacidadMaxima)
				SiNo
					Escribir "Carga cancelada. Los registros en memoria se mantienen."
				FinSi
			0:
				Escribir "Programa finalizado. Recuerde guardar los cambios con la opcion 5 antes de salir."
		FinSegun
	Hasta Que eleccion = 0
FinAlgoritmo

SubProceso nuevaCantidad <- RegistrarExpediente(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad, capacidadMaxima)
	Definir nuevaCantidad, posicion, numero, numeroFolios Como Entero
	Definir respuesta, codigo, remitente, esPersona, dni, tipoDocumento, asunto, fechaHora Como Caracter

	nuevaCantidad <- cantidad

	Si cantidad >= capacidadMaxima Entonces
		Escribir "No se puede registrar: se alcanzo la capacidad maxima."
	SiNo
		Escribir ""
		Escribir "--- REGISTRO DE EXPEDIENTE ---"
		respuesta <- LeerRespuestaSN("El documento ya tiene numero de expediente? (S/N): ")

		Si respuesta = "S" Entonces
			Repetir
				codigo <- Mayusculas(LeerTextoObligatorio("Ingrese el numero o codigo de expediente: "))
				posicion <- BuscarPosicion(codigos, cantidad, codigo)
				Si posicion <> -1 Entonces
					Escribir "Ese codigo ya existe. Ingrese el numero correcto sin duplicarlo."
				FinSi
			Hasta Que posicion = -1
		SiNo
			numero <- cantidad + 1
			codigo <- GenerarCodigo(numero)
			Mientras BuscarPosicion(codigos, cantidad, codigo) <> -1 Hacer
				numero <- numero + 1
				codigo <- GenerarCodigo(numero)
			FinMientras
			Escribir "Numero de expediente generado: ", codigo
		FinSi

		remitente <- LeerTextoObligatorio("Nombre del remitente o institucion: ")
		esPersona <- LeerRespuestaSN("El remitente es una persona natural? (S/N): ")

		Si esPersona = "S" Entonces
			dni <- LeerDNI
		SiNo
			dni <- "No corresponde"
		FinSi

		tipoDocumento <- LeerTextoObligatorio("Tipo de documento o tramite: ")
		asunto <- LeerTextoObligatorio("Asunto del documento: ")
		numeroFolios <- LeerEnteroPositivo("Numero de folios: ")
		fechaHora <- LeerFechaHora

		nuevaCantidad <- cantidad + 1
		codigos[nuevaCantidad] <- codigo
		remitentes[nuevaCantidad] <- remitente
		dnis[nuevaCantidad] <- dni
		tiposDocumento[nuevaCantidad] <- tipoDocumento
		asuntos[nuevaCantidad] <- asunto
		folios[nuevaCantidad] <- numeroFolios
		fechasHoras[nuevaCantidad] <- fechaHora
		estados[nuevaCantidad] <- "Recibido"

		Escribir "Expediente registrado correctamente. Estado inicial: Recibido."
	FinSi
FinSubProceso

SubProceso ListarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
	Definir i Como Entero

	Escribir ""
	Escribir "--- EXPEDIENTES REGISTRADOS ---"

	Si cantidad = 0 Entonces
		Escribir "Todavia no hay expedientes registrados."
	SiNo
		Para i <- 1 Hasta cantidad Con Paso 1 Hacer
			Escribir ""
			Escribir "Expediente ", i
			MostrarExpediente(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, i)
		FinPara
	FinSi
FinSubProceso

SubProceso BuscarExpediente(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
	Definir codigo Como Caracter
	Definir posicion Como Entero

	Si cantidad = 0 Entonces
		Escribir "No hay expedientes para buscar."
	SiNo
		codigo <- Mayusculas(LeerTextoObligatorio("Ingrese el codigo que desea buscar: "))
		posicion <- BuscarPosicion(codigos, cantidad, codigo)

		Si posicion = -1 Entonces
			Escribir "No se encontro un expediente con ese codigo."
		SiNo
			Escribir ""
			MostrarExpediente(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, posicion)
		FinSi
	FinSi
FinSubProceso

SubProceso posicion <- BuscarPosicion(codigos, cantidad, codigo)
	Definir posicion, i Como Entero
	Definir buscado Como Caracter

	posicion <- -1
	buscado <- Mayusculas(Recortar(codigo))
	i <- 1
	Mientras i <= cantidad & posicion = -1 Hacer
		Si Mayusculas(codigos[i]) = buscado Entonces
			posicion <- i
		FinSi
		i <- i + 1
	FinMientras
FinSubProceso

SubProceso OrdenarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
	Definir i, j Como Entero

	Si cantidad = 0 Entonces
		Escribir "No hay expedientes para ordenar."
	SiNo
		Para i <- 1 Hasta cantidad - 1 Con Paso 1 Hacer
			Para j <- 1 Hasta cantidad - i Con Paso 1 Hacer
				Si Mayusculas(codigos[j]) > Mayusculas(codigos[j + 1]) Entonces
					IntercambiarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, j, j + 1)
				FinSi
			FinPara
		FinPara

		Escribir "Expedientes ordenados por codigo."
		ListarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
	FinSi
FinSubProceso

SubProceso IntercambiarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, p1, p2)
	Definir texto Como Caracter
	Definir numeroFolios Como Entero

	texto <- codigos[p1]
	codigos[p1] <- codigos[p2]
	codigos[p2] <- texto

	texto <- remitentes[p1]
	remitentes[p1] <- remitentes[p2]
	remitentes[p2] <- texto

	texto <- dnis[p1]
	dnis[p1] <- dnis[p2]
	dnis[p2] <- texto

	texto <- tiposDocumento[p1]
	tiposDocumento[p1] <- tiposDocumento[p2]
	tiposDocumento[p2] <- texto

	texto <- asuntos[p1]
	asuntos[p1] <- asuntos[p2]
	asuntos[p2] <- texto

	numeroFolios <- folios[p1]
	folios[p1] <- folios[p2]
	folios[p2] <- numeroFolios

	texto <- fechasHoras[p1]
	fechasHoras[p1] <- fechasHoras[p2]
	fechasHoras[p2] <- texto

	texto <- estados[p1]
	estados[p1] <- estados[p2]
	estados[p2] <- texto
FinSubProceso

SubProceso MostrarExpediente(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, posicion)
	Escribir "Codigo: ", codigos[posicion]
	Escribir "Remitente/institucion: ", remitentes[posicion]
	Escribir "DNI: ", dnis[posicion]
	Escribir "Tipo de documento: ", tiposDocumento[posicion]
	Escribir "Asunto: ", asuntos[posicion]
	Escribir "Numero de folios: ", folios[posicion]
	Escribir "Fecha y hora de recepcion: ", fechasHoras[posicion]
	Escribir "Estado: ", estados[posicion]
FinSubProceso

SubProceso codigo <- GenerarCodigo(numero)
	Definir codigo Como Caracter

	Si numero < 10 Entonces
		codigo <- Concatenar("EXP00", ConvertirATexto(numero))
	SiNo
		Si numero < 100 Entonces
			codigo <- Concatenar("EXP0", ConvertirATexto(numero))
		SiNo
			codigo <- Concatenar("EXP", ConvertirATexto(numero))
		FinSi
	FinSi
FinSubProceso

SubProceso GuardarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, cantidad)
	Definir i Como Entero

	Si cantidad <= 0 Entonces
		Escribir "No hay expedientes para guardar."
	SiNo
		Para i <- 1 Hasta cantidad Con Paso 1 Hacer
			Escribir "Registro ", i, " preparado para escritura:"
			Escribir "Codigo: ", codigos[i]
			Escribir "Remitente: ", remitentes[i]
			Escribir "DNI: ", dnis[i]
			Escribir "Tipo: ", tiposDocumento[i]
			Escribir "Asunto: ", asuntos[i]
			Escribir "Folios: ", folios[i]
			Escribir "Fecha y hora: ", fechasHoras[i]
			Escribir "Estado: ", estados[i]
		FinPara
		Escribir "Se prepararon ", cantidad, " expedientes para guardar."
	FinSi
FinSubProceso

SubProceso nuevaCantidad <- CargarExpedientes(codigos, remitentes, dnis, tiposDocumento, asuntos, folios, fechasHoras, estados, capacidadMaxima)
	Definir nuevaCantidad, numeroFolios, omitidos, i, registrosArchivo Como Entero
	Definir codigoArchivo, remitenteArchivo, dniArchivo, tipoArchivo Como Caracter
	Definir asuntoArchivo, foliosTexto, fechaArchivo, estadoArchivo Como Caracter
	Definir registroValido, dniValido Como Logico
	Definir codigosAux, remitentesAux, dnisAux, tiposAux, asuntosAux, fechasAux, estadosAux Como Caracter
	Definir foliosAux Como Entero

	Dimension codigosAux[100]
	Dimension remitentesAux[100]
	Dimension dnisAux[100]
	Dimension tiposAux[100]
	Dimension asuntosAux[100]
	Dimension foliosAux[100]
	Dimension fechasAux[100]
	Dimension estadosAux[100]

	Para i <- 1 Hasta capacidadMaxima Con Paso 1 Hacer
		codigosAux[i] <- ""
		remitentesAux[i] <- ""
		dnisAux[i] <- ""
		tiposAux[i] <- ""
		asuntosAux[i] <- ""
		foliosAux[i] <- 0
		fechasAux[i] <- ""
		estadosAux[i] <- ""
	FinPara

	nuevaCantidad <- 0
	omitidos <- 0
	registrosArchivo <- LeerEnteroEnRango("Cantidad de registros que se van a procesar (0-100): ", 0, capacidadMaxima)

	Para i <- 1 Hasta registrosArchivo Con Paso 1 Hacer
		Escribir ""
		Escribir "Ingrese los datos del registro numero ", i
		Escribir Sin Saltar "Codigo: "
		Leer codigoArchivo
		Escribir Sin Saltar "Remitente: "
		Leer remitenteArchivo
		Escribir Sin Saltar "DNI (8 digitos o No corresponde): "
		Leer dniArchivo
		Escribir Sin Saltar "Tipo de documento: "
		Leer tipoArchivo
		Escribir Sin Saltar "Asunto: "
		Leer asuntoArchivo
		Escribir Sin Saltar "Numero de folios: "
		Leer foliosTexto
		Escribir Sin Saltar "Fecha y hora (dd/MM/aaaa HH:mm): "
		Leer fechaArchivo
		Escribir Sin Saltar "Estado: "
		Leer estadoArchivo

		dniValido <- (dniArchivo = "No corresponde") | (Longitud(dniArchivo) = 8 & EsSoloDigitos(dniArchivo))
		registroValido <- Falso
		numeroFolios <- 0

		Si Longitud(codigoArchivo) > 0 & Longitud(remitenteArchivo) > 0 & dniValido & Longitud(tipoArchivo) > 0 & Longitud(asuntoArchivo) > 0 & EsSoloDigitos(foliosTexto) & FechaHoraValida(fechaArchivo) & Longitud(estadoArchivo) > 0 Entonces
			numeroFolios <- ConvertirANumero(foliosTexto)
			registroValido <- numeroFolios > 0
		FinSi

		Si registroValido & nuevaCantidad < capacidadMaxima & BuscarPosicion(codigosAux, nuevaCantidad, Mayusculas(Recortar(codigoArchivo))) = -1 Entonces
			nuevaCantidad <- nuevaCantidad + 1
			codigosAux[nuevaCantidad] <- Mayusculas(Recortar(codigoArchivo))
			remitentesAux[nuevaCantidad] <- remitenteArchivo
			dnisAux[nuevaCantidad] <- dniArchivo
			tiposAux[nuevaCantidad] <- tipoArchivo
			asuntosAux[nuevaCantidad] <- asuntoArchivo
			foliosAux[nuevaCantidad] <- numeroFolios
			fechasAux[nuevaCantidad] <- fechaArchivo
			estadosAux[nuevaCantidad] <- estadoArchivo
		SiNo
			omitidos <- omitidos + 1
			Escribir "Registro omitido por datos invalidos o codigo duplicado."
		FinSi
	FinPara

	Para i <- 1 Hasta capacidadMaxima Con Paso 1 Hacer
		codigos[i] <- codigosAux[i]
		remitentes[i] <- remitentesAux[i]
		dnis[i] <- dnisAux[i]
		tiposDocumento[i] <- tiposAux[i]
		asuntos[i] <- asuntosAux[i]
		folios[i] <- foliosAux[i]
		fechasHoras[i] <- fechasAux[i]
		estados[i] <- estadosAux[i]
	FinPara

	Escribir "Se procesaron ", nuevaCantidad, " expedientes validos."
	Si omitidos > 0 Entonces
		Escribir "Se omitieron ", omitidos, " registros incompletos, invalidos o duplicados."
	FinSi
FinSubProceso

SubProceso texto <- LeerTextoObligatorio(mensaje)
	Definir texto Como Caracter

	Repetir
		Escribir Sin Saltar mensaje
		Leer texto
		texto <- Recortar(texto)
		Si Longitud(texto) = 0 Entonces
			Escribir "Este dato es obligatorio."
		FinSi
	Hasta Que Longitud(texto) > 0
FinSubProceso

SubProceso respuesta <- LeerRespuestaSN(mensaje)
	Definir respuesta Como Caracter

	Repetir
		respuesta <- Mayusculas(LeerTextoObligatorio(mensaje))
		Si respuesta <> "S" & respuesta <> "N" Entonces
			Escribir "Responda solamente S o N."
		FinSi
	Hasta Que respuesta = "S" | respuesta = "N"
FinSubProceso

SubProceso dni <- LeerDNI
	Definir dni Como Caracter
	Definir valido Como Logico

	Repetir
		dni <- LeerTextoObligatorio("DNI de 8 digitos: ")
		valido <- (Longitud(dni) = 8) & EsSoloDigitos(dni)
		Si ~valido Entonces
			Escribir "El DNI debe contener exactamente 8 digitos."
		FinSi
	Hasta Que valido
FinSubProceso

SubProceso valor <- LeerEnteroPositivo(mensaje)
	Definir valor Como Entero
	Definir entrada Como Caracter

	Repetir
		entrada <- LeerTextoObligatorio(mensaje)
		Si EsSoloDigitos(entrada) & Longitud(entrada) <= 9 Entonces
			valor <- ConvertirANumero(entrada)
		SiNo
			valor <- 0
		FinSi
		Si valor <= 0 Entonces
			Escribir "Ingrese un numero entero mayor que cero."
		FinSi
	Hasta Que valor > 0
FinSubProceso

SubProceso valor <- LeerEnteroEnRango(mensaje, minimo, maximo)
	Definir valor Como Entero
	Definir entrada Como Caracter

	Repetir
		entrada <- LeerTextoObligatorio(mensaje)
		Si EsSoloDigitos(entrada) & Longitud(entrada) <= 9 Entonces
			valor <- ConvertirANumero(entrada)
		SiNo
			valor <- minimo - 1
		FinSi
		Si valor < minimo | valor > maximo Entonces
			Escribir "Ingrese un numero entre ", minimo, " y ", maximo, "."
		FinSi
	Hasta Que valor >= minimo & valor <= maximo
FinSubProceso

SubProceso fechaHora <- LeerFechaHora
	Definir fechaHora Como Caracter
	Definir valida Como Logico

	Repetir
		fechaHora <- LeerTextoObligatorio("Fecha y hora de recepcion (dd/MM/aaaa HH:mm): ")
		valida <- FechaHoraValida(fechaHora)
		Si ~valida Entonces
			Escribir "Fecha u hora invalida. Use dd/MM/aaaa HH:mm, por ejemplo 09/07/2026 08:45."
		FinSi
	Hasta Que valida
FinSubProceso

SubProceso valida <- FechaHoraValida(fechaHora)
	Definir valida, bisiesto Como Logico
	Definir diaTexto, mesTexto, anioTexto, horaTexto, minutoTexto Como Caracter
	Definir dia, mes, anio, hora, minuto, diasMes Como Entero

	valida <- Falso

	Si Longitud(fechaHora) = 16 Entonces
		Si Trozo(fechaHora, 3, 3) = "/" & Trozo(fechaHora, 6, 6) = "/" & Trozo(fechaHora, 11, 11) = " " & Trozo(fechaHora, 14, 14) = ":" Entonces
			diaTexto <- Trozo(fechaHora, 1, 2)
			mesTexto <- Trozo(fechaHora, 4, 5)
			anioTexto <- Trozo(fechaHora, 7, 10)
			horaTexto <- Trozo(fechaHora, 12, 13)
			minutoTexto <- Trozo(fechaHora, 15, 16)

			Si EsSoloDigitos(diaTexto) & EsSoloDigitos(mesTexto) & EsSoloDigitos(anioTexto) & EsSoloDigitos(horaTexto) & EsSoloDigitos(minutoTexto) Entonces
				dia <- ConvertirANumero(diaTexto)
				mes <- ConvertirANumero(mesTexto)
				anio <- ConvertirANumero(anioTexto)
				hora <- ConvertirANumero(horaTexto)
				minuto <- ConvertirANumero(minutoTexto)

				Si anio >= 1 & mes >= 1 & mes <= 12 & hora >= 0 & hora <= 23 & minuto >= 0 & minuto <= 59 Entonces
					Si mes = 4 | mes = 6 | mes = 9 | mes = 11 Entonces
						diasMes <- 30
					SiNo
						Si mes = 2 Entonces
							bisiesto <- (anio % 400 = 0) | (anio % 4 = 0 & anio % 100 <> 0)
							Si bisiesto Entonces
								diasMes <- 29
							SiNo
								diasMes <- 28
							FinSi
						SiNo
							diasMes <- 31
						FinSi
					FinSi

					valida <- (dia >= 1) & (dia <= diasMes)
				FinSi
			FinSi
		FinSi
	FinSi
FinSubProceso

SubProceso resultado <- Trozo(texto, posIni, posFin)
	Definir resultado Como Caracter
	Definir base Como Entero

	Si Subcadena("AB", 1, 1) = "B" Entonces
		base <- 0
	SiNo
		base <- 1
	FinSi

	Si posIni < 1 | posFin > Longitud(texto) | posIni > posFin Entonces
		resultado <- ""
	SiNo
		resultado <- Subcadena(texto, posIni - 1 + base, posFin - 1 + base)
	FinSi
FinSubProceso

SubProceso resultado <- EsSoloDigitos(texto)
	Definir resultado, encontrado Como Logico
	Definir i, k Como Entero
	Definir letra Como Caracter
	Definir digitos Como Caracter

	digitos <- "0123456789"

	Si Longitud(texto) = 0 Entonces
		resultado <- Falso
	SiNo
		resultado <- Verdadero
		i <- 1
		Mientras i <= Longitud(texto) & resultado Hacer
			letra <- Trozo(texto, i, i)
			encontrado <- Falso
			Para k <- 1 Hasta 10 Con Paso 1 Hacer
				Si letra = Trozo(digitos, k, k) Entonces
					encontrado <- Verdadero
				FinSi
			FinPara
			Si ~encontrado Entonces
				resultado <- Falso
			FinSi
			i <- i + 1
		FinMientras
	FinSi
FinSubProceso

SubProceso resultado <- Recortar(texto)
	Definir resultado Como Caracter
	Definir inicio, ultimo Como Entero
	Definir seguir Como Logico

	inicio <- 1
	ultimo <- Longitud(texto)

	seguir <- Verdadero
	Mientras inicio <= ultimo & seguir Hacer
		Si Trozo(texto, inicio, inicio) = " " Entonces
			inicio <- inicio + 1
		SiNo
			seguir <- Falso
		FinSi
	FinMientras

	seguir <- Verdadero
	Mientras ultimo >= inicio & seguir Hacer
		Si Trozo(texto, ultimo, ultimo) = " " Entonces
			ultimo <- ultimo - 1
		SiNo
			seguir <- Falso
		FinSi
	FinMientras

	Si inicio > ultimo Entonces
		resultado <- ""
	SiNo
		resultado <- Trozo(texto, inicio, ultimo)
	FinSi
FinSubProcesotado <- Trozo(texto, inicio, ultimo)
	FinSi
FinSubProceso
