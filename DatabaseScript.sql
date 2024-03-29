USE [master]
GO

/****** Object:  Database [SEKOLAHMINGGU]    Script Date: 03/29/2012 00:15:03 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'SEKOLAHMINGGU')
DROP DATABASE [SEKOLAHMINGGU]
GO

USE [master]
GO

/****** Object:  Database [SEKOLAHMINGGU]    Script Date: 03/29/2012 00:15:03 ******/
CREATE DATABASE [SEKOLAHMINGGU] ON  PRIMARY 
( NAME = N'SEKOLAHMINGGU', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\SEKOLAHMINGGU.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SEKOLAHMINGGU_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\SEKOLAHMINGGU_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [SEKOLAHMINGGU] SET COMPATIBILITY_LEVEL = 90
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SEKOLAHMINGGU].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [SEKOLAHMINGGU] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET ARITHABORT OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET  DISABLE_BROKER 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET  READ_WRITE 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET  MULTI_USER 
GO

ALTER DATABASE [SEKOLAHMINGGU] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [SEKOLAHMINGGU] SET DB_CHAINING OFF 
GO

