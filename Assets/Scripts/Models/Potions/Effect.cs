using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Effect : ICloneable
    {
        [SerializeField]
        string name;
        [SerializeField]
        bool discovered;

        public Effect(string name)
        {
            this.name = name;
            discovered = false;
        }

		public string Name
		{
			get { return name; }
		}

		public bool Discovered
		{
			get { return discovered; }
			set { discovered = value; }
		}

		public virtual object Clone()
        {
            var clone = (Effect)MemberwiseClone();
            clone.discovered = false;
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