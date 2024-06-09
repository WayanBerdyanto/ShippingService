using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ShippingService.Connections;
using ShippingService.DAL.Interfaces;
using ShippingService.Models;

namespace ShippingService.DAL
{
    public class CurrierDAL : ICurrier
    {

        private readonly IConfiguration _config;
        private readonly Connect _conn;

        public CurrierDAL(IConfiguration config)
        {
            _config = config;
            _conn = new Connect(_config);
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Currier> GetAll()
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"SELECT * FROM Currier ORDER BY CurrierId DESC";
                var detailPayment = conn.Query<Currier>(strSql);
                return detailPayment;
            }
        }

        public Currier GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Currier obj)
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"INSERT INTO Currier (CurrierName, CurrierAddress, CurrierPhone) VALUES (@CurrierName, @CurrierAddress, @CurrierPhone); select @@IDENTITY";
                var param = new
                {
                    CurrierName = obj.CurrierName,
                    CurrierAddress = obj.CurrierAddress,
                    CurrierPhone = obj.CurrierPhone
                };
                try
                {
                    conn.Execute(strSql, param);

                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException(sqlEx.Message);
                }
            }
        }

        public void Update(Currier obj)
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"UPDATE Currier SET CurrierName = @CurrierName, CurrierAddress = @CurrierAddress, CurrierPhone = @CurrierPhone WHERE CurrierId = @CurrierId";
                var param = new
                {
                    CurrierId = obj.CurrierId,
                    CurrierName = obj.CurrierName,
                    CurrierAddress = obj.CurrierAddress,
                    CurrierPhone = obj.CurrierPhone
                };
                try
                {
                    conn.Execute(strSql, param);
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"Error: {sqlEx.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}");
                }
            }
        }
    }
}