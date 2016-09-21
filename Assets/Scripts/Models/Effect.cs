using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Effect : ICloneable
    {
        [SerializeField]string _name;
        [SerializeField]bool _discovered;

        public string Name
        {
            get { return _name; }
        }

        public bool Discovered
        {
            get { return _discovered; }
            set { _discovered = value; }
        }

        public Effect(string name)
        {
            _name = name;
            _discovered = false;
        }

        public virtual object Clone()
        {
            var clone = (Effect)MemberwiseClone();
            clone._discovered = false;

            return clone;
        }

        public Effect Combine(Effect other)
        {
            if (Name == other.Name)
            {
                Discovered = true;
                other.Discovered = true;

                return this;
            }

            return null;
        }
    }
}