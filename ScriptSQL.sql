Create database Serprofing 
use Serprofing
go

--b. En la entidad oficina almacenar información básica: Descripción, teléfono,dirección
Create table Oficina(
ID int identity(1,1) not null primary key,
Descripcion varchar(250) not null,
Telefono varchar(8) not null,
Direccion varchar(250) not null
)
alter table Oficina add Nombre varchar(100)

--En la entidad empleado, almacenar la información básica: Nombre, Dui, Dirección,
--Teléfono, Fecha de Ingreso, Activo

Create table Empleado(
ID int identity(1,1) not null primary key,
Nombre varchar(200) not null,
Dui varchar(10) not null,
Direccion varchar(250) not null,
Telefono varchar(8) not null,
Fecha_Ingreso Datetime Default GetDate(),
Activo bit,
ID_Oficina int
Foreign key(ID_Oficina) references Oficina(ID)
)
