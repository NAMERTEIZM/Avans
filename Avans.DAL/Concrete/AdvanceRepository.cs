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

        public List<AdvanceDTO> GetAdvanceAll()
        {
            var sqlQuery = "select adh.AdvanceID,adv.AdvanceAmount,adv.RequestDate,adv.DesiredDate,prj.ProjectName,sts.StatusName,emp.[Name],ttl.TitleName,adh.Date,rcp.ReceiptNo,adh.ApprovedAmount,pym.DeterminedPaymentDate,rcp.ReceiptDate,rcp.isRefundReceipt from AdvanceHistory adh \r\ninner join Advance adv ON adv.ID = adh.AdvanceID\r\ninner join Project prj ON prj.ID=adv.ProjectID \r\nleft join [Status] sts ON sts.ID = adh.StatusID\r\nleft join Employee emp ON emp.ID = adh.TransactorID \r\nleft join Title ttl ON ttl.ID = emp.TitleID \r\nleft join Payment pym ON pym.AdvanceID = adv.ID \r\nleft join Receipt rcp ON rcp.AdvanceID =adv.ID";

            List<AdvanceDTO> advances = _dbConnection.Query<AdvanceDTO>(sqlQuery).ToList();

            return advances;
        }
        public List<AdvanceDTO> GetAdvanceByID(int advanceID)
        {
            var sqlQuery = @"
        SELECT adv.AdvanceAmount, adv.RequestDate, adv.DesiredDate, prj.ProjectName, sts.StatusName,
       emp.[Name], ttl.TitleName, adh.Date, rcp.ReceiptNo, adh.ApprovedAmount,
       pym.DeterminedPaymentDate, rcp.ReceiptDate, rcp.isRefundReceipt
FROM AdvanceHistory adh
left JOIN Advance adv ON adv.ID = adh.AdvanceID
left JOIN Project prj ON prj.ID = adv.ProjectID
LEFT JOIN [Status] sts ON sts.ID = adv.StatusID
LEFT JOIN Employee emp ON emp.ID = adh.TransactorID
LEFT JOIN Title ttl ON ttl.ID = emp.TitleID
LEFT JOIN Payment pym ON pym.AdvanceID = adv.ID
LEFT JOIN Receipt rcp ON rcp.AdvanceID = adv.ID
WHERE adh.ID = @AdvanceID";

            var parameters = new { AdvanceID = advanceID};

            List<AdvanceDTO> advances = _dbConnection.Query<AdvanceDTO>(sqlQuery, parameters).ToList();

            return advances;
        }
        public List<AdvancesPendingApprovalSelectDTO> GetAdvanceForPendingApprovalDetailByID(int advanceID)
        {
            var sqlQuery = @"
        select emp.Name,ttl.TitleName,pym.DeterminedPaymentDate,bsu.BusinessUnitName,adh.StatusID,adh.TransactorID,sts.StatusName,adv.ID,adv.RequestDate,adv.AdvanceAmount,adh.Date,adv.DesiredDate,prj.ProjectName,adh.ApprovedAmount,adv.AdvanceDescription from Advance adv 
inner join Employee emp ON emp.ID = adv.EmployeeID
inner join Title ttl ON ttl.ID = emp.TitleID
inner join BusinessUnit bsu ON bsu.ID = emp.BusinessUnitID
inner join [Status] sts ON sts.ID = adv.StatusID
left join Project prj ON prj.ID = adv.ProjectID
inner join AdvanceHistory adh ON adh.AdvanceID = adv.ID
left join Payment pym ON pym.AdvanceID = adv.ID
Where adv.ID = @AdvanceID";

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
                                using (var command = new SqlCommand("INSERT INTO Advance (AdvanceAmount, AdvanceDescription, ProjectID, DesiredDate,EmployeeID) VALUES (@AdvanceAmount, @AdvanceDescription, @ProjectID, @DesiredDate,@EmployeeID); SELECT SCOPE_IDENTITY();", _dbConnection as SqlConnection, transaction as SqlTransaction))
                                {
                                    command.Parameters.AddWithValue("@AdvanceAmount", advanceInsertDTO.AdvanceAmount);
                                    command.Parameters.AddWithValue("@EmployeeID", 9);

                                    command.Parameters.AddWithValue("@AdvanceDescription", advanceInsertDTO.AdvanceDescription);
                                    command.Parameters.AddWithValue("@ProjectID", advanceInsertDTO.ProjectID);
                                    command.Parameters.AddWithValue("@DesiredDate", advanceInsertDTO.DesiredDate);
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
                                using (var command = new SqlCommand("UPDATE Advance SET StatusID = @StatusID,isApproved = @isApproved WHERE ID = @ID", _dbConnection as SqlConnection, transaction as SqlTransaction))
                                {
                                    command.Parameters.AddWithValue("@ID", advanceUpdateDTO.ID);
                                    command.Parameters.AddWithValue("@StatusID", advanceUpdateDTO.StatusID);
                                    command.Parameters.AddWithValue("@isApproved", advanceUpdateDTO.isApproved);




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
