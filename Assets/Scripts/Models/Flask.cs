using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Flask : ICloneable {
        [SerializeField]
        private string name;
        [SerializeField]
        private Quality quality;
        [SerializeField]
        private float value;

        public string Name => this.name;

        public Quality Quality => this.quality;

        public float Value => this.value;

        public Flask(string name, Quality quality, float value) {
            this.name = name;
            this.quality = quality;
            this.value = value;
        }

        public object Clone() {
            var clone = (Flask)this.MemberwiseClone();

            return clone;
        }
    }

    public enum Quality {
        Poor,
        Fair,
        Good,
        Excellent,
        Perfect
    }

    public class FlaskEventArgs : EventArgs {
        public Flask Flask { get; }

        public FlaskEventArgs(Flask flask) {
            this.Flask = flask;
        }
    }
}
