using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Shopkeeper : Employee
    {
        [SerializeField]
        int _charisma;

        public Shopkeeper(World world, string name, int salary)
            : base(world, "Shopkeeper", name, salary)
        {

        }

        public override void StartWorking()
        {
            base.StartWorking();

            _world.HourChanged += SellPotion;
        }

        public override void StopWorking()
        {
            base.StopWorking();

            _world.HourChanged -= SellPotion;
        }

        void SellPotion(object sender, IntEventArgs e)
        {
            if (_world.Random.Next(0, 100) < 10)
            {
                Potion potion = null;
                float value = -1;
                for (int i = 0; i < _world.Shop.PotionsForSale.Count; i++)
                {
                    if (_world.Shop.PotionsForSale[i].Value > value)
                    {
                        potion = _world.Shop.PotionsForSale[i];
                        value = _world.Shop.PotionsForSale[i].Value;
                    }
                }
                if (potion != null)
                {
                    _world.Shop.SellPotion(potion);
                }
            }
        }
    }
}