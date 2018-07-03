using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Effect : ICloneable {
        [SerializeField]
        private string name;
        [SerializeField]
        private int magnitude;

        public string Name {
            get { return name; }
        }

        public int Magnitude {
            get { return magnitude; }
            set { magnitude = value; }
        }

        public Effect(string name, int magnitude) {
            this.name = name;
            this.magnitude = magnitude;
        }

        public virtual object Clone() {
            var clone = (Effect)MemberwiseClone();

            return clone;
        }

        public Effect Combine(Effect other) {
            if (Name == other.Name) {
                var clone = (Effect)Clone();

                clone.Magnitude = Magnitude + other.Magnitude;

                return clone;
            }

            return null;
        }
    }
}