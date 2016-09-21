using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Herb : Ingredient
    {
        [SerializeField]Rarity _rarity;

        public Rarity Rarity
        {
            get { return _rarity; }
        }

        public Herb(string name, Effect[] effects, Rarity rarity)
            : base(name, effects)
        {
            _rarity = rarity;
        }
    }
}