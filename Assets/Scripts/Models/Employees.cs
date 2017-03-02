using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
	[Serializable]
	public class Employees
	{
		[SerializeField]
		List<Apothecary> apothecaries;
		[SerializeField]
		List<Guard> guards;
		[SerializeField]
		List<Herbalist> herbalists;
		[SerializeField]
		List<Shopkeeper> shopkeepers;

		public Employees()
		{
			apothecaries = new List<Apothecary>();
			guards = new List<Guard>();
			herbalists = new List<Herbalist>();
			shopkeepers = new List<Shopkeeper>();

			for (int i = 0; i < Total.Length; i++)
			{
				Total[i].StartWorking();
			}

			World.Instance.DayChanged += PayEmployees;
		}

		public List<Apothecary> Apothecaries
		{
			get { return apothecaries; }
		}

		public List<Guard> Guards
		{
			get { return guards; }
		}

		public List<Herbalist> Herbalists
		{
			get { return herbalists; }
		}

		public List<Shopkeeper> Shopkeepers
		{
			get { return shopkeepers; }
		}

		public Employee[] Total
		{
			get
			{
				var total = new List<Employee>();
				for (int i = 0; i < Apothecaries.Count; i++)
				{
					total.Add(Apothecaries[i]);
				}
				for (int i = 0; i < Guards.Count; i++)
				{
					total.Add(Guards[i]);
				}
				for (int i = 0; i < Herbalists.Count; i++)
				{
					total.Add(Herbalists[i]);
				}
				for (int i = 0; i < Shopkeepers.Count; i++)
				{
					total.Add(Shopkeepers[i]);
				}
				return total.ToArray();
			}
		}

		public void Add(Employee employee)
		{
			if (employee is Apothecary)
			{
				apothecaries.Add((Apothecary)employee);
			}
			else if (employee is Guard)
			{
				guards.Add((Guard)employee);
			}
			else if (employee is Herbalist)
			{
				herbalists.Add((Herbalist)employee);
			}
			else if (employee is Shopkeeper)
			{
				shopkeepers.Add((Shopkeeper)employee);
			}

			employee.StartWorking();
		}

		public void Remove(Employee employee)
		{
			if (employee is Apothecary)
			{
				apothecaries.Remove((Apothecary)employee);
			}
			else if (employee is Guard)
			{
				guards.Remove((Guard)employee);
			}
			else if (employee is Herbalist)
			{
				herbalists.Remove((Herbalist)employee);
			}
			else if (employee is Shopkeeper)
			{
				shopkeepers.Remove((Shopkeeper)employee);
			}

			employee.StopWorking();
		}

		void PayEmployees(object sender, IntEventArgs e)
		{
			for (int i = 0; i < Total.Length; i++)
			{
				World.Instance.Shop.Gold -= Total[i].Salary;
			}
		}
	}
}