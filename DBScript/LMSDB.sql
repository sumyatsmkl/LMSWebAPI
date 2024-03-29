USE [master]
GO
/****** Object:  Database [LMSDB]    Script Date: 11/23/2023 12:27:43 AM ******/
CREATE DATABASE [LMSDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LMSDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS2012\MSSQL\DATA\LMSDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LMSDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS2012\MSSQL\DATA\LMSDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LMSDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LMSDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LMSDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LMSDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LMSDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LMSDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LMSDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LMSDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LMSDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [LMSDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LMSDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LMSDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LMSDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LMSDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LMSDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LMSDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LMSDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LMSDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LMSDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LMSDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LMSDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LMSDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LMSDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LMSDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LMSDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LMSDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LMSDB] SET  MULTI_USER 
GO
ALTER DATABASE [LMSDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LMSDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LMSDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LMSDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [LMSDB]
GO
/****** Object:  Table [dbo].[T_Profile]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Profile](
	[ProfileID] [uniqueidentifier] NOT NULL,
	[TenantSubID] [uniqueidentifier] NOT NULL,
	[SalutationTitleID] [uniqueidentifier] NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[PhoneNo] [nvarchar](100) NULL,
	[DateOfBirth] [datetime] NULL,
	[IDNo] [nvarchar](50) NOT NULL,
	[IDTypeID] [uniqueidentifier] NOT NULL,
	[GenderID] [uniqueidentifier] NULL,
	[MaritalStatusID] [uniqueidentifier] NULL,
	[ProfileStatus] [int] NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_T_Profile] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC,
	[TenantSubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Profile_Account]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Profile_Account](
	[ProfileAccountID] [uniqueidentifier] NOT NULL,
	[TenantSubID] [uniqueidentifier] NOT NULL,
	[ProfileID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[AccessToken] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[AccountStatus] [int] NOT NULL,
	[IsMustChangePassword] [bit] NULL,
	[LastLoginTime] [datetime] NULL,
	[LoginRetryCount] [int] NULL,
	[ReactivationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_T_Profile_Account] PRIMARY KEY CLUSTERED 
(
	[ProfileAccountID] ASC,
	[TenantSubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Profile_Education]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Profile_Education](
	[EducationID] [uniqueidentifier] NOT NULL,
	[TenantSubID] [uniqueidentifier] NOT NULL,
	[ProfileID] [uniqueidentifier] NOT NULL,
	[Institution] [nvarchar](500) NULL,
	[QualificationLevelID] [uniqueidentifier] NULL,
	[QualificationName] [nvarchar](500) NULL,
	[CountryID] [uniqueidentifier] NULL,
	[SchoolStartDate] [datetime] NULL,
	[SchoolEndDate] [datetime] NULL,
	[LanguageProficiencyID] [uniqueidentifier] NULL,
	[IsHighestQualification] [bit] NULL,
	[YearObtained] [datetime] NULL,
	[Remarks] [nvarchar](500) NULL,
	[Status] [int] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
	[IsNextHighestQualification] [bit] NULL,
 CONSTRAINT [PK_T_Profile_Education] PRIMARY KEY CLUSTERED 
(
	[EducationID] ASC,
	[TenantSubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Profile_Role]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Profile_Role](
	[ProfileRoleID] [uniqueidentifier] NOT NULL,
	[TenantSubID] [uniqueidentifier] NOT NULL,
	[ProfileID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[ValidFrom] [datetime] NULL,
	[ValidTo] [datetime] NULL,
	[Remarks] [nvarchar](500) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_T_Profile_Role] PRIMARY KEY CLUSTERED 
(
	[ProfileRoleID] ASC,
	[TenantSubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Role]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Role](
	[RoleID] [uniqueidentifier] NOT NULL,
	[TenantSubID] [uniqueidentifier] NOT NULL,
	[RoleCode] [nvarchar](20) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
	[Status] [int] NOT NULL,
	[Remarks] [nvarchar](200) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_T_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[TenantSubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_System_Code_Value]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_System_Code_Value](
	[CodeValueID] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[CodeValue] [nvarchar](500) NOT NULL,
	[CodeTypeID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[IsDefault] [bit] NULL,
	[Remarks] [nvarchar](500) NULL,
	[CodeOrder] [int] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
	[TenantSubID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_T_System_Code_Value] PRIMARY KEY CLUSTERED 
(
	[CodeValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_System_Currency]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_System_Currency](
	[CurrencyID] [uniqueidentifier] NOT NULL,
	[TenantSubID] [uniqueidentifier] NOT NULL,
	[CurrencyName] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_T_System_Currency] PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC,
	[TenantSubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Tenant_Main]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Tenant_Main](
	[TenantMainID] [uniqueidentifier] NOT NULL,
	[TenantMainName] [nvarchar](200) NULL,
	[Remarks] [nvarchar](500) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_T_Tenant_Main] PRIMARY KEY CLUSTERED 
(
	[TenantMainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Tenant_Sub]    Script Date: 11/23/2023 12:27:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Tenant_Sub](
	[TenantSubID] [uniqueidentifier] NOT NULL,
	[TenantMainID] [uniqueidentifier] NOT NULL,
	[TenantName] [varchar](200) NULL,
	[TelNo] [nvarchar](50) NULL,
	[FaxNo] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Website] [nvarchar](300) NULL,
	[Logo] [varbinary](max) NULL,
	[Status] [int] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
	[TenantCode] [nvarchar](100) NULL,
	[MaxActiveUserCount] [int] NOT NULL,
 CONSTRAINT [PK_T_Tenant_Sub] PRIMARY KEY CLUSTERED 
(
	[TenantSubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[T_Role] ([RoleID], [TenantSubID], [RoleCode], [RoleName], [Status], [Remarks], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (N'd2139701-d03d-4e5e-bd67-76f12813d778', N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4', N'R004', N'Learner', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[T_Role] ([RoleID], [TenantSubID], [RoleCode], [RoleName], [Status], [Remarks], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (N'a1c1ae77-bdb8-463f-8744-79f794a49e2c', N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4', N'R003', N'Lecturer', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[T_Role] ([RoleID], [TenantSubID], [RoleCode], [RoleName], [Status], [Remarks], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (N'be442a4f-79f6-4db7-b9ac-be4ac67bb0c4', N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4', N'R001', N'Super Admin', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[T_Role] ([RoleID], [TenantSubID], [RoleCode], [RoleName], [Status], [Remarks], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (N'11affe40-79a9-47a7-bb05-c0049e857457', N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4', N'R002', N'Admin', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'7c5c3190-cb68-4dd5-b322-04e756b0b56d', N'Q001', N'Certificate', 1, 1, 1, N'qualification level', 1, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'453d28fe-ad70-4a20-aa2b-149119fc2c24', N'L002', N'Spanish', 8, 1, 0, N'language', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'84413bf9-2aba-4efe-947f-22ccf96ab547', N'C008', N'Armenia', 7, 1, 0, N'country', 8, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'84eb7887-053b-4039-aaad-25f2254b27e0', N'ID002', N'Passport', 2, 1, 0, N'passport', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'ec2d1bf0-3ae6-4fc9-a525-2c576024e580', N'ST002', N'Ms', 4, 1, 0, N'Predefine Salutation Title', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'44aca1fd-b6a1-43c5-be79-327310657a16', N'L005', N'Japanese', 8, 1, 0, N'language', 5, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'205a09d5-13ee-4abf-ac7a-33e87c7bfe8d', N'C007', N'Argentina', 7, 1, 0, N'country', 7, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'567d7b06-2387-4625-82cb-346d2ee52c0f', N'MS001', N'Single', 5, 1, 1, N'Marital Status', 1, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'aad872d9-7d27-4e67-bceb-3dbc78e605c4', N'ID001', N'NRIC', 2, 1, 1, N'national registration identity card', 1, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'49724d21-c570-4e63-9888-3dcd9cb1c960', N'ES003', N'Self-Employed', 6, 1, 0, N'employment status', 3, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'9da9531e-e3c0-43c3-845a-5e20129c6cb1', N'C003', N'Algeria', 7, 1, 0, N'country', 3, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'b0c6265b-fd84-4d0c-856b-63986c804a08', N'C002', N'Albania', 7, 1, 0, N'country', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'315ca5d4-90c6-4851-bf65-6f5871b47121', N'Q002', N'Diploma', 1, 1, 0, N'qualification level', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'a5908cf2-3cef-40d3-9100-70f6167bfa49', N'G003', N'Not Specified', 3, 1, 0, N'predefine gender', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'a7fa8c05-3cc9-4f76-8be9-7486d95b7a8f', N'MS002', N'Married', 5, 1, 0, N'Marital Status', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'a4369278-8192-4565-aea0-79743ddee893', N'MS003', N'Divorced', 5, 1, 0, N'Marital Status', 3, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'55e58e48-bd3b-4e14-a5bc-7a46d6a1fde5', N'C011', N'Azerbaijan', 7, 1, 0, N'country', 11, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'a044956e-95c6-46fe-b1f9-7b5c34758bcc', N'L001', N'English', 8, 1, 1, N'language', 1, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'0ffdd664-3034-43e8-8eb9-7d9b2dd5e1f5', N'ST005', N'Prof', 4, 1, 0, N'Predefine Salutation Title', 5, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'efda73c6-f41f-48d9-b2f4-84c3d6f7b3cc', N'C005', N'Angola', 7, 1, 0, N'country', 5, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'60d7e806-2405-4bf7-ab51-85a42d98b32e', N'C010', N'Austria', 7, 1, 0, N'country', 10, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'4c9be767-f0e5-444c-b2b5-875a18f43f17', N'L008', N'French', 8, 1, 0, N'language', 8, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'5c507350-d443-4c32-8f3b-8ce0bfe6a4e4', N'C004', N'Andorra', 7, 1, 0, N'country', 4, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'ae98a80b-bc1d-4734-a8c8-8f6663c718e4', N'MS004', N'Widowed', 5, 1, 0, N'Marital Status', 4, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'efde65e2-aa76-4fb4-b66f-99fcd7f8d396', N'C001', N'Afghanistan', 7, 1, 1, N'country', 1, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'818f0537-ce9c-4185-be87-9de079115432', N'L004', N'Hindi', 8, 1, 0, N'language', 4, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'0a577f06-d8f4-47eb-b081-9e9de4975a98', N'C009', N'Australia', 7, 1, 0, N'country', 9, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'0d68a758-9053-46c2-9f1b-a3e010abcd3a', N'ES001', N'Employed', 6, 1, 1, N'employment status', 1, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'eb0b60f1-9f39-4811-a305-ab412afe61de', N'G002', N'Female', 3, 1, 0, N'predefine gender', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'9f74c3c0-4dae-45cc-8cbe-ae17c2546a58', N'ES002', N'Unemployed', 6, 1, 0, N'employment status', 2, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'2191540a-5061-4779-b522-b32e9ae8ec36', N'L011', N'Russian', 8, 1, 0, N'language', 11, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'8e804b25-c8f6-4017-ba31-b37d71d79fca', N'Q003', N'Bachelor Degree', 1, 1, 0, N'qualification level', 3, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'ef1005b8-8627-44db-90b4-b862293a11be', N'MS005', N'Not Specified', 5, 1, 0, N'Marital Status', 5, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'169623d0-c00d-4889-b241-bdcb657eb9c0', N'L003', N'Chinese', 8, 1, 0, N'language', 3, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'e90cde26-a77f-4fa7-afb5-c18320a33db1', N'L006', N'Arabic', 8, 1, 0, N'language', 6, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'41fbbef0-8a64-4fa1-b591-c52d6d22af03', N'Q004', N'Master Degree', 1, 1, 0, N'qualification level', 4, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'7e489a51-2e9f-492a-a20f-c78765cb3bff', N'C006', N'Antigua and Barbuda', 7, 1, 0, N'country', 6, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'25944142-6623-432e-814d-da33d97ad249', N'ST004', N'Dr', 4, 1, 0, N'Predefine Salutation Title', 4, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'8d7638c9-3c57-464c-8156-db21db923acd', N'L010', N'Turkish', 8, 1, 0, N'language', 10, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'f7d38864-04b9-4fc4-8bd1-e0215c6f4a26', N'L009', N'German', 8, 1, 0, N'language', 9, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'5d274c54-1883-49f1-b70c-e0ce46140326', N'ST001', N'Mr', 4, 1, 1, N'Predefine Salutation Title', 1, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'41d4e2ad-cac9-4771-8809-ed1285074a54', N'ST003', N'Mrs', 4, 1, 0, N'Predefine Salutation Title', 3, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'1ff166d0-eef4-4baa-a36f-f676c0f48d3b', N'L007', N'Korean', 8, 1, 0, N'language', 7, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'b24f5ff0-9639-4269-9839-fc76a8fc67b4', N'G001', N'Male', 3, 1, 1, N'predefine gender', 1, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_System_Code_Value] ([CodeValueID], [Code], [CodeValue], [CodeTypeID], [Status], [IsDefault], [Remarks], [CodeOrder], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantSubID]) VALUES (N'd8a2b51f-ea98-41e2-971b-fce59c09f3b3', N'Q005', N'Doctorate Degree', 1, 1, 0, N'qualification level', 5, NULL, NULL, NULL, NULL, N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4')
INSERT [dbo].[T_Tenant_Main] ([TenantMainID], [TenantMainName], [Remarks], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (N'94991616-d255-4e91-a639-fc6ed1081eec', N'eHealth Education And Training Provider', N'', NULL, NULL, NULL, NULL)
INSERT [dbo].[T_Tenant_Sub] ([TenantSubID], [TenantMainID], [TenantName], [TelNo], [FaxNo], [Email], [Website], [Logo], [Status], [CreatedBy], [CreatedDate], [LastUpdatedBy], [LastUpdatedDate], [TenantCode], [MaxActiveUserCount]) VALUES (N'3e72e19f-058d-4b18-a09a-e0903e9c8ae4', N'94991616-d255-4e91-a639-fc6ed1081eec', N'eHealth Education Provider - Branch 1', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, N'T001', 50)
ALTER TABLE [dbo].[T_Tenant_Sub] ADD  DEFAULT ((50)) FOR [MaxActiveUserCount]
GO
ALTER TABLE [dbo].[T_Profile_Account]  WITH CHECK ADD  CONSTRAINT [FK_T_Profile_Account_T_Profile] FOREIGN KEY([ProfileID], [TenantSubID])
REFERENCES [dbo].[T_Profile] ([ProfileID], [TenantSubID])
GO
ALTER TABLE [dbo].[T_Profile_Account] CHECK CONSTRAINT [FK_T_Profile_Account_T_Profile]
GO
ALTER TABLE [dbo].[T_Profile_Education]  WITH CHECK ADD  CONSTRAINT [FK_T_Profile_Education_T_Profile] FOREIGN KEY([ProfileID], [TenantSubID])
REFERENCES [dbo].[T_Profile] ([ProfileID], [TenantSubID])
GO
ALTER TABLE [dbo].[T_Profile_Education] CHECK CONSTRAINT [FK_T_Profile_Education_T_Profile]
GO
ALTER TABLE [dbo].[T_Profile_Role]  WITH CHECK ADD  CONSTRAINT [FK_T_Profile_Role_T_Role] FOREIGN KEY([RoleID], [TenantSubID])
REFERENCES [dbo].[T_Role] ([RoleID], [TenantSubID])
GO
ALTER TABLE [dbo].[T_Profile_Role] CHECK CONSTRAINT [FK_T_Profile_Role_T_Role]
GO
ALTER TABLE [dbo].[T_Role]  WITH CHECK ADD  CONSTRAINT [FK_T_Role_T_Tenant_Sub] FOREIGN KEY([TenantSubID])
REFERENCES [dbo].[T_Tenant_Sub] ([TenantSubID])
GO
ALTER TABLE [dbo].[T_Role] CHECK CONSTRAINT [FK_T_Role_T_Tenant_Sub]
GO
ALTER TABLE [dbo].[T_System_Code_Value]  WITH CHECK ADD  CONSTRAINT [FK_T_System_Code_Value_T_Tenant_Sub] FOREIGN KEY([TenantSubID])
REFERENCES [dbo].[T_Tenant_Sub] ([TenantSubID])
GO
ALTER TABLE [dbo].[T_System_Code_Value] CHECK CONSTRAINT [FK_T_System_Code_Value_T_Tenant_Sub]
GO
ALTER TABLE [dbo].[T_Tenant_Sub]  WITH CHECK ADD  CONSTRAINT [FK_T_Tenant_Sub_T_Tenant_Main] FOREIGN KEY([TenantMainID])
REFERENCES [dbo].[T_Tenant_Main] ([TenantMainID])
GO
ALTER TABLE [dbo].[T_Tenant_Sub] CHECK CONSTRAINT [FK_T_Tenant_Sub_T_Tenant_Main]
GO
USE [master]
GO
ALTER DATABASE [LMSDB] SET  READ_WRITE 
GO
