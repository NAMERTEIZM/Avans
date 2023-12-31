/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2022 (16.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2022
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyEventProcessingLogin##]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'##MS_PolicyEventProcessingLogin##')
CREATE LOGIN [##MS_PolicyEventProcessingLogin##] WITH PASSWORD=N'gsuITHN3Cw2+OAypVU3blH4+Y+qosnL8oizvwpH3Iz0=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyEventProcessingLogin##] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyTsqlExecutionLogin##]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'##MS_PolicyTsqlExecutionLogin##')
CREATE LOGIN [##MS_PolicyTsqlExecutionLogin##] WITH PASSWORD=N'4YdkV4K6r7xXroF4fucr3mFHaJw++DgqnrN6+1bBzpo=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyTsqlExecutionLogin##] DISABLE
GO
/****** Object:  Login [BUILTIN\Users]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'BUILTIN\Users')
CREATE LOGIN [BUILTIN\Users] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [DESKTOP-HUUEEIS\kumpo]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'DESKTOP-HUUEEIS\kumpo')
CREATE LOGIN [DESKTOP-HUUEEIS\kumpo] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [muhammetenes]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'muhammetenes')
CREATE LOGIN [muhammetenes] WITH PASSWORD=N'YBxs9KLqAVNY7y5LrKfCQXmCHbCKUg0xA0Bp09+EbD0=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [muhammetenes] DISABLE
GO
/****** Object:  Login [NT AUTHORITY\SYSTEM]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT AUTHORITY\SYSTEM')
CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLTELEMETRY$SQLEXPRESS]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT SERVICE\SQLTELEMETRY$SQLEXPRESS')
CREATE LOGIN [NT SERVICE\SQLTELEMETRY$SQLEXPRESS] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLWriter]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT SERVICE\SQLWriter')
CREATE LOGIN [NT SERVICE\SQLWriter] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\Winmgmt]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT SERVICE\Winmgmt')
CREATE LOGIN [NT SERVICE\Winmgmt] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT Service\MSSQL$SQLEXPRESS]    Script Date: 19.12.2023 18:04:44 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT Service\MSSQL$SQLEXPRESS')
CREATE LOGIN [NT Service\MSSQL$SQLEXPRESS] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [DESKTOP-HUUEEIS\kumpo]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLWriter]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\Winmgmt]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT Service\MSSQL$SQLEXPRESS]
GO
USE [AvansDatabase]
GO
/****** Object:  Table [dbo].[Advance]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Advance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Advance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AdvanceAmount] [money] NULL,
	[AdvanceDescription] [nvarchar](max) NULL,
	[ProjectID] [int] NULL,
	[DesiredDate] [datetime] NULL,
	[StatusID] [int] NULL,
	[RequestDate] [datetime] NULL,
	[isApproved] [bit] NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_Advance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Advance] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[AdvanceHistory]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdvanceHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdvanceHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StatusID] [int] NULL,
	[AdvanceID] [int] NULL,
	[TransactorID] [int] NULL,
	[ApprovedAmount] [money] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_AdvanceHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[AdvanceHistory] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Authorization]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Authorization]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Authorization](
	[AuthorizationID] [int] IDENTITY(1,1) NOT NULL,
	[AuthroizationName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Authorization] PRIMARY KEY CLUSTERED 
(
	[AuthorizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Authorization] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[BusinessUnit]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessUnit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessUnit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessUnitName] [nvarchar](50) NULL,
	[BusinessUnitDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_BusinessUnit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[BusinessUnit] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[PasswordHash] [varbinary](max) NULL,
	[PasswordSalt] [varbinary](max) NULL,
	[BusinessUnitID] [int] NULL,
	[TitleID] [int] NULL,
	[UpperEmployeeID] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Employee] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[EmployeeProject]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeProject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeeProject](
	[EmployeeID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeProject] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[EmployeeProject] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Page]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Page]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Page](
	[PageID] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](50) NULL,
	[Path] [nvarchar](50) NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Page] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DeterminedPaymentDate] [datetime] NULL,
	[FinanceManagerID] [int] NULL,
	[AdvanceID] [int] NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Payment] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Project]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Project]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](50) NULL,
	[ProjectDescription] [nvarchar](max) NULL,
	[EndDate] [datetime] NULL,
	[StartingDate] [datetime] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Project] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Receipt]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Receipt](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptNo] [nvarchar](50) NULL,
	[isRefundReceipt] [bit] NULL,
	[AdvanceID] [int] NULL,
	[ReceiptDate] [datetime] NULL,
	[AccountantID] [int] NULL,
 CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Receipt] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Rule]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Rule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaxAmount] [money] NULL,
	[MinAmount] [money] NULL,
	[Date] [datetime] NULL,
	[TitleID] [int] NULL,
 CONSTRAINT [PK_Rule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Rule] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Status]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Status] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[TitleAuthorization]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TitleAuthorization]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TitleAuthorization](
	[TitleID] [int] NOT NULL,
	[AuthorizationID] [int] NOT NULL,
	[PageID] [int] NOT NULL,
 CONSTRAINT [PK_TitleAuthorization] PRIMARY KEY CLUSTERED 
(
	[TitleID] ASC,
	[AuthorizationID] ASC,
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[TitleAuthorization] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Titles]    Script Date: 19.12.2023 18:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Titles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Titles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TitleName] [nvarchar](50) NULL,
	[TitleDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_Title] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Titles] TO  SCHEMA OWNER 
GO
SET IDENTITY_INSERT [dbo].[Advance] ON 

INSERT [dbo].[Advance] ([ID], [AdvanceAmount], [AdvanceDescription], [ProjectID], [DesiredDate], [StatusID], [RequestDate], [isApproved], [EmployeeID]) VALUES (29, 7500.0000, N'saat 00.06 salı', 5, CAST(N'2023-12-30T00:00:00.000' AS DateTime), 201, NULL, 1, 9)
INSERT [dbo].[Advance] ([ID], [AdvanceAmount], [AdvanceDescription], [ProjectID], [DesiredDate], [StatusID], [RequestDate], [isApproved], [EmployeeID]) VALUES (35, 5665.0000, N'deneme 001', 5, CAST(N'2023-12-09T00:00:00.000' AS DateTime), 201, CAST(N'2023-12-19T16:52:14.680' AS DateTime), 1, 13)
INSERT [dbo].[Advance] ([ID], [AdvanceAmount], [AdvanceDescription], [ProjectID], [DesiredDate], [StatusID], [RequestDate], [isApproved], [EmployeeID]) VALUES (36, 5555.0000, N'deneme 002', 6, CAST(N'2023-12-15T00:00:00.000' AS DateTime), 201, CAST(N'2023-12-19T17:19:13.593' AS DateTime), 1, 13)
SET IDENTITY_INSERT [dbo].[Advance] OFF
GO
SET IDENTITY_INSERT [dbo].[AdvanceHistory] ON 

INSERT [dbo].[AdvanceHistory] ([ID], [StatusID], [AdvanceID], [TransactorID], [ApprovedAmount], [Date]) VALUES (48, 201, 29, 9, 7500.0000, CAST(N'2023-12-19T00:06:16.133' AS DateTime))
INSERT [dbo].[AdvanceHistory] ([ID], [StatusID], [AdvanceID], [TransactorID], [ApprovedAmount], [Date]) VALUES (49, 202, 29, 9, 7400.0000, CAST(N'2023-12-19T00:20:30.127' AS DateTime))
INSERT [dbo].[AdvanceHistory] ([ID], [StatusID], [AdvanceID], [TransactorID], [ApprovedAmount], [Date]) VALUES (50, 203, 29, 9, 7300.0000, CAST(N'2023-12-19T00:21:15.560' AS DateTime))
INSERT [dbo].[AdvanceHistory] ([ID], [StatusID], [AdvanceID], [TransactorID], [ApprovedAmount], [Date]) VALUES (51, 208, 29, 9, 7200.0000, CAST(N'2023-12-19T00:21:43.877' AS DateTime))
INSERT [dbo].[AdvanceHistory] ([ID], [StatusID], [AdvanceID], [TransactorID], [ApprovedAmount], [Date]) VALUES (69, 201, 35, 13, 5665.0000, CAST(N'2023-12-19T16:52:14.690' AS DateTime))
INSERT [dbo].[AdvanceHistory] ([ID], [StatusID], [AdvanceID], [TransactorID], [ApprovedAmount], [Date]) VALUES (70, 201, 36, 13, 5555.0000, CAST(N'2023-12-19T17:19:13.600' AS DateTime))
SET IDENTITY_INSERT [dbo].[AdvanceHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Authorization] ON 

INSERT [dbo].[Authorization] ([AuthorizationID], [AuthroizationName]) VALUES (1, N'Create')
INSERT [dbo].[Authorization] ([AuthorizationID], [AuthroizationName]) VALUES (2, N'Select')
INSERT [dbo].[Authorization] ([AuthorizationID], [AuthroizationName]) VALUES (3, N'Update')
INSERT [dbo].[Authorization] ([AuthorizationID], [AuthroizationName]) VALUES (4, N'Delete')
SET IDENTITY_INSERT [dbo].[Authorization] OFF
GO
SET IDENTITY_INSERT [dbo].[BusinessUnit] ON 

INSERT [dbo].[BusinessUnit] ([ID], [BusinessUnitName], [BusinessUnitDescription]) VALUES (4, N'Yazılımcı', N'Tech ekibi')
INSERT [dbo].[BusinessUnit] ([ID], [BusinessUnitName], [BusinessUnitDescription]) VALUES (5, N'Muhasebe', N'Finansal işlemler')
INSERT [dbo].[BusinessUnit] ([ID], [BusinessUnitName], [BusinessUnitDescription]) VALUES (6, N'İnsan Kaynakları', N'Çalışan memnuniyeti')
INSERT [dbo].[BusinessUnit] ([ID], [BusinessUnitName], [BusinessUnitDescription]) VALUES (7, N'Teknik Destek', N'Yardım')
INSERT [dbo].[BusinessUnit] ([ID], [BusinessUnitName], [BusinessUnitDescription]) VALUES (8, N'İdari İşler', N'Yoğun bölüm')
INSERT [dbo].[BusinessUnit] ([ID], [BusinessUnitName], [BusinessUnitDescription]) VALUES (9, N'Danışma', N'Yardım')
INSERT [dbo].[BusinessUnit] ([ID], [BusinessUnitName], [BusinessUnitDescription]) VALUES (10, N'Analiz', N'Analistler')
SET IDENTITY_INSERT [dbo].[BusinessUnit] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (4, N'Semih', N'Kaya', N'5531106991', N'semihhky@outlook.com', NULL, NULL, 4, 1, 4)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (5, N'Muhammet Enes', N'Ay', N'5551106547', N'menezay@gmail.com', NULL, NULL, 5, 2, 4)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (6, N'Furkan', N'Gökırmak', N'1455789515', N'furkan@mail.com', NULL, NULL, 6, 3, 5)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (7, N'İhsan', N'Kuzuuuu', N'1452475585', N'ihssanss@gmail.com', NULL, NULL, 7, 4, 6)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (8, N'Burak', N'Çoban', N'5513246556', N'burakcoban@hotmail.com', NULL, NULL, 8, 5, 7)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (9, N'Esma', N'Bediz', N'5465321454', N'esma@gmail.com', NULL, NULL, 9, 6, 8)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (10, N'Fatih', N'Çatal', N'5423546545', N'fcatal@gmail.com', NULL, NULL, 10, 7, 9)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (11, N'merdo', N'retto', NULL, N'm@gmail.com', 0x6997858BE631F11DB19A348CBA6399B1993A18D8E04802B87BAF6A0330B3F81F723A3A6495A60EABB96663F1811EC43544C4057DA2ABB465F68CF8A0C1AD01F0, 0x3F2ECB09365AB9C123A0ECA0D57D34FE5BFCDFC2B99E3AF10A93ECA5DF37628B40BB1BF38B2FF884196B0404BDE835B24C21D3A7151C4EF606EF989334E481D80AC0A78004547802EAA450251746E829608EF725958259885AE6362E1EC355C4BDB8B6A18F269D2C6EB0F63C6BE2CBE2083374CEB564E6433BE7B0163393C4E5, NULL, 3, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (12, NULL, NULL, NULL, N's@gmail.com', 0x33DCAA4310F404EA6E1EA8B185619746F78DC435C5E28E544492BBE19375AB608A208BFD63F6DBA8D3390B4F89BCD4DF9E528E533606A0570E5C5C50032722E1, 0xEEEE1BE450ADC1A752DFD065F1D35BF740DAB6217E0269593E19A4C0D1C62D236434AEB1B7046A2AAC419CF52DB7A0AA1BF30BDDB378ED53D4DAC0218C7B6E73D06C3FB17E93A35D7A523F1E1C9F2DAD544FA348BC99A2DF0FF420C13B220F673DC8DD385C7707774914FE5EF546A2389878A4583F36527E22C9E50511579B38, NULL, 3, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (13, N'neco', N'musa', NULL, N'n@gmail.com', 0xDE050AE3D653C9CDDA0CC9A61E6A12E9AB10B97DAFFE7E7A832E55F83C4E664C6770B34932AD3F4ACF8AF442317B5D3B804D2A8477596967EB75E73624C95760, 0x2B83B1FD9F3B4A554A5FA86B152C4F4E54260FD9E067CDF71BF8614C750026C3AA7B44501FBFCD473D0C86ADD07B3D94EA8C8796646C8118E21D8C82E21FB3CA4ACFBD89A814ECB2C5567A4D9CFF8BD056A22456E7DC5A9B561D83BEE8CFB4E98678DE9E7A3758C19ED1CCB1143789C035766408CCD8D15C44637DB63AF70239, NULL, 7, 14)
INSERT [dbo].[Employee] ([ID], [Name], [Surname], [PhoneNumber], [Email], [PasswordHash], [PasswordSalt], [BusinessUnitID], [TitleID], [UpperEmployeeID]) VALUES (14, N'Jon', N'Snow', NULL, N'p@gmail.com', 0x64962A8CE6F8B7230053EFB7D1FDED3092F0575FF90E835D004CF1E5B95EFCA7F878468D661519A5A12ED2330430BA94C64B61E80AD8C9EBACB9158E4AA9E7AF, 0xCF25787A8011EC393698456CA6A97F4B48F1BC06F8FB65606DC45520C2E9A68A360F0ABD3B87D4184A9C2186402FC7D209882FA7BA70F4B9E8D5ED901F7F25F86541F518A5C97DD3F238EC7A740CEE6F77AE6B7FC3EB35CF0FC78F4DB468F7F29E555987C370CB24500908F3E0874911435C2D81E61BE83F598C49671790EFC1, NULL, 5, NULL)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
INSERT [dbo].[EmployeeProject] ([EmployeeID], [ProjectID]) VALUES (7, 3)
INSERT [dbo].[EmployeeProject] ([EmployeeID], [ProjectID]) VALUES (8, 4)
INSERT [dbo].[EmployeeProject] ([EmployeeID], [ProjectID]) VALUES (9, 5)
INSERT [dbo].[EmployeeProject] ([EmployeeID], [ProjectID]) VALUES (10, 6)
GO
SET IDENTITY_INSERT [dbo].[Page] ON 

INSERT [dbo].[Page] ([PageID], [PageName], [Path]) VALUES (2, N'Avans Talep Sayfası', N'/avanstalep')
INSERT [dbo].[Page] ([PageID], [PageName], [Path]) VALUES (3, N'Avans Onay', N'/avansonay')
INSERT [dbo].[Page] ([PageID], [PageName], [Path]) VALUES (4, N'Avans Geri ödeme', N'/avansgeriodeme')
INSERT [dbo].[Page] ([PageID], [PageName], [Path]) VALUES (5, N'Avans Makbuz', N'/avansmakbuz')
INSERT [dbo].[Page] ([PageID], [PageName], [Path]) VALUES (6, N'Avans Finans Müdürü Ekranı', N'/avansfinansmudur')
SET IDENTITY_INSERT [dbo].[Page] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([ID], [ProjectName], [ProjectDescription], [EndDate], [StartingDate]) VALUES (3, N'Turkcell Varlık Zimmet Projesi', N'Bu projede varlıkların zimmete atanma durumları kontrol edilir.', CAST(N'2023-12-12T00:00:00.000' AS DateTime), CAST(N'2022-12-12T00:00:00.000' AS DateTime))
INSERT [dbo].[Project] ([ID], [ProjectName], [ProjectDescription], [EndDate], [StartingDate]) VALUES (4, N'Avans Projesi', N'Proje avans yönetimi gerçekleştirir', CAST(N'2022-11-24T00:00:00.000' AS DateTime), CAST(N'2019-10-15T00:00:00.000' AS DateTime))
INSERT [dbo].[Project] ([ID], [ProjectName], [ProjectDescription], [EndDate], [StartingDate]) VALUES (5, N'Fineksus FM projesi', N'FM Tech', CAST(N'2022-05-20T00:00:00.000' AS DateTime), CAST(N'2020-05-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Project] ([ID], [ProjectName], [ProjectDescription], [EndDate], [StartingDate]) VALUES (6, N'Bilge Adam .NET Dönüşümü', N'Enes yapacak dönüşümü 2 tane log kullansın', CAST(N'2019-10-12T00:00:00.000' AS DateTime), CAST(N'2017-10-12T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
SET IDENTITY_INSERT [dbo].[Rule] ON 

INSERT [dbo].[Rule] ([ID], [MaxAmount], [MinAmount], [Date], [TitleID]) VALUES (1, 1000.0000, 0.0000, CAST(N'2023-12-11T00:00:00.000' AS DateTime), 5)
INSERT [dbo].[Rule] ([ID], [MaxAmount], [MinAmount], [Date], [TitleID]) VALUES (2, 5000.0000, 1001.0000, CAST(N'2023-12-11T00:00:00.000' AS DateTime), 4)
INSERT [dbo].[Rule] ([ID], [MaxAmount], [MinAmount], [Date], [TitleID]) VALUES (3, 10000.0000, 5001.0000, CAST(N'2023-12-11T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Rule] ([ID], [MaxAmount], [MinAmount], [Date], [TitleID]) VALUES (4, NULL, 10001.0000, CAST(N'2023-12-11T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Rule] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (101, N'Bekliyor')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (102, N'Onaylandı')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (103, N'Reddedildi')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (201, N'TalepOluşturuldu BM Onayı Bekleniyor')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (202, N'BM Onayladı Direktör Onayı Bekleniyor')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (203, N'DirektörOnayladı GMY Onayı Bekleniyor')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (204, N'GMY Onayladı')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (205, N'GM Onayladı FM Tarih Belirleyecek')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (206, N'FM Tarih Belirleyecek')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (207, N'Avans Ödendi')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (208, N'Muhasebenin makbuz kesmesi bekleniyor')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (1, 1, 2)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (1, 1, 3)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (1, 2, 2)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (1, 2, 3)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (1, 3, 2)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (1, 3, 3)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (1, 4, 2)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (1, 4, 3)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (2, 1, 4)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (2, 2, 4)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (2, 3, 4)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (2, 4, 4)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (3, 1, 5)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (3, 2, 5)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (3, 3, 5)
INSERT [dbo].[TitleAuthorization] ([TitleID], [AuthorizationID], [PageID]) VALUES (3, 4, 5)
GO
SET IDENTITY_INSERT [dbo].[Titles] ON 

INSERT [dbo].[Titles] ([ID], [TitleName], [TitleDescription]) VALUES (1, N'Genel Müdür', N'Genel Müdür')
INSERT [dbo].[Titles] ([ID], [TitleName], [TitleDescription]) VALUES (2, N'Genel Müdür Yardımcısı', N'Gmy')
INSERT [dbo].[Titles] ([ID], [TitleName], [TitleDescription]) VALUES (3, N'Finans Müdürü', N'Finans müdürü')
INSERT [dbo].[Titles] ([ID], [TitleName], [TitleDescription]) VALUES (4, N'Direktör', N'direktör')
INSERT [dbo].[Titles] ([ID], [TitleName], [TitleDescription]) VALUES (5, N'Birim Müdürü', N'birim müdür')
INSERT [dbo].[Titles] ([ID], [TitleName], [TitleDescription]) VALUES (6, N'Muhasabeci', N'muhasebe/finans')
INSERT [dbo].[Titles] ([ID], [TitleName], [TitleDescription]) VALUES (7, N'İşci', N'işci/çalışan')
SET IDENTITY_INSERT [dbo].[Titles] OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Advance_StatusID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Advance] ADD  CONSTRAINT [DF_Advance_StatusID]  DEFAULT ((201)) FOR [StatusID]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Advance_isApproved]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Advance] ADD  CONSTRAINT [DF_Advance_isApproved]  DEFAULT ((1)) FOR [isApproved]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_AdvanceHistory_StatusID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AdvanceHistory] ADD  CONSTRAINT [DF_AdvanceHistory_StatusID]  DEFAULT ((201)) FOR [StatusID]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Advance_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Advance]'))
ALTER TABLE [dbo].[Advance]  WITH CHECK ADD  CONSTRAINT [FK_Advance_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Advance_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Advance]'))
ALTER TABLE [dbo].[Advance] CHECK CONSTRAINT [FK_Advance_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Advance_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Advance]'))
ALTER TABLE [dbo].[Advance]  WITH CHECK ADD  CONSTRAINT [FK_Advance_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Advance_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Advance]'))
ALTER TABLE [dbo].[Advance] CHECK CONSTRAINT [FK_Advance_Project]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdvanceHistory_Advance]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdvanceHistory]'))
ALTER TABLE [dbo].[AdvanceHistory]  WITH CHECK ADD  CONSTRAINT [FK_AdvanceHistory_Advance] FOREIGN KEY([AdvanceID])
REFERENCES [dbo].[Advance] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdvanceHistory_Advance]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdvanceHistory]'))
ALTER TABLE [dbo].[AdvanceHistory] CHECK CONSTRAINT [FK_AdvanceHistory_Advance]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdvanceHistory_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdvanceHistory]'))
ALTER TABLE [dbo].[AdvanceHistory]  WITH CHECK ADD  CONSTRAINT [FK_AdvanceHistory_Employee] FOREIGN KEY([TransactorID])
REFERENCES [dbo].[Employee] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdvanceHistory_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdvanceHistory]'))
ALTER TABLE [dbo].[AdvanceHistory] CHECK CONSTRAINT [FK_AdvanceHistory_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdvanceHistory_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdvanceHistory]'))
ALTER TABLE [dbo].[AdvanceHistory]  WITH CHECK ADD  CONSTRAINT [FK_AdvanceHistory_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdvanceHistory_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdvanceHistory]'))
ALTER TABLE [dbo].[AdvanceHistory] CHECK CONSTRAINT [FK_AdvanceHistory_Status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_BusinessUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_BusinessUnit] FOREIGN KEY([BusinessUnitID])
REFERENCES [dbo].[BusinessUnit] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_BusinessUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_BusinessUnit]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([UpperEmployeeID])
REFERENCES [dbo].[Employee] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Title]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Title] FOREIGN KEY([TitleID])
REFERENCES [dbo].[Titles] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Title]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Title]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmployeeProject_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmployeeProject]'))
ALTER TABLE [dbo].[EmployeeProject]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProject_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmployeeProject_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmployeeProject]'))
ALTER TABLE [dbo].[EmployeeProject] CHECK CONSTRAINT [FK_EmployeeProject_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmployeeProject_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmployeeProject]'))
ALTER TABLE [dbo].[EmployeeProject]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProject_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmployeeProject_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmployeeProject]'))
ALTER TABLE [dbo].[EmployeeProject] CHECK CONSTRAINT [FK_EmployeeProject_Project]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Payment_Advance]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payment]'))
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Advance] FOREIGN KEY([AdvanceID])
REFERENCES [dbo].[Advance] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Payment_Advance]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payment]'))
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Advance]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Payment_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payment]'))
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Employee] FOREIGN KEY([FinanceManagerID])
REFERENCES [dbo].[Employee] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Payment_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payment]'))
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Receipt_Advance]') AND parent_object_id = OBJECT_ID(N'[dbo].[Receipt]'))
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Advance] FOREIGN KEY([AdvanceID])
REFERENCES [dbo].[Advance] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Receipt_Advance]') AND parent_object_id = OBJECT_ID(N'[dbo].[Receipt]'))
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Advance]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Receipt_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Receipt]'))
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Employee] FOREIGN KEY([AccountantID])
REFERENCES [dbo].[Employee] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Receipt_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Receipt]'))
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rule_Title]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rule]'))
ALTER TABLE [dbo].[Rule]  WITH CHECK ADD  CONSTRAINT [FK_Rule_Title] FOREIGN KEY([TitleID])
REFERENCES [dbo].[Titles] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rule_Title]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rule]'))
ALTER TABLE [dbo].[Rule] CHECK CONSTRAINT [FK_Rule_Title]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TitleAuthorization_Authorization]') AND parent_object_id = OBJECT_ID(N'[dbo].[TitleAuthorization]'))
ALTER TABLE [dbo].[TitleAuthorization]  WITH CHECK ADD  CONSTRAINT [FK_TitleAuthorization_Authorization] FOREIGN KEY([AuthorizationID])
REFERENCES [dbo].[Authorization] ([AuthorizationID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TitleAuthorization_Authorization]') AND parent_object_id = OBJECT_ID(N'[dbo].[TitleAuthorization]'))
ALTER TABLE [dbo].[TitleAuthorization] CHECK CONSTRAINT [FK_TitleAuthorization_Authorization]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TitleAuthorization_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[TitleAuthorization]'))
ALTER TABLE [dbo].[TitleAuthorization]  WITH CHECK ADD  CONSTRAINT [FK_TitleAuthorization_Page] FOREIGN KEY([PageID])
REFERENCES [dbo].[Page] ([PageID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TitleAuthorization_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[TitleAuthorization]'))
ALTER TABLE [dbo].[TitleAuthorization] CHECK CONSTRAINT [FK_TitleAuthorization_Page]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TitleAuthorization_Title]') AND parent_object_id = OBJECT_ID(N'[dbo].[TitleAuthorization]'))
ALTER TABLE [dbo].[TitleAuthorization]  WITH CHECK ADD  CONSTRAINT [FK_TitleAuthorization_Title] FOREIGN KEY([TitleID])
REFERENCES [dbo].[Titles] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TitleAuthorization_Title]') AND parent_object_id = OBJECT_ID(N'[dbo].[TitleAuthorization]'))
ALTER TABLE [dbo].[TitleAuthorization] CHECK CONSTRAINT [FK_TitleAuthorization_Title]
GO
