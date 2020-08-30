CREATE DATABASE Db_Patrimonios
GO

USE [Db_Patrimonios]
GO

/****** Tabela [dbo].[tbMarca] ******/
CREATE TABLE [dbo].[tbMarca](
		[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
		[Nome] [varchar](100) NOT NULL,
		[DataUltimaModificacao] [datetime] NOT NULL,
		[DataCriacao] [datetime] NOT NULL,
	);

/****** Tabela [dbo].[tbPatrimonio] ******/
CREATE TABLE [dbo].[tbPatrimonio](
		[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
		[Nome] [varchar](100) NOT NULL,
		[MarcaId] [int] NOT NULL,
		[Descricao] [varchar](255) NULL,
		[NumeroTombo] [int] NOT NULL,
		[DataUltimaModificacao] [datetime] NOT NULL,
		[DataCriacao] [datetime] NOT NULL,
	);

/****** StoredProcedure [dbo].[prDelMarca] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Julierme A. C. Santos
-- Create date: 28/08/2020
-- Description:	Apaga um registro na tabela tbMarca
-- =============================================
CREATE PROCEDURE [dbo].[prDelMarca] 
	@Id int
AS
BEGIN	
	SET NOCOUNT ON;

	DELETE FROM tbMarca WHERE Id = @Id		
END
GO

/****** StoredProcedure [dbo].[prDelPatrimonio] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Julierme A. C. Santos
-- Create date: 28/08/2020
-- Description:	Apaga um registro na tabela tbPatrimonio
-- =============================================
CREATE PROCEDURE [dbo].[prDelPatrimonio] 
	@Id int
AS
BEGIN	
	SET NOCOUNT ON;

	DELETE FROM tbPatrimonio WHERE Id = @Id		
END
GO

/****** StoredProcedure [dbo].[prInsMarca] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Julierme A. C. Santos
-- Create date: 28/08/2020
-- Description:	Insere um registro na tabela tbMarca
-- =============================================
CREATE PROCEDURE [dbo].[prInsMarca] 
	  @Nome varchar(100)
AS
BEGIN	
	SET NOCOUNT ON;

	INSERT INTO tbMarca
	( 
	   Nome      
      ,DataUltimaModificacao
	  ,DataCriacao
	) 
	VALUES
	( 
		@Nome		
		,GETDATE()
		,GETDATE()
	) 
	SELECT SCOPE_IDENTITY() AS Id;
END
GO

/****** StoredProcedure [dbo].[prInsPatrimonio] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Julierme A. C. Santos
-- Create date: 28/08/2020
-- Description:	Insere um registro na tabela tbPatrimonio
-- =============================================
CREATE PROCEDURE [dbo].[prInsPatrimonio] 
	  @Nome varchar(100)
      ,@MarcaId int
      ,@Descricao varchar(255)
      ,@NumeroTombo int      
AS
BEGIN	
	SET NOCOUNT ON;

	INSERT INTO tbPatrimonio
	( 
	   Nome
      ,MarcaId
      ,Descricao
      ,NumeroTombo
      ,DataUltimaModificacao
	  ,DataCriacao
	) 
	VALUES
	( 
		@Nome
		,@MarcaId
		,@Descricao
		,@NumeroTombo
		,GETDATE()
		,GETDATE()
	) 
	SELECT SCOPE_IDENTITY() AS Id;
END
GO

/****** StoredProcedure [dbo].[prSelMarca] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Julierme A. C. Santos
-- Create date: 28/08/2020
-- Description:	Consulta a tabela tbMarca
-- =============================================
CREATE PROCEDURE [dbo].[prSelMarca] 
	@Id int = 0
AS
BEGIN	
	SET NOCOUNT ON;

	IF @Id = 0    
		SELECT * FROM tbMarca (nolock)
	ELSE
		SELECT * FROM tbMarca (nolock) WHERE Id = @Id
END
GO

/****** StoredProcedure [dbo].[prSelPatrimonio] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Julierme A. C. Santos
-- Create date: 28/08/2020
-- Description:	Consulta a tabela tbPatrimonio
-- =============================================
CREATE PROCEDURE [dbo].[prSelPatrimonio] 
	@Id int = 0
AS
BEGIN	
	SET NOCOUNT ON;

	IF @Id = 0    
		SELECT * FROM tbPatrimonio (nolock)
	ELSE
		SELECT * FROM tbPatrimonio (nolock) WHERE Id = @Id
END
GO

/****** StoredProcedure [dbo].[prUpdMarca] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Julierme A. C. Santos
-- Create date: 28/08/2020
-- Description:	Atualiza um registro na tabela tbMarca
-- =============================================
CREATE PROCEDURE [dbo].[prUpdMarca] 
	  @Id int
	  ,@Nome varchar(100)     
AS
BEGIN	
	SET NOCOUNT ON;

	UPDATE tbMarca
	SET
		Nome = @Nome		
		,DataUltimaModificacao = GETDATE()
	WHERE Id = @Id
END
GO
/****** StoredProcedure [dbo].[prUpdPatrimonio] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Julierme A. C. Santos
-- Create date: 28/08/2020
-- Description:	Atualiza um registro na tabela tbPatrimonio
-- =============================================
CREATE PROCEDURE [dbo].[prUpdPatrimonio] 
	  @Id int
	  ,@Nome varchar(100)
      ,@MarcaId int
      ,@Descricao varchar(255)
AS
BEGIN	
	SET NOCOUNT ON;

	UPDATE tbPatrimonio
	SET
		Nome = @Nome
		,MarcaId = @MarcaId
		,Descricao = @Descricao
		,DataUltimaModificacao = GETDATE()
	WHERE Id = @Id
END
GO
