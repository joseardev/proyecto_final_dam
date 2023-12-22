-- Insertar tipos de aviso
use Tickets;
INSERT INTO TK_TIPOS_AVISOS (TIPO, DESCRIPCION, CENTRO, ESTADO, PRIORIDAD_RECOMENDADA)
VALUES 
('Incidencia', 'Descripción del tipo 1', 'Centro A', 'Activo', 1),
('Cambio', 'Descripción del tipo 2', 'Centro B', 'Inactivo', 2);

-- Insertar tipos de origen
INSERT INTO TK_TIPOS_ORIGEN (ORIGEN, DESCRIPCION)
VALUES 
('APP', 'Tickets desde App'),
('DESTOP', 'Ticket desde Destop');


INSERT INTO [dbo].[TK_USUARIOS]
           ([USUARIO]
           ,[PASSWORD]
           ,[PERFIL]
           ,[NOMBRE]
           ,[APELLIDOS]
           ,[CENTRO]
           ,[PERMISOS_CREAR_TK]
           ,[PERMISOS_MODIFICAR_TK]
           ,[PERMISOS_BORRAR_TK]
           ,[mail])
     VALUES
           ('soporte' -- USUARIO
           ,'1234' -- PASSWORD
           ,'Administrador' -- PERFIL
           ,'Juan' -- NOMBRE
           ,'Pérez' -- APELLIDOS
           ,'Centro1' -- CENTRO
           ,1 -- PERMISOS_CREAR_TK (0 = falso, 1 = verdadero)
           ,1 -- PERMISOS_MODIFICAR_TK (0 = falso, 1 = verdadero)
           ,0 -- PERMISOS_BORRAR_TK (0 = falso, 1 = verdadero)
           ,'jose.alonso.riveiro@ciclosmontecastelo.com');

INSERT INTO [dbo].[TK_USUARIOS]
           ([USUARIO]
           ,[PASSWORD]
           ,[PERFIL]
           ,[NOMBRE]
           ,[APELLIDOS]
           ,[CENTRO]
           ,[PERMISOS_CREAR_TK]
           ,[PERMISOS_MODIFICAR_TK]
           ,[PERMISOS_BORRAR_TK]
           ,[mail])
     VALUES
           ('jose' -- USUARIO
           ,'1234' -- PASSWORD
           ,'soporte' -- PERFIL
           ,'Juan' -- NOMBRE
           ,'Pérez' -- APELLIDOS
           ,'Centro1' -- CENTRO
           ,1 -- PERMISOS_CREAR_TK (0 = falso, 1 = verdadero)
           ,1 -- PERMISOS_MODIFICAR_TK (0 = falso, 1 = verdadero)
           ,0 -- PERMISOS_BORRAR_TK (0 = falso, 1 = verdadero)
           ,'jose.alonso.riveiro@ciclosmontecastelo.com');