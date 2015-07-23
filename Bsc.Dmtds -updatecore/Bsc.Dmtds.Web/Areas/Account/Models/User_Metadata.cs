using System;
using System.ComponentModel.DataAnnotations;
using Bsc.Dmtds.Common.ComponentModel;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Mvc.Grid.Design;
using Bsc.Dmtds.Web.Grid;

namespace Bsc.Dmtds.Web.Areas.Account.Models
{
    [MetadataFor(typeof(User))]
    [Grid(Checkable = true,IdProperty = "UserName")]
    public class User_Metadata
    {
        [GridColumn(Order = 2, GridColumnType = typeof(SortableGridColumn))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [GridColumn(Order = 4, HeaderText = "是否验证邮箱", GridColumnType = typeof(SortableGridColumn), GridItemColumnType = typeof(BooleanGridItemColumn))]
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        [GridColumn(Order = 5, HeaderText = "是否被锁定", GridColumnType = typeof(SortableGridColumn), GridItemColumnType = typeof(BooleanGridItemColumn))]
        public bool LockoutEnabled { get; set; }

        [GridColumn(Order = 6, HeaderText = "连续登录失败次数", GridColumnType = typeof(SortableGridColumn))]
        public int AccessFailedCount { get; set; }

        public string Id { get; set; }

        [GridColumn(Order = 1, GridColumnType = typeof(SortableGridColumn), GridItemColumnType = typeof(EditGridActionItemColumn))]
        public string UserName { get; set; }


    }
}