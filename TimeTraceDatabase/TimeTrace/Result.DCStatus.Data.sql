
USE [TimeTrace]
GO

SET NOCOUNT ON

SET IDENTITY_INSERT [Application].[Result.DCStatus] ON

MERGE INTO [Application].[Result.DCStatus] AS [Target]
USING (VALUES
  (1,1,'2020-12-08T20:24:03.3066667',N'Pending',N'Result is in pending status')
 ,(2,1,'2020-12-08T20:28:40.5133333',N'Approved',N'Result is approved')
 ,(3,1,'2020-12-08T20:29:15.4133333',N'Declined',N'Result is declined')
) AS [Source] ([Id],[Active],[DateCreated],[Name],[Description])
ON ([Target].[Id] = [Source].[Id])
WHEN MATCHED AND (
	NULLIF([Source].[Active], [Target].[Active]) IS NOT NULL OR NULLIF([Target].[Active], [Source].[Active]) IS NOT NULL OR 
	NULLIF([Source].[DateCreated], [Target].[DateCreated]) IS NOT NULL OR NULLIF([Target].[DateCreated], [Source].[DateCreated]) IS NOT NULL OR 
	NULLIF([Source].[Name], [Target].[Name]) IS NOT NULL OR NULLIF([Target].[Name], [Source].[Name]) IS NOT NULL OR 
	NULLIF([Source].[Description], [Target].[Description]) IS NOT NULL OR NULLIF([Target].[Description], [Source].[Description]) IS NOT NULL) THEN
 UPDATE SET
  [Target].[Active] = [Source].[Active], 
  [Target].[DateCreated] = [Source].[DateCreated], 
  [Target].[Name] = [Source].[Name], 
  [Target].[Description] = [Source].[Description]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([Id],[Active],[DateCreated],[Name],[Description])
 VALUES([Source].[Id],[Source].[Active],[Source].[DateCreated],[Source].[Name],[Source].[Description])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Application].[Result.DCStatus]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Application].[Result.DCStatus] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO



SET IDENTITY_INSERT [Application].[Result.DCStatus] OFF
SET NOCOUNT OFF
GO