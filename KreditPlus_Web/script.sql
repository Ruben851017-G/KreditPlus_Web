USE [master]
GO
/****** Object:  Database [DBKreditPlus]    Script Date: 12/4/2022 8:36:42 PM ******/
CREATE DATABASE [DBKreditPlus]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBKreditPlus', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER2019\MSSQL\DATA\DBKreditPlus.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBKreditPlus_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER2019\MSSQL\DATA\DBKreditPlus_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DBKreditPlus] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBKreditPlus].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBKreditPlus] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBKreditPlus] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBKreditPlus] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBKreditPlus] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBKreditPlus] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBKreditPlus] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBKreditPlus] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBKreditPlus] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBKreditPlus] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBKreditPlus] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBKreditPlus] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBKreditPlus] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBKreditPlus] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBKreditPlus] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBKreditPlus] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBKreditPlus] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBKreditPlus] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBKreditPlus] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBKreditPlus] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBKreditPlus] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBKreditPlus] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBKreditPlus] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBKreditPlus] SET RECOVERY FULL 
GO
ALTER DATABASE [DBKreditPlus] SET  MULTI_USER 
GO
ALTER DATABASE [DBKreditPlus] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBKreditPlus] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBKreditPlus] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBKreditPlus] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBKreditPlus] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBKreditPlus] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBKreditPlus', N'ON'
GO
ALTER DATABASE [DBKreditPlus] SET QUERY_STORE = OFF
GO
USE [DBKreditPlus]
GO
/****** Object:  Table [dbo].[tbl_user_type]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_user_type](
	[user_type_id] [int] IDENTITY(1,1) NOT NULL,
	[user_type_name] [varchar](50) NULL,
	[created_date] [datetime] NULL,
 CONSTRAINT [PK_tbl_user_type] PRIMARY KEY CLUSTERED 
(
	[user_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_user]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](150) NULL,
	[user_full_name] [varchar](150) NULL,
	[password] [varchar](150) NULL,
	[user_type] [smallint] NULL,
	[token] [varchar](max) NULL,
	[token_expired] [datetime] NULL,
	[created_date] [datetime] NULL,
	[last_login] [datetime] NULL,
 CONSTRAINT [PK_tbl_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_user]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[v_user]
as
select a.*,b.user_type_id,b.user_type_name 
from tbl_user a
left join tbl_user_type b on a.user_type = b.user_type_id
GO
/****** Object:  Table [dbo].[tbl_menu]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_menu](
	[menu_id] [int] IDENTITY(1,1) NOT NULL,
	[menu_name] [varchar](150) NULL,
	[status_menu_id] [smallint] NULL,
	[menu_type_id] [smallint] NULL,
	[price] [money] NULL,
 CONSTRAINT [PK_tbl_item] PRIMARY KEY CLUSTERED 
(
	[menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_status_menu]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_status_menu](
	[status_menu_id] [smallint] IDENTITY(1,1) NOT NULL,
	[status_menu_desc] [varchar](150) NULL,
 CONSTRAINT [PK_tbl_status_menu] PRIMARY KEY CLUSTERED 
(
	[status_menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_menu_type]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_menu_type](
	[menu_type_id] [smallint] IDENTITY(1,1) NOT NULL,
	[menu_type_desc] [varchar](100) NULL,
 CONSTRAINT [PK_tbl_menu_type] PRIMARY KEY CLUSTERED 
(
	[menu_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_menu]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[v_menu]
as
select a.menu_id,a.menu_name,a.price,b.menu_type_desc,c.status_menu_desc
from tbl_menu a
left join tbl_menu_type b on a.menu_type_id = b.menu_type_id
left join tbl_status_menu c on a.status_menu_id = c.status_menu_id
GO
/****** Object:  Table [dbo].[tbl_order]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_order](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[order_number] [varchar](50) NULL,
	[order_date] [datetime] NULL,
	[order_status] [smallint] NULL,
	[created_by_cashier] [int] NULL,
	[ordered_by_waiters] [int] NULL,
	[table_number] [smallint] NULL,
	[closed_by] [int] NULL,
 CONSTRAINT [PK_tbl_order] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_order_detail]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_order_detail](
	[order_detail_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [nchar](10) NULL,
	[menu_id] [int] NULL,
	[qty] [int] NULL,
	[price] [money] NULL,
 CONSTRAINT [PK_tbl_order_detail] PRIMARY KEY CLUSTERED 
(
	[order_detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_order_status]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_order_status](
	[order_status_id] [smallint] IDENTITY(1,1) NOT NULL,
	[order_status_desc] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_order_status] PRIMARY KEY CLUSTERED 
(
	[order_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_order]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[v_order]
as
select 
a.order_id,
a.order_number,
a.order_date,
b.order_status_desc as status_order,
c.user_full_name as cashier,
d.user_full_name as waiters,
e.user_full_name as closed_by_name,
a.table_number,
(select sum(x.price) from tbl_order_detail x where x.order_id = a.order_id) as total
from tbl_order a
left join tbl_order_status b on a.order_status = b.order_status_id
left join tbl_user c on a.created_by_cashier = c.user_id
left join tbl_user d on a.ordered_by_waiters = d.user_id
left join tbl_user e on a.closed_by = e.user_id
GO
/****** Object:  View [dbo].[v_transaction]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[v_transaction]
as
select 
a.order_id,
a.order_number,
a.order_date,
b.user_id as user_waiters_order,
b.user_full_name as user_waiters_name_order,
(select sum(x.price) from tbl_order_detail x where x.order_id = a.order_id) as total
from tbl_order a
left join tbl_user b on a.ordered_by_waiters = b.user_id
GO
/****** Object:  Table [dbo].[tbl_transaction]    Script Date: 12/4/2022 8:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_transaction](
	[transaction_id] [int] IDENTITY(1,1) NOT NULL,
	[transaction_date] [datetime] NULL,
	[order_number] [varchar](50) NULL,
	[total] [money] NULL,
 CONSTRAINT [PK_tbl_transaction] PRIMARY KEY CLUSTERED 
(
	[transaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_menu] ON 

INSERT [dbo].[tbl_menu] ([menu_id], [menu_name], [status_menu_id], [menu_type_id], [price]) VALUES (1, N'Soto Ayam', 1, 1, 120000.0000)
INSERT [dbo].[tbl_menu] ([menu_id], [menu_name], [status_menu_id], [menu_type_id], [price]) VALUES (2, N'Nasi Goreng', 2, 1, 90000.0000)
INSERT [dbo].[tbl_menu] ([menu_id], [menu_name], [status_menu_id], [menu_type_id], [price]) VALUES (3, N'Teh Manis', 1, 2, 30000.0000)
INSERT [dbo].[tbl_menu] ([menu_id], [menu_name], [status_menu_id], [menu_type_id], [price]) VALUES (4, N'Kopi Hitam', 1, 5, 20000.0000)
INSERT [dbo].[tbl_menu] ([menu_id], [menu_name], [status_menu_id], [menu_type_id], [price]) VALUES (8, N'Soto Kelinci', 3, 2, 20000.0000)
SET IDENTITY_INSERT [dbo].[tbl_menu] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_menu_type] ON 

INSERT [dbo].[tbl_menu_type] ([menu_type_id], [menu_type_desc]) VALUES (1, N'Food')
INSERT [dbo].[tbl_menu_type] ([menu_type_id], [menu_type_desc]) VALUES (2, N'Drink')
INSERT [dbo].[tbl_menu_type] ([menu_type_id], [menu_type_desc]) VALUES (3, N'Desserts')
INSERT [dbo].[tbl_menu_type] ([menu_type_id], [menu_type_desc]) VALUES (4, N'Soup')
INSERT [dbo].[tbl_menu_type] ([menu_type_id], [menu_type_desc]) VALUES (5, N'Appetizer ')
INSERT [dbo].[tbl_menu_type] ([menu_type_id], [menu_type_desc]) VALUES (6, N'Main course')
INSERT [dbo].[tbl_menu_type] ([menu_type_id], [menu_type_desc]) VALUES (7, N'Hot beverage')
SET IDENTITY_INSERT [dbo].[tbl_menu_type] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_order] ON 

INSERT [dbo].[tbl_order] ([order_id], [order_number], [order_date], [order_status], [created_by_cashier], [ordered_by_waiters], [table_number], [closed_by]) VALUES (1, N'ABC03122022-001', CAST(N'2022-12-02T00:00:00.000' AS DateTime), 2, 2, 2, 1, 2)
INSERT [dbo].[tbl_order] ([order_id], [order_number], [order_date], [order_status], [created_by_cashier], [ordered_by_waiters], [table_number], [closed_by]) VALUES (2, N'ABC03122022-002', CAST(N'2022-12-02T00:00:00.000' AS DateTime), 2, 2, 5, 2, 2)
SET IDENTITY_INSERT [dbo].[tbl_order] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_order_detail] ON 

INSERT [dbo].[tbl_order_detail] ([order_detail_id], [order_id], [menu_id], [qty], [price]) VALUES (1, N'1         ', 1, 2, 24000.0000)
INSERT [dbo].[tbl_order_detail] ([order_detail_id], [order_id], [menu_id], [qty], [price]) VALUES (2, N'1         ', 3, 1, 3000.0000)
INSERT [dbo].[tbl_order_detail] ([order_detail_id], [order_id], [menu_id], [qty], [price]) VALUES (3, N'2         ', 1, 1, 12000.0000)
INSERT [dbo].[tbl_order_detail] ([order_detail_id], [order_id], [menu_id], [qty], [price]) VALUES (4, N'2         ', 3, 2, 6000.0000)
INSERT [dbo].[tbl_order_detail] ([order_detail_id], [order_id], [menu_id], [qty], [price]) VALUES (5, N'1         ', 1, 1, 12000.0000)
SET IDENTITY_INSERT [dbo].[tbl_order_detail] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_order_status] ON 

INSERT [dbo].[tbl_order_status] ([order_status_id], [order_status_desc]) VALUES (1, N'Active')
INSERT [dbo].[tbl_order_status] ([order_status_id], [order_status_desc]) VALUES (2, N'Closed Bill')
INSERT [dbo].[tbl_order_status] ([order_status_id], [order_status_desc]) VALUES (103, N'Test')
SET IDENTITY_INSERT [dbo].[tbl_order_status] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_status_menu] ON 

INSERT [dbo].[tbl_status_menu] ([status_menu_id], [status_menu_desc]) VALUES (1, N'Ready')
INSERT [dbo].[tbl_status_menu] ([status_menu_id], [status_menu_desc]) VALUES (2, N'Out of Stock / Not Ready')
INSERT [dbo].[tbl_status_menu] ([status_menu_id], [status_menu_desc]) VALUES (3, N'Not Provided')
INSERT [dbo].[tbl_status_menu] ([status_menu_id], [status_menu_desc]) VALUES (4, N'Others')
SET IDENTITY_INSERT [dbo].[tbl_status_menu] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_user] ON 

INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (2, N'ruben851017', N'Ruben Sitepu', N'202cb962ac59075b964b07152d234b70', 1002, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiJiNmM0YzFjOS1iOTBkLTQxMzctOTg5Ny1mNzkyOTE3YTA2MTMiLCJpYXQiOiIxMi80LzIwMjIgMTI6NDY6MjMgUE0iLCJVc2VySWQiOiIyIiwiVXNlclR5cGUiOiIxMDAyIiwiZXhwIjoxNjcwMTcyMzgzLCJpc3MiOiJKV1RBdXRoZW50aWNhdGlvblNlcnZlciIsImF1ZCI6IkpXVFNlcnZpY2VBdWRpZW5jZSJ9.k14EpbJryc6MaQZCOx8zf1uiVy5BrMa6m7t8LORzALs', CAST(N'2022-12-04T16:46:23.000' AS DateTime), CAST(N'2022-12-01T23:12:17.620' AS DateTime), CAST(N'2022-12-04T19:46:23.927' AS DateTime))
INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (4, N'ruben171085', N'Ruben B', NULL, 2, NULL, NULL, CAST(N'2022-12-02T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (5, N'Test123', N'Test', NULL, 2, NULL, NULL, CAST(N'2022-12-02T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (1002, N'john123', N'John Doe', N'202cb962ac59075b964b07152d234b70', 2, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiIzZDI1MzVlMS1lMGJiLTQ1NTItYWJhMy0xMzg5NTAzNTgwODIiLCJpYXQiOiIxMi80LzIwMjIgOTo1Mjo1NCBBTSIsIlVzZXJJZCI6IjEwMDIiLCJVc2VyVHlwZSI6IjIiLCJleHAiOjE2NzAxNjE5NzQsImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2VydmVyIiwiYXVkIjoiSldUU2VydmljZUF1ZGllbmNlIn0.hkVBP_dgwRCjILJVFjlcwDlmAm9LDIp3_ZSVP2C6boM', CAST(N'2022-12-04T13:52:54.000' AS DateTime), CAST(N'2022-12-02T18:50:37.403' AS DateTime), CAST(N'2022-12-04T16:52:54.973' AS DateTime))
INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (1003, N'joko007', N'John Lennon', N'202cb962ac59075b964b07152d234b70', 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiI1YjI3MTkwOS0xMjM2LTRhNDMtOTc4NS1jNjYzOThlMmE2NzQiLCJpYXQiOiIxMi80LzIwMjIgOTo0OTo0MiBBTSIsIlVzZXJJZCI6IjEwMDMiLCJVc2VyVHlwZSI6IjEiLCJleHAiOjE2NzAxNjE3ODIsImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2VydmVyIiwiYXVkIjoiSldUU2VydmljZUF1ZGllbmNlIn0.Juf7aCZcetcmZuH8WYsVZymhQq3zvcL43F8pBpoVR44', CAST(N'2022-12-04T13:49:42.000' AS DateTime), CAST(N'2022-12-02T18:55:36.637' AS DateTime), CAST(N'2022-12-04T16:49:42.297' AS DateTime))
INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (1004, N'susi12', N'Administrator', N'202cb962ac59075b964b07152d234b70', 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiIxZjYyOGIzNi04OWFkLTRjNzgtOThlOC01NGZiNjFlZTNlZTYiLCJpYXQiOiIxMi80LzIwMjIgOTowNTo0OSBBTSIsIlVzZXJJZCI6IjEwMDQiLCJVc2VyVHlwZSI6IjEiLCJleHAiOjE2NzAxNTkxNDksImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2VydmVyIiwiYXVkIjoiSldUU2VydmljZUF1ZGllbmNlIn0.9oPEiBQU19-DDnIGRM9xlPd2rfd6EmnQOTNp_l1h53A', CAST(N'2022-12-04T13:05:49.000' AS DateTime), CAST(N'2022-12-02T18:57:56.720' AS DateTime), CAST(N'2022-12-04T16:05:49.563' AS DateTime))
INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (1005, N's123', N'Administrator', N'202cb962ac59075b964b07152d234b70', 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiIzYmUyOGI5Ni0yNmMwLTRlMzktYmYzYy0wYjNkNzNhOTAzMTYiLCJpYXQiOiIxMi8zLzIwMjIgNTo1NDo1OCBQTSIsIlVzZXJJZCI6IjEwMDUiLCJVc2VyVHlwZSI6IjEiLCJleHAiOjE2NzAxMDQ0OTgsImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2VydmVyIiwiYXVkIjoiSldUU2VydmljZUF1ZGllbmNlIn0.gHBGqGWUK3SPjLC2Oceb3KIl2E2iOoaxeLWGbmrqDzQ', CAST(N'2022-12-03T21:54:58.000' AS DateTime), CAST(N'2022-12-02T19:13:21.987' AS DateTime), CAST(N'2022-12-04T00:54:58.690' AS DateTime))
INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (1010, N's1234', N'Admin', N'297da0070bb35bab41c71c0b5fa84885', 2, NULL, NULL, CAST(N'2022-12-02T19:38:58.303' AS DateTime), NULL)
INSERT [dbo].[tbl_user] ([user_id], [user_name], [user_full_name], [password], [user_type], [token], [token_expired], [created_date], [last_login]) VALUES (1012, N'admin', N'Administrator', N'202cb962ac59075b964b07152d234b70', 1002, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiI3NjlhNzQ0Yy01OTk2LTQ2Y2EtODJlNS1lZGQ4ZmRlZmMyZDQiLCJpYXQiOiIxMi80LzIwMjIgOTo1Mzo1MCBBTSIsIlVzZXJJZCI6IjEwMTIiLCJVc2VyVHlwZSI6IjEwMDIiLCJleHAiOjE2NzAxNjIwMzAsImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2VydmVyIiwiYXVkIjoiSldUU2VydmljZUF1ZGllbmNlIn0.qEi14OMO4bXlxBxfScax5B-DozWBL2r_CWDayx235C0', CAST(N'2022-12-04T13:53:50.000' AS DateTime), CAST(N'2022-12-04T01:12:09.170' AS DateTime), CAST(N'2022-12-04T16:53:50.367' AS DateTime))
SET IDENTITY_INSERT [dbo].[tbl_user] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_user_type] ON 

INSERT [dbo].[tbl_user_type] ([user_type_id], [user_type_name], [created_date]) VALUES (1, N'Cashier', CAST(N'2022-12-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tbl_user_type] ([user_type_id], [user_type_name], [created_date]) VALUES (2, N'Waiters', CAST(N'2022-12-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tbl_user_type] ([user_type_id], [user_type_name], [created_date]) VALUES (1002, N'Admin', CAST(N'2022-12-02T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tbl_user_type] OFF
GO
USE [master]
GO
ALTER DATABASE [DBKreditPlus] SET  READ_WRITE 
GO
