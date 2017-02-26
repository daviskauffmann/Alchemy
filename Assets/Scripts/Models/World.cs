using System;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

namespace Alchemy.Models
{
    [Serializable]
    public class World
    {
        Random random;
        string[] nameDatabase;
        Flask[] flaskDatabase;
        Solvent[] solventDatabase;
        Herb[] herbDatabase;
        [SerializeField]
        int speed;
        [SerializeField]
        float time;
        float timePerHour;
        [SerializeField]
        int hour;
        [SerializeField]
        int day;
        [SerializeField]
        Shop shop;
        [SerializeField]
        Applicants applicants;
		[SerializeField]
		List<Flask> flasksForSale;

        public World()
        {
            random = new Random();
            nameDatabase = new string[]
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
            flaskDatabase = new Flask[]
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
            solventDatabase = new Solvent[]
            {

            };
			herbDatabase = new Herb[]
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
            speed = 1;
            time = 0;
            timePerHour = 1;
            hour = 0;
            day = 1;
            shop = new Shop(this);
            applicants = new Applicants(this);
            flasksForSale = new List<Flask>();

            DayChanged += FindRandomApplicant;
            DayChanged += GenerateFlaskForSale;
            DayChanged += (object sender, IntEventArgs e) =>
            {
                Shop.Gold += 1000;
            };
        }

		public event EventHandler<IntEventArgs> SpeedChanged;

		public event EventHandler<IntEventArgs> HourChanged;

		public event EventHandler<IntEventArgs> DayChanged;

		public event EventHandler<EmployeeEventArgs> ApplicantReceived;

		public event EventHandler<EmployeeEventArgs> ApplicantDismissed;

		public event EventHandler<FlaskEventArgs> FlaskDisplayed;

		public event EventHandler<FlaskEventArgs> FlaskSold;

		public Random Random
		{
			get { return random; }
		}

		public string[] NameDatabase
		{
			get { return nameDatabase; }
		}

		public Flask[] FlaskDatabase
		{
			get { return flaskDatabase; }
		}

		public Solvent[] SolventDatabase
		{
			get { return solventDatabase; }
		}

		public Herb[] HerbDatabase
		{
			get { return herbDatabase; }
		}

		public int Speed
		{
			get { return speed; }
			set
			{
				if (speed != value)
				{
					speed = value;
					OnSpeedChanged(speed);
				}
			}
		}

		public float Time
		{
			get { return time; }
			set
			{
				if (time != value)
				{
					time = value;
					if (time >= timePerHour)
					{
						Hour++;
						time = 0;
					}
				}
			}
		}

		public int Hour
		{
			get { return hour; }
			set
			{
				if (hour != value)
				{
					hour = value;
					if (hour >= 24)
					{
						Day++;
						hour = 0;
					}
					OnHourChanged(hour);
				}
			}
		}

		public int Day
		{
			get { return day; }
			set
			{
				if (day != value)
				{
					day = value;
					OnDayChanged(day);
				}
			}
		}

		public Shop Shop
		{
			get { return shop; }
		}

		public Applicants Applicants
		{
			get { return applicants; }
		}

		public List<Flask> FlasksForSale
		{
			get { return flasksForSale; }
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

		void GenerateFlaskForSale(object sender, IntEventArgs e)
		{
			var flask = FlaskDatabase[Random.Next(FlaskDatabase.Length)];
			DisplayFlask(flask);
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
                SpeedChanged(this, new IntEventArgs() { value = value });
            }
        }

        protected virtual void OnHourChanged(int value)
        {
            if (HourChanged != null)
            {
                HourChanged(this, new IntEventArgs() { value = value });
            }
        }

        protected virtual void OnDayChanged(int value)
        {
            if (DayChanged != null)
            {
                DayChanged(this, new IntEventArgs() { value = value });
            }
        }

        protected virtual void OnApplicantReceived(Employee applicant)
        {
            if (ApplicantReceived != null)
            {
                ApplicantReceived(this, new EmployeeEventArgs() { employee = applicant });
            }
        }

        protected virtual void OnApplicantDismissed(Employee applicant)
        {
            if (ApplicantDismissed != null)
            {
                ApplicantDismissed(this, new EmployeeEventArgs() { employee = applicant });
            }
        }

        protected virtual void OnFlaskDisplayed(Flask flask)
        {
            if (FlaskDisplayed != null)
            {
                FlaskDisplayed(this, new FlaskEventArgs() { flask = flask });
            }
        }

        protected virtual void OnFlaskSold(Flask flask)
        {
            if (FlaskSold != null)
            {
                FlaskSold(this, new FlaskEventArgs() { flask = flask });
            }
        }
    }
}