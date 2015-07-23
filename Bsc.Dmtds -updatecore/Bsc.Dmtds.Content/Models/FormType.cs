﻿using System;

namespace Bsc.Dmtds.Content.Models
{
    [Flags]
    public enum FormType
    {
        /// <summary>
        /// 列表
        /// </summary>
        Grid = 1,
        /// <summary>
        /// 新建
        /// </summary>
        Create = 2,
        /// <summary>
        /// 修改
        /// </summary>
        Update = 4,
        /// <summary>
        /// 类别选择
        /// </summary>
        Selectable = 8,
        /// <summary>
        /// 用于前台的列表显示
        /// </summary>
        List = 16,
        /// <summary>
        /// 用于前台的内容详细显示
        /// </summary>
        Detail = 32,

        Grid_Menu = 64,
        Create_Menu = 128,
        Update_Menu = 256,

        All = int.MaxValue
    }
}