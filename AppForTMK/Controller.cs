using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppForTMK.Model;

namespace AppForTMK
{
    internal class Controller
    {
        private readonly Database _database = new Database();

        // Получение списка всех заказов
        public List<ProductionOrder> GetOrders(string workshop = null)
        {
            var orders = new List<ProductionOrder>();

            using var connection = _database.GetConnection();
            connection.Open();

            string query = "SELECT * FROM ProductionOrders";
            if (!string.IsNullOrEmpty(workshop))
            {
                query += " WHERE Workshop = @Workshop";
            }

            using var cmd = new NpgsqlCommand(query, connection);
            if (!string.IsNullOrEmpty(workshop))
            {
                cmd.Parameters.AddWithValue("Workshop", workshop);
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new ProductionOrder
                {
                    OrderId = reader.GetInt32(0),
                    Workshop = reader.GetString(1),
                    StartDate = reader.GetDateTime(2),
                    EndDate = reader.GetDateTime(3),
                    Status = reader.GetString(4)
                });
            }

            return orders;
        }

        // Создание нового заказа
        public void AddOrder(ProductionOrder order)
        {
            using var connection = _database.GetConnection();
            connection.Open();

            string query = @"INSERT INTO ProductionOrders (Workshop, StartDate, EndDate, Status) 
                             VALUES (@Workshop, @StartDate, @EndDate, @Status) RETURNING OrderId";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("Workshop", order.Workshop);
            cmd.Parameters.AddWithValue("StartDate", order.StartDate);
            cmd.Parameters.AddWithValue("EndDate", order.EndDate);
            cmd.Parameters.AddWithValue("Status", order.Status);

            order.OrderId = (int)cmd.ExecuteScalar();
        }

        // Удаление заказа
        public void DeleteOrder(int orderId)
        {
            using var connection = _database.GetConnection();
            connection.Open();

            string query = "DELETE FROM ProductionOrders WHERE OrderId = @OrderId";
            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("OrderId", orderId);
            cmd.ExecuteNonQuery();
        }

        // Добавление позиции к заказу
        public void AddOrderPosition(OrderPosition position)
        {
            using var connection = _database.GetConnection();
            connection.Open();

            string query = @"INSERT INTO OrderPositions 
                            (OrderId, SteelGrade, Diameter, WallThickness, Volume, Unit, Status)
                            VALUES (@OrderId, @SteelGrade, @Diameter, @WallThickness, @Volume, @Unit, @Status)";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("OrderId", position.OrderId);
            cmd.Parameters.AddWithValue("SteelGrade", position.SteelGrade);
            cmd.Parameters.AddWithValue("Diameter", position.Diameter);
            cmd.Parameters.AddWithValue("WallThickness", position.WallThickness);
            cmd.Parameters.AddWithValue("Volume", position.Volume);
            cmd.Parameters.AddWithValue("Unit", position.Unit);
            cmd.Parameters.AddWithValue("Status", position.Status);

            cmd.ExecuteNonQuery();
        }
    }
}
