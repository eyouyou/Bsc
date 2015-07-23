using System;
using System.Collections.Generic;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core.Module;
using Bsc.Dmtds.Respository.IRepository;


namespace Bsc.Dmtds.Service.effection
{
    public class RecommendService:IRecommendService
    {
        private readonly IUserRepository _userRepository;

        public RecommendService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        
        public List<Course> Euclidean(string userid)
        {
            List<Course> courses=new List<Course>();

            return courses;
        }

        public double Test()
        {
            Dictionary<String, Double> ratingMap = new Dictionary<String, Double>();
            Dictionary<String, Double> ratingMap2 = new Dictionary<String, Double>();
            ratingMap.Add("1",1d);
            ratingMap.Add("2", 1d);
            ratingMap.Add("3", 1d);
            ratingMap.Add("4", 1d);
            ratingMap2.Add("1", 1d);
            ratingMap2.Add("2", 1d);
            ratingMap2.Add("3", 1d);
            ratingMap2.Add("4", 1d);
            return SimilarityHelper.Euclidean(ratingMap,ratingMap2);
        }
    }
}