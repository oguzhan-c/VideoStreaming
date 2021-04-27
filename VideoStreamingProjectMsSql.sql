/*
** Copyright Microsoft, Inc. 1994 - 2000
** All Rights Reserved.
*/

SET NOCOUNT ON
GO

USE master
GO
if exists (select * from sysdatabases where name='VideoStream')
		drop database VideoStream
go

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE VideoStream
  ON PRIMARY (NAME = N''VideoStream'', FILENAME = N''' + @device_directory + N'videoStream.mdf'')
  LOG ON (NAME = N''VideoStream_log'',  FILENAME = N''' + @device_directory + N'videoStream.ldf'')')
go

if CAST(SERVERPROPERTY('ProductMajorVersion') AS INT)<12 
BEGIN
  exec sp_dboption 'VideoStream','trunc. log on chkpt.','true'
  exec sp_dboption 'VideoStream','select into/bulkcopy','true'
END
ELSE ALTER DATABASE VideoStream SET RECOVERY SIMPLE WITH NO_WAIT
GO

set quoted_identifier on
GO

/* Set DATEFORMAT so that the date strings are interpreted correctly regardless of
   the default DATEFORMAT on the server.
*/
SET DATEFORMAT mdy
GO
use "VideoStream"
--Yýkaridaki kodlar sayesinde eðer VideoStream isimli bir veritabaný varsa silindi yok ise oluþturuldu.


create table Users(
	Id int identity (1,1) not null,
	FirstName varchar(50) not null,
	LastName varchar(50) not null,
	Email varchar(50) not null,
	PasswordSalt varbinary(500) not null,
	PasswordHash varbinary(500) not null,
	Status bit not null,
	
	Constraint PrimarKey_Id_Users primary key (Id)
);

create table UserDetails(
	Id int identity (1,1) not null,
	UserId int not null ,
	Gender varchar(7) not null,
	IdentityNumber varchar(20) not null,
	DateOfBorn datetime not null,
	RecoveryEmail varchar(50) null,
	DateOfJoin datetime default current_Timestamp not null,--Backend de registerDTO su oluþturunca orada register olduðu zamaný direk olarak buna ata

	Constraint PrimaryKey_UserDetails_Id primary key (Id),
	Constraint ForeignKey_UserDetails_UserId foreign key (UserId)
	references [dbo].[Users](Id) on update cascade on delete cascade
);


create table Communications (
	Id int identity (1,1) not null,
	UserId int not null ,
	Street varchar(50)not null,
	City varchar(50) not null,
	Continent  varchar(50) not null,
	Country varchar(50) not null,
	Address1 varchar(100) not null,
	Address2 varchar(100) ,
	PhoneNumber varchar(15) not null,--baþýna + ülke kodu konularak
	ZipCode varchar(20) not null,

	Constraint PrimarKey_Id_Communications primary key (Id),
	Constraint ForeignKey_Users_UserId foreign key (UserId) 
		references [dbo].[Users](Id) on update cascade on delete cascade
);


create table ProfilePicture(
	Id int identity(1,1) not null,
	UserId int not null,
	PicturePath text not null,
	Date datetime default current_Timestamp not null,--datetime.now yapýlacak

	Constraint PrimaryKey_ProfilePicture_Id primary key(Id),
	Constraint ForeignKey_ProfilePicture_UserId foreign key(UserId)
		references [dbo].[Users](Id),

);


--Burada Tüm yetkiler belirtilecek
create table OperationClaims(--yetki operasyonlarý
	Id int identity(1,1) not null,
	Name varchar(500),
	Date datetime default current_Timestamp not null,--datetime.now yapýlacak
	ClaimType varchar(25) default 'Default' not null,  


	Constraint PrimaryKey_Id_OperationClaims primary key (Id),
)
--Burada User Id yardýmý ile daha sonra backend de kim hangi yetkiyi almýþ o saðlanacak.
create table UserOperationClaims(
	Id int not null identity(1,1),
	UserId int not null,
	OperationClaimId int not null,
	Date datetime default current_Timestamp not null,--datetime.now yapýlacak

	Constraint PrimaryKey_UserOperationClaimId_UserOperationClaims primary key (Id),
	Constraint ForeignKey_UserId_UserOperationClaims foreign key(UserId)
	references [dbo].Users(Id) on update cascade on delete cascade,
	Constraint ForeignKey_OperationClaimId_UserOperationClaims foreign key(OperationClaimId)
	references [dbo].[OperationClaims](Id) on update cascade on delete cascade
);


create table Channels
(
	Id int identity(1,1) not null,
	UserId int not null,
	ChannelName varchar(25) not null,--Backend de Kullanýcý ismi kanal ismi olacak þekilde kurgula.
	InstallationDate datetime not null,--Backend de users e atanan dateofjoin deki bilgiyi buraya ata.
	UpdateDate datetime null,
	ChannelPhotoPath text not null,
	Description text  not null,


	Constraint PrimaryKey_Channels_Id primary key(Id) ,
	Constraint ForeignKey_Channels_UserId foreign key(UserId)
		references [dbo].[Users](Id)
); 

create table Videos(
	Id int identity(1,1) not null,
	UserId int not null,
	ChannelId int not null,
	Description text not null,
	Views int not null,--görüntülenme sayýsý
	Duration int not null,-- Þimdilik datetime sonra belki string olabilir.
	VideoPath text not null,
	ThumbnailPath text not null,
	Date datetime not null,
	UpdateDate datetime null,

	Constraint PrimaryKey_Videos_Id primary key(Id),
	Constraint ForeignKey_Videos_UserId foreign key(UserId)
		references [dbo].[Users](Id),
	Constraint ForeignKey_Videos_ChannelId foreign key(ChannelId)
		references [dbo].[Channels](Id),
);

Create table Subscribers (
	Id int identity(1,1) NOT NULL,
	UserId int not null,
	ChannelId int not null,
  	Date datetime default current_Timestamp not null,--datetime.now yapýlacak

	Constraint PrimaryKey_Subscribers_Id primary key(Id),
	Constraint ForeignKey_Subscribers_UserId foreign key (UserId)
		references [dbo].[Users](Id),
	Constraint ForeignKey_Subscribers_ChannelId foreign key (ChannelId)
		references [dbo].[Channels](Id)
);



Create table Dislikes(
	Id int identity(1,1) not null,
	UserId int not null,
	VideoId int not null,

	Constraint PrimaryKey_Dislakes_Id primary key(Id),
	Constraint ForeignKey_Dislakes_UserId foreign key(UserId)
		references [dbo].[Users](Id),
	Constraint ForeignKey_Dislakes_VideoId foreign key(VideoId)
		references [dbo].[Videos](Id)
);

Create table Likes(
	Id int identity(1,1) not null,
	UserId int not null,
	VideoId int not null, 
	

	Constraint PrimaryKey_Likes_Id primary key(Id),
	Constraint ForeignKey_Likes_VideoId Foreign key(VideoId)
		references[dbo].[Videos](Id),
	Constraint ForeignKey_Likes_UserId foreign key(UserId)
		references [dbo].[Users](Id)
);

Create table Comments(
	Id int identity(1,1) not null,
	PostedByUserId int not null,
	VideoId int not  null,
	ResponseByUserId int not null,
	LikeId int default 0 not null,
	DislikeId int default 0 not null,
	CommentBody text not null,
	Date datetime default current_Timestamp not null,

	Constraint PrimaryKey_Comments_Id primary key(Id),
	Constraint ForeignKey_Comments_PostedByUserId foreign key(PostedByUserId)
		references [dbo].[Users](Id),
	Constraint ForeignKey_Comments_VideoId foreign key(VideoId)
		references [dbo].[Videos](Id),
	Constraint ForeignKey_Comments_ResponseByUserId foreign key(ResponseByUserId)
		references [dbo].[Users](Id),
	Constraint ForeignKey_Comments_DislakeId foreign key(DislikeId)
		references [dbo].[Dislikes](Id),
	Constraint ForeignKey_Comments_LikId foreign key(LikeId)
		references [dbo].[Likes](Id)
);

create table JasonWebTokens
(
        Id int identity(1,1) not null, 
		UserId int not null,
        Token text not null,
        Expiration datetime not null, 

		Constraint PrimaryKey_JasonWebTokens_Id primary key(Id),
		Constraint ForeignKey_JasonWebTokens_UserId foreign key(UserId)
			references [dbo].[Users](Id)
);
