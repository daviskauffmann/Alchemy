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
        List<Herbalist> herbalists;
        [SerializeField]
        List<Apothecary> apothecaries;
        [SerializeField]
        List<Shopkeeper> shopkeepers;
        [SerializeField]
        List<Guard> guards;

        public Applicants(World world)
        {
			this.world = world;
            herbalists = new List<Herbalist>();
            apothecaries = new List<Apothecary>();
            shopkeepers = new List<Shopkeeper>();
            guards = new List<Guard>();
        }

		public event EventHandler<IntEventArgs> CountChanged;

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

		public void Add(Employee applicant)
        {
            if (applicant is Herbalist)
            {
                herbalists.Add((Herbalist)applicant);
            }
            if (applicant is Apothecary)
            {
                apothecaries.Add((Apothecary)applicant);
            }
            if (applicant is Shopkeeper)
            {
                shopkeepers.Add((Shopkeeper)applicant);
            }
            if (applicant is Guard)
            {
                guards.Add((Guard)applicant);
            }

            OnCountChanged(Total.Length);
        }

        public void Remove(Employee applicant)
        {
            if (applicant is Herbalist)
            {
                herbalists.Remove((Herbalist)applicant);
            }
            if (applicant is Apothecary)
            {
                apothecaries.Remove((Apothecary)applicant);
            }
            if (applicant is Shopkeeper)
            {
                shopkeepers.Remove((Shopkeeper)applicant);
            }
            if (applicant is Guard)
            {
                guards.Remove((Guard)applicant);
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