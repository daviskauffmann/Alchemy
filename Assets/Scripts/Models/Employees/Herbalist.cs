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

        public Herbalist(World world, string name, int salary)
            : base(world, "Herbalist", name, salary)
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
            world.HourChanged += FindHerb;
        }

        public override void StopWorking()
        {
            base.StopWorking();
            world.HourChanged -= FindHerb;
        }

        void FindHerb(object sender, IntEventArgs e)
        {
            if (world.Random.Next(0, 100) < 10)
            {
                var herbs = new List<Herb>();
                for (int i = 0; i < world.HerbDatabase.Length; i++)
                {
					/*for (int j = 0; j < _world.HerbDatabase[i].Regions.Length; j++)
                    {
                        if (_world.HerbDatabase[i].Regions[j] == RegionToSearch)
                        {
                            herbs.Add(_world.HerbDatabase[i]);
                        }
                    }*/
					herbs.Add(world.HerbDatabase[i]);
				}
                if (herbs.Count > 0)
                {
                    var herb = herbs[world.Random.Next(herbs.Count)];
                    world.Shop.DeliverIngredient(herb);
					herbsFound++;
                }
            }
			excursions++;
		}
    }
}