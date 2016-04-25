SET IDENTITY_INSERT [dbo].[Meals] ON
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (1, N'Protein-rich breakfast', N'10:00:00', 1)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (3, N'Vegetable-rich dinner', N'13:30:00', 1)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (5, N'Vegetarian supper', N'18:00:00', 1)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (6, N'Meat-reach dinner', N'13:30:00', NULL)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (8, N'Carbohydrates-rich breakfast', N'09:00:00', NULL)
SET IDENTITY_INSERT [dbo].[Meals] OFF
