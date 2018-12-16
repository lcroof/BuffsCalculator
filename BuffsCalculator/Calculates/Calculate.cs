using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CalculatorDataController;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BuffsCalculator
{
    public class Calculate
    {
        #region field
        /// <summary>
        /// 攻击触发类型
        /// </summary>
        private decimal hitType;

        /// <summary>
        /// BUFF开始时间
        /// </summary>
        private string buffStartTime;

        /// <summary>
        /// BUFF结束时间
        /// </summary>
        private string buffEndTime;

        /// <summary>
        /// 伤害技能ID
        /// </summary>
        private decimal damangeAbilityID;

        /// <summary>
        /// 伤害技能名称
        /// </summary>
        private string damageAbilityName;

        /// <summary>
        /// BUFF技能ID
        /// </summary>
        private decimal buffAbilityID;

        /// <summary>
        /// BUFF技能名称
        /// </summary>
        private string buffAbilityName;

        /// <summary>
        /// 本次LOG记录的参战人员ID
        /// </summary>
        private decimal personID;

        /// <summary>
        /// 本次LOG记录的参战人员职业
        /// </summary>
        private string personType;

        /// <summary>
        /// 造成伤害量
        /// </summary>
        private decimal damageAmount;

        /// <summary>
        /// FFlogs记录战斗场次ID
        /// </summary>
        private decimal fightID;

        /// <summary>
        /// FFlogs记录战斗上传的Report代码
        /// </summary>
        private string fightCode;

        /// <summary>
        /// 获取report/fights的json
        /// </summary>
        private string jsonGetFight;

        /// <summary>
        /// 战斗开始时间
        /// </summary>
        private string fightStartTime;

        /// <summary>
        /// 战斗结束时间
        /// </summary>
        private string fightEndTime;

        /// <summary>
        /// 战斗Event
        /// </summary>
        private string fightEventLog;


        private CalculatorDataSet.CAL_BUFFS_INTERFACEDataTable buffInterfaceTable;

        private CalculatorDataSet.CAL_BUFFS_DAMAGEDataTable buffDamageTable;

        private CalculatorDataSet.CAL_JOB_DAMAGEDataTable jobDamageTable;

        private CalculatorDataSet.CAL_FIGHT_PERSONDataTable personTable;

        public CalculatorDataSet.CAL_FIGHT_INFODataTable fightInfoTable;

        public CalculatorDataSet.CAL_FFLOGS_FIGHTEVENTDataTable fightEventTable;

        #endregion


        #region properties

        public string FightCode
        {
            set
            {
                this.fightCode = value;
            }
        }

        public decimal FightID
        {
            set
            {
                this.fightID = value;
            }
        }

        #endregion


        #region construction

        public Calculate()
        {
            this.buffInterfaceTable = new CalculatorDataSet.CAL_BUFFS_INTERFACEDataTable();
            this.fightInfoTable = new CalculatorDataSet.CAL_FIGHT_INFODataTable();
            this.personTable = new CalculatorDataSet.CAL_FIGHT_PERSONDataTable();

            this.Initilize();
        }

        public void Initilize()
        {
            //this.fightStartTime = "8974432";
            //this.fightEndTime = "9904313";
            this.fightEventLog = string.Empty;
            this.fightEventTable = new CalculatorDataSet.CAL_FFLOGS_FIGHTEVENTDataTable();
        }



        #endregion

        public void GetJsonString()
        {
            this.GetApiJsonString();
            this.fightEventLog = this.GetBuffFromFightEvent();
            this.GetAllEventToDataTable();
            //this.GetBuffFromFight();
            //this.GetPersonInfo();
        }

        private void GetApiJsonString()
        {
            this.jsonGetFight = FFlogsAPI.GetFight(fightCode);
            FFLogsAPIFightModel fightInfo = JsonConvert.DeserializeObject<FFLogsAPIFightModel>(this.jsonGetFight);
            //JObject jArray = (JObject)JsonConvert.DeserializeObject(DataPublic.GetJsonSerializeString(this.jsonGetFight, "fights"));
            this.fightStartTime = fightInfo.fights[(int)this.fightID - 1].start_time;
            this.fightEndTime = fightInfo.fights[(int)this.fightID - 1].end_time;
        }

        #region CustomMethod


        #region GetAllFromFFlogs

        /// <summary>
        /// 从某场战斗中获取该场以能力技为限制的信息
        /// </summary>
        private void GetBuffFromFight()
        {
            string jsonBuffLogs = FFlogsAPI.GetAllResoucesByAbilityID(fightCode, buffStartTime, buffEndTime, buffAbilityID);
        }

        /// <summary>
        /// 从某场战斗中获取该场以所有技能事件发生总表的信息
        /// </summary>
        private string GetBuffFromFightEvent()
        {
            string nextPageLogs = "0";
            string tempJsonEventLogs = string.Empty;
            string jsonEventLogs = string.Empty;
            while (Convert.ToDecimal(nextPageLogs) < Convert.ToDecimal(this.fightEndTime))
            {
                if (nextPageLogs == "0")
                {
                    tempJsonEventLogs = FFlogsAPI.GetFightEvent(fightCode, this.fightStartTime, this.fightEndTime);
                }
                else
                {
                    tempJsonEventLogs = FFlogsAPI.GetFightEvent(fightCode, nextPageLogs, this.fightEndTime);
                }
                nextPageLogs = DataPublic.GetJsonSerializeString(tempJsonEventLogs, "nextPageTimestamp");
                if (nextPageLogs == string.Empty)
                {
                    nextPageLogs = this.fightEndTime;
                }
                jsonEventLogs += DataPublic.GetJsonSerializeString(tempJsonEventLogs, "events");
            }

            return jsonEventLogs.Replace("][",",");
        }

        /// <summary>
        /// 获取个人资料
        /// </summary>
        private void GetPersonInfo()
        {
            this.fightInfoTable.Clear();
            string jsonFightInfo = DataPublic.GetJsonSerializeString(this.jsonGetFight, "fights");
            string jsonPersonInfo = DataPublic.GetJsonSerializeString(this.jsonGetFight, "friendlies");
            string jsonPersonPetInfo = DataPublic.GetJsonSerializeString(this.jsonGetFight, "friendlyPets");

            DataTable fightTable = DataPublic.JsonStringConvert<DataTable>(jsonFightInfo);
            DataTable personPetTable = DataPublic.JsonStringConvert<DataTable>(jsonPersonPetInfo);
            DataTable personTable = DataPublic.JsonStringConvert<DataTable>(jsonPersonInfo);

            foreach(DataRow fightRow in fightTable.Rows)
            {
                CalculatorDataSet.CAL_FIGHT_INFORow fightInfoNewRow = this.fightInfoTable.NewCAL_FIGHT_INFORow();
                fightInfoNewRow.FIGHT_NO = fightRow[GlobalVariable.ID].ToString();
                fightInfoNewRow.START_TIME = fightRow[GlobalVariable.START_TIME].ToString();
                fightInfoNewRow.END_TIME = fightRow[GlobalVariable.END_TIME].ToString();
                this.fightInfoTable.AddCAL_FIGHT_INFORow(fightInfoNewRow);
                this.fightInfoTable.AcceptChanges();
            }

            foreach (DataRow personPetRow in personPetTable.Rows)
            {
                CalculatorDataSet.CAL_FIGHT_PERSONRow personPetNewRow = this.personTable.NewCAL_FIGHT_PERSONRow();
                personPetNewRow.GUID = Convert.ToDecimal(personPetRow[GlobalVariable.GUID]);
                personPetNewRow.NAME = personPetRow[GlobalVariable.NAME].ToString();
                personPetNewRow.JOB_TYPE = personPetRow[GlobalVariable.TYPE].ToString();
                personPetNewRow.PET = GlobalVariable.EnabledY;
                personPetNewRow.OWNER = Convert.ToDecimal(personPetRow[GlobalVariable.OWNER]);
                this.personTable.AddCAL_FIGHT_PERSONRow(personPetNewRow);
                this.personTable.AcceptChanges();
            }

            foreach (DataRow personRow in personTable.Rows)
            {
                CalculatorDataSet.CAL_FIGHT_PERSONRow personNewRow = this.personTable.NewCAL_FIGHT_PERSONRow();
                personNewRow.GUID = Convert.ToDecimal(personRow[GlobalVariable.GUID]);
                personNewRow.NAME = personRow[GlobalVariable.NAME].ToString();
                personNewRow.JOB_TYPE = personRow[GlobalVariable.TYPE].ToString();
                personNewRow.PET = GlobalVariable.EnabledN;
                personNewRow.OWNER = 0;
                this.personTable.AddCAL_FIGHT_PERSONRow(personNewRow);
                this.personTable.AcceptChanges();
            }
        }

        /// <summary>
        /// 获取所有伤害类Event
        /// </summary>
        private void GetAllEventToDataTable()
        {
            FFLogAPIEventModel fightEventInfo = JsonConvert.DeserializeObject<FFLogAPIEventModel>("{events:" +this.fightEventLog + "}");
            
            foreach(var item in fightEventInfo.events)
            {
                if (item.type == "damage")
                {
                    CalculatorDataSet.CAL_FFLOGS_FIGHTEVENTRow newRow = this.fightEventTable.NewCAL_FFLOGS_FIGHTEVENTRow();
                    newRow.RAID_JOB_ID = item.sourceID;
                    newRow.SKILL_ID = item.ability.guid;
                    newRow.SKILL_NAME = item.ability.name;
                    newRow.SKILL_DAMAGE = item.amount;
                    newRow.TIMESTAMP = item.timestamp;
                    newRow.HIT_TYPE = item.hitType + (item.multistrike ? 2 : 0);
                    this.fightEventTable.AddCAL_FFLOGS_FIGHTEVENTRow(newRow);
                }
            }
            this.fightEventTable.AcceptChanges();
        }

        #endregion


        #region CalculateMethod



        #endregion

        #endregion
    }
}
