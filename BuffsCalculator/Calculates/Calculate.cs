using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CalculatorDataController;

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


        private CalculatorDataSet.CAL_BUFFS_INTERFACEDataTable buffInterfaceTable;

        private CalculatorDataSet.CAL_BUFFS_DAMAGEDataTable buffDamageTable;

        private CalculatorDataSet.CAL_JOB_DAMAGEDataTable jobDamageTable;

        private CalculatorDataSet.CAL_FIGHT_PERSONDataTable personTable;

        public CalculatorDataSet.CAL_FIGHT_INFODataTable fightInfoTable;

        #endregion


        #region properties

        public string FightCode
        {
            set
            {
                this.fightCode = value;
            }
        }

        #endregion


        #region construction

        public Calculate()
        {
            this.buffInterfaceTable = new CalculatorDataSet.CAL_BUFFS_INTERFACEDataTable();
            this.fightInfoTable = new CalculatorDataSet.CAL_FIGHT_INFODataTable();
            this.personTable = new CalculatorDataSet.CAL_FIGHT_PERSONDataTable();

            //this.Initilize();
        }

        public void Initilize()
        {
            
        }



        #endregion

        public void GetJsonString()
        {
            this.GetApiJsonString();
            this.GetBuffFromFight();
            this.GetPersonInfo();
        }

        private void GetApiJsonString()
        {
            this.jsonGetFight = FFlogsAPI.GetFight(fightCode);
        }

        #region CustomMethod


        #region GetAllFromFFlogs

        private void GetBuffFromFight()
        {
            string jsonBuffLogs = FFlogsAPI.GetAllResoucesByAbilityID(fightCode, buffStartTime, buffEndTime, buffAbilityID);
        }

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

        #endregion


        #region Damages Not Include Dots

        #endregion


        #region CalculateMethod



        #endregion

        #endregion
    }
}
