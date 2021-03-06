USE [master]
GO
/****** Object:  Database [DBServerPonce]    Script Date: 28/03/2020 14:26:36 ******/
CREATE DATABASE [DBServerPonce]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBServerPonce', FILENAME = N'/var/opt/mssql/data/DBServerPonce.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBServerPonce_log', FILENAME = N'/var/opt/mssql/data/DBServerPonce_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBServerPonce].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBServerPonce] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBServerPonce] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBServerPonce] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBServerPonce] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBServerPonce] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBServerPonce] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBServerPonce] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBServerPonce] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBServerPonce] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBServerPonce] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBServerPonce] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBServerPonce] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBServerPonce] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBServerPonce] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBServerPonce] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBServerPonce] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBServerPonce] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBServerPonce] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBServerPonce] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBServerPonce] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBServerPonce] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBServerPonce] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBServerPonce] SET RECOVERY FULL 
GO
ALTER DATABASE [DBServerPonce] SET  MULTI_USER 
GO
ALTER DATABASE [DBServerPonce] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBServerPonce] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBServerPonce] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBServerPonce] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBServerPonce] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBServerPonce', N'ON'
GO
ALTER DATABASE [DBServerPonce] SET QUERY_STORE = OFF
GO
USE [DBServerPonce]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 28/03/2020 14:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [bigint] IDENTITY(1,1) NOT NULL,
	[NomeCompleto] [varchar](100) NOT NULL,
	[Cpf] [bigint] NOT NULL,
	[DataNascimento] [datetime] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContaCorrente]    Script Date: 28/03/2020 14:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContaCorrente](
	[ContaCorrenteId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClienteId] [bigint] NOT NULL,
	[DataAbertura] [datetime] NOT NULL,
	[Numero] [int] NOT NULL,
	[Digito] [int] NOT NULL,
	[Senha] [varchar](128) NOT NULL,
	[Saldo] [decimal](18, 2) NOT NULL,
	[TipoConta] [char](1) NULL,
 CONSTRAINT [PK_ContaCorrente] PRIMARY KEY CLUSTERED 
(
	[Numero] ASC,
	[Digito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lacamentos]    Script Date: 28/03/2020 14:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lacamentos](
	[LancamentoId] [bigint] IDENTITY(1,1) NOT NULL,
	[ContaOrigem] [varchar](50) NOT NULL,
	[ContaDestino] [varchar](50) NOT NULL,
	[DataRealizacao] [datetime] NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[TX] [varchar](512) NOT NULL,
 CONSTRAINT [PK_Lacamentos] PRIMARY KEY CLUSTERED 
(
	[LancamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([ClienteId], [NomeCompleto], [Cpf], [DataNascimento]) VALUES (1, N'DIOGO LUIZ PONCE', 1234567890, CAST(N'1984-04-30T00:00:00.000' AS DateTime))
INSERT [dbo].[Cliente] ([ClienteId], [NomeCompleto], [Cpf], [DataNascimento]) VALUES (3, N'SOPHIA ROBERTA PONCE', 22739541074, CAST(N'2000-01-10T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Cliente] OFF
SET IDENTITY_INSERT [dbo].[ContaCorrente] ON 

INSERT [dbo].[ContaCorrente] ([ContaCorrenteId], [ClienteId], [DataAbertura], [Numero], [Digito], [Senha], [Saldo], [TipoConta]) VALUES (2, 3, CAST(N'2020-03-27T21:27:55.323' AS DateTime), 784512, 1, N'db64ae11bcc6a6bf14955b2932494020680aa245ec2feaa092a3ffb10a30a050', CAST(100.00 AS Decimal(18, 2)), N'P')
INSERT [dbo].[ContaCorrente] ([ContaCorrenteId], [ClienteId], [DataAbertura], [Numero], [Digito], [Senha], [Saldo], [TipoConta]) VALUES (1, 1, CAST(N'2020-03-27T21:27:52.010' AS DateTime), 895623, 1, N'093933ee964e49f82bac81f69e83213f986b7f8a2a83b6ab0a42fcab1cf47f4e', CAST(100.00 AS Decimal(18, 2)), N'p')
SET IDENTITY_INSERT [dbo].[ContaCorrente] OFF
/****** Object:  Index [IX_Cliente]    Script Date: 28/03/2020 14:26:37 ******/
ALTER TABLE [dbo].[Cliente] ADD  CONSTRAINT [IX_Cliente] UNIQUE NONCLUSTERED 
(
	[Cpf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ContaCorrente]    Script Date: 28/03/2020 14:26:37 ******/
ALTER TABLE [dbo].[ContaCorrente] ADD  CONSTRAINT [IX_ContaCorrente] UNIQUE NONCLUSTERED 
(
	[DataAbertura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContaCorrente] ADD  CONSTRAINT [DF_ContaCorrente_Digito]  DEFAULT ((1)) FOR [Digito]
GO
ALTER TABLE [dbo].[ContaCorrente] ADD  CONSTRAINT [DF_ContaCorrente_Saldo]  DEFAULT ((0)) FOR [Saldo]
GO
ALTER TABLE [dbo].[ContaCorrente] ADD  CONSTRAINT [DF_ContaCorrente_TipoConta]  DEFAULT ('P') FOR [TipoConta]
GO
ALTER TABLE [dbo].[Lacamentos] ADD  CONSTRAINT [DF_Lacamentos_DataRealizacao]  DEFAULT (getdate()) FOR [DataRealizacao]
GO
ALTER TABLE [dbo].[Lacamentos]  WITH CHECK ADD  CONSTRAINT [CK_Lacamentos_Maior_Que_Zero] CHECK  (([Valor]>(0)))
GO
ALTER TABLE [dbo].[Lacamentos] CHECK CONSTRAINT [CK_Lacamentos_Maior_Que_Zero]
GO
/****** Object:  StoredProcedure [dbo].[usp_cmdTranferTo]    Script Date: 28/03/2020 14:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: DIOGO LUIZ PONCE
-- Create date: 28/03/2020
-- =============================================
CREATE PROCEDURE [dbo].[usp_cmdTranferTo]
    @OrigemNumero INT,
    @OrigemDigito INT,
    @OrigemSenha VARCHAR(128),
    @DestinoNumero INT,
    @DestinoDigito INT,
    @DestinoValor DECIMAL(18, 2)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;

        DECLARE @OrigemSaldo DECIMAL(18, 2);

        --Obtem o saldo da conta
        SELECT @OrigemSaldo = Saldo
        FROM dbo.ContaCorrente
        WHERE Numero = @OrigemNumero
              AND Digito = @OrigemDigito
              AND Senha = @OrigemSenha;

        IF (@OrigemSaldo IS NOT NULL)
        BEGIN
            IF (@OrigemSaldo > 0 AND @OrigemSaldo >= @DestinoValor)
            BEGIN
                --Valida as partes da transação, a origem e destino não podem ser os mesmos
                IF (CONCAT(@OrigemNumero, @OrigemDigito) <> CONCAT(@DestinoNumero, @DestinoDigito))
                BEGIN
                    --Valida se a conta destino existe
                    IF (EXISTS
                    (
                        SELECT *
                        FROM dbo.ContaCorrente
                        WHERE Numero = @DestinoNumero
                              AND Digito = @DestinoDigito
                    )
                       )
                    BEGIN
                        --Subtrai o saldo da origem
                        UPDATE dbo.ContaCorrente
                        SET Saldo -= @DestinoValor
                        WHERE Numero = @OrigemNumero
                              AND Digito = @OrigemDigito
                              AND Senha = @OrigemSenha;;

                        --Adiciona saldo no destino
                        UPDATE dbo.ContaCorrente
                        SET Saldo += @DestinoValor
                        WHERE Numero = @DestinoNumero
                              AND Digito = @DestinoDigito;

                        DECLARE @TX VARCHAR(512);

                        SELECT @TX
                            = CONVERT(
                                         VARCHAR(256),
                                         HASHBYTES(
                                                      'SHA2_256',
                                                      CONCAT(
                                                                @OrigemNumero,
                                                                '-',
                                                                @OrigemDigito,
                                                                '.',
                                                                @DestinoNumero,
                                                                '-',
                                                                @DestinoDigito,
                                                                '.',
                                                                @DestinoValor
                                                            )
                                                  ),
                                         2
                                     );

                        INSERT INTO dbo.Lacamentos
                        (
                            ContaOrigem,
                            ContaDestino,
                            DataRealizacao,
                            Valor,
                            TX
                        )
                        VALUES
                        (   CONCAT(@OrigemNumero, '-', @OrigemDigito),   -- ContaOrigem - varchar(50)
                            CONCAT(@DestinoNumero, '-', @DestinoDigito), -- ContaDestino - varchar(50)
                            GETDATE(),                                   -- DataRealizacao - datetime
                            @DestinoValor,                               -- Valor - decimal(18, 2)
                            @TX                                          -- TX - varchar(512)
                            );

                        COMMIT;
                        SELECT 1 AS ID,
                               'OK' AS [Message];
                    END;
                    ELSE
                    BEGIN
                        ROLLBACK;
                        SELECT -1 AS ID,
                               'Conta destino inválida' AS [Message];
                    END;
                END;
                ELSE
                BEGIN
                    ROLLBACK;
                    SELECT -1 AS ID,
                           'Transação inválida' AS [Message];
                END;

            END;
            ELSE
            BEGIN
                ROLLBACK;
                SELECT -1 AS ID,
                       'Saldo insuficiente' AS [Message];
            END;
        END;
        ELSE
        BEGIN
            ROLLBACK;
            SELECT -1 AS ID,
                   'Acesso negado' AS [Message];
        END;

    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT -1 AS ID,
               ERROR_MESSAGE() AS [Message];
    END CATCH;
END;

GO
/****** Object:  StoredProcedure [dbo].[usp_insContaCorrente]    Script Date: 28/03/2020 14:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: DIOGO LUIZ PONCE
-- Create date: 28/03/2020
-- =============================================
CREATE PROCEDURE [dbo].[usp_insContaCorrente]
    @ClienteId INT,
    @Numero INT,
    @Digito INT,
    @Senha VARCHAR(128),
    @TipoConta CHAR(1) = 'P'
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;

        INSERT INTO dbo.ContaCorrente
        (
            ClienteId,
            DataAbertura,
            Numero,
            Digito,
            Senha,
            Saldo,
            TipoConta
        )
        VALUES
        (   @ClienteId, -- ClienteId - bigint
            GETDATE(),  -- DataAbertura - datetime
            @Numero,    -- Numero - int
            @Digito,    -- Digito - int
            @Senha,     -- Senha - varchar(128)
            0.0,        -- Saldo - decimal(18, 2)
            @TipoConta  -- TipoConta - char(1)
            );
        COMMIT;
        SELECT SCOPE_IDENTITY() AS ID,
               'OK' AS [Message];
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT -1 AS ID,
               ERROR_MESSAGE() AS [Message];
    END CATCH;
END;

GO
/****** Object:  StoredProcedure [dbo].[usp_updCliente]    Script Date: 28/03/2020 14:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: DIOGO LUIZ PONCE
-- Create date: 28/03/2020
-- =============================================
CREATE PROCEDURE [dbo].[usp_updCliente]
    @NomeCompleto VARCHAR(100),
    @Cpf BIGINT,
    @DataNascimento DATETIME,
    @ClienteId BIGINT = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        IF @ClienteId IS NULL
        BEGIN
            INSERT INTO dbo.Cliente
            (
                NomeCompleto,
                Cpf,
                DataNascimento
            )
            VALUES
            (   @NomeCompleto,  -- NomeCompleto - varchar(100)
                @Cpf,           -- Cpf - bigint
                @DataNascimento -- DataNascimento - datetime
                );
            SET @ClienteId = SCOPE_IDENTITY();
        END;
        ELSE
        BEGIN
            UPDATE dbo.Cliente
            SET NomeCompleto = @NomeCompleto,
                DataNascimento = @DataNascimento
            WHERE ClienteId = @ClienteId;
        END;
        COMMIT;
        SELECT @ClienteId AS ID,
               'OK' AS [Message];
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT -1 AS ID,
               ERROR_MESSAGE() AS [Message];
    END CATCH;
END;
GO
USE [master]
GO
ALTER DATABASE [DBServerPonce] SET  READ_WRITE 
GO
