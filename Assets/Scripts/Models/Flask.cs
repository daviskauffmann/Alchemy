using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Flask : ICloneable {
        [SerializeField]
        string name;
        [SerializeField]
        Quality quality;
        [SerializeField]
        float value;

        public Flask(string name, Quality quality, float value) {
            this.name = name;
            this.quality = quality;
            this.value = value;
        }

        public string Name {
            get { return name; }
        }

        public Quality Quality {
            get { return quality; }
        }

        public float Value {
            get { return value; }
        }

        public object Clone() {
            var clone = (Flask)MemberwiseClone();

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
        public Flask flask { get; set; }
    }
}