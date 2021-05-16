/****** Object:  Table [dbo].[BOLAO_SOLICITACAO]    Script Date: 09/03/2020 12:19:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BOLAO_SOLICITACAO](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBolao] [int] NOT NULL,
	[IdUsuarioSolicitante] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_BOLAO_SOLICITACAO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BOLAO_SOLICITACAO]  WITH CHECK ADD  CONSTRAINT [FK_BOLAO_SOLICITACAO_BOLAO] FOREIGN KEY([IdBolao])
REFERENCES [dbo].[BOLAO] ([Id])
GO

ALTER TABLE [dbo].[BOLAO_SOLICITACAO] CHECK CONSTRAINT [FK_BOLAO_SOLICITACAO_BOLAO]
GO

ALTER TABLE [dbo].[BOLAO_SOLICITACAO]  WITH CHECK ADD  CONSTRAINT [FK_BOLAO_SOLICITACAO_USUARIO] FOREIGN KEY([IdUsuarioSolicitante])
REFERENCES [dbo].[USUARIO] ([Id])
GO

ALTER TABLE [dbo].[BOLAO_SOLICITACAO] CHECK CONSTRAINT [FK_BOLAO_SOLICITACAO_USUARIO]
GO


