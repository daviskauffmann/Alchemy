using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Solvent : ICloneable
    {
        [SerializeField]
        string name;
        [SerializeField]
        int strength;
		[SerializeField]
		int amount;

		public Solvent(string name, int strength)
        {
            this.name = name;
            this.strength = strength;
			amount = -1;
        }

		public string Name
		{
			get { return name; }
		}

		public int Strength
		{
			get { return strength; }
		}

		public int Amount
		{
			get { return amount; }
			set { amount = value; }
		}

		public object Clone()
        {
            var clone = (Solvent)MemberwiseClone();
            clone.Amount = -1;
            return clone;
        }
    }
}