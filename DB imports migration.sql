-- Creating table 'IFunction_import'
DECLARE @F TABLE ([Id] int  NOT NULL, [Type] bit  NOT NULL, [Name] varchar(100)  NOT NULL, [Description] varchar(250)  NULL
)
INSERT @F
SELECT	*
FROM	IFunction_import

-- Creating table 'IFunctionAccess_import'
DECLARE @FA TABLE ( [Id] int  NOT NULL, [UserId] int  NOT NULL, [FunctionId] int  NOT NULL
)
INSERT @FA
SELECT	*
FROM	IFunctionAccess_import

-- Creating table 'IQuest_import'
DECLARE @Q TABLE (
    [quest_id] int  NOT NULL, [name] varchar(80)  NOT NULL, [name_eng] varchar(80)  NULL, [q_index] varchar(30)  NOT NULL
)
INSERT @Q
SELECT	*
FROM	IQuest_import

-- Creating table 'IQuestAccess_import'
DECLARE @QA TABLE ( [Id] int  NOT NULL, [FunctionAccessId] int  NOT NULL, [QuestId] int  NOT NULL
)
INSERT @QA
SELECT	*
FROM	IQuestAccess_import

-- Creating table 'ISDAVSUser_import'
DECLARE @IU TABLE ( [user_id] int  NOT NULL, [user_login_name] varchar(30)  NOT NULL, [user_surname] varchar(50)  NULL, [user_phone] varchar(20)  NULL, [user_email] varchar(300)  NULL, [user_horizon_id] int  NULL, [user_name] varchar(30)  NOT NULL
)
INSERT @IU
SELECT	*
FROM	ISDAVSUser_import

-- Creating table 'PVEmploee_import'
DECLARE @PVE TABLE ( [Id] int  NOT NULL, [Name] varchar(50)  NULL, [Surname] varchar(50)  NULL, [ProfessionId] int  NULL, [PartId] nchar(50)  NULL, [DepartmentId] nchar(50)  NULL, [Status] int  NULL
)
INSERT @PVE
SELECT	*
FROM	PVEmploee_import

-- Creating table 'PVStructural_import'
DECLARE @PVS TABLE (
    [Id] nchar(50)  NOT NULL, [Code] varchar(50)  NOT NULL, [Name] varchar(50)  NULL, [Level] varchar(50)  NULL, [HeadDeptCode] varchar(50)  NULL
)
INSERT @PVS
SELECT	*
FROM	PVStructural_import

-----------------------------------------------------------------------------

INSERT	IFunction_import
SELECT	*
FROM	@F

INSERT	IFunctionAccess_import
SELECT	*
FROM	@FA

INSERT	IQuest_import
SELECT	*
FROM	@Q

INSERT	IQuestAccess_import
SELECT	*
FROM	@QA

INSERT	ISDAVSUser_import
SELECT	*
FROM	@IU

INSERT	PVEmploee_import
SELECT	*
FROM	@PVE

INSERT	PVStructural_import
SELECT	*
FROM	@PVS
