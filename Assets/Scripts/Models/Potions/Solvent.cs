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

		public Solvent(string name, int strength)
		{
			this.name = name;
			this.strength = strength;
		}

		public string Name
		{
			get { return name; }
		}

		public int Strength
		{
			get { return strength; }
		}

		public object Clone()
		{
			var clone = (Solvent)MemberwiseClone();
			return clone;
		}
	}
}