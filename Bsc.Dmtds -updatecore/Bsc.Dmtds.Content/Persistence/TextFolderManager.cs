﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Services;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Content.Persistence
{
    [Dependency(typeof(TextFolderManager))]
    public class TextFolderManager : FolderManager<TextFolder>
    {
        public TextFolderManager(ITextFolderProvider provider) : base(provider) { }


        #region ChildFoldersWithSameSchema
        public IEnumerable<TextFolder> ChildFoldersWithSameSchema(TextFolder parentFolder)
        {
            parentFolder = parentFolder.AsActual();
            return ChildFolders(parentFolder)
                .Select(it => it.AsActual())
                .Where(it => it.SchemaName.EqualsOrNullEmpty(parentFolder.SchemaName, StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }
        #endregion

        #region AllChildFoldersWithSameSchema
        public IEnumerable<TextFolder> AllChildFoldersWithSameSchema(TextFolder parentFolder, List<TextFolder> results = null)
        {
            if (results == null)
            {
                results = new List<TextFolder>();
            }
            var children = ChildFoldersWithSameSchema(parentFolder);
            foreach (var c in children)
            {
                results.Add(c);
                AllChildFoldersWithSameSchema(c, results);
            }
            return results;
        }
        #endregion

        public override void Remove(Repository repository, TextFolder item)
        {
            if (Relations(item).Count() > 0)
            {
                throw new Exception(string.Format("'{0}' 正在使用!", item.Name));
            }
            base.Remove(repository, item);
        }
        #region Relations
        public virtual IEnumerable<RelationModel> Relations(TextFolder textFolder)
        {
            var allFolders = AllFoldersFlattened(textFolder.Repository).Select(it => it.AsActual()).ToArray();
            var asCategories = allFolders.Where(it => it.Categories != null && it.Categories.Any(ic => ic.FolderName.EqualsOrNullEmpty(textFolder.FullName, StringComparison.OrdinalIgnoreCase)))
                .Select(it => new RelationModel()
                {
                    DisplayName = it.FriendlyText,
                    ObjectUUID = it.FullName,
                    RelationObject = it,
                    RelationType = "Category folder"
                });
            var embbedFolder = allFolders.Where(it => it.EmbeddedFolders != null && it.EmbeddedFolders.Any(ie => ie.EqualsOrNullEmpty(textFolder.FullName, StringComparison.OrdinalIgnoreCase)))
                .Select(it => new RelationModel()
                {
                    DisplayName = it.FriendlyText,
                    ObjectUUID = it.FullName,
                    RelationObject = it,
                    RelationType = "Embedded folder"
                });
            return asCategories.Concat(embbedFolder);
        }
        #endregion
    }
}