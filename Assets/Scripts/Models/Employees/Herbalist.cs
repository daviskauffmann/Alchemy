using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Herbalist : Employee {
        [SerializeField]
        Region regionToSearch;
        [SerializeField]
        int excursions;
        [SerializeField]
        int herbsFound;
        [SerializeField]
        int rareHerbsFound;

        public Herbalist(string name, int salary)
            : base("Herbalist", name, salary) {
            regionToSearch = Region.Plains;
        }

        public Region RegionToSearch {
            get { return regionToSearch; }
            set { regionToSearch = value; }
        }

        public override void StartWorking() {
            base.StartWorking();

            World.Instance.HourChanged += FindHerb;
        }

        public override void StopWorking() {
            base.StopWorking();

            World.Instance.HourChanged -= FindHerb;
        }

        void FindHerb(object sender, IntEventArgs e) {
            excursions++;

            if (World.Instance.Random.Next(0, 100) < 10) {
                var herbs = new List<Herb>();

                foreach (var herb in World.Instance.HerbDatabase) {
                    herbs.Add((Herb)herb.Clone());

                    // foreach (var region in herb.Regions) {
                    //     if (region == regionToSearch) {
                    //         herbs.Add((Herb)herb.Clone());
                    //     }
                    // }
                }

                if (herbs.Count > 0) {
                    herbsFound++;

                    var herb = herbs[World.Instance.Random.Next(herbs.Count)];

                    World.Instance.Shop.DeliverIngredient(herb);
                }
            }
        }
    }
}