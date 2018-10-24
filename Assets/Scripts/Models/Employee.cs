using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public abstract class Employee {
        [SerializeField]
        protected string title;
        [SerializeField]
        protected string name;
        [SerializeField]
        protected string description;
        [SerializeField]
        protected float health;
        [SerializeField]
        protected int salary;
        [SerializeField]
        protected int daysEmployed;

        public string Title => this.title;

        public string Name => this.name;

        public string Description => this.description;

        public float Health => this.health;

        public int Salary => this.salary;

        public int DaysEmployed => this.daysEmployed;

        protected Employee(string title, string name, int salary) {
            this.title = title;
            this.name = name;
            this.salary = salary;
        }

        public virtual void StartWorking() => World.Instance.DayChanged += this.DayChanged;

        public virtual void StopWorking() => World.Instance.DayChanged -= this.DayChanged;

        private void DayChanged(object sender, IntEventArgs e) => this.daysEmployed++;
    }

    public class EmployeeEventArgs : EventArgs {
        public Employee Employee { get; }

        public EmployeeEventArgs(Employee employee) {
            this.Employee = employee;
        }
    }
}
