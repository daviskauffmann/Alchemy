using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public abstract class Employee
    {
        [SerializeField]
        protected string _title;
        [SerializeField]
        protected string _name;
        [SerializeField]
        protected string _description;
        [SerializeField]
        protected float _health;
        [SerializeField]
        protected int _salary;
        [SerializeField]
        protected int _daysEmployed;

        public string Title
        {
            get { return _title; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public float Health
        {
            get { return _health; }
        }

        public int Salary
        {
            get { return _salary; }
        }

        public int DaysEmployed
        {
            get { return _daysEmployed; }
        }

        protected Employee(string title, string name, int salary)
        {
            _title = title;
            _name = name;
            _salary = salary;
        }

        public virtual void StartWorking()
        {
            World.Instance.DayChanged += IncrementDaysEmployed;
        }

        public virtual void StopWorking()
        {
            World.Instance.DayChanged -= IncrementDaysEmployed;
        }

        protected void IncrementDaysEmployed(object sender, IntEventArgs e)
        {
            _daysEmployed++;
        }
    }
}