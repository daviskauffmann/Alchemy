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

        public string Name {
            get { return name; }
        }

        public Quality Quality {
            get { return quality; }
        }

        public float Value {
            get { return value; }
        }

        public Flask(string name, Quality quality, float value) {
            this.name = name;
            this.quality = quality;
            this.value = value;
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
        private Flask flask;

        public Flask Flask {
            get { return flask; }
        }

        public FlaskEventArgs(Flask flask) {
            this.flask = flask;
        }
    }
}