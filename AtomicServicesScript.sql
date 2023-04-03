
/****** Object:  Table [dbo].[AtomicServices]    Script Date: 31/03/2023 20:46:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtomicServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceSKU] [varchar](20) NULL,
	[ServiceName] [varchar](250) NULL,
	[ServiceType] [varchar](20) NULL,
	[NbUsersMax] [int] NULL,
	[NbShippingMax] [int] NULL,
	[NbStoresMax] [int] NULL,
	[CustomerType] [varchar](50) NULL,
	[NbLabelsMax] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[DisplayGroup] [varchar](50) NULL,
	[DisplayOrder] [int] NULL,
	[UpgradeID] [int] NULL,
	[DownGradeID] [int] NULL,
	[ServiceDescription] [nvarchar](max) NULL,
	[UnitPriceExclTax] [decimal](18, 2) NOT NULL,
	[Visible] [bit] NOT NULL,
	[ImageFilePath] [nvarchar](max) NULL,
	[MonthUnitPriceExclTax] [decimal](18, 2) NULL,
	[AnnualUnitPriceExclTax] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AtomicServices] ON 
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (1, N'ECOWMS', N'Eco plan', N'PLAN', NULL, 500, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Boutique Eco', CAST(100.00 AS Decimal(18, 2)), 1, NULL, CAST(10.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (2, N'BUSINESSWMS', N'Business plan', N'PLAN', NULL, 500, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Boutique Business', CAST(200.00 AS Decimal(18, 2)), 1, NULL, CAST(20.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (3, N'BTOBWMS', N'BToB plan', N'PLAN', NULL, 500, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Boutique BToB', CAST(500.00 AS Decimal(18, 2)), 1, NULL, CAST(50.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (4, N'TECHWMS', N'Tech plan', N'PLAN', NULL, 500, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Boutique Tech', CAST(900.00 AS Decimal(18, 2)), 1, NULL, CAST(90.00 AS Decimal(18, 2)), CAST(900.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (5, N'PACK1000', N'Pack 1000 shippings / month', N'ADDON', NULL, 1000, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Pack 1000 commandes supplémentaires / mois', CAST(200.00 AS Decimal(18, 2)), 1, NULL, CAST(20.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (6, N'STOCKUPDATE', N'Daily stock update', N'ADDON', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Option mise à jour des stocks', CAST(150.00 AS Decimal(18, 2)), 1, NULL, CAST(15.00 AS Decimal(18, 2)), CAST(150.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (7, N'STOREPARAM', N'Settings service by support team', N'ADDON', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Paramétrage 1 boutique et ses transporteurs', CAST(100.00 AS Decimal(18, 2)), 1, NULL, CAST(10.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (8, N'PDFPARAM', N'Business document settings', N'ADDON', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Paramétrage spécifique document PDF', CAST(100.00 AS Decimal(18, 2)), 1, NULL, CAST(10.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (9, N'FREE', N'Boutique gratuite', N'FREEPLAN', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Boutique gratuite', CAST(0.00 AS Decimal(18, 2)), 1, NULL, NULL, NULL)
GO
INSERT [dbo].[AtomicServices] ([Id], [ServiceSKU], [ServiceName], [ServiceType], [NbUsersMax], [NbShippingMax], [NbStoresMax], [CustomerType], [NbLabelsMax], [StartDate], [EndDate], [DisplayGroup], [DisplayOrder], [UpgradeID], [DownGradeID], [ServiceDescription], [UnitPriceExclTax], [Visible], [ImageFilePath], [MonthUnitPriceExclTax], [AnnualUnitPriceExclTax]) VALUES (10, N'FREETEMP', N'Boutique temporairement gratuite', N'FREEPLAN', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Boutique temporairement gratuite', CAST(0.00 AS Decimal(18, 2)), 1, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[AtomicServices] OFF
GO
