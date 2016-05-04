using System;
using System.Collections.Generic;
using System.Text;

namespace InnKeeper.Shared
{
    public class Inn
    {
        public string Name { get; set; }
        public int TotalGold { get; private set; }
        int netGold;
        List<Room> rooms;

        public Inn(string name)
        {
            this.Name = name;
            rooms = new List<Room>();
            TotalGold = 900;
            netGold = 0;
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
            return income;
        }

        public void ProcessUpdate()
        {
            netGold = CalculateIncome() - CalculateExpenses();
            TotalGold += netGold;
        }
    }
}
