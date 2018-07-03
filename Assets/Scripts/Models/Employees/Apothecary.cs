using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Apothecary : Employee {
        [SerializeField]
        int potionsCrafted;

        public Apothecary(string name, int salary)
            : base("Apothecary", name, salary) {

        }

        public override void StartWorking() {
            base.StartWorking();

            World.Instance.HourChanged += CreatePotion;
        }

        public override void StopWorking() {
            base.StopWorking();

            World.Instance.HourChanged -= CreatePotion;
        }

        void CreatePotion(object sender, IntEventArgs e) {
            if (World.Instance.Random.Next(0, 100) < 10) {
                foreach (var potionPrototype in World.Instance.Shop.PotionPrototypes) {
                    Flask flask = null;

                    foreach (var shopFlask in World.Instance.Shop.Flasks) {
                        if (shopFlask.Name == potionPrototype.Flask.Name) {
                            flask = shopFlask;

                            break;
                        }
                    }

                    if (flask == null) {
                        continue;
                    }

                    Solvent solvent = null;

                    var ingredients = new List<Ingredient>();

                    foreach (var herb in potionPrototype.Herbs) {
                        foreach (var shopHerb in World.Instance.Shop.Herbs) {
                            if (shopHerb.Name == herb.Name) {
                                ingredients.Add(shopHerb);

                                break;
                            }
                        }
                    }

                    if (ingredients.Count != potionPrototype.IngredientCount) {
                        continue;
                    }

                    potionsCrafted++;

                    World.Instance.Shop.CreatePotion(flask, solvent, ingredients.ToArray(), this);

                    break;
                }
            }
        }
    }
}