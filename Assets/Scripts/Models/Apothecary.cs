using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Apothecary : Employee
    {
        [SerializeField]int _potionsCrafted;

        public Apothecary(string name, int salary)
            : base("Apothecary", name, salary)
        {
            
        }

        public override void StartWorking()
        {
            base.StartWorking();
            World.Instance.HourChanged += CreatePotion;
        }

        public override void StopWorking()
        {
            base.StopWorking();
            World.Instance.HourChanged -= CreatePotion;
        }

        void CreatePotion(object sender, IntEventArgs e)
        {
            if (World.Instance.Random.Next(0, 100) < 10)
            {
                for (int i = 0; i < World.Instance.Shop.PotionPrototypes.Count; i++)
                {
                    Flask flask = null;
                    for (int j = 0; j < World.Instance.Shop.Flasks.Count; j++)
                    {
                        if (World.Instance.Shop.Flasks[j].Amount > 0)
                        {
                            if (World.Instance.Shop.Flasks[j].Name == World.Instance.Shop.PotionPrototypes[i].Flask.Name)
                            {
                                flask = World.Instance.Shop.Flasks[j];
                                break;
                            }
                        }
                    }
                    if (flask == null)
                    {
                        continue;
                    }

                    Solvent solvent = null;

                    var ingredients = new List<Ingredient>();
                    for (int j = 0; j < World.Instance.Shop.PotionPrototypes[i].Herbs.Length; j++)
                    {
                        for (int k = 0; k < World.Instance.Shop.Ingredients.Herbs.Count; k++)
                        {
                            if (World.Instance.Shop.Ingredients.Herbs[k].Amount > 0)
                            {
                                if (World.Instance.Shop.Ingredients.Herbs[k].Name == World.Instance.Shop.PotionPrototypes[i].Herbs[j].Name)
                                {
                                    ingredients.Add(World.Instance.Shop.Ingredients.Herbs[k]);
                                    break;
                                }
                            }
                        }
                    }
                    if (ingredients.Count != World.Instance.Shop.PotionPrototypes[i].IngredientCount)
                    {
                        continue;
                    }

                    World.Instance.Shop.CreatePotion(flask, solvent, ingredients.ToArray());
                    break;
                }
            }
        }
    }
}