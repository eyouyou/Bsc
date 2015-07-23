using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Models.Paths;
using Bsc.Dmtds.Content.Persistence;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Content.Services
{
    [Dependency(typeof(SchemaManager))]
    public class SchemaManager: ManagerBase<Schema, ISchemaProvider>, IManager<Schema, ISchemaProvider>
    {
        #region .ctor
        public SchemaManager(ISchemaProvider provider)
            : base(provider)
        {
        }
        #endregion

        #region Create
        public virtual Schema Create(Repository repository, string schemaName, string templateName)
        {
            var template = ServiceFactory.RepositoryTemplateManager.GetItemTemplate(templateName);
            if (template == null)
            {
                throw new BscException("该模板文件不存在");
            }
            using (FileStream fs = new FileStream(template.TemplateFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Create(repository, schemaName, fs);
            }
        }

        public virtual Schema Create(Repository repository, string schemaName, Stream templateStream)
        {
            return Provider.Create(repository, schemaName, templateStream);
        }
        #endregion

        #region Add
        public override void Add(Repository repository, Schema item)
        {
            base.Add(repository, item);

            ResetForm(repository, item.Name, FormType.All);
        }
        #endregion

        #region Get
        public override Schema Get(Repository repository, string name)
        {
            var dummy = new Schema(repository, name);
            return Provider.Get(dummy);
        }

        #endregion

        #region Remove
        public new void Remove(Repository repository, Schema schema)
        {
            if (Relations(schema).Count() > 0)
            {
                throw new Exception(string.Format("'{0}' is being used by some folders!", schema.Name));
            }
            base.Remove(repository, schema);
        }
        #endregion

        #region Import & Export Schema
        public virtual void Export(Repository repository, Stream stream, IEnumerable<Schema> exports)
        {
            foreach (var item in exports)
            {
                item.Repository = repository;
            }
            Provider.Export(repository, exports, stream);
        }

        public virtual void Import(Repository repository, Stream stream, bool @override)
        {
            Provider.Import(repository, stream, @override);
        }
        #endregion
        #region Build Forms
        public virtual void ResetForm(Repository repository, string schemaName, FormType formType)
        {
            Schema schema = new Schema(repository, schemaName).AsActual();
            SchemaPath path = new SchemaPath(schema);
            if ((formType & FormType.Grid) == FormType.Grid)
            {
                ResetForm(schema, path, FormType.Grid);
            }
            if ((formType & FormType.Create) == FormType.Create)
            {
                ResetForm(schema, path, FormType.Create);
            }
            if ((formType & FormType.Detail) == FormType.Detail)
            {
                ResetForm(schema, path, FormType.Detail);
            }
            if ((formType & FormType.List) == FormType.List)
            {
                ResetForm(schema, path, FormType.List);
            }
            if ((formType & FormType.Selectable) == FormType.Selectable)
            {
                ResetForm(schema, path, FormType.Selectable);
            }
            if ((formType & FormType.Update) == FormType.Update)
            {
                ResetForm(schema, path, FormType.Update);
            }

            //schema.TemplateBuildByMachine = true;

            Update(repository, schema, schema);
        }

        private static void ResetForm(Schema schema, SchemaPath path, FormType formType)
        {
            var formFilePhysicalPath = schema.GetFormFilePhysicalPath(formType);
            string form = schema.GenerateForm(formType);
            IOUtility.SaveStringToFile(formFilePhysicalPath, form);
        }

        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="formType">Type of the form.</param>
        /// <returns></returns>
        public virtual string GetForm(Repository repository, string schemaName, FormType formType)
        {
            Schema schema = new Schema(repository, schemaName);
            var formFilePhysicalPath = schema.GetCustomTemplatePhysicalPath(formType);
            if (!File.Exists(formFilePhysicalPath))
            {
                formFilePhysicalPath = schema.GetFormFilePhysicalPath(formType);
                if (!File.Exists(formFilePhysicalPath))
                {
                    formFilePhysicalPath = schema.GetCustomTemplatePhysicalPath(formType);
                }
            }

            return IOUtility.ReadAsString(formFilePhysicalPath);
        }
        /// <summary>
        /// Saves the form into custom template
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="body">The body.</param>
        /// <param name="formType">Type of the form.</param>
        public virtual void SaveForm(Repository repository, string schemaName, string body, FormType formType)
        {
            Schema schema = new Schema(repository, schemaName);

            var formFilePhysicalPath = schema.GetCustomTemplatePhysicalPath(formType);

            IOUtility.SaveStringToFile(formFilePhysicalPath, body);
        }
        #endregion

        #region relation

        public virtual IEnumerable<RelationModel> Relations(Schema schema)
        {
            var folderProvider = Providers.DefaultProviderFactory.GetProvider<ITextFolderProvider>();

            return folderProvider.BySchema(schema).Select(it => new RelationModel()
            {
                DisplayName = it.FriendlyText,
                ObjectUUID = it.FullName,
                RelationObject = it,
                RelationType = "TextFolder"
            });

        }
        #endregion

        #region Copy

        public virtual void Copy(Repository repository, string sourceName, string destName)
        {
            var provider = Provider;
            provider.Copy(repository, sourceName, destName);
        }

        #endregion
    }
}