using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
	[Serializable]
	public class Ingredients
	{
		[SerializeField]
		List<Herb> herbs;

		public Ingredients()
		{
			herbs = new List<Herb>();
		}

		public List<Herb> Herbs
		{
			get { return herbs; }
		}

		public Ingredient[] Total
		{
			get
			{
				var total = new List<Ingredient>();
				for (int i = 0; i < herbs.Count; i++)
				{
					total.Add(herbs[i]);
				}
				return total.ToArray();
			}
		}

		public void Add(Ingredient ingredient)
		{
			if (ingredient is Herb)
			{
				Herbs.Add((Herb)ingredient);
			}
		}

		public void Remove(Ingredient ingredient)
		{
			if (ingredient is Herb)
			{
				Herbs.Remove((Herb)ingredient);
			}
		}
	}
}