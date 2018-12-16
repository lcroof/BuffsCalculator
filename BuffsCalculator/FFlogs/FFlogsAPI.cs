using System.Net;

namespace BuffsCalculator
{
    public class FFlogsAPI
    {

        /// <summary>
        /// 根据能力ID查询资源
        /// </summary>
        /// <param name="fightCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="abilityID"></param>
        /// <returns></returns>
        public static string GetAllResoucesByAbilityID(string fightCode, string startTime, string endTime, decimal abilityID)
        {
            string jsonString = string.Empty;
            return jsonString;
        }

        /// <summary>
        /// 查询总战斗信息
        /// </summary>
        /// <param name="fightCode"></param>
        /// <returns></returns>
        public static string GetFight(string fightCode)
        {
            string jsonString = WebAPI.HttpGet(
                string.Format(GlobalVariable.FFlogsHttpsHeader + GlobalVariable.FFlogsAPIGetFightsUrl + GlobalVariable.question + GlobalVariable.FFlogsAPIKey, 
                fightCode));
            return jsonString;
        }

        /// <summary>
        /// 查询单场战斗
        /// </summary>
        /// <param name="fightCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static string GetFightEvent(string fightCode, string startTime, string endTime)
        {
            string jsonString = WebAPI.HttpGet(
                string.Format(GlobalVariable.FFlogsHttpsHeader + GlobalVariable.FFlogsAPIGetEventsUrl + GlobalVariable.ampersand + GlobalVariable.FFlogsAPIKey, 
                fightCode, startTime, endTime));
            return jsonString;
        }
    }
}
