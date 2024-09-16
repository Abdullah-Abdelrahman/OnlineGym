using Microsoft.EntityFrameworkCore;
using OnlineGym.Entities.Models;
using OnlineGym.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
    public class StaticsForAdmin
    {

        public int UserNumbers { get; set; }

        public int AprovedOrders { get; set; }

        public int ProccessedOrders { get; set; }

        public List<UsersMoney> Top10Users { get; set; }

        public List<SubscriptionCount> subscriptionsStatics { get; set; }



        public StaticsForAdmin(List<Client> users, List<ClientSubscription> Orders,List<Subscription> subscriptions)
        {

            this.UserNumbers = users.Count;
            this.AprovedOrders = Orders.Where(s => s.Status == SD.Aproved).ToList().Count;
            this.ProccessedOrders = Orders.Where(s => s.Status == SD.proccessed).ToList().Count;


            this.Top10Users = GetTop10Users(users, Orders);
            this.subscriptionsStatics = GetTop10Subscription(Orders,subscriptions);
        }

        public List<UsersMoney> GetTop10Users(List<Client> users, List<ClientSubscription> Orders)
        {
            List<UsersMoney> Top10 = new List<UsersMoney>();


            Top10 = GetAllUserMoney(users, Orders).OrderByDescending(s => s.Money).Take(10).ToList();


            return Top10;
        }


        public List<UsersMoney> GetAllUserMoney(List<Client> users, List<ClientSubscription> Orders)
        {

            List<UsersMoney> listOfuserMony = new List<UsersMoney>();
            foreach (var client in users)
            {
                int money = Orders.Where(s =>s.ClientId==client.Id&& s.Status != SD.Pending&& s.Status !=SD.Canceled).Select(i => i.price).Sum();
                listOfuserMony.Add(new UsersMoney() { Client = client, Money = money });
            }
           
            return listOfuserMony;
        }



        public List<SubscriptionCount> GetTop10Subscription(List<ClientSubscription> Orders,List<Subscription> subscriptions)
        {
            List<SubscriptionCount> Top10 = new List<SubscriptionCount>();

            Top10 = GetAllSubscriptionCount(subscriptions, Orders).OrderByDescending(s => s.Count).Take(10).ToList();
            return Top10;
        }

        public List<SubscriptionCount> GetAllSubscriptionCount(List<Subscription> subscriptions, List<ClientSubscription> Orders)
        {

            List<SubscriptionCount> listOfSubscriptionCount = new List<SubscriptionCount>();
            foreach (var sub in subscriptions)
            {
                int count = Orders.Where(s => s.SubscriptionId==sub.SubscriptionId&&s.Status != SD.Canceled &&s.Status !=SD.Pending).ToList().Count;

                listOfSubscriptionCount.Add(new SubscriptionCount() { Subscription = sub, Count = count });
            }

            return listOfSubscriptionCount;
        }
        public class UsersMoney
        {
            public Client Client { get; set; }

            public int Money { get; set; }
        }

        public class SubscriptionCount
        {
            public Subscription Subscription { get; set; }

            public int Count { get; set; }
        }
    }
}
