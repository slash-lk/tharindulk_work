CREATE TABLE [dbo].[Customers] (
    [CustId]    INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NULL,
    [LastName]  NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([CustId] ASC)
);


CREATE TABLE [dbo].[Inventory] (
    [CarId]   INT           IDENTITY (1, 1) NOT NULL,
    [Make]    NVARCHAR (50) NULL,
    [Color]   NVARCHAR (50) NULL,
    [PetName] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([CarId] ASC)
);


CREATE TABLE [dbo].[Orders] (
    [OrderId] INT IDENTITY (1, 1) NOT NULL,
    [CustId]  INT NOT NULL,
    [CarId]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Orders_ToCustomers] FOREIGN KEY ([CustId]) REFERENCES [dbo].[Customers] ([CustId]),
    CONSTRAINT [FK_Orders_ToInventory] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Inventory] ([CarId])
);


SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT INTO [dbo].[Customers] ([CustId], [FirstName], [LastName]) VALUES (1, N'Dave', N'Brenner')
INSERT INTO [dbo].[Customers] ([CustId], [FirstName], [LastName]) VALUES (2, N'Matt', N'Walton')
INSERT INTO [dbo].[Customers] ([CustId], [FirstName], [LastName]) VALUES (3, N'Steve', N'Hagen')
INSERT INTO [dbo].[Customers] ([CustId], [FirstName], [LastName]) VALUES (4, N'Pat', N'Walton')
INSERT INTO [dbo].[Customers] ([CustId], [FirstName], [LastName]) VALUES (5, N'Homer', N'Simpson')
SET IDENTITY_INSERT [dbo].[Customers] OFF

SET IDENTITY_INSERT [dbo].[Inventory] ON
INSERT INTO [dbo].[Inventory] ([CarId], [Make], [Color], [PetName]) VALUES (1, N'VW', N'Black', N'Zippy')
INSERT INTO [dbo].[Inventory] ([CarId], [Make], [Color], [PetName]) VALUES (2, N'Ford', N'Rust', N'Rusty')
INSERT INTO [dbo].[Inventory] ([CarId], [Make], [Color], [PetName]) VALUES (3, N'Saab', N'Black', N'Mel')
INSERT INTO [dbo].[Inventory] ([CarId], [Make], [Color], [PetName]) VALUES (4, N'Yugo', N'Yellow', N'Clunker')
INSERT INTO [dbo].[Inventory] ([CarId], [Make], [Color], [PetName]) VALUES (5, N'BMW', N'Black', N'Bimmer')
INSERT INTO [dbo].[Inventory] ([CarId], [Make], [Color], [PetName]) VALUES (6, N'BMW', N'Green', N'Hank')
INSERT INTO [dbo].[Inventory] ([CarId], [Make], [Color], [PetName]) VALUES (7, N'BMW', N'Pink', N'Pinky')
SET IDENTITY_INSERT [dbo].[Inventory] OFF

SET IDENTITY_INSERT [dbo].[Orders] ON
INSERT INTO [dbo].[Orders] ([OrderId], [CustId], [CarId]) VALUES (1, 1, 5)
INSERT INTO [dbo].[Orders] ([OrderId], [CustId], [CarId]) VALUES (2, 2, 1)
INSERT INTO [dbo].[Orders] ([OrderId], [CustId], [CarId]) VALUES (3, 3, 4)
INSERT INTO [dbo].[Orders] ([OrderId], [CustId], [CarId]) VALUES (4, 4, 7)
SET IDENTITY_INSERT [dbo].[Orders] OFF

CREATE TABLE [dbo].[CreditRisks] (
    [CustId]    INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CustId] ASC)
);
