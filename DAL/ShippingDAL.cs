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
    public class ShippingDAL : IShipping
    {

        private readonly IConfiguration _config;
        private readonly Connect _conn;

        public ShippingDAL(IConfiguration config)
        {
            _config = config;
            _conn = new Connect(_config);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shipping> GetAll()
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"SELECT * FROM Shipping ORDER BY ShippingId DESC";
                var shipping = conn.Query<Shipping>(strSql);
                return shipping;
            }
        }

        public Shipping GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Shipping obj)
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"INSERT INTO Shipping (OrderHeaderId, CurrierId, ShippingAddress, ShippingVendor, ShippingDate, ShippingStatus, ShippingInformation, EstimatedShipping, ItemWeight, ShippingCosts) VALUES (@OrderHeaderId, @CurrierId, @ShippingAddress, @ShippingVendor, @ShippingDate, @ShippingStatus, @ShippingInformation, @EstimatedShipping, @ItemWeight, @ShippingCosts)";
                var param = new
                {
                    OrderHeaderId = obj.OrderHeaderId,
                    CurrierId = obj.CurrierId,
                    ShippingAddress = obj.ShippingAddress,
                    ShippingVendor = obj.ShippingVendor,
                    ShippingDate = obj.ShippingDate,
                    ShippingStatus = obj.ShippingStatus,
                    ShippingInformation = obj.ShippingInformation,
                    EstimatedShipping = obj.EstimatedShipping,
                    ItemWeight = obj.ItemWeight,
                    ShippingCosts = obj.ShippingCosts
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

        public void Update(Shipping obj)
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"UPDATE Shipping SET OrderHeaderId = @OrderHeaderId, CurrierId = @CurrierId, ShippingAddress = @ShippingAddress, ShippingVendor = @ShippingVendor, ShippingDate = @ShippingDate, ShippingStatus = @ShippingStatus, ShippingInformation = @ShippingInformation, EstimatedShipping = @EstimatedShipping, ItemWeight = @ItemWeight, ShippingCosts = @ShippingCosts WHERE ShippingId = @ShippingId";
                var param = new
                {
                    ShippingId = obj.ShippingId,
                    OrderHeaderId = obj.OrderHeaderId,
                    CurrierId = obj.CurrierId,
                    ShippingAddress = obj.ShippingAddress,
                    ShippingVendor = obj.ShippingVendor,
                    ShippingDate = obj.ShippingDate,
                    ShippingStatus = obj.ShippingStatus,
                    ShippingInformation = obj.ShippingInformation,
                    EstimatedShipping = obj.EstimatedShipping,
                    ItemWeight = obj.ItemWeight,
                    ShippingCosts = obj.ShippingCosts
                };
                try
                {
                    var newId = conn.ExecuteScalar<int>(strSql, param);
                    obj.ShippingId = newId;
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException(sqlEx.Message);
                }
            }
        }
    }
}