﻿using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.Query.Translator.String
{
    public class TranslatedTextContentQuery : TranslatedQuery
    {
        public TranslatedTextContentQuery(Repository repository, Schema schema, TextFolder textFolder)
        {
            this.Repository = repository;
            this.Schema = schema;
            this.TextFolder = textFolder;
        }
        public Repository Repository { get; private set; }
        public Schema Schema { get; private set; }
        public TextFolder TextFolder { get; private set; }

        private IEnumerable<TranslatedTextContentQuery> categoryQueries = new TranslatedCategoriesQuery[0];
        public IEnumerable<TranslatedTextContentQuery> CategroyQueries
        {
            get
            {
                return categoryQueries;
            }
            set
            {
                categoryQueries = value;
            }
        }

        public override string ToString()
        {
            return string.Format("[TextContent] SELECT {0} FROM [{1}.{2}] WHERE {3} Category:({9}) ORDER {4} | OP:{5} PageSize:{6} TOP:{7} Skip:{8} ",
                SelectText, Repository.Name, TextFolder == null ? Schema.Name : (Schema.Name + "$" + TextFolder.FullName),
                ClauseText, SortText, CallType, TakeCount, Top, Skip,
                string.Join(",", categoryQueries.Select(it => it.ToString())));
        }
    }

    public class TranslatedCategoriesQuery : TranslatedTextContentQuery
    {
        public TranslatedCategoriesQuery(Repository repository, TextFolder categoryFolder)
            : base(repository, new Schema(repository, categoryFolder.AsActual().SchemaName), categoryFolder)
        {
            this.CategoryFolder = categoryFolder;
        }
        public TextFolder CategoryFolder { get; private set; }
        public TranslatedQuery SubQuery { get; set; }
        public override string ToString()
        {
            return string.Format("[Categories] SELECT {0} FROM [{1}.{2}] WHERE {3} ORDER {4} | OP:{5} PageSize:{6} TOP:{7} Skip:{8} SubQuery:{9} ",
                SelectText, Repository.Name, TextFolder == null ? Schema.Name : (Schema.Name + "$" + TextFolder.FullName),
                ClauseText, SortText, CallType, TakeCount, Top, Skip, SubQuery.ToString());
        }
    }

    public class TranslatedChildrenQuery : TranslatedTextContentQuery
    {
        public TranslatedChildrenQuery(Repository repository, Schema schema)
            : base(repository, schema, null)
        {
        }
        public TranslatedQuery ParentQuery { get; set; }

        public override string ToString()
        {
            return string.Format("[Children] SELECT {0} FROM [{1}.{2}] WHERE {3} ORDER {4} | OP:{5} PageSize:{6} TOP:{7} Skip:{8} ParentQuery:{9} ",
                SelectText, Repository.Name, TextFolder == null ? Schema.Name : (Schema.Name + "$" + TextFolder.FullName),
                ClauseText, SortText, CallType, TakeCount, Top, Skip, ParentQuery.ToString());
        }
    }
    public class TranslatedParentQuery : TranslatedTextContentQuery
    {
        public TranslatedParentQuery(Repository repository, Schema schema)
            : base(repository, schema, null)
        {
        }
        public TranslatedQuery ChildrenQuery { get; set; }

        public override string ToString()
        {
            return string.Format("[Children] SELECT {0} FROM [{1}.{2}] WHERE {3} ORDER {4} | OP:{5} PageSize:{6} TOP:{7} Skip:{8} ChildrenQuery:{9} ",
                SelectText, Repository.Name, TextFolder == null ? Schema.Name : (Schema.Name + "$" + TextFolder.FullName),
                ClauseText, SortText, CallType, TakeCount, Top, Skip, ChildrenQuery.ToString());
        }
    }
}