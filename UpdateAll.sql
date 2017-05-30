USE [PTR]

IF OBJECT_ID(N'[dbo].[FK_ADDriveADFolder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ADFolders] DROP CONSTRAINT [FK_ADDriveADFolder];
GO
IF OBJECT_ID(N'[dbo].[FK_ADFolderAccessADFolderAccess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ADFolderAccesses] DROP CONSTRAINT [FK_ADFolderAccessADFolderAccess];
GO
IF OBJECT_ID(N'[dbo].[FK_ADFolderADFolderAccess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ADFolderAccesses] DROP CONSTRAINT [FK_ADFolderADFolderAccess];
GO
IF OBJECT_ID(N'[dbo].[FK_ADParentFolderADFolder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ADFolders] DROP CONSTRAINT [FK_ADParentFolderADFolder];
GO
IF OBJECT_ID(N'[dbo].[FK_ADUserADFolderAccess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ADFolderAccesses] DROP CONSTRAINT [FK_ADUserADFolderAccess];
GO
IF OBJECT_ID(N'[dbo].[FK_FunctionAccessFunction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IFunctionAccesses] DROP CONSTRAINT [FK_FunctionAccessFunction];
GO
IF OBJECT_ID(N'[dbo].[FK_FunctionAccessQuestAccess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IQuestAccesses] DROP CONSTRAINT [FK_FunctionAccessQuestAccess];
GO
IF OBJECT_ID(N'[dbo].[FK_ISDAVS_UserFunctionAccess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IFunctionAccesses] DROP CONSTRAINT [FK_ISDAVS_UserFunctionAccess];
GO
IF OBJECT_ID(N'[dbo].[FK_PV_EmploeePV_HistEmploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVHistEmploees] DROP CONSTRAINT [FK_PV_EmploeePV_HistEmploee];
GO
IF OBJECT_ID(N'[dbo].[FK_PV_ProfessionPV_Emploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVEmploees] DROP CONSTRAINT [FK_PV_ProfessionPV_Emploee];
GO
IF OBJECT_ID(N'[dbo].[FK_PV_ProfessionPV_HistEmploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVHistEmploees] DROP CONSTRAINT [FK_PV_ProfessionPV_HistEmploee];
GO
IF OBJECT_ID(N'[dbo].[FK_PV_StructuralPV_Emploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVEmploees] DROP CONSTRAINT [FK_PV_StructuralPV_Emploee];
GO
IF OBJECT_ID(N'[dbo].[FK_PV_StructuralPV_HistEmploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVHistEmploees] DROP CONSTRAINT [FK_PV_StructuralPV_HistEmploee];
GO
IF OBJECT_ID(N'[dbo].[FK_PVStructuralPVEmploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVEmploees] DROP CONSTRAINT [FK_PVStructuralPVEmploee];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestAccessQuest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IQuestAccesses] DROP CONSTRAINT [FK_QuestAccessQuest];
GO
IF OBJECT_ID(N'[dbo].[FK_UUserADUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ADUsers] DROP CONSTRAINT [FK_UUserADUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UUserISDAVSUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISDAVSUsers] DROP CONSTRAINT [FK_UUserISDAVSUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UUserPVEmploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVEmploees] DROP CONSTRAINT [FK_UUserPVEmploee];
GO


/*
	DELETE FROM [PTR].[dbo].PVEmploees
	DELETE FROM [PTR].[dbo].PVHistEmploees
	DELETE FROM [PTR].[dbo].IFunctionAccesses
	DELETE FROM [PTR].[dbo].IFunctions
	DELETE FROM [PTR].[dbo].IQuestAccesses
	DELETE FROM [PTR].[dbo].IQuests
	DELETE FROM [PTR].[dbo].ISDAVSUsers

	UPDATE [PTR].[dbo].ADUsers
	SET UUserId = NULL
	FROM [PTR].[dbo].ADUsers

	DELETE FROM [PTR].[dbo].UUsers
	DELETE FROM [PTR].[dbo].[ImportReports]
*/
/*
	DELETE FROM [PTR].[dbo].ADFolderAccesses
	DELETE FROM [PTR].[dbo].ADFolders
	DELETE FROM [PTR].[dbo].ADDrives
	DELETE FROM [PTR].[dbo].ADUsers

	
	DELETE FROM [PTR].[dbo].PVStructurals
*/

	-----------------------------------------------------------
	Print(              '---PVStructurals---'					)
	-----------------------------------------------------------

	Print('Prep Query')

	DECLARE @q1 TABLE (IsNew bit, 
			[i_id] varchar(50), [i_code] varchar(50), [i_name] varchar(50),
			[i_level] varchar(50), [i_headDeptCode] varchar(50),
			[_id] varchar(50), [_code] varchar(50), [_name] varchar(50), 
			[_level] varchar(50), [_headDeptCode] varchar(50))

	INSERT INTO @q1
	SELECT		CASE WHEN SI.Id = S.Id THEN 0 ELSE 1 END AS IsNew,
				SI.Id, SI.Code, SI.Name, SI.[Level], SI.HeadDeptCode,
				S.Id, S.Code, S.Name, S.[Level], S.HeadDepartmentId
	FROM		[PTR].[dbo].[PVStructurals] AS S RIGHT OUTER JOIN
				[PTR].[dbo].[PVStructural_import] AS SI ON S.Id = SI.Id OR S.Id IS NULL
	WHERE		S.Id IS NULL OR S.Code <> SI.Code OR S.Name <> SI.Name OR
				S.HeadDepartmentId <> SI.HeadDeptCode OR S.HeadDepartmentId IS NULL

	--SELECT * FROM @q1

	If 1 = 1
	Begin
		Print('Report')
	
		INSERT INTO [PTR].[dbo].ImportReports (NewRecord,[Table],RecordId, Hide)
		SELECT IsNew, 'PVStructurals', 1, 0
		FROM @q1
	End
	

	Print('Update')

	UPDATE		PVStructurals
	SET			Id = i_id, Code = i_code, Name = i_name, [Level] = i_level, HeadDepartmentId = i_headDeptCode
	FROM		PVStructurals AS S INNER JOIN @q1 ON _id = S.Id

	Print('Insert')

	INSERT INTO	PVStructurals
				(Id, Code, Name, [Level], HeadDepartmentId)
	SELECT		i_id, i_code, i_name, i_level, i_headDeptCode
	FROM		@q1
	WHERE		IsNew = 1

	Print('Add that Flippin 1 record')

	BEGIN
	   IF NOT EXISTS (SELECT * FROM PVStructurals 
					   WHERE ID = '1')
	   BEGIN
			INSERT INTO	PVStructurals
						(Id, Code)
			Values ('1', '0000')
	   END
	END
	
	-----------------------------------------------------------
	Print(              '---PVEmploees---'						)
	-----------------------------------------------------------

	PRINT 'Prep Query'
	DECLARE @q2 TABLE (IsNew int, 
	[i_id] int, [i_name] varchar(50), [i_surname] varchar(50), [i_nfullname] nvarchar(MAX), [i_professionId] int,
	[i_partId] int, [i_departmentId] int, [i_status] int,
	[_id] int,[_name] varchar(50),[_surname] varchar(50),[_professionId] int,
	[_partId] int,[_departmentId] int,[_status] int)

	INSERT INTO @q2
	SELECT     CASE WHEN EI.Id = E.Id THEN 0 ELSE 1 END AS IsNew, 
				EI.Id, replace(EI.Name, ' ', ''), replace(EI.Surname, ' ', ''),
				dbo.LatinToASCII(LOWER(replace(EI.Name, ' ', '') + ' ' + replace(EI.Surname, ' ', ''))),
				EI.ProfessionId, EI.PartId, EI.DepartmentId, EI.[Status],
				E.Id, E.Name, E.Surname, E.ProfessionId,
				E.PartId, E.DepartmentId, E.[Status]
	FROM        [PTR].[dbo].PVEmploees AS E RIGHT OUTER JOIN
				[PTR].[dbo].PVEmploee_import AS EI ON EI.Id = E.Id OR E.Id IS NULL
	WHERE       E.Id IS NULL OR EI.Id <> E.Id OR EI.Name <> E.Name OR EI.Surname <> E.Surname OR
				EI.ProfessionId <> E.ProfessionId OR E.PartId <> EI.PartId OR
				E.DepartmentId <> EI.DepartmentId OR E.[Status] <> EI.[Status]
	                      
	INSERT INTO @q2 (IsNew, _id, _name, _surname, _professionId, _partId, _departmentId, _status)
	SELECT      - 1 AS IsNew, E.Id, E.Name, E.Surname, E.ProfessionId, E.PartId, E.DepartmentId, E.Status
	FROM        [PTR].[dbo].PVEmploees AS E FULL OUTER JOIN
				[PTR].[dbo].PVEmploee_import AS EI ON EI.Id = E.Id OR E.Id IS NULL
	WHERE       EI.Id IS NULL

	--select * from @q2
		
	If 1 = 1
	Begin
		Print('Report')

		INSERT INTO [PTR].[dbo].[ImportReports] (NewRecord,[Table],RecordId, Hide)
		SELECT      IsNew, 'PVEmploees', CASE WHEN IsNew<0 THEN _id ELSE i_id END, 0
		FROM        @q2
	End

	Print('Generate Departments and Professions')

	INSERT INTO [PTR].[dbo].PVStructurals (Id, Code, Name, [Level])
	SELECT      Deps.Id, '' AS Code, '' AS Name, 0 AS [Level]
	FROM         (SELECT DISTINCT DepartmentId AS Id FROM [PTR].[dbo].PVEmploee_import) AS Deps LEFT OUTER JOIN
				  [PTR].[dbo].PVStructurals ON Deps.Id = PVStructurals.Id
	WHERE       PVStructurals.Id IS NULL AND PVStructurals.Code <> 0
	
	SET IDENTITY_INSERT PVProfessions ON
	INSERT INTO [PTR].[dbo].PVProfessions (Id, Name)
	SELECT      Profs.Id, '' AS Name
	FROM         (SELECT DISTINCT ProfessionId AS Id FROM [PTR].[dbo].PVEmploee_import) AS Profs LEFT OUTER JOIN
				  [PTR].[dbo].PVProfessions ON Profs.Id = PVProfessions.Id
	WHERE       PVProfessions.Id IS NULL AND Profs.Id IS NOT NULL

	Print('Update')
	Print('Add to History first')

	INSERT INTO [PTR].[dbo].PVHistEmploees
						  (Name, Surname, [From], [To], Email, Phone, LocalPhone, Status, DepartmentId, PartId, ProfessionId, ChangeDate, EmploeeID)
	SELECT      E.Name, E.Surname, E.[From], E.[To], E.Email, E.Phone, E.LocalPhone, E.Status, E.DepartmentId, E.PartId, E.ProfessionId, GETDATE() AS Date, E.Id
	FROM        [PTR].[dbo].PVEmploees AS E INNER JOIN @q2 ON _id = E.Id
	WHERE       IsNew = 0

	UPDATE      [PTR].[dbo].PVEmploees
	SET         ID = i_id, Name = i_name, Surname = i_surname, NormalizedFullName = i_nfullname, ProfessionId = i_professionId,
				PartId = i_partId, DepartmentId = i_departmentId, [Status] = i_status
	FROM        [PTR].[dbo].PVEmploees AS E INNER JOIN @q2 ON _id = E.Id 
	WHERE       IsNew = 0

	Print('Register Deleted')
	Print('Add to History first')

	INSERT INTO [PTR].[dbo].PVHistEmploees
				(Name, Surname, [From], [To], Email, Phone, LocalPhone, Status, DepartmentId, PartId, ProfessionId, ChangeDate, EmploeeID)
	SELECT      E.Name, E.Surname, E.[From], E.[To], E.Email, E.Phone, E.LocalPhone, E.Status, E.DepartmentId, E.PartId, E.ProfessionId, GETDATE() AS Date, E.Id
	FROM        [PTR].[dbo].PVEmploees AS E INNER JOIN @q2 ON _id = E.Id
	WHERE       IsNew = -1

	UPDATE      [PTR].[dbo].PVEmploees
	SET         [To] = GETDATE()
	FROM        [PTR].[dbo].PVEmploees AS E INNER JOIN @q2 ON _id = E.Id
	WHERE       IsNew = -1

	Print('Insert Newly Created')

	INSERT INTO	[PTR].[dbo].PVEmploees (Id, Name, Surname, NormalizedFullName, ProfessionId, PartId, DepartmentId, [Status], [From])
	SELECT		i_id, i_name, i_surname, i_nfullname,
				i_professionId, i_partId, i_departmentId, i_status, GETDATE()
	FROM		@q2
	Where		IsNew = 1 --and i_professionId <> null

	Print('Insert/Update UUsers')

	INSERT INTO	UUsers (Name, Surname, NormalizedFullName, Hide)
	SELECT		i_name, i_surname, i_nfullname, 0
	FROM		@q2 LEFT OUTER JOIN UUsers ON i_nfullname = UUsers.NormalizedFullName
	Where		IsNew = 1 AND UUsers.Id IS NULL
	
	Update		PVEmploees
	SET			UUserId = UUsers.Id
	From		UUsers LEFT OUTER JOIN PVEmploees ON UUsers.NormalizedFullName = PVEmploees.NormalizedFullName

	Update		UUsers
	SET			UUsers.Name = PVEmploees.Name, UUsers.Surname = PVEmploees.Surname
	From		UUsers INNER JOIN PVEmploees ON UUsers.NormalizedFullName = PVEmploees.NormalizedFullName
	
	-----------------------------------------------------------
	Print(              '---ADUsers---'							)
	-----------------------------------------------------------

	--Connect to existing ADUsers
	UPDATE		ADUsers
	SET			UUserId = UUsers.Id
	FROM		UUsers INNER JOIN
				ADUsers ON UUsers.NormalizedFullName = ADUsers.DN			
	-----------------------------------------------------------
	Print(              '---ISDAVSUsers---'						)
	-----------------------------------------------------------

	Print('Prep Query')

	DECLARE @q3 TABLE (IsNew bit, u_id int, u_login varchar(30), u_name varchar(30),
					  u_surname varchar(50), u_nfullname nvarchar (MAX), u_phone varchar(20), u_email varchar(300), u_hid int,
					  _id int, _login varchar(30), _name varchar(30), _surname varchar(50),
					  _phone varchar(20), _email varchar(300))

	INSERT INTO @q3
	SELECT		CASE WHEN UI.user_id = U.Id THEN 0 ELSE 1 END AS IsNew, 
				UI.user_id, UI.user_login_name, replace(UI.user_name, ' ', ''), Case When UI.user_surname Is Null Then '' Else  replace(UI.user_surname, ' ', '') End, 
				dbo.LatinToASCII(LOWER(replace(UI.user_name, ' ', '') + ' ' + replace(UI.user_surname, ' ', ''))),
				UI.user_phone, UI.user_email, UI.user_horizon_id, 
				U.Id, U.[Login], U.Name, U.Surname, 
				U.Phone, U.Email
	FROM		[PTR].[dbo].ISDAVSUsers AS U RIGHT OUTER JOIN
				[PTR].[dbo].ISDAVSUser_import AS UI ON U.Id = UI.user_id OR U.Id IS NULL
	WHERE		U.Id is null or (U.[Login] <> UI.user_login_name OR U.Name <> UI.user_name OR U.Surname <> UI.user_surname OR
				U.Phone <> UI.user_phone OR U.Email <> UI.user_email)
	
	If 1 = 0
	Begin
		Print('Report')

		INSERT INTO [PTR].[dbo].ImportReports (NewRecord,[Table],RecordId, Hide)
		SELECT IsNew, 'ISDAVSUsers', u_id, 0
		FROM @q3
	End

	SELECT * FROM @q3

	Print('Update')

	Update [PTR].[dbo].ISDAVSUsers
	SET         Id = u_id, [Login] = u_login, Name = u_name, Surname = u_surname,
				NormalizedFullName = u_nfullname, Phone = u_phone, Email = u_email
	FROM [PTR].[dbo].ISDAVSUsers AS IU INNER JOIN @q3 ON _id = IU.Id

	Print('Insert')

	INSERT INTO	[PTR].[dbo].ISDAVSUsers (Id, [Login], Name, Surname, NormalizedFullName, Phone, Email)
	SELECT		u_id, u_login, u_name, u_surname, u_nfullname, u_phone, u_email
	FROM		@q3
	Where		IsNew = 1
	
	Print('Insert/Update UUsers')

	INSERT INTO	UUsers (Name, Surname, NormalizedFullName, Email, Hide)
	SELECT		ISDAVSUsers.Name, ISDAVSUsers.Surname, ISDAVSUsers.NormalizedFullName, ISDAVSUsers.Email, 0
	FROM		ISDAVSUsers LEFT OUTER JOIN UUsers ON ISDAVSUsers.NormalizedFullName = UUsers.NormalizedFullName
	WHERE       (UUsers.Id IS NULL)
	--!!!
	UPDATE		ISDAVSUsers
	SET			UUserId = UUsers.Id
	FROM		@q3 INNER JOIN
				ISDAVSUsers ON u_id = Id INNER JOIN
				UUsers ON UUsers.NormalizedFullName = ISDAVSUsers.NormalizedFullName
	WHERE		IsNew = 1

	
	-----------------------------------------------------------
	Print(              '---IQuests---'							)
	-----------------------------------------------------------

	--Prep Query
	DECLARE @q4 TABLE (IsNew bit, imp_Id int, imp_Name varchar(80), imp_Ind varchar(30), _Id int, _Name varchar(80), _Ind varchar(30))

	INSERT INTO @q4
	SELECT Case When IQuest_import.quest_id = IQuests.Id Then 0 Else 1 End As IsNew,
		   IQuest_import.quest_id AS imp_Id, IQuest_import.name AS imp_Name, IQuest_import.q_index AS imp_Ind,
		   IQuests.Id AS _Id, IQuests.Name AS _Name, IQuests.[Index] AS _Ind
	FROM   IQuest_import LEFT OUTER JOIN
		   IQuests ON IQuest_import.quest_id = IQuests.Id OR IQuests.Id IS NULL
	WHERE  IQuest_import.name <> IQuests.Name OR IQuest_import.q_index <> IQuests.[Index] OR IQuests.Id IS NULL

	--SELECT * FROM @q
	
	If 1 = 1
	Begin
		Print('Report')

		INSERT INTO ImportReports (NewRecord,[Table],RecordId,Hide)
		SELECT IsNew, 'IQuests', imp_Id, 0
		FROM @q4
	End

	--Update
	Update IQuests
	SET IQuests.Name = imp_Name,IQuests.[Index] = imp_Ind
	FROM IQuests INNER JOIN @q4 ON _Id = IQuests.Id

	--Insert
	INSERT INTO IQuests
						  (Id, Name, [Index])
	SELECT     imp_Id, imp_Name, imp_Ind
	FROM       @q4
	Where      IsNew = 1
	
	-----------------------------------------------------------
	Print(              '---IFunctions---'						)
	-----------------------------------------------------------

    Print('Prep Query')
	DECLARE @q5 TABLE (IsNew bit, i_id int, i_type bit, i_name varchar(100), i_descr varchar(250), _id int, _type bit, _name varchar(100), _descr varchar(250))

	INSERT INTO @q5
	SELECT Case When FI.Id = F.Id Then 0 Else 1 End As IsNew,
		   FI.Id, FI.[Type], FI.Name, FI.[Description], F.Id, F.[Type], F.Name, F.[Description]
	FROM   [PTR].[dbo].[IFunction_import] AS FI LEFT OUTER JOIN
		   [PTR].[dbo].[IFunctions] AS F ON FI.Id = F.Id OR F.Id IS NULL
	WHERE  F.[Type] <> FI.[Type] OR F.Name <> FI.Name OR F.[Description] <> FI.[Description] OR F.Id IS NULL
	
	If 1 = 1
	Begin
		Print('Report')
		INSERT INTO [PTR].[dbo].ImportReports (NewRecord,[Table],RecordId,Hide)
		SELECT IsNew, 'IFunctions', i_id, 0
		FROM @q5
	End

	Print('Update')
	UPDATE		F
	SET			Id = i_id, [Type] = i_type, Name = i_name, [Description] = i_descr
	FROM		[PTR].[dbo].IFunctions AS F INNER JOIN @q5 ON _id = F.Id

	Print('Insert')
	INSERT INTO	[PTR].[dbo].IFunctions
				(Id, [Type], Name, [Description])
	SELECT		i_id, i_type, i_name, i_descr
	FROM		@q5
	WHERE		IsNew = 1
	
	-----------------------------------------------------------
	Print(              '---IFunctionAccesses---'				)
	-----------------------------------------------------------

	--Prep Query
	DECLARE @q6 TABLE (IsNew bit,
			[i_id] int, [i_userId] int, [i_functionId] int,
			[_id] int, [_userId] int, [_functionId] int)

	INSERT INTO @q6
	SELECT		CASE WHEN FI.Id = F.Id THEN 0 ELSE 1 END AS IsNew,
				FI.Id, FI.UserId, FI.FunctionId,
				F.Id, F.UserId, F.FunctionId
	FROM		[PTR].[dbo].[IFunctionAccesses] AS F RIGHT OUTER JOIN
				[PTR].[dbo].[IFunctionAccess_import] AS FI ON F.Id = FI.Id OR F.Id IS NULL
	WHERE		F.Id IS NULL OR F.UserId <> FI.UserId OR F.FunctionId <> FI.FunctionId

	INSERT INTO @q6 (IsNew, _id, _userId, _functionId)
	SELECT      - 1 AS IsNew, F.Id, F.UserId, F.FunctionId
	FROM        [PTR].[dbo].[IFunctionAccesses] AS F FULL OUTER JOIN
				[PTR].[dbo].[IFunctionAccess_import] AS FI ON FI.Id = F.Id
	WHERE       FI.Id IS NULL

	SELECT * FROM @q6

	--Update
	UPDATE		F
	SET			Id = i_id, UserId = i_userId, FunctionId = _functionId
	FROM		[PTR].[dbo].[IFunctionAccesses] AS F INNER JOIN @q6 ON _id = F.Id



	--Insert
	INSERT INTO	[PTR].[dbo].[IFunctionAccesses]
				(Id, UserId, FunctionId, [From])
	SELECT		i_id, i_userId, i_functionId, GETDATE()
	FROM		@q6
	WHERE		IsNew = 1
	
	-----------------------------------------------------------
	Print(              '---IQuestAccesses---'					)
	-----------------------------------------------------------

	--Prep Query
	DECLARE @q7 TABLE (IsNew bit,
			i_id int, i_functionAccessId int, i_questId int,
			_id int, _functionAccessId int, _questId int)

	INSERT INTO @q7
	SELECT		CASE WHEN AI.Id = A.Id THEN 0 ELSE 1 END AS IsNew,
				AI.Id, AI.FunctionAccessId, AI.QuestId,
				A.Id, A.FunctionAccessId, A.QuestId
	FROM		[PTR].[dbo].IQuestAccesses AS A RIGHT OUTER JOIN
				[PTR].[dbo].IQuestAccess_import AS AI ON A.Id = AI.Id OR A.Id IS NULL
	WHERE		A.Id IS NULL OR A.FunctionAccessId <> AI.FunctionAccessId OR A.QuestId <> AI.QuestId

	INSERT INTO @q7 (IsNew, _id, _functionAccessId, _questId)
	SELECT      - 1 AS IsNew, A.Id, A.FunctionAccessId, A.QuestId
	FROM		[PTR].[dbo].IQuestAccesses AS A RIGHT OUTER JOIN
				[PTR].[dbo].IQuestAccess_import AS AI ON A.Id = AI.Id
	WHERE       AI.Id IS NULL

	--SELECT * FROM @q7

	--Update
	UPDATE		[PTR].[dbo].IQuestAccesses
	SET			Id = i_id, FunctionAccessId = i_functionAccessId, QuestId = i_questId
	FROM		[PTR].[dbo].IQuestAccesses AS A INNER JOIN @q7 ON _id = A.Id

	--Register Deleted
	UPDATE      [PTR].[dbo].IQuestAccesses
	SET         [To] = GETDATE()
	FROM        [PTR].[dbo].IQuestAccesses AS A INNER JOIN @q7 ON _id = A.Id
	WHERE       IsNew = -1

	--Insert
	INSERT INTO	[PTR].[dbo].IQuestAccesses
				(Id, FunctionAccessId, QuestId, [From])
	SELECT		i_id, i_functionAccessId, i_questId, GETDATE()
	FROM		@q7
	WHERE		IsNew = 1
	
	-----------------------------------------------------------
	Print(              '---ADUsers---'					)
	-----------------------------------------------------------
	--Just fixing if something happens
	UPDATE       ADUsers
	SET                UUserId = UUsers.Id
	FROM            ADUsers INNER JOIN
							 UUsers ON ADUsers.DN = UUsers.NormalizedFullName
	WHERE			ADUsers.UUserId IS NULL

	-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DriveId] in table 'ADFolders'
ALTER TABLE [dbo].[ADFolders]
ADD CONSTRAINT [FK_ADDriveADFolder]
    FOREIGN KEY ([DriveId])
    REFERENCES [dbo].[ADDrives]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ParentFolderAccessId] in table 'ADFolderAccesses'
ALTER TABLE [dbo].[ADFolderAccesses]
ADD CONSTRAINT [FK_ADFolderAccessADFolderAccess]
    FOREIGN KEY ([ParentFolderAccessId])
    REFERENCES [dbo].[ADFolderAccesses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FolderID] in table 'ADFolderAccesses'
ALTER TABLE [dbo].[ADFolderAccesses]
ADD CONSTRAINT [FK_ADFolderADFolderAccess]
    FOREIGN KEY ([FolderID])
    REFERENCES [dbo].[ADFolders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'ADFolderAccesses'
ALTER TABLE [dbo].[ADFolderAccesses]
ADD CONSTRAINT [FK_ADUserADFolderAccess]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[ADUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ParentFolderId] in table 'ADFolders'
ALTER TABLE [dbo].[ADFolders]
ADD CONSTRAINT [FK_ADParentFolderADFolder]
    FOREIGN KEY ([ParentFolderId])
    REFERENCES [dbo].[ADFolders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UUserId] in table 'ADUsers'
ALTER TABLE [dbo].[ADUsers]
ADD CONSTRAINT [FK_UUserADUser]
    FOREIGN KEY ([UUserId])
    REFERENCES [dbo].[UUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FunctionId] in table 'IFunctionAccesses'
ALTER TABLE [dbo].[IFunctionAccesses]
ADD CONSTRAINT [FK_FunctionAccessFunction]
    FOREIGN KEY ([FunctionId])
    REFERENCES [dbo].[IFunctions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FunctionAccessId] in table 'IQuestAccesses'
ALTER TABLE [dbo].[IQuestAccesses]
ADD CONSTRAINT [FK_FunctionAccessQuestAccess]
    FOREIGN KEY ([FunctionAccessId])
    REFERENCES [dbo].[IFunctionAccesses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserID] in table 'IFunctionAccesses'
ALTER TABLE [dbo].[IFunctionAccesses]
ADD CONSTRAINT [FK_ISDAVS_UserFunctionAccess]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[ISDAVSUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [QuestId] in table 'IQuestAccesses'
ALTER TABLE [dbo].[IQuestAccesses]
ADD CONSTRAINT [FK_QuestAccessQuest]
    FOREIGN KEY ([QuestId])
    REFERENCES [dbo].[IQuests]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UUserId] in table 'ISDAVSUsers'
ALTER TABLE [dbo].[ISDAVSUsers]
ADD CONSTRAINT [FK_UUserISDAVSUser]
    FOREIGN KEY ([UUserId])
    REFERENCES [dbo].[UUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EmploeeID] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [FK_PV_EmploeePV_HistEmploee]
    FOREIGN KEY ([EmploeeID])
    REFERENCES [dbo].[PVEmploees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProfessionId] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [FK_PV_ProfessionPV_Emploee]
    FOREIGN KEY ([ProfessionId])
    REFERENCES [dbo].[PVProfessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DepartmentId] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [FK_PV_StructuralPV_Emploee]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UUserId] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [FK_UUserPVEmploee]
    FOREIGN KEY ([UUserId])
    REFERENCES [dbo].[UUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProfessionId] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [FK_PV_ProfessionPV_HistEmploee]
    FOREIGN KEY ([ProfessionId])
    REFERENCES [dbo].[PVProfessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DepartmentId] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [FK_PV_StructuralPV_HistEmploee]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PartId] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [FK_PVStructuralPVEmploee]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PartId] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [FK_PVHistEmploeePVStructural]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [HeadDepartmentId] in table 'PVStructurals'
ALTER TABLE [dbo].[PVStructurals]
ADD CONSTRAINT [FK_PVStructuralPVStructural]
    FOREIGN KEY ([HeadDepartmentId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

