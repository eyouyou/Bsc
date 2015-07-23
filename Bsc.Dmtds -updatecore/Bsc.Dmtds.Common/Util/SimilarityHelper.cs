using System;
using System.Collections.Generic;

namespace Bsc.Dmtds.Common.Util
{
    public static class SimilarityHelper
    {
        public static double Euclidean(Dictionary<String, Double> ratingMap, Dictionary<String, Double> ratingMap2)
        {
            double sim = 0d;
            double commonItems = 0;

            IEnumerator<String> ratingMapIterator = ratingMap.Keys.GetEnumerator();
            if (ratingMapIterator.Current.Equals(""))
                return 1d;
            ratingMapIterator.Reset();
            while (ratingMapIterator.MoveNext())
            {
                String ratingMapIteratorKey = ratingMapIterator.Current;
                IEnumerator<String> uRatingMapIterator = ratingMap2.Keys.GetEnumerator();
                if (uRatingMapIterator.Current.Equals(""))
                    return 1d;
                uRatingMapIterator.Reset();
                while (uRatingMapIterator.MoveNext())
                {
                    String uRatingMapIteratorKey = uRatingMapIterator.Current;
                    if (ratingMapIteratorKey.Equals(uRatingMapIteratorKey))
                    {
                        //相似度计数加一  
                        //求差值的平方和  
                        commonItems++;
                        sim += Math.Pow((ratingMap2[uRatingMapIteratorKey] - ratingMap[ratingMapIteratorKey]), 2);
                    }
                }
            }

            //如果等于零则无相同条目，返回sim=0即可  
            if (commonItems > 0)
            {
                //相似度的范围在0-1之间//tanh取值范围-1到1  
                //0表示完全不相似  
                //1表示完全相似  
                //求平均后开跟  
                //乘上相同的数量占最大可能相同的数量的比重  
                sim = Math.Sqrt(sim / commonItems);
                sim = 1.0d - Math.Tanh(sim);
                int maxCommonItems = Math.Min(ratingMap.Count, ratingMap2.Count);
                sim = sim * (commonItems / maxCommonItems);
            }
            return sim;
        }   
    }
}