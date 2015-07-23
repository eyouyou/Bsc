
using System;
using System.Collections.Generic;
using System.IO;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Persistence;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Content.Services
{
    [Dependency(typeof(RepositoryManager))]
    public class RepositoryManager
    {
        #region .ctor
        IRepositoryProvider Provider
        {
            get;
            set;
        }

        public RepositoryManager(IRepositoryProvider provider)
        {
            this.Provider = provider;
        }
        #endregion

        #region All

        public virtual IEnumerable<Repository> All()
        {
            return Provider.All();
        }

        #endregion

        #region Add
        public virtual void Add(Repository repository)
        {
            if (string.IsNullOrEmpty(repository.Name))
            {
                throw new NameIsReqiredException();
            }
            if (Provider.Get(repository) != null)
            {
                throw new ItemAlreadyExistsException();
            }
            Provider.Add(repository);
        }

        #endregion

        #region Update
        public virtual void Update(Repository repository, Repository old)
        {
            if (string.IsNullOrEmpty(repository.Name))
            {
                throw new NameIsReqiredException();
            }
            if (Provider.Get(repository) == null)
            {
                throw new ItemDoesNotExistException();
            }
            Provider.Update(repository, old);
        }

        #endregion

        #region Remove
        public virtual void Remove(Repository repository)
        {
            if (string.IsNullOrEmpty(repository.Name))
            {
                throw new NameIsReqiredException();
            }
            if (Provider.Get(repository) != null)
            {
                Provider.Remove(repository);
            }
        }

        #endregion

        #region Get

        public virtual Repository Get(string name)
        {
            return Provider.Get(new Repository(name));
        }

        #endregion

        #region Create
        public virtual Repository Create(string repositoryName, string templateName)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                var repository = new Repository(repositoryName);
                Add(repository);
                return repository;
            }
            else
            {
                var template = ServiceFactory.RepositoryTemplateManager.GetItemTemplate(templateName);
                if (template == null)
                {
                    throw new BscException("该模板文件不存在.");
                }
                using (FileStream fs = new FileStream(template.TemplateFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return Create(repositoryName, fs);
                }
            }
        }
        public virtual Repository Create(string repositoryName, Stream templateStream)
        {
            var repository = Provider.Create(repositoryName, templateStream);
            Provider.Initialize(repository);
            Provider.Online(repository);

            return repository;
        }
        #endregion

        #region Copy
        public virtual Repository Copy(Repository sourceRepository, string destRepositoryName)
        {
            var repository = Provider.Copy(sourceRepository, destRepositoryName);

            return repository;
        }
        #endregion

        #region Export
        public virtual void Export(string repositoryName, Stream outputStream)
        {
            Repository repository = new Repository(repositoryName);
            if (Get(repositoryName) == null)
            {
                throw new Exception(string.Format("该 repository 不存在:'{0}'", repositoryName));
            }
            Provider.Export(repository, outputStream);
        }
        #endregion

        #region Offline/Online/IsOnline
        public virtual void Offline(string repositoryName)
        {
            Provider.Offline(new Repository(repositoryName));
        }
        public virtual void Online(string repositoryName)
        {
            Provider.Online(new Repository(repositoryName));
        }
        public virtual bool IsOnline(string repositoryName)
        {
            return Provider.IsOnline(new Repository(repositoryName));
        }
        #endregion
    }
}