--create db first
--CREATE DATABASE [Interview];

USE [Interview]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 6/4/2025 2:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/4/2025 2:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderNO] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseCommission]    Script Date: 6/4/2025 2:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseCommission](
	[Id] [uniqueidentifier] NOT NULL,
	[CurrentDate] [date] NOT NULL,
	[ProductCode] [int] NOT NULL,
	[ProductDescription] [nvarchar](50) NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[RequiredDate] [date] NULL,
	[Comment] [nvarchar](200) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_PurchaseCommision] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseCommissionCustomer]    Script Date: 6/4/2025 2:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseCommissionCustomer](
	[Id] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [varchar](50) NOT NULL,
	[TotalPrice] [varchar](50) NOT NULL,
	[DeliveryDate] [date] NULL,
	[PurchaseCondition] [nvarchar](50) NULL,
	[ProducerName] [nvarchar](50) NULL,
	[LastPurchaseDate] [date] NULL,
	[PurchaseCommissionId] [uniqueidentifier] NOT NULL,
	[SellerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PurchaseCommissionCustomer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PurchaseCommission]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseCommission_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[PurchaseCommission] CHECK CONSTRAINT [FK_PurchaseCommission_Order]
GO
ALTER TABLE [dbo].[PurchaseCommissionCustomer]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseCommissionCustomer_Customer] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[PurchaseCommissionCustomer] CHECK CONSTRAINT [FK_PurchaseCommissionCustomer_Customer]
GO
ALTER TABLE [dbo].[PurchaseCommissionCustomer]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseCommissionCustomer_PurchaseCommission] FOREIGN KEY([PurchaseCommissionId])
REFERENCES [dbo].[PurchaseCommission] ([Id])
GO
ALTER TABLE [dbo].[PurchaseCommissionCustomer] CHECK CONSTRAINT [FK_PurchaseCommissionCustomer_PurchaseCommission]
GO
INSERT INTO [Customer] (Id, Name)
VALUES (NEWID(), N'فروشنده شماره 1');
INSERT INTO [Customer] (Id, Name)
VALUES (NEWID(), N'فروشنده شماره 2');
INSERT INTO [Customer] (Id, Name)
VALUES (NEWID(), N'فروشنده شماره 3');
INSERT INTO [Order] (Id, OrderNO)
VALUES (NEWID(), 1234);
INSERT INTO [Order] (Id, OrderNO)
VALUES (NEWID(), 5678);
INSERT INTO [Order] (Id, OrderNO)
VALUES (NEWID(), 9999);
