using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace AppForTMK
{
    internal class Model
    {
        public class ProductionOrder
        {
            public int OrderId { get; set; }
            public string Workshop { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Status { get; set; }
        }

        public class OrderPosition
        {
            public int PositionId { get; set; }
            public int OrderId { get; set; }
            public string SteelGrade { get; set; }
            public decimal Diameter { get; set; }
            public decimal WallThickness { get; set; }
            public decimal Volume { get; set; }
            public string Unit { get; set; }
            public string Status { get; set; }
        }

        public class Database
        {
            private const string ConnectionString = "Host=localhost;Port=5432;Username=your_user;Password=your_password;Database=your_db";

            public NpgsqlConnection GetConnection()
            {
                return new NpgsqlConnection(ConnectionString);
            }
        }
    }
}
