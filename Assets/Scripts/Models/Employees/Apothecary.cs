using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
	[Serializable]
	public class Apothecary : Employee
	{
		[SerializeField]
		int potionsCrafted;

		public Apothecary(World world, string name, int salary)
			: base(world, "Apothecary", name, salary)
		{

		}

		public override void StartWorking()
		{
			base.StartWorking();
			world.HourChanged += CreatePotion;
		}

		public override void StopWorking()
		{
			base.StopWorking();
			world.HourChanged -= CreatePotion;
		}

		void CreatePotion(object sender, IntEventArgs e)
		{
			if (world.Random.Next(0, 100) < 10)
			{
				for (int i = 0; i < world.Shop.PotionPrototypes.Count; i++)
				{
					Flask flask = null;
					for (int j = 0; j < world.Shop.Flasks.Count; j++)
					{
						if (world.Shop.Flasks[j].Name == world.Shop.PotionPrototypes[i].Flask.Name)
						{
							flask = world.Shop.Flasks[j];
							break;
						}
					}
					if (flask == null)
					{
						continue;
					}

					Solvent solvent = null;

					var ingredients = new List<Ingredient>();
					for (int j = 0; j < world.Shop.PotionPrototypes[i].Herbs.Length; j++)
					{
						for (int k = 0; k < world.Shop.Ingredients.Herbs.Count; k++)
						{
							if (world.Shop.Ingredients.Herbs[k].Name == world.Shop.PotionPrototypes[i].Herbs[j].Name)
							{
								ingredients.Add(world.Shop.Ingredients.Herbs[k]);
								break;
							}
						}
					}
					if (ingredients.Count != world.Shop.PotionPrototypes[i].IngredientCount)
					{
						continue;
					}

					world.Shop.CreatePotion(flask, solvent, ingredients.ToArray(), this);
					potionsCrafted++;
					break;
				}
			}
		}
	}
}