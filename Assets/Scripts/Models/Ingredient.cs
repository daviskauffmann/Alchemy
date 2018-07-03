using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public abstract class Ingredient : ICloneable {
        [SerializeField]
        protected string name;
        [SerializeField]
        protected Effect[] effects;

        public string Name {
            get { return name; }
        }

        public Effect[] Effects {
            get { return effects; }
        }

        protected Ingredient(string name, Effect[] effects) {
            this.name = name;
            this.effects = effects;
        }

        public virtual object Clone() {
            var ingredient = (Ingredient)MemberwiseClone();

            ingredient.effects = new Effect[effects.Length];
            for (int i = 0; i < effects.Length; i++) {
                ingredient.effects[i] = (Effect)effects[i].Clone();
            }

            return ingredient;
        }
    }

    public class IngredientEventArgs : EventArgs {
        private Ingredient ingredient;

        public Ingredient Ingredient {
            get { return ingredient; }
        }

        public IngredientEventArgs(Ingredient ingredient) {
            this.ingredient = ingredient;
        }
    }
}