USE [wepApiMvcDb]
GO
/****** Object:  Table [dbo].[DovizTable]    Script Date: 22.01.2020 23:50:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DovizTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Doviz] [nvarchar](100) NULL,
	[StartDate] [nvarchar](100) NULL,
	[EndDate] [nvarchar](100) NULL,
	[QueryDate] [smalldatetime] NULL,
 CONSTRAINT [PK_DovizTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
