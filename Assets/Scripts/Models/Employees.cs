using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Employees
    {
		World world;
        [SerializeField]
        List<Herbalist> herbalists;
        [SerializeField]
        List<Apothecary> apothecaries;
        [SerializeField]
        List<Shopkeeper> shopkeepers;
        [SerializeField]
        List<Guard> guards;

        public Employees(World world)
        {
			this.world = world;
            herbalists = new List<Herbalist>();
            apothecaries = new List<Apothecary>();
            shopkeepers = new List<Shopkeeper>();
            guards = new List<Guard>();

            this.world.DayChanged += PayEmployees;
        }

		public List<Herbalist> Herbalists
		{
			get { return herbalists; }
		}

		public List<Apothecary> Apothecaries
		{
			get { return apothecaries; }
		}

		public List<Shopkeeper> Shopkeepers
		{
			get { return shopkeepers; }
		}

		public List<Guard> Guards
		{
			get { return guards; }
		}

		public Employee[] Total
		{
			get
			{
				var total = new List<Employee>();
				for (int i = 0; i < Herbalists.Count; i++)
				{
					total.Add(Herbalists[i]);
				}
				for (int i = 0; i < Apothecaries.Count; i++)
				{
					total.Add(Apothecaries[i]);
				}
				for (int i = 0; i < Shopkeepers.Count; i++)
				{
					total.Add(Shopkeepers[i]);
				}
				for (int i = 0; i < Guards.Count; i++)
				{
					total.Add(Guards[i]);
				}
				return total.ToArray();
			}
		}

		public void Add(Employee employee)
        {
            if (employee is Herbalist)
            {
                herbalists.Add((Herbalist)employee);
            }
            if (employee is Apothecary)
            {
                apothecaries.Add((Apothecary)employee);
            }
            if (employee is Shopkeeper)
            {
                shopkeepers.Add((Shopkeeper)employee);
            }
            if (employee is Guard)
            {
                guards.Add((Guard)employee);
            }

            employee.StartWorking();
        }

        public void Remove(Employee employee)
        {
            if (employee is Herbalist)
            {
                herbalists.Remove((Herbalist)employee);
            }
            if (employee is Apothecary)
            {
                apothecaries.Remove((Apothecary)employee);
            }
            if (employee is Shopkeeper)
            {
                shopkeepers.Remove((Shopkeeper)employee);
            }
            if (employee is Guard)
            {
                guards.Remove((Guard)employee);
            }

            employee.StopWorking();
        }

        void PayEmployees(object sender, IntEventArgs e)
        {
            for (int i = 0; i < Total.Length; i++)
            {
                world.Shop.Gold -= Total[i].Salary;
            }
        }
    }
}