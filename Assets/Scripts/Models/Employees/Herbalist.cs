using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Herbalist : Employee
    {
        [SerializeField]
        Region _regionToSearch;
        [SerializeField]
        int _excursions;
        [SerializeField]
        int _herbsFound;
        [SerializeField]
        int _rareHerbsFound;

        public Region RegionToSearch
        {
            get { return _regionToSearch; }
            set { _regionToSearch = value; }
        }

        public Herbalist(World world, string name, int salary)
            : base(world, "Herbalist", name, salary)
        {
            _regionToSearch = Region.Plains;
        }

        public override void StartWorking()
        {
            base.StartWorking();
            _world.HourChanged += FindHerb;
        }

        public override void StopWorking()
        {
            base.StopWorking();
            _world.HourChanged -= FindHerb;
        }

        void FindHerb(object sender, IntEventArgs e)
        {
            if (_world.Random.Next(0, 100) < 10)
            {
                var herbs = new List<Herb>();
                for (int i = 0; i < _world.HerbDatabase.Length; i++)
                {
					/*for (int j = 0; j < _world.HerbDatabase[i].Regions.Length; j++)
                    {
                        if (_world.HerbDatabase[i].Regions[j] == RegionToSearch)
                        {
                            herbs.Add(_world.HerbDatabase[i]);
                        }
                    }*/
					herbs.Add(_world.HerbDatabase[i]);
				}
                if (herbs.Count > 0)
                {
                    var herb = herbs[_world.Random.Next(herbs.Count)];
                    _world.Shop.DeliverIngredient(herb);
                }
            }
        }
    }
}