using System;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

namespace Alchemy.Models
{
    [Serializable]
    public class World
    {
        Random _random;
        string[] _nameDatabase;
        Flask[] _flaskDatabase;
        Solvent[] _solventDatabase;
        Herb[] _herbDatabase;
        [SerializeField]
        int _speed;
        [SerializeField]
        float _time;
        float _timePerHour;
        [SerializeField]
        int _hour;
        [SerializeField]
        int _day;
        [SerializeField]
        Shop _shop;
        [SerializeField]
        Applicants _applicants;
        [SerializeField]
        List<Flask> _flasksForSale;

        public event EventHandler<IntEventArgs> SpeedChanged;

        public event EventHandler<IntEventArgs> HourChanged;

        public event EventHandler<IntEventArgs> DayChanged;

        public event EventHandler<EmployeeEventArgs> ApplicantReceived;

        public event EventHandler<EmployeeEventArgs> ApplicantDismissed;

        public event EventHandler<FlaskEventArgs> FlaskDisplayed;

        public event EventHandler<FlaskEventArgs> FlaskSold;

        public Random Random
        {
            get { return _random; }
        }

        public string[] NameDatabase
        { 
            get { return _nameDatabase; }
        }

        public Flask[] FlaskDatabase
        {
            get { return _flaskDatabase; }
        }

        public Solvent[] SolventDatabase
        {
            get { return _solventDatabase; }
        }

        public Herb[] HerbDatabase
        {
            get { return _herbDatabase; }
        }

        public int Speed
        {
            get { return _speed; }
            set
            { 
                if (_speed != value)
                {
                    _speed = value;
                    OnSpeedChanged(_speed);
                }
            }
        }

        public float Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    if (_time >= _timePerHour)
                    {
                        Hour++;
                        _time = 0;
                    }
                }
            }
        }

        public int Hour
        {
            get { return _hour; }
            set
            {
                if (_hour != value)
                {
                    _hour = value;
                    if (_hour >= 24)
                    {
                        Day++;
                        _hour = 0;
                    }
                    OnHourChanged(_hour);
                }
            }
        }

        public int Day
        {
            get { return _day; }
            set
            {
                if (_day != value)
                {
                    _day = value; 
                    OnDayChanged(_day);
                }
            }
        }

        public Shop Shop
        {
            get { return _shop; }
        }

        public Applicants Applicants
        {
            get { return _applicants; }
        }

        public List<Flask> FlasksForSale
        {
            get { return _flasksForSale; }
        }

        public World()
        {
            _random = new Random();
            _nameDatabase = new string[]
            {
                "Pavel",
                "Bim",
                "Olgerd",
                "Berna",
                "Herban",
                "Erdan",
                "Gilbert",
                "Ferdinand",
                "Jaroo",
                "Elynwyd",
                "Ketta",
                "Geldar",
                "Fenthick",
                "Desther",
                "Aribeth"
            };
            _flaskDatabase = new Flask[]
            {
                new Flask
                (
                    "Brittle Flask",
                    Quality.Poor,
                    5
                ),
                new Flask
                (
                    "Weak Flask",
                    Quality.Fair,
                    10
                ),
                new Flask
                (
                    "Strong Flask",
                    Quality.Good,
                    20
                ),
                new Flask
                (
                    "Alchemist's Flask",
                    Quality.Excellent,
                    50
                ),
                new Flask
                (
                    "Magical Flask",
                    Quality.Perfect,
                    100
                ),
            };
            _solventDatabase = new Solvent[]
            {

            };
			_herbDatabase = new Herb[]
			{
				new Herb
				(
					"Silverleaf",
					new Effect[]
					{
						new Effect("Restore Health"),
						new Effect("Restore Mana"),
						new Effect("Fortify Strength"),
						new Effect("Fortify Agility")
					},
					Rarity.Common,
					new Region[]
					{
						Region.Plains,
						Region.Forest
					}
				),
				new Herb
				(
					"Peacebloom",
					new Effect[]
					{
						new Effect("Restore Mana"),
						new Effect("Fortify Agility"),
						new Effect("Resist Fire"),
						new Effect("Resist Poison")
					},
					Rarity.Uncommon,
					new Region[]
					{
						Region.Forest,
						Region.Desert
					}
				),
				new Herb
				(
					"Earthroot",
					new Effect[]
					{
						new Effect("Restore Mana"),
						new Effect("Fortify Strength"),
						new Effect("Resist Frost"),
						new Effect("Resist Poison")
					},
					Rarity.Rare,
					new Region[]
					{
						Region.Desert,
						Region.Tundra
					}
				)
			};
            _speed = 1;
            _time = 0;
            _timePerHour = 1;
            _hour = 0;
            _day = 1;
            _shop = new Shop(this);
            _applicants = new Applicants();
            _flasksForSale = new List<Flask>();

            DayChanged += FindRandomApplicant;
            DayChanged += GenerateFlaskForSale;
            DayChanged += (object sender, IntEventArgs e) =>
            {
                Shop.Gold += 1000;
            };
        }

        public Flask GetFlaskPrototype(string name)
        {
            for (int i = 0; i < FlaskDatabase.Length; i++)
            {
                if (FlaskDatabase[i].Name == name)
                {
                    return FlaskDatabase[i];
                }
            }
            return null;
        }

        public Solvent GetSolventPrototype(string name)
        {
            for (int i = 0; i < SolventDatabase.Length; i++)
            {
                if (SolventDatabase[i].Name == name)
                {
                    return SolventDatabase[i];
                }
            }
            return null;
        }

        public Herb GetHerbPrototype(string name)
        {
            for (int i = 0; i < HerbDatabase.Length; i++)
            {
                if (HerbDatabase[i].Name == name)
                {
                    return HerbDatabase[i];
                }
            }
            return null;
        }

        public void ReceiveApplication(Employee applicant)
        {
            Applicants.Add(applicant);
            OnApplicantReceived(applicant);
        }

        public void DismissApplication(Employee applicant)
        {
            Applicants.Remove(applicant);
            OnApplicantDismissed(applicant);
        }

        public void DisplayFlask(Flask prototype)
        {
            bool newEntry = true;
            for (int i = 0; i < FlasksForSale.Count; i++)
            {
                if (FlasksForSale[i].Name == prototype.Name)
                {
                    FlasksForSale[i].Amount++;
                    OnFlaskDisplayed(FlasksForSale[i]);
                    newEntry = false;
                    break;
                }
            }
            if (newEntry)
            {
                var flask = (Flask)prototype.Clone();
                flask.Amount = 1;
                FlasksForSale.Add(flask);
                OnFlaskDisplayed(flask);
            }
        }

        public void SellFlask(Flask prototype)
        {
            if (!Shop.PurchaseFlask(prototype))
            {
                return;
            }
            for (int i = 0; i < FlasksForSale.Count; i++)
            {
                if (FlasksForSale[i].Name == prototype.Name)
                {
                    FlasksForSale[i].Amount--;
                    if (FlasksForSale[i].Amount < 0)
                    {
                        FlasksForSale[i].Amount = 0;
                    }
                    OnFlaskSold(FlasksForSale[i]);
                    break;
                }
            }
        }

        protected virtual void OnSpeedChanged(int value)
        {
            if (SpeedChanged != null)
            {
                SpeedChanged(this, new IntEventArgs(value));
            }
        }

        protected virtual void OnHourChanged(int value)
        {
            if (HourChanged != null)
            {
                HourChanged(this, new IntEventArgs(value));
            }
        }

        protected virtual void OnDayChanged(int value)
        {
            if (DayChanged != null)
            {
                DayChanged(this, new IntEventArgs(value));
            }
        }

        protected virtual void OnApplicantReceived(Employee employee)
        {
            if (ApplicantReceived != null)
            {
                ApplicantReceived(this, new EmployeeEventArgs(employee));
            }
        }

        protected virtual void OnApplicantDismissed(Employee employee)
        {
            if (ApplicantDismissed != null)
            {
                ApplicantDismissed(this, new EmployeeEventArgs(employee));
            }
        }

        protected virtual void OnFlaskDisplayed(Flask flask)
        {
            if (FlaskDisplayed != null)
            {
                FlaskDisplayed(this, new FlaskEventArgs(flask));
            }
        }

        protected virtual void OnFlaskSold(Flask flask)
        {
            if (FlaskSold != null)
            {
                FlaskSold(this, new FlaskEventArgs(flask));
            }
        }

        void FindRandomApplicant(object sender, IntEventArgs e)
        {
            Employee applicant = null;
            switch (Random.Next(4))
            {
                case 0:
                    applicant = new Herbalist(this, NameDatabase[Random.Next(NameDatabase.Length)], Random.Next(1, 100));
                    break;
                case 1:
                    applicant = new Guard(this, NameDatabase[Random.Next(NameDatabase.Length)], Random.Next(1, 100));
                    break;
                case 2:
                    applicant = new Apothecary(this, NameDatabase[Random.Next(NameDatabase.Length)], Random.Next(1, 100));
                    break;
                case 3:
                    applicant = new Shopkeeper(this, NameDatabase[Random.Next(NameDatabase.Length)], Random.Next(1, 100));
                    break;
                default:
                    break;
            }
            if (applicant != null)
            {
                ReceiveApplication(applicant);
            }
        }

        void GenerateFlaskForSale(object sender, IntEventArgs e)
        {
            var flask = FlaskDatabase[Random.Next(FlaskDatabase.Length)];
            DisplayFlask(flask);
        }
    }
}