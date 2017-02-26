using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Shopkeeper : Employee
    {
        [SerializeField]
        int charisma;

        public Shopkeeper(World world, string name, int salary)
            : base(world, "Shopkeeper", name, salary)
        {

        }

        public override void StartWorking()
        {
            base.StartWorking();

            world.HourChanged += SellPotion;
        }

        public override void StopWorking()
        {
            base.StopWorking();

            world.HourChanged -= SellPotion;
        }

        void SellPotion(object sender, IntEventArgs e)
        {
            if (world.Random.Next(0, 100) < 10)
            {
                Potion potion = null;
                float value = -1;
                for (int i = 0; i < world.Shop.PotionsForSale.Count; i++)
                {
                    if (world.Shop.PotionsForSale[i].Value > value)
                    {
                        potion = world.Shop.PotionsForSale[i];
                        value = world.Shop.PotionsForSale[i].Value;
                    }
                }
                if (potion != null)
                {
                    world.Shop.SellPotion(potion, this);
                }
            }
        }
    }
}