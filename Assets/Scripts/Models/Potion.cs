using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Potion {
        [SerializeField]
        private string name;
        [SerializeField]
        private Flask flask;
        [SerializeField]
        private Solvent solvent;
        [SerializeField]
        private Herb[] herbs;
        [SerializeField]
        private Effect[] effects;
        [SerializeField]
        private float value;

        public string Name {
            get { return name; }
        }

        public Flask Flask {
            get { return flask; }
        }

        public Solvent Solvent {
            get { return solvent; }
        }

        public Herb[] Herbs {
            get { return herbs; }
        }

        public int IngredientCount {
            get { return herbs.Length; }
        }

        public Effect[] Effects {
            get { return effects; }
        }

        public float Value {
            get { return value; }
        }

        public Potion(Flask flask, Solvent solvent, Ingredient[] ingredients) {
            var herbs = new List<Herb>();
            // var otherTypes = new List<OtherType>();

            foreach (var ingredient in ingredients) {
                if (ingredient is Herb) {
                    herbs.Add((Herb)ingredient.Clone());
                }
                // if (ingredients[i] is OtherType)
                // {
                //     otherTypes.Add((OtherType)ingredients[i].Clone());
                // }
            }

            //this.otherTypes = otherTypes.ToArray();

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
            this.flask = flask;
            this.solvent = /*(Solvent)*/solvent/*.Clone()*/;
            this.herbs = herbs.ToArray();
            this.effects = effects.ToArray();
            this.value = flask.Value * ingredients.Length;
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