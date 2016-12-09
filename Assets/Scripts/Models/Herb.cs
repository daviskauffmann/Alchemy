using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Herb : Ingredient
    {
        [SerializeField]
        Rarity _rarity;
        [SerializeField]
        Region[] _regions;

        public Rarity Rarity
        {
            get { return _rarity; }
        }

        public Region[] Regions
        {
            get { return _regions; }
        }

        public Herb(string name, Effect[] effects, Rarity rarity, Region[] regions)
            : base(name, effects)
        {
            _rarity = rarity;
            _regions = regions;
        }
    }
}