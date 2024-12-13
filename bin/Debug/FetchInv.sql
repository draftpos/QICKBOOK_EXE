USE [FetchInv]
/****** Object:  Table [dbo].[Company]    Script Date: 11-Dec-24 3:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](250) NOT NULL,
	[MailingName] [nvarchar](250) NULL,
	[Country] [nvarchar](250) NULL,
	[Address] [nvarchar](250) NULL,
	[City] [nchar](200) NULL,
	[State] [nchar](200) NULL,
	[PinCode] [nchar](30) NULL,
	[ContactNo] [nchar](100) NULL,
	[Fax] [nchar](100) NULL,
	[Email] [nchar](200) NULL,
	[Website] [nvarchar](max) NULL,
	[TIN] [nchar](50) NULL,
	[LicenseNo] [nchar](50) NULL,
	[ServiceTaxNo] [nvarchar](max) NULL,
	[CST] [nchar](50) NULL,
	[PAN] [nchar](100) NULL,
	[CurrencyCode] [nchar](10) NULL,
	[Currency] [nchar](100) NULL,
	[Logo] [image] NULL,
	[ShowLogo] [nchar](20) NULL,
	[CapitalAccount] [decimal](18, 3) NULL,
	[NP] [int] NULL,
	[QCode] [nchar](10) NULL,
	[BCode] [nchar](10) NULL,
	[InvoiceHeader] [nchar](50) NULL,
	[ItemWiseVAT] [nchar](10) NULL,
	[QTC] [nvarchar](max) NULL,
	[ZeroPrice] [varchar](50) NULL,
	[BelowCost] [varchar](50) NULL,
	[ActiveBelow] [varchar](50) NULL,
	[wscalable] [bit] NULL,
	[pscalable] [bit] NULL,
	[MultiCurrencyReceipt] [bit] NULL,
	[ShowMultiCurrency] [bit] NULL,
	[VatNo] [varchar](50) NULL,
	[RevMaxKey] [varchar](200) NULL,
	[ShowDiscount] [bit] NULL,
	[EnableRevMax] [bit] NULL,
	[selnegative] [bit] NULL,
	[patchinter] [bit] NULL,
	[cash] [bit] NULL,
	[autoprint] [bit] NULL,
	[textprinting] [bit] NULL,
	[autoprintshift] [bit] NULL,
	[vat_display] [bit] NULL,
	[resturantui] [bit] NULL,
	[Ishavanozimra] [bit] NULL,
	[havanozimrakey] [varchar](500) NULL,
	[Ishavanozra] [bit] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditMemo]    Script Date: 11-Dec-24 3:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditMemo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TxnId] [nvarchar](50) NULL,
	[CustomerName] [nvarchar](max) NULL,
	[TxnDate] [nvarchar](50) NULL,
	[CustomerListId] [nvarchar](50) NULL,
	[Amount] [numeric](18, 3) NULL,
	[CreditNoteNumber] [nvarchar](50) NULL,
	[Subtotal] [decimal](18, 0) NULL,
	[SalesTaxPercentage] [decimal](18, 0) NULL,
	[SalesTaxTotal] [decimal](18, 0) NULL,
	[TotalAmount] [decimal](18, 0) NULL,
	[CreditRemaining] [decimal](18, 0) NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_CreditMemo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11-Dec-24 3:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sClientCode] [nvarchar](50) NULL,
	[sClientName] [nvarchar](100) NULL,
	[sClientGrpCode] [nvarchar](50) NULL,
	[sClientGrpName] [nvarchar](100) NULL,
	[sRepCode] [nvarchar](50) NULL,
	[sClassification] [nvarchar](25) NULL,
	[sAddress] [nvarchar](500) NULL,
	[sPostCode] [nvarchar](20) NULL,
	[sCity] [nvarchar](50) NULL,
	[sCountry] [nvarchar](30) NULL,
	[sEmail] [nvarchar](50) NULL,
	[sPhone] [nvarchar](50) NULL,
	[sPayTerms] [nvarchar](50) NULL,
	[sDistrictName] [nvarchar](50) NULL,
	[sProv] [nvarchar](50) NULL,
	[sTerrName] [nvarchar](50) NULL,
	[sControlType] [nvarchar](50) NULL,
	[sBannerName] [nvarchar](50) NULL,
	[iAge] [smallint] NULL,
	[dDateOfBirth] [date] NULL,
	[Sex] [nvarchar](15) NULL,
	[sSegment] [nvarchar](100) NULL,
	[sMarket] [nvarchar](100) NULL,
	[sCustomerVat] [nvarchar](50) NULL,
	[sCurrency] [nvarchar](50) NULL,
	[sCustomerTin] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 11-Dec-24 3:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TxnId] [nvarchar](50) NOT NULL,
	[CustomerName] [nvarchar](255) NOT NULL,
	[TxnDate] [nvarchar](10) NOT NULL,
	[CustomerListId] [nvarchar](50) NOT NULL,
	[Amount] [decimal](18, 2) NULL,
	[AppliedAmount] [decimal](18, 2) NULL,
	[Subtotal] [decimal](18, 2) NULL,
	[SalesTaxPercentage] [decimal](18, 2) NULL,
	[SalesTaxTotal] [decimal](18, 2) NULL,
	[BalanceRemaining] [decimal](18, 2) NULL,
	[Currency] [nvarchar](50) NULL,
	[ExchangeRate] [decimal](18, 6) NULL,
	[BalanceRemainingInHomeCurrency] [decimal](18, 2) NULL,
	[InvoiceNumber] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 11-Dec-24 3:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ListId] [nvarchar](50) NULL,
	[Name] [nvarchar](max) NULL,
	[Qty] [real] NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[TxnId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_LicenseNo]  DEFAULT ((0.00)) FOR [LicenseNo]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_CapitalAccount]  DEFAULT ((0.00)) FOR [CapitalAccount]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [wscalable]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [pscalable]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [MultiCurrencyReceipt]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [ShowMultiCurrency]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [ShowDiscount]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [EnableRevMax]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [selnegative]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [patchinter]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [cash]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [autoprint]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [textprinting]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [autoprintshift]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [vat_display]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [resturantui]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [Ishavanozimra]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [Ishavanozra]
GO
USE [master]
GO
ALTER DATABASE [FetchInv] SET  READ_WRITE 
GO
