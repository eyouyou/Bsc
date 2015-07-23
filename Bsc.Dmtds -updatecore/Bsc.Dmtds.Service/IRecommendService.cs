using System.Collections.Generic;
using Bsc.Dmtds.Core.Module;


namespace Bsc.Dmtds.Service
{
    /// <summary>
    /// 推荐
    /// </summary>
    public interface IRecommendService
    {
        double Test();
        /// <summary>
        /// 欧几里得距离
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<Course> Euclidean(string userid);
    }
}