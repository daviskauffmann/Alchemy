using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Potion
    {
        [SerializeField]
        string _name;
        [SerializeField]
        Flask _flask;
        [SerializeField]
        Solvent _solvent;
        [SerializeField]
        Herb[] _herbs;
        [SerializeField]
        Effect[] _effects;
        [SerializeField]
        float _value;

        public string Name
        {
            get { return _name; }
        }

        public Flask Flask
        {
            get { return _flask; }
        }

        public Solvent Solvent
        {
            get { return _solvent; }
        }

        public Herb[] Herbs
        {
            get { return _herbs; }
        }

        public int IngredientCount
        {
            get { return _herbs.Length; }
        }

        public Effect[] Effects
        {
            get { return _effects; }
        }

        public float Value
        {
            get { return _value; }
        }

        public Potion(Flask flask, Solvent solvent, Ingredient[] ingredients)
        {
            _name = "";
            _value = 0;
            _flask = (Flask)flask.Clone();
            _solvent = /*(Solvent)*/solvent/*.Clone()*/;
            var herbs = new List<Herb>();
            //var otherTypes = new List<OtherType>();
            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i] is Herb)
                {
                    herbs.Add((Herb)ingredients[i].Clone());
                }
                /*
                if (ingredients[i] is OtherType)
                {
                    otherTypes.Add((OtherType)ingredients[i].Clone());
                }
                */
            }
            _herbs = herbs.ToArray();
            //_otherTypes = otherTypes.ToArray();

            var effectsToCheck = new Queue<Effect>();
            for (int i = 0; i < ingredients.Length; i++)
            {
                for (int j = 0; j < ingredients[i].Effects.Length; j++)
                {
                    effectsToCheck.Enqueue(ingredients[i].Effects[j]);
                }
            }

            var effects = new List<Effect>();
            while (effectsToCheck.Count > 0)
            {
                var currentEffect = effectsToCheck.Dequeue();
                foreach (var effect in effectsToCheck)
                {
                    var combinedEffect = currentEffect.Combine(effect);
                    if (combinedEffect != null)
                    {
                        effect.Discovered = true;
                        currentEffect.Discovered = true;
                        effects.Add(combinedEffect);
                    }
                }
            }
            _effects = effects.ToArray();

            var prefix = Flask.Quality.ToString();
            var name = "Potion of ";
            for (int i = 0; i < Effects.Length; i++)
            {
                name += Effects[i].Name;
                if (i < Effects.Length - 1)
                {
                    name += ", ";
                }
            }
            _name = prefix + " " + name;

            _value = Flask.Value * IngredientCount;
        }
    }
}