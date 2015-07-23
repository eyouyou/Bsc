using System;
using System.Linq;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Persistence;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.EventBus.Content
{
    public class ContentBroadcastingSubscriber : ISubscriber
    {
        static ContentBroadcastingSubscriber()
        {
        }
        #region ISubscriber Members

        public virtual EventResult Receive(IEventContext context)
        {
            EventResult eventResult = new EventResult();
            try
            {
                if (context is ContentEventContext)
                {
                    var contentEventContext = (ContentEventContext)context;

                    //Can not run the content broadcasting in parallel threads, must to make sure the execution by sequence.
                    //Thread processThread = new Thread(delegate()
                    //{
                    var contentContext = (ContentEventContext)context;

                    try
                    {
                        var sendingRepository = contentContext.Content.GetRepository().AsActual();
                        var sendingSetting = AllowSending(contentContext.Content, sendingRepository, contentContext);
                        if (sendingSetting != null)
                        {
                            var allRepositories = Services.ServiceFactory.RepositoryManager.All().Where(it => string.Compare(it.Name, sendingRepository.Name, true) != 0);

                            var summarize = contentContext.Content.GetSummary();
                            foreach (var receiver in allRepositories)
                            {
                                var repository = receiver.AsActual();
                                if (repository.EnableBroadcasting)
                                {
                                    Services.ServiceFactory.ReceiveSettingManager.ReceiveContent(repository, contentContext.Content, contentContext.ContentAction);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Bsc.Dmtds.Common.HealthMonitoring.Log.LogException(e);
                    }
                    //});

                    //processThread.Start();
                }
            }
            catch (Exception e)
            {
                eventResult.Exception = e;
            }

            return eventResult;
        }



        protected virtual SendingSetting AllowSending(TextContent content, Repository repository, ContentEventContext eventContext)
        {
            if (!repository.EnableBroadcasting)
            {
                return null;
            }
            var list = Providers.DefaultProviderFactory.GetProvider<ISendingSettingProvider>().All(repository).Select(o => Providers.DefaultProviderFactory.GetProvider<ISendingSettingProvider>().Get(o));
            foreach (var item in list)
            {
                if (AllowSendingSetting(content, item, eventContext))
                {
                    return item;
                }
            }
            return null;
        }
        protected virtual bool AllowSendingSetting(TextContent content, SendingSetting sendingSetting, ContentEventContext eventContext)
        {
            if (!string.IsNullOrEmpty(sendingSetting.FolderName) && !sendingSetting.FolderName.Equals(content.FolderName, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(content.OriginalUUID))
            {
                if (content.IsLocalized == null || content.IsLocalized.Value == false)
                {
                    if ((sendingSetting.SendReceived == null || sendingSetting.SendReceived.Value == false))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}