using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Flask : ICloneable
    {
        [SerializeField]
        string name;
        [SerializeField]
        Quality quality;
        [SerializeField]
        float value;
		[SerializeField]
		int amount;

		public Flask(string name, Quality quality, float value)
        {
            this.name = name;
            this.quality = quality;
            this.value = value;
			amount = -1;
		}

		public string Name
		{
			get { return name; }
		}

		public Quality Quality
		{
			get { return quality; }
		}

		public float Value
		{
			get { return value; }
		}

		public int Amount
		{
			get { return amount; }
			set { amount = value; }
		}

		public object Clone()
        {
            var clone = (Flask)MemberwiseClone();
            clone.Amount = -1;
            return clone;
        }
    }

	public enum Quality
	{
		Poor,
		Fair,
		Good,
		Excellent,
		Perfect
	}

	public class FlaskEventArgs : EventArgs
	{
		public Flask flask;
	}
}