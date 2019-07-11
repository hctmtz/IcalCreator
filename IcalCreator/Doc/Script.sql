--+-----------------------------------------------------+
--| Archivo de configuración para la aplicación	de		|
--|	IcalFileCreator										|
--+-----------------------------------------------------+

--+-----------------------------------------------------+
--| Tablas												|
--+-----------------------------------------------------+

--Tabla que registra las cirugías que ya han sido envíadas
IF NOT EXISTS(SELECT * FROM sys.objects WHERE NAME = 'CatRegistroEventoAgenda')
CREATE TABLE dbo.CatRegistroEventoAgenda
(
	iIdEventoAgenda INT IDENTITY(1,1),
	iFolioEvento INT NOT NULL,
	iEnviado INT NOT NULL,
	iIntentosEnvio INT NOT NULL,
	dtFechaEnvio DATETIME DEFAULT GETDATE(),
	iTipoEvento INT NOT NULL -- 0: Cirugía, 1: Cita
)
GO

 --Parámetro para intervalo de fechas
IF NOT EXISTS ( SELECT * FROM sysParametro WHERE iCodParametro = 134061 )
INSERT INTO sysParametro 
( 
	iCodParametro, 
	vchDescripcion, 
	vchValor, 
	siAgrupador, 
	TIMESTAMP, 
	siSubAgrupador, 
	iReferencia, 
	siTipoDato, 
	vchConsulta_ListaValores, 
	siValorInicial, 
	siValorFinal, 
	tiActivo, 
	iIdNodo, 
	vchComentario, 
	iIdConsecutivo_tmp 
)
VALUES 
( 
	134061, 
	'Intervalo de días para envío de notificación de evento', 
	'0', 
	0,
	NULL,
	0, 
	0, 
	3250,
	'', 
	0, 
	0, 
	1, 
	0, 
	'Fecha de intervalo para envío de notificación de archivo de evento ICalendar', 
	134061 
)
GO

--+-----------------------------------------------------+
--| Procedimientos										|
--+-----------------------------------------------------+

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ppRegistrarEventoAgenda')
	DROP PROCEDURE dbo.ppRegistrarEventoAgenda
GO
CREATE PROCEDURE dbo.ppRegistrarEventoAgenda
(
	@iFolioEvento INT,
	@iTipoEvento INT
)
/*
** Nombre:					ppRegistrarEventoAgenda		
** Propósito:				Registra que evento ha sido enviado por
**							correo
**
** Campos:					--
** Dependencias: 			--	
** Error Base:				--
** Retorna:					--
**							
** Fecha creación:			4/Junio/2019
** Autor creación:			HMARTINEZ
** Csd creación:			CSD175
** Fecha modificación: 		--
** Autor modificación: 		--
** Csd modificación:		--	
** Compatibilidad:			1.75
** Revisión:				0
*/
AS

IF EXISTS(SELECT * FROM CatRegistroEventoAgenda WHERE iFolioEvento = @iFolioEvento)
UPDATE CatRegistroEventoAgenda
SET iIntentosEnvio = 
(
	SELECT iIntentosEnvio 
	FROM CatRegistroEventoAgenda 
	WHERE iFolioEvento = @iFolioEvento
) + 1
WHERE iFolioEvento = @iFolioEvento
AND iTipoEvento = @iTipoEvento

IF NOT EXISTS(SELECT * FROM CatRegistroEventoAgenda WHERE iFolioEvento = @iFolioEvento)
INSERT INTO CatRegistroEventoAgenda
SELECT 
	@iFolioEvento,
	1,
	1,
	GETDATE(),
	@iTipoEvento
GO

--+--------------------------------------------------------------------------------+--

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ppObtenerAsistentesAgenda')
	DROP PROCEDURE dbo.ppObtenerAsistentesAgenda
GO
CREATE PROCEDURE dbo.ppObtenerAsistentesAgenda
(
	@iEpisodio INT = 0
)
/*
** Nombre:					ppObtenerAsistentesAgenda		
** Propósito:				Obtiene los datos de los médicos
**							asistentes a la cirugía
** Campos:					--
** Dependencias: 			--	
** Error Base:				--
** Retorna:					--
**							
** Fecha creación:			30/Mayo/2019
** Autor creación:			HMARTINEZ
** Csd creación:			CSD175
** Fecha modificación: 		--
** Autor modificación: 		--
** Csd modificación:		--	
** Compatibilidad:			1.75
** Revisión:				0
*/
AS

DECLARE @Asistentes AS TABLE
(
	iIdUsuario INT,
	vchNombre VARCHAR(100),
	vchCorreo VARCHAR(100),
	iEstadoParticipación INT,
	iRsvp INT
)

INSERT INTO @Asistentes
 SELECT
	U.siusuario,
	U.vchNombreCompletoPaciente,	--	vchNombreOrganizador
	'',								--	vchCorreo
	0,								--	iEstadoParticipación (ACCEPTED)		
	0								--	iRsvp (false - No se espera una confirmación)
 FROM 
 	Catusuario U,
	vpeProgramacionCirugia C,
	vpqTranCirugiaPersonal CP
WHERE   
	C.iCodCirugia = @iEpisodio
	AND CP.iCodCirugia = C.iCodCirugia
	AND CP.siCodPersonal = U.siusuario	

UPDATE @Asistentes
SET vchCorreo = M.vchMedioLocalizacion
 FROM 
 	Catusuario U,
	CatMedioLocalizacionDireccionUsuario M,
	vpqTranCirugiaPersonal CP,
	@Asistentes A
WHERE   
	CP.iCodCirugia = @iEpisodio
	AND CP.siCodPersonal = U.siusuario
	AND M.iUsuario = CP.siCodPersonal
	AND M.siTipoMedioLocalizacion = 1383 --Correo
	AND M.tiJerarquiaDireccion = 1
	AND A.iIdUsuario = U.siusuario
	AND A.iIdUsuario = M.iUsuario


SELECT * 
FROM @Asistentes

GO

--+--------------------------------------------------------------------------------+--

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ppGenerarEventoAgenda')
	DROP PROCEDURE dbo.ppGenerarEventoAgenda
GO
CREATE PROCEDURE dbo.ppGenerarEventoAgenda
(
	@iEpisodio INT
)
/*
** Nombre:					ppGenerarEventoAgenda
** Propósito:				Recoleta datos para generar 
**							un archivo de evento de agenda
** Campos:					@iEpisodio	--	Código de cirugía / Código del episodio / Código de evento 
**
** Dependencias: 			--	
** Error Base:				--
** Retorna:					--
**							
** Fecha creación:			24/Mayo/2019
** Autor creación:			HMARTINEZ
** Csd creación:			CSD175
** Fecha modificación: 		--
** Autor modificación: 		--
** Csd modificación:		--
** Compatibilidad:			1.75
** Revisión:				0
*/
 AS

 DECLARE @EventoCirugia AS TABLE
 (
	iFolioEvento INT,
	dtFechaInicio DATETIME,
	dtFechaFin DATETIME,
	vchUbicacion VARCHAR(100),
	vchDescripcion VARCHAR(2000),
	vchResumen VARCHAR(2000),
	vchNombreOrganizador VARCHAR(100),
	vchCorreoOrganizador VARCHAR(50),
	vchResumenAlerta VARCHAR(2000)
 )
 
 SET NOCOUNT ON

 INSERT INTO @EventoCirugia
  SELECT
	C.iCodCirugia, 																										--	iEpisodio
	C.dtFechaProgramacion,																								--	dtFechaInicio 
	DATEADD(MINUTE,DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), CAST(dtTiempoDuracion AS TIME)), dtFechaProgramacion),	--	dtFechaFin 
	'',																													--	vchUbicacion 
	'',																													--	Descripción
	'',																													--	vchResumen  
	U.vchNombreCompletoPaciente,																						--	vchNombreOrganizador 
	M.vchMedioLocalizacion,																								--	vchCorreoOrganizador
	''																													--	vchResumenAlerta 
 FROM 
 	Catusuario U,
	vpeProgramacionCirugia C,
	TranEspCirProcedimientos PC,
	CatMedioLocalizacionDireccionUsuario M
WHERE   
	PC.siCodCirujano = U.siusuario 
	AND C.iCodCirugia = PC.iCodCirugia
	AND C.iCodCirugia =  @iEpisodio
	AND M.siTipoMedioLocalizacion = 1383 --Correo
	AND M.tiJerarquiaDireccion = 1
	AND M.iUsuario = PC.siCodCirujano

--Esto quedará como una descripción del evento y, a su vez, se usa como cuerpo del correo. 
UPDATE @EventoCirugia
SET vchDescripcion = 
	'Evento: Recordatorio de evento de cirugía programada en ' + 
	(
		SELECT TOP 1 vchDependecia
		FROM CatDependencias CD, CatEmpresa CE
		WHERE CD.iEmpresa = CE.iCodEmpresa
		AND CD.iTipoDependencia = 7200 --Empresa
		AND CD.iIdEstatus = 141000 --Activo
	) + 
	CHAR(13) + CHAR(10) + 
	'Paciente: ' + 
	(
		SELECT (vchNombrePac + ' ' + vchApellidoPaternoPac + ' ' + vchApellidoMaternoPac)
		FROM vpeProgramacionCirugia
		WHERE iCodCirugia = @iEpisodio
	) +
	CHAR(13) + CHAR(10) + 
	'Fecha de inicio: ' +
	(
		SELECT CONVERT(VARCHAR, C.dtFechaProgramacion, 120)
		FROM vpeProgramacionCirugia C
		WHERE C.iCodCirugia =  @iEpisodio
	) +
	CHAR(13) + CHAR(10) + 
	'Fecha de fin: ' +
	(
		SELECT CONVERT(VARCHAR, DATEADD(MINUTE,DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), CAST(C.dtTiempoDuracion AS TIME)), C.dtFechaProgramacion), 120) 
		FROM vpeProgramacionCirugia C
		WHERE C.iCodCirugia =  @iEpisodio
	) +
	CHAR(13) + CHAR(10) + 
	'Duración: ' +
	(
		SELECT CAST(DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), CAST(dtTiempoDuracion AS TIME)) AS VARCHAR(10))
		FROM vpeProgramacionCirugia
		WHERE iCodCirugia =  @iEpisodio
	) + 
	' minutos' +
	CHAR(13) + CHAR(10) +
	'Procedimiento: ' +
	(
		SELECT H.AvDslHno
		FROM vpeTranProcedimientoCirugia C, ACFHONO1 H
		WHERE C.iCodCirugia = @iEpisodio
		AND C.iCodProcedimiento = H.iCodHono
	)
WHERE iFolioEvento = @iEpisodio

--Esto se usará como título para el evento
UPDATE @EventoCirugia
SET vchResumen = 'Recordatorio de evento de cirugía programada en: ' +
(
	SELECT TOP 1 vchDependecia
	FROM CatDependencias CD, CatEmpresa CE
	WHERE CD.iEmpresa = CE.iCodEmpresa
	AND CD.iTipoDependencia = 7200 --Empresa
	AND CD.iIdEstatus = 141000 --Activo
)
WHERE iFolioEvento = @iEpisodio

--Esta parte representa una descripción para la alarma/alerta
UPDATE @EventoCirugia
SET vchResumenAlerta = vchResumen
WHERE iFolioEvento = @iEpisodio

--Ubicación
UPDATE @EventoCirugia
SET vchUbicacion =
(	
	SELECT TOP 1 vchvchUbicacion = (CE.vchDireccion + ', ' + CE.vchCodEstado + ', ' + CE.vchCodPais)
	FROM CatDependencias CD, CatEmpresa CE
	WHERE CD.iEmpresa = CE.iCodEmpresa
	AND CD.iTipoDependencia = 7200 --Empresa
	AND CD.iIdEstatus = 141000 --Activo
)

EXEC ppRegistrarEventoAgenda @iEpisodio, 0

--Registro de envio de evento de agenda

SELECT *
FROM @EventoCirugia

GO

 --+--------------------------------------------------------------------------------+--

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ppObtenerCirugiasPrograAgenda')
	DROP PROCEDURE dbo.ppObtenerCirugiasPrograAgenda
GO
CREATE PROCEDURE dbo.ppObtenerCirugiasPrograAgenda

/*
** Nombre:					ppObtenerCirugiasPrograAgenda		
** Propósito:				Obtiene los identificadores de las
**							cirugías programadas de las cuáles
**							se generarán archivos de eventos ICalendar
** Campos:					--
** Dependencias: 			--	
** Error Base:				--
** Retorna:					--
**							
** Fecha creación:			04/Junio/2019
** Autor creación:			HMARTINEZ
** Csd creación:			CSD175
** Fecha modificación: 		--
** Autor modificación: 		--
** Csd modificación:		--	
** Compatibilidad:			1.75
** Revisión:				0
*/
AS

DECLARE @Cirugias AS TABLE
(
	iCodCirugia INT,
	iIntentosEnvio INT
)

INSERT INTO @Cirugias(iCodCirugia)
SELECT iCodCirugia
FROM vpeProgramacionCirugia
WHERE dtFechaProgramacion 
BETWEEN GETDATE()
AND 
(
	SELECT 
	DATEADD
	(DAY, 
		(
			SELECT CAST(vchValor AS INT)
			FROM sysParametro 
			WHERE iCodParametro = 134061
		), 
	GETDATE()
	)
)


DELETE FROM @Cirugias
WHERE iCodCirugia IN
(
	SELECT C.iCodCirugia
	FROM @Cirugias C, CatRegistroEventoAgenda A
	WHERE C.iCodCirugia = a.iFolioEvento
	AND A.iIntentosEnvio > 0
)

SELECT iCodCirugia
FROM @Cirugias
			
GO

 --+--------------------------------------------------------------------------------+--*

 IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ppObtenerCitaMedicaAgenda')
	DROP PROCEDURE dbo.ppObtenerCitaMedicaAgenda
GO
CREATE PROCEDURE dbo.ppObtenerCitaMedicaAgenda

/*
** Nombre:					ppObtenerCitaMedicaAgenda		
** Propósito:				Obtiene los identificadores de las
**							citas médicas de las cuáles se generarán 
**							archivos de eventos ICalendar
** Campos:					--
** Dependencias: 			--	
** Error Base:				--
** Retorna:					--
**							
** Fecha creación:			17/Junio/2019
** Autor creación:			HMARTINEZ
** Csd creación:			CSD175
** Fecha modificación: 		--
** Autor modificación: 		--
** Csd modificación:		--	
** Compatibilidad:			1.75
** Revisión:				0
*/
AS

DECLARE @Citas AS TABLE
(
	iCodCita INT
)

INSERT INTO @Citas(iCodCita)
SELECT iFolioActividad
FROM TranactividAdagenda
WHERE dtFechaActividad
BETWEEN GETDATE()
AND 
(
	SELECT 
	DATEADD
	(DAY, 
		(
			SELECT CAST(vchValor AS INT)
			FROM sysParametro 
			WHERE iCodParametro = 134061
		), 
	GETDATE()
	)
)
AND iFolioActividad <> 0


DELETE FROM @Citas
WHERE iCodCita IN
(
	SELECT C.iCodCita
	FROM @Citas C, CatRegistroEventoAgenda A
	WHERE C.iCodCita = a.iFolioEvento
	AND A.iIntentosEnvio > 0
)

SELECT iCodCita
FROM @Citas
			
GO

 --+--------------------------------------------------------------------------------+--

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ppGenerarEventoAgendaCita')
	DROP PROCEDURE dbo.ppGenerarEventoAgendaCita
GO
CREATE PROCEDURE dbo.ppGenerarEventoAgendaCita
(
	@iEpisodio INT
)
/*
** Nombre:					ppGenerarEventoAgendaCita
** Propósito:				Recoleta datos para generar 
**							un archivo de evento de agenda
** Campos:					@iEpisodio	--	Código de cirugía / Código del episodio / Código de evento 
**
** Dependencias: 			--	
** Error Base:				--
** Retorna:					--
**							
** Fecha creación:			18/Junio/2019
** Autor creación:			HMARTINEZ
** Csd creación:			CSD175
** Fecha modificación: 		--
** Autor modificación: 		--
** Csd modificación:		--
** Compatibilidad:			1.75
** Revisión:				0
*/
 AS

 DECLARE @EventoCita AS TABLE
 (
	iFolioEvento INT,
	dtFechaInicio DATETIME,
	dtFechaFin DATETIME,
	vchUbicacion VARCHAR(100),
	vchDescripcion VARCHAR(2000),
	vchResumen VARCHAR(2000),
	vchNombreOrganizador VARCHAR(100),
	vchCorreoOrganizador VARCHAR(50),
	vchResumenAlerta VARCHAR(2000)
 )
 
 SET NOCOUNT ON

 INSERT INTO @EventoCita
	SELECT 
		TA.iFolioActividad, 
		TA.dtFechaIniActividad,
		TA.dtFechaFinActividad,
		'',
		'',
		'',
		U.vchNombreCompletoPaciente,
		M.vchMedioLocalizacion,
		''
	FROM 
		TranactividAdagenda TA,
		Catusuario U,
		CatMedioLocalizacionDireccionUsuario M
	WHERE 
		TA.iFolioActividad = @iEpisodio
		AND TA.iFolioActividad <> 0
		AND U.siusuario = TA.siAgendado
		AND M.siTipoMedioLocalizacion = 1383 --Correo
		AND M.tiJerarquiaDireccion = 1
		AND M.iUsuario = TA.siAgendado

--Esto quedará como una descripción del evento y, a su vez, se usa como cuerpo del correo. 
UPDATE @EventoCita
SET vchDescripcion = 
	'Evento: Recordatorio de evento de cita médica programada en ' + 
	(
		SELECT TOP 1 vchDependecia
		FROM CatDependencias CD, CatEmpresa CE
		WHERE CD.iEmpresa = CE.iCodEmpresa
		AND CD.iTipoDependencia = 7200 --Empresa
		AND CD.iIdEstatus = 141000 --Activo
	) + 
	CHAR(13) + CHAR(10) + 
	'Paciente: ' + 
	(
		SELECT P.vchNombreCompletoPaciente
		FROM TranactividAdagenda TA, Paciente P
		WHERE TA.iFolioActividad =  @iEpisodio
		AND P.iIdPaciente = TA.iIdPersonaCliente
	) +
	CHAR(13) + CHAR(10) + 
	'Fecha de inicio: ' +
	(
		SELECT CONVERT(VARCHAR, TA.dtFechaIniActividad, 120)
		FROM TranactividAdagenda TA
		WHERE TA.iFolioActividad =  @iEpisodio
	) +
	CHAR(13) + CHAR(10) + 
	'Fecha de fin: ' +
	(
		SELECT CONVERT(VARCHAR, TA.dtFechaFinActividad, 120)
		FROM TranactividAdagenda TA
		WHERE TA.iFolioActividad =  @iEpisodio
	) +
	CHAR(13) + CHAR(10) + 
	'Duración: ' +
	(
		SELECT CAST(DATEDIFF(MINUTE, CAST(TA.dtFechaIniActividad AS TIME), CAST(TA.dtFechaFinActividad AS TIME)) AS VARCHAR(10))
		FROM TranactividAdagenda TA
		WHERE TA.iFolioActividad =  @iEpisodio
	) + 
	' minutos' +
	CHAR(13) + CHAR(10) 
WHERE iFolioEvento = @iEpisodio

--Esto se usará como título para el evento
UPDATE @EventoCita
SET vchResumen = 'Recordatorio de evento de cita médica programada en: ' +
(
	SELECT TOP 1 vchDependecia
	FROM CatDependencias CD, CatEmpresa CE
	WHERE CD.iEmpresa = CE.iCodEmpresa
	AND CD.iTipoDependencia = 7200 --Empresa
	AND CD.iIdEstatus = 141000 --Activo
)
WHERE iFolioEvento = @iEpisodio

--Esta parte representa una descripción para la alarma/alerta
UPDATE @EventoCita
SET vchResumenAlerta = vchResumen
WHERE iFolioEvento = @iEpisodio

--Ubicación
UPDATE @EventoCita
SET vchUbicacion =
(	
	SELECT TOP 1 vchvchUbicacion = (CE.vchDireccion + ', ' + CE.vchCodEstado + ', ' + CE.vchCodPais)
	FROM CatDependencias CD, CatEmpresa CE
	WHERE CD.iEmpresa = CE.iCodEmpresa
	AND CD.iTipoDependencia = 7200 --Empresa
	AND CD.iIdEstatus = 141000 --Activo
)

EXEC ppRegistrarEventoAgenda @iEpisodio, 1

--Registro de envio de evento de agenda

SELECT *
FROM @EventoCita

GO

 --+--------------------------------------------------------------------------------+--

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ppObtenerAsistentesCitaAgenda')
	DROP PROCEDURE dbo.ppObtenerAsistentesCitaAgenda
GO
CREATE PROCEDURE dbo.ppObtenerAsistentesCitaAgenda
(
	@iEpisodio INT = 0
)
/*
** Nombre:					ppObtenerAsistentesAgenda		
** Propósito:				Obtiene los datos de los médicos
**							asistentes a la cirugía
** Campos:					--
** Dependencias: 			--	
** Error Base:				--
** Retorna:					--
**							
** Fecha creación:			18/Junio/2019
** Autor creación:			HMARTINEZ
** Csd creación:			CSD175
** Fecha modificación: 		--
** Autor modificación: 		--
** Csd modificación:		--	
** Compatibilidad:			1.75
** Revisión:				0
*/
AS

DECLARE @Asistentes AS TABLE
(
	iIdUsuario INT,
	vchNombre VARCHAR(100),
	vchCorreo VARCHAR(100),
	iEstadoParticipación INT,
	iRsvp INT
)

INSERT INTO @Asistentes
	SELECT 
		P.iIdPaciente, 
		P.vchNombreCompletoPaciente,
		p.vchEmail,
		0,
		0
	FROM 
		TranactividAdagenda TA,
		Paciente P
	WHERE 
		TA.iFolioActividad = @iEpisodio
		AND TA.iFolioActividad <> 0
		AND P.iIdPaciente = TA.iIdPersonaCliente

SELECT * 
FROM @Asistentes

GO


