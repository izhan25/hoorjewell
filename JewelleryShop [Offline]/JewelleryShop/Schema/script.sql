USE [JewelleryShop]
GO
/****** Object:  StoredProcedure [dbo].[GetOrders]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOrders]
(
    @orderId int
)

AS
BEGIN
    select * from OrderView where orderId = @orderId
END;
GO
/****** Object:  Table [dbo].[EmployeeTable]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeeTable](
	[Employee_Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeUserName] [varchar](50) NULL,
	[EmployeePwd] [varchar](50) NULL,
	[EmployeeImage] [varchar](50) NULL,
	[EmployeeContact] [varchar](50) NULL,
	[roleId] [int] NULL,
 CONSTRAINT [PK_EmployeeTable] PRIMARY KEY CLUSTERED 
(
	[Employee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ordered_Products]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ordered_Products](
	[OP_Id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [int] NULL,
	[productId] [int] NULL,
	[Qty] [int] NULL,
	[price] [int] NULL,
 CONSTRAINT [PK_Ordered_Products] PRIMARY KEY CLUSTERED 
(
	[OP_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrdersTable]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrdersTable](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[placementDate] [date] NULL,
	[deliveryData] [date] NULL,
	[isDelievered] [varchar](50) NULL,
 CONSTRAINT [PK_OrdersTable] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductCategoryTable]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductCategoryTable](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [varchar](50) NULL,
 CONSTRAINT [PK_ProductCategoryTable] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductsTable]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductsTable](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [varchar](50) NULL,
	[productDescription] [varchar](5000) NULL,
	[productPrice] [int] NULL,
	[categoryId] [int] NULL,
	[productImage] [varchar](50) NULL,
 CONSTRAINT [PK_ProductsTable] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RolesTable]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RolesTable](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [varchar](50) NULL,
	[status] [varchar](50) NULL,
 CONSTRAINT [PK_RolesTable] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsersTable]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsersTable](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NULL,
	[userEmail] [varchar](50) NULL,
	[userContact] [varchar](50) NULL,
	[userPassword] [varchar](50) NULL,
	[userAddress] [varchar](50) NULL,
 CONSTRAINT [PK_UsersTable] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[OrderView]    Script Date: 9/26/2018 9:47:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[OrderView]
AS
SELECT        dbo.OrdersTable.orderId, dbo.UsersTable.userName, dbo.UsersTable.userContact, dbo.UsersTable.userAddress, dbo.ProductsTable.productName, 
                         dbo.Ordered_Products.Qty, dbo.Ordered_Products.price, dbo.OrdersTable.placementDate, dbo.OrdersTable.deliveryData, dbo.OrdersTable.isDelievered
FROM            dbo.Ordered_Products INNER JOIN
                         dbo.OrdersTable ON dbo.Ordered_Products.orderId = dbo.OrdersTable.orderId INNER JOIN
                         dbo.ProductsTable ON dbo.Ordered_Products.productId = dbo.ProductsTable.productId INNER JOIN
                         dbo.UsersTable ON dbo.OrdersTable.userId = dbo.UsersTable.userId

GO
SET IDENTITY_INSERT [dbo].[EmployeeTable] ON 

INSERT [dbo].[EmployeeTable] ([Employee_Id], [EmployeeUserName], [EmployeePwd], [EmployeeImage], [EmployeeContact], [roleId]) VALUES (2, N'admin', N'admin', N'admin.png', NULL, 1)
INSERT [dbo].[EmployeeTable] ([Employee_Id], [EmployeeUserName], [EmployeePwd], [EmployeeImage], [EmployeeContact], [roleId]) VALUES (5, N'Jameel', N'pass', N'download.png', N'03145214964', 2)
INSERT [dbo].[EmployeeTable] ([Employee_Id], [EmployeeUserName], [EmployeePwd], [EmployeeImage], [EmployeeContact], [roleId]) VALUES (6, N'Kanwal', N'pass', N'163832 (1).png', N'03245698712', 3)
SET IDENTITY_INSERT [dbo].[EmployeeTable] OFF
SET IDENTITY_INSERT [dbo].[OrdersTable] ON 

INSERT [dbo].[OrdersTable] ([orderId], [userId], [placementDate], [deliveryData], [isDelievered]) VALUES (45, 3, CAST(0xC23E0B00 AS Date), CAST(0xC53E0B00 AS Date), N'true')
SET IDENTITY_INSERT [dbo].[OrdersTable] OFF
SET IDENTITY_INSERT [dbo].[ProductCategoryTable] ON 

INSERT [dbo].[ProductCategoryTable] ([categoryId], [categoryName]) VALUES (1, N'Necklace')
INSERT [dbo].[ProductCategoryTable] ([categoryId], [categoryName]) VALUES (2, N'Ring')
INSERT [dbo].[ProductCategoryTable] ([categoryId], [categoryName]) VALUES (3, N'Bangles')
INSERT [dbo].[ProductCategoryTable] ([categoryId], [categoryName]) VALUES (6, N'Bracelets')
INSERT [dbo].[ProductCategoryTable] ([categoryId], [categoryName]) VALUES (14, N'Earings')
SET IDENTITY_INSERT [dbo].[ProductCategoryTable] OFF
SET IDENTITY_INSERT [dbo].[ProductsTable] ON 

INSERT [dbo].[ProductsTable] ([productId], [productName], [productDescription], [productPrice], [categoryId], [productImage]) VALUES (12, N'Marvellous Gold Plated Pearl Bangle For Women', N'This Sukkhi Marvellous Gold Plated Pearl Bangle For Women is made of Alloy. Women love jewellery; specially traditional jewellery adore a women. They wear it on different occasion. They have special importance on ring ceremony, wedding and festive time. They can also wear it on regular basics. Make your moment memorable with this range. This jewel set features a unique one of a kind traditional embellish with antic finish.', 900, 3, N'bangles.jpg')
INSERT [dbo].[ProductsTable] ([productId], [productName], [productDescription], [productPrice], [categoryId], [productImage]) VALUES (13, N'Shiny Bracelets Chian', N'Brand Name:BlucomeItem Type:BraceletsFine or Fashion:FashionShape\pattern:PlantClasp Type:LobsterMaterial:CrystalLength:20cmSetting Type:Channel SettingMetals Type:Zinc AlloyBracelets Type:Chain & Link BraceletsStyle:VintageGender:WomenChain Type:Beaded BraceletFunction:bracelet jewelryCompatibility:gift jewelrycolor::Green red blue crystal antique gold platedSafety Standard::Nickel free, lead free, CEKeywords::Sapphire Antique Gold Large Blue Turco Banglesis_customized::YesOccasion :Wedding Engagement Anniversary BirthdayStatus :Brand New', 1200, 6, N'bracelet1.jpg')
INSERT [dbo].[ProductsTable] ([productId], [productName], [productDescription], [productPrice], [categoryId], [productImage]) VALUES (14, N'22k Gold Effect Kundan Necklace', N'Regal South Indian inspired medium sized neckace set in 22k gold plate with mild antique & multi coloured; set with laac kundan & semi-precious stones stones. Set includes: kundan necklace & jhumki earrings. Dimensions (at widest points): Earring - 1.5cm wide, 5.5cm long. Necklace - 3.5cm at centre. Buy this head-turing South Indian inspired kundan set before another lady does! The quality of this Khazana Range set truly emulates true 22k jewellery, even down to the engraved matar mala balls at the end of the chain.', 18800, 1, N'necklace1.jpg')
INSERT [dbo].[ProductsTable] ([productId], [productName], [productDescription], [productPrice], [categoryId], [productImage]) VALUES (15, N'Classic Elements Silicone Ring', N'Our Elements Silicone Rings are redefining silicone rings. Each ring is infused with precious materials, blurring the line between traditional rings and silicone rings. Our copper rings are infused with real copper, our gold and rose gold rings are infused with real gold, our silver rings with real silver, and our black pearl rings with real pearl. Available in Classic width (this one) and thin.', 1200, 2, N'ring1.jpg')
INSERT [dbo].[ProductsTable] ([productId], [productName], [productDescription], [productPrice], [categoryId], [productImage]) VALUES (16, N'Thin Elements Rings', N'The thinner, sleeker version of our Elements Collection. Our Elements Silicone Rings are redefining silicone rings. Each ring is infused with precious materials, blurring the line between traditional rings and silicone rings. Our copper rings are infused with real copper, our gold and rose gold rings are infused with real gold, our silver rings with real silver, and our black pearl rings with real pearl. Available in classic width, and thin (this one)', 2500, 2, N'ring2.jpg')
INSERT [dbo].[ProductsTable] ([productId], [productName], [productDescription], [productPrice], [categoryId], [productImage]) VALUES (17, N'BlueGold Ethiopian Opal and Diamond ', N'14K Yellow Gold Ethiopian Opal and Diamond Milgrain-Accented Ring Bedazzled by the fire? Is there a better way to celebrate a special occasion or merely gift yourself? Here''s a stunning ring that will turn heads. Go for it. You deserve it! ', 5500, 2, N'ring3.jpg')
SET IDENTITY_INSERT [dbo].[ProductsTable] OFF
SET IDENTITY_INSERT [dbo].[RolesTable] ON 

INSERT [dbo].[RolesTable] ([roleId], [roleName], [status]) VALUES (1, N'Admin', N'active')
INSERT [dbo].[RolesTable] ([roleId], [roleName], [status]) VALUES (2, N'Marketing', N'active')
INSERT [dbo].[RolesTable] ([roleId], [roleName], [status]) VALUES (3, N'Accounts', N'disable')
INSERT [dbo].[RolesTable] ([roleId], [roleName], [status]) VALUES (4, N'Manager', N'active')
INSERT [dbo].[RolesTable] ([roleId], [roleName], [status]) VALUES (6, N'Editor', N'disable')
SET IDENTITY_INSERT [dbo].[RolesTable] OFF
SET IDENTITY_INSERT [dbo].[UsersTable] ON 

INSERT [dbo].[UsersTable] ([userId], [userName], [userEmail], [userContact], [userPassword], [userAddress]) VALUES (3, N'Umer', N'umer@email.com', N'03152230485', N'pass', N'lahore')
INSERT [dbo].[UsersTable] ([userId], [userName], [userEmail], [userContact], [userPassword], [userAddress]) VALUES (4, N'Ghazanfer', N'ghazanfer@email.com', N'03331321548', N'pass', N'karachi')
INSERT [dbo].[UsersTable] ([userId], [userName], [userEmail], [userContact], [userPassword], [userAddress]) VALUES (5, N'Omer', N'omer@email.com', N'03214578963', N'pass', N'quetta')
INSERT [dbo].[UsersTable] ([userId], [userName], [userEmail], [userContact], [userPassword], [userAddress]) VALUES (6, N'Parvez', N'pervez@mail.com', N'03214578963', N'pass', N'karachi')
INSERT [dbo].[UsersTable] ([userId], [userName], [userEmail], [userContact], [userPassword], [userAddress]) VALUES (7, N'Kamal', N'kamal@gmail.com', N'03142233665', N'pass', N'quetta')
INSERT [dbo].[UsersTable] ([userId], [userName], [userEmail], [userContact], [userPassword], [userAddress]) VALUES (8, N'Junaid', N'junaid@gmail.com', N'03214578963', N'pass', N'karachi')
INSERT [dbo].[UsersTable] ([userId], [userName], [userEmail], [userContact], [userPassword], [userAddress]) VALUES (9, N'Faraz', N'faraz@gmail.com', N'03549874563', N'pass', N'Islamabad')
INSERT [dbo].[UsersTable] ([userId], [userName], [userEmail], [userContact], [userPassword], [userAddress]) VALUES (10, N'Noor', N'noor@gmail.com', N'03214578963', N'pass', N'Multan')
SET IDENTITY_INSERT [dbo].[UsersTable] OFF
ALTER TABLE [dbo].[Ordered_Products] ADD  CONSTRAINT [DF_Ordered_Products_productId]  DEFAULT ((0)) FOR [productId]
GO
ALTER TABLE [dbo].[EmployeeTable]  WITH CHECK ADD  CONSTRAINT [FK_roleId_RolesTable] FOREIGN KEY([roleId])
REFERENCES [dbo].[RolesTable] ([roleId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[EmployeeTable] CHECK CONSTRAINT [FK_roleId_RolesTable]
GO
ALTER TABLE [dbo].[Ordered_Products]  WITH CHECK ADD  CONSTRAINT [FK_orderId_OrdersTable] FOREIGN KEY([orderId])
REFERENCES [dbo].[OrdersTable] ([orderId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Ordered_Products] CHECK CONSTRAINT [FK_orderId_OrdersTable]
GO
ALTER TABLE [dbo].[Ordered_Products]  WITH CHECK ADD  CONSTRAINT [FK_productId_ProductsTable] FOREIGN KEY([productId])
REFERENCES [dbo].[ProductsTable] ([productId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Ordered_Products] CHECK CONSTRAINT [FK_productId_ProductsTable]
GO
ALTER TABLE [dbo].[OrdersTable]  WITH CHECK ADD  CONSTRAINT [FK_userId_OrdersTable] FOREIGN KEY([userId])
REFERENCES [dbo].[UsersTable] ([userId])
GO
ALTER TABLE [dbo].[OrdersTable] CHECK CONSTRAINT [FK_userId_OrdersTable]
GO
ALTER TABLE [dbo].[ProductsTable]  WITH CHECK ADD  CONSTRAINT [FK_categoryId_ProductsTable] FOREIGN KEY([categoryId])
REFERENCES [dbo].[ProductCategoryTable] ([categoryId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[ProductsTable] CHECK CONSTRAINT [FK_categoryId_ProductsTable]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[56] 4[35] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Ordered_Products"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 175
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "OrdersTable"
            Begin Extent = 
               Top = 9
               Left = 334
               Bottom = 201
               Right = 500
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductsTable"
            Begin Extent = 
               Top = 187
               Left = 145
               Bottom = 378
               Right = 315
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UsersTable"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 201
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OrderView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OrderView'
GO
