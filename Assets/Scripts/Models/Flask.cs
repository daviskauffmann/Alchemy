using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Flask : ICloneable
    {
        [SerializeField]string _name;
        [SerializeField]int _amount;
        [SerializeField]Quality _quality;
        [SerializeField]float _value;

        public string Name
        {
            get { return _name; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public Quality Quality
        {
            get { return _quality; }
        }

        public float Value
        {
            get { return _value; }
        }

        public Flask(string name, Quality quality, float value)
        {
            _name = name;
            _amount = -1;
            _quality = quality;
            _value = value;
        }

        public object Clone()
        {
            var clone = (Flask)MemberwiseClone();
            clone.Amount = -1;

            return clone;
        }
    }
}