using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public abstract class Ingredient : ICloneable
    {
        [SerializeField]protected string _name;
        [SerializeField]protected int _amount;
        [SerializeField]protected Effect[] _effects;

        public string Name
        {
            get { return _name; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public Effect[] Effects
        {
            get { return _effects; }
        }

        protected Ingredient(string name, Effect[] effects)
        {
            _name = name;
            _amount = -1;
            _effects = effects;
        }

        public virtual object Clone()
        {
            var ingredient = (Ingredient)MemberwiseClone();
            ingredient.Amount = -1;
            ingredient._effects = new Effect[_effects.Length];
            for (int i = 0; i < _effects.Length; i++)
            {
                ingredient._effects[i] = (Effect)_effects[i].Clone();
            }
            return ingredient;
        }
    }
}