using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Linq;
using System.Text;
using System.IO;
using Practice;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.ServiceModel.Channels;

namespace pr
{
    class Program
    {
        static void Main(string[] args) 
        { 

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Clients.Add(new Client { ID = 11, FIO = "Лисичкин Андрей Алексеевич", DateofBirth = new DateTime(2003, 08, 21), Tel = "+79268963312" });
                //db.SaveChanges();
                var qСlients = db.Clients.ToArray();
                Console.WriteLine("Список Клиентов");
                foreach (Client q in qСlients)
                {
                    Console.WriteLine(q.ID + " " + q.FIO + " " + q.DateofBirth + " " + q.Tel + " ");
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.Add(new Employee { ID = 12, FIO = "Федикин Владислав Александрович", birthday = new DateTime(2002, 07, 23), passport = "4617867943", tel = "+79036409843", Rank = "Звукорежиссёр", Experience = 4 });
                //db.SaveChanges();
                var employees = db.Employees.ToArray();
                Console.WriteLine("Список Сотрудников");
                foreach (Employee p in employees)
                {
                    Console.WriteLine(p.ID + " " + p.FIO + " " + p.birthday + " " + p.passport + " " + p.tel + " " + p.Rank + " " + p.Experience);
                }
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                var clients = db.Clients.ToArray();
                Console.WriteLine("Список Клиентов:");
                foreach (Client client in clients)
                {
                    Console.WriteLine(client.ID + " " + client.DateofBirth + " " + client.Tel);
                }
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                Client? clientUpd = (from client in db.Clients where client.ID == 3 select client).First();
                if (clientUpd != null)
                {
                    clientUpd.Tel = "+79912955211";
                    db.SaveChanges();
                }
                var clients = db.Clients.ToArray();
                Console.WriteLine("Список клиентов:");
                foreach (Client client in clients)
                {
                    Console.WriteLine(client.ID + " " + client.FIO + " " + client.DateofBirth + " " + client.Tel);
                }
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                Client? clientDel = (from client in db.Clients where client.ID == 3 select client).First();
                if (clientDel != null)
                {
                    db.Clients.Remove(clientDel);
                    db.SaveChanges();
                }
                var clients = db.Clients.ToArray();
                Console.WriteLine("Список объектов");
                foreach (Client client in clients)
                {
                    Console.WriteLine(client.ID + " " + client.FIO + " " + client.DateofBirth + " " + client.Tel);
                }

            }

            //вывести всех Сотрудников
            using (ApplicationContext db = new ApplicationContext())
            {
                var Employee = db.Employees.ToArray();
                Console.WriteLine("Список Сотрудников:");
                foreach (Employee u in Employee)
                {
                    Console.WriteLine(u.ID + " " + u.FIO + " " + u.birthday + " " + u.passport + " " + u.tel + " " + u.Rank + " " + u.Experience);
                }
            }

            Console.WriteLine();

            // вывести названия типов записи, Id которых 3 или более
            using (ApplicationContext db = new ApplicationContext())
            {
                var Type = from p in db.RecordTypes.ToArray()
                      where p.ID >= 3
                      select p.name;


                foreach (var p in Type)
                {
                    Console.WriteLine(p);

                }
            }

            Console.WriteLine();

            // вывести данные из двух таблиц
            using (ApplicationContext db = new ApplicationContext())
            {


                var I = db.Sessions.Include(u => u.IDPayment).ToArray();



                foreach (var p in I)
                {
                    Console.WriteLine(p.IDclient + " " + p.ServiceNumber + " " + p.IDSession );
                }
            }

            Console.WriteLine();

            // увеличить цены поставок на 25%
            using (ApplicationContext db = new ApplicationContext())
            {

                var Type = db.RecordTypes.ToArray().Select(p => new
                {
                    ID = p.ID,
                    CostPerHour = p.CostPerHour * 1.25
                });
                foreach (var u in Type)
                {
                    Console.WriteLine(u.ID + " " + u.CostPerHour);

                }

            }

            Console.WriteLine();

            // добавить к Сотруднику с Индексом 1 Admin
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee? upduser = (from Employee in db.Employees where Employee.ID == 1 select Employee).First();
                if (upduser != null)
                {
                    upduser.FIO = upduser.FIO + " Admin";
                    db.SaveChanges();
                }
                var employees = db.Employees.ToArray();
                Console.WriteLine("Список объектов");
                foreach (Employee u in employees)
                {
                    Console.WriteLine(u.ID + " " + u.FIO + " " + u.birthday + " " + u.passport + " " + u.tel + " " + u.Rank + " " + u.Experience);
                }

            }

            Console.WriteLine();

            // вывести ФИО клиентов, идентификатор которых 102 и более
            using (ApplicationContext db = new ApplicationContext())
            {
                var clients = from p in db.Clients.ToArray()
                              where p.ID >= 102
                              select p.FIO;

                foreach (var p in clients)
                {
                    Console.WriteLine(p);

                }
            }

            Console.WriteLine();

            // вывести декартово произведение столбцов 
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = from u in db.Sessions.ToArray()
                            from p in db.Clients.ToArray()
                            select new
                            {
                                dayTime = u.dayTime,
                                FIO = p.FIO
                            };
                foreach (var c in users)
                {
                    Console.WriteLine(c.dayTime + " " + c.FIO);

                }

            }
            
            // изменить цены на записи на 1500 тысячи и вывести их цену и название
            using (ApplicationContext db = new ApplicationContext())
            {
                var types = from u in db.RecordTypes.ToArray()
                               let CostPerHour = u.CostPerHour + 1500
                               select new
                               {
                                   name = u.name,
                                   CostPerHour = CostPerHour
                               };

                foreach (var u in types)
                {
                    Console.WriteLine(u.name + " " + u.CostPerHour);

                }
            }

            // вывести данные в порядке: статус заказа, идентификатор заказа и ФИО клиента
            using (ApplicationContext db = new ApplicationContext())
            {
                var jointables = db.Sessions.ToArray().Join(db.DiscountCards.ToArray(), s => s.IDcard, p => p.Id, (s, p) =>
                new { card = p.card, order = p.Id, client = p.FIO });
                foreach (var j in jointables)
                {
                    Console.WriteLine(j.card + "     " + j.order + "      " + j.client);
                }
            }
            // добавить новую оплату
            using (ApplicationContext db = new ApplicationContext())
            {
                int id = db.Payments.Max(r => r.IDPayment);
                Console.WriteLine($"Max id: {id}");

                Payment test = new Payment
                {
                    IDPayment = id + 1,
                    IDSession = 4,
                    Result = 6,
                    Entered = 7,
                    Change = 1,
                    DatePayment = DateTime.Now
                };
                db.Payments.Add(test);
                db.SaveChanges();
                var payments = db.Payments.ToArray();
                Console.WriteLine("Список оплат:");
                foreach (Payment u in payments)
                {
                    Console.WriteLine(u.IDPayment + " " + u.IDSession + " " + u.Result + " " + u.Entered + " " + u.Change + " " + u.DatePayment);
                }

            }
            // Связанный запрос: Таблица Payment связываем с таблицей Session
            using (ApplicationContext db = new ApplicationContext())
            {
                int maxId = db.Payments.Max(p => p.IDPayment);
                Console.WriteLine($"Max id: {maxId}");

                Payment test = new Payment { IDSession = maxId + 1, IDPayment = 11, Result = 4000, Entered = 5000, Change = 1000, DatePayment = new DateTime(2022,08,19)};
                db.Payments.Add(test);
                db.SaveChanges();
                var payments = db.Payments.ToArray();
                Console.WriteLine("Список оплат:");
                foreach (Payment u in payments)
                {
                    Console.WriteLine(u.IDSession + " " + u.IDPayment + " " + u.Result + " " + u.Entered + " " + u.Change + " " + u.DatePayment);
                }

                int maxId1 = db.Sessions.Max(p => p.IDSession);
                Console.WriteLine($"Max id: {maxId1}");

                Session test1 = new Session { IDSession = maxId1 + 1, IDclient = 330, IDZapis = 20, IDcard = 20, IDPayment = 20, ServiceNumber = 20, dayTime = new DateTime(2022,08,21), Duration = new DateTime(09,23,40)};
                db.Sessions.Add(test1);
                db.SaveChanges();
                var sessions = db.Sessions.ToArray();
                Console.WriteLine("Список сеансов:");
                foreach (Session u in sessions)
                {
                    Console.WriteLine(u.IDSession + " " + u.IDclient + " " + u.IDZapis + " " + u.IDcard + " " + u.IDPayment + " " + u.ServiceNumber + " " + u.dayTime + " " + u.Duration);
                }

            }
        }
    }
}