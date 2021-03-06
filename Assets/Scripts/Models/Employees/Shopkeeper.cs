﻿using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Shopkeeper : Employee {
        [SerializeField]
        private int charisma;
        [SerializeField]
        private int potionsSold;

        public Shopkeeper(string name, int salary) : base("Shopkeeper", name, salary) {

        }

        public override void StartWorking() {
            base.StartWorking();

            World.Instance.HourChanged += this.SellPotion;
        }

        public override void StopWorking() {
            base.StopWorking();

            World.Instance.HourChanged -= this.SellPotion;
        }

        private void SellPotion(object sender, IntEventArgs e) {
            if (World.Instance.Random.Next(0, 100) > 10) {
                return;
            }

            Potion potion = null;

            float value = -1;

            foreach (var shopPotion in World.Instance.Shop.PotionsForSale) {
                if (shopPotion.Value > value) {
                    potion = shopPotion;
                    value = shopPotion.Value;
                }
            }

            if (potion != null) {
                this.potionsSold++;

                World.Instance.Shop.SellPotion(potion, this);
            }
        }
    }
}
