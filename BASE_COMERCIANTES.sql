create database COMERCIANTES
use COMERCIANTES

/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     1/2/2020 18:54:47                            */
/*==============================================================*/

if exists (select 1
            from  sysobjects
           where  id = object_id('TCABECERARANSACCION')
            and   type = 'U')
   drop table TCABECERARANSACCION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TCALIFICACION')
            and   type = 'U')
   drop table TCALIFICACION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TDETALLE_TRANSACCION')
            and   type = 'U')
   drop table TDETALLE_TRANSACCION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TFORMA_ENTREGA')
            and   type = 'U')
   drop table TFORMA_ENTREGA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TFORMA_PAGO')
            and   type = 'U')
   drop table TFORMA_PAGO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TPRODUCTO')
            and   type = 'U')
   drop table TPRODUCTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TTIPO')
            and   type = 'U')
   drop table TTIPO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TUSUARIO')
            and   type = 'U')
   drop table TUSUARIO
go

/*==============================================================*/
/* Table: TCABECERARANSACCION                                   */
/*==============================================================*/
create table TCABECERARANSACCION (
   TRA_ID               int  identity (1,1)          not null,
   USU_CEDULA           varchar(10)          null,
   CALI_CODIGO          int                  null,
   FORENTRE_CODIGO      int                  null,
   FORPA_CODIGO         int                  null,
   TRA_ESTADO           varchar(25)          null,
   TRA_FORMAPAGO        varchar(25)          null,
   TRA_FECHAGTRANSACCION datetime             null,
   TRA_FORMAENTREGA     varchar(50)          null,
   TRA_DIRECCIONENTREGA varchar(50)          null,
   TRA_TOTAL            float                null,
   constraint PK_TCABECERARANSACCION primary key nonclustered (TRA_ID)
)
go



/*==============================================================*/
/* Table: TCALIFICACION                                         */
/*==============================================================*/
create table TCALIFICACION (
   CALI_CODIGO          int          identity (1,1)         not null,
   CALI_DESCRIPCION     varchar(50)          null,
   constraint PK_TCALIFICACION primary key nonclustered (CALI_CODIGO)
)
go

/*==============================================================*/
/* Table: TDETALLE_TRANSACCION                                  */
/*==============================================================*/
create table TDETALLE_TRANSACCION (
   DETA_CODIGO          int                  not null,
   TIPO_CODIGO          int                  null,
   PRO_ID               int                  null,
   TRA_ID               int                  null,
   constraint PK_TDETALLE_TRANSACCION primary key nonclustered (DETA_CODIGO)
)
go



/*==============================================================*/
/* Table: TFORMA_ENTREGA                                        */
/*==============================================================*/
create table TFORMA_ENTREGA (
   FORENTRE_CODIGO      int             identity (1,1)      not null,
   FORENTRE_DOMICILIO   varchar(50)          null,
   FORENTRE_OFICINA     varchar(50)          null,
   constraint PK_TFORMA_ENTREGA primary key nonclustered (FORENTRE_CODIGO)
)
go

/*==============================================================*/
/* Table: TFORMA_PAGO                                           */
/*==============================================================*/
create table TFORMA_PAGO (
   FORPA_CODIGO         int           identity (1,1)        not null,
   FORPA_TARJETA        float                null,
   FORPA_CHEQUE         float                null,
   FORPA_EFECTIVO       float                null,
   constraint PK_TFORMA_PAGO primary key nonclustered (FORPA_CODIGO)
)
go

/*==============================================================*/
/* Table: TPRODUCTO                                             */
/*==============================================================*/
create table TPRODUCTO (
   PRO_ID               int      identity (1,1)   not null,
   TIPO_CODIGO          int                  null,
   USU_CEDULA           varchar(10)          null,
   PRO_NOMBRE           varchar(50)          null,
   PRO_PRECIO           decimal              null,
   PRO_STOCK            int                  null,
   PRO_DESCRIPCION      varchar(25)          null,
   PRO_FOTO                 image                null,
   PRO_TIPO                 bit                  null,
   constraint PK_TPRODUCTO primary key nonclustered (PRO_ID)
)
go


/*==============================================================*/
/* Table: TTIPO                                                 */
/*==============================================================*/
create table TTIPO (
   TIPO_CODIGO          int                  not null,
   TIPO_PRODUCTO        int                  null,
   TIPO_SERVICIO        int                  null,
   constraint PK_TTIPO primary key nonclustered (TIPO_CODIGO)
)
go



/*==============================================================*/
/* Table: TUSUARIO                                              */
/*==============================================================*/
create table TUSUARIO (
   USU_CEDULA           varchar(10)           not null,
   USU_PASSWORD         varchar(25)          null,
   USU_TIPO             varchar(25)          null,
   USU_NOMBRE           varchar(50)          null,
   USU_TELEFONO         varchar(25)          null,
   USU_CORREO          varchar(25)          null,
   USU_DIRECCION        varchar(50)          null,
   USU_SEXO             varchar(25)          null,
   USU_CALIFICACION     int                  null,
   constraint PK_TUSUARIO primary key nonclustered (USU_CEDULA)
)
go

alter table  TCABECERARANSACCION
add constraint FK_formaentrega_cabecera FOREIGN KEY ( FORENTRE_CODIGO  ) REFERENCES [dbo].[TFORMA_ENTREGA]( FORENTRE_CODIGO),
 constraint FK_usuario_cabecera FOREIGN KEY ( USU_CEDULA ) REFERENCES [dbo].[TUSUARIO](USU_CEDULA),
 constraint FK_calificacion_cabecera FOREIGN KEY ( CALI_CODIGO  ) REFERENCES [dbo].[TCALIFICACION](CALI_CODIGO ),
 constraint FK_formapago_cabecera FOREIGN KEY (  FORPA_CODIGO  ) REFERENCES [dbo].[TFORMA_PAGO]( FORPA_CODIGO  )
on update cascade
go



alter table  TPRODUCTO
add constraint FK_tipo_producto FOREIGN KEY ( TIPO_CODIGO  ) REFERENCES [dbo].[TTIPO]( TIPO_CODIGO),
 constraint FK_usuarioproducto FOREIGN KEY ( USU_CEDULA  ) REFERENCES [dbo].[Tusuario](USU_CEDULA )
 on update cascade
go

alter table  TDETALLE_TRANSACCION
add constraint FK_tipo_detalle FOREIGN KEY ( TIPO_CODIGO  ) REFERENCES [dbo].[TTIPO]( TIPO_CODIGO),
 constraint FK_productos_detalle FOREIGN KEY (PRO_ID) REFERENCES [dbo].[TPRODUCTO](PRO_ID),
 constraint FK_cabecetransaccion_detalle FOREIGN KEY (TRA_ID) REFERENCES [dbo].[TCABECERARANSACCION](TRA_ID)
 on update cascade
go

