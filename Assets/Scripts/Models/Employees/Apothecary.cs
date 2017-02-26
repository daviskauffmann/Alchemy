using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Apothecary : Employee
    {
        [SerializeField]
        int _potionsCrafted;

        public Apothecary(World world, string name, int salary)
            : base(world, "Apothecary", name, salary)
        {
			
        }

        public override void StartWorking()
        {
            base.StartWorking();
            _world.HourChanged += CreatePotion;
        }

        public override void StopWorking()
        {
            base.StopWorking();
            _world.HourChanged -= CreatePotion;
        }

        void CreatePotion(object sender, IntEventArgs e)
        {
            if (_world.Random.Next(0, 100) < 10)
            {
                for (int i = 0; i < _world.Shop.PotionPrototypes.Count; i++)
                {
                    Flask flask = null;
                    for (int j = 0; j < _world.Shop.Flasks.Count; j++)
                    {
                        if (_world.Shop.Flasks[j].Amount > 0)
                        {
                            if (_world.Shop.Flasks[j].Name == _world.Shop.PotionPrototypes[i].Flask.Name)
                            {
                                flask = _world.Shop.Flasks[j];
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
                    for (int j = 0; j < _world.Shop.PotionPrototypes[i].Herbs.Length; j++)
                    {
                        for (int k = 0; k < _world.Shop.Ingredients.Herbs.Count; k++)
                        {
                            if (_world.Shop.Ingredients.Herbs[k].Amount > 0)
                            {
                                if (_world.Shop.Ingredients.Herbs[k].Name == _world.Shop.PotionPrototypes[i].Herbs[j].Name)
                                {
                                    ingredients.Add(_world.Shop.Ingredients.Herbs[k]);
                                    break;
                                }
                            }
                        }
                    }
                    if (ingredients.Count != _world.Shop.PotionPrototypes[i].IngredientCount)
                    {
                        continue;
                    }

                    _world.Shop.CreatePotion(flask, solvent, ingredients.ToArray());
                    break;
                }
            }
        }
    }
}