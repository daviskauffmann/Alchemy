using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
	[Serializable]
	public class Herbalist : Employee
	{
		[SerializeField]
		Region regionToSearch;
		[SerializeField]
		int excursions;
		[SerializeField]
		int herbsFound;
		[SerializeField]
		int rareHerbsFound;

		public Herbalist(string name, int salary)
			: base("Herbalist", name, salary)
		{
			regionToSearch = Region.Plains;
		}

		public Region RegionToSearch
		{
			get { return regionToSearch; }
			set { regionToSearch = value; }
		}

		public override void StartWorking()
		{
			base.StartWorking();
			World.Instance.HourChanged += FindHerb;
		}

		public override void StopWorking()
		{
			base.StopWorking();
			World.Instance.HourChanged -= FindHerb;
		}

		void FindHerb(object sender, IntEventArgs e)
		{
			if (World.Instance.Random.Next(0, 100) < 10)
			{
				var herbs = new List<Herb>();
				for (int i = 0; i < World.Instance.HerbDatabase.Length; i++)
				{
					/*for (int j = 0; j < World.Instance.HerbDatabase[i].Regions.Length; j++)
                    {
                        if (World.Instance.HerbDatabase[i].Regions[j] == RegionToSearch)
                        {
                            herbs.Add((Herb)World.Instance.HerbDatabase[i].Clone());
                        }
                    }*/
					herbs.Add((Herb)World.Instance.HerbDatabase[i].Clone());
				}
				if (herbs.Count > 0)
				{
					var herb = herbs[World.Instance.Random.Next(herbs.Count)];
					World.Instance.Shop.DeliverIngredient(herb);
					herbsFound++;
				}
			}
			excursions++;
		}
	}
}