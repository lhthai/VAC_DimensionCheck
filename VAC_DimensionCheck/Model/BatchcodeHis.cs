using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAC_DimensionCheck.Helpers;

namespace VAC_DimensionCheck.Model
{
    public class BatchcodeHis
    {
        private static BatchcodeHis instance;

        public static BatchcodeHis Instance { get => instance == null ? new BatchcodeHis() : instance; set => instance = value; }
        public BatchcodeHis() { }

        public void AddRecord(string product, string batchcode, string mainWO, string WONO, string changeNote, double value, string machine, string empCode, string empName, string user)
        {
            int maxHisSeq = GetMaxHisSeq(batchcode);
            string result = "";
            string query = $@"INSERT INTO BatchCodeHis(Product, BatchCode, HisSeq, Qty, MainWONO, WONO, TransType, ChangeNote, StartTime, FinishTime, MC, Station, 
                              Area, EmpCode, EmpName, Result, SysNote, LinkFile, WorkStation, CreateBy, CreateDate,ChangeBy, ChangeDate) 
                              VALUES ('{product}', '{batchcode}', {maxHisSeq}, 1, '{mainWO}', '{WONO}', 'QC.', '{changeNote}','{DateTime.Now}', '{DateTime.Now}',
                             '{machine}', '{machine}', '9Q', '{empCode}', '{empName}', '{result}', '', '', 'MES001', '{user}', '{DateTime.Now}', '{user}', '{DateTime.Now}')";
            DataProvider.Instance.ExecuteNonQuery(query);
        }


        public int GetMaxHisSeq(string batchcode)
        {
            string query = $"select max(HisSeq) as MaxSeq from BatchCodeHis where BatchCode = '{batchcode}'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            return Int16.Parse(dt.Rows[0]["MaxSeq"].ToString());
        }

        public string CheckResult(double value, double minValue, double maxValue)
        {
            if (minValue <= value && value <= maxValue)
            {
                return "PASS";
            }
            else
            {
                return "FAIL";
            }
        }
    }
}
