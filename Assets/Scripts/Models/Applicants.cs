using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Applicants
    {
        [SerializeField]List<Herbalist> _herbalists;
        [SerializeField]List<Apothecary> _apothecaries;
        [SerializeField]List<Shopkeeper> _shopkeepers;
        [SerializeField]List<Guard> _guards;

        public event EventHandler<IntEventArgs> CountChanged;

        public List<Herbalist> Herbalists
        {
            get { return _herbalists; }
        }

        public List<Apothecary> Apothecaries
        {
            get { return _apothecaries; }
        }

        public List<Shopkeeper> Shopkeepers
        {
            get { return _shopkeepers; }
        }

        public List<Guard> Guards
        {
            get { return _guards; }
        }

        public Employee[] Total
        {
            get
            {
                var total = new List<Employee>();

                for (int i = 0; i < Herbalists.Count; i++)
                {
                    total.Add(Herbalists[i]);
                }

                for (int i = 0; i < Apothecaries.Count; i++)
                {
                    total.Add(Apothecaries[i]);
                }

                for (int i = 0; i < Shopkeepers.Count; i++)
                {
                    total.Add(Shopkeepers[i]);
                }

                for (int i = 0; i < Guards.Count; i++)
                {
                    total.Add(Guards[i]);
                }

                return total.ToArray();
            }
        }

        public Applicants()
        {
            _herbalists = new List<Herbalist>();
            _apothecaries = new List<Apothecary>();
            _shopkeepers = new List<Shopkeeper>();
            _guards = new List<Guard>();
        }

        public void Add(Employee applicant)
        {
            if (applicant is Herbalist)
            {
                _herbalists.Add((Herbalist)applicant);
            }

            if (applicant is Apothecary)
            {
                _apothecaries.Add((Apothecary)applicant);
            }

            if (applicant is Shopkeeper)
            {
                _shopkeepers.Add((Shopkeeper)applicant);
            }

            if (applicant is Guard)
            {
                _guards.Add((Guard)applicant);
            }

            OnCountChanged(Total.Length);
        }

        public void Remove(Employee applicant)
        {
            if (applicant is Herbalist)
            {
                _herbalists.Remove((Herbalist)applicant);
            }

            if (applicant is Apothecary)
            {
                _apothecaries.Remove((Apothecary)applicant);
            }

            if (applicant is Shopkeeper)
            {
                _shopkeepers.Remove((Shopkeeper)applicant);
            }

            if (applicant is Guard)
            {
                _guards.Remove((Guard)applicant);
            }

            OnCountChanged(Total.Length);
        }

        protected virtual void OnCountChanged(int value)
        {
            if (CountChanged != null)
            {
                CountChanged(this, new IntEventArgs(value));  
            }
        }
    }
}