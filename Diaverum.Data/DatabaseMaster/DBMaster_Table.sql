--Scaffold-DbContext "Server=localhost\SQLEXPRESS02;Initial Catalog=diaverum;persist security info=True;Integrated Security=SSPI;MultipleActiveResultSets=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir . -Context DiaverumDbContext -NoOnConfiguring -Force

IF OBJECTPROPERTY(object_id('dbo.DiaverumItem'), N'IsTable') = 1 DROP TABLE [dbo].[DiaverumItem]
GO

CREATE TABLE [dbo].[DiaverumItem](
	[Id] [smallint] NOT NULL IDENTITY(1,1),
	[CreatedAt] [datetime2] NOT NULL DEFAULT(GETUTCDATE()),
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
