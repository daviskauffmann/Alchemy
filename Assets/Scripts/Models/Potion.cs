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

        public string Name {
            get { return name; }
        }

        public string FlaskName {
            get { return flaskName; }
        }

        public string SolventName {
            get { return solventName; }
        }

        public string[] HerbNames {
            get { return herbNames; }
        }

        public int IngredientCount {
            get { return HerbNames.Length /*+ OtherTypeNames.Length*/; }
        }

        public Effect[] Effects {
            get { return effects; }
        }

        public float Value {
            get { return value; }
        }

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

            string name = flask.Quality.ToString() + " Potion of ";

            for (int i = 0; i < effects.Count; i++) {
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
        private Potion potion;

        public Potion Potion {
            get { return potion; }
        }

        public PotionEventArgs(Potion potion) {
            this.potion = potion;
        }
    }

    public class PotionCreatedEventArgs : PotionEventArgs {
        private Employee creator;

        public Employee Creator {
            get { return creator; }
        }

        public PotionCreatedEventArgs(Potion potion, Employee creator) : base(potion) {
            this.creator = creator;
        }
    }

    public class PotionSoldEventArgs : PotionEventArgs {
        private Employee seller;

        public Employee Seller {
            get { return seller; }
        }

        public PotionSoldEventArgs(Potion potion, Employee seller) : base(potion) {
            this.seller = seller;
        }
    }
}