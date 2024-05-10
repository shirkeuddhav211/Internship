using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Auth
{
    public static class Permissions
    {

        public static class AccountFinance
        {
            public const string Accounts_Receivable_ReadOnly = "Permissions.Account/Finance.Accounts Receivable.ReadOnly";
            public const string Gate_Keeper_to_from_QB_Budget_Allocation_ReadOnly = "Permissions.Account/Finance.Gate Keeper to/from QB-Budget Allocation.ReadOnly";
            public const string Gate_Keeper_to_from_QB_Reports_ReadOnly = "Permissions.Account/Finance.Gate Keeper to/from QB-Reports.ReadOnly";
            public const string Gate_Keeper_to_from_QB_Time_Sheets_Payroll_ReadOnly = "Permissions.Account/Finance.Gate Keeper to/from QB-Time Sheets/ Payroll.ReadOnly";
            public const string Gate_Keeper_to_from_QB_Receipts_ReadOnly = "Permissions.Account/Finance.Gate Keeper to/from QB-Receipts.ReadOnly";
            public const string Gate_Keeper_to_from_QB_Invoices_ReadOnly = "Permissions.Account/Finance.Gate Keeper to/from QB-Invoices.ReadOnly";
            public const string Gate_Keeper_to_from_QB_ReadOnly = "Permissions.Account/Finance.Gate Keeper to/from QB.ReadOnly";
            public const string Accounts_Payable_ReadOnly = "Permissions.Account/Finance.Accounts Payable.ReadOnly";
            public const string Budget_Allocation_ReadOnly = "Permissions.Account/Finance.Budget Allocation.ReadOnly";
            public const string Run_Rates_Labor_Burden_ReadOnly = "Permissions.Account/Finance.Run Rates/Labor Burden.ReadOnly";
            public const string Reports_ReadOnly = "Permissions.Account/Finance.Reports.ReadOnly";
            public const string Invoices_ReadOnly = "Permissions.Account/Finance.Invoices.ReadOnly";
            public const string Receipts_ReadOnly = "Permissions.Account/Finance.Receipts.ReadOnly";
            public const string Time_Sheets_Payroll_ReadOnly = "Permissions.Account/Finance.Time Sheets/ Payroll.ReadOnly";
            
            public const string Accounts_Receivable_AddUpdateDelete = "Permissions.Account/Finance.Accounts Receivable.AddUpdateDelete";
            public const string Gate_Keeper_to_from_QB_Budget_Allocation_AddUpdateDelete = "Permissions.Account/Finance.Gate Keeper to/from QB-Budget Allocation.AddUpdateDelete";
            public const string Gate_Keeper_to_from_QB_Reports_AddUpdateDelete = "Permissions.Account/Finance.Gate Keeper to/from QB-Reports.AddUpdateDelete";
            public const string Gate_Keeper_to_from_QB_Time_Sheets_Payroll_AddUpdateDelete = "Permissions.Account/Finance.Gate Keeper to/from QB-Time Sheets/ Payroll.AddUpdateDelete";
            public const string Gate_Keeper_to_from_QB_Receipts_AddUpdateDelete = "Permissions.Account/Finance.Gate Keeper to/from QB-Receipts.AddUpdateDelete";
            public const string Gate_Keeper_to_from_QB_Invoices_AddUpdateDelete = "Permissions.Account/Finance.Gate Keeper to/from QB-Invoices.AddUpdateDelete";
            public const string Gate_Keeper_to_from_QB_AddUpdateDelete = "Permissions.Account/Finance.Gate Keeper to/from QB.AddUpdateDelete";
            public const string Accounts_Payable_AddUpdateDelete = "Permissions.Account/Finance.Accounts Payable.AddUpdateDelete";
            public const string Budget_Allocation_AddUpdateDelete = "Permissions.Account/Finance.Budget Allocation.AddUpdateDelete";
            public const string Run_Rates_Labor_Burden_AddUpdateDelete = "Permissions.Account/Finance.Run Rates/Labor Burden.AddUpdateDelete";
            public const string Reports_AddUpdateDelete = "Permissions.Account/Finance.Reports.AddUpdateDelete";
            public const string Invoices_AddUpdateDelete = "Permissions.Account/Finance.Invoices.AddUpdateDelete";
            public const string Receipts_AddUpdateDelete = "Permissions.Account/Finance.Receipts.AddUpdateDelete";
            public const string Time_Sheets_Payroll_AddUpdateDelete = "Permissions.Account/Finance.Time Sheets/ Payroll.AddUpdateDelete";
        }

        public static class SiteAdmin
        {
            public const string SalesMarketing_ReadOnly = "Permissions.Site Admin.Sales/Marketing Settings.ReadOnly";
            public const string HrSettings_ReadOnly = "Permissions.Site Admin.Hr Settings.ReadOnly";
            public const string AdministrativeSettings_ReadOnly = "Administrative Settings.ReadOnly";
            public const string FileShareLog_ReadOnly = "Permissions.Site Admin.File Share Log.ReadOnly";
            public const string AccountFinanceSettngs_ReadOnly = "Permissions.Site Admin.Account/Finance Settings.ReadOnly";
            public const string SystemSettings_ReadOnly = "Permissions.Site Admin.System Settings.ReadOnly";
            public const string User_ReadOnly = "Permissions.Site Admin.User.ReadOnly";
            public const string FileShareSettings_ReadOnly = "Permissions.Site Admin.File Share Settings.ReadOnly";
            public const string OperationsSettings_ReadOnly = "Permissions.Site Admin.Operations Settings.ReadOnly";

            public const string SalesMarketing_AddUpdateDelete = "Permissions.Site Admin.Sales/Marketing Settings.AddUpdateDelete";
            public const string HrSettings_AddUpdateDelete = "Permissions.Site Admin.Hr Settings.AddUpdateDelete";
            public const string AdministrativeSettings_AddUpdateDelete = "Administrative Settings.AddUpdateDelete";
            public const string FileShareLog_AddUpdateDelete = "Permissions.Site Admin.File Share Log.AddUpdateDelete";
            public const string AccountFinanceSettngs_AddUpdateDelete = "Permissions.Site Admin.Account/Finance Settings.AddUpdateDelete";
            public const string SystemSettings_AddUpdateDelete = "Permissions.Site Admin.System Settings.AddUpdateDelete";
            public const string User_AddUpdateDelete = "Permissions.Site Admin.User.AddUpdateDelete";
            public const string FileShareSettings_AddUpdateDelete = "Permissions.Site Admin.File Share Settings.AddUpdateDelete";
            public const string OperationsSettings_AddUpdateDelete = "Permissions.Site Admin.Operations Settings.AddUpdateDelete";
        }

        public static class Administrative
        {
            public const string Liscensing_ReadOnly = "Permissions.Administrative.Liscensing.ReadOnly";
            public const string Insurance_ReadOnly = "Permissions.Administrative.Insurance.ReadOnly";
            public const string FormFieldCrewsFormFieldCrewsIssuesConcerns_ReadOnly = "Permissions.Administrative.Form Field Crews-Form Field Crews-Issues/Concerns.ReadOnly";
            public const string FormFieldCrewsFormFieldCrewsSafetyMeetings_ReadOnly = "Permissions.Administrative.Form Field Crews-Form Field Crews-Safety Meetings.ReadOnly";
            public const string FormFieldCrewsFormFieldCrewsVehicleConditionReports_ReadOnly = "Permissions.Administrative.Form Field Crews-Form Field Crews-Vehicle Condition Reports.ReadOnly";
            public const string FormFieldCrews_ReadOnly = "Permissions.Administrative.Form Field Crews.ReadOnly";
            public const string Calender_ReadOnly = "Permissions.Administrative.Calender.ReadOnly";

            public const string Liscensing_AddUpdateDelete = "Permissions.Administrative.Liscensing.AddUpdateDelete";
            public const string Insurance_AddUpdateDelete = "Permissions.Administrative.Insurance.AddUpdateDelete";
            public const string FormFieldCrewsFormFieldCrewsIssuesConcerns_AddUpdateDelete = "Permissions.Administrative.Form Field Crews-Form Field Crews-Issues/Concerns.AddUpdateDelete";
            public const string FormFieldCrewsFormFieldCrewsSafetyMeetings_AddUpdateDelete = "Permissions.Administrative.Form Field Crews-Form Field Crews-Safety Meetings.AddUpdateDelete";
            public const string FormFieldCrewsFormFieldCrewsVehicleConditionReports_AddUpdateDelete = "Permissions.Administrative.Form Field Crews-Form Field Crews-Vehicle Condition Reports.AddUpdateDelete";
            public const string FormFieldCrews_AddUpdateDelete = "Permissions.Administrative.Form Field Crews.AddUpdateDelete";
            public const string Calender_AddUpdateDelete = "Permissions.Administrative.Calender.AddUpdateDelete";
        }

        public static class HR
        {
            public const string Teams_ReadOnly = "Permissions.HR.Teams.ReadOnly";
            public const string OnboaardingOnboaardingNewEmployeePacket_ReadOnly = "Permissions.HR.Onboaarding-Onboaarding-New Employee Packet.ReadOnly";
            public const string OnboaardingOnboaardingInterviewApplication_ReadOnly = "Permissions.HR.Onboaarding-Onboaarding-Interview/Application.ReadOnly";
            public const string Onboaarding_ReadOnly = "Permissions.HR.Onboaarding.ReadOnly";
            public const string TeamsTeamsEmployeeFiles_ReadOnly = "Permissions.HR.Teams-Teams-Employee Files.ReadOnly";

            public const string Teams_AddUpdateDelete = "Permissions.HR.Teams.AddUpdateDelete";
            public const string OnboaardingOnboaardingNewEmployeePacket_AddUpdateDelete = "Permissions.HR.Onboaarding-Onboaarding-New Employee Packet.AddUpdateDelete";
            public const string OnboaardingOnboaardingInterviewApplication_AddUpdateDelete = "Permissions.HR.Onboaarding-Onboaarding-Interview/Application.AddUpdateDelete";
            public const string Onboaarding_AddUpdateDelete = "Permissions.HR.Onboaarding.AddUpdateDelete";
            public const string TeamsTeamsEmployeeFiles_AddUpdateDelete = "Permissions.HR.Teams-Teams-Employee Files.AddUpdateDelete";
        }

        public static class Operations
        {
            public const string ManagerEstimate_ReadOnly = "Permissions.Operations.Active Jobs Performance.ReadOnly";
            public const string AllJobsList_ReadOnly = "Permissions.Operations.Active Jobs Performance-Active Jobs Performance-Job Page.ReadOnly";
            public const string EstimateList_ReadOnly = "Permissions.Operations.Estimate List.ReadOnly";
            public const string DocumentAndTemplate_ReadOnly = "Permissions.Operations.Purchase Request.ReadOnly";
            public const string Reporting_ReadOnly = "Permissions.Operations.Active Jobs Performance-Active Jobs Performance-Dashboard.ReadOnly";
            public const string Schedule_ReadOnly = "Permissions.Operations.Schedule.ReadOnly";

            public const string ManagerEstimate_AddUpdateDelete = "Permissions.Operations.Active Jobs Performance-Active Jobs Performance-Job Page.AddUpdateDelete";
            public const string EstimateList_AddUpdateDelete = "Permissions.Operations.Estimate List.AddUpdateDelete";
            public const string AllJobsList_AddUpdateDelete = "Permissions.Operations.Purchase Request.AddUpdateDelete";
            public const string DocumentAndTemplate_AddUpdateDelete = "Permissions.Operations.Active Jobs Performance-Active Jobs Performance-Dashboard.AddUpdateDelete";
            public const string Reporting_AddUpdateDelete = "Permissions.Operations.Active Jobs Performance.AddUpdateDelete";            
            public const string Schedule_AddUpdateDelete = "Permissions.Operations.Schedule.AddUpdateDelete";
        }

        public static class FileShare
        {
            public const string FileShare_ReadOnly = "Permissions.Operations.File Share.ReadOnly";
            public const string FileShare_AddUpdateDelete = "Permissions.Operations.File Share.AddUpdateDelete";

        }
        public static class SalesAndMarketing
        {
            public const string CRM_ReadOnly = "Permissions.Sales and Marketing.CRM.ReadOnly";
            public const string SocialMedia_ReadOnly = "Permissions.Sales and Marketing.Social Media.ReadOnly";
            public const string EmailBlasts_ReadOnly = "Permissions.Sales and Marketing.Email Blasts.ReadOnly";
            public const string MarketingMaterial_ReadOnly = "Permissions.Sales and Marketing.Marketing Material.ReadOnly";

            public const string CRM_AddUpdateDelete = "Permissions.Sales and Marketing.CRM.AddUpdateDelete";
            public const string SocialMedia_AddUpdateDelete = "Permissions.Sales and Marketing.Social Media.AddUpdateDelete";
            public const string EmailBlasts_AddUpdateDelete = "Permissions.Sales and Marketing.Email Blasts.AddUpdateDelete";
            public const string MarketingMaterial_AddUpdateDelete = "Permissions.Sales and Marketing.Marketing Material.AddUpdateDelete";
        }
    }
}
