using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Potion {
        [SerializeField]
        private string name;
        [SerializeField]
        private string flaskName;
        [SerializeField]
        private string solventName;
        [SerializeField]
        private string[] herbNames;
        [SerializeField]
        private Effect[] effects;
        [SerializeField]
        private float value;

        public string Name => this.name;

        public string FlaskName => this.flaskName;

        public string SolventName => this.solventName;

        public string[] HerbNames => this.herbNames;

        public int IngredientCount => this.HerbNames.Length /*+ OtherTypeNames.Length*/;

        public Effect[] Effects => this.effects;

        public float Value => this.value;

        public Potion(Flask flask, Solvent solvent, Ingredient[] ingredients) {
            var herbNames = new List<string>();
            // var otherTypeNames = new List<string>();

            foreach (var ingredient in ingredients) {
                if (ingredient is Herb) {
                    herbNames.Add(ingredient.Name);
                }/* else if (ingredient is OtherType) {
                    otherTypes.Add(ingredient.Name);
                }*/
            }

            var effectsToCheck = new Queue<Effect>();

            foreach (var ingredient in ingredients) {
                foreach (var effect in ingredient.Effects) {
                    effectsToCheck.Enqueue(effect);
                }
            }

            var effects = new List<Effect>();

            while (effectsToCheck.Count > 0) {
                var currentEffect = effectsToCheck.Dequeue();

                foreach (var effect in effectsToCheck) {
                    var combinedEffect = currentEffect.Combine(effect);

                    if (combinedEffect != null) {
                        effects.Add(combinedEffect);
                    }
                }
            }

            var name = flask.Quality.ToString() + " Potion of ";

            for (var i = 0; i < effects.Count; i++) {
                name += effects[i].Name;

                if (i < effects.Count - 1) {
                    name += ", ";
                }
            }

            this.name = name;
            this.flaskName = flask.Name;
            this.solventName = solvent == null ? string.Empty : solvent.Name;
            this.herbNames = herbNames.ToArray();
            // this.otherTypeNames = otherTypeNames.ToArray();
            this.effects = effects.ToArray();
            this.value = flask.Value * effects.Count;
        }
    }

    public class PotionEventArgs : EventArgs {
        public Potion Potion { get; }

        public PotionEventArgs(Potion potion) {
            this.Potion = potion;
        }
    }

    public class PotionCreatedEventArgs : PotionEventArgs {
        public Employee Creator { get; }

        public PotionCreatedEventArgs(Potion potion, Employee creator) : base(potion) {
            this.Creator = creator;
        }
    }

    public class PotionSoldEventArgs : PotionEventArgs {
        public Employee Seller { get; }

        public PotionSoldEventArgs(Potion potion, Employee seller) : base(potion) {
            this.Seller = seller;
        }
    }
}
