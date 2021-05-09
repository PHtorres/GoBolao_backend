/****** Object:  Table [dbo].[JOGO]    Script Date: 09/03/2020 12:20:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[JOGO](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCampeonato] [int] NOT NULL,
	[DataHora] [datetime] NOT NULL,
	[Mandante] [varchar](50) NOT NULL,
	[Visitante] [varchar](50) NOT NULL,
	[NomeImagemAvatarMandante] [varchar](100) NULL,
	[NomeImagemAvatarVisitante] [varchar](100) NULL,
	[PlacarMandante] [int] NULL,
	[PlacarVisitante] [int] NULL,
	[Resultado] [varchar](10) NULL,
	[Fase] [varchar](30) NOT NULL,
 CONSTRAINT [PK_JOGO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[JOGO]  WITH CHECK ADD  CONSTRAINT [FK_JOGO_CAMPEONATO] FOREIGN KEY([IdCampeonato])
REFERENCES [dbo].[CAMPEONATO] ([Id])
GO

ALTER TABLE [dbo].[JOGO] CHECK CONSTRAINT [FK_JOGO_CAMPEONATO]
GO


