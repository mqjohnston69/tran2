using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Oc.Carbon.DTO;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DTO.SolutionDTO;
using Oc.Carbon.DTO.PlatformDTO;

namespace Oc.Carbon.DTO.Mapping.Core
{
    /// <summary>
    /// Platform specific mapper definitions for mapping DTO's to domain objects and vice versa.  Leverages
    /// the Mapper for core implementation.
    /// </summary>
    public static class PlatformMappingHelper
    {
        static PlatformMappingHelper()
        {
            CreateMaps();
        }

        private static void CreateMaps()
        {
            MapDomainToDtoObjects();
            MapDtoToDomainObjects();
        }

        private static void MapDtoToDomainObjects()
        {

            //#region DocTyp
            //var dataElmTypDTOMapTodataElmTyp = Mapper.CreateMap<DataElmTypDTO, DataElmTyp>();
            //var DataElmMapTodataElmDTO = Mapper.CreateMap<DataElmDTO, DataElm>();
            //DataElmMapTodataElmDTO.ForMember(dest => dest.DataElmTyp, opt => opt.MapFrom(src => src.DataElmTyp));
            //var dataElmDTOTOMapOrgDocTypDataElm = Mapper.CreateMap<DataElmDTO, OrgDocTypDataElm>();
            //var docTypDTOTOMapOrgDocTyp = Mapper.CreateMap<DocTypDTO, OrgDocTyp>();
            //docTypDTOTOMapOrgDocTyp.ForMember(dest => dest.OrgDocTypDataElms, opt => opt.MapFrom(src => src.DataElms));
            //dataElmDTOTOMapOrgDocTypDataElm.ForMember(dest => dest.DataElm, opt => opt.MapFrom(src => new DataElm { Id = src.Id, Name = src.Name, Descript = src.Descript, DataElmTypId = src.DataElmTypId, DataElmTyp = new DataElmTyp { Id = src.DataElmTyp.Id, Name = src.DataElmTyp.Name, Descript= src.DataElmTyp.Descript, DataElmTypCd= src.DataElmTyp.DataElmTypCd} }));

            //#endregion

        }

        private static void MapDomainToDtoObjects()
        {
            var contactTpyToContactTypDtoMap = Mapper.CreateMap<ContactTyp, ContactTypDTO>();

            var contactToContactpDtoMap = Mapper.CreateMap<Contact, ContactDTO>();

            var locnToLocnDtoMap = Mapper.CreateMap<Locn, LocnDTO>();

            var orgContactToContactDtoMap = Mapper.CreateMap<OrgContact, ContactDTO>();
            orgContactToContactDtoMap.ForMember(dest => dest.Ext, opt => opt.MapFrom(src => src.Contact.Ext));
            orgContactToContactDtoMap.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Contact.Value));
            orgContactToContactDtoMap.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Contact.Id));
            orgContactToContactDtoMap.ForMember(dest => dest.ContactTyp, opt => opt.MapFrom(src => src.Contact.ContactTyp));


            var orgLocnToOrgLocnDtoMap = Mapper.CreateMap<OrgLocn, LocnDTO>();
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Locn.AddressLine1));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Locn.AddressLine2));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Locn.City));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Locn.CountryId));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Locn.CreateDate));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.DateLastMaint, opt => opt.MapFrom(src => src.Locn.DateLastMaint));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.GeoLocationData, opt => opt.MapFrom(src => src.Locn.GeoLocationData));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Locn.State));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Locn.Id));
            orgLocnToOrgLocnDtoMap.ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Locn.ZipCode));


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var portToPortalDTOMap = Mapper.CreateMap<Port, PortalDTO>();
            portToPortalDTOMap.ForMember(dest => dest.PortalModules, opt => opt.MapFrom(src => src.PortMods));

            var portModToPortalModuleDTOMap = Mapper.CreateMap<PortMod, PortalModuleDTO>();
            portModToPortalModuleDTOMap.ForMember(dest => dest.Module, opt => opt.MapFrom(src => src.Mod));
            portModToPortalModuleDTOMap.ForMember(dest => dest.Menu, opt => opt.MapFrom(src => src.Menu));


            var modTypToModuleTypDTOMap = Mapper.CreateMap<ModTyp, ModuleTypDTO>();

            var modToModuleDTOMap = Mapper.CreateMap<Mod, ModuleDTO>();
            modToModuleDTOMap.ForMember(dest => dest.ModuleTyp, opt => opt.MapFrom(src => src.ModTyp));
            modToModuleDTOMap.ForMember(dest => dest.States, opt => opt.MapFrom(src => src.ModStates));

            var modStateToModuleStateDTOMap = Mapper.CreateMap<ModState, ModuleStateDTO>();
            


            var portModToModuleDTOMap = Mapper.CreateMap<PortMod, ModuleDTO>();

            var menuToMenuDTOMap = Mapper.CreateMap<Menu, MenuDTO>();

            var menuItemToMenuItemDTOMap = Mapper.CreateMap<MenuItem, MenuItemDTO>();
            menuItemToMenuItemDTOMap.ForMember(dest => dest.ModuleState, opt => opt.MapFrom(src => src.ModState));

            var iconVisualToIconVisualDTOMap = Mapper.CreateMap<IconVisual, IconVisualDTO>();

            var iconVisualTypToIconVisualTypDTOMap = Mapper.CreateMap<IconVisualTyp, IconVisualTypDTO>();

            var portalUserToPortalUserDTOMap = Mapper.CreateMap<User, PortalUserDTO>();
            portalUserToPortalUserDTOMap.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Per.FirstName));
            portalUserToPortalUserDTOMap.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Per.LastName));
            portalUserToPortalUserDTOMap.ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.Per.DOB));
            portalUserToPortalUserDTOMap.ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Per.MiddleName));
            portalUserToPortalUserDTOMap.ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Per.GenderId));

            var userToUserDTOMap = Mapper.CreateMap<User, PlatformDTO.UserDTO>();
            var authItemToAuthItemDTOMap = Mapper.CreateMap<AuthItem, AuthItemDTO>();
            var authPermToAuthPermDTOMap = Mapper.CreateMap<AuthPerm, AuthPermDTO>();
            var AuthItemAuthPermToAuthItemAuthPermDTOMap = Mapper.CreateMap<AuthItemAuthPerm, AuthItemAuthPermDTO>();
            var authRolToAuthRolDTOMap = Mapper.CreateMap<AuthRol, AuthRolDTO>();
            var authRolTypToAuthRolTypDTOMap = Mapper.CreateMap<AuthRolTyp, AuthRolTypDTO>();


            #region User DTO
            var userToMapPlatUserDTO = Mapper.CreateMap<User, SolutionDTO.UserDTO>();
            userToMapPlatUserDTO.ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.Per.FirstName));
            userToMapPlatUserDTO.ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.Per.LastName));
            userToMapPlatUserDTO.ForMember(dest => dest.middleName, opt => opt.MapFrom(src => src.Per.MiddleName));
            userToMapPlatUserDTO.ForMember(dest => dest.password, opt => opt.MapFrom(src => ""));
            userToMapPlatUserDTO.ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Per.Title));

            userToMapPlatUserDTO.ForMember(dest => dest.lastLogin, opt => opt.MapFrom(src => src.UserLoginHists.Count()>0?src.UserLoginHists.Last().LoginDate:null));
            userToMapPlatUserDTO.ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.Per.PersContacts.Where(p=>p.Contact.ContactTyp.ContactTypCd=="PHON").FirstOrDefault().Contact.Value));

            userToMapPlatUserDTO.ForMember(dest => dest.orgId, opt => opt.MapFrom(src => src.OrgUsers.Where(s=>s.Type==null || s.Type.Contains("Primary")).FirstOrDefault().OrgId));
            userToMapPlatUserDTO.ForMember(dest => dest.orgName, opt => opt.MapFrom(src => src.OrgUsers.Where(s => s.Type == null || s.Type.Contains("Primary")).FirstOrDefault().Org.Name));
            userToMapPlatUserDTO.ForMember(dest => dest.roleId, opt => opt.MapFrom(src => src.UserAuthRols.FirstOrDefault().AuthRolId));
            userToMapPlatUserDTO.ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.UserAuthRols.FirstOrDefault().AuthRol.Name));
            #endregion


            var AuthRolAuthItemAuthPermToAuthRolAuthItemAuthPermDTOMap = Mapper.CreateMap<AuthRolAuthItemAuthPerm, AuthRolAuthItemAuthPermDTO>();
            

            var perToPerDTOMap = Mapper.CreateMap<Per, PerDTO>();

            var userAuthRolToUserAuthRolDTOMap = Mapper.CreateMap<UserAuthRol, UserAuthRolDTO>();

            var orgToOrgDTOMap = Mapper.CreateMap<Org, OrgDTO>();
            var orgToOrgTreeDTOMap = Mapper.CreateMap<Org, OrgTreeDTO>();
            var orgTypTorgTypDTOMap = Mapper.CreateMap<OrgTyp, OrgTypDTO>();

            #region Message
            var messageUserToMapMessageDTO = Mapper.CreateMap<MessageUser, MessageDTO>();
            messageUserToMapMessageDTO.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MessageId));
            messageUserToMapMessageDTO.ForMember(dest => dest.HeaderText, opt => opt.MapFrom(src => src.Message.HeaderText));
            messageUserToMapMessageDTO.ForMember(dest => dest.MessageBody, opt => opt.MapFrom(src => src.Message.MessageBody));
            messageUserToMapMessageDTO.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.UserName));
            #endregion

            #region DocTyp
            var DocTypStatusMapToDataTypStatusDTO = Mapper.CreateMap<DocTypStatus, DocTypStatusDTO>();
            var dataElmTypDTOToDataElmTypDTOMap = Mapper.CreateMap<DataElmTyp, DataElmTypDTO>();
            var dataElmDTOToDataElmDTOMap = Mapper.CreateMap<DataElm, DataElmDTO>();
            dataElmDTOToDataElmDTOMap.ForMember(dest => dest.DataElmTyp, opt => opt.MapFrom(src => src.DataElmTyp));
            var orgDocTypDataElmDTOToDataElmDTOMap = Mapper.CreateMap<OrgDocTypDataElm, DataElmDTO>();
            var orgDocTypToDocTypDTOMap = Mapper.CreateMap<OrgDocTyp, DocTypDTO>();
            orgDocTypToDocTypDTOMap.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.DocTypStatu.DisplayText));
            orgDocTypToDocTypDTOMap.ForMember(dest => dest.DocTypStatus, opt => opt.MapFrom(src => src.DocTypStatu));
            orgDocTypToDocTypDTOMap.ForMember(dest => dest.DataElms, opt => opt.MapFrom(src => src.OrgDocTypDataElms));
            orgDocTypToDocTypDTOMap.ForMember(dest => dest.DataElms, opt => opt.MapFrom(src => src.OrgDocTypDataElms.Select(p=> p.DataElm)));
            orgDocTypToDocTypDTOMap.ForMember(dest => dest.OrgName, opt => opt.MapFrom(src => src.Org.Name));
            #endregion


            var fileTypToFileTypDTOMap = Mapper.CreateMap<FileType, FileTypeDTO>();

            var soUploadToMapDocDTO = Mapper.CreateMap<soUpload, DocDTO>();

            var wkflowInstanceDocDTOToWkflowInstanceDocDTOMap = Mapper.CreateMap<WkflowInstanceDoc, WkflowInstanceDocDTO>();

            var wkflowInstanceToWkflowInstanceDTOMap = Mapper.CreateMap<WkflowInstance, WkflowInstanceDTO>();
            wkflowInstanceToWkflowInstanceDTOMap.ForMember(dest => dest.ChildWkflowInstances, opt => opt.MapFrom(src => src.WkflowInstances1));


            var wkflowStepHistToWkflowStepHistDTOMap = Mapper.CreateMap<WkflowStepHist, WkflowStepHistDTO>();
            var wkflowStatReaToWkflowStatReaDTOMap = Mapper.CreateMap<WkflowStatRea, WkflowStatReaDTO>();
            var WkflowStepNoteToWkflowStepNoteDTOMap = Mapper.CreateMap<WkflowStepNote, WkflowStepNoteDTO>();




            #region UploadActivity

            var wkflowStatToWkflowStatDTOMap = Mapper.CreateMap<WkflowStat, WkflowStatDTO>();


            var pPWorkflowToDPWorkflowDTOMap = Mapper.CreateMap<WkflowInstance, DPWorkflowDTO>();
            pPWorkflowToDPWorkflowDTOMap.ForMember(dest => dest.StartIndex, opt => opt.MapFrom(src => src.WkflowInstanceDocs.FirstOrDefault().Doc.StartIndex));
            pPWorkflowToDPWorkflowDTOMap.ForMember(dest => dest.EndIndex, opt => opt.MapFrom(src => src.WkflowInstanceDocs.FirstOrDefault().Doc.EndIndex));
            pPWorkflowToDPWorkflowDTOMap.ForMember(dest => dest.ImageCount, opt => opt.MapFrom(src => src.WkflowInstanceDocs.FirstOrDefault().Doc.soPages));
            pPWorkflowToDPWorkflowDTOMap.ForMember(dest => dest.ParentFileName, opt => opt.MapFrom(src => src.WkflowInstance1.WkflowInstanceDocs.FirstOrDefault().Doc.soFileName));
            pPWorkflowToDPWorkflowDTOMap.ForMember(dest => dest.docType, opt => opt.MapFrom(src => src.WkflowInstanceDocs.FirstOrDefault().Doc.OrgDocTyp));
            pPWorkflowToDPWorkflowDTOMap.ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStat));
            pPWorkflowToDPWorkflowDTOMap.ForMember(dest => dest.CurrentReason, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStatRea));
            pPWorkflowToDPWorkflowDTOMap.ForMember(dest => dest.CurrentNote, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStepNotes.FirstOrDefault()));

            var WkflowInstanceToBatchProcessingWorkflowDTOMap = Mapper.CreateMap<WkflowInstance, BatchProcessingWorkflowDTO>();
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending (s=>s.Id).FirstOrDefault().WkflowStat));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.CurrentReason, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStatRea));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.CurrentNote, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStepNotes.FirstOrDefault()));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.UploadDate, opt => opt.MapFrom(src => src.CreateDate));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.WkflowInstanceDocs.FirstOrDefault().Doc.Name));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.SourceFile, opt => opt.MapFrom(src => src.WkflowInstance1.WkflowInstanceDocs.FirstOrDefault().Doc.Name));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.ImageCount, opt => opt.MapFrom(src => src.WkflowInstanceDocs.FirstOrDefault().Doc.soPages));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.Wip, opt => opt.MapFrom(src => src.WkflowInstances1.Where(p => p.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStat.Code == "INPR").ToList().Count));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.Exceptions, opt => opt.MapFrom(src => src.WkflowInstances1.Where(p => p.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStat.Code == "EXCP").ToList().Count));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.WkflowInstances1.Where(p => p.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStat.Code == "CMPL").ToList().Count));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStat.Color));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.FileExt, opt => opt.MapFrom(src => src.WkflowInstanceDocs.FirstOrDefault().Doc.FileExt));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.ChildWkflowInstances, opt => opt.MapFrom(src => src.WkflowInstances1));
            WkflowInstanceToBatchProcessingWorkflowDTOMap.ForMember(dest => dest.docType, opt => opt.MapFrom(src => src.WkflowInstanceDocs.FirstOrDefault().Doc.OrgDocTyp));


            #endregion

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            var PackageToPackageDTOMap = Mapper.CreateMap<Package, PackageDTO>();

            #region ReSeller

            var OrgResellerDiscHistToRebateDTOMap = Mapper.CreateMap<OrgResellerDiscHist, RebateDTO>();
            OrgResellerDiscHistToRebateDTOMap.ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src.EffectiveDate));
            OrgResellerDiscHistToRebateDTOMap.ForMember(dest => dest.InActiveDate, opt => opt.MapFrom(src => src.InActiveDate));
            OrgResellerDiscHistToRebateDTOMap.ForMember(dest => dest.AgreementNumber, opt => opt.MapFrom(src => src.AgreementNum));
            OrgResellerDiscHistToRebateDTOMap.ForMember(dest => dest.AmendId, opt => opt.MapFrom(src => src.Amend));
            OrgResellerDiscHistToRebateDTOMap.ForMember(dest => dest.AnnualRevenue, opt => opt.MapFrom(src => src.AnnualRevenue));
            OrgResellerDiscHistToRebateDTOMap.ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));
            OrgResellerDiscHistToRebateDTOMap.ForMember(dest => dest.PDFDoc, opt => opt.MapFrom(src => src.PDFDoc));
            OrgResellerDiscHistToRebateDTOMap.ForMember(dest => dest.ResellerId, opt => opt.MapFrom(src => src.OrgReseller.Org.Id));


            var OrgResellerToResellerDTOMap = Mapper.CreateMap<OrgReseller, ResellerDTO>();
            OrgResellerToResellerDTOMap.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Org.Id));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.CustomerNbr, opt => opt.MapFrom(src => src.Org.SOAccountNbr));

            OrgResellerToResellerDTOMap.ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.Org.OrgStatusHists.OrderByDescending (s=>s.Id).FirstOrDefault().OrgTypOrgStatusId));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Org.OrgStatusHists.OrderByDescending (s=>s.Id).FirstOrDefault().OrgTypOrgStatu.OrgStatus.DisplayText));

            OrgResellerToResellerDTOMap.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.UserName : ""));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Id : 0));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Where(b => b.Contact.ContactTyp.DisplayName == "Phone").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Where(b => b.Contact.ContactTyp.DisplayName == "Phone").FirstOrDefault().Contact.Value : "" : ""));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.SalesRep, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "SalesRep").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault().User.UserName : ""));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.CustomerCare, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "CustomerCare").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault().User.UserName : null));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.UserLoginHists.Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.UserLoginHists.OrderByDescending (s=>s.Id).FirstOrDefault().LoginDate : null : null));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.CustomerSince, opt => opt.MapFrom(src => src.Org.CreateDate));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Org.Name));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.AddressLine1));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.AddressLine2));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.City));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.State));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.ZipCode));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.FirstName : ""));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.MiddleName : ""));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.LastName : ""));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.Title : ""));

            OrgResellerToResellerDTOMap.ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Org.Logo));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.Agreement, opt => opt.MapFrom(src => src.Org.Agreement));

            OrgResellerToResellerDTOMap.ForMember(dest => dest.BillingInfo, opt => opt.MapFrom(src => src.Org.BillingInfo));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.GotAgreement, opt => opt.MapFrom(src => src.Org.GotAgreement));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.SOW, opt => opt.MapFrom(src => src.Org.SOW));

            OrgResellerToResellerDTOMap.ForMember(dest => dest.RebateHist, opt => opt.MapFrom(src => src.OrgResellerDiscHists));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.CurrentDiscount, opt => opt.MapFrom(src => src.OrgResellerDiscHists.OrderByDescending(o=>o.Amend).FirstOrDefault()));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.LastReviewDate, opt => opt.MapFrom(src => src.LastReviewDate));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.NextReviewDate, opt => opt.MapFrom(src => src.NextReviewDate));
            //OrgResellerToResellerDTOMap.ForMember(dest => dest.AnnualSubscription, opt => opt.MapFrom(src => src.Org.OrgOrgs.Sum(s=>s.Org1.WkflowInstances.Where(w=>w.WkflowDef.Code=="SOW").Sum(ss=>ss.SowWkflows.OrderByDescending(sw=>sw.Amend).FirstOrDefault().MonthlyCommitment * 12))));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.AnnualSubscription, opt => opt.MapFrom(src => src.Org.OrgReseller.OrgResellerDiscHists.Where(s=>s.InActiveDate == null).FirstOrDefault().AnnualRevenue));
            OrgResellerToResellerDTOMap.ForMember(dest => dest.ActiveCustomers, opt => opt.MapFrom(src => src.Org.OrgOrgs.Count(c=>c.Org1.InactiveDate == null)));

            #endregion


            #region Customer
            var OrgCustToCustomerDTOMap = Mapper.CreateMap<OrgCust, CustomerDTO>();
            OrgCustToCustomerDTOMap.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Org.Id));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.CustomerNbr, opt => opt.MapFrom(src => src.Org.SOAccountNbr));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.OtherAccountNbr, opt => opt.MapFrom(src => src.Org.OtherAccountNbr));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Org.Name));

            OrgCustToCustomerDTOMap.ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.Org.OrgStatusHists.OrderByDescending (s=>s.Id).FirstOrDefault().OrgTypOrgStatusId));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Org.OrgStatusHists.OrderByDescending (s=>s.Id).FirstOrDefault().OrgTypOrgStatu.OrgStatus.DisplayText));

            OrgCustToCustomerDTOMap.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.UserName : ""));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p=>p.Type=="Primary").Count()>0? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Id:0));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Where(b => b.Contact.ContactTyp.DisplayName == "Phone").Count()>0?src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Where(b=>b.Contact.ContactTyp.DisplayName=="Phone").FirstOrDefault().Contact.Value:"" : ""));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.SalesRepId, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "SalesRep").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault().UserId : (int?) null));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.SalesRep, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "SalesRep").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault().User.UserName : ""));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.CustomerCareId, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "CustomerCare").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault().UserId : (int?) null));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.CustomerCare, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "CustomerCare").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault().User.UserName : null));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.ResellerRepId, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "ResellerRep").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "ResellerRep").FirstOrDefault().UserId : (int?)null));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.ResellerRep, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "ResellerRep").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "ResellerRep").FirstOrDefault().User.UserName : null));

            OrgCustToCustomerDTOMap.ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.UserLoginHists.Count()>0? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.UserLoginHists.OrderByDescending (s=>s.Id).FirstOrDefault().LoginDate:null : null));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.CustomerSince, opt => opt.MapFrom(src => src.Org.CreateDate));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.AddressLine1));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.AddressLine2));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.City));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.State));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Org.OrgLocns.FirstOrDefault().Locn.ZipCode));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.FirstName:""));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.MiddleName : ""));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.LastName : ""));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Org.OrgUsers.Where(p => p.Type == "Primary").Count() > 0 ? src.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.Title : ""));

            OrgCustToCustomerDTOMap.ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Org.Logo));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.Agreement, opt => opt.MapFrom(src => src.Org.Agreement));

            OrgCustToCustomerDTOMap.ForMember(dest => dest.BillingInfo, opt => opt.MapFrom(src => src.Org.BillingInfo));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.GotAgreement, opt => opt.MapFrom(src => src.Org.GotAgreement));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.SOW, opt => opt.MapFrom(src => src.Org.SOW));

            OrgCustToCustomerDTOMap.ForMember(dest => dest.SubmissionOpts, opt => opt.MapFrom(src => src.Org.OrgCusts.FirstOrDefault().SubmissionOpts));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.RemoveBlank, opt => opt.MapFrom(src => src.Org.OrgCusts.FirstOrDefault().RemoveBlank));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.SLA, opt => opt.MapFrom(src => src.Org.OrgCusts.FirstOrDefault().SLA));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.ImageCleanUp, opt => opt.MapFrom(src => src.Org.ImageCleanUp));

            OrgCustToCustomerDTOMap.ForMember(dest => dest.PromoCode, opt => opt.MapFrom(src => src.Org.PromoCode));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.BillMe, opt => opt.MapFrom(src => src.Org.BillMe));

            OrgCustToCustomerDTOMap.ForMember(dest => dest.parentId, opt => opt.MapFrom(src => src.Org.OrgOrgs1.FirstOrDefault().OrgId));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.parentName, opt => opt.MapFrom(src => src.Org.OrgOrgs1.FirstOrDefault().Org.Name));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.RebateOverride, opt => opt.MapFrom(src => src.Org.WkflowInstances.Where(w=>w.WkflowDef.Code=="SOW").FirstOrDefault().SowWkflows.OrderByDescending(s=>s.Id).FirstOrDefault().SOWWkflowOrgResellerDiscOverrides.Count>0));
            OrgCustToCustomerDTOMap.ForMember(dest => dest.ResellerRebate, opt => opt.MapFrom(src => src.Org.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").FirstOrDefault().SowWkflows.OrderByDescending(s => s.Id).FirstOrDefault().SOWWkflowOrgResellerDiscOverrides.Count > 0?src.Org.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").FirstOrDefault().SowWkflows.OrderByDescending(s => s.Id).FirstOrDefault().SOWWkflowOrgResellerDiscOverrides.FirstOrDefault().Discount: src.Org.OrgOrgs1.FirstOrDefault().Org.OrgReseller.OrgResellerDiscHists.Where(o=>o.InActiveDate==null).FirstOrDefault().Discount));


            #endregion

            #region Support Activity
            var WkflowStepNoteToSupportActivityNoteDTOMap = Mapper.CreateMap<WkflowStepNote, SupportActivityNoteDTO>();
            WkflowStepNoteToSupportActivityNoteDTOMap.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            WkflowStepNoteToSupportActivityNoteDTOMap.ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedDate));
            WkflowStepNoteToSupportActivityNoteDTOMap.ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.NoteText));


            var WkflowInstanceToSupportActivityDTOMap = Mapper.CreateMap<WkflowInstance, SupportActivityDTO>();
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.SOAccountNbr, opt => opt.MapFrom(src => src.Org.SOAccountNbr));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.SubmittedBy, opt => opt.MapFrom(src => src.User.Per.LastName + "," + src.User.Per.FirstName));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.SubmittedByEmail, opt => opt.MapFrom(src => src.User.UserName));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.CCUserId, opt => opt.MapFrom(src => src.CCUserId));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.User1.Per.LastName + ", " +src.User1.Per.FirstName));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.OrgType, opt => opt.MapFrom(src => src.Org.OrgTyp.Name));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Org.Name));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending (s=>s.Id).FirstOrDefault().WkflowStat.Descript));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => src.WkflowDef.Name));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.RequestTypeId, opt => opt.MapFrom(src => src.WkflowDef.Id));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending (s=>s.Id).FirstOrDefault().CreateDate));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.ReasonId, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStatReasId));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.ReasonName, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStatRea.Descript));
            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.Otherreason, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStepNotes.FirstOrDefault().NoteText));

            WkflowInstanceToSupportActivityDTOMap.ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.WkflowStepHists.OrderByDescending(s => s.Id).Select(p => p.WkflowStepNotes.FirstOrDefault())));


            #endregion

            #region SOW
            var SowWklowSowAttributesTOAttribsMap = Mapper.CreateMap<SowWklowSowAttribute, SOWAttribDTO>();
            SowWklowSowAttributesTOAttribsMap.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SowAttributeId));
            SowWklowSowAttributesTOAttribsMap.ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.SowAttributeValueHists.FirstOrDefault().Qty));
            SowWklowSowAttributesTOAttribsMap.ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.SowAttributeValueHists.FirstOrDefault().UnitPrice));
            SowWklowSowAttributesTOAttribsMap.ForMember(dest => dest.ExtendedPrice, opt => opt.MapFrom(src => src.SowAttributeValueHists.FirstOrDefault().ExtendedPrice));
            SowWklowSowAttributesTOAttribsMap.ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src.SowAttributeValueHists.FirstOrDefault().EffectiveDate));
            SowWklowSowAttributesTOAttribsMap.ForMember(dest => dest.InactiveDate, opt => opt.MapFrom(src => src.SowAttributeValueHists.FirstOrDefault().InactiveDate));

            var SowWkflowDocSetupsTODocSetupsMap = Mapper.CreateMap<SowWkflowDocSetup,SOWDocSetupDTO>();
            SowWkflowDocSetupsTODocSetupsMap.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DocumentName));
            SowWkflowDocSetupsTODocSetupsMap.ForMember(dest => dest.NoIndexes, opt => opt.MapFrom(src => src.NoIndexes));
            SowWkflowDocSetupsTODocSetupsMap.ForMember(dest => dest.NoDataFields, opt => opt.MapFrom(src => src.NoDataFields));
            SowWkflowDocSetupsTODocSetupsMap.ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.ListPrice));
            SowWkflowDocSetupsTODocSetupsMap.ForMember(dest => dest.SLA, opt => opt.MapFrom(src => src.SLA));
            SowWkflowDocSetupsTODocSetupsMap.ForMember(dest => dest.CommitVolume, opt => opt.MapFrom(src => src.Volume));

            var SowWkflowToSOWDTOMap = Mapper.CreateMap<SowWkflow, SOWDTO>();
            SowWkflowToSOWDTOMap.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.WkflowInstance.OrgId));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.WkflowInstanceId, opt => opt.MapFrom(src => src.WkflowInstanceId));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.WkflowInstance.Org.Name));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.ResellerName, opt => opt.MapFrom(src => src.WkflowInstance.Org.OrgOrgs1.FirstOrDefault().Org.Name));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.Commitment, opt => opt.MapFrom(src => src.MonthlyCommitment));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.Rebate, opt => opt.MapFrom(src => src.MonthlyCommitment * src.WkflowInstance.Org.OrgOrgs1.FirstOrDefault().Org.OrgReseller.OrgResellerDiscHists.Where(p=>p.InActiveDate == null).FirstOrDefault().Discount/100));
            //SowWkflowToSOWDTOMap.ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src));
            //SowWkflowToSOWDTOMap.ForMember(dest => dest.InactiveDate, opt => opt.MapFrom(src => src.InactiveDate));
            //SowWkflowToSOWDTOMap.ForMember(dest => dest.NextReviewDate, opt => opt.MapFrom(src => src.NextReviewDate));
            //SowWkflowToSOWDTOMap.ForMember(dest => dest.LastReviewDate, opt => opt.MapFrom(src => src.LastReviewDate));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.PDFDoc, opt => opt.MapFrom(src => src.PDFDoc));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.Attribs, opt => opt.MapFrom(src => src.SowWklowSowAttributes));
            SowWkflowToSOWDTOMap.ForMember(dest => dest.DocSetups, opt => opt.MapFrom(src => src.SowWkflowDocSetups));

            #endregion

        }

        /// <summary>
        /// Map objects from a source to a destination instance that is provided.
        /// </summary>
        /// <typeparam name="TSource">source type</typeparam>
        /// <typeparam name="TDestination">destination type</typeparam>
        /// <param name="source">source object</param>
        /// <param name="destination">destination object</param>
        /// <returns>a reference to the destination instance</returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            try
            {
                return Mapper.Map(source, destination);
            }
            catch (AutoMapperMappingException ex)
            {
                //If the mapper exception occurred because of a domain object validation exception, unwrap that exception 
                //and propogate it.  This way the client can more easily expose a user readable validation message instead of a cryptic
                //mapper exception message.
                //if (ex.GetBaseException() is PlatformDomainValidationException)
                //{
                //    Logger.Write(ex, TraceEventType.Warning);
                //    throw ex.GetBaseException();
                //}
                throw;
            }
        }

        /// <summary>
        /// Map objects
        /// </summary>
        /// <typeparam name="TSource">source type</typeparam>
        /// <typeparam name="TDestination">destination type</typeparam>
        /// <param name="obj">source object(s)</param>
        /// <returns>destination objects(s)</returns>
        public static TDestination Map<TSource, TDestination>(TSource obj)
        {
            try
            {
                return Mapper.Map<TSource, TDestination>(obj);
            }
            catch (AutoMapperMappingException ex)
            {
                //If the mapper exception occurred because of a domain object validation exception, unwrap that exception 
                //and propogate it.  This way the client can more easily expose a user readable validation message instead of a cryptic
                //mapper exception message.
                //if (ex.GetBaseException() is PlatformDomainValidationException)
                //{
                //    Logger.Write(ex, TraceEventType.Warning);
                //    throw ex.GetBaseException();
                //}
                throw;
            }
        }
    }
}

