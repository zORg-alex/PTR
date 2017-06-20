
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/06/2017 16:44:12
-- Generated from EDMX file: C:\Users\AUljanovs\Documents\GitHub\PTR\SharedLib\PTREDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ptr];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

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
IF OBJECT_ID(N'[dbo].[FK_DepartmentPVHistEmploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVHistEmploees] DROP CONSTRAINT [FK_DepartmentPVHistEmploee];
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
IF OBJECT_ID(N'[dbo].[FK_PartPVHistEmploee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVHistEmploees] DROP CONSTRAINT [FK_PartPVHistEmploee];
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
IF OBJECT_ID(N'[dbo].[FK_PVEmploeeDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVEmploees] DROP CONSTRAINT [FK_PVEmploeeDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_PVEmploeePart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVEmploees] DROP CONSTRAINT [FK_PVEmploeePart];
GO
IF OBJECT_ID(N'[dbo].[FK_PVStructuralPVStructural]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PVStructurals] DROP CONSTRAINT [FK_PVStructuralPVStructural];
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

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ADDrives]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ADDrives];
GO
IF OBJECT_ID(N'[dbo].[ADFolderAccesses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ADFolderAccesses];
GO
IF OBJECT_ID(N'[dbo].[ADFolders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ADFolders];
GO
IF OBJECT_ID(N'[dbo].[ADUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ADUsers];
GO
IF OBJECT_ID(N'[dbo].[IFunction_import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IFunction_import];
GO
IF OBJECT_ID(N'[dbo].[IFunctionAccess_import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IFunctionAccess_import];
GO
IF OBJECT_ID(N'[dbo].[IFunctionAccesses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IFunctionAccesses];
GO
IF OBJECT_ID(N'[dbo].[IFunctions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IFunctions];
GO
IF OBJECT_ID(N'[dbo].[ImportReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImportReports];
GO
IF OBJECT_ID(N'[dbo].[IQuest_import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IQuest_import];
GO
IF OBJECT_ID(N'[dbo].[IQuestAccess_import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IQuestAccess_import];
GO
IF OBJECT_ID(N'[dbo].[IQuestAccesses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IQuestAccesses];
GO
IF OBJECT_ID(N'[dbo].[IQuests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IQuests];
GO
IF OBJECT_ID(N'[dbo].[ISDAVSUser_import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISDAVSUser_import];
GO
IF OBJECT_ID(N'[dbo].[ISDAVSUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISDAVSUsers];
GO
IF OBJECT_ID(N'[dbo].[PVEmploee_import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PVEmploee_import];
GO
IF OBJECT_ID(N'[dbo].[PVEmploees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PVEmploees];
GO
IF OBJECT_ID(N'[dbo].[PVHistEmploees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PVHistEmploees];
GO
IF OBJECT_ID(N'[dbo].[PVProfessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PVProfessions];
GO
IF OBJECT_ID(N'[dbo].[PVStructural_import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PVStructural_import];
GO
IF OBJECT_ID(N'[dbo].[PVStructurals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PVStructurals];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[UUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ADDrives'
CREATE TABLE [dbo].[ADDrives] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Path] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- Creating table 'ADFolderAccesses'
CREATE TABLE [dbo].[ADFolderAccesses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [FolderID] int  NOT NULL,
    [FileSystemRights_] int  NULL,
    [From] datetime  NULL,
    [To] datetime  NULL,
    [ParentFolderAccessId] int  NULL
);
GO

-- Creating table 'ADFolders'
CREATE TABLE [dbo].[ADFolders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParentFolderId] int  NULL,
    [DriveId] int  NOT NULL,
    [FullPath] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SubHash] nvarchar(max)  NOT NULL,
    [IsRootFolder] bit  NOT NULL
);
GO

-- Creating table 'ADUsers'
CREATE TABLE [dbo].[ADUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [DN] nvarchar(max)  NOT NULL,
    [UUserId] int  NULL
);
GO

-- Creating table 'IFunction_import'
CREATE TABLE [dbo].[IFunction_import] (
    [Id] int  NOT NULL,
    [Type] bit  NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [Description] varchar(250)  NULL
);
GO

-- Creating table 'IFunctionAccess_import'
CREATE TABLE [dbo].[IFunctionAccess_import] (
    [Id] int  NOT NULL,
    [UserId] int  NOT NULL,
    [FunctionId] int  NOT NULL
);
GO

-- Creating table 'IFunctionAccesses'
CREATE TABLE [dbo].[IFunctionAccesses] (
    [Id] int  NOT NULL,
    [UserID] int  NOT NULL,
    [FunctionId] int  NOT NULL,
    [From] datetime  NULL,
    [To] datetime  NULL
);
GO

-- Creating table 'IFunctions'
CREATE TABLE [dbo].[IFunctions] (
    [Id] int  NOT NULL,
    [Type] bit  NOT NULL,
    [Name] nchar(100)  NOT NULL,
    [Description] nchar(250)  NULL
);
GO

-- Creating table 'ImportReports'
CREATE TABLE [dbo].[ImportReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NewRecord] int  NOT NULL,
    [Table] nvarchar(max)  NOT NULL,
    [RecordId] int  NOT NULL,
    [Hide] int  NOT NULL
);
GO

-- Creating table 'IQuest_import'
CREATE TABLE [dbo].[IQuest_import] (
    [quest_id] int  NOT NULL,
    [name] varchar(80)  NOT NULL,
    [name_eng] varchar(80)  NULL,
    [q_index] varchar(30)  NOT NULL
);
GO

-- Creating table 'IQuestAccess_import'
CREATE TABLE [dbo].[IQuestAccess_import] (
    [Id] int  NOT NULL,
    [FunctionAccessId] int  NOT NULL,
    [QuestId] int  NOT NULL
);
GO

-- Creating table 'IQuestAccesses'
CREATE TABLE [dbo].[IQuestAccesses] (
    [Id] int  NOT NULL,
    [FunctionAccessId] int  NOT NULL,
    [QuestId] int  NOT NULL,
    [From] datetime  NULL,
    [To] datetime  NULL
);
GO

-- Creating table 'IQuests'
CREATE TABLE [dbo].[IQuests] (
    [Id] int  NOT NULL,
    [Name] varchar(max)  NULL,
    [Index] varchar(max)  NOT NULL
);
GO

-- Creating table 'ISDAVSUser_import'
CREATE TABLE [dbo].[ISDAVSUser_import] (
    [user_id] int  NOT NULL,
    [user_login_name] varchar(30)  NOT NULL,
    [user_surname] varchar(50)  NULL,
    [user_phone] varchar(20)  NULL,
    [user_email] varchar(300)  NULL,
    [user_horizon_id] int  NULL,
    [user_name] varchar(30)  NOT NULL
);
GO

-- Creating table 'ISDAVSUsers'
CREATE TABLE [dbo].[ISDAVSUsers] (
    [Id] int  NOT NULL,
    [Login] nchar(30)  NOT NULL,
    [Name] nchar(30)  NOT NULL,
    [Surname] nchar(50)  NULL,
    [Phone] nchar(20)  NULL,
    [Email] nchar(300)  NULL,
    [UUserId] int  NULL,
    [NormalizedFullName] nvarchar(max)  NULL
);
GO

-- Creating table 'PVEmploee_import'
CREATE TABLE [dbo].[PVEmploee_import] (
    [Id] int  NOT NULL,
    [Name] varchar(50)  NULL,
    [Surname] varchar(50)  NULL,
    [ProfessionId] int  NULL,
    [PartId] nchar(50)  NULL,
    [DepartmentId] nchar(50)  NULL,
    [Status] int  NULL
);
GO

-- Creating table 'PVEmploees'
CREATE TABLE [dbo].[PVEmploees] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [NormalizedFullName] nvarchar(max)  NULL,
    [From] datetime  NULL,
    [To] datetime  NULL,
    [Email] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NULL,
    [LocalPhone] nvarchar(max)  NULL,
    [Status] bit  NOT NULL,
    [DepartmentId] varchar(50)  NULL,
    [PartId] varchar(50)  NULL,
    [ProfessionId] int  NULL,
    [UUserId] int  NULL
);
GO

-- Creating table 'PVHistEmploees'
CREATE TABLE [dbo].[PVHistEmploees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [From] datetime  NULL,
    [To] datetime  NULL,
    [Email] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NULL,
    [LocalPhone] nvarchar(max)  NULL,
    [Status] bit  NOT NULL,
    [DepartmentId] varchar(50)  NULL,
    [PartId] varchar(50)  NULL,
    [ProfessionId] int  NULL,
    [ChangeDate] datetime  NOT NULL,
    [EmploeeID] int  NOT NULL
);
GO

-- Creating table 'PVProfessions'
CREATE TABLE [dbo].[PVProfessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(250)  NOT NULL
);
GO

-- Creating table 'PVStructural_import'
CREATE TABLE [dbo].[PVStructural_import] (
    [Id] nchar(50)  NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Name] varchar(50)  NULL,
    [Level] varchar(50)  NULL,
    [HeadDeptCode] varchar(50)  NULL
);
GO

-- Creating table 'PVStructurals'
CREATE TABLE [dbo].[PVStructurals] (
    [Id] varchar(50)  NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Name] varchar(50)  NULL,
    [Level] varchar(50)  NULL,
    [HeadDepartmentId] varchar(50)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'UUsers'
CREATE TABLE [dbo].[UUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NULL,
    [hide] bit  NOT NULL,
    [NormalizedFullName] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ADDrives'
ALTER TABLE [dbo].[ADDrives]
ADD CONSTRAINT [PK_ADDrives]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ADFolderAccesses'
ALTER TABLE [dbo].[ADFolderAccesses]
ADD CONSTRAINT [PK_ADFolderAccesses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ADFolders'
ALTER TABLE [dbo].[ADFolders]
ADD CONSTRAINT [PK_ADFolders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ADUsers'
ALTER TABLE [dbo].[ADUsers]
ADD CONSTRAINT [PK_ADUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Type], [Name] in table 'IFunction_import'
ALTER TABLE [dbo].[IFunction_import]
ADD CONSTRAINT [PK_IFunction_import]
    PRIMARY KEY CLUSTERED ([Id], [Type], [Name] ASC);
GO

-- Creating primary key on [Id], [UserId], [FunctionId] in table 'IFunctionAccess_import'
ALTER TABLE [dbo].[IFunctionAccess_import]
ADD CONSTRAINT [PK_IFunctionAccess_import]
    PRIMARY KEY CLUSTERED ([Id], [UserId], [FunctionId] ASC);
GO

-- Creating primary key on [Id] in table 'IFunctionAccesses'
ALTER TABLE [dbo].[IFunctionAccesses]
ADD CONSTRAINT [PK_IFunctionAccesses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IFunctions'
ALTER TABLE [dbo].[IFunctions]
ADD CONSTRAINT [PK_IFunctions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ImportReports'
ALTER TABLE [dbo].[ImportReports]
ADD CONSTRAINT [PK_ImportReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [quest_id] in table 'IQuest_import'
ALTER TABLE [dbo].[IQuest_import]
ADD CONSTRAINT [PK_IQuest_import]
    PRIMARY KEY CLUSTERED ([quest_id] ASC);
GO

-- Creating primary key on [Id], [FunctionAccessId], [QuestId] in table 'IQuestAccess_import'
ALTER TABLE [dbo].[IQuestAccess_import]
ADD CONSTRAINT [PK_IQuestAccess_import]
    PRIMARY KEY CLUSTERED ([Id], [FunctionAccessId], [QuestId] ASC);
GO

-- Creating primary key on [Id] in table 'IQuestAccesses'
ALTER TABLE [dbo].[IQuestAccesses]
ADD CONSTRAINT [PK_IQuestAccesses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IQuests'
ALTER TABLE [dbo].[IQuests]
ADD CONSTRAINT [PK_IQuests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [user_id], [user_login_name], [user_name] in table 'ISDAVSUser_import'
ALTER TABLE [dbo].[ISDAVSUser_import]
ADD CONSTRAINT [PK_ISDAVSUser_import]
    PRIMARY KEY CLUSTERED ([user_id], [user_login_name], [user_name] ASC);
GO

-- Creating primary key on [Id] in table 'ISDAVSUsers'
ALTER TABLE [dbo].[ISDAVSUsers]
ADD CONSTRAINT [PK_ISDAVSUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PVEmploee_import'
ALTER TABLE [dbo].[PVEmploee_import]
ADD CONSTRAINT [PK_PVEmploee_import]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [PK_PVEmploees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [PK_PVHistEmploees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PVProfessions'
ALTER TABLE [dbo].[PVProfessions]
ADD CONSTRAINT [PK_PVProfessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Code] in table 'PVStructural_import'
ALTER TABLE [dbo].[PVStructural_import]
ADD CONSTRAINT [PK_PVStructural_import]
    PRIMARY KEY CLUSTERED ([Id], [Code] ASC);
GO

-- Creating primary key on [Id] in table 'PVStructurals'
ALTER TABLE [dbo].[PVStructurals]
ADD CONSTRAINT [PK_PVStructurals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'UUsers'
ALTER TABLE [dbo].[UUsers]
ADD CONSTRAINT [PK_UUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

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

-- Creating non-clustered index for FOREIGN KEY 'FK_ADDriveADFolder'
CREATE INDEX [IX_FK_ADDriveADFolder]
ON [dbo].[ADFolders]
    ([DriveId]);
GO

-- Creating foreign key on [ParentFolderAccessId] in table 'ADFolderAccesses'
ALTER TABLE [dbo].[ADFolderAccesses]
ADD CONSTRAINT [FK_ADFolderAccessADFolderAccess]
    FOREIGN KEY ([ParentFolderAccessId])
    REFERENCES [dbo].[ADFolderAccesses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ADFolderAccessADFolderAccess'
CREATE INDEX [IX_FK_ADFolderAccessADFolderAccess]
ON [dbo].[ADFolderAccesses]
    ([ParentFolderAccessId]);
GO

-- Creating foreign key on [FolderID] in table 'ADFolderAccesses'
ALTER TABLE [dbo].[ADFolderAccesses]
ADD CONSTRAINT [FK_ADFolderADFolderAccess]
    FOREIGN KEY ([FolderID])
    REFERENCES [dbo].[ADFolders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ADFolderADFolderAccess'
CREATE INDEX [IX_FK_ADFolderADFolderAccess]
ON [dbo].[ADFolderAccesses]
    ([FolderID]);
GO

-- Creating foreign key on [UserId] in table 'ADFolderAccesses'
ALTER TABLE [dbo].[ADFolderAccesses]
ADD CONSTRAINT [FK_ADUserADFolderAccess]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[ADUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ADUserADFolderAccess'
CREATE INDEX [IX_FK_ADUserADFolderAccess]
ON [dbo].[ADFolderAccesses]
    ([UserId]);
GO

-- Creating foreign key on [ParentFolderId] in table 'ADFolders'
ALTER TABLE [dbo].[ADFolders]
ADD CONSTRAINT [FK_ADParentFolderADFolder]
    FOREIGN KEY ([ParentFolderId])
    REFERENCES [dbo].[ADFolders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ADParentFolderADFolder'
CREATE INDEX [IX_FK_ADParentFolderADFolder]
ON [dbo].[ADFolders]
    ([ParentFolderId]);
GO

-- Creating foreign key on [UUserId] in table 'ADUsers'
ALTER TABLE [dbo].[ADUsers]
ADD CONSTRAINT [FK_UUserADUser]
    FOREIGN KEY ([UUserId])
    REFERENCES [dbo].[UUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UUserADUser'
CREATE INDEX [IX_FK_UUserADUser]
ON [dbo].[ADUsers]
    ([UUserId]);
GO

-- Creating foreign key on [FunctionId] in table 'IFunctionAccesses'
ALTER TABLE [dbo].[IFunctionAccesses]
ADD CONSTRAINT [FK_FunctionAccessFunction]
    FOREIGN KEY ([FunctionId])
    REFERENCES [dbo].[IFunctions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FunctionAccessFunction'
CREATE INDEX [IX_FK_FunctionAccessFunction]
ON [dbo].[IFunctionAccesses]
    ([FunctionId]);
GO

-- Creating foreign key on [FunctionAccessId] in table 'IQuestAccesses'
ALTER TABLE [dbo].[IQuestAccesses]
ADD CONSTRAINT [FK_FunctionAccessQuestAccess]
    FOREIGN KEY ([FunctionAccessId])
    REFERENCES [dbo].[IFunctionAccesses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FunctionAccessQuestAccess'
CREATE INDEX [IX_FK_FunctionAccessQuestAccess]
ON [dbo].[IQuestAccesses]
    ([FunctionAccessId]);
GO

-- Creating foreign key on [UserID] in table 'IFunctionAccesses'
ALTER TABLE [dbo].[IFunctionAccesses]
ADD CONSTRAINT [FK_ISDAVS_UserFunctionAccess]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[ISDAVSUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISDAVS_UserFunctionAccess'
CREATE INDEX [IX_FK_ISDAVS_UserFunctionAccess]
ON [dbo].[IFunctionAccesses]
    ([UserID]);
GO

-- Creating foreign key on [QuestId] in table 'IQuestAccesses'
ALTER TABLE [dbo].[IQuestAccesses]
ADD CONSTRAINT [FK_QuestAccessQuest]
    FOREIGN KEY ([QuestId])
    REFERENCES [dbo].[IQuests]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestAccessQuest'
CREATE INDEX [IX_FK_QuestAccessQuest]
ON [dbo].[IQuestAccesses]
    ([QuestId]);
GO

-- Creating foreign key on [UUserId] in table 'ISDAVSUsers'
ALTER TABLE [dbo].[ISDAVSUsers]
ADD CONSTRAINT [FK_UUserISDAVSUser]
    FOREIGN KEY ([UUserId])
    REFERENCES [dbo].[UUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UUserISDAVSUser'
CREATE INDEX [IX_FK_UUserISDAVSUser]
ON [dbo].[ISDAVSUsers]
    ([UUserId]);
GO

-- Creating foreign key on [EmploeeID] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [FK_PV_EmploeePV_HistEmploee]
    FOREIGN KEY ([EmploeeID])
    REFERENCES [dbo].[PVEmploees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PV_EmploeePV_HistEmploee'
CREATE INDEX [IX_FK_PV_EmploeePV_HistEmploee]
ON [dbo].[PVHistEmploees]
    ([EmploeeID]);
GO

-- Creating foreign key on [ProfessionId] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [FK_PV_ProfessionPV_Emploee]
    FOREIGN KEY ([ProfessionId])
    REFERENCES [dbo].[PVProfessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PV_ProfessionPV_Emploee'
CREATE INDEX [IX_FK_PV_ProfessionPV_Emploee]
ON [dbo].[PVEmploees]
    ([ProfessionId]);
GO

-- Creating foreign key on [UUserId] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [FK_UUserPVEmploee]
    FOREIGN KEY ([UUserId])
    REFERENCES [dbo].[UUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UUserPVEmploee'
CREATE INDEX [IX_FK_UUserPVEmploee]
ON [dbo].[PVEmploees]
    ([UUserId]);
GO

-- Creating foreign key on [ProfessionId] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [FK_PV_ProfessionPV_HistEmploee]
    FOREIGN KEY ([ProfessionId])
    REFERENCES [dbo].[PVProfessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PV_ProfessionPV_HistEmploee'
CREATE INDEX [IX_FK_PV_ProfessionPV_HistEmploee]
ON [dbo].[PVHistEmploees]
    ([ProfessionId]);
GO

-- Creating foreign key on [HeadDepartmentId] in table 'PVStructurals'
ALTER TABLE [dbo].[PVStructurals]
ADD CONSTRAINT [FK_PVStructuralPVStructural]
    FOREIGN KEY ([HeadDepartmentId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PVStructuralPVStructural'
CREATE INDEX [IX_FK_PVStructuralPVStructural]
ON [dbo].[PVStructurals]
    ([HeadDepartmentId]);
GO

-- Creating foreign key on [PartId] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [FK_PVEmploeePart]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PVEmploeePart'
CREATE INDEX [IX_FK_PVEmploeePart]
ON [dbo].[PVEmploees]
    ([PartId]);
GO

-- Creating foreign key on [DepartmentId] in table 'PVEmploees'
ALTER TABLE [dbo].[PVEmploees]
ADD CONSTRAINT [FK_PVEmploeeDepartment]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PVEmploeeDepartment'
CREATE INDEX [IX_FK_PVEmploeeDepartment]
ON [dbo].[PVEmploees]
    ([DepartmentId]);
GO

-- Creating foreign key on [PartId] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [FK_PartPVHistEmploee]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartPVHistEmploee'
CREATE INDEX [IX_FK_PartPVHistEmploee]
ON [dbo].[PVHistEmploees]
    ([PartId]);
GO

-- Creating foreign key on [DepartmentId] in table 'PVHistEmploees'
ALTER TABLE [dbo].[PVHistEmploees]
ADD CONSTRAINT [FK_DepartmentPVHistEmploee]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[PVStructurals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentPVHistEmploee'
CREATE INDEX [IX_FK_DepartmentPVHistEmploee]
ON [dbo].[PVHistEmploees]
    ([DepartmentId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------