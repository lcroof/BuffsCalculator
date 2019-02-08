namespace BuffsCalculator
{
    public class GlobalVariable
    {
        #region FFlogsAPI Url

        public const string FFlogsAPIGetFightsUrl = @"/report/fights/{0}";
        public const string FFlogsAPIGetEventsUrl = @"/report/events/{0}?start={1}&end={2}";
        public const string FFlogsAPIGetTablesUrl = @"/report/tables/{0}/{1}";
        public const string FFlogsHttpsHeader = @"https://www.fflogs.com:443/v1";

        #endregion
        
        public const string FFlogsAPIKey = @"api_key=02903654300d62111fc1d279b07c9102";

        #region BuffAbilityName

        public const string TrickAttack = "Trick Attack"; //背刺
        public const string Hypercharge = "Hypercharge";  //超荷
        public const string PiercingResistanceDown = "Piercing Resistance Down";  //穿刺特性降低
        public const string ChainStratagem = "Chain Stratagem";  //连环计
        public const string FoeRequiem = "Foe Requiem";  //魔人
        public const string LeftEye = "LeftEye"; //龙视左眼
        public const string TheArrow = "The Arrow";  //箭卡
        public const string TheSpear = "The Spear";  //枪卡
        public const string TheBalance = "The Balance"; //太阳神
        public const string BattleVoice = "Battle Voice"; //战斗之声
        public const string BattleLitany = "Battle Litany"; //战斗连祷
        public const string Devotion = "Devotion"; //以太契约
        public const string CriticalUp = "Critical Up"; //暴击率上升
        public const string MagicVulnerabilityUp = "Magic Vulnerability Up"; //魔法易伤
        public const string PhysicalVulnerabilityUp = "Physical Vulnerability Up"; //物理易伤
        public const string Brotherhood = "Brotherhood"; //桃园结义
        public const string Embolden = "Embolden";  //鼓励
        public const string ExpandedRoyalRoad = "Expanded Royal Road"; //扩散神圣路

        #endregion

        #region Types

        public const string aaa = "";

        #endregion

        #region Table Column Name

        public const string ID = "ID";
        public const string START_TIME = "START_TIME";
        public const string END_TIME = "END_TIME";
        public const string GUID = "GUID";
        public const string NAME = "NAME";
        public const string JOB_TYPE = "JOB_TYPE";
        public const string EnabledN = "N";
        public const string EnabledY = "Y";
        public const string OWNER = "PETOWNER";
        public const string TYPE = "TYPE";

        #endregion

        #region Symbol

        public const string question = @"?";
        public const string ampersand = @"&";

        #endregion
    }
}
