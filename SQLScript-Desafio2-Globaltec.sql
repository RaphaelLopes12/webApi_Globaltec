--Criar o banco de dados
CREATE DATABASE [desafio2_globaltec]
GO

USE [desafio2_globaltec]
GO

--Criar as tabelas
CREATE TABLE [dbo].[pessoas](
	[Codigo] [bigint] NOT NULL,
	[Nome] [varchar](110) NOT NULL,
	[CpfCnpj] [varchar](14) NOT NULL,
 CONSTRAINT [PK_pessoas] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_pessoas] UNIQUE NONCLUSTERED 
(
	[CpfCnpj] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ContasPagas](
	[Numero] [bigint] NOT NULL,
	[CodigoFornecedor] [bigint] NOT NULL,
	[DataVencimento] [date] NOT NULL,
	[DataPagamento] [date] NOT NULL,
	[Valor] [numeric](18, 6) NOT NULL,
	[Acrescimo] [numeric](18, 6) NULL,
	[Desconto] [numeric](18, 6) NULL,
 CONSTRAINT [PK_ContasPagas] PRIMARY KEY CLUSTERED 
(
	[Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ContasAPagar](
	[Numero] [bigint] NOT NULL,
	[CodigoFornecedor] [bigint] NOT NULL,
	[DataVencimento] [date] NOT NULL,
	[DataProrrogacao] [date] NULL,
	[Valor] [numeric](18, 6) NOT NULL,
	[Acrescimo] [numeric](18, 6) NULL,
	[Desconto] [numeric](18, 6) NULL,
 CONSTRAINT [PK_ContasAPagar] PRIMARY KEY CLUSTERED 
(
	[Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabelas
insert into dbo.pessoas
	  (Codigo, Nome, cpfCnpj)
values (1, 'Raphael Ferreira Lopes', '22233344455');

insert into dbo.pessoas
	  (Codigo, Nome, cpfCnpj)
values (2, 'Fernanda Silva', '88899977766');

insert into dbo.pessoas
	  (Codigo, Nome, cpfCnpj)
values (3, 'Albert Einstein', '11122233344');

 insert into dbo.pessoas
	  (Codigo, Nome, cpfCnpj)
values (4, 'Silva Lopes Ferreira', '41346557000166');

 insert into dbo.pessoas
	  (Codigo, Nome, cpfCnpj)
values (5, 'Ralph Tech', '41346557000166');

insert 
  into dbo.ContasAPagar 
      (Numero
	  ,CodigoFornecedor
	  ,DataVencimento
	  ,DataProrrogacao
	  ,Valor
	  ,Acrescimo
	  ,Desconto)
values
      (1
	  ,4
	  ,'16-06-2022'
	  ,null
	  ,1290.65
	  ,0
	  ,0);

insert 
  into dbo.ContasAPagar 
      (Numero
	  ,CodigoFornecedor
	  ,DataVencimento
	  ,DataProrrogacao
	  ,Valor
	  ,Acrescimo
	  ,Desconto)
values
      (2
	  ,4
	  ,'21-06-2022'
	  ,'24-06-2022'
	  ,500.62
	  ,50.06
	  ,0);

insert 
  into dbo.ContasAPagar 
      (Numero
	  ,CodigoFornecedor
	  ,DataVencimento
	  ,DataProrrogacao
	  ,Valor
	  ,Acrescimo
	  ,Desconto)
values
      (3
	  ,5
	  ,'25-06-2022'
	  ,null
	  ,500.62
	  ,0
	  ,0);

insert 
  into dbo.ContasPagas
      (Numero
	  ,CodigoFornecedor
	  ,DataVencimento
	  ,DataPagamento
	  ,Valor
	  ,Acrescimo
	  ,Desconto)
values 
	  (4
	  ,1
	  ,'29-06-2022'
	  ,'29-06-2022'
	  ,178.00
	  ,0
	  ,0);

insert 
  into dbo.ContasPagas
      (Numero
	  ,CodigoFornecedor
	  ,DataVencimento
	  ,DataPagamento
	  ,Valor
	  ,Acrescimo
	  ,Desconto)
values 
	  (5
	  ,1
	  ,'29-06-2022'
	  ,'25-06-2022'
	  ,178.00
	  ,0
	  ,10.00);

insert 
  into dbo.ContasPagas
      (Numero
	  ,CodigoFornecedor
	  ,DataVencimento
	  ,DataPagamento
	  ,Valor
	  ,Acrescimo
	  ,Desconto)
values 
	  (6
	  ,3
	  ,'29-06-2022'
	  ,'02-07-2022'
	  ,215.00
	  ,10
	  ,0);

-- Retorno do desafio 2 em query
select cp.numero
	  ,ps.Nome
	  ,cp.DataVencimento
	  ,null as DataPagamento
	  ,(cp.valor - cp.Desconto) + cp.Acrescimo as ValorLiquido
	  ,'A PAGAR' as Identificador
  from dbo.ContasAPagar as cp
  left join dbo.pessoas as ps on (cp.CodigoFornecedor = ps.Codigo)
union all
select cpg.numero
	  ,ps.Nome
	  ,cpg.DataVencimento
	  ,cpg.DataPagamento
	  ,(cpg.valor - cpg.Desconto) + cpg.Acrescimo as ValorLiquido
	  ,'PAGA' as Identificador
  from dbo.ContasPagas as cpg
  left join dbo.pessoas as ps on (cpg.CodigoFornecedor = ps.Codigo);

