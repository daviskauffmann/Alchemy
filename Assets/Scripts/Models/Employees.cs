using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Employees
    {
        [SerializeField]
        List<Herbalist> _herbalists;
        [SerializeField]
        List<Apothecary> _apothecaries;
        [SerializeField]
        List<Shopkeeper> _shopkeepers;
        [SerializeField]
        List<Guard> _guards;

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

        public Employees()
        {
            _herbalists = new List<Herbalist>();
            _apothecaries = new List<Apothecary>();
            _shopkeepers = new List<Shopkeeper>();
            _guards = new List<Guard>();

            World.Instance.DayChanged += PayEmployees;
        }

        public void Add(Employee employee)
        {
            if (employee is Herbalist)
            {
                _herbalists.Add((Herbalist)employee);
            }
            if (employee is Apothecary)
            {
                _apothecaries.Add((Apothecary)employee);
            }
            if (employee is Shopkeeper)
            {
                _shopkeepers.Add((Shopkeeper)employee);
            }
            if (employee is Guard)
            {
                _guards.Add((Guard)employee);
            }

            employee.StartWorking();
        }

        public void Remove(Employee employee)
        {
            if (employee is Herbalist)
            {
                _herbalists.Remove((Herbalist)employee);
            }
            if (employee is Apothecary)
            {
                _apothecaries.Remove((Apothecary)employee);
            }
            if (employee is Shopkeeper)
            {
                _shopkeepers.Remove((Shopkeeper)employee);
            }
            if (employee is Guard)
            {
                _guards.Remove((Guard)employee);
            }

            employee.StopWorking();
        }

        void PayEmployees(object sender, IntEventArgs e)
        {
            for (int i = 0; i < Total.Length; i++)
            {
                World.Instance.Shop.Gold -= Total[i].Salary;
            }
        }
    }
}