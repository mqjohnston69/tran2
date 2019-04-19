
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/23/2016 10:04:33
-- Generated from EDMX file: C:\Projects\Release1.3\easy.forward.Git\Oc.Carbon.DataAccess\SoPlatformModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [So_Platform];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AttribAttribTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attribs] DROP CONSTRAINT [FK_AttribAttribTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_AuditDtl_AuditAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuditDtls] DROP CONSTRAINT [FK_AuditDtl_AuditAction];
GO
IF OBJECT_ID(N'[dbo].[FK_Audit_AuditCatAuditTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Audits] DROP CONSTRAINT [FK_Audit_AuditCatAuditTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_AuditCatAuditTyp_AuditCat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuditCatAuditTyps] DROP CONSTRAINT [FK_AuditCatAuditTyp_AuditCat];
GO
IF OBJECT_ID(N'[dbo].[FK_AuditCatAuditTyp_AuditTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuditCatAuditTyps] DROP CONSTRAINT [FK_AuditCatAuditTyp_AuditTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_AuditDtl_Audit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuditDtls] DROP CONSTRAINT [FK_AuditDtl_Audit];
GO
IF OBJECT_ID(N'[dbo].[FK_Audit_AncestorAudit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Audits] DROP CONSTRAINT [FK_Audit_AncestorAudit];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthItemAuthItemAuthPerm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthItemAuthPerms] DROP CONSTRAINT [FK_AuthItemAuthItemAuthPerm];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthItemAuthPermAuthRolAuthItemAuthPerm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthRolAuthItemAuthPerms] DROP CONSTRAINT [FK_AuthItemAuthPermAuthRolAuthItemAuthPerm];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthPermAuthItemAuthPerm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthItemAuthPerms] DROP CONSTRAINT [FK_AuthPermAuthItemAuthPerm];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthItemMenu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Menus] DROP CONSTRAINT [FK_AuthItemMenu];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthItemModelEntityAttribute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModelEntityAttributes] DROP CONSTRAINT [FK_AuthItemModelEntityAttribute];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthItemModElmElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModElmElms] DROP CONSTRAINT [FK_AuthItemModElmElm];
GO
IF OBJECT_ID(N'[dbo].[FK_ElmAuthItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Elms] DROP CONSTRAINT [FK_ElmAuthItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ElmElmAuthItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ElmElms] DROP CONSTRAINT [FK_ElmElmAuthItem];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuItemAuthItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuItems] DROP CONSTRAINT [FK_MenuItemAuthItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ModelEntityAuthItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModelEntities] DROP CONSTRAINT [FK_ModelEntityAuthItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ModElmAuthItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModElms] DROP CONSTRAINT [FK_ModElmAuthItem];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthRolAuthRolAuthItemAuthPerm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthRolAuthItemAuthPerms] DROP CONSTRAINT [FK_AuthRolAuthRolAuthItemAuthPerm];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthRolModAuthRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModAuthRols] DROP CONSTRAINT [FK_AuthRolModAuthRol];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthRolTypAuthRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthRols] DROP CONSTRAINT [FK_AuthRolTypAuthRol];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactOrgContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgContacts] DROP CONSTRAINT [FK_ContactOrgContract];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactPersContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersContacts] DROP CONSTRAINT [FK_ContactPersContact];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactTypContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_ContactTypContact];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryLocn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locns] DROP CONSTRAINT [FK_CountryLocn];
GO
IF OBJECT_ID(N'[dbo].[FK_DocWkflowInstanceDoc]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowInstanceDocs] DROP CONSTRAINT [FK_DocWkflowInstanceDoc];
GO
IF OBJECT_ID(N'[dbo].[FK_ElmElmElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ElmElms] DROP CONSTRAINT [FK_ElmElmElm];
GO
IF OBJECT_ID(N'[dbo].[FK_ElmElmElm1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ElmElms] DROP CONSTRAINT [FK_ElmElmElm1];
GO
IF OBJECT_ID(N'[dbo].[FK_ElmElmTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Elms] DROP CONSTRAINT [FK_ElmElmTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_ElmModElmElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModElmElms] DROP CONSTRAINT [FK_ElmModElmElm];
GO
IF OBJECT_ID(N'[dbo].[FK_ModelEntityAttributeEntityAttributeTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModelEntityAttributes] DROP CONSTRAINT [FK_ModelEntityAttributeEntityAttributeTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_GenderIconVisual]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Genders] DROP CONSTRAINT [FK_GenderIconVisual];
GO
IF OBJECT_ID(N'[dbo].[FK_GenderPers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pers] DROP CONSTRAINT [FK_GenderPers];
GO
IF OBJECT_ID(N'[dbo].[FK_IconVisualIconVisualTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IconVisuals] DROP CONSTRAINT [FK_IconVisualIconVisualTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_IconVisualLogCat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogCats] DROP CONSTRAINT [FK_IconVisualLogCat];
GO
IF OBJECT_ID(N'[dbo].[FK_IconVisualMenuItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuItems] DROP CONSTRAINT [FK_IconVisualMenuItem];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuIconVisual]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Menus] DROP CONSTRAINT [FK_MenuIconVisual];
GO
IF OBJECT_ID(N'[dbo].[FK_PortModIconVisual]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortMods] DROP CONSTRAINT [FK_PortModIconVisual];
GO
IF OBJECT_ID(N'[dbo].[FK_SceneLayoutTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Scenes] DROP CONSTRAINT [FK_SceneLayoutTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_LocalityPers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pers] DROP CONSTRAINT [FK_LocalityPers];
GO
IF OBJECT_ID(N'[dbo].[FK_LocnOrgLocn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgLocns] DROP CONSTRAINT [FK_LocnOrgLocn];
GO
IF OBJECT_ID(N'[dbo].[FK_PersLocn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pers] DROP CONSTRAINT [FK_PersLocn];
GO
IF OBJECT_ID(N'[dbo].[FK_LogEntryLogCat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogEntries] DROP CONSTRAINT [FK_LogEntryLogCat];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuItemMenu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuItems] DROP CONSTRAINT [FK_MenuItemMenu];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuItemMenuItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuItems] DROP CONSTRAINT [FK_MenuItemMenuItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ModStateMenuItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuItems] DROP CONSTRAINT [FK_ModStateMenuItem];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuPortMod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortMods] DROP CONSTRAINT [FK_MenuPortMod];
GO
IF OBJECT_ID(N'[dbo].[FK_ModAuthRolUserModAuthRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserModAuthRols] DROP CONSTRAINT [FK_ModAuthRolUserModAuthRol];
GO
IF OBJECT_ID(N'[dbo].[FK_ModModAuthRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModAuthRols] DROP CONSTRAINT [FK_ModModAuthRol];
GO
IF OBJECT_ID(N'[dbo].[FK_ModelEntityModelEntityAttribute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModelEntityAttributes] DROP CONSTRAINT [FK_ModelEntityModelEntityAttribute];
GO
IF OBJECT_ID(N'[dbo].[FK_ModElmModElmElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModElmElms] DROP CONSTRAINT [FK_ModElmModElmElm];
GO
IF OBJECT_ID(N'[dbo].[FK_ModElmModElmTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModElms] DROP CONSTRAINT [FK_ModElmModElmTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_ModElmPortModModElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortModModElms] DROP CONSTRAINT [FK_ModElmPortModModElm];
GO
IF OBJECT_ID(N'[dbo].[FK_ModModElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModElms] DROP CONSTRAINT [FK_ModModElm];
GO
IF OBJECT_ID(N'[dbo].[FK_ModModSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModSettings] DROP CONSTRAINT [FK_ModModSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_ModModState]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModStates] DROP CONSTRAINT [FK_ModModState];
GO
IF OBJECT_ID(N'[dbo].[FK_ModPart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parts] DROP CONSTRAINT [FK_ModPart];
GO
IF OBJECT_ID(N'[dbo].[FK_ModPortMod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortMods] DROP CONSTRAINT [FK_ModPortMod];
GO
IF OBJECT_ID(N'[dbo].[FK_ModTypMod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mods] DROP CONSTRAINT [FK_ModTypMod];
GO
IF OBJECT_ID(N'[dbo].[FK_ModSettingModSettingValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModSettingValues] DROP CONSTRAINT [FK_ModSettingModSettingValue];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingModSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModSettings] DROP CONSTRAINT [FK_SettingModSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgContacts] DROP CONSTRAINT [FK_OrgOrgContract];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgCust]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgCusts] DROP CONSTRAINT [FK_OrgOrgCust];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgLocn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgLocns] DROP CONSTRAINT [FK_OrgOrgLocn];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgNoteUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgNotes] DROP CONSTRAINT [FK_OrgNoteUser];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgNotes] DROP CONSTRAINT [FK_OrgOrgNote];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgOrg]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgOrgs] DROP CONSTRAINT [FK_OrgOrgOrg];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgOrg1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgOrgs] DROP CONSTRAINT [FK_OrgOrgOrg1];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgStatusHist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgStatusHists] DROP CONSTRAINT [FK_OrgOrgStatusHist];
GO
IF OBJECT_ID(N'[dbo].[FK_UserOrg]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orgs] DROP CONSTRAINT [FK_UserOrg];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgStatusOrgTypOrgStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgTypOrgStatus] DROP CONSTRAINT [FK_OrgStatusOrgTypOrgStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgTypOrgTypOrgStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgTypOrgStatus] DROP CONSTRAINT [FK_OrgTypOrgTypOrgStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_PartGrpPartGrpPart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartGrpParts] DROP CONSTRAINT [FK_PartGrpPartGrpPart];
GO
IF OBJECT_ID(N'[dbo].[FK_PartPartGrpPart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartGrpParts] DROP CONSTRAINT [FK_PartPartGrpPart];
GO
IF OBJECT_ID(N'[dbo].[FK_PartIndicatorPart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parts] DROP CONSTRAINT [FK_PartIndicatorPart];
GO
IF OBJECT_ID(N'[dbo].[FK_PartPartSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartSettings] DROP CONSTRAINT [FK_PartPartSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_PartScenePart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SceneParts] DROP CONSTRAINT [FK_PartScenePart];
GO
IF OBJECT_ID(N'[dbo].[FK_PartTypPart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parts] DROP CONSTRAINT [FK_PartTypPart];
GO
IF OBJECT_ID(N'[dbo].[FK_PartSettingScenePartPartSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenePartPartSettings] DROP CONSTRAINT [FK_PartSettingScenePartPartSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingPartSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartSettings] DROP CONSTRAINT [FK_SettingPartSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_PerPerNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PerNotes] DROP CONSTRAINT [FK_PerPerNote];
GO
IF OBJECT_ID(N'[dbo].[FK_PersPersContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersContacts] DROP CONSTRAINT [FK_PersPersContact];
GO
IF OBJECT_ID(N'[dbo].[FK_PerUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_PerUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenePlaceHolder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlaceHolders] DROP CONSTRAINT [FK_ScenePlaceHolder];
GO
IF OBJECT_ID(N'[dbo].[FK_PortModPortModModElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortModModElms] DROP CONSTRAINT [FK_PortModPortModModElm];
GO
IF OBJECT_ID(N'[dbo].[FK_PortModPortModSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortModSettings] DROP CONSTRAINT [FK_PortModPortModSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_PortPortMod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortMods] DROP CONSTRAINT [FK_PortPortMod];
GO
IF OBJECT_ID(N'[dbo].[FK_PortModSettingPortModSettingValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortModSettingValues] DROP CONSTRAINT [FK_PortModSettingPortModSettingValue];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingPortModSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortModSettings] DROP CONSTRAINT [FK_SettingPortModSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_PortPortScene]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortScenes] DROP CONSTRAINT [FK_PortPortScene];
GO
IF OBJECT_ID(N'[dbo].[FK_PortPortUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortUsers] DROP CONSTRAINT [FK_PortPortUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenePortScene]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortScenes] DROP CONSTRAINT [FK_ScenePortScene];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPortUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortUsers] DROP CONSTRAINT [FK_UserPortUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenePartScenePartPartSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenePartPartSettings] DROP CONSTRAINT [FK_ScenePartScenePartPartSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_SceneScenePart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SceneParts] DROP CONSTRAINT [FK_SceneScenePart];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingTypSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Settings] DROP CONSTRAINT [FK_SettingTypSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserLoginHist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLoginHists] DROP CONSTRAINT [FK_UserUserLoginHist];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserModAuthRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserModAuthRols] DROP CONSTRAINT [FK_UserUserModAuthRol];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowDefWkflowDefWkflowStat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowDefWkflowStats] DROP CONSTRAINT [FK_WkflowDefWkflowDefWkflowStat];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowDefWkflowInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowInstances] DROP CONSTRAINT [FK_WkflowDefWkflowInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowTypWkflowDef]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowDefs] DROP CONSTRAINT [FK_WkflowTypWkflowDef];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowDefStatReasWkflowDefWkflowStat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowDefStatReas] DROP CONSTRAINT [FK_WkflowDefStatReasWkflowDefWkflowStat];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowDefStatReasWkflowDefWkflowStat1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowDefStatReas] DROP CONSTRAINT [FK_WkflowDefStatReasWkflowDefWkflowStat1];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowStatReasWkflowTypStatReas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowDefStatReas] DROP CONSTRAINT [FK_WkflowStatReasWkflowTypStatReas];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowDefWkflowStatWkflowDefWkflowStatWkflowStat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowDefWkflowStatWkflowStats] DROP CONSTRAINT [FK_WkflowDefWkflowStatWkflowDefWkflowStatWkflowStat];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowStatWkflowTypWkflowStat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowDefWkflowStats] DROP CONSTRAINT [FK_WkflowStatWkflowTypWkflowStat];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowStatWkflowTypeWkflowStatWkflowStat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowDefWkflowStatWkflowStats] DROP CONSTRAINT [FK_WkflowStatWkflowTypeWkflowStatWkflowStat];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowInstanceWkflowInstanceDoc]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowInstanceDocs] DROP CONSTRAINT [FK_WkflowInstanceWkflowInstanceDoc];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowInstanceWkflowInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowInstances] DROP CONSTRAINT [FK_WkflowInstanceWkflowInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowWkflowStep]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowStepHists] DROP CONSTRAINT [FK_WkflowWkflowStep];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowStatReasWkflowStepHist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowStepHists] DROP CONSTRAINT [FK_WkflowStatReasWkflowStepHist];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowStepHistWkflowStepNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowStepNotes] DROP CONSTRAINT [FK_WkflowStepHistWkflowStepNote];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgReseller]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgResellers] DROP CONSTRAINT [FK_OrgOrgReseller];
GO
IF OBJECT_ID(N'[dbo].[FK_FileTypeDoc]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[soUploads] DROP CONSTRAINT [FK_FileTypeDoc];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowInstanceDPWorkflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DPWorkflows] DROP CONSTRAINT [FK_WkflowInstanceDPWorkflow];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgDPWorkflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DPWorkflows] DROP CONSTRAINT [FK_OrgDPWorkflow];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDPWorkflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DPWorkflows] DROP CONSTRAINT [FK_UserDPWorkflow];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgDocTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTyps] DROP CONSTRAINT [FK_OrgOrgDocTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_DataElmDataElmTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DataElms] DROP CONSTRAINT [FK_DataElmDataElmTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgDocTypOrgDocTypDataElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTypDataElms] DROP CONSTRAINT [FK_OrgDocTypOrgDocTypDataElm];
GO
IF OBJECT_ID(N'[dbo].[FK_DataElmOrgDocTypDataElm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTypDataElms] DROP CONSTRAINT [FK_DataElmOrgDocTypDataElm];
GO
IF OBJECT_ID(N'[dbo].[FK_DocOrg]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[soUploads] DROP CONSTRAINT [FK_DocOrg];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgUsers] DROP CONSTRAINT [FK_OrgOrgUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserOrgUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgUsers] DROP CONSTRAINT [FK_UserOrgUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthItemMod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mods] DROP CONSTRAINT [FK_AuthItemMod];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthRolUserAuthRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAuthRols] DROP CONSTRAINT [FK_AuthRolUserAuthRol];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserAuthRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAuthRols] DROP CONSTRAINT [FK_UserUserAuthRol];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowStatWkflowStepHist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowStepHists] DROP CONSTRAINT [FK_WkflowStatWkflowStepHist];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgTypOrg]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orgs] DROP CONSTRAINT [FK_OrgTypOrg];
GO
IF OBJECT_ID(N'[dbo].[FK_PackageOrgPackage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgPackages] DROP CONSTRAINT [FK_PackageOrgPackage];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgPackage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgPackages] DROP CONSTRAINT [FK_OrgOrgPackage];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageMessageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageUsers] DROP CONSTRAINT [FK_MessageMessageUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMessageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageUsers] DROP CONSTRAINT [FK_UserMessageUser];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgPackageOrgPackageHist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgPackageHists] DROP CONSTRAINT [FK_OrgPackageOrgPackageHist];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgWkflowInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowInstances] DROP CONSTRAINT [FK_OrgWkflowInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowInstanceUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowInstances] DROP CONSTRAINT [FK_WkflowInstanceUser];
GO
IF OBJECT_ID(N'[dbo].[FK_DocOrgDocTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[soUploads] DROP CONSTRAINT [FK_DocOrgDocTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgTypOrgStatusOrgStatusHist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgStatusHists] DROP CONSTRAINT [FK_OrgTypOrgStatusOrgStatusHist];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingPortSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortSettings] DROP CONSTRAINT [FK_SettingPortSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_PortPortSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortSettings] DROP CONSTRAINT [FK_PortPortSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_PortSettingPortSettingValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortSettingValues] DROP CONSTRAINT [FK_PortSettingPortSettingValue];
GO
IF OBJECT_ID(N'[dbo].[FK_DocTypStatusOrgDocTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTyps] DROP CONSTRAINT [FK_DocTypStatusOrgDocTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_UserOrgDocTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTyps] DROP CONSTRAINT [FK_UserOrgDocTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_UserOrgDocTyp1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTyps] DROP CONSTRAINT [FK_UserOrgDocTyp1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserOrgDocTyp2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTyps] DROP CONSTRAINT [FK_UserOrgDocTyp2];
GO
IF OBJECT_ID(N'[dbo].[FK_PortMessageTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageTemplates] DROP CONSTRAINT [FK_PortMessageTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowInstanceMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_WkflowInstanceMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryMethodMessageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageUsers] DROP CONSTRAINT [FK_DeliveryMethodMessageUser];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowStepHistUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowStepHists] DROP CONSTRAINT [FK_WkflowStepHistUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserWkflowInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WkflowInstances] DROP CONSTRAINT [FK_UserWkflowInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgDocTypWkflowInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTyps] DROP CONSTRAINT [FK_OrgDocTypWkflowInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_SowAttributeSowWklowSowAttribute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SowWklowSowAttributes] DROP CONSTRAINT [FK_SowAttributeSowWklowSowAttribute];
GO
IF OBJECT_ID(N'[dbo].[FK_SowWkflowSowWklowSowAttribute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SowWklowSowAttributes] DROP CONSTRAINT [FK_SowWkflowSowWklowSowAttribute];
GO
IF OBJECT_ID(N'[dbo].[FK_SowWkflowSowWkflowDocSetup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SowWkflowDocSetups] DROP CONSTRAINT [FK_SowWkflowSowWkflowDocSetup];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgResellerOrgResellerDiscHist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgResellerDiscHists] DROP CONSTRAINT [FK_OrgResellerOrgResellerDiscHist];
GO
IF OBJECT_ID(N'[dbo].[FK_SowWkflowSOWWkflowOrgResellerDiscOverride]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SOWWkflowOrgResellerDiscOverrides] DROP CONSTRAINT [FK_SowWkflowSOWWkflowOrgResellerDiscOverride];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgResellerSOWWkflowOrgResellerDiscOverride]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SOWWkflowOrgResellerDiscOverrides] DROP CONSTRAINT [FK_OrgResellerSOWWkflowOrgResellerDiscOverride];
GO
IF OBJECT_ID(N'[dbo].[FK_WkflowInstanceSowWkflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SowWkflows] DROP CONSTRAINT [FK_WkflowInstanceSowWkflow];
GO
IF OBJECT_ID(N'[dbo].[FK_SowWklowSowAttributeSowAttributeValueHist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SowAttributeValueHists] DROP CONSTRAINT [FK_SowWklowSowAttributeSowAttributeValueHist];
GO
IF OBJECT_ID(N'[dbo].[FK_MonthOrgDocTypMonth]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTypMonths] DROP CONSTRAINT [FK_MonthOrgDocTypMonth];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgDocTypOrgDocTypMonth]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTypMonths] DROP CONSTRAINT [FK_OrgDocTypOrgDocTypMonth];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgDocTypSowWkflowDocSetup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SowWkflowDocSetups] DROP CONSTRAINT [FK_OrgDocTypSowWkflowDocSetup];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgSubWhiteList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubWhiteLists] DROP CONSTRAINT [FK_OrgSubWhiteList];
GO
IF OBJECT_ID(N'[dbo].[FK_MonthOrgMonthCommitment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgMonthCommitments] DROP CONSTRAINT [FK_MonthOrgMonthCommitment];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgMonthCommitment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgMonthCommitments] DROP CONSTRAINT [FK_OrgOrgMonthCommitment];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgDocTypOrgDocTypDailyUpload]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTypDailyUploads] DROP CONSTRAINT [FK_OrgDocTypOrgDocTypDailyUpload];
GO
IF OBJECT_ID(N'[dbo].[FK_OrgOrgDocTypDailyUpload]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrgDocTypDailyUploads] DROP CONSTRAINT [FK_OrgOrgDocTypDailyUpload];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Attribs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attribs];
GO
IF OBJECT_ID(N'[dbo].[AttribTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AttribTyps];
GO
IF OBJECT_ID(N'[dbo].[AuditActions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditActions];
GO
IF OBJECT_ID(N'[dbo].[AuditCatAuditTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditCatAuditTyps];
GO
IF OBJECT_ID(N'[dbo].[AuditCats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditCats];
GO
IF OBJECT_ID(N'[dbo].[AuditDtls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditDtls];
GO
IF OBJECT_ID(N'[dbo].[Audits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Audits];
GO
IF OBJECT_ID(N'[dbo].[AuditTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditTyps];
GO
IF OBJECT_ID(N'[dbo].[AuthItemAuthPerms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthItemAuthPerms];
GO
IF OBJECT_ID(N'[dbo].[AuthItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthItems];
GO
IF OBJECT_ID(N'[dbo].[AuthPerms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthPerms];
GO
IF OBJECT_ID(N'[dbo].[AuthRolAuthItemAuthPerms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthRolAuthItemAuthPerms];
GO
IF OBJECT_ID(N'[dbo].[AuthRols]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthRols];
GO
IF OBJECT_ID(N'[dbo].[AuthRolTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthRolTyps];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[ContactTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactTyps];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[soUploads]', 'U') IS NOT NULL
    DROP TABLE [dbo].[soUploads];
GO
IF OBJECT_ID(N'[dbo].[ElmElms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ElmElms];
GO
IF OBJECT_ID(N'[dbo].[Elms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Elms];
GO
IF OBJECT_ID(N'[dbo].[ElmTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ElmTyps];
GO
IF OBJECT_ID(N'[dbo].[EntityAttributeTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntityAttributeTyps];
GO
IF OBJECT_ID(N'[dbo].[EquipTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EquipTyps];
GO
IF OBJECT_ID(N'[dbo].[Genders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genders];
GO
IF OBJECT_ID(N'[dbo].[IconVisuals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IconVisuals];
GO
IF OBJECT_ID(N'[dbo].[IconVisualTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IconVisualTyps];
GO
IF OBJECT_ID(N'[dbo].[LayoutTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LayoutTyps];
GO
IF OBJECT_ID(N'[dbo].[Localities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Localities];
GO
IF OBJECT_ID(N'[dbo].[Locns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locns];
GO
IF OBJECT_ID(N'[dbo].[LogCats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogCats];
GO
IF OBJECT_ID(N'[dbo].[LogEntries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogEntries];
GO
IF OBJECT_ID(N'[dbo].[MenuItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuItems];
GO
IF OBJECT_ID(N'[dbo].[Menus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menus];
GO
IF OBJECT_ID(N'[dbo].[ModAuthRols]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModAuthRols];
GO
IF OBJECT_ID(N'[dbo].[ModelEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModelEntities];
GO
IF OBJECT_ID(N'[dbo].[ModelEntityAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModelEntityAttributes];
GO
IF OBJECT_ID(N'[dbo].[ModElmElms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModElmElms];
GO
IF OBJECT_ID(N'[dbo].[ModElms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModElms];
GO
IF OBJECT_ID(N'[dbo].[ModElmTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModElmTyps];
GO
IF OBJECT_ID(N'[dbo].[Mods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mods];
GO
IF OBJECT_ID(N'[dbo].[ModSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModSettings];
GO
IF OBJECT_ID(N'[dbo].[ModSettingValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModSettingValues];
GO
IF OBJECT_ID(N'[dbo].[ModStates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModStates];
GO
IF OBJECT_ID(N'[dbo].[ModTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModTyps];
GO
IF OBJECT_ID(N'[dbo].[OrgContacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgContacts];
GO
IF OBJECT_ID(N'[dbo].[OrgCusts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgCusts];
GO
IF OBJECT_ID(N'[dbo].[OrgLocns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgLocns];
GO
IF OBJECT_ID(N'[dbo].[OrgNotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgNotes];
GO
IF OBJECT_ID(N'[dbo].[OrgOrgs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgOrgs];
GO
IF OBJECT_ID(N'[dbo].[Orgs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orgs];
GO
IF OBJECT_ID(N'[dbo].[OrgStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgStatus];
GO
IF OBJECT_ID(N'[dbo].[OrgStatusHists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgStatusHists];
GO
IF OBJECT_ID(N'[dbo].[OrgTypOrgStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgTypOrgStatus];
GO
IF OBJECT_ID(N'[dbo].[OrgTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgTyps];
GO
IF OBJECT_ID(N'[dbo].[PartGrpParts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartGrpParts];
GO
IF OBJECT_ID(N'[dbo].[PartGrps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartGrps];
GO
IF OBJECT_ID(N'[dbo].[PartIndicators]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartIndicators];
GO
IF OBJECT_ID(N'[dbo].[Parts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parts];
GO
IF OBJECT_ID(N'[dbo].[PartSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartSettings];
GO
IF OBJECT_ID(N'[dbo].[PartTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartTyps];
GO
IF OBJECT_ID(N'[dbo].[PerNotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerNotes];
GO
IF OBJECT_ID(N'[dbo].[Pers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pers];
GO
IF OBJECT_ID(N'[dbo].[PersContacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersContacts];
GO
IF OBJECT_ID(N'[dbo].[PlaceHolders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlaceHolders];
GO
IF OBJECT_ID(N'[dbo].[PortModModElms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortModModElms];
GO
IF OBJECT_ID(N'[dbo].[PortMods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortMods];
GO
IF OBJECT_ID(N'[dbo].[PortModSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortModSettings];
GO
IF OBJECT_ID(N'[dbo].[PortModSettingValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortModSettingValues];
GO
IF OBJECT_ID(N'[dbo].[Ports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ports];
GO
IF OBJECT_ID(N'[dbo].[PortScenes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortScenes];
GO
IF OBJECT_ID(N'[dbo].[PortUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortUsers];
GO
IF OBJECT_ID(N'[dbo].[ScenePartPartSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScenePartPartSettings];
GO
IF OBJECT_ID(N'[dbo].[SceneParts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SceneParts];
GO
IF OBJECT_ID(N'[dbo].[Scenes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scenes];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[SettingTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettingTyps];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[UserLoginHists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLoginHists];
GO
IF OBJECT_ID(N'[dbo].[UserModAuthRols]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserModAuthRols];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[WkflowDefs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowDefs];
GO
IF OBJECT_ID(N'[dbo].[WkflowDefStatReas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowDefStatReas];
GO
IF OBJECT_ID(N'[dbo].[WkflowDefWkflowStats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowDefWkflowStats];
GO
IF OBJECT_ID(N'[dbo].[WkflowDefWkflowStatWkflowStats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowDefWkflowStatWkflowStats];
GO
IF OBJECT_ID(N'[dbo].[WkflowInstanceDocs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowInstanceDocs];
GO
IF OBJECT_ID(N'[dbo].[WkflowInstances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowInstances];
GO
IF OBJECT_ID(N'[dbo].[WkflowStatReas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowStatReas];
GO
IF OBJECT_ID(N'[dbo].[WkflowStats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowStats];
GO
IF OBJECT_ID(N'[dbo].[WkflowStepHists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowStepHists];
GO
IF OBJECT_ID(N'[dbo].[WkflowStepNotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowStepNotes];
GO
IF OBJECT_ID(N'[dbo].[WkflowTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WkflowTyps];
GO
IF OBJECT_ID(N'[dbo].[ZipCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZipCodes];
GO
IF OBJECT_ID(N'[dbo].[OrgResellers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgResellers];
GO
IF OBJECT_ID(N'[dbo].[ServicePackages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServicePackages];
GO
IF OBJECT_ID(N'[dbo].[FileTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileTypes];
GO
IF OBJECT_ID(N'[dbo].[DPWorkflows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DPWorkflows];
GO
IF OBJECT_ID(N'[dbo].[DataElms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DataElms];
GO
IF OBJECT_ID(N'[dbo].[OrgDocTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgDocTyps];
GO
IF OBJECT_ID(N'[dbo].[DataElmTyps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DataElmTyps];
GO
IF OBJECT_ID(N'[dbo].[OrgDocTypDataElms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgDocTypDataElms];
GO
IF OBJECT_ID(N'[dbo].[OrgUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgUsers];
GO
IF OBJECT_ID(N'[dbo].[UserAuthRols]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuthRols];
GO
IF OBJECT_ID(N'[dbo].[Packages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Packages];
GO
IF OBJECT_ID(N'[dbo].[OrgPackages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgPackages];
GO
IF OBJECT_ID(N'[dbo].[MessageTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageTemplates];
GO
IF OBJECT_ID(N'[dbo].[DeliveryMethods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryMethods];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[MessageUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageUsers];
GO
IF OBJECT_ID(N'[dbo].[OrgPackageHists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgPackageHists];
GO
IF OBJECT_ID(N'[dbo].[Holidays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Holidays];
GO
IF OBJECT_ID(N'[dbo].[SOActivities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SOActivities];
GO
IF OBJECT_ID(N'[dbo].[DocTypStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocTypStatus];
GO
IF OBJECT_ID(N'[dbo].[PortSettingValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortSettingValues];
GO
IF OBJECT_ID(N'[dbo].[PortSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortSettings];
GO
IF OBJECT_ID(N'[dbo].[SowWkflows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SowWkflows];
GO
IF OBJECT_ID(N'[dbo].[SowAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SowAttributes];
GO
IF OBJECT_ID(N'[dbo].[SowAttributeValueHists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SowAttributeValueHists];
GO
IF OBJECT_ID(N'[dbo].[SowWklowSowAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SowWklowSowAttributes];
GO
IF OBJECT_ID(N'[dbo].[SowWkflowDocSetups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SowWkflowDocSetups];
GO
IF OBJECT_ID(N'[dbo].[OrgResellerDiscHists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgResellerDiscHists];
GO
IF OBJECT_ID(N'[dbo].[SOWWkflowOrgResellerDiscOverrides]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SOWWkflowOrgResellerDiscOverrides];
GO
IF OBJECT_ID(N'[dbo].[Months]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Months];
GO
IF OBJECT_ID(N'[dbo].[OrgDocTypMonths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgDocTypMonths];
GO
IF OBJECT_ID(N'[dbo].[SubWhiteLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubWhiteLists];
GO
IF OBJECT_ID(N'[dbo].[OrgMonthCommitments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgMonthCommitments];
GO
IF OBJECT_ID(N'[dbo].[OrgDocTypDailyUploads]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrgDocTypDailyUploads];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Attribs'
CREATE TABLE [dbo].[Attribs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AttribTypId] int  NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AttribTyps'
CREATE TABLE [dbo].[AttribTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AuditActions'
CREATE TABLE [dbo].[AuditActions] (
    [AuditActionCd] nvarchar(4)  NOT NULL,
    [Descript] nvarchar(256)  NOT NULL,
    [DateLastMaint] datetime  NOT NULL
);
GO

-- Creating table 'AuditCatAuditTyps'
CREATE TABLE [dbo].[AuditCatAuditTyps] (
    [AuditCatCd] nvarchar(4)  NOT NULL,
    [AuditTypCd] nvarchar(4)  NOT NULL,
    [DateLastMaint] datetime  NOT NULL
);
GO

-- Creating table 'AuditCats'
CREATE TABLE [dbo].[AuditCats] (
    [AuditCatCd] nvarchar(4)  NOT NULL,
    [Descript] nvarchar(256)  NOT NULL,
    [DateLastMaint] datetime  NOT NULL
);
GO

-- Creating table 'AuditDtls'
CREATE TABLE [dbo].[AuditDtls] (
    [AuditNbr] bigint  NOT NULL,
    [DtlNbr] bigint IDENTITY(1,1) NOT NULL,
    [AuditActionCd] nvarchar(4)  NULL,
    [TableId] nvarchar(120)  NOT NULL,
    [ValueName] nvarchar(120)  NOT NULL,
    [KeyYN] char(1)  NOT NULL,
    [OldValue] nvarchar(256)  NULL,
    [NewValue] nvarchar(256)  NULL,
    [DateLastMaint] datetime  NOT NULL
);
GO

-- Creating table 'Audits'
CREATE TABLE [dbo].[Audits] (
    [AuditNbr] bigint IDENTITY(1,1) NOT NULL,
    [AuditCatCd] nvarchar(4)  NULL,
    [AuditTypCd] nvarchar(4)  NULL,
    [AncestorAuditNbr] bigint  NULL,
    [PortalModuleID] bigint  NULL,
    [PersID] bigint  NULL,
    [ContextPersID] bigint  NULL,
    [StudID] bigint  NULL,
    [StudVisitID] bigint  NULL,
    [WorkflowID] bigint  NULL,
    [AuditDateTime] datetime  NOT NULL,
    [DateLastMaint] datetime  NOT NULL
);
GO

-- Creating table 'AuditTyps'
CREATE TABLE [dbo].[AuditTyps] (
    [AuditTypCd] nvarchar(4)  NOT NULL,
    [Descript] nvarchar(256)  NOT NULL,
    [DateLastMaint] datetime  NOT NULL
);
GO

-- Creating table 'AuthItemAuthPerms'
CREATE TABLE [dbo].[AuthItemAuthPerms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuthItemId] int  NOT NULL,
    [AuthPermId] int  NOT NULL,
    [DateLastMaint] datetime  NULL,
    [CreateDate] datetime  NULL
);
GO

-- Creating table 'AuthItems'
CREATE TABLE [dbo].[AuthItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [DateLastMaint] datetime  NULL,
    [CreateDate] datetime  NULL
);
GO

-- Creating table 'AuthPerms'
CREATE TABLE [dbo].[AuthPerms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [PermWeight] nvarchar(max)  NOT NULL,
    [DateLastMaint] datetime  NULL,
    [CreateDate] datetime  NULL
);
GO

-- Creating table 'AuthRolAuthItemAuthPerms'
CREATE TABLE [dbo].[AuthRolAuthItemAuthPerms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuthRolId] int  NOT NULL,
    [AuthRolAuthRolId] int  NOT NULL,
    [AuthItemAuthPermId] int  NOT NULL,
    [DateLastMaint] datetime  NULL,
    [CreateDate] datetime  NULL
);
GO

-- Creating table 'AuthRols'
CREATE TABLE [dbo].[AuthRols] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [EffDateTime] decimal(18,0)  NOT NULL,
    [InactiveDate] datetime  NULL,
    [SignonIdReqdYN] nvarchar(max)  NULL,
    [DateLastMaint] datetime  NULL,
    [AuthRoleTypId] int  NOT NULL,
    [CreateDate] datetime  NULL
);
GO

-- Creating table 'AuthRolTyps'
CREATE TABLE [dbo].[AuthRolTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateLastMaint] datetime  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [CreateDate] datetime  NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ContactTypId] int  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [Ext] nvarchar(max)  NULL
);
GO

-- Creating table 'ContactTyps'
CREATE TABLE [dbo].[ContactTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DisplayName] nvarchar(max)  NULL,
    [Descript] nvarchar(max)  NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [ContactTypCd] nvarchar(max)  NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL
);
GO

-- Creating table 'soUploads'
CREATE TABLE [dbo].[soUploads] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [Location] nvarchar(max)  NULL,
    [soPages] int  NULL,
    [FileTypeId] int  NULL,
    [OrgId] int  NULL,
    [FileExt] nvarchar(max)  NULL,
    [OrgDocTypId] int  NULL,
    [soMethod] nvarchar(max)  NULL,
    [soWorkstation] nvarchar(max)  NULL,
    [soUserData] nvarchar(max)  NULL,
    [soFileName] nvarchar(max)  NULL,
    [soFormType] nvarchar(max)  NULL,
    [soUploadApp] nvarchar(max)  NULL,
    [soUploadAppVersion] nvarchar(max)  NULL,
    [soStorageType] nvarchar(max)  NULL,
    [soStorageLocation] nvarchar(max)  NULL,
    [soStorageContainer] nvarchar(max)  NULL,
    [soStorageKey] nvarchar(max)  NULL,
    [soActivitiesKey] uniqueidentifier  NULL,
    [soActivityTime] datetime  NULL,
    [soUploadTime] datetime  NULL,
    [soUploadDurationMS] bigint  NULL,
    [soImportTime] datetime  NULL,
    [soDeletedTime] datetime  NULL,
    [soCompletedTime] datetime  NULL,
    [soSyncTime] datetime  NULL,
    [soTest] bit  NULL,
    [soUserID] nvarchar(max)  NULL,
    [soKey] uniqueidentifier  NOT NULL,
    [soParentUploadsKey] uniqueidentifier  NULL,
    [soParentOrganizationsKey] uniqueidentifier  NULL,
    [soOrganizationsKey] uniqueidentifier  NULL,
    [soItems] int  NULL,
    [soFormTypesKey] uniqueidentifier  NULL,
    [soCustomerID] nvarchar(max)  NULL,
    [LockID] nvarchar(max)  NULL,
    [soUsersKey] uniqueidentifier  NULL,
    [BlankCount] int  NULL,
    [SepCount] int  NULL,
    [Seq] int  NULL,
    [Destination] nvarchar(max)  NULL,
    [StartIndex] int  NULL,
    [EndIndex] int  NULL,
    [SLAInHours] int  NULL
);
GO

-- Creating table 'ElmElms'
CREATE TABLE [dbo].[ElmElms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ElmId] int  NOT NULL,
    [AssociatedElmId] int  NOT NULL,
    [AuthItemId] int  NULL
);
GO

-- Creating table 'Elms'
CREATE TABLE [dbo].[Elms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ElmTypId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [AuthItemId] int  NULL
);
GO

-- Creating table 'ElmTyps'
CREATE TABLE [dbo].[ElmTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypCd] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'EntityAttributeTyps'
CREATE TABLE [dbo].[EntityAttributeTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TypCd] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EquipTyps'
CREATE TABLE [dbo].[EquipTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [EquipTypCd] nvarchar(max)  NULL
);
GO

-- Creating table 'Genders'
CREATE TABLE [dbo].[Genders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GenderCd] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [IconVisualId] int  NULL
);
GO

-- Creating table 'IconVisuals'
CREATE TABLE [dbo].[IconVisuals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IconPath] nvarchar(max)  NULL,
    [Descript] nvarchar(max)  NULL,
    [Content] nvarchar(max)  NOT NULL,
    [IconVisualTypId] int  NULL
);
GO

-- Creating table 'IconVisualTyps'
CREATE TABLE [dbo].[IconVisualTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LayoutTyps'
CREATE TABLE [dbo].[LayoutTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'Localities'
CREATE TABLE [dbo].[Localities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [CountryCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Locns'
CREATE TABLE [dbo].[Locns] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AddressLine1] nvarchar(max)  NULL,
    [AddressLine2] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [State] nvarchar(max)  NULL,
    [ZipCode] nvarchar(max)  NULL,
    [GeoLocationData] nvarchar(max)  NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [CountryId] int  NULL,
    [Latitude] nvarchar(max)  NULL,
    [Longitude] nvarchar(max)  NULL
);
GO

-- Creating table 'LogCats'
CREATE TABLE [dbo].[LogCats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [IconVisualId] int  NULL
);
GO

-- Creating table 'LogEntries'
CREATE TABLE [dbo].[LogEntries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ErrorCode] nvarchar(max)  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [CreateDate] nvarchar(max)  NOT NULL,
    [Application] nvarchar(max)  NULL,
    [LogCatId] int  NOT NULL
);
GO

-- Creating table 'MenuItems'
CREATE TABLE [dbo].[MenuItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MenuItemId] int  NOT NULL,
    [DisplayName] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [SeqNbr] int  NULL,
    [NavURL] nvarchar(max)  NULL,
    [State] nvarchar(max)  NULL,
    [IconVisualId] int  NULL,
    [AuthItemId] int  NULL,
    [TemplateUrl] nvarchar(max)  NULL,
    [MenuId] int  NULL,
    [ParentMenuItemId] int  NULL,
    [ModStateId] int  NULL
);
GO

-- Creating table 'Menus'
CREATE TABLE [dbo].[Menus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [IconVisualId] int  NULL,
    [AuthItemId] int  NULL
);
GO

-- Creating table 'ModAuthRols'
CREATE TABLE [dbo].[ModAuthRols] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModId] int  NOT NULL,
    [AuthRolId] int  NOT NULL
);
GO

-- Creating table 'ModelEntities'
CREATE TABLE [dbo].[ModelEntities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [AuthItemId] int  NULL
);
GO

-- Creating table 'ModelEntityAttributes'
CREATE TABLE [dbo].[ModelEntityAttributes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModelEntityId] int  NOT NULL,
    [EntityAttributeTypId] int  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [AuthItemId] int  NULL
);
GO

-- Creating table 'ModElmElms'
CREATE TABLE [dbo].[ModElmElms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModElmId] int  NOT NULL,
    [ElmId] int  NOT NULL,
    [AuthItemId] int  NULL
);
GO

-- Creating table 'ModElms'
CREATE TABLE [dbo].[ModElms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModId] int  NOT NULL,
    [ElmId] int  NOT NULL,
    [DefaultRoute] nvarchar(max)  NULL,
    [AuthItemId] int  NULL,
    [ModElmTypId] int  NOT NULL
);
GO

-- Creating table 'ModElmTyps'
CREATE TABLE [dbo].[ModElmTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TypeCd] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'Mods'
CREATE TABLE [dbo].[Mods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [Version] nvarchar(max)  NOT NULL,
    [ModTypId] int  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [DisplayName] nvarchar(max)  NULL,
    [AuthItemId] int  NULL
);
GO

-- Creating table 'ModSettings'
CREATE TABLE [dbo].[ModSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SettingId] int  NOT NULL,
    [ModId] int  NOT NULL
);
GO

-- Creating table 'ModSettingValues'
CREATE TABLE [dbo].[ModSettingValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModSettingId] int  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ModStates'
CREATE TABLE [dbo].[ModStates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DefaultUrl] nvarchar(max)  NULL,
    [ControllerName] nvarchar(max)  NULL,
    [TemplateUrl] nvarchar(max)  NULL,
    [Template] nvarchar(max)  NULL,
    [IsDefaultState] bit  NULL
);
GO

-- Creating table 'ModTyps'
CREATE TABLE [dbo].[ModTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrgContacts'
CREATE TABLE [dbo].[OrgContacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [ContactId] int  NOT NULL
);
GO

-- Creating table 'OrgCusts'
CREATE TABLE [dbo].[OrgCusts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Org_Id] int  NOT NULL,
    [SubmissionOpts] smallint  NULL,
    [RemoveBlank] bit  NULL,
    [SLA] int  NULL
);
GO

-- Creating table 'OrgLocns'
CREATE TABLE [dbo].[OrgLocns] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [LocnId] int  NOT NULL
);
GO

-- Creating table 'OrgNotes'
CREATE TABLE [dbo].[OrgNotes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [NoteText] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'OrgOrgs'
CREATE TABLE [dbo].[OrgOrgs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [AssociatedOrgId] int  NOT NULL
);
GO

-- Creating table 'Orgs'
CREATE TABLE [dbo].[Orgs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [CreateDate] datetime  NULL,
    [CreatedUserId] int  NOT NULL,
    [XRefNbr] nvarchar(max)  NULL,
    [OrgTypId] int  NOT NULL,
    [soKey] uniqueidentifier  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [ModifiedUserId] int  NULL,
    [ApprovedDate] datetime  NULL,
    [ApprovedUserId] int  NULL,
    [soOrgType] nvarchar(max)  NULL,
    [soTaxExempt] bit  NULL,
    [Comments] nvarchar(max)  NULL,
    [Logo] varbinary(max)  NULL,
    [Agreement] varbinary(max)  NULL,
    [BillingInfo] bit  NULL,
    [GotAgreement] bit  NULL,
    [InactiveDate] datetime  NULL,
    [InactiveUserId] int  NULL,
    [SOAccountNbr] nvarchar(max)  NULL,
    [OtherAccountNbr] nvarchar(max)  NULL,
    [ImageCleanUp] bit  NULL,
    [InviteDate] datetime  NULL,
    [SOW] bit  NULL,
    [PromoCode] nvarchar(max)  NULL,
    [BillMe] bit  NULL,
    [soTest] bit  NULL,
    [soDefaultFormTypesKey] uniqueidentifier  NULL,
    [SubEmail] nvarchar(max)  NULL
);
GO

-- Creating table 'OrgStatus'
CREATE TABLE [dbo].[OrgStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgTypId] int  NOT NULL,
    [StatusCd] nvarchar(max)  NULL,
    [DisplayText] nvarchar(max)  NULL
);
GO

-- Creating table 'OrgStatusHists'
CREATE TABLE [dbo].[OrgStatusHists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreateDate] datetime  NULL,
    [OrgId] int  NOT NULL,
    [InactiveDate] datetime  NULL,
    [Comment] nvarchar(max)  NULL,
    [OrgTypOrgStatusId] int  NOT NULL
);
GO

-- Creating table 'OrgTypOrgStatus'
CREATE TABLE [dbo].[OrgTypOrgStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgTypId] int  NOT NULL,
    [OrgStatusId] int  NOT NULL,
    [OrgStatusHistId] int  NOT NULL
);
GO

-- Creating table 'OrgTyps'
CREATE TABLE [dbo].[OrgTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [TypCd] nvarchar(max)  NULL
);
GO

-- Creating table 'PartGrpParts'
CREATE TABLE [dbo].[PartGrpParts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PartId] int  NOT NULL,
    [PartGrpId] int  NOT NULL
);
GO

-- Creating table 'PartGrps'
CREATE TABLE [dbo].[PartGrps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'PartIndicators'
CREATE TABLE [dbo].[PartIndicators] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TooltipText] nvarchar(max)  NULL
);
GO

-- Creating table 'Parts'
CREATE TABLE [dbo].[Parts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [IsEnabled] bit  NOT NULL,
    [PartTypId] int  NULL,
    [PartIndicatorId] int  NULL,
    [ModId] int  NOT NULL
);
GO

-- Creating table 'PartSettings'
CREATE TABLE [dbo].[PartSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PartId] int  NOT NULL,
    [SettingId] int  NOT NULL
);
GO

-- Creating table 'PartTyps'
CREATE TABLE [dbo].[PartTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'PerNotes'
CREATE TABLE [dbo].[PerNotes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PerId] int  NOT NULL,
    [NoteText] nvarchar(max)  NOT NULL,
    [DateLastMaint] datetime  NOT NULL
);
GO

-- Creating table 'Pers'
CREATE TABLE [dbo].[Pers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NULL,
    [Salutation] nvarchar(max)  NULL,
    [Suffix] nvarchar(max)  NULL,
    [DOB] nvarchar(max)  NULL,
    [CreateUserId] int  NULL,
    [Createdate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [LocalityId] int  NULL,
    [GenderId] int  NULL,
    [LocnId] int  NULL,
    [Title] nvarchar(max)  NULL
);
GO

-- Creating table 'PersContacts'
CREATE TABLE [dbo].[PersContacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersId] int  NOT NULL,
    [ContactId] int  NOT NULL
);
GO

-- Creating table 'PlaceHolders'
CREATE TABLE [dbo].[PlaceHolders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SceneId] int  NOT NULL,
    [Row] int  NOT NULL,
    [RowSpan] int  NOT NULL,
    [Column] int  NOT NULL,
    [ColumnSpan] int  NOT NULL
);
GO

-- Creating table 'PortModModElms'
CREATE TABLE [dbo].[PortModModElms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Route] nvarchar(max)  NOT NULL,
    [PortModId] int  NOT NULL,
    [ModElmId] int  NOT NULL,
    [HeaderCmdNbr] bigint  NULL
);
GO

-- Creating table 'PortMods'
CREATE TABLE [dbo].[PortMods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PortId] int  NOT NULL,
    [ModId] int  NOT NULL,
    [IconVisualId] int  NULL,
    [MenuId] int  NULL
);
GO

-- Creating table 'PortModSettings'
CREATE TABLE [dbo].[PortModSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PortModId] int  NOT NULL,
    [SettingId] int  NOT NULL
);
GO

-- Creating table 'PortModSettingValues'
CREATE TABLE [dbo].[PortModSettingValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [PortModSettingId] int  NOT NULL
);
GO

-- Creating table 'Ports'
CREATE TABLE [dbo].[Ports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [Enabled] bit  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL
);
GO

-- Creating table 'PortScenes'
CREATE TABLE [dbo].[PortScenes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PortId] int  NOT NULL,
    [SceneId] int  NOT NULL,
    [Sequence] int  NOT NULL,
    [DefaultScene] bit  NOT NULL
);
GO

-- Creating table 'PortUsers'
CREATE TABLE [dbo].[PortUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PortId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'ScenePartPartSettings'
CREATE TABLE [dbo].[ScenePartPartSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScenePartId] int  NOT NULL,
    [PartSettingId] int  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SceneParts'
CREATE TABLE [dbo].[SceneParts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SceneId] int  NOT NULL,
    [Part_Id] int  NOT NULL
);
GO

-- Creating table 'Scenes'
CREATE TABLE [dbo].[Scenes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LayoutTypId] int  NULL,
    [Rows] int  NOT NULL,
    [Columns] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [DefaultValue] nvarchar(max)  NOT NULL,
    [SettingTypId] int  NOT NULL
);
GO

-- Creating table 'SettingTyps'
CREATE TABLE [dbo].[SettingTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Abbreviation] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserLoginHists'
CREATE TABLE [dbo].[UserLoginHists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LoginDate] datetime  NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'UserModAuthRols'
CREATE TABLE [dbo].[UserModAuthRols] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModAuthRolId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [IsSuperAdmin] bit  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreateUserId] int  NOT NULL,
    [DateLastMaint] datetime  NULL,
    [ModifiedUserId] int  NULL,
    [InactiveDate] datetime  NULL,
    [InactiveUserId] int  NULL,
    [IsProcess] bit  NULL,
    [soKey] uniqueidentifier  NULL,
    [InviteDate] datetime  NULL
);
GO

-- Creating table 'WkflowDefs'
CREATE TABLE [dbo].[WkflowDefs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [WkflowTypId] int  NOT NULL,
    [DateLastMaint] datetime  NULL,
    [CreateDate] datetime  NULL,
    [Version] nvarchar(max)  NULL,
    [Code] nvarchar(max)  NULL
);
GO

-- Creating table 'WkflowDefStatReas'
CREATE TABLE [dbo].[WkflowDefStatReas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WkflowStatReasId] int  NOT NULL,
    [DateLastMaint] datetime  NULL,
    [CreateDate] datetime  NULL,
    [Action] int  NOT NULL,
    [WkflowDefWkflowStatId] int  NOT NULL,
    [NextWkflowDefWkflowStatId] int  NULL
);
GO

-- Creating table 'WkflowDefWkflowStats'
CREATE TABLE [dbo].[WkflowDefWkflowStats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WkflowStatId] int  NOT NULL,
    [WkflowDefId] int  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [Data] int  NULL,
    [Name] nvarchar(max)  NULL,
    [Start] bit  NULL
);
GO

-- Creating table 'WkflowDefWkflowStatWkflowStats'
CREATE TABLE [dbo].[WkflowDefWkflowStatWkflowStats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WkflowDefWkflowStatId] int  NOT NULL,
    [AvailWkflowStatId] int  NOT NULL,
    [HideFromCust] bit  NULL,
    [ShowInSelect] bit  NULL
);
GO

-- Creating table 'WkflowInstanceDocs'
CREATE TABLE [dbo].[WkflowInstanceDocs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WkflowInstanceId] int  NOT NULL,
    [DocId] int  NOT NULL
);
GO

-- Creating table 'WkflowInstances'
CREATE TABLE [dbo].[WkflowInstances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CurrWkflowStatId] int  NOT NULL,
    [WkflowDefId] int  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [AncestorWkflowId] int  NULL,
    [OrgId] int  NULL,
    [UserId] int  NULL,
    [Summary] nvarchar(max)  NULL,
    [Priority] smallint  NULL,
    [CCUserId] int  NULL
);
GO

-- Creating table 'WkflowStatReas'
CREATE TABLE [dbo].[WkflowStatReas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL
);
GO

-- Creating table 'WkflowStats'
CREATE TABLE [dbo].[WkflowStats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL,
    [Color] nvarchar(max)  NULL
);
GO

-- Creating table 'WkflowStepHists'
CREATE TABLE [dbo].[WkflowStepHists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WkflowInstanceId] int  NOT NULL,
    [WkflowStatReasId] int  NULL,
    [CreateDate] datetime  NULL,
    [CreatedUserId] int  NULL,
    [DateLastMaint] datetime  NULL,
    [WkflowStatId] int  NOT NULL
);
GO

-- Creating table 'WkflowStepNotes'
CREATE TABLE [dbo].[WkflowStepNotes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NoteText] nvarchar(max)  NOT NULL,
    [WkflowStepHistId] int  NOT NULL,
    [Order] int  NOT NULL,
    [CreatedDate] datetime  NULL
);
GO

-- Creating table 'WkflowTyps'
CREATE TABLE [dbo].[WkflowTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NULL,
    [DateLastMaint] datetime  NULL
);
GO

-- Creating table 'ZipCodes'
CREATE TABLE [dbo].[ZipCodes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Latitude] nvarchar(max)  NOT NULL,
    [Longitude] nvarchar(max)  NOT NULL,
    [Class] nvarchar(max)  NULL,
    [City] nvarchar(max)  NOT NULL,
    [StateCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrgResellers'
CREATE TABLE [dbo].[OrgResellers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastReviewDate] datetime  NULL,
    [NextReviewDate] datetime  NULL,
    [Org_Id] int  NOT NULL
);
GO

-- Creating table 'ServicePackages'
CREATE TABLE [dbo].[ServicePackages] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'FileTypes'
CREATE TABLE [dbo].[FileTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'DPWorkflows'
CREATE TABLE [dbo].[DPWorkflows] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NULL,
    [UserId] int  NULL,
    [WkflowInstance_Id] int  NOT NULL
);
GO

-- Creating table 'DataElms'
CREATE TABLE [dbo].[DataElms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [DataElmTypId] int  NULL
);
GO

-- Creating table 'OrgDocTyps'
CREATE TABLE [dbo].[OrgDocTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [SubmissionOpt] smallint  NULL,
    [RemoveBlank] bit  NULL,
    [NeedHelp] bit  NULL,
    [SampleImageName] nvarchar(max)  NULL,
    [SampleImage] varbinary(max)  NULL,
    [PageCount] smallint  NULL,
    [Disposition] nvarchar(max)  NULL,
    [RouteToBW] bit  NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedUserId] int  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [ModifiedUserId] int  NULL,
    [InactiveDate] datetime  NULL,
    [InactivatedUserId] int  NULL,
    [Approved] bit  NULL,
    [soKey] uniqueidentifier  NOT NULL,
    [ParentId] int  NULL,
    [ImageCleanUp] bit  NULL,
    [Show] bit  NOT NULL,
    [SLAInHours] int  NULL,
    [SepSheetImg] varbinary(max)  NULL,
    [CurrStatusId] int  NOT NULL,
    [InActiveReason] nvarchar(max)  NULL,
    [WkflowInstanceId] int  NULL
);
GO

-- Creating table 'DataElmTyps'
CREATE TABLE [dbo].[DataElmTyps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [DataElmTypCd] nvarchar(max)  NULL
);
GO

-- Creating table 'OrgDocTypDataElms'
CREATE TABLE [dbo].[OrgDocTypDataElms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgDocTypId] int  NOT NULL,
    [DataElmId] int  NOT NULL,
    [InActiveDate] datetime  NULL,
    [PageNbr] smallint  NOT NULL,
    [DoubleKey] bit  NULL,
    [IndexField] bit  NULL
);
GO

-- Creating table 'OrgUsers'
CREATE TABLE [dbo].[OrgUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [Type] nvarchar(max)  NULL
);
GO

-- Creating table 'UserAuthRols'
CREATE TABLE [dbo].[UserAuthRols] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuthRolId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Packages'
CREATE TABLE [dbo].[Packages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [InActiveDate] datetime  NULL,
    [TotalClickCount] bigint  NOT NULL,
    [Price] float  NOT NULL
);
GO

-- Creating table 'OrgPackages'
CREATE TABLE [dbo].[OrgPackages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PackageId] int  NOT NULL,
    [OrgId] int  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [InActiveDate] datetime  NULL,
    [AutoRenewal] bit  NOT NULL,
    [RenewalDate] datetime  NULL
);
GO

-- Creating table 'MessageTemplates'
CREATE TABLE [dbo].[MessageTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [TemplateText] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [InactiveDate] datetime  NULL,
    [PortId] int  NOT NULL,
    [HeaderText] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DeliveryMethods'
CREATE TABLE [dbo].[DeliveryMethods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [InactiveDate] datetime  NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MessageBody] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [DeliveredDate] datetime  NULL,
    [WkflowInstanceId] int  NULL,
    [HeaderText] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MessageUsers'
CREATE TABLE [dbo].[MessageUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MessageId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [ReadDate] datetime  NULL,
    [DeliveryMethodId] int  NOT NULL
);
GO

-- Creating table 'OrgPackageHists'
CREATE TABLE [dbo].[OrgPackageHists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgPackageId] int  NOT NULL,
    [TotalClickCount] int  NOT NULL,
    [UsedClickCount] int  NULL,
    [RemainingClickCount] int  NULL,
    [CreateDate] datetime  NOT NULL,
    [Price] float  NOT NULL,
    [InactiveDate] datetime  NULL
);
GO

-- Creating table 'Holidays'
CREATE TABLE [dbo].[Holidays] (
    [soKey] uniqueidentifier  NOT NULL,
    [TheDay] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'SOActivities'
CREATE TABLE [dbo].[SOActivities] (
    [soKey] uniqueidentifier  NOT NULL,
    [soActivity] nvarchar(max)  NOT NULL,
    [soTitle] nvarchar(max)  NOT NULL,
    [soShowCustomer] bit  NOT NULL
);
GO

-- Creating table 'DocTypStatus'
CREATE TABLE [dbo].[DocTypStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StatusCd] nvarchar(max)  NULL,
    [DisplayText] nvarchar(max)  NULL,
    [Show] bit  NULL
);
GO

-- Creating table 'PortSettingValues'
CREATE TABLE [dbo].[PortSettingValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NULL,
    [PortSettingId] int  NOT NULL
);
GO

-- Creating table 'PortSettings'
CREATE TABLE [dbo].[PortSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SettingId] int  NOT NULL,
    [PortId] int  NOT NULL
);
GO

-- Creating table 'SowWkflows'
CREATE TABLE [dbo].[SowWkflows] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descript] nvarchar(max)  NULL,
    [ProjectName] nvarchar(max)  NULL,
    [MonthlyCommitment] decimal(18,0)  NULL,
    [EffectiveDate] datetime  NULL,
    [InactiveDate] datetime  NULL,
    [LastReviewDate] datetime  NOT NULL,
    [NextReviewDate] datetime  NOT NULL,
    [WkflowInstanceId] int  NOT NULL,
    [Amend] smallint  NOT NULL,
    [PDFDoc] varbinary(max)  NULL
);
GO

-- Creating table 'SowAttributes'
CREATE TABLE [dbo].[SowAttributes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'SowAttributeValueHists'
CREATE TABLE [dbo].[SowAttributeValueHists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Qty] int  NULL,
    [UnitPrice] decimal(18,0)  NULL,
    [ExtendedPrice] decimal(18,0)  NULL,
    [EffectiveDate] datetime  NULL,
    [InactiveDate] datetime  NULL,
    [SowWklowSowAttributeId] int  NOT NULL
);
GO

-- Creating table 'SowWklowSowAttributes'
CREATE TABLE [dbo].[SowWklowSowAttributes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SowAttributeId] int  NOT NULL,
    [SowWkflowId] int  NOT NULL
);
GO

-- Creating table 'SowWkflowDocSetups'
CREATE TABLE [dbo].[SowWkflowDocSetups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SowWkflowId] int  NOT NULL,
    [DocumentName] nvarchar(max)  NULL,
    [NoIndexes] int  NULL,
    [NoDataFields] int  NULL,
    [ListPrice] decimal(18,0)  NULL,
    [Volume] int  NULL,
    [SLA] int  NOT NULL,
    [OrgDocTypId] int  NULL,
    [SubEmail] nvarchar(max)  NULL
);
GO

-- Creating table 'OrgResellerDiscHists'
CREATE TABLE [dbo].[OrgResellerDiscHists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AgreementNum] int  NOT NULL,
    [Amend] int  NOT NULL,
    [AnnualRevenue] decimal(18,0)  NOT NULL,
    [Discount] decimal(18,0)  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [InActiveDate] datetime  NULL,
    [OrgResellerId] int  NOT NULL,
    [PDFDoc] varbinary(max)  NULL
);
GO

-- Creating table 'SOWWkflowOrgResellerDiscOverrides'
CREATE TABLE [dbo].[SOWWkflowOrgResellerDiscOverrides] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SowWkflowId] int  NOT NULL,
    [OrgResellerId] int  NOT NULL,
    [Discount] decimal(18,0)  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [InactivateDate] datetime  NOT NULL
);
GO

-- Creating table 'Months'
CREATE TABLE [dbo].[Months] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrgDocTypMonths'
CREATE TABLE [dbo].[OrgDocTypMonths] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [MonthId] int  NOT NULL,
    [OrgDocTypId] int  NOT NULL,
    [Images] int  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Revenue] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'SubWhiteLists'
CREATE TABLE [dbo].[SubWhiteLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [Address] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrgMonthCommitments'
CREATE TABLE [dbo].[OrgMonthCommitments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MonthId] int  NOT NULL,
    [OrgId] int  NOT NULL,
    [Commitment] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'OrgDocTypDailyUploads'
CREATE TABLE [dbo].[OrgDocTypDailyUploads] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrgId] int  NOT NULL,
    [OrgDocTypId] int  NOT NULL,
    [Day] datetime  NOT NULL,
    [Images] int  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Revenue] decimal(18,0)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Attribs'
ALTER TABLE [dbo].[Attribs]
ADD CONSTRAINT [PK_Attribs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AttribTyps'
ALTER TABLE [dbo].[AttribTyps]
ADD CONSTRAINT [PK_AttribTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AuditActionCd] in table 'AuditActions'
ALTER TABLE [dbo].[AuditActions]
ADD CONSTRAINT [PK_AuditActions]
    PRIMARY KEY CLUSTERED ([AuditActionCd] ASC);
GO

-- Creating primary key on [AuditCatCd], [AuditTypCd] in table 'AuditCatAuditTyps'
ALTER TABLE [dbo].[AuditCatAuditTyps]
ADD CONSTRAINT [PK_AuditCatAuditTyps]
    PRIMARY KEY CLUSTERED ([AuditCatCd], [AuditTypCd] ASC);
GO

-- Creating primary key on [AuditCatCd] in table 'AuditCats'
ALTER TABLE [dbo].[AuditCats]
ADD CONSTRAINT [PK_AuditCats]
    PRIMARY KEY CLUSTERED ([AuditCatCd] ASC);
GO

-- Creating primary key on [AuditNbr], [DtlNbr] in table 'AuditDtls'
ALTER TABLE [dbo].[AuditDtls]
ADD CONSTRAINT [PK_AuditDtls]
    PRIMARY KEY CLUSTERED ([AuditNbr], [DtlNbr] ASC);
GO

-- Creating primary key on [AuditNbr] in table 'Audits'
ALTER TABLE [dbo].[Audits]
ADD CONSTRAINT [PK_Audits]
    PRIMARY KEY CLUSTERED ([AuditNbr] ASC);
GO

-- Creating primary key on [AuditTypCd] in table 'AuditTyps'
ALTER TABLE [dbo].[AuditTyps]
ADD CONSTRAINT [PK_AuditTyps]
    PRIMARY KEY CLUSTERED ([AuditTypCd] ASC);
GO

-- Creating primary key on [Id] in table 'AuthItemAuthPerms'
ALTER TABLE [dbo].[AuthItemAuthPerms]
ADD CONSTRAINT [PK_AuthItemAuthPerms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuthItems'
ALTER TABLE [dbo].[AuthItems]
ADD CONSTRAINT [PK_AuthItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuthPerms'
ALTER TABLE [dbo].[AuthPerms]
ADD CONSTRAINT [PK_AuthPerms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuthRolAuthItemAuthPerms'
ALTER TABLE [dbo].[AuthRolAuthItemAuthPerms]
ADD CONSTRAINT [PK_AuthRolAuthItemAuthPerms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuthRols'
ALTER TABLE [dbo].[AuthRols]
ADD CONSTRAINT [PK_AuthRols]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuthRolTyps'
ALTER TABLE [dbo].[AuthRolTyps]
ADD CONSTRAINT [PK_AuthRolTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContactTyps'
ALTER TABLE [dbo].[ContactTyps]
ADD CONSTRAINT [PK_ContactTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'soUploads'
ALTER TABLE [dbo].[soUploads]
ADD CONSTRAINT [PK_soUploads]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ElmElms'
ALTER TABLE [dbo].[ElmElms]
ADD CONSTRAINT [PK_ElmElms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Elms'
ALTER TABLE [dbo].[Elms]
ADD CONSTRAINT [PK_Elms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ElmTyps'
ALTER TABLE [dbo].[ElmTyps]
ADD CONSTRAINT [PK_ElmTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EntityAttributeTyps'
ALTER TABLE [dbo].[EntityAttributeTyps]
ADD CONSTRAINT [PK_EntityAttributeTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EquipTyps'
ALTER TABLE [dbo].[EquipTyps]
ADD CONSTRAINT [PK_EquipTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [PK_Genders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IconVisuals'
ALTER TABLE [dbo].[IconVisuals]
ADD CONSTRAINT [PK_IconVisuals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IconVisualTyps'
ALTER TABLE [dbo].[IconVisualTyps]
ADD CONSTRAINT [PK_IconVisualTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LayoutTyps'
ALTER TABLE [dbo].[LayoutTyps]
ADD CONSTRAINT [PK_LayoutTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Localities'
ALTER TABLE [dbo].[Localities]
ADD CONSTRAINT [PK_Localities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locns'
ALTER TABLE [dbo].[Locns]
ADD CONSTRAINT [PK_Locns]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LogCats'
ALTER TABLE [dbo].[LogCats]
ADD CONSTRAINT [PK_LogCats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [PK_LogEntries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [PK_MenuItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Menus'
ALTER TABLE [dbo].[Menus]
ADD CONSTRAINT [PK_Menus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModAuthRols'
ALTER TABLE [dbo].[ModAuthRols]
ADD CONSTRAINT [PK_ModAuthRols]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModelEntities'
ALTER TABLE [dbo].[ModelEntities]
ADD CONSTRAINT [PK_ModelEntities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModelEntityAttributes'
ALTER TABLE [dbo].[ModelEntityAttributes]
ADD CONSTRAINT [PK_ModelEntityAttributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModElmElms'
ALTER TABLE [dbo].[ModElmElms]
ADD CONSTRAINT [PK_ModElmElms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModElms'
ALTER TABLE [dbo].[ModElms]
ADD CONSTRAINT [PK_ModElms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModElmTyps'
ALTER TABLE [dbo].[ModElmTyps]
ADD CONSTRAINT [PK_ModElmTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mods'
ALTER TABLE [dbo].[Mods]
ADD CONSTRAINT [PK_Mods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModSettings'
ALTER TABLE [dbo].[ModSettings]
ADD CONSTRAINT [PK_ModSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModSettingValues'
ALTER TABLE [dbo].[ModSettingValues]
ADD CONSTRAINT [PK_ModSettingValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModStates'
ALTER TABLE [dbo].[ModStates]
ADD CONSTRAINT [PK_ModStates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModTyps'
ALTER TABLE [dbo].[ModTyps]
ADD CONSTRAINT [PK_ModTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgContacts'
ALTER TABLE [dbo].[OrgContacts]
ADD CONSTRAINT [PK_OrgContacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgCusts'
ALTER TABLE [dbo].[OrgCusts]
ADD CONSTRAINT [PK_OrgCusts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgLocns'
ALTER TABLE [dbo].[OrgLocns]
ADD CONSTRAINT [PK_OrgLocns]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgNotes'
ALTER TABLE [dbo].[OrgNotes]
ADD CONSTRAINT [PK_OrgNotes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgOrgs'
ALTER TABLE [dbo].[OrgOrgs]
ADD CONSTRAINT [PK_OrgOrgs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orgs'
ALTER TABLE [dbo].[Orgs]
ADD CONSTRAINT [PK_Orgs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgStatus'
ALTER TABLE [dbo].[OrgStatus]
ADD CONSTRAINT [PK_OrgStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgStatusHists'
ALTER TABLE [dbo].[OrgStatusHists]
ADD CONSTRAINT [PK_OrgStatusHists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgTypOrgStatus'
ALTER TABLE [dbo].[OrgTypOrgStatus]
ADD CONSTRAINT [PK_OrgTypOrgStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgTyps'
ALTER TABLE [dbo].[OrgTyps]
ADD CONSTRAINT [PK_OrgTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartGrpParts'
ALTER TABLE [dbo].[PartGrpParts]
ADD CONSTRAINT [PK_PartGrpParts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartGrps'
ALTER TABLE [dbo].[PartGrps]
ADD CONSTRAINT [PK_PartGrps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartIndicators'
ALTER TABLE [dbo].[PartIndicators]
ADD CONSTRAINT [PK_PartIndicators]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Parts'
ALTER TABLE [dbo].[Parts]
ADD CONSTRAINT [PK_Parts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartSettings'
ALTER TABLE [dbo].[PartSettings]
ADD CONSTRAINT [PK_PartSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartTyps'
ALTER TABLE [dbo].[PartTyps]
ADD CONSTRAINT [PK_PartTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PerNotes'
ALTER TABLE [dbo].[PerNotes]
ADD CONSTRAINT [PK_PerNotes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pers'
ALTER TABLE [dbo].[Pers]
ADD CONSTRAINT [PK_Pers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersContacts'
ALTER TABLE [dbo].[PersContacts]
ADD CONSTRAINT [PK_PersContacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlaceHolders'
ALTER TABLE [dbo].[PlaceHolders]
ADD CONSTRAINT [PK_PlaceHolders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PortModModElms'
ALTER TABLE [dbo].[PortModModElms]
ADD CONSTRAINT [PK_PortModModElms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PortMods'
ALTER TABLE [dbo].[PortMods]
ADD CONSTRAINT [PK_PortMods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PortModSettings'
ALTER TABLE [dbo].[PortModSettings]
ADD CONSTRAINT [PK_PortModSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PortModSettingValues'
ALTER TABLE [dbo].[PortModSettingValues]
ADD CONSTRAINT [PK_PortModSettingValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ports'
ALTER TABLE [dbo].[Ports]
ADD CONSTRAINT [PK_Ports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PortScenes'
ALTER TABLE [dbo].[PortScenes]
ADD CONSTRAINT [PK_PortScenes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PortUsers'
ALTER TABLE [dbo].[PortUsers]
ADD CONSTRAINT [PK_PortUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScenePartPartSettings'
ALTER TABLE [dbo].[ScenePartPartSettings]
ADD CONSTRAINT [PK_ScenePartPartSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SceneParts'
ALTER TABLE [dbo].[SceneParts]
ADD CONSTRAINT [PK_SceneParts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Scenes'
ALTER TABLE [dbo].[Scenes]
ADD CONSTRAINT [PK_Scenes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SettingTyps'
ALTER TABLE [dbo].[SettingTyps]
ADD CONSTRAINT [PK_SettingTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserLoginHists'
ALTER TABLE [dbo].[UserLoginHists]
ADD CONSTRAINT [PK_UserLoginHists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserModAuthRols'
ALTER TABLE [dbo].[UserModAuthRols]
ADD CONSTRAINT [PK_UserModAuthRols]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowDefs'
ALTER TABLE [dbo].[WkflowDefs]
ADD CONSTRAINT [PK_WkflowDefs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowDefStatReas'
ALTER TABLE [dbo].[WkflowDefStatReas]
ADD CONSTRAINT [PK_WkflowDefStatReas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowDefWkflowStats'
ALTER TABLE [dbo].[WkflowDefWkflowStats]
ADD CONSTRAINT [PK_WkflowDefWkflowStats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowDefWkflowStatWkflowStats'
ALTER TABLE [dbo].[WkflowDefWkflowStatWkflowStats]
ADD CONSTRAINT [PK_WkflowDefWkflowStatWkflowStats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowInstanceDocs'
ALTER TABLE [dbo].[WkflowInstanceDocs]
ADD CONSTRAINT [PK_WkflowInstanceDocs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowInstances'
ALTER TABLE [dbo].[WkflowInstances]
ADD CONSTRAINT [PK_WkflowInstances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowStatReas'
ALTER TABLE [dbo].[WkflowStatReas]
ADD CONSTRAINT [PK_WkflowStatReas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowStats'
ALTER TABLE [dbo].[WkflowStats]
ADD CONSTRAINT [PK_WkflowStats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowStepHists'
ALTER TABLE [dbo].[WkflowStepHists]
ADD CONSTRAINT [PK_WkflowStepHists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowStepNotes'
ALTER TABLE [dbo].[WkflowStepNotes]
ADD CONSTRAINT [PK_WkflowStepNotes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WkflowTyps'
ALTER TABLE [dbo].[WkflowTyps]
ADD CONSTRAINT [PK_WkflowTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ZipCodes'
ALTER TABLE [dbo].[ZipCodes]
ADD CONSTRAINT [PK_ZipCodes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgResellers'
ALTER TABLE [dbo].[OrgResellers]
ADD CONSTRAINT [PK_OrgResellers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ServicePackages'
ALTER TABLE [dbo].[ServicePackages]
ADD CONSTRAINT [PK_ServicePackages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileTypes'
ALTER TABLE [dbo].[FileTypes]
ADD CONSTRAINT [PK_FileTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DPWorkflows'
ALTER TABLE [dbo].[DPWorkflows]
ADD CONSTRAINT [PK_DPWorkflows]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DataElms'
ALTER TABLE [dbo].[DataElms]
ADD CONSTRAINT [PK_DataElms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgDocTyps'
ALTER TABLE [dbo].[OrgDocTyps]
ADD CONSTRAINT [PK_OrgDocTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DataElmTyps'
ALTER TABLE [dbo].[DataElmTyps]
ADD CONSTRAINT [PK_DataElmTyps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgDocTypDataElms'
ALTER TABLE [dbo].[OrgDocTypDataElms]
ADD CONSTRAINT [PK_OrgDocTypDataElms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgUsers'
ALTER TABLE [dbo].[OrgUsers]
ADD CONSTRAINT [PK_OrgUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserAuthRols'
ALTER TABLE [dbo].[UserAuthRols]
ADD CONSTRAINT [PK_UserAuthRols]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [PK_Packages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgPackages'
ALTER TABLE [dbo].[OrgPackages]
ADD CONSTRAINT [PK_OrgPackages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageTemplates'
ALTER TABLE [dbo].[MessageTemplates]
ADD CONSTRAINT [PK_MessageTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeliveryMethods'
ALTER TABLE [dbo].[DeliveryMethods]
ADD CONSTRAINT [PK_DeliveryMethods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageUsers'
ALTER TABLE [dbo].[MessageUsers]
ADD CONSTRAINT [PK_MessageUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgPackageHists'
ALTER TABLE [dbo].[OrgPackageHists]
ADD CONSTRAINT [PK_OrgPackageHists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [soKey] in table 'Holidays'
ALTER TABLE [dbo].[Holidays]
ADD CONSTRAINT [PK_Holidays]
    PRIMARY KEY CLUSTERED ([soKey] ASC);
GO

-- Creating primary key on [soKey] in table 'SOActivities'
ALTER TABLE [dbo].[SOActivities]
ADD CONSTRAINT [PK_SOActivities]
    PRIMARY KEY CLUSTERED ([soKey] ASC);
GO

-- Creating primary key on [Id] in table 'DocTypStatus'
ALTER TABLE [dbo].[DocTypStatus]
ADD CONSTRAINT [PK_DocTypStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PortSettingValues'
ALTER TABLE [dbo].[PortSettingValues]
ADD CONSTRAINT [PK_PortSettingValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PortSettings'
ALTER TABLE [dbo].[PortSettings]
ADD CONSTRAINT [PK_PortSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SowWkflows'
ALTER TABLE [dbo].[SowWkflows]
ADD CONSTRAINT [PK_SowWkflows]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SowAttributes'
ALTER TABLE [dbo].[SowAttributes]
ADD CONSTRAINT [PK_SowAttributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SowAttributeValueHists'
ALTER TABLE [dbo].[SowAttributeValueHists]
ADD CONSTRAINT [PK_SowAttributeValueHists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SowWklowSowAttributes'
ALTER TABLE [dbo].[SowWklowSowAttributes]
ADD CONSTRAINT [PK_SowWklowSowAttributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SowWkflowDocSetups'
ALTER TABLE [dbo].[SowWkflowDocSetups]
ADD CONSTRAINT [PK_SowWkflowDocSetups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgResellerDiscHists'
ALTER TABLE [dbo].[OrgResellerDiscHists]
ADD CONSTRAINT [PK_OrgResellerDiscHists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SOWWkflowOrgResellerDiscOverrides'
ALTER TABLE [dbo].[SOWWkflowOrgResellerDiscOverrides]
ADD CONSTRAINT [PK_SOWWkflowOrgResellerDiscOverrides]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Months'
ALTER TABLE [dbo].[Months]
ADD CONSTRAINT [PK_Months]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgDocTypMonths'
ALTER TABLE [dbo].[OrgDocTypMonths]
ADD CONSTRAINT [PK_OrgDocTypMonths]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubWhiteLists'
ALTER TABLE [dbo].[SubWhiteLists]
ADD CONSTRAINT [PK_SubWhiteLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrgMonthCommitments'
ALTER TABLE [dbo].[OrgMonthCommitments]
ADD CONSTRAINT [PK_OrgMonthCommitments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Price] in table 'OrgDocTypDailyUploads'
ALTER TABLE [dbo].[OrgDocTypDailyUploads]
ADD CONSTRAINT [PK_OrgDocTypDailyUploads]
    PRIMARY KEY CLUSTERED ([Id], [Price] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AttribTypId] in table 'Attribs'
ALTER TABLE [dbo].[Attribs]
ADD CONSTRAINT [FK_AttribAttribTyp]
    FOREIGN KEY ([AttribTypId])
    REFERENCES [dbo].[AttribTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AttribAttribTyp'
CREATE INDEX [IX_FK_AttribAttribTyp]
ON [dbo].[Attribs]
    ([AttribTypId]);
GO

-- Creating foreign key on [AuditActionCd] in table 'AuditDtls'
ALTER TABLE [dbo].[AuditDtls]
ADD CONSTRAINT [FK_AuditDtl_AuditAction]
    FOREIGN KEY ([AuditActionCd])
    REFERENCES [dbo].[AuditActions]
        ([AuditActionCd])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuditDtl_AuditAction'
CREATE INDEX [IX_FK_AuditDtl_AuditAction]
ON [dbo].[AuditDtls]
    ([AuditActionCd]);
GO

-- Creating foreign key on [AuditCatCd], [AuditTypCd] in table 'Audits'
ALTER TABLE [dbo].[Audits]
ADD CONSTRAINT [FK_Audit_AuditCatAuditTyp]
    FOREIGN KEY ([AuditCatCd], [AuditTypCd])
    REFERENCES [dbo].[AuditCatAuditTyps]
        ([AuditCatCd], [AuditTypCd])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Audit_AuditCatAuditTyp'
CREATE INDEX [IX_FK_Audit_AuditCatAuditTyp]
ON [dbo].[Audits]
    ([AuditCatCd], [AuditTypCd]);
GO

-- Creating foreign key on [AuditCatCd] in table 'AuditCatAuditTyps'
ALTER TABLE [dbo].[AuditCatAuditTyps]
ADD CONSTRAINT [FK_AuditCatAuditTyp_AuditCat]
    FOREIGN KEY ([AuditCatCd])
    REFERENCES [dbo].[AuditCats]
        ([AuditCatCd])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AuditTypCd] in table 'AuditCatAuditTyps'
ALTER TABLE [dbo].[AuditCatAuditTyps]
ADD CONSTRAINT [FK_AuditCatAuditTyp_AuditTyp]
    FOREIGN KEY ([AuditTypCd])
    REFERENCES [dbo].[AuditTyps]
        ([AuditTypCd])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuditCatAuditTyp_AuditTyp'
CREATE INDEX [IX_FK_AuditCatAuditTyp_AuditTyp]
ON [dbo].[AuditCatAuditTyps]
    ([AuditTypCd]);
GO

-- Creating foreign key on [AuditNbr] in table 'AuditDtls'
ALTER TABLE [dbo].[AuditDtls]
ADD CONSTRAINT [FK_AuditDtl_Audit]
    FOREIGN KEY ([AuditNbr])
    REFERENCES [dbo].[Audits]
        ([AuditNbr])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AncestorAuditNbr] in table 'Audits'
ALTER TABLE [dbo].[Audits]
ADD CONSTRAINT [FK_Audit_AncestorAudit]
    FOREIGN KEY ([AncestorAuditNbr])
    REFERENCES [dbo].[Audits]
        ([AuditNbr])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Audit_AncestorAudit'
CREATE INDEX [IX_FK_Audit_AncestorAudit]
ON [dbo].[Audits]
    ([AncestorAuditNbr]);
GO

-- Creating foreign key on [AuthItemId] in table 'AuthItemAuthPerms'
ALTER TABLE [dbo].[AuthItemAuthPerms]
ADD CONSTRAINT [FK_AuthItemAuthItemAuthPerm]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthItemAuthItemAuthPerm'
CREATE INDEX [IX_FK_AuthItemAuthItemAuthPerm]
ON [dbo].[AuthItemAuthPerms]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthItemAuthPermId] in table 'AuthRolAuthItemAuthPerms'
ALTER TABLE [dbo].[AuthRolAuthItemAuthPerms]
ADD CONSTRAINT [FK_AuthItemAuthPermAuthRolAuthItemAuthPerm]
    FOREIGN KEY ([AuthItemAuthPermId])
    REFERENCES [dbo].[AuthItemAuthPerms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthItemAuthPermAuthRolAuthItemAuthPerm'
CREATE INDEX [IX_FK_AuthItemAuthPermAuthRolAuthItemAuthPerm]
ON [dbo].[AuthRolAuthItemAuthPerms]
    ([AuthItemAuthPermId]);
GO

-- Creating foreign key on [AuthPermId] in table 'AuthItemAuthPerms'
ALTER TABLE [dbo].[AuthItemAuthPerms]
ADD CONSTRAINT [FK_AuthPermAuthItemAuthPerm]
    FOREIGN KEY ([AuthPermId])
    REFERENCES [dbo].[AuthPerms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthPermAuthItemAuthPerm'
CREATE INDEX [IX_FK_AuthPermAuthItemAuthPerm]
ON [dbo].[AuthItemAuthPerms]
    ([AuthPermId]);
GO

-- Creating foreign key on [AuthItemId] in table 'Menus'
ALTER TABLE [dbo].[Menus]
ADD CONSTRAINT [FK_AuthItemMenu]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthItemMenu'
CREATE INDEX [IX_FK_AuthItemMenu]
ON [dbo].[Menus]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthItemId] in table 'ModelEntityAttributes'
ALTER TABLE [dbo].[ModelEntityAttributes]
ADD CONSTRAINT [FK_AuthItemModelEntityAttribute]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthItemModelEntityAttribute'
CREATE INDEX [IX_FK_AuthItemModelEntityAttribute]
ON [dbo].[ModelEntityAttributes]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthItemId] in table 'ModElmElms'
ALTER TABLE [dbo].[ModElmElms]
ADD CONSTRAINT [FK_AuthItemModElmElm]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthItemModElmElm'
CREATE INDEX [IX_FK_AuthItemModElmElm]
ON [dbo].[ModElmElms]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthItemId] in table 'Elms'
ALTER TABLE [dbo].[Elms]
ADD CONSTRAINT [FK_ElmAuthItem]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElmAuthItem'
CREATE INDEX [IX_FK_ElmAuthItem]
ON [dbo].[Elms]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthItemId] in table 'ElmElms'
ALTER TABLE [dbo].[ElmElms]
ADD CONSTRAINT [FK_ElmElmAuthItem]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElmElmAuthItem'
CREATE INDEX [IX_FK_ElmElmAuthItem]
ON [dbo].[ElmElms]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthItemId] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [FK_MenuItemAuthItem]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuItemAuthItem'
CREATE INDEX [IX_FK_MenuItemAuthItem]
ON [dbo].[MenuItems]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthItemId] in table 'ModelEntities'
ALTER TABLE [dbo].[ModelEntities]
ADD CONSTRAINT [FK_ModelEntityAuthItem]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModelEntityAuthItem'
CREATE INDEX [IX_FK_ModelEntityAuthItem]
ON [dbo].[ModelEntities]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthItemId] in table 'ModElms'
ALTER TABLE [dbo].[ModElms]
ADD CONSTRAINT [FK_ModElmAuthItem]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModElmAuthItem'
CREATE INDEX [IX_FK_ModElmAuthItem]
ON [dbo].[ModElms]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthRolAuthRolId] in table 'AuthRolAuthItemAuthPerms'
ALTER TABLE [dbo].[AuthRolAuthItemAuthPerms]
ADD CONSTRAINT [FK_AuthRolAuthRolAuthItemAuthPerm]
    FOREIGN KEY ([AuthRolAuthRolId])
    REFERENCES [dbo].[AuthRols]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthRolAuthRolAuthItemAuthPerm'
CREATE INDEX [IX_FK_AuthRolAuthRolAuthItemAuthPerm]
ON [dbo].[AuthRolAuthItemAuthPerms]
    ([AuthRolAuthRolId]);
GO

-- Creating foreign key on [AuthRolId] in table 'ModAuthRols'
ALTER TABLE [dbo].[ModAuthRols]
ADD CONSTRAINT [FK_AuthRolModAuthRol]
    FOREIGN KEY ([AuthRolId])
    REFERENCES [dbo].[AuthRols]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthRolModAuthRol'
CREATE INDEX [IX_FK_AuthRolModAuthRol]
ON [dbo].[ModAuthRols]
    ([AuthRolId]);
GO

-- Creating foreign key on [AuthRoleTypId] in table 'AuthRols'
ALTER TABLE [dbo].[AuthRols]
ADD CONSTRAINT [FK_AuthRolTypAuthRol]
    FOREIGN KEY ([AuthRoleTypId])
    REFERENCES [dbo].[AuthRolTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthRolTypAuthRol'
CREATE INDEX [IX_FK_AuthRolTypAuthRol]
ON [dbo].[AuthRols]
    ([AuthRoleTypId]);
GO

-- Creating foreign key on [ContactId] in table 'OrgContacts'
ALTER TABLE [dbo].[OrgContacts]
ADD CONSTRAINT [FK_ContactOrgContract]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactOrgContract'
CREATE INDEX [IX_FK_ContactOrgContract]
ON [dbo].[OrgContacts]
    ([ContactId]);
GO

-- Creating foreign key on [ContactId] in table 'PersContacts'
ALTER TABLE [dbo].[PersContacts]
ADD CONSTRAINT [FK_ContactPersContact]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactPersContact'
CREATE INDEX [IX_FK_ContactPersContact]
ON [dbo].[PersContacts]
    ([ContactId]);
GO

-- Creating foreign key on [ContactTypId] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_ContactTypContact]
    FOREIGN KEY ([ContactTypId])
    REFERENCES [dbo].[ContactTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactTypContact'
CREATE INDEX [IX_FK_ContactTypContact]
ON [dbo].[Contacts]
    ([ContactTypId]);
GO

-- Creating foreign key on [CountryId] in table 'Locns'
ALTER TABLE [dbo].[Locns]
ADD CONSTRAINT [FK_CountryLocn]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryLocn'
CREATE INDEX [IX_FK_CountryLocn]
ON [dbo].[Locns]
    ([CountryId]);
GO

-- Creating foreign key on [DocId] in table 'WkflowInstanceDocs'
ALTER TABLE [dbo].[WkflowInstanceDocs]
ADD CONSTRAINT [FK_DocWkflowInstanceDoc]
    FOREIGN KEY ([DocId])
    REFERENCES [dbo].[soUploads]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocWkflowInstanceDoc'
CREATE INDEX [IX_FK_DocWkflowInstanceDoc]
ON [dbo].[WkflowInstanceDocs]
    ([DocId]);
GO

-- Creating foreign key on [ElmId] in table 'ElmElms'
ALTER TABLE [dbo].[ElmElms]
ADD CONSTRAINT [FK_ElmElmElm]
    FOREIGN KEY ([ElmId])
    REFERENCES [dbo].[Elms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElmElmElm'
CREATE INDEX [IX_FK_ElmElmElm]
ON [dbo].[ElmElms]
    ([ElmId]);
GO

-- Creating foreign key on [AssociatedElmId] in table 'ElmElms'
ALTER TABLE [dbo].[ElmElms]
ADD CONSTRAINT [FK_ElmElmElm1]
    FOREIGN KEY ([AssociatedElmId])
    REFERENCES [dbo].[Elms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElmElmElm1'
CREATE INDEX [IX_FK_ElmElmElm1]
ON [dbo].[ElmElms]
    ([AssociatedElmId]);
GO

-- Creating foreign key on [ElmTypId] in table 'Elms'
ALTER TABLE [dbo].[Elms]
ADD CONSTRAINT [FK_ElmElmTyp]
    FOREIGN KEY ([ElmTypId])
    REFERENCES [dbo].[ElmTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElmElmTyp'
CREATE INDEX [IX_FK_ElmElmTyp]
ON [dbo].[Elms]
    ([ElmTypId]);
GO

-- Creating foreign key on [ElmId] in table 'ModElmElms'
ALTER TABLE [dbo].[ModElmElms]
ADD CONSTRAINT [FK_ElmModElmElm]
    FOREIGN KEY ([ElmId])
    REFERENCES [dbo].[Elms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElmModElmElm'
CREATE INDEX [IX_FK_ElmModElmElm]
ON [dbo].[ModElmElms]
    ([ElmId]);
GO

-- Creating foreign key on [EntityAttributeTypId] in table 'ModelEntityAttributes'
ALTER TABLE [dbo].[ModelEntityAttributes]
ADD CONSTRAINT [FK_ModelEntityAttributeEntityAttributeTyp]
    FOREIGN KEY ([EntityAttributeTypId])
    REFERENCES [dbo].[EntityAttributeTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModelEntityAttributeEntityAttributeTyp'
CREATE INDEX [IX_FK_ModelEntityAttributeEntityAttributeTyp]
ON [dbo].[ModelEntityAttributes]
    ([EntityAttributeTypId]);
GO

-- Creating foreign key on [IconVisualId] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [FK_GenderIconVisual]
    FOREIGN KEY ([IconVisualId])
    REFERENCES [dbo].[IconVisuals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenderIconVisual'
CREATE INDEX [IX_FK_GenderIconVisual]
ON [dbo].[Genders]
    ([IconVisualId]);
GO

-- Creating foreign key on [GenderId] in table 'Pers'
ALTER TABLE [dbo].[Pers]
ADD CONSTRAINT [FK_GenderPers]
    FOREIGN KEY ([GenderId])
    REFERENCES [dbo].[Genders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenderPers'
CREATE INDEX [IX_FK_GenderPers]
ON [dbo].[Pers]
    ([GenderId]);
GO

-- Creating foreign key on [IconVisualTypId] in table 'IconVisuals'
ALTER TABLE [dbo].[IconVisuals]
ADD CONSTRAINT [FK_IconVisualIconVisualTyp]
    FOREIGN KEY ([IconVisualTypId])
    REFERENCES [dbo].[IconVisualTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IconVisualIconVisualTyp'
CREATE INDEX [IX_FK_IconVisualIconVisualTyp]
ON [dbo].[IconVisuals]
    ([IconVisualTypId]);
GO

-- Creating foreign key on [IconVisualId] in table 'LogCats'
ALTER TABLE [dbo].[LogCats]
ADD CONSTRAINT [FK_IconVisualLogCat]
    FOREIGN KEY ([IconVisualId])
    REFERENCES [dbo].[IconVisuals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IconVisualLogCat'
CREATE INDEX [IX_FK_IconVisualLogCat]
ON [dbo].[LogCats]
    ([IconVisualId]);
GO

-- Creating foreign key on [IconVisualId] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [FK_IconVisualMenuItem]
    FOREIGN KEY ([IconVisualId])
    REFERENCES [dbo].[IconVisuals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IconVisualMenuItem'
CREATE INDEX [IX_FK_IconVisualMenuItem]
ON [dbo].[MenuItems]
    ([IconVisualId]);
GO

-- Creating foreign key on [IconVisualId] in table 'Menus'
ALTER TABLE [dbo].[Menus]
ADD CONSTRAINT [FK_MenuIconVisual]
    FOREIGN KEY ([IconVisualId])
    REFERENCES [dbo].[IconVisuals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuIconVisual'
CREATE INDEX [IX_FK_MenuIconVisual]
ON [dbo].[Menus]
    ([IconVisualId]);
GO

-- Creating foreign key on [IconVisualId] in table 'PortMods'
ALTER TABLE [dbo].[PortMods]
ADD CONSTRAINT [FK_PortModIconVisual]
    FOREIGN KEY ([IconVisualId])
    REFERENCES [dbo].[IconVisuals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortModIconVisual'
CREATE INDEX [IX_FK_PortModIconVisual]
ON [dbo].[PortMods]
    ([IconVisualId]);
GO

-- Creating foreign key on [LayoutTypId] in table 'Scenes'
ALTER TABLE [dbo].[Scenes]
ADD CONSTRAINT [FK_SceneLayoutTyp]
    FOREIGN KEY ([LayoutTypId])
    REFERENCES [dbo].[LayoutTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SceneLayoutTyp'
CREATE INDEX [IX_FK_SceneLayoutTyp]
ON [dbo].[Scenes]
    ([LayoutTypId]);
GO

-- Creating foreign key on [LocalityId] in table 'Pers'
ALTER TABLE [dbo].[Pers]
ADD CONSTRAINT [FK_LocalityPers]
    FOREIGN KEY ([LocalityId])
    REFERENCES [dbo].[Localities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocalityPers'
CREATE INDEX [IX_FK_LocalityPers]
ON [dbo].[Pers]
    ([LocalityId]);
GO

-- Creating foreign key on [LocnId] in table 'OrgLocns'
ALTER TABLE [dbo].[OrgLocns]
ADD CONSTRAINT [FK_LocnOrgLocn]
    FOREIGN KEY ([LocnId])
    REFERENCES [dbo].[Locns]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocnOrgLocn'
CREATE INDEX [IX_FK_LocnOrgLocn]
ON [dbo].[OrgLocns]
    ([LocnId]);
GO

-- Creating foreign key on [LocnId] in table 'Pers'
ALTER TABLE [dbo].[Pers]
ADD CONSTRAINT [FK_PersLocn]
    FOREIGN KEY ([LocnId])
    REFERENCES [dbo].[Locns]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersLocn'
CREATE INDEX [IX_FK_PersLocn]
ON [dbo].[Pers]
    ([LocnId]);
GO

-- Creating foreign key on [LogCatId] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [FK_LogEntryLogCat]
    FOREIGN KEY ([LogCatId])
    REFERENCES [dbo].[LogCats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LogEntryLogCat'
CREATE INDEX [IX_FK_LogEntryLogCat]
ON [dbo].[LogEntries]
    ([LogCatId]);
GO

-- Creating foreign key on [MenuId] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [FK_MenuItemMenu]
    FOREIGN KEY ([MenuId])
    REFERENCES [dbo].[Menus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuItemMenu'
CREATE INDEX [IX_FK_MenuItemMenu]
ON [dbo].[MenuItems]
    ([MenuId]);
GO

-- Creating foreign key on [ParentMenuItemId] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [FK_MenuItemMenuItem]
    FOREIGN KEY ([ParentMenuItemId])
    REFERENCES [dbo].[MenuItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuItemMenuItem'
CREATE INDEX [IX_FK_MenuItemMenuItem]
ON [dbo].[MenuItems]
    ([ParentMenuItemId]);
GO

-- Creating foreign key on [ModStateId] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [FK_ModStateMenuItem]
    FOREIGN KEY ([ModStateId])
    REFERENCES [dbo].[ModStates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModStateMenuItem'
CREATE INDEX [IX_FK_ModStateMenuItem]
ON [dbo].[MenuItems]
    ([ModStateId]);
GO

-- Creating foreign key on [MenuId] in table 'PortMods'
ALTER TABLE [dbo].[PortMods]
ADD CONSTRAINT [FK_MenuPortMod]
    FOREIGN KEY ([MenuId])
    REFERENCES [dbo].[Menus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuPortMod'
CREATE INDEX [IX_FK_MenuPortMod]
ON [dbo].[PortMods]
    ([MenuId]);
GO

-- Creating foreign key on [ModAuthRolId] in table 'UserModAuthRols'
ALTER TABLE [dbo].[UserModAuthRols]
ADD CONSTRAINT [FK_ModAuthRolUserModAuthRol]
    FOREIGN KEY ([ModAuthRolId])
    REFERENCES [dbo].[ModAuthRols]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModAuthRolUserModAuthRol'
CREATE INDEX [IX_FK_ModAuthRolUserModAuthRol]
ON [dbo].[UserModAuthRols]
    ([ModAuthRolId]);
GO

-- Creating foreign key on [ModId] in table 'ModAuthRols'
ALTER TABLE [dbo].[ModAuthRols]
ADD CONSTRAINT [FK_ModModAuthRol]
    FOREIGN KEY ([ModId])
    REFERENCES [dbo].[Mods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModModAuthRol'
CREATE INDEX [IX_FK_ModModAuthRol]
ON [dbo].[ModAuthRols]
    ([ModId]);
GO

-- Creating foreign key on [ModelEntityId] in table 'ModelEntityAttributes'
ALTER TABLE [dbo].[ModelEntityAttributes]
ADD CONSTRAINT [FK_ModelEntityModelEntityAttribute]
    FOREIGN KEY ([ModelEntityId])
    REFERENCES [dbo].[ModelEntities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModelEntityModelEntityAttribute'
CREATE INDEX [IX_FK_ModelEntityModelEntityAttribute]
ON [dbo].[ModelEntityAttributes]
    ([ModelEntityId]);
GO

-- Creating foreign key on [ModElmId] in table 'ModElmElms'
ALTER TABLE [dbo].[ModElmElms]
ADD CONSTRAINT [FK_ModElmModElmElm]
    FOREIGN KEY ([ModElmId])
    REFERENCES [dbo].[ModElms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModElmModElmElm'
CREATE INDEX [IX_FK_ModElmModElmElm]
ON [dbo].[ModElmElms]
    ([ModElmId]);
GO

-- Creating foreign key on [ModElmTypId] in table 'ModElms'
ALTER TABLE [dbo].[ModElms]
ADD CONSTRAINT [FK_ModElmModElmTyp]
    FOREIGN KEY ([ModElmTypId])
    REFERENCES [dbo].[ModElmTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModElmModElmTyp'
CREATE INDEX [IX_FK_ModElmModElmTyp]
ON [dbo].[ModElms]
    ([ModElmTypId]);
GO

-- Creating foreign key on [ModElmId] in table 'PortModModElms'
ALTER TABLE [dbo].[PortModModElms]
ADD CONSTRAINT [FK_ModElmPortModModElm]
    FOREIGN KEY ([ModElmId])
    REFERENCES [dbo].[ModElms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModElmPortModModElm'
CREATE INDEX [IX_FK_ModElmPortModModElm]
ON [dbo].[PortModModElms]
    ([ModElmId]);
GO

-- Creating foreign key on [ModId] in table 'ModElms'
ALTER TABLE [dbo].[ModElms]
ADD CONSTRAINT [FK_ModModElm]
    FOREIGN KEY ([ModId])
    REFERENCES [dbo].[Mods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModModElm'
CREATE INDEX [IX_FK_ModModElm]
ON [dbo].[ModElms]
    ([ModId]);
GO

-- Creating foreign key on [ModId] in table 'ModSettings'
ALTER TABLE [dbo].[ModSettings]
ADD CONSTRAINT [FK_ModModSetting]
    FOREIGN KEY ([ModId])
    REFERENCES [dbo].[Mods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModModSetting'
CREATE INDEX [IX_FK_ModModSetting]
ON [dbo].[ModSettings]
    ([ModId]);
GO

-- Creating foreign key on [ModId] in table 'ModStates'
ALTER TABLE [dbo].[ModStates]
ADD CONSTRAINT [FK_ModModState]
    FOREIGN KEY ([ModId])
    REFERENCES [dbo].[Mods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModModState'
CREATE INDEX [IX_FK_ModModState]
ON [dbo].[ModStates]
    ([ModId]);
GO

-- Creating foreign key on [ModId] in table 'Parts'
ALTER TABLE [dbo].[Parts]
ADD CONSTRAINT [FK_ModPart]
    FOREIGN KEY ([ModId])
    REFERENCES [dbo].[Mods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModPart'
CREATE INDEX [IX_FK_ModPart]
ON [dbo].[Parts]
    ([ModId]);
GO

-- Creating foreign key on [ModId] in table 'PortMods'
ALTER TABLE [dbo].[PortMods]
ADD CONSTRAINT [FK_ModPortMod]
    FOREIGN KEY ([ModId])
    REFERENCES [dbo].[Mods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModPortMod'
CREATE INDEX [IX_FK_ModPortMod]
ON [dbo].[PortMods]
    ([ModId]);
GO

-- Creating foreign key on [ModTypId] in table 'Mods'
ALTER TABLE [dbo].[Mods]
ADD CONSTRAINT [FK_ModTypMod]
    FOREIGN KEY ([ModTypId])
    REFERENCES [dbo].[ModTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModTypMod'
CREATE INDEX [IX_FK_ModTypMod]
ON [dbo].[Mods]
    ([ModTypId]);
GO

-- Creating foreign key on [ModSettingId] in table 'ModSettingValues'
ALTER TABLE [dbo].[ModSettingValues]
ADD CONSTRAINT [FK_ModSettingModSettingValue]
    FOREIGN KEY ([ModSettingId])
    REFERENCES [dbo].[ModSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModSettingModSettingValue'
CREATE INDEX [IX_FK_ModSettingModSettingValue]
ON [dbo].[ModSettingValues]
    ([ModSettingId]);
GO

-- Creating foreign key on [SettingId] in table 'ModSettings'
ALTER TABLE [dbo].[ModSettings]
ADD CONSTRAINT [FK_SettingModSetting]
    FOREIGN KEY ([SettingId])
    REFERENCES [dbo].[Settings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingModSetting'
CREATE INDEX [IX_FK_SettingModSetting]
ON [dbo].[ModSettings]
    ([SettingId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgContacts'
ALTER TABLE [dbo].[OrgContacts]
ADD CONSTRAINT [FK_OrgOrgContract]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgContract'
CREATE INDEX [IX_FK_OrgOrgContract]
ON [dbo].[OrgContacts]
    ([OrgId]);
GO

-- Creating foreign key on [Org_Id] in table 'OrgCusts'
ALTER TABLE [dbo].[OrgCusts]
ADD CONSTRAINT [FK_OrgOrgCust]
    FOREIGN KEY ([Org_Id])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgCust'
CREATE INDEX [IX_FK_OrgOrgCust]
ON [dbo].[OrgCusts]
    ([Org_Id]);
GO

-- Creating foreign key on [OrgId] in table 'OrgLocns'
ALTER TABLE [dbo].[OrgLocns]
ADD CONSTRAINT [FK_OrgOrgLocn]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgLocn'
CREATE INDEX [IX_FK_OrgOrgLocn]
ON [dbo].[OrgLocns]
    ([OrgId]);
GO

-- Creating foreign key on [UserId] in table 'OrgNotes'
ALTER TABLE [dbo].[OrgNotes]
ADD CONSTRAINT [FK_OrgNoteUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgNoteUser'
CREATE INDEX [IX_FK_OrgNoteUser]
ON [dbo].[OrgNotes]
    ([UserId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgNotes'
ALTER TABLE [dbo].[OrgNotes]
ADD CONSTRAINT [FK_OrgOrgNote]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgNote'
CREATE INDEX [IX_FK_OrgOrgNote]
ON [dbo].[OrgNotes]
    ([OrgId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgOrgs'
ALTER TABLE [dbo].[OrgOrgs]
ADD CONSTRAINT [FK_OrgOrgOrg]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgOrg'
CREATE INDEX [IX_FK_OrgOrgOrg]
ON [dbo].[OrgOrgs]
    ([OrgId]);
GO

-- Creating foreign key on [AssociatedOrgId] in table 'OrgOrgs'
ALTER TABLE [dbo].[OrgOrgs]
ADD CONSTRAINT [FK_OrgOrgOrg1]
    FOREIGN KEY ([AssociatedOrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgOrg1'
CREATE INDEX [IX_FK_OrgOrgOrg1]
ON [dbo].[OrgOrgs]
    ([AssociatedOrgId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgStatusHists'
ALTER TABLE [dbo].[OrgStatusHists]
ADD CONSTRAINT [FK_OrgOrgStatusHist]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgStatusHist'
CREATE INDEX [IX_FK_OrgOrgStatusHist]
ON [dbo].[OrgStatusHists]
    ([OrgId]);
GO

-- Creating foreign key on [CreatedUserId] in table 'Orgs'
ALTER TABLE [dbo].[Orgs]
ADD CONSTRAINT [FK_UserOrg]
    FOREIGN KEY ([CreatedUserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOrg'
CREATE INDEX [IX_FK_UserOrg]
ON [dbo].[Orgs]
    ([CreatedUserId]);
GO

-- Creating foreign key on [OrgStatusId] in table 'OrgTypOrgStatus'
ALTER TABLE [dbo].[OrgTypOrgStatus]
ADD CONSTRAINT [FK_OrgStatusOrgTypOrgStatus]
    FOREIGN KEY ([OrgStatusId])
    REFERENCES [dbo].[OrgStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgStatusOrgTypOrgStatus'
CREATE INDEX [IX_FK_OrgStatusOrgTypOrgStatus]
ON [dbo].[OrgTypOrgStatus]
    ([OrgStatusId]);
GO

-- Creating foreign key on [OrgTypId] in table 'OrgTypOrgStatus'
ALTER TABLE [dbo].[OrgTypOrgStatus]
ADD CONSTRAINT [FK_OrgTypOrgTypOrgStatus]
    FOREIGN KEY ([OrgTypId])
    REFERENCES [dbo].[OrgTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgTypOrgTypOrgStatus'
CREATE INDEX [IX_FK_OrgTypOrgTypOrgStatus]
ON [dbo].[OrgTypOrgStatus]
    ([OrgTypId]);
GO

-- Creating foreign key on [PartGrpId] in table 'PartGrpParts'
ALTER TABLE [dbo].[PartGrpParts]
ADD CONSTRAINT [FK_PartGrpPartGrpPart]
    FOREIGN KEY ([PartGrpId])
    REFERENCES [dbo].[PartGrps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartGrpPartGrpPart'
CREATE INDEX [IX_FK_PartGrpPartGrpPart]
ON [dbo].[PartGrpParts]
    ([PartGrpId]);
GO

-- Creating foreign key on [PartId] in table 'PartGrpParts'
ALTER TABLE [dbo].[PartGrpParts]
ADD CONSTRAINT [FK_PartPartGrpPart]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[Parts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartPartGrpPart'
CREATE INDEX [IX_FK_PartPartGrpPart]
ON [dbo].[PartGrpParts]
    ([PartId]);
GO

-- Creating foreign key on [PartIndicatorId] in table 'Parts'
ALTER TABLE [dbo].[Parts]
ADD CONSTRAINT [FK_PartIndicatorPart]
    FOREIGN KEY ([PartIndicatorId])
    REFERENCES [dbo].[PartIndicators]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartIndicatorPart'
CREATE INDEX [IX_FK_PartIndicatorPart]
ON [dbo].[Parts]
    ([PartIndicatorId]);
GO

-- Creating foreign key on [PartId] in table 'PartSettings'
ALTER TABLE [dbo].[PartSettings]
ADD CONSTRAINT [FK_PartPartSetting]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[Parts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartPartSetting'
CREATE INDEX [IX_FK_PartPartSetting]
ON [dbo].[PartSettings]
    ([PartId]);
GO

-- Creating foreign key on [Part_Id] in table 'SceneParts'
ALTER TABLE [dbo].[SceneParts]
ADD CONSTRAINT [FK_PartScenePart]
    FOREIGN KEY ([Part_Id])
    REFERENCES [dbo].[Parts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartScenePart'
CREATE INDEX [IX_FK_PartScenePart]
ON [dbo].[SceneParts]
    ([Part_Id]);
GO

-- Creating foreign key on [PartTypId] in table 'Parts'
ALTER TABLE [dbo].[Parts]
ADD CONSTRAINT [FK_PartTypPart]
    FOREIGN KEY ([PartTypId])
    REFERENCES [dbo].[PartTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartTypPart'
CREATE INDEX [IX_FK_PartTypPart]
ON [dbo].[Parts]
    ([PartTypId]);
GO

-- Creating foreign key on [PartSettingId] in table 'ScenePartPartSettings'
ALTER TABLE [dbo].[ScenePartPartSettings]
ADD CONSTRAINT [FK_PartSettingScenePartPartSetting]
    FOREIGN KEY ([PartSettingId])
    REFERENCES [dbo].[PartSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartSettingScenePartPartSetting'
CREATE INDEX [IX_FK_PartSettingScenePartPartSetting]
ON [dbo].[ScenePartPartSettings]
    ([PartSettingId]);
GO

-- Creating foreign key on [SettingId] in table 'PartSettings'
ALTER TABLE [dbo].[PartSettings]
ADD CONSTRAINT [FK_SettingPartSetting]
    FOREIGN KEY ([SettingId])
    REFERENCES [dbo].[Settings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingPartSetting'
CREATE INDEX [IX_FK_SettingPartSetting]
ON [dbo].[PartSettings]
    ([SettingId]);
GO

-- Creating foreign key on [PerId] in table 'PerNotes'
ALTER TABLE [dbo].[PerNotes]
ADD CONSTRAINT [FK_PerPerNote]
    FOREIGN KEY ([PerId])
    REFERENCES [dbo].[Pers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PerPerNote'
CREATE INDEX [IX_FK_PerPerNote]
ON [dbo].[PerNotes]
    ([PerId]);
GO

-- Creating foreign key on [PersId] in table 'PersContacts'
ALTER TABLE [dbo].[PersContacts]
ADD CONSTRAINT [FK_PersPersContact]
    FOREIGN KEY ([PersId])
    REFERENCES [dbo].[Pers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersPersContact'
CREATE INDEX [IX_FK_PersPersContact]
ON [dbo].[PersContacts]
    ([PersId]);
GO

-- Creating foreign key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_PerUser]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Pers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SceneId] in table 'PlaceHolders'
ALTER TABLE [dbo].[PlaceHolders]
ADD CONSTRAINT [FK_ScenePlaceHolder]
    FOREIGN KEY ([SceneId])
    REFERENCES [dbo].[Scenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScenePlaceHolder'
CREATE INDEX [IX_FK_ScenePlaceHolder]
ON [dbo].[PlaceHolders]
    ([SceneId]);
GO

-- Creating foreign key on [PortModId] in table 'PortModModElms'
ALTER TABLE [dbo].[PortModModElms]
ADD CONSTRAINT [FK_PortModPortModModElm]
    FOREIGN KEY ([PortModId])
    REFERENCES [dbo].[PortMods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortModPortModModElm'
CREATE INDEX [IX_FK_PortModPortModModElm]
ON [dbo].[PortModModElms]
    ([PortModId]);
GO

-- Creating foreign key on [PortModId] in table 'PortModSettings'
ALTER TABLE [dbo].[PortModSettings]
ADD CONSTRAINT [FK_PortModPortModSetting]
    FOREIGN KEY ([PortModId])
    REFERENCES [dbo].[PortMods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortModPortModSetting'
CREATE INDEX [IX_FK_PortModPortModSetting]
ON [dbo].[PortModSettings]
    ([PortModId]);
GO

-- Creating foreign key on [PortId] in table 'PortMods'
ALTER TABLE [dbo].[PortMods]
ADD CONSTRAINT [FK_PortPortMod]
    FOREIGN KEY ([PortId])
    REFERENCES [dbo].[Ports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortPortMod'
CREATE INDEX [IX_FK_PortPortMod]
ON [dbo].[PortMods]
    ([PortId]);
GO

-- Creating foreign key on [PortModSettingId] in table 'PortModSettingValues'
ALTER TABLE [dbo].[PortModSettingValues]
ADD CONSTRAINT [FK_PortModSettingPortModSettingValue]
    FOREIGN KEY ([PortModSettingId])
    REFERENCES [dbo].[PortModSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortModSettingPortModSettingValue'
CREATE INDEX [IX_FK_PortModSettingPortModSettingValue]
ON [dbo].[PortModSettingValues]
    ([PortModSettingId]);
GO

-- Creating foreign key on [SettingId] in table 'PortModSettings'
ALTER TABLE [dbo].[PortModSettings]
ADD CONSTRAINT [FK_SettingPortModSetting]
    FOREIGN KEY ([SettingId])
    REFERENCES [dbo].[Settings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingPortModSetting'
CREATE INDEX [IX_FK_SettingPortModSetting]
ON [dbo].[PortModSettings]
    ([SettingId]);
GO

-- Creating foreign key on [PortId] in table 'PortScenes'
ALTER TABLE [dbo].[PortScenes]
ADD CONSTRAINT [FK_PortPortScene]
    FOREIGN KEY ([PortId])
    REFERENCES [dbo].[Ports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortPortScene'
CREATE INDEX [IX_FK_PortPortScene]
ON [dbo].[PortScenes]
    ([PortId]);
GO

-- Creating foreign key on [PortId] in table 'PortUsers'
ALTER TABLE [dbo].[PortUsers]
ADD CONSTRAINT [FK_PortPortUser]
    FOREIGN KEY ([PortId])
    REFERENCES [dbo].[Ports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortPortUser'
CREATE INDEX [IX_FK_PortPortUser]
ON [dbo].[PortUsers]
    ([PortId]);
GO

-- Creating foreign key on [SceneId] in table 'PortScenes'
ALTER TABLE [dbo].[PortScenes]
ADD CONSTRAINT [FK_ScenePortScene]
    FOREIGN KEY ([SceneId])
    REFERENCES [dbo].[Scenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScenePortScene'
CREATE INDEX [IX_FK_ScenePortScene]
ON [dbo].[PortScenes]
    ([SceneId]);
GO

-- Creating foreign key on [UserId] in table 'PortUsers'
ALTER TABLE [dbo].[PortUsers]
ADD CONSTRAINT [FK_UserPortUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPortUser'
CREATE INDEX [IX_FK_UserPortUser]
ON [dbo].[PortUsers]
    ([UserId]);
GO

-- Creating foreign key on [ScenePartId] in table 'ScenePartPartSettings'
ALTER TABLE [dbo].[ScenePartPartSettings]
ADD CONSTRAINT [FK_ScenePartScenePartPartSetting]
    FOREIGN KEY ([ScenePartId])
    REFERENCES [dbo].[SceneParts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScenePartScenePartPartSetting'
CREATE INDEX [IX_FK_ScenePartScenePartPartSetting]
ON [dbo].[ScenePartPartSettings]
    ([ScenePartId]);
GO

-- Creating foreign key on [SceneId] in table 'SceneParts'
ALTER TABLE [dbo].[SceneParts]
ADD CONSTRAINT [FK_SceneScenePart]
    FOREIGN KEY ([SceneId])
    REFERENCES [dbo].[Scenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SceneScenePart'
CREATE INDEX [IX_FK_SceneScenePart]
ON [dbo].[SceneParts]
    ([SceneId]);
GO

-- Creating foreign key on [SettingTypId] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [FK_SettingTypSetting]
    FOREIGN KEY ([SettingTypId])
    REFERENCES [dbo].[SettingTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingTypSetting'
CREATE INDEX [IX_FK_SettingTypSetting]
ON [dbo].[Settings]
    ([SettingTypId]);
GO

-- Creating foreign key on [UserId] in table 'UserLoginHists'
ALTER TABLE [dbo].[UserLoginHists]
ADD CONSTRAINT [FK_UserUserLoginHist]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserLoginHist'
CREATE INDEX [IX_FK_UserUserLoginHist]
ON [dbo].[UserLoginHists]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserModAuthRols'
ALTER TABLE [dbo].[UserModAuthRols]
ADD CONSTRAINT [FK_UserUserModAuthRol]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserModAuthRol'
CREATE INDEX [IX_FK_UserUserModAuthRol]
ON [dbo].[UserModAuthRols]
    ([UserId]);
GO

-- Creating foreign key on [WkflowDefId] in table 'WkflowDefWkflowStats'
ALTER TABLE [dbo].[WkflowDefWkflowStats]
ADD CONSTRAINT [FK_WkflowDefWkflowDefWkflowStat]
    FOREIGN KEY ([WkflowDefId])
    REFERENCES [dbo].[WkflowDefs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowDefWkflowDefWkflowStat'
CREATE INDEX [IX_FK_WkflowDefWkflowDefWkflowStat]
ON [dbo].[WkflowDefWkflowStats]
    ([WkflowDefId]);
GO

-- Creating foreign key on [WkflowDefId] in table 'WkflowInstances'
ALTER TABLE [dbo].[WkflowInstances]
ADD CONSTRAINT [FK_WkflowDefWkflowInstance]
    FOREIGN KEY ([WkflowDefId])
    REFERENCES [dbo].[WkflowDefs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowDefWkflowInstance'
CREATE INDEX [IX_FK_WkflowDefWkflowInstance]
ON [dbo].[WkflowInstances]
    ([WkflowDefId]);
GO

-- Creating foreign key on [WkflowTypId] in table 'WkflowDefs'
ALTER TABLE [dbo].[WkflowDefs]
ADD CONSTRAINT [FK_WkflowTypWkflowDef]
    FOREIGN KEY ([WkflowTypId])
    REFERENCES [dbo].[WkflowTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowTypWkflowDef'
CREATE INDEX [IX_FK_WkflowTypWkflowDef]
ON [dbo].[WkflowDefs]
    ([WkflowTypId]);
GO

-- Creating foreign key on [WkflowDefWkflowStatId] in table 'WkflowDefStatReas'
ALTER TABLE [dbo].[WkflowDefStatReas]
ADD CONSTRAINT [FK_WkflowDefStatReasWkflowDefWkflowStat]
    FOREIGN KEY ([WkflowDefWkflowStatId])
    REFERENCES [dbo].[WkflowDefWkflowStats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowDefStatReasWkflowDefWkflowStat'
CREATE INDEX [IX_FK_WkflowDefStatReasWkflowDefWkflowStat]
ON [dbo].[WkflowDefStatReas]
    ([WkflowDefWkflowStatId]);
GO

-- Creating foreign key on [NextWkflowDefWkflowStatId] in table 'WkflowDefStatReas'
ALTER TABLE [dbo].[WkflowDefStatReas]
ADD CONSTRAINT [FK_WkflowDefStatReasWkflowDefWkflowStat1]
    FOREIGN KEY ([NextWkflowDefWkflowStatId])
    REFERENCES [dbo].[WkflowDefWkflowStats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowDefStatReasWkflowDefWkflowStat1'
CREATE INDEX [IX_FK_WkflowDefStatReasWkflowDefWkflowStat1]
ON [dbo].[WkflowDefStatReas]
    ([NextWkflowDefWkflowStatId]);
GO

-- Creating foreign key on [WkflowStatReasId] in table 'WkflowDefStatReas'
ALTER TABLE [dbo].[WkflowDefStatReas]
ADD CONSTRAINT [FK_WkflowStatReasWkflowTypStatReas]
    FOREIGN KEY ([WkflowStatReasId])
    REFERENCES [dbo].[WkflowStatReas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowStatReasWkflowTypStatReas'
CREATE INDEX [IX_FK_WkflowStatReasWkflowTypStatReas]
ON [dbo].[WkflowDefStatReas]
    ([WkflowStatReasId]);
GO

-- Creating foreign key on [WkflowDefWkflowStatId] in table 'WkflowDefWkflowStatWkflowStats'
ALTER TABLE [dbo].[WkflowDefWkflowStatWkflowStats]
ADD CONSTRAINT [FK_WkflowDefWkflowStatWkflowDefWkflowStatWkflowStat]
    FOREIGN KEY ([WkflowDefWkflowStatId])
    REFERENCES [dbo].[WkflowDefWkflowStats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowDefWkflowStatWkflowDefWkflowStatWkflowStat'
CREATE INDEX [IX_FK_WkflowDefWkflowStatWkflowDefWkflowStatWkflowStat]
ON [dbo].[WkflowDefWkflowStatWkflowStats]
    ([WkflowDefWkflowStatId]);
GO

-- Creating foreign key on [WkflowStatId] in table 'WkflowDefWkflowStats'
ALTER TABLE [dbo].[WkflowDefWkflowStats]
ADD CONSTRAINT [FK_WkflowStatWkflowTypWkflowStat]
    FOREIGN KEY ([WkflowStatId])
    REFERENCES [dbo].[WkflowStats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowStatWkflowTypWkflowStat'
CREATE INDEX [IX_FK_WkflowStatWkflowTypWkflowStat]
ON [dbo].[WkflowDefWkflowStats]
    ([WkflowStatId]);
GO

-- Creating foreign key on [AvailWkflowStatId] in table 'WkflowDefWkflowStatWkflowStats'
ALTER TABLE [dbo].[WkflowDefWkflowStatWkflowStats]
ADD CONSTRAINT [FK_WkflowStatWkflowTypeWkflowStatWkflowStat]
    FOREIGN KEY ([AvailWkflowStatId])
    REFERENCES [dbo].[WkflowStats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowStatWkflowTypeWkflowStatWkflowStat'
CREATE INDEX [IX_FK_WkflowStatWkflowTypeWkflowStatWkflowStat]
ON [dbo].[WkflowDefWkflowStatWkflowStats]
    ([AvailWkflowStatId]);
GO

-- Creating foreign key on [WkflowInstanceId] in table 'WkflowInstanceDocs'
ALTER TABLE [dbo].[WkflowInstanceDocs]
ADD CONSTRAINT [FK_WkflowInstanceWkflowInstanceDoc]
    FOREIGN KEY ([WkflowInstanceId])
    REFERENCES [dbo].[WkflowInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowInstanceWkflowInstanceDoc'
CREATE INDEX [IX_FK_WkflowInstanceWkflowInstanceDoc]
ON [dbo].[WkflowInstanceDocs]
    ([WkflowInstanceId]);
GO

-- Creating foreign key on [AncestorWkflowId] in table 'WkflowInstances'
ALTER TABLE [dbo].[WkflowInstances]
ADD CONSTRAINT [FK_WkflowInstanceWkflowInstance]
    FOREIGN KEY ([AncestorWkflowId])
    REFERENCES [dbo].[WkflowInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowInstanceWkflowInstance'
CREATE INDEX [IX_FK_WkflowInstanceWkflowInstance]
ON [dbo].[WkflowInstances]
    ([AncestorWkflowId]);
GO

-- Creating foreign key on [WkflowInstanceId] in table 'WkflowStepHists'
ALTER TABLE [dbo].[WkflowStepHists]
ADD CONSTRAINT [FK_WkflowWkflowStep]
    FOREIGN KEY ([WkflowInstanceId])
    REFERENCES [dbo].[WkflowInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowWkflowStep'
CREATE INDEX [IX_FK_WkflowWkflowStep]
ON [dbo].[WkflowStepHists]
    ([WkflowInstanceId]);
GO

-- Creating foreign key on [WkflowStatReasId] in table 'WkflowStepHists'
ALTER TABLE [dbo].[WkflowStepHists]
ADD CONSTRAINT [FK_WkflowStatReasWkflowStepHist]
    FOREIGN KEY ([WkflowStatReasId])
    REFERENCES [dbo].[WkflowStatReas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowStatReasWkflowStepHist'
CREATE INDEX [IX_FK_WkflowStatReasWkflowStepHist]
ON [dbo].[WkflowStepHists]
    ([WkflowStatReasId]);
GO

-- Creating foreign key on [WkflowStepHistId] in table 'WkflowStepNotes'
ALTER TABLE [dbo].[WkflowStepNotes]
ADD CONSTRAINT [FK_WkflowStepHistWkflowStepNote]
    FOREIGN KEY ([WkflowStepHistId])
    REFERENCES [dbo].[WkflowStepHists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowStepHistWkflowStepNote'
CREATE INDEX [IX_FK_WkflowStepHistWkflowStepNote]
ON [dbo].[WkflowStepNotes]
    ([WkflowStepHistId]);
GO

-- Creating foreign key on [Org_Id] in table 'OrgResellers'
ALTER TABLE [dbo].[OrgResellers]
ADD CONSTRAINT [FK_OrgOrgReseller]
    FOREIGN KEY ([Org_Id])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgReseller'
CREATE INDEX [IX_FK_OrgOrgReseller]
ON [dbo].[OrgResellers]
    ([Org_Id]);
GO

-- Creating foreign key on [FileTypeId] in table 'soUploads'
ALTER TABLE [dbo].[soUploads]
ADD CONSTRAINT [FK_FileTypeDoc]
    FOREIGN KEY ([FileTypeId])
    REFERENCES [dbo].[FileTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileTypeDoc'
CREATE INDEX [IX_FK_FileTypeDoc]
ON [dbo].[soUploads]
    ([FileTypeId]);
GO

-- Creating foreign key on [WkflowInstance_Id] in table 'DPWorkflows'
ALTER TABLE [dbo].[DPWorkflows]
ADD CONSTRAINT [FK_WkflowInstanceDPWorkflow]
    FOREIGN KEY ([WkflowInstance_Id])
    REFERENCES [dbo].[WkflowInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowInstanceDPWorkflow'
CREATE INDEX [IX_FK_WkflowInstanceDPWorkflow]
ON [dbo].[DPWorkflows]
    ([WkflowInstance_Id]);
GO

-- Creating foreign key on [OrgId] in table 'DPWorkflows'
ALTER TABLE [dbo].[DPWorkflows]
ADD CONSTRAINT [FK_OrgDPWorkflow]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgDPWorkflow'
CREATE INDEX [IX_FK_OrgDPWorkflow]
ON [dbo].[DPWorkflows]
    ([OrgId]);
GO

-- Creating foreign key on [UserId] in table 'DPWorkflows'
ALTER TABLE [dbo].[DPWorkflows]
ADD CONSTRAINT [FK_UserDPWorkflow]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDPWorkflow'
CREATE INDEX [IX_FK_UserDPWorkflow]
ON [dbo].[DPWorkflows]
    ([UserId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgDocTyps'
ALTER TABLE [dbo].[OrgDocTyps]
ADD CONSTRAINT [FK_OrgOrgDocTyp]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgDocTyp'
CREATE INDEX [IX_FK_OrgOrgDocTyp]
ON [dbo].[OrgDocTyps]
    ([OrgId]);
GO

-- Creating foreign key on [DataElmTypId] in table 'DataElms'
ALTER TABLE [dbo].[DataElms]
ADD CONSTRAINT [FK_DataElmDataElmTyp]
    FOREIGN KEY ([DataElmTypId])
    REFERENCES [dbo].[DataElmTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DataElmDataElmTyp'
CREATE INDEX [IX_FK_DataElmDataElmTyp]
ON [dbo].[DataElms]
    ([DataElmTypId]);
GO

-- Creating foreign key on [OrgDocTypId] in table 'OrgDocTypDataElms'
ALTER TABLE [dbo].[OrgDocTypDataElms]
ADD CONSTRAINT [FK_OrgDocTypOrgDocTypDataElm]
    FOREIGN KEY ([OrgDocTypId])
    REFERENCES [dbo].[OrgDocTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgDocTypOrgDocTypDataElm'
CREATE INDEX [IX_FK_OrgDocTypOrgDocTypDataElm]
ON [dbo].[OrgDocTypDataElms]
    ([OrgDocTypId]);
GO

-- Creating foreign key on [DataElmId] in table 'OrgDocTypDataElms'
ALTER TABLE [dbo].[OrgDocTypDataElms]
ADD CONSTRAINT [FK_DataElmOrgDocTypDataElm]
    FOREIGN KEY ([DataElmId])
    REFERENCES [dbo].[DataElms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DataElmOrgDocTypDataElm'
CREATE INDEX [IX_FK_DataElmOrgDocTypDataElm]
ON [dbo].[OrgDocTypDataElms]
    ([DataElmId]);
GO

-- Creating foreign key on [OrgId] in table 'soUploads'
ALTER TABLE [dbo].[soUploads]
ADD CONSTRAINT [FK_DocOrg]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocOrg'
CREATE INDEX [IX_FK_DocOrg]
ON [dbo].[soUploads]
    ([OrgId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgUsers'
ALTER TABLE [dbo].[OrgUsers]
ADD CONSTRAINT [FK_OrgOrgUser]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgUser'
CREATE INDEX [IX_FK_OrgOrgUser]
ON [dbo].[OrgUsers]
    ([OrgId]);
GO

-- Creating foreign key on [UserId] in table 'OrgUsers'
ALTER TABLE [dbo].[OrgUsers]
ADD CONSTRAINT [FK_UserOrgUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOrgUser'
CREATE INDEX [IX_FK_UserOrgUser]
ON [dbo].[OrgUsers]
    ([UserId]);
GO

-- Creating foreign key on [AuthItemId] in table 'Mods'
ALTER TABLE [dbo].[Mods]
ADD CONSTRAINT [FK_AuthItemMod]
    FOREIGN KEY ([AuthItemId])
    REFERENCES [dbo].[AuthItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthItemMod'
CREATE INDEX [IX_FK_AuthItemMod]
ON [dbo].[Mods]
    ([AuthItemId]);
GO

-- Creating foreign key on [AuthRolId] in table 'UserAuthRols'
ALTER TABLE [dbo].[UserAuthRols]
ADD CONSTRAINT [FK_AuthRolUserAuthRol]
    FOREIGN KEY ([AuthRolId])
    REFERENCES [dbo].[AuthRols]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthRolUserAuthRol'
CREATE INDEX [IX_FK_AuthRolUserAuthRol]
ON [dbo].[UserAuthRols]
    ([AuthRolId]);
GO

-- Creating foreign key on [UserId] in table 'UserAuthRols'
ALTER TABLE [dbo].[UserAuthRols]
ADD CONSTRAINT [FK_UserUserAuthRol]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserAuthRol'
CREATE INDEX [IX_FK_UserUserAuthRol]
ON [dbo].[UserAuthRols]
    ([UserId]);
GO

-- Creating foreign key on [WkflowStatId] in table 'WkflowStepHists'
ALTER TABLE [dbo].[WkflowStepHists]
ADD CONSTRAINT [FK_WkflowStatWkflowStepHist]
    FOREIGN KEY ([WkflowStatId])
    REFERENCES [dbo].[WkflowStats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowStatWkflowStepHist'
CREATE INDEX [IX_FK_WkflowStatWkflowStepHist]
ON [dbo].[WkflowStepHists]
    ([WkflowStatId]);
GO

-- Creating foreign key on [OrgTypId] in table 'Orgs'
ALTER TABLE [dbo].[Orgs]
ADD CONSTRAINT [FK_OrgTypOrg]
    FOREIGN KEY ([OrgTypId])
    REFERENCES [dbo].[OrgTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgTypOrg'
CREATE INDEX [IX_FK_OrgTypOrg]
ON [dbo].[Orgs]
    ([OrgTypId]);
GO

-- Creating foreign key on [PackageId] in table 'OrgPackages'
ALTER TABLE [dbo].[OrgPackages]
ADD CONSTRAINT [FK_PackageOrgPackage]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackageOrgPackage'
CREATE INDEX [IX_FK_PackageOrgPackage]
ON [dbo].[OrgPackages]
    ([PackageId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgPackages'
ALTER TABLE [dbo].[OrgPackages]
ADD CONSTRAINT [FK_OrgOrgPackage]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgPackage'
CREATE INDEX [IX_FK_OrgOrgPackage]
ON [dbo].[OrgPackages]
    ([OrgId]);
GO

-- Creating foreign key on [MessageId] in table 'MessageUsers'
ALTER TABLE [dbo].[MessageUsers]
ADD CONSTRAINT [FK_MessageMessageUser]
    FOREIGN KEY ([MessageId])
    REFERENCES [dbo].[Messages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageMessageUser'
CREATE INDEX [IX_FK_MessageMessageUser]
ON [dbo].[MessageUsers]
    ([MessageId]);
GO

-- Creating foreign key on [UserId] in table 'MessageUsers'
ALTER TABLE [dbo].[MessageUsers]
ADD CONSTRAINT [FK_UserMessageUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMessageUser'
CREATE INDEX [IX_FK_UserMessageUser]
ON [dbo].[MessageUsers]
    ([UserId]);
GO

-- Creating foreign key on [OrgPackageId] in table 'OrgPackageHists'
ALTER TABLE [dbo].[OrgPackageHists]
ADD CONSTRAINT [FK_OrgPackageOrgPackageHist]
    FOREIGN KEY ([OrgPackageId])
    REFERENCES [dbo].[OrgPackages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgPackageOrgPackageHist'
CREATE INDEX [IX_FK_OrgPackageOrgPackageHist]
ON [dbo].[OrgPackageHists]
    ([OrgPackageId]);
GO

-- Creating foreign key on [OrgId] in table 'WkflowInstances'
ALTER TABLE [dbo].[WkflowInstances]
ADD CONSTRAINT [FK_OrgWkflowInstance]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgWkflowInstance'
CREATE INDEX [IX_FK_OrgWkflowInstance]
ON [dbo].[WkflowInstances]
    ([OrgId]);
GO

-- Creating foreign key on [UserId] in table 'WkflowInstances'
ALTER TABLE [dbo].[WkflowInstances]
ADD CONSTRAINT [FK_WkflowInstanceUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowInstanceUser'
CREATE INDEX [IX_FK_WkflowInstanceUser]
ON [dbo].[WkflowInstances]
    ([UserId]);
GO

-- Creating foreign key on [OrgDocTypId] in table 'soUploads'
ALTER TABLE [dbo].[soUploads]
ADD CONSTRAINT [FK_DocOrgDocTyp]
    FOREIGN KEY ([OrgDocTypId])
    REFERENCES [dbo].[OrgDocTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocOrgDocTyp'
CREATE INDEX [IX_FK_DocOrgDocTyp]
ON [dbo].[soUploads]
    ([OrgDocTypId]);
GO

-- Creating foreign key on [OrgTypOrgStatusId] in table 'OrgStatusHists'
ALTER TABLE [dbo].[OrgStatusHists]
ADD CONSTRAINT [FK_OrgTypOrgStatusOrgStatusHist]
    FOREIGN KEY ([OrgTypOrgStatusId])
    REFERENCES [dbo].[OrgTypOrgStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgTypOrgStatusOrgStatusHist'
CREATE INDEX [IX_FK_OrgTypOrgStatusOrgStatusHist]
ON [dbo].[OrgStatusHists]
    ([OrgTypOrgStatusId]);
GO

-- Creating foreign key on [SettingId] in table 'PortSettings'
ALTER TABLE [dbo].[PortSettings]
ADD CONSTRAINT [FK_SettingPortSetting]
    FOREIGN KEY ([SettingId])
    REFERENCES [dbo].[Settings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingPortSetting'
CREATE INDEX [IX_FK_SettingPortSetting]
ON [dbo].[PortSettings]
    ([SettingId]);
GO

-- Creating foreign key on [PortId] in table 'PortSettings'
ALTER TABLE [dbo].[PortSettings]
ADD CONSTRAINT [FK_PortPortSetting]
    FOREIGN KEY ([PortId])
    REFERENCES [dbo].[Ports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortPortSetting'
CREATE INDEX [IX_FK_PortPortSetting]
ON [dbo].[PortSettings]
    ([PortId]);
GO

-- Creating foreign key on [PortSettingId] in table 'PortSettingValues'
ALTER TABLE [dbo].[PortSettingValues]
ADD CONSTRAINT [FK_PortSettingPortSettingValue]
    FOREIGN KEY ([PortSettingId])
    REFERENCES [dbo].[PortSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortSettingPortSettingValue'
CREATE INDEX [IX_FK_PortSettingPortSettingValue]
ON [dbo].[PortSettingValues]
    ([PortSettingId]);
GO

-- Creating foreign key on [CurrStatusId] in table 'OrgDocTyps'
ALTER TABLE [dbo].[OrgDocTyps]
ADD CONSTRAINT [FK_DocTypStatusOrgDocTyp]
    FOREIGN KEY ([CurrStatusId])
    REFERENCES [dbo].[DocTypStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocTypStatusOrgDocTyp'
CREATE INDEX [IX_FK_DocTypStatusOrgDocTyp]
ON [dbo].[OrgDocTyps]
    ([CurrStatusId]);
GO

-- Creating foreign key on [CreatedUserId] in table 'OrgDocTyps'
ALTER TABLE [dbo].[OrgDocTyps]
ADD CONSTRAINT [FK_UserOrgDocTyp]
    FOREIGN KEY ([CreatedUserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOrgDocTyp'
CREATE INDEX [IX_FK_UserOrgDocTyp]
ON [dbo].[OrgDocTyps]
    ([CreatedUserId]);
GO

-- Creating foreign key on [InactivatedUserId] in table 'OrgDocTyps'
ALTER TABLE [dbo].[OrgDocTyps]
ADD CONSTRAINT [FK_UserOrgDocTyp1]
    FOREIGN KEY ([InactivatedUserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOrgDocTyp1'
CREATE INDEX [IX_FK_UserOrgDocTyp1]
ON [dbo].[OrgDocTyps]
    ([InactivatedUserId]);
GO

-- Creating foreign key on [ModifiedUserId] in table 'OrgDocTyps'
ALTER TABLE [dbo].[OrgDocTyps]
ADD CONSTRAINT [FK_UserOrgDocTyp2]
    FOREIGN KEY ([ModifiedUserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOrgDocTyp2'
CREATE INDEX [IX_FK_UserOrgDocTyp2]
ON [dbo].[OrgDocTyps]
    ([ModifiedUserId]);
GO

-- Creating foreign key on [PortId] in table 'MessageTemplates'
ALTER TABLE [dbo].[MessageTemplates]
ADD CONSTRAINT [FK_PortMessageTemplate]
    FOREIGN KEY ([PortId])
    REFERENCES [dbo].[Ports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PortMessageTemplate'
CREATE INDEX [IX_FK_PortMessageTemplate]
ON [dbo].[MessageTemplates]
    ([PortId]);
GO

-- Creating foreign key on [WkflowInstanceId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_WkflowInstanceMessage]
    FOREIGN KEY ([WkflowInstanceId])
    REFERENCES [dbo].[WkflowInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowInstanceMessage'
CREATE INDEX [IX_FK_WkflowInstanceMessage]
ON [dbo].[Messages]
    ([WkflowInstanceId]);
GO

-- Creating foreign key on [DeliveryMethodId] in table 'MessageUsers'
ALTER TABLE [dbo].[MessageUsers]
ADD CONSTRAINT [FK_DeliveryMethodMessageUser]
    FOREIGN KEY ([DeliveryMethodId])
    REFERENCES [dbo].[DeliveryMethods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryMethodMessageUser'
CREATE INDEX [IX_FK_DeliveryMethodMessageUser]
ON [dbo].[MessageUsers]
    ([DeliveryMethodId]);
GO

-- Creating foreign key on [CreatedUserId] in table 'WkflowStepHists'
ALTER TABLE [dbo].[WkflowStepHists]
ADD CONSTRAINT [FK_WkflowStepHistUser]
    FOREIGN KEY ([CreatedUserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowStepHistUser'
CREATE INDEX [IX_FK_WkflowStepHistUser]
ON [dbo].[WkflowStepHists]
    ([CreatedUserId]);
GO

-- Creating foreign key on [CCUserId] in table 'WkflowInstances'
ALTER TABLE [dbo].[WkflowInstances]
ADD CONSTRAINT [FK_UserWkflowInstance]
    FOREIGN KEY ([CCUserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserWkflowInstance'
CREATE INDEX [IX_FK_UserWkflowInstance]
ON [dbo].[WkflowInstances]
    ([CCUserId]);
GO

-- Creating foreign key on [WkflowInstanceId] in table 'OrgDocTyps'
ALTER TABLE [dbo].[OrgDocTyps]
ADD CONSTRAINT [FK_OrgDocTypWkflowInstance]
    FOREIGN KEY ([WkflowInstanceId])
    REFERENCES [dbo].[WkflowInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgDocTypWkflowInstance'
CREATE INDEX [IX_FK_OrgDocTypWkflowInstance]
ON [dbo].[OrgDocTyps]
    ([WkflowInstanceId]);
GO

-- Creating foreign key on [SowAttributeId] in table 'SowWklowSowAttributes'
ALTER TABLE [dbo].[SowWklowSowAttributes]
ADD CONSTRAINT [FK_SowAttributeSowWklowSowAttribute]
    FOREIGN KEY ([SowAttributeId])
    REFERENCES [dbo].[SowAttributes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SowAttributeSowWklowSowAttribute'
CREATE INDEX [IX_FK_SowAttributeSowWklowSowAttribute]
ON [dbo].[SowWklowSowAttributes]
    ([SowAttributeId]);
GO

-- Creating foreign key on [SowWkflowId] in table 'SowWklowSowAttributes'
ALTER TABLE [dbo].[SowWklowSowAttributes]
ADD CONSTRAINT [FK_SowWkflowSowWklowSowAttribute]
    FOREIGN KEY ([SowWkflowId])
    REFERENCES [dbo].[SowWkflows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SowWkflowSowWklowSowAttribute'
CREATE INDEX [IX_FK_SowWkflowSowWklowSowAttribute]
ON [dbo].[SowWklowSowAttributes]
    ([SowWkflowId]);
GO

-- Creating foreign key on [SowWkflowId] in table 'SowWkflowDocSetups'
ALTER TABLE [dbo].[SowWkflowDocSetups]
ADD CONSTRAINT [FK_SowWkflowSowWkflowDocSetup]
    FOREIGN KEY ([SowWkflowId])
    REFERENCES [dbo].[SowWkflows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SowWkflowSowWkflowDocSetup'
CREATE INDEX [IX_FK_SowWkflowSowWkflowDocSetup]
ON [dbo].[SowWkflowDocSetups]
    ([SowWkflowId]);
GO

-- Creating foreign key on [OrgResellerId] in table 'OrgResellerDiscHists'
ALTER TABLE [dbo].[OrgResellerDiscHists]
ADD CONSTRAINT [FK_OrgResellerOrgResellerDiscHist]
    FOREIGN KEY ([OrgResellerId])
    REFERENCES [dbo].[OrgResellers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgResellerOrgResellerDiscHist'
CREATE INDEX [IX_FK_OrgResellerOrgResellerDiscHist]
ON [dbo].[OrgResellerDiscHists]
    ([OrgResellerId]);
GO

-- Creating foreign key on [SowWkflowId] in table 'SOWWkflowOrgResellerDiscOverrides'
ALTER TABLE [dbo].[SOWWkflowOrgResellerDiscOverrides]
ADD CONSTRAINT [FK_SowWkflowSOWWkflowOrgResellerDiscOverride]
    FOREIGN KEY ([SowWkflowId])
    REFERENCES [dbo].[SowWkflows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SowWkflowSOWWkflowOrgResellerDiscOverride'
CREATE INDEX [IX_FK_SowWkflowSOWWkflowOrgResellerDiscOverride]
ON [dbo].[SOWWkflowOrgResellerDiscOverrides]
    ([SowWkflowId]);
GO

-- Creating foreign key on [OrgResellerId] in table 'SOWWkflowOrgResellerDiscOverrides'
ALTER TABLE [dbo].[SOWWkflowOrgResellerDiscOverrides]
ADD CONSTRAINT [FK_OrgResellerSOWWkflowOrgResellerDiscOverride]
    FOREIGN KEY ([OrgResellerId])
    REFERENCES [dbo].[OrgResellers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgResellerSOWWkflowOrgResellerDiscOverride'
CREATE INDEX [IX_FK_OrgResellerSOWWkflowOrgResellerDiscOverride]
ON [dbo].[SOWWkflowOrgResellerDiscOverrides]
    ([OrgResellerId]);
GO

-- Creating foreign key on [WkflowInstanceId] in table 'SowWkflows'
ALTER TABLE [dbo].[SowWkflows]
ADD CONSTRAINT [FK_WkflowInstanceSowWkflow]
    FOREIGN KEY ([WkflowInstanceId])
    REFERENCES [dbo].[WkflowInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WkflowInstanceSowWkflow'
CREATE INDEX [IX_FK_WkflowInstanceSowWkflow]
ON [dbo].[SowWkflows]
    ([WkflowInstanceId]);
GO

-- Creating foreign key on [SowWklowSowAttributeId] in table 'SowAttributeValueHists'
ALTER TABLE [dbo].[SowAttributeValueHists]
ADD CONSTRAINT [FK_SowWklowSowAttributeSowAttributeValueHist]
    FOREIGN KEY ([SowWklowSowAttributeId])
    REFERENCES [dbo].[SowWklowSowAttributes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SowWklowSowAttributeSowAttributeValueHist'
CREATE INDEX [IX_FK_SowWklowSowAttributeSowAttributeValueHist]
ON [dbo].[SowAttributeValueHists]
    ([SowWklowSowAttributeId]);
GO

-- Creating foreign key on [MonthId] in table 'OrgDocTypMonths'
ALTER TABLE [dbo].[OrgDocTypMonths]
ADD CONSTRAINT [FK_MonthOrgDocTypMonth]
    FOREIGN KEY ([MonthId])
    REFERENCES [dbo].[Months]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MonthOrgDocTypMonth'
CREATE INDEX [IX_FK_MonthOrgDocTypMonth]
ON [dbo].[OrgDocTypMonths]
    ([MonthId]);
GO

-- Creating foreign key on [OrgDocTypId] in table 'OrgDocTypMonths'
ALTER TABLE [dbo].[OrgDocTypMonths]
ADD CONSTRAINT [FK_OrgDocTypOrgDocTypMonth]
    FOREIGN KEY ([OrgDocTypId])
    REFERENCES [dbo].[OrgDocTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgDocTypOrgDocTypMonth'
CREATE INDEX [IX_FK_OrgDocTypOrgDocTypMonth]
ON [dbo].[OrgDocTypMonths]
    ([OrgDocTypId]);
GO

-- Creating foreign key on [OrgDocTypId] in table 'SowWkflowDocSetups'
ALTER TABLE [dbo].[SowWkflowDocSetups]
ADD CONSTRAINT [FK_OrgDocTypSowWkflowDocSetup]
    FOREIGN KEY ([OrgDocTypId])
    REFERENCES [dbo].[OrgDocTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgDocTypSowWkflowDocSetup'
CREATE INDEX [IX_FK_OrgDocTypSowWkflowDocSetup]
ON [dbo].[SowWkflowDocSetups]
    ([OrgDocTypId]);
GO

-- Creating foreign key on [OrgId] in table 'SubWhiteLists'
ALTER TABLE [dbo].[SubWhiteLists]
ADD CONSTRAINT [FK_OrgSubWhiteList]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgSubWhiteList'
CREATE INDEX [IX_FK_OrgSubWhiteList]
ON [dbo].[SubWhiteLists]
    ([OrgId]);
GO

-- Creating foreign key on [MonthId] in table 'OrgMonthCommitments'
ALTER TABLE [dbo].[OrgMonthCommitments]
ADD CONSTRAINT [FK_MonthOrgMonthCommitment]
    FOREIGN KEY ([MonthId])
    REFERENCES [dbo].[Months]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MonthOrgMonthCommitment'
CREATE INDEX [IX_FK_MonthOrgMonthCommitment]
ON [dbo].[OrgMonthCommitments]
    ([MonthId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgMonthCommitments'
ALTER TABLE [dbo].[OrgMonthCommitments]
ADD CONSTRAINT [FK_OrgOrgMonthCommitment]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgMonthCommitment'
CREATE INDEX [IX_FK_OrgOrgMonthCommitment]
ON [dbo].[OrgMonthCommitments]
    ([OrgId]);
GO

-- Creating foreign key on [OrgDocTypId] in table 'OrgDocTypDailyUploads'
ALTER TABLE [dbo].[OrgDocTypDailyUploads]
ADD CONSTRAINT [FK_OrgDocTypOrgDocTypDailyUpload]
    FOREIGN KEY ([OrgDocTypId])
    REFERENCES [dbo].[OrgDocTyps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgDocTypOrgDocTypDailyUpload'
CREATE INDEX [IX_FK_OrgDocTypOrgDocTypDailyUpload]
ON [dbo].[OrgDocTypDailyUploads]
    ([OrgDocTypId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgDocTypDailyUploads'
ALTER TABLE [dbo].[OrgDocTypDailyUploads]
ADD CONSTRAINT [FK_OrgOrgDocTypDailyUpload]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgDocTypDailyUpload'
CREATE INDEX [IX_FK_OrgOrgDocTypDailyUpload]
ON [dbo].[OrgDocTypDailyUploads]
    ([OrgId]);
GO

-- Creating foreign key on [OrgId] in table 'OrgDocTypMonths'
ALTER TABLE [dbo].[OrgDocTypMonths]
ADD CONSTRAINT [FK_OrgOrgDocTypMonth]
    FOREIGN KEY ([OrgId])
    REFERENCES [dbo].[Orgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrgOrgDocTypMonth'
CREATE INDEX [IX_FK_OrgOrgDocTypMonth]
ON [dbo].[OrgDocTypMonths]
    ([OrgId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------