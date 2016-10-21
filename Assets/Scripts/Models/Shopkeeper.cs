using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Shopkeeper : Employee
    {
        [SerializeField]int _charisma;

        public Shopkeeper(string name, int salary)
            : base("Shopkeeper", name, salary)
        {

        }

        public override void StartWorking()
        {
            base.StartWorking();

            World.Instance.HourChanged += SellPotion;
        }

        public override void StopWorking()
        {
            base.StopWorking();

            World.Instance.HourChanged -= SellPotion;
        }

        void SellPotion(object sender, IntEventArgs e)
        {
            if (World.Instance.Random.Next(0, 100) < 10)
            {
                Potion potion = null;
                float value = -1;
                for (int i = 0; i < World.Instance.Shop.PotionsForSale.Count; i++)
                {
                    if (World.Instance.Shop.PotionsForSale[i].Value > value)
                    {
                        potion = World.Instance.Shop.PotionsForSale[i];
                        value = World.Instance.Shop.PotionsForSale[i].Value;
                    }
                }
                if (potion != null)
                {
                    World.Instance.Shop.SellPotion(potion);
                }
            }
        }
    }
}