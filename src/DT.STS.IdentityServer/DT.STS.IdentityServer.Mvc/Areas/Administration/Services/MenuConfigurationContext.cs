using DT.Core.Web.Common.Models;
using DT.Core.Web.Ui.Navigation;
using System.Linq;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Services
{
    public static class MenuNameConstants
    {
        public const string SystemManagement = "SystemManagement";

        public const string Organization = "Organization";
        public const string Department = "Department";
        public const string Company = "Company";
        public const string Role = "Role";
        public const string GradeLevel = "GradeLevel";

        public const string UserAndPermission = "UserAndPermission";
        public const string User = "User";
        public const string Scope = "Scope";
        public const string Client = "Client";
        public const string UserScope = "UserScope";
    }

    public class MenuConfigurationContext : IMenuConfigurationContext
    {
        public ModuleMenuItems Menu {
            get => BuildIdentityMenu();
        }       

        private ModuleMenuItems BuildIdentityMenu()
        {
            ModuleMenuItems moduleMenuItems = new ModuleMenuItems();

            ModuleMenuItem moduleMenuItem = new ModuleMenuItem();
            moduleMenuItem.Name = MenuNameConstants.SystemManagement;
            moduleMenuItem.DisplayName = "Quản trị hệ thống";
            moduleMenuItem.Order = 0;

            #region Menu Organization
            MenuItem menuOrganization = new MenuItem();
            menuOrganization.Name = MenuNameConstants.Organization;
            menuOrganization.DisplayName = "Sơ đồ tổ chức";
            menuOrganization.Order = 0;
            menuOrganization.Url = "#";
            menuOrganization.Icon = "fa fa-cog";
            menuOrganization.CssClass = "treeview";

            menuOrganization.Items.Add(new MenuItem {
                Name = MenuNameConstants.Company,
                DisplayName = "Công ty",
                Url = "#",
                Order = 0
            });

            menuOrganization.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.GradeLevel,
                DisplayName ="Khối",
                Url = "#",
                Order = 1
            });

            menuOrganization.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.Department,
                DisplayName = "Phòng ban",
                Url = "#",
                Order = 2
            });

            menuOrganization.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.Role,
                DisplayName = "Cấp bậc",
                Url = "~/administration/users/list",
                Order = 3
            });

            moduleMenuItem.AddItem(menuOrganization);
            #endregion

            #region Users and Permission
            MenuItem menuUserAndPermission = new MenuItem();
            menuUserAndPermission.Name = MenuNameConstants.UserAndPermission;
            menuUserAndPermission.DisplayName = "Người dùng và quyền hạn";
            menuUserAndPermission.Order = 1;
            menuUserAndPermission.Url = "#";
            menuUserAndPermission.Icon = "fa fa-users";
            menuUserAndPermission.CssClass = "treeview";

            menuUserAndPermission.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.User,
                DisplayName = "Người dùng",
                Url = "/administration/users/list",
                Order = 0
            });

            menuUserAndPermission.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.Scope,
                DisplayName = "Scope",
                Url = "/administration/scopes/list",
                Order = 1
            });

            menuUserAndPermission.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.Client,
                DisplayName = "Client",
                Url = "/administration/clients/list",
                Order = 2
            });

            menuUserAndPermission.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.UserScope,
                DisplayName = "Phân quyền",
                Url = "/administration/userscopes/list",
                Order = 3
            });

            moduleMenuItem.AddItem(menuUserAndPermission);
            #endregion
            moduleMenuItems.Add(moduleMenuItem);
            return moduleMenuItems;
        }
    }
}