
CREATE TABLE [dbo].[BusinessCards](
	[BusinessCardId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessCard_Name] [nvarchar](100) NOT NULL,
	[BusinessCard_Email] [nvarchar](100) NOT NULL,
	[BusinessCard_PhoneNumber] [nvarchar](max) NOT NULL,
	[BusinessCard_Address] [nvarchar](200) NOT NULL,
	[BusinessCard_Gender] [nvarchar](10) NOT NULL,
	[BusinessCard_Date_Of_Birth] [nvarchar](max) NOT NULL,
	[Photo] [nvarchar](max) NOT NULL,
)

