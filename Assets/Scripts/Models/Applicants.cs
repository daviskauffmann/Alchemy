using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
	[Serializable]
	public class Applicants
	{
		World world;
		[SerializeField]
		List<Apothecary> apothecaries;
		[SerializeField]
		List<Guard> guards;
		[SerializeField]
		List<Herbalist> herbalists;
		[SerializeField]
		List<Shopkeeper> shopkeepers;

		public event EventHandler<IntEventArgs> CountChanged;

		public Applicants(World world)
		{
			this.world = world;
			apothecaries = new List<Apothecary>();
			guards = new List<Guard>();
			herbalists = new List<Herbalist>();
			shopkeepers = new List<Shopkeeper>();
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

		public void Add(Employee applicant)
		{
			if (applicant is Apothecary)
			{
				apothecaries.Add((Apothecary)applicant);
			}
			else if (applicant is Guard)
			{
				guards.Add((Guard)applicant);
			}
			else if (applicant is Herbalist)
			{
				herbalists.Add((Herbalist)applicant);
			}
			else if (applicant is Shopkeeper)
			{
				shopkeepers.Add((Shopkeeper)applicant);
			}

			OnCountChanged(Total.Length);
		}

		public void Remove(Employee applicant)
		{
			if (applicant is Apothecary)
			{
				apothecaries.Remove((Apothecary)applicant);
			}
			if (applicant is Guard)
			{
				guards.Remove((Guard)applicant);
			}
			if (applicant is Herbalist)
			{
				herbalists.Remove((Herbalist)applicant);
			}
			if (applicant is Shopkeeper)
			{
				shopkeepers.Remove((Shopkeeper)applicant);
			}

			OnCountChanged(Total.Length);
		}

		protected virtual void OnCountChanged(int value)
		{
			if (CountChanged != null)
			{
				CountChanged(this, new IntEventArgs() { value = value });
			}
		}
	}
}