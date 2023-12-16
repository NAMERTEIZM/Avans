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

        ConnectionHelper connectionHelper;
        private IDbConnection _dbConnection;

        public AdvanceRepository(ConnectionHelper connectionHelper, IDbConnection dbConnection)
        {
            this.connectionHelper = connectionHelper;

            //_dbConnection = new SqlConnection(ConnectionHelper.GetConnectionString());
            _dbConnection = dbConnection;

            _dbConnection.Open();
        }

        public List<AdvanceDTO> GetAdvanceAll()
        {
            var sqlQuery = "select adv.AdvanceAmount,adv.RequestDate,adv.DesiredDate,prj.ProjectName,sts.StatusName,emp.[Name],ttl.TitleName,adh.Date,adh.ApprovedAmount,pym.DeterminedPaymentDate,rcp.ReceiptDate,rcp.isRefundReceipt from AdvanceHistory adh \r\ninner join Advance adv ON adv.ID = adh.AdvanceID \r\ninner join Project prj ON prj.ID=adv.ProjectID \r\nleft join [Status] sts ON sts.ID = adv.StatusID \r\nleft join Employee emp ON emp.ID = adh.TransactorID \r\nleft join Title ttl ON ttl.ID = emp.TitleID \r\nleft join Payment pym ON pym.ID = adv.PaymentID \r\nleft join Receipt rcp ON rcp.AdvanceID =adv.ID";

            List<AdvanceDTO> advances = _dbConnection.Query<AdvanceDTO>(sqlQuery).ToList();

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
                                using (var command = new SqlCommand("INSERT INTO Advance (AdvanceAmount, AdvanceDescription, ProjectID, DesiredDate) VALUES (@AdvanceAmount, @AdvanceDescription, @ProjectID, @DesiredDate); SELECT SCOPE_IDENTITY();", _dbConnection as SqlConnection,transaction as SqlTransaction))
                                {
                                    command.Parameters.AddWithValue("@AdvanceAmount", advanceInsertDTO.AdvanceAmount);
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




    }
}
