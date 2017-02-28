using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public abstract class Employee
    {
		protected World world;
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

        protected Employee(World world, string title, string name, int salary)
        {
			this.world = world;
            this.title = title;
            this.name = name;
            this.salary = salary;
        }

		public World World
		{
			get { return world; }
			set { world = value; }
		}

		public string Title
		{
			get { return title; }
		}

		public string Name
		{
			get { return name; }
		}

		public string Description
		{
			get { return description; }
		}

		public float Health
		{
			get { return health; }
		}

		public int Salary
		{
			get { return salary; }
		}

		public int DaysEmployed
		{
			get { return daysEmployed; }
		}

		public virtual void StartWorking()
        {
            world.DayChanged += DayChanged;
        }

        public virtual void StopWorking()
        {
            world.DayChanged -= DayChanged;
        }

        void DayChanged(object sender, IntEventArgs e)
        {
            daysEmployed++;
        }
    }

	public class EmployeeEventArgs : EventArgs
	{
		public Employee employee { get; set; }
	}
}