using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuffsCalculator
{
    public class DataPublic
    {
        /// <summary>
        /// 选出json节点字串
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static string GetJsonSerializeString(string jsonString, string node)
        {
            JObject obj = JObject.Parse(jsonString);
            string jsonSelectSting = obj[node].ToString();
            return jsonSelectSting;
        }

        /// <summary>
        /// JSON格式数组转化为对应的T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr">JSON格式数组</param>
        /// <returns></returns>
        public static T JsonStringConvert<T>(string jsonStr)
        {
            //设置转化JSON格式时字段长度
            T obj = JsonConvert.DeserializeObject<T>(jsonStr);
            return obj;
        }
    }
}
