using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public abstract class Ingredient : ICloneable {
        [SerializeField]
        protected string name;
        [SerializeField]
        protected Effect[] effects;

        public string Name => this.name;

        public Effect[] Effects => this.effects;

        protected Ingredient(string name, Effect[] effects) {
            this.name = name;
            this.effects = effects;
        }

        public virtual object Clone() {
            var ingredient = (Ingredient)this.MemberwiseClone();

            ingredient.effects = new Effect[this.effects.Length];
            for (var i = 0; i < this.effects.Length; i++) {
                ingredient.effects[i] = (Effect)this.effects[i].Clone();
            }

            return ingredient;
        }
    }

    public class IngredientEventArgs : EventArgs {
        public Ingredient Ingredient { get; }

        public IngredientEventArgs(Ingredient ingredient) {
            this.Ingredient = ingredient;
        }
    }
}
