﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InnKeeper.Shared
{
    public class Inn
    {
        Random rand;

        public string Name { get; set; }
        public int TotalGold { get; private set; }

        int netGold;
        int numberOfCustomers = 0;
        int chanceForCustomer = 0;

        List<Room> rooms;

        public Inn(string name)
        {
            this.Name = name;
            rooms = new List<Room>();
            TotalGold = 900;
            netGold = 0;

            rand = new Random();
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);
            TotalGold -= room.Cost;
        }

        public List<Room> GetRooms()
        {
            return rooms;
        }

        public int CalculateExpenses()
        {
            int cost = 0;

            for (int i = 0; i < rooms.Count; i++)
            {
                cost += rooms[i].ExpenseRate;
            }
            return cost;
        }

        public int CalculateIncome()
        {
            int income = 0;
            for (int i = 0; i < rooms.Count; i++)
            {
                income += rooms[i].IncomeRate;
            }
            income += numberOfCustomers * 5;
            return income;
        }

        public void ProcessUpdate()
        {
            netGold = CalculateIncome() - CalculateExpenses();
            TotalGold += netGold;

            // determine if a customer has arrived
            if(rand.Next(0,101) <= chanceForCustomer)
            {
                numberOfCustomers++;
            }

        }

        public int GetNetGold()
        {
            return netGold;
        }

        public void IncreaseCustomerChance(int percent)
        {
            chanceForCustomer += percent;

            if(chanceForCustomer > 100)
            {
                chanceForCustomer = 100;
            }
            if(chanceForCustomer < 0)
            {
                chanceForCustomer = 0;
            }
        }

        public void AddCustomer()
        {
            numberOfCustomers++;
        }

        public void RemoveCustomer()
        {
            numberOfCustomers--;

            if (numberOfCustomers < 0)
            {
                numberOfCustomers = 0;
            }
        }

        public int GetNumCustomers()
        {
            return numberOfCustomers;
        }
    }
}
