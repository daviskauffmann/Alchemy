using System;
using UnityEngine;

namespace Alchemy.Models
{
	[Serializable]
	public class Herb : Ingredient
	{
		[SerializeField]
		Rarity rarity;
		[SerializeField]
		Region[] regions;

		public Herb(string name, Effect[] effects, Rarity rarity, Region[] regions)
			: base(name, effects)
		{
			this.rarity = rarity;
			this.regions = regions;
		}

		public Rarity Rarity
		{
			get { return rarity; }
		}

		public Region[] Regions
		{
			get { return regions; }
		}
	}

	public enum Rarity
	{
		Common,
		Uncommon,
		Rare
	}

	public enum Region
	{
		Plains,
		Forest,
		Desert,
		Tundra
	}

	public class HerbEventArgs : EventArgs
	{
		public Herb herb { get; set; }
	}
}