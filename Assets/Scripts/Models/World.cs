using System;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

namespace Alchemy.Models {
    [Serializable]
    public class World {
        static World instance;

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

        public event EventHandler<IntEventArgs> SpeedChanged;

        public event EventHandler<IntEventArgs> HourChanged;

        public event EventHandler<IntEventArgs> DayChanged;

        public event EventHandler<EmployeeEventArgs> ApplicantReceived;

        public event EventHandler<EmployeeEventArgs> ApplicantDismissed;

        public event EventHandler<FlaskEventArgs> FlaskDisplayed;

        public event EventHandler<FlaskEventArgs> FlaskSold;

        public World() {
            instance = this;
            random = new Random();
            nameDatabase = new string[] {
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
            flaskDatabase = new Flask[] {
                new Flask("Brittle Flask", Quality.Poor, 5),
                new Flask("Weak Flask", Quality.Fair, 10),
                new Flask("Strong Flask", Quality.Good, 20),
                new Flask("Alchemist's Flask", Quality.Excellent, 50),
                new Flask("Magical Flask", Quality.Perfect, 100),
            };
            solventDatabase = new Solvent[] {

            };
            herbDatabase = new Herb[] {
                new Herb(
                    "Silverleaf",
                    new Effect[] {
                        new Effect("Restore Health", 10),
                        new Effect("Restore Mana", 10),
                        new Effect("Fortify Strength", 10),
                        new Effect("Fortify Agility", 10)
                    },
                    Rarity.Common,
                    new Region[] {
                        Region.Plains,
                        Region.Forest
                    }),
                new Herb(
                    "Peacebloom",
                    new Effect[] {
                        new Effect("Restore Mana", 10),
                        new Effect("Fortify Agility", 10),
                        new Effect("Resist Fire", 10),
                        new Effect("Resist Poison", 10)
                    },
                    Rarity.Uncommon,
                    new Region[] {
                        Region.Forest,
                        Region.Desert
                    }),
                new Herb(
                    "Earthroot",
                    new Effect[] {
                        new Effect("Restore Mana", 10),
                        new Effect("Fortify Strength", 10),
                        new Effect("Resist Frost", 10),
                        new Effect("Resist Poison", 10)
                    },
                    Rarity.Rare,
                    new Region[] {
                        Region.Desert,
                        Region.Tundra
                    })
            };
            speed = 1;
            time = 0;
            timePerHour = 1;
            hour = 0;
            day = 1;
            shop = new Shop();
            applicants = new Applicants();
            flasksForSale = new List<Flask>();

            DayChanged += (sender, e) => {
                Employee applicant = null;

                switch (Random.Next(4)) {
                    case 0:
                        applicant = new Herbalist(NameDatabase[Random.Next(NameDatabase.Length)], Random.Next(1, 100));
                        break;
                    case 1:
                        applicant = new Guard(NameDatabase[Random.Next(NameDatabase.Length)], Random.Next(1, 100));
                        break;
                    case 2:
                        applicant = new Apothecary(NameDatabase[Random.Next(NameDatabase.Length)], Random.Next(1, 100));
                        break;
                    case 3:
                        applicant = new Shopkeeper(NameDatabase[Random.Next(NameDatabase.Length)], Random.Next(1, 100));
                        break;
                    default:
                        break;
                }

                if (applicant != null) {
                    ReceiveApplication(applicant);
                }
            };
            DayChanged += (sender, e) => {
                var flask = (Flask)FlaskDatabase[Random.Next(FlaskDatabase.Length)].Clone();
                DisplayFlask(flask);
            };
            DayChanged += (sender, e) => {
                Shop.Gold += 1000;
            };
        }

        public static World Instance {
            get { return instance; }
        }

        public Random Random {
            get { return random; }
        }

        public string[] NameDatabase {
            get { return nameDatabase; }
        }

        public Flask[] FlaskDatabase {
            get { return flaskDatabase; }
        }

        public Solvent[] SolventDatabase {
            get { return solventDatabase; }
        }

        public Herb[] HerbDatabase {
            get { return herbDatabase; }
        }

        public int Speed {
            get { return speed; }
            set {
                if (speed != value) {
                    speed = value;

                    OnSpeedChanged(speed);
                }
            }
        }

        public float Time {
            get { return time; }
            set {
                if (time != value) {
                    time = value;

                    if (time >= timePerHour) {
                        time = 0;

                        Hour++;
                    }
                }
            }
        }

        public int Hour {
            get { return hour; }
            set {
                if (hour != value) {
                    hour = value;

                    if (hour >= 24) {
                        hour = 0;

                        Day++;
                    }

                    OnHourChanged(hour);
                }
            }
        }

        public int Day {
            get { return day; }
            set {
                if (day != value) {
                    day = value;

                    OnDayChanged(day);
                }
            }
        }

        public Shop Shop {
            get { return shop; }
        }

        public Applicants Applicants {
            get { return applicants; }
        }

        public List<Flask> FlasksForSale {
            get { return flasksForSale; }
        }

        public void ReceiveApplication(Employee applicant) {
            Applicants.Add(applicant);

            OnApplicantReceived(applicant);
        }

        public void DismissApplication(Employee applicant) {
            Applicants.Remove(applicant);

            OnApplicantDismissed(applicant);
        }

        public void DisplayFlask(Flask flask) {
            FlasksForSale.Add(flask);

            OnFlaskDisplayed(flask);
        }

        public void SellFlask(Flask flask) {
            if (!Shop.PurchaseFlask(flask)) {
                return;
            }

            FlasksForSale.Remove(flask);

            OnFlaskSold(flask);
        }

        protected virtual void OnSpeedChanged(int value) {
            if (SpeedChanged != null) {
                SpeedChanged(this, new IntEventArgs() { value = value });
            }
        }

        protected virtual void OnHourChanged(int value) {
            if (HourChanged != null) {
                HourChanged(this, new IntEventArgs() { value = value });
            }
        }

        protected virtual void OnDayChanged(int value) {
            if (DayChanged != null) {
                DayChanged(this, new IntEventArgs() { value = value });
            }
        }

        protected virtual void OnApplicantReceived(Employee applicant) {
            if (ApplicantReceived != null) {
                ApplicantReceived(this, new EmployeeEventArgs() { employee = applicant });
            }
        }

        protected virtual void OnApplicantDismissed(Employee applicant) {
            if (ApplicantDismissed != null) {
                ApplicantDismissed(this, new EmployeeEventArgs() { employee = applicant });
            }
        }

        protected virtual void OnFlaskDisplayed(Flask flask) {
            if (FlaskDisplayed != null) {
                FlaskDisplayed(this, new FlaskEventArgs() { flask = flask });
            }
        }

        protected virtual void OnFlaskSold(Flask flask) {
            if (FlaskSold != null) {
                FlaskSold(this, new FlaskEventArgs() { flask = flask });
            }
        }
    }

    public class StringEventArgs : EventArgs {
        public string value { get; set; }
    }

    public class FloatEventArgs : EventArgs {
        public float value { get; set; }
    }

    public class IntEventArgs : EventArgs {
        public int value { get; set; }
    }
}