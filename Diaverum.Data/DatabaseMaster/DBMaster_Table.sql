--Scaffold-DbContext "Server=localhost\SQLEXPRESS02;Initial Catalog=diaverum;persist security info=True;Integrated Security=SSPI;MultipleActiveResultSets=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir . -Context DiaverumDbContext -NoOnConfiguring -Force

IF OBJECTPROPERTY(object_id('dbo.DiaverumItem'), N'IsTable') = 1 DROP TABLE [dbo].[DiaverumItem]
GO

CREATE TABLE [dbo].[DiaverumItem](
	[Id] [smallint] NOT NULL IDENTITY(1,1),
	[CreatedAt] [datetime2] NOT NULL DEFAULT(SYSUTCDATETIME()),
	[CreatedBy] [int] NOT NULL DEFAULT(0),
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
	[Text] [nvarchar](100) NOT NULL,
	[TextDetails] [nvarchar](500) NULL,
	[EvenNumber] [smallint] NOT NULL,
	[EventDate] [datetime2](0) NOT NULL
 CONSTRAINT [DiaverumItem_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

IF OBJECTPROPERTY(object_id('dbo.LabResult'), N'IsTable') = 1 DROP TABLE [dbo].[LabResult]
GO

CREATE TABLE [dbo].[LabResult](
	[Id] [bigint] NOT NULL IDENTITY(1,1),
	[CreatedAt] [datetime2] NOT NULL DEFAULT (SYSUTCDATETIME()),
	[CreatedBy] [int] NOT NULL DEFAULT(0),
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
	[ClinicNo] [nvarchar](100) NOT NULL,
	[Barcode] [nvarchar](100) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientName] [nvarchar](100) NOT NULL,
	[DBO] [date] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[CollentionDate] [date] NOT NULL,
	[CollentionTime] [time](0) NOT NULL,
	[TestCode] [nvarchar](100) NOT NULL,
	[TestName] [nvarchar](100) NOT NULL,
	[Result] [decimal](8, 4) NULL,
	[Unit] [nvarchar](100) NULL,
	[RefrangeLow] [nvarchar](100) NULL,
	[RefrangeHigh] [nvarchar](100) NULL,
	[Note] [nvarchar](500) NULL,
	[NonSpecRefs] [nvarchar](500) NULL
 CONSTRAINT [LabResult_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE NONCLUSTERED INDEX [IdxLabResult_ClinicNo] ON [dbo].[LabResult]([ClinicNo] ASC);
GO

CREATE NONCLUSTERED INDEX [IdxLabResult_PatientId] ON [dbo].[LabResult]([PatientId] ASC);
GO