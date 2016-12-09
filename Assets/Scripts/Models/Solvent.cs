using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Solvent : ICloneable
    {
        [SerializeField]
        string _name;
        [SerializeField]
        int _amount;
        [SerializeField]
        int _strength;

        public string Name
        {
            get { return _name; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int Strength
        {
            get { return _strength; }
        }

        public Solvent(string name, int strength)
        {
            _name = name;
            _strength = strength;
        }

        public object Clone()
        {
            var clone = (Solvent)MemberwiseClone();
            clone.Amount = -1;
            return clone;
        }
    }
}