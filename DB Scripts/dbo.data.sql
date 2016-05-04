GO 
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'16298c3d-e5fa-45d8-99b4-6373f14ccf62', N'Admin', N'AppRole')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'ad73ad6d-6dd7-4d2b-9a34-46958b99eb6f', N'User', N'AppRole')

GO
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4bde1348-fc5d-414a-9c04-9fab17cec5e3', NULL, 0, N'ALAF6tu+skQrLK3sQqF1ExOuLjZVNh8P88WuMch7Z1zSxImnE+v295+icmdHvWvSwg==', N'72c5b13b-e448-453c-9b3c-e0b268c77225', NULL, 0, 0, NULL, 0, 0, N'User')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f', NULL, 0, N'AErxWQS/P+35S4h4MAWE35sBZT0ken1/nEn+ElWkxglD5zANNA6IXCz8uaabdcw02A==', N'1c5dfe23-facb-4c91-9be4-2b811edeec1c', NULL, 0, 0, NULL, 0, 0, N'Admin')

GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f', N'16298c3d-e5fa-45d8-99b4-6373f14ccf62')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4bde1348-fc5d-414a-9c04-9fab17cec5e3', N'ad73ad6d-6dd7-4d2b-9a34-46958b99eb6f')

GO
SET IDENTITY_INSERT [dbo].[Ingredients] ON
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (6, N'Potato', N'The potato', 5, 0.4, 16.3, 72.7, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (7, N'Carrot', N'The carrot', 1.3, 0.1, 6.9, 32, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (8, N'Cucumber', N'The cucumber', 0.8, 0.1, 3, 15.4, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (9, N'Apple', N'The apple', 0.4, 0.4, 9.8, 44, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (20, N'Egg', N'The han''s egg', 12.7, 11.5, 0.7, 157, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (22, N'Banana', N'The banana', 1.5, 0, 22.4, 91, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (23, N'Peach', N'The peach', 0.9, 0, 10.4, 44, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (25, N'Salad', N'The salad leaves', 1.5, 0, 2.2, 14, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (27, N'Mushroom', N'The porcino mushroom', 3.2, 0.7, 1.6, 25, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (28, N'Milk', N'The cow''s milk', 40, 39, 15, 30, NULL)
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (125, N'Apple', N'The apple', 0.4, 0.4, 9.8, 44, N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (126, N'Banana', N'The banana', 1.5, 0, 22.4, 91, N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (127, N'Peach', N'The peach', 0.9, 0, 10.4, 44, N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (128, N'Milk', N'The cow''s milk', 40, 39, 15, 30, N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (129, N'Potato', N'The potato', 5, 0.4, 16.3, 72.7, N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (130, N'Carrot', N'The carrot', 1.3, 0.1, 6.9, 32, N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
INSERT INTO [dbo].[Ingredients] ([ID], [Name], [Description], [Protein], [Fat], [Carbohydrates], [Caloricity], [OwnerID]) VALUES (131, N'Cucumber', N'The cucumber', 0.8, 0.1, 3, 15.4, N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
SET IDENTITY_INSERT [dbo].[Ingredients] OFF

GO
SET IDENTITY_INSERT [dbo].[Dishes] ON
INSERT INTO [dbo].[Dishes] ([ID], [Name], [Description], [OwnerID]) VALUES (12, N'Fruit Cake', N'Perfect fruit cake for a desert', NULL)
INSERT INTO [dbo].[Dishes] ([ID], [Name], [Description], [OwnerID]) VALUES (13, N'Summer salad', N'A great salad that consists of a variety of vegetables', NULL)
INSERT INTO [dbo].[Dishes] ([ID], [Name], [Description], [OwnerID]) VALUES (14, N'Milk Glass', N'Perfect breakfast for active day', NULL)
INSERT INTO [dbo].[Dishes] ([ID], [Name], [Description], [OwnerID]) VALUES (58, N'Fruit Cake', N'Perfect fruit cake for a desert', N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
INSERT INTO [dbo].[Dishes] ([ID], [Name], [Description], [OwnerID]) VALUES (59, N'Milk Glass', N'Perfect breakfast for active day', N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
INSERT INTO [dbo].[Dishes] ([ID], [Name], [Description], [OwnerID]) VALUES (60, N'Summer salad', N'A great salad that consists of a variety of vegetables', N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
SET IDENTITY_INSERT [dbo].[Dishes] OFF

GO
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (6, 13, 100)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (7, 13, 150)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (8, 13, 150)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (9, 12, 150)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (22, 12, 60)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (23, 12, 90)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (28, 14, 200)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (125, 58, 150)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (126, 58, 60)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (127, 58, 90)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (128, 59, 200)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (129, 60, 100)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (130, 60, 150)
INSERT INTO [dbo].[DishComponents] ([ID], [DishRefID], [Weight]) VALUES (131, 60, 150)

GO
SET IDENTITY_INSERT [dbo].[Courses] ON
INSERT INTO [dbo].[Courses] ([ID], [Description], [OwnerID]) VALUES (1, N'A course for loosing weight', NULL)
INSERT INTO [dbo].[Courses] ([ID], [Description], [OwnerID]) VALUES (4, N'Sample course', N'5fc31c2b-41f0-4bf7-8e5a-af286850e56f')
SET IDENTITY_INSERT [dbo].[Courses] OFF

GO
SET IDENTITY_INSERT [dbo].[Days] ON
INSERT INTO [dbo].[Days] ([ID], [Description], [Course_ID]) VALUES (1, N'Initial day', 1)
INSERT INTO [dbo].[Days] ([ID], [Description], [Course_ID]) VALUES (3, N'Day #1', 4)
INSERT INTO [dbo].[Days] ([ID], [Description], [Course_ID]) VALUES (4, N'Day #2', 4)
INSERT INTO [dbo].[Days] ([ID], [Description], [Course_ID]) VALUES (5, N'Day #3', 4)
INSERT INTO [dbo].[Days] ([ID], [Description], [Course_ID]) VALUES (6, N'Day #4', 4)
SET IDENTITY_INSERT [dbo].[Days] OFF

GO
SET IDENTITY_INSERT [dbo].[Meals] ON
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (1, N'Protein-rich breakfast', N'10:00:00', 1)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (3, N'Vegetable-rich dinner', N'13:30:00', 1)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (5, N'Vegetarian supper', N'18:00:00', 1)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (6, N'Meat-reach dinner', N'13:30:00', NULL)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (8, N'Carbohydrates-rich breakfast', N'09:00:00', NULL)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (11, N'Breakfast', N'09:00:00', 3)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (12, N'Dinner', N'13:00:00', 3)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (13, N'Breakfast', N'09:00:00', 4)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (14, N'Dinner', N'13:00:00', 4)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (15, N'Breakfast', N'09:00:00', 5)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (16, N'Dinner', N'13:00:00', 5)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (17, N'Breakfast', N'09:00:00', 6)
INSERT INTO [dbo].[Meals] ([ID], [Description], [Time], [Day_ID]) VALUES (18, N'Dinner', N'13:00:00', 6)
SET IDENTITY_INSERT [dbo].[Meals] OFF

GO
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (14, 1)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (13, 3)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (13, 5)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (14, 5)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (58, 11)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (59, 11)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (59, 12)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (60, 12)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (58, 13)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (59, 13)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (59, 14)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (60, 14)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (58, 15)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (59, 15)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (59, 16)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (60, 16)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (58, 17)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (59, 17)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (59, 18)
INSERT INTO [dbo].[DishMeals] ([Dish_ID], [Meal_ID]) VALUES (60, 18)

GO
INSERT INTO [dbo].[ActiveCourses] ([ID], [StartDate]) VALUES (4, N'2016-05-04 00:00:00')
