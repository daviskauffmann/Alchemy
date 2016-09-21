using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Herbalist : Employee
    {
        [SerializeField]int _excursions;
        [SerializeField]int _herbsFound;
        [SerializeField]int _rareHerbsFound;

        public Herbalist(string name, int salary)
            : base("Herbalist", name, salary)
        {
            
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
                var herb = World.Instance.HerbDatabase[World.Instance.Random.Next(World.Instance.HerbDatabase.Length)];
                World.Instance.Shop.DeliverIngredient(herb);
            }
        }
    }
}