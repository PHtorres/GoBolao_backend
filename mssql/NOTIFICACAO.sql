/****** Object:  Table [dbo].[NOTIFICACAO]    Script Date: 15/03/2020 17:10:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NOTIFICACAO](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuarioDestino] [int] NOT NULL,
	[IdUsuarioOrigem] [int] NOT NULL,
	[IdBolao] [int] NOT NULL,
	[Mensagem] [varchar](100) NOT NULL,
	[Tipo] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_NOTIFICACAO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[NOTIFICACAO]  WITH CHECK ADD  CONSTRAINT [FK_NOTIFICACAO_BOLAO] FOREIGN KEY([IdBolao])
REFERENCES [dbo].[BOLAO] ([Id])
GO

ALTER TABLE [dbo].[NOTIFICACAO] CHECK CONSTRAINT [FK_NOTIFICACAO_BOLAO]
GO

ALTER TABLE [dbo].[NOTIFICACAO]  WITH CHECK ADD  CONSTRAINT [FK_NOTIFICACAO_USUARIO_DESTINO] FOREIGN KEY([IdUsuarioDestino])
REFERENCES [dbo].[USUARIO] ([Id])
GO

ALTER TABLE [dbo].[NOTIFICACAO] CHECK CONSTRAINT [FK_NOTIFICACAO_USUARIO_DESTINO]
GO

ALTER TABLE [dbo].[NOTIFICACAO]  WITH CHECK ADD  CONSTRAINT [FK_NOTIFICACAO_USUARIO_ORIGEM] FOREIGN KEY([IdUsuarioOrigem])
REFERENCES [dbo].[USUARIO] ([Id])
GO

ALTER TABLE [dbo].[NOTIFICACAO] CHECK CONSTRAINT [FK_NOTIFICACAO_USUARIO_ORIGEM]
GO


