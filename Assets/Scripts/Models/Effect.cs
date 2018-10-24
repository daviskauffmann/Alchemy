using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Effect : ICloneable {
        [SerializeField]
        private string name;
        [SerializeField]
        private int magnitude;

        public string Name => this.name;

        public int Magnitude {
            get { return this.magnitude; }
            set { this.magnitude = value; }
        }

        public Effect(string name, int magnitude) {
            this.name = name;
            this.magnitude = magnitude;
        }

        public virtual object Clone() {
            var clone = (Effect)this.MemberwiseClone();

            return clone;
        }

        public Effect Combine(Effect other) {
            if (this.Name == other.Name) {
                var clone = (Effect)this.Clone();

                clone.Magnitude = this.Magnitude + other.Magnitude;

                return clone;
            }

            return null;
        }
    }
}
