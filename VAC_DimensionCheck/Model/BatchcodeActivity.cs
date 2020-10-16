using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAC_DimensionCheck.Helpers;

namespace VAC_DimensionCheck.Model
{
    public class BatchcodeActivity
    {
        private static BatchcodeActivity instance;

        public static BatchcodeActivity Instance { get => instance==null?new BatchcodeActivity():instance; set => instance = value; }
        public BatchcodeActivity() { }

        public void AddRecord(string product, string batchcode, string WONO, string changeNote, double value, string machine, string empCode, string empName, string user)
        {
            int maxSeq = GetMaxSeq(batchcode);
            string query = $@"INSERT INTO BatchCodeActivity(Product, BatchCode, Seq, Qty, WONO, TransType, ChangeNote, ChangeTime, MC, Station, Area, EmpCode, EmpName,
                              SysNote, LinkFile, WorkStation, CreateBy, CreateDate, ChangeBy, ChangeDate) 
                              VALUES ('{product}', '{batchcode}', {maxSeq}, 1, '{WONO}', 'QC.', '{changeNote}','{DateTime.Now}', '{machine}', '{machine}', '9Q', '{empCode}', '{empName}',
                              '','','MES001', '{user}', '{DateTime.Now}', '{user}', '{DateTime.Now}')";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        private int GetMaxSeq(string batchcode)
        {
            string query = $"select max(Seq) as MaxSeq from BatchCodeActivity where BatchCode = '{batchcode}'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            return Int16.Parse(dt.Rows[0]["MaxSeq"].ToString());
        }
    }
}
