using Avans.Core.Contexts;
using Avans.DTOs;
using Avans.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using System.Transactions;


namespace Avans.DAL.Concrete
{
    public class AdvanceRepository : GenericRepository<Advance>
    {

        private IDbConnection _dbConnection;

        public AdvanceRepository(IDbConnection dbConnection)
        {

            //_dbConnection = new SqlConnection(ConnectionHelper.GetConnectionString());
            _dbConnection = dbConnection;

            _dbConnection.Open();
        }

        public List<AdvanceDTO> GetAdvanceAll(int EmployeeID)
        {
            var sqlQuery = "WITH LatestAdvanceHistory AS (\r\n    SELECT\r\n        AdvanceID,\r\n        MAX(Date) AS LatestDate\r\n    FROM\r\n        AdvanceHistory\r\n    GROUP BY\r\n        AdvanceID\r\n)\r\n\r\nSELECT\r\n    adh.AdvanceID,\r\n    adv.AdvanceAmount,\r\n    adv.RequestDate,\r\n    adv.DesiredDate,\r\n    prj.ProjectName,\r\n    sts.StatusName,\r\n    emp.[Name],\r\n    ttl.TitleName,\r\n    adh.Date,\r\n    rcp.ReceiptNo,\r\n    adh.ApprovedAmount,\r\n    pym.DeterminedPaymentDate,\r\n    rcp.ReceiptDate,\r\n    rcp.isRefundReceipt\r\nFROM\r\n    LatestAdvanceHistory latest\r\nINNER JOIN\r\n    AdvanceHistory adh ON adh.AdvanceID = latest.AdvanceID AND adh.Date = latest.LatestDate\r\nINNER JOIN\r\n    Advance adv ON adv.ID = adh.AdvanceID\r\nINNER JOIN\r\n    Project prj ON prj.ID = adv.ProjectID\r\nLEFT JOIN\r\n    [Status] sts ON sts.ID = adh.StatusID\r\nLEFT JOIN\r\n    Employee emp ON emp.ID = adh.TransactorID\r\nLEFT JOIN\r\n    Title ttl ON ttl.ID = emp.TitleID\r\nLEFT JOIN\r\n    Payment pym ON pym.AdvanceID = adv.ID\r\nLEFT JOIN\r\n    Receipt rcp ON rcp.AdvanceID = adv.ID\r\nWHERE\r\n    emp.ID = @EmployeeID;\r\n";

            var parameters = new { EmployeeID = EmployeeID };

            List<AdvanceDTO> advances = _dbConnection.Query<AdvanceDTO>(sqlQuery, parameters).ToList();

            return advances;
        }
        public List<AdvanceDTO> GetAdvanceByID(int advanceID)
        {
            var sqlQuery = @"
       SELECT 
    adv.AdvanceAmount, 
    adv.RequestDate, 
    adv.DesiredDate, 
    prj.ProjectName, 
    sts.StatusName,
    emp.[Name], 
    ttl.TitleName, 
    adh.Date, 
    rcp.ReceiptNo, 
    adh.ApprovedAmount,
    pym.DeterminedPaymentDate, 
    rcp.ReceiptDate, 
    rcp.isRefundReceipt
FROM 
    AdvanceHistory adh
LEFT JOIN 
    Advance adv ON adv.ID = adh.AdvanceID
LEFT JOIN 
    Project prj ON prj.ID = adv.ProjectID
LEFT JOIN 
    [Status] sts ON sts.ID = adh.StatusID
LEFT JOIN 
    Employee emp ON emp.ID = adh.TransactorID
LEFT JOIN 
    Title ttl ON ttl.ID = emp.TitleID
LEFT JOIN 
    Payment pym ON pym.AdvanceID = adv.ID
LEFT JOIN 
    Receipt rcp ON rcp.AdvanceID = adv.ID
WHERE 
    adv.ID = @AdvanceID;
";

            var parameters = new { AdvanceID = advanceID };

            List<AdvanceDTO> advances = _dbConnection.Query<AdvanceDTO>(sqlQuery, parameters).ToList();

            return advances;
        }
        public List<AdvancesPendingApprovalSelectDTO> GetAdvanceForPendingApprovalDetailByID(int advanceID)
        {
            var sqlQuery = @"
       SELECT 
    emp.Name,
    ttl.TitleName,
    pym.DeterminedPaymentDate,
    adh.StatusID,
    adv.ID,
    adh.Date,
    adh.TransactorID,
    bsu.BusinessUnitName,
    sts.StatusName,
    adv.RequestDate,
    adv.AdvanceAmount,
    adv.DesiredDate,
    prj.ProjectName
FROM 
    Advance adv 
INNER JOIN 
    Employee emp ON emp.ID = adv.EmployeeID
INNER JOIN 
    Title ttl ON ttl.ID = emp.TitleID
INNER JOIN 
    BusinessUnit bsu ON bsu.ID = emp.BusinessUnitID
INNER JOIN 
    AdvanceHistory adh ON adh.AdvanceID = adv.ID
INNER JOIN 
    [Status] sts ON sts.ID = adh.StatusID
INNER JOIN 
    Project prj ON prj.ID = adv.ProjectID
LEFT JOIN 
    Payment pym ON pym.AdvanceID = adv.ID
WHERE 
    adv.ID = @AdvanceID;
";

            var parameters = new { AdvanceID = advanceID };

            List<AdvancesPendingApprovalSelectDTO> advances = _dbConnection.Query<AdvancesPendingApprovalSelectDTO>(sqlQuery, parameters).ToList();

            return advances;
        }


        public List<AdvancesPendingApprovalSelectDTO> GetAdvanceForPendingApproval()
        {
            var sqlQuery = "select emp.Name,ttl.TitleName,pym.DeterminedPaymentDate,adh.StatusID,adv.ID,adh.Date,adh.TransactorID,bsu.BusinessUnitName,sts.StatusName,adv.RequestDate,adv.AdvanceAmount,adv.DesiredDate,prj.ProjectName from Advance adv \r\ninner join Employee emp ON emp.ID = adv.EmployeeID\r\ninner join Title ttl ON ttl.ID = emp.TitleID\r\ninner join BusinessUnit bsu ON bsu.ID = emp.BusinessUnitID\r\ninner join AdvanceHistory adh ON adh.AdvanceID = adv.ID\r\ninner join [Status] sts ON sts.ID = adh.StatusID\r\ninner join Project prj ON prj.ID = adv.ProjectID\r\nleft join Payment pym ON pym.AdvanceID = adv.ID";

            List<AdvancesPendingApprovalSelectDTO> advances = _dbConnection.Query<AdvancesPendingApprovalSelectDTO>(sqlQuery).ToList();

            return advances;
        }


        public bool AddAdvanceWithHistory(AdvanceInsertDTO advanceInsertDTO)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var connection = _dbConnection)
                    {

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // 1. Advance tablosuna ekleme
                                int advanceID;
                                using (var command = new SqlCommand("INSERT INTO Advance (RequestDate,AdvanceAmount, AdvanceDescription, ProjectID, DesiredDate,EmployeeID) VALUES (@RequestDate,@AdvanceAmount, @AdvanceDescription, @ProjectID, @DesiredDate,@EmployeeID); SELECT SCOPE_IDENTITY();", _dbConnection as SqlConnection, transaction as SqlTransaction))
                                {
                                    command.Parameters.AddWithValue("@AdvanceAmount", advanceInsertDTO.AdvanceAmount);
                                    command.Parameters.AddWithValue("@EmployeeID", 9);

                                    command.Parameters.AddWithValue("@AdvanceDescription", advanceInsertDTO.AdvanceDescription);
                                    command.Parameters.AddWithValue("@ProjectID", advanceInsertDTO.ProjectID);
                                    command.Parameters.AddWithValue("@DesiredDate", advanceInsertDTO.DesiredDate);
                                    command.Parameters.AddWithValue("@RequestDate", DateTime.Now);
                                    //command.Parameters.AddWithValue("@StatusID", advanceInsertDTO.StatusID);

                                    // Advance tablosuna ekleme işlemini gerçekleştir ve eklenen ID'yi al
                                    advanceID = Convert.ToInt32(command.ExecuteScalar());
                                }

                                // 2. AdvanceHistory tablosuna ekleme
                                using (var command = new SqlCommand("INSERT INTO AdvanceHistory (AdvanceID, TransactorID, ApprovedAmount, Date) VALUES  (@AdvanceID, @TransactorID, @ApprovedAmount, @Date);", _dbConnection as SqlConnection, transaction as SqlTransaction))
                                {

                                    command.Parameters.AddWithValue("@AdvanceID", advanceID);
                                    command.Parameters.AddWithValue("@TransactorID", 9); // Burada bir transactor ID belirtmelisiniz
                                    command.Parameters.AddWithValue("@ApprovedAmount", advanceInsertDTO.AdvanceAmount);
                                    command.Parameters.AddWithValue("@Date", DateTime.Now);

                                    // AdvanceHistory tablosuna ekleme işlemini gerçekleştir
                                    command.ExecuteNonQuery();
                                }

                                // İşlemleri tamamla
                                transaction.Commit();
                                scope.Complete(); // TransactionScope'u tamamla

                                return true;
                            }
                            catch (Exception ex)
                            {
                                // Hata durumunda işlemleri geri al
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateAdvanceWithHistory(AdvanceUpdateDTO advanceUpdateDTO)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var connection = _dbConnection)
                    {
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // 1. Advance tablosunu güncelle
                                using (var command = new SqlCommand("UPDATE A\r\nSET \r\n    A.StatusID = @StatusID,\r\n    A.isApproved = @isApproved\r\nFROM \r\n    Advance A\r\nINNER JOIN \r\n    Payment P ON A.ID = P.AdvanceID\r\nWHERE \r\n    A.ID = @ID;\r\n\r\nUPDATE P\r\nSET \r\n    P.DeterminedPaymentDate = @DeterminedPaymentDate\r\nFROM \r\n    Payment P\r\nWHERE \r\n    P.AdvanceID = @ID;\r\n\r\nUPDATE R\r\nSET \r\n    R.ReceiptNo = @NewReceiptNo -- Yeni ReceiptNo değerini burada belirtin\r\nFROM \r\n    Receipt R\r\nWHERE \r\n    R.AdvanceID = @ID;\r\n", _dbConnection as SqlConnection, transaction as SqlTransaction))
                                {
                                    command.Parameters.AddWithValue("@ID", advanceUpdateDTO.ID);
                                    command.Parameters.AddWithValue("@NewReceiptNo", advanceUpdateDTO.ReceiptNo);
                                    command.Parameters.AddWithValue("@StatusID", advanceUpdateDTO.StatusID);
                                    command.Parameters.AddWithValue("@isApproved", advanceUpdateDTO.isApproved);
                                    // Eğer DeterminedPaymentDate değeri null değilse parametreyi ekleyin
                                    if (advanceUpdateDTO.DeterminedPaymentDate.HasValue)
                                    {
                                        command.Parameters.AddWithValue("@DeterminedPaymentDate", advanceUpdateDTO.DeterminedPaymentDate.Value);
                                    }
                                    else
                                    {

                                        command.Parameters.AddWithValue("@DeterminedPaymentDate", DBNull.Value);
                                    }




                                    command.ExecuteNonQuery();
                                }


                                // 2. AdvanceHistory tablosuna ekleme
                                using (var command = new SqlCommand("INSERT INTO AdvanceHistory (AdvanceID,StatusID, TransactorID, ApprovedAmount, Date) VALUES  (@AdvanceID,@StatusID, @TransactorID, @ApprovedAmount, @Date);", _dbConnection as SqlConnection, transaction as SqlTransaction))
                                {
                                    command.Parameters.AddWithValue("@AdvanceID", advanceUpdateDTO.ID);
                                    command.Parameters.AddWithValue("@StatusID", advanceUpdateDTO.StatusID);
                                    command.Parameters.AddWithValue("@TransactorID", 9); // Burada bir transactor ID belirtmelisiniz
                                    command.Parameters.AddWithValue("@ApprovedAmount", advanceUpdateDTO.ApprovedAmount);
                                    command.Parameters.AddWithValue("@Date", DateTime.Now);

                                    // AdvanceHistory tablosuna ekleme işlemini gerçekleştir
                                    command.ExecuteNonQuery();
                                }

                                // İşlemleri tamamla
                                transaction.Commit();
                                scope.Complete(); // TransactionScope'u tamamla

                                return true;
                            }
                            catch (Exception ex)
                            {
                                // Hata durumunda işlemleri geri al
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }





    }
}
