using System;
using UnityEngine;

namespace Alchemy.Models
{
	[Serializable]
	public class Effect : ICloneable
	{
		[SerializeField]
		string name;

		public Effect(string name)
		{
			this.name = name;
		}

		public string Name
		{
			get { return name; }
		}

		public virtual object Clone()
		{
			var clone = (Effect)MemberwiseClone();
			return clone;
		}

		public Effect Combine(Effect other)
		{
			if (Name == other.Name)
			{
				return (Effect)Clone();
			}
			return null;
		}
	}

	public class EffectEventArgs : EventArgs
	{
		public Effect effect { get; set; }
		public Ingredient ingredient { get; set; }
	}
}