USE [master]
GO

/****** Object:  Database [Prakta]    Script Date: 13.06.2024 0:53:22 ******/
CREATE DATABASE [Prakta]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Prakta', FILENAME = N'C:\Users\jeffp\Apps\SSMS\MSSQL16.SQLEXPRESS\MSSQL\DATA\Prakta.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Prakta_log', FILENAME = N'C:\Users\jeffp\Apps\SSMS\MSSQL16.SQLEXPRESS\MSSQL\DATA\Prakta_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Prakta].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Prakta] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Prakta] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Prakta] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Prakta] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Prakta] SET ARITHABORT OFF 
GO

ALTER DATABASE [Prakta] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Prakta] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Prakta] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Prakta] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Prakta] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Prakta] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Prakta] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Prakta] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Prakta] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Prakta] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Prakta] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Prakta] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Prakta] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Prakta] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Prakta] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Prakta] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Prakta] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Prakta] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Prakta] SET  MULTI_USER 
GO

ALTER DATABASE [Prakta] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Prakta] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Prakta] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Prakta] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Prakta] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Prakta] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [Prakta] SET QUERY_STORE = ON
GO

ALTER DATABASE [Prakta] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [Prakta] SET  READ_WRITE 
GO

