# Agenda de turnos 📖

## Objetivos 📋
Desarrollar un sistema, que permita la administración general de un consultorio (de cara a los administradores): Prestaciones, Profesionales, Pacientes, etc., como así también, permitir a los pacientes, realizar reserva sobre turnos ofrecidos.
Utilizar Visual Studio 2019 preferentemente y crear una aplicación utilizando ASP.NET MVC Core (versión a definir por el docente 2.2 o 3.1).

<hr />

## Enunciado 📢
La idea principal de este trabajo práctico, es que Uds. se comporten como un equipo de desarrollo.
Este documento, les acerca, un equivalente al resultado de una primera entrevista entre el cliente y alguien del equipo, el cual relevó e identificó la información aquí contenida. 
A partir de este momento, deberán comprender lo que se está requiriendo y construir dicha aplicación, 

Deben recopilar todas las dudas que tengan y evacuarlas con su nexo (el docente) de cara al cliente. De esta manera, él nos ayudará a conseguir la información ya un poco más procesada. 
Es importante destacar, que este proceso, no debe esperar a ser en clase; es importante, que junten algunas consultas, sea de índole funcional o técnicas, en lugar de cada consulta enviarla de forma independiente.

Las consultas que sean realizadas por correo deben seguir el siguiente formato:

Subject: [NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta

Body: 

1.<xxxxxxxx>

2.< xxxxxxxx>


# Ejemplo
**Subject:** [NT1-A-GRP-5] Agenda de Turnos | Consulta

**Body:**

1.La relación del paciente con Turno es 1:1 o 1:N?

2.Está bien que encaremos la validación del turno activo, con una propiedad booleana en el Turno?

<hr />

### Proceso de ejecución en alto nivel ☑️
 - Crear un nuevo proyecto en [visual studio](https://visualstudio.microsoft.com/en/vs/).
 - Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.
 - Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades. [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1).
 - Crear las relaciones entre las entidades
 - Crear una carpeta Data que dentro tendrá al menos la clase que representará el contexto de la base de datos DbContext. 
 - Crear el DbContext utilizando base de datos en memoria (con fines de testing inicial). [DbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1), [Database In-Memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=vs).
 - Agregar los DbSet para cada una de las entidades en el DbContext.
 - Crear el Scaffolding para permitir los CRUD de las entidades al menos solicitadas en el enunciado.
 - Aplicar las adecuaciones y validaciones necesarias en los controladores.  
 - Realizar un sistema de login con al menos los roles equivalentes a <Usuario Cliente> y <Usuario Administrador> (o con permisos elevados).
 - Si el proyecto lo requiere, generar el proceso de registración. 
 - Un administrador podrá realizar todas tareas que impliquen interacción del lado del negocio (ABM "Alta-Baja-Modificación" de las entidades del sistema y configuraciones en caso de ser necesarias).
 - El <Usuario Cliente> sólo podrá tomar acción en el sistema, en base al rol que tiene.
 - Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
 - Realizar los ajustes requeridos del lado de los permisos.
 - Todo lo referido a la presentación de la aplicaión (cuestiones visuales).
 
<hr />

## Entidades 📄

- Usuario
- Paciente
- Profesional
- Turno
- Prestacion
- Formulario

`Importante: Todas las entidades deben tener su identificador unico. Id o <ClassNameId>`

`
Las propiedades descriptas a continuación, son las minimas que deben tener las entidades. Uds. pueden agregar las que consideren necesarias.
De la misma manera Uds. deben definir los tipos de datos asociados a cada una de ellas, como así también las restricciones.
`

**Usuario**
```
- Nombre
- Email
- FechaAlta
- Password
```

**Paciente**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email
- ObraSocial
- Turnos
```

**Profesional**
```
- Nombre
- Apellido
- Telefono
- Direccion
- FechaAlta
- Email
- Matricula
- Prestacion
- HoraInicio
- HoraFin
- Turnos
```

**Turno**
```
- Fecha
- Confirmado
- Activo
- FechaAlta
- Paciente
- Profesional
- DescripcionCancelacion
```


**Prestación**
```
- Nombre
- Descripcion
- Duracion
- Precio
- Profesionales
```

**Formulario**
```
- Fecha
- Email
- Nombre
- Apellido
- Leido
- Titulo
- Mensaje
- Usuario
```

**NOTA:** aquí un link para refrescar el uso de los [Data annotations](https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/).

<hr />

## Caracteristicas y Funcionalidades ⌨️
`Todas las entidades, deben tener implementado su correspondiente ABM, a menos que sea implicito el no tener que soportar alguna de estas acciones.`

**Usuario**
- Los pacientes pueden auto registrarse.
- La autoregistración desde el sitio, es exclusiva pra los pacientes. Por lo cual, se le asignará dicho rol.
- Los profesionales, deben ser agregados por un Administrador.
	- Al momento, del alta del profesional, se le definirá un username y password.
    - También se le asignará a estas cuentas el rol de Profesional.

**Paciente**
- Un paciente puede sacar un turno Online
    - El proceso será en modo Wizard.
        - Selecciona la prestación
        - Selecciona un profesional que tenga dicha prestación
        - Seleccionará un turno disponible dentro de la oferta.
            - La oferta estará restringida desde el momento de la consulta hasta 7 dias posteriores.
            - No debe incluir desde luego turnos tomados.
            - Debe ser en base a la oferta del profesional seleccionado. Turnos no tomados y dentro del horario del profesional.
            - El paciente, solo puede tener un turno activo.
        - El paciente, podrá en todo momento, ver si tiene o no un turno solicitado.
            - Verá el estado, si está o no confirmado
            - Podrá cancelarlo, solo si es hasta 24hs. antes.
        - El paciente puede llenar un formulario de contacto, para enviar una consulta.
            - El formulario, puede ser enviado de forma anonima o no. Si el paciente está logueado, cargará automaticamente los datos de este. 
- Puede actualizar datos de contacto, como el telefono, dirección, Obra Social. Pero no puede modificar su DNI, Nombre, Apellido, etc.

**Profesional**
- El profesional puede listar los turnos que tiene asignado en el futuro, para atender.
- El profesional, puede confirmar sus turnos. 
- También, puede ver un balance de los turnos que ya realizó en el mes calendario. 
    - Visualizará en este el valor que deberá percibir a fin de mes x valor hora.

**Administrador**
- Un Administrador, puede confirmar los turnos de cualquier profesional.
- Un Administrador, puede cancelar un turno en cualquier momento, agregando una descripción del motivo.
- Puede cargar las prestaciones que brinda el centro.
- Dar de alta a profesionales, con su horario de atención, y todos los datos requeridos.

**Turno**
- El turno al crearse, debe estar en estado sin confirmar, y algun administrador debe confirmar el mismo.
- No se pueden superponer dos turnos del mismo profesional.

**Aplicación General**
- Información institucional.
- Se deben mostrar las Prestaciones ofrecidas, con una descripción de la misma, costos, duración tipica de la prestación, etc. 
- Se deben listar los profesionales que brindan su atención.
- Nadie, puede eliminar los pacientes del sistema. Esto debe estar restringido.
- Los accesos a las funcionalidades y/o capacidades, debe estar basada en los roles que tenga cada individuo.

`
Nota: El centro tiene consultorios ilimitados y cada consultorio está preparado para soportar cualquier prestación, por lo cual esto no implica restricciones.
`
