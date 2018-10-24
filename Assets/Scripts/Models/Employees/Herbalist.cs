using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Herbalist : Employee {
        [SerializeField]
        private Region regionToSearch;
        [SerializeField]
        private int excursions;
        [SerializeField]
        private int herbsFound;
        [SerializeField]
        private int rareHerbsFound;

        public Region RegionToSearch {
            get { return this.regionToSearch; }
            set { this.regionToSearch = value; }
        }

        public Herbalist(string name, int salary) : base("Herbalist", name, salary) {
            this.regionToSearch = Region.Plains;
        }

        public override void StartWorking() {
            base.StartWorking();

            World.Instance.HourChanged += this.FindHerb;
        }

        public override void StopWorking() {
            base.StopWorking();

            World.Instance.HourChanged -= this.FindHerb;
        }

        private void FindHerb(object sender, IntEventArgs e) {
            if (World.Instance.Random.Next(0, 100) > 20) {
                return;
            }

            this.excursions++;

            var herbPrototypes = new List<Herb>();

            foreach (var herbPrototype in World.Instance.HerbDatabase) {
                // foreach (var region in herb.Regions) {
                //     if (region == regionToSearch) {
                herbPrototypes.Add(herbPrototype);
                //     }
                // }
            }

            if (herbPrototypes.Count > 0) {
                this.herbsFound++;

                var herbPrototype = herbPrototypes[World.Instance.Random.Next(herbPrototypes.Count)];
                var herb = (Herb)herbPrototype.Clone();

                foreach (var effect in herb.Effects) {
                    var magnitude = effect.Magnitude * (World.Instance.Random.Next(1, 10) / 10.0f);
                    effect.Magnitude = Mathf.RoundToInt(magnitude);
                }

                World.Instance.Shop.DeliverIngredient(herb);
            }
        }
    }
}
