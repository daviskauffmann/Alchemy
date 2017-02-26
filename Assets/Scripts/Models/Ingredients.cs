using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Ingredients
    {
        [SerializeField]
        List<Herb> herbs;

        public Ingredients()
        {
            herbs = new List<Herb>();
        }

		public event EventHandler<HerbEventArgs> HerbAdded;

		public event EventHandler<HerbEventArgs> HerbRemoved;

		public List<Herb> Herbs
		{
			get { return herbs; }
		}

		public Ingredient[] Total
		{
			get
			{
				var total = new List<Ingredient>();
				for (int i = 0; i < herbs.Count; i++)
				{
					total.Add(herbs[i]);
				}
				return total.ToArray();
			}
		}

		public void Add(Ingredient prototype)
        {
            if (prototype is Herb)
            {
                bool newEntry = true;
                for (int i = 0; i < Herbs.Count; i++)
                {
                    if (Herbs[i].Name == prototype.Name)
                    {
                        Herbs[i].Amount++;
                        OnHerbAdded(Herbs[i]);
                        newEntry = false;
                        break;
                    }
                }
                if (newEntry)
                {
                    var herb = (Herb)prototype.Clone();
                    herb.Amount = 1;
                    Herbs.Add(herb);
                    OnHerbAdded(herb);
                }
            }
        }

        public void Remove(Ingredient prototype)
        {
            if (prototype is Herb)
            {
                for (int i = 0; i < Herbs.Count; i++)
                {
                    if (Herbs[i].Name == prototype.Name)
                    {
                        Herbs[i].Amount--;
                        if (Herbs[i].Amount < 0)
                        {
                            Herbs[i].Amount = 0;
                        }
                        OnHerbRemoved(Herbs[i]);
                        break;
                    }
                }
            }
        }

        protected virtual void OnHerbAdded(Herb herb)
        {
            if (HerbAdded != null)
            {
                HerbAdded(this, new HerbEventArgs() { herb = herb });
            }
        }

        protected virtual void OnHerbRemoved(Herb herb)
        {
            if (HerbRemoved != null)
            {
                HerbRemoved(this, new HerbEventArgs() { herb = herb });
            }
        }
    }
}