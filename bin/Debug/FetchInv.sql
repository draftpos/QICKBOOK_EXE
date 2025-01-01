USE [FetchInv]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 27/12/2024 06:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](255) NULL,
	[MailingName] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[PinCode] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[ShowLogo] [nvarchar](10) NULL,
	[CurrencyCode] [nvarchar](10) NULL,
	[Currency] [nvarchar](50) NULL,
	[InvoiceHeader] [nvarchar](255) NULL,
	[wscalable] [bit] NULL,
	[pscalable] [bit] NULL,
	[MultiCurrencyReceipt] [bit] NULL,
	[ShowMultiCurrency] [bit] NULL,
	[RevMaxKey] [nvarchar](max) NULL,
	[ShowDiscount] [nvarchar](10) NULL,
	[VatNo] [nvarchar](50) NULL,
	[EnableRevMax] [nvarchar](10) NULL,
	[selnegative] [nvarchar](10) NULL,
	[ZeroPrice] [nvarchar](10) NULL,
	[BelowCost] [nvarchar](10) NULL,
	[ActiveBelow] [nvarchar](10) NULL,
	[NP] [int] NULL,
	[QCode] [nvarchar](10) NULL,
	[BCode] [nvarchar](10) NULL,
	[Website] [nvarchar](255) NULL,
	[TIN] [nvarchar](50) NULL,
	[Logo] [image] NULL,
	[ItemWiseVAT] [nvarchar](10) NULL,
	[QTC] [nvarchar](max) NULL,
	[ServiceTaxNo] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditMemo]    Script Date: 27/12/2024 06:44:46 ******/
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
	[Currency] [varchar](50) NULL,
	[Amount] [numeric](18, 3) NULL,
	[CreditNoteNumber] [nvarchar](50) NULL,
	[Subtotal] [decimal](18, 0) NULL,
	[SalesTaxPercentage] [decimal](18, 0) NULL,
	[SalesTaxTotal] [decimal](18, 0) NULL,
	[TotalAmount] [decimal](18, 0) NULL,
	[CreditRemaining] [decimal](18, 0) NULL,
	[Message] [nvarchar](max) NULL,
	[QRcode] [image] NULL,
	[DeviceID] [varchar](50) NULL,
	[FiscalDay] [varchar](50) NULL,
	[CustomerRef] [varchar](50) NULL,
	[Vcode] [varchar](90) NULL,
	[HavanoZimraStatus] [bit] NULL,
    [CustomerAddress] [nvarchar] (500) NULL,
	[CustomerTin] [nvarchar](50) NULL,
	[CustomerVat] [nvarchar](50) NULL,
	[CustomerEmail] [nvarchar](100) NULL,
	[CustomerPhone] [nvarchar](50) NULL,
 CONSTRAINT [PK_CreditMemo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 27/12/2024 06:44:46 ******/
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
/****** Object:  Table [dbo].[Invoice]    Script Date: 27/12/2024 06:44:46 ******/
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
	[InvoiceNumber] [nvarchar](250) NULL,
	[QRcode] [image] NULL,
	[DeviceID] [varchar](10) NULL,
	[FiscalDay] [varchar](10) NULL,
	[CustomerRef] [varchar](150) NULL,
	[ReceiptNo] [varchar](250) NULL,
	[Vcode] [varchar](100) NULL,
	[HavanoZimraStatus] [bit] NULL,
    [CustomerAddress] [nvarchar](500) NULL,
	[CustomerTin] [nvarchar](50) NULL,
	[CustomerVat] [nvarchar](50) NULL,
	[CustomerEmail] [nvarchar](100) NULL,
	[CustomerPhone] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 27/12/2024 06:44:46 ******/
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
	[Vat] [nvarchar](50) NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CreditMemo] ADD  CONSTRAINT [DF_CreditMemo_HavanoZimraStatus]  DEFAULT ((0)) FOR [HavanoZimraStatus]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_HavanoZimraStatus]  DEFAULT ((0)) FOR [HavanoZimraStatus]
GO
