using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public abstract class Ingredient : ICloneable
    {
        [SerializeField]
        protected string name;
        [SerializeField]
        protected Effect[] effects;
		[SerializeField]
		protected int amount;

		protected Ingredient(string name, Effect[] effects)
        {
            this.name = name;
            this.effects = effects;
			amount = -1;
		}

		public string Name
		{
			get { return name; }
		}

		public Effect[] Effects
		{
			get { return effects; }
		}

		public int Amount
		{
			get { return amount; }
			set { amount = value; }
		}

		public virtual object Clone()
        {
            var ingredient = (Ingredient)MemberwiseClone();
            ingredient.Amount = -1;
            ingredient.effects = new Effect[effects.Length];
            for (int i = 0; i < effects.Length; i++)
            {
                ingredient.effects[i] = (Effect)effects[i].Clone();
            }
            return ingredient;
        }
    }

	public class IngredientEventArgs : EventArgs
	{
		public Ingredient ingredient { get; set; }
	}
}