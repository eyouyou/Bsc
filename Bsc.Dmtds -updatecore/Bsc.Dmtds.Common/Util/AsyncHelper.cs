using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Bsc.Dmtds.Common.Util
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory MyTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            return TaskExtensions.Unwrap<TResult>(MyTaskFactory.StartNew<Task<TResult>>((Func<Task<TResult>>)(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }))).GetAwaiter().GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            TaskExtensions.Unwrap(MyTaskFactory.StartNew<Task>((Func<Task>)(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }))).GetAwaiter().GetResult();
        }
    }
}
