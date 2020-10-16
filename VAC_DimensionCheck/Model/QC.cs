using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAC_DimensionCheck.Helpers;

namespace VAC_DimensionCheck.Model
{
    public class QC
    {
        private static QC instance;

        public static QC Instance { get => instance==null?new QC():instance; set => instance = value; }
        public QC() { }

        public void AddRecord()
        {
            string query = $@"INSERT INTO QC (QCNO, QCDate, QCStatus, QCBy, BatchNo, Product, ProductName, UOM, LotNo, WH, Location, StockQty, StockAmount, UnitCost, RefDoc, 
                            MFRDate, [ExpireDate], ToWH, Remark, CreateBy, CreateDate, ChangeBy, ChangeDate, Reason, ReasonName, PassQty, FailQty, SamplePass, SampleFail, PassRate, FaultyRate, 
                            QualityRate, PendingQty, IsDone, Width, Lenght, Grade, TotalPenaltyScore, FPScore, QCType, RefInfo, ProductionLot, QCNote, FPGrade, InspectionDesc, 
                            AutoOpenQC, ScreenType, BOMNo, OprSeq, OperationCode, ProductDesc) VALUES () ";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        private DataTable GetBOMInfoByBatchcode(string batchcode)
        {
            string query = $@"select A.BOMNO, A.OprSeq, A.Operation, A.QCType, A.OprDesc, A.Product, B.ProductName from BOMOperation A
                            cross apply (select * from BatchCode where BOMNo = A.BOMNo) B
                            where B.BatchCode = '{batchcode}' and OPRC3 = '9Q0101'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            return dt;
        }
    }
}
