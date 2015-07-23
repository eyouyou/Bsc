﻿using System;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;

namespace Bsc.Dmtds.Content.Models
{
    public class Category
    {
        /// <summary>
        /// 内容的UUID
        /// </summary>
        public string ContentUUID { get; set; }
        /// <summary>
        /// 类别目录
        /// </summary>
        public string CategoryFolder { get; set; }
        /// <summary>
        /// 类别内容的UUID
        /// </summary>
        public string CategoryUUID { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var c = (Category)obj;
            if (this.ContentUUID.EqualsOrNullEmpty(c.ContentUUID, StringComparison.CurrentCultureIgnoreCase) &&
                this.CategoryFolder.EqualsOrNullEmpty(c.CategoryFolder, StringComparison.CurrentCultureIgnoreCase) &&
                this.CategoryUUID.EqualsOrNullEmpty(c.CategoryUUID, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return base.Equals(obj);
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        } 
    }
}