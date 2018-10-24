using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Solvent : ICloneable {
        [SerializeField]
        private string name;
        [SerializeField]
        private int strength;

        public string Name => this.name;

        public int Strength => this.strength;

        public Solvent(string name, int strength) {
            this.name = name;
            this.strength = strength;
        }

        public object Clone() {
            var clone = (Solvent)this.MemberwiseClone();

            return clone;
        }
    }
}
