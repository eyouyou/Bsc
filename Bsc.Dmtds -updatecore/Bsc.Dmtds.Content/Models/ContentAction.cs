﻿using System;

namespace Bsc.Dmtds.Content.Models
{
    [Flags]
    public enum ContentAction
    {
        /// <summary>
        /// 内容添加成功后
        /// </summary>
        Add = 0x1,
        /// <summary>
        /// 内容修改成功后
        /// </summary>
        Update = 0x2,
        /// <summary>
        /// 内容删除成功后
        /// </summary>
        Delete = 0x4,
        /// <summary>
        /// 内容添加前
        /// </summary>
        PreAdd = 0x8,
        /// <summary>
        /// 内容修改前
        /// </summary>
        PreUpdate = 16,
        /// <summary>
        /// 内容删除前
        /// </summary>
        PreDelete = 32
    }
}