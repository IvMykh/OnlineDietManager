SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([ID], [Name], [Login], [Password], [IsAdmin]) VALUES (1, N'John Doe', N'johndoe', N'secret', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
