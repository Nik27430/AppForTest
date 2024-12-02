using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppForTMK.Model;
using static AppForTMK.Controller;

namespace AppForTMK
{
    internal class View
    {
        private readonly Controller _controller = new Controller();

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("=== Меню управления производственными заказами ===");
                Console.WriteLine("1. Показать все заказы");
                Console.WriteLine("2. Создать новый заказ");
                Console.WriteLine("3. Удалить заказ");
                Console.WriteLine("4. Добавить позицию к заказу");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите действие: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowOrders();
                        break;
                    case "2":
                        CreateOrder();
                        break;
                    case "3":
                        DeleteOrder();
                        break;
                    case "4":
                        AddPosition();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

        private void ShowOrders()
        {
            Console.Write("Введите цех (или оставьте пустым для всех заказов): ");
            var workshop = Console.ReadLine();

            var orders = _controller.GetOrders(workshop);
            foreach (var order in orders)
            {
                Console.WriteLine($"Заказ #{order.OrderId}: Цех {order.Workshop}, Статус: {order.Status}");
            }
        }

        private void CreateOrder()
        {
            Console.Write("Введите цех: ");
            var workshop = Console.ReadLine();

            Console.Write("Введите дату начала (ГГГГ-ММ-ДД): ");
            var startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите дату окончания (ГГГГ-ММ-ДД): ");
            var endDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите статус (новый/в работе/выполнен): ");
            var status = Console.ReadLine();

            var order = new ProductionOrder
            {
                Workshop = workshop,
                StartDate = startDate,
                EndDate = endDate,
                Status = status
            };

            _controller.AddOrder(order);
            Console.WriteLine("Заказ успешно создан.");
        }

        private void DeleteOrder()
        {
            Console.Write("Введите ID заказа для удаления: ");
            var orderId = int.Parse(Console.ReadLine());

            _controller.DeleteOrder(orderId);
            Console.WriteLine("Заказ успешно удален.");
        }

        private void AddPosition()
        {
            Console.Write("Введите ID заказа: ");
            var orderId = int.Parse(Console.ReadLine());

            Console.Write("Введите марку стали: ");
            var steelGrade = Console.ReadLine();

            Console.Write("Введите диаметр: ");
            var diameter = decimal.Parse(Console.ReadLine());

            Console.Write("Введите толщину стенки: ");
            var wallThickness = decimal.Parse(Console.ReadLine());

            Console.Write("Введите объем: ");
            var volume = decimal.Parse(Console.ReadLine());

            Console.Write("Введите единицу измерения: ");
            var unit = Console.ReadLine();

            Console.Write("Введите статус (новая/в работе/выполнена): ");
            var status = Console.ReadLine();

            var position = new OrderPosition
            {
                OrderId = orderId,
                SteelGrade = steelGrade,
                Diameter = diameter,
                WallThickness = wallThickness,
                Volume = volume,
                Unit = unit,
                Status = status
            };

            _controller.AddOrderPosition(position);
            Console.WriteLine("Позиция добавлена к заказу.");
        }
    }
}
