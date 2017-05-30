sqlb\sqlb
PTR
--=====================================================================--
--<Davisa db tabulas nosaukums> <PTR Table name>
-- <Server>
-- <Database>
--=====================================================================--
--p_i_user ISDAVSUser_import
-- sqla
-- Admin
SELECT user_id, user_login_name, user_name, user_surname, user_phone, user_email, user_horizon_id
FROM [dbo].[ad_users]
--Table--
CREATE TABLE [dbo].[ISDAVSUser_import] (
[user_id] int NOT NULL,
[user_login_name] varchar(30) NOT NULL,
[user_name] varchar(30) NOT NULL,
[user_surname] varchar(50),
[user_phone] varchar(20),
[user_email] varchar(300),
[user_horizon_id] int
)
--p_function_access IFunctionAccess_import
-- sqla
-- Admin
SELECT [access_id]
      ,[user_id]
      ,[appl_funct_id]
FROM [Admin].[dbo].[ad_user_funct_rights]
--Table--
CREATE TABLE [dbo].[IFunctionAccess_import] (
[Id] int NOT NULL,
[UserId] int NOT NULL,
[FunctionId] int NOT NULL
)
--p_function IFunction_import
-- sqla
-- Admin
SELECT [appl_funct_id]
      ,[appl_funct_type]
      ,[appl_funct_short_name]
      ,[appl_funct_description]
FROM [Admin].[dbo].[ad_appl_functions]
--
CREATE TABLE [dbo].[IFunction_import] (
[Id] int NOT NULL,
[Type] bit NOT NULL,
[Name] varchar(100) NOT NULL,
[Description] varchar(250)
)
--p_quest_access IQuestAccess_import
-- sqla
-- Admin
SELECT [user_quest_id]
      ,[access_id]
      ,[quest_id]
FROM [Admin].[dbo].[ad_user_quests]
--Table--
CREATE TABLE [dbo].[IQuestAccess_import] (
[Id] int NOT NULL,
[FunctionAccessId] int NOT NULL,
[QuestId] int NOT NULL
)
--p_quest IQuest_import
-- sqla
-- MetaDataBase
SELECT [quest_id]
      ,[name]
      ,[name_eng]
      ,[q_index]
FROM [MetaDataBase].[dbo].[me_questionnaires]
--
CREATE TABLE [dbo].[IQuest_import] (
[quest_id] int NOT NULL,
[name] varchar(80) NOT NULL,
[name_eng] varchar(80),
[q_index] varchar(30) NOT NULL
)

--PVEmploee
-- H-59181
-- CSP_WH
SELECT [PER_ID]
      ,[PER_VAR]
      ,[PER_UZV]
      ,[AMATS_ID]
      ,[DALA_ID]
      ,[DEPART_ID]
      ,[PER_STATUSS]
  FROM [CSP_WH].[dbo].[LU_PER]
--Table--
CREATE TABLE [dbo].[PVEmploee_import] (
[Id] int NOT NULL,
[Name] varchar(50),
[Surname] varchar(50),
[ProfessionId] int,
[PartId] varchar(50),
[DepartmentId] varchar(50),
[Status] int
)
--PVStructural
-- H-59181
-- CSP_WH
SELECT [STR_ID]
      ,[STR_KODS]
      ,[STR_NOSAUKUMS]
      ,[STR_LIMENIS]
      ,[ATB_STR_KODS]
  FROM [CSP_WH].[exigen].[LN_Struktura]
  --Table--
CREATE TABLE [dbo].[PVStructural_import] (
[Id] varchar(50) NOT NULL,
[Code] varchar(50) NOT NULL,
[Name] varchar(50),
[Level] varchar(50),
[HeadDeptCode] varchar(50)
