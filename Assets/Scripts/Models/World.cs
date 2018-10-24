using System;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

namespace Alchemy.Models {
    [Serializable]
    public class World {
        [SerializeField]
        private int speed;
        [SerializeField]
        private float time;
        private float timePerHour;
        [SerializeField]
        private int hour;
        [SerializeField]
        private int day;
        [SerializeField]
        private Shop shop;
        [SerializeField]
        private List<Apothecary> apothecaries;
        [SerializeField]
        private List<Guard> guards;
        [SerializeField]
        private List<Herbalist> herbalists;
        [SerializeField]
        private List<Shopkeeper> shopkeepers;
        [SerializeField]
        private List<Flask> flasks;

        public static World Instance { get; private set; }

        public Random Random { get; }

        public string[] NameDatabase { get; }

        public Flask[] FlaskDatabase { get; }

        public Solvent[] SolventDatabase { get; }

        public Herb[] HerbDatabase { get; }

        public int Speed {
            get { return this.speed; }
            set {
                if (this.speed != value) {
                    this.speed = value;

                    this.OnSpeedChanged(this.speed);
                }
            }
        }

        public float Time {
            get { return this.time; }
            set {
                if (this.time != value) {
                    this.time = value;

                    if (this.time >= this.timePerHour) {
                        this.time = 0;

                        this.Hour++;
                    }
                }
            }
        }

        public int Hour {
            get { return this.hour; }
            set {
                if (this.hour != value) {
                    this.hour = value;

                    if (this.hour >= 24) {
                        this.hour = 0;

                        this.Day++;
                    }

                    this.OnHourChanged(this.hour);
                }
            }
        }

        public int Day {
            get { return this.day; }
            set {
                if (this.day != value) {
                    this.day = value;

                    this.OnDayChanged(this.day);
                }
            }
        }

        public Shop Shop => this.shop;

        public List<Apothecary> Apothecaries => this.apothecaries;

        public List<Guard> Guards => this.guards;

        public List<Herbalist> Herbalists => this.herbalists;

        public List<Shopkeeper> Shopkeepers => this.shopkeepers;

        public Employee[] Applicants {
            get {
                var applicants = new List<Employee>();

                foreach (var apothecary in this.Apothecaries) {
                    applicants.Add(apothecary);
                }

                foreach (var guard in this.Guards) {
                    applicants.Add(guard);
                }

                foreach (var herbalist in this.Herbalists) {
                    applicants.Add(herbalist);
                }

                foreach (var shopkeeper in this.Shopkeepers) {
                    applicants.Add(shopkeeper);
                }

                return applicants.ToArray();
            }
        }

        public List<Flask> Flasks => this.flasks;

        public event EventHandler<IntEventArgs> SpeedChanged;

        public event EventHandler<IntEventArgs> HourChanged;

        public event EventHandler<IntEventArgs> DayChanged;

        public event EventHandler<EmployeeEventArgs> ApplicantReceived;

        public event EventHandler<EmployeeEventArgs> ApplicantDismissed;

        public event EventHandler<IntEventArgs> ApplicantCountChanged;

        public event EventHandler<FlaskEventArgs> FlaskDisplayed;

        public event EventHandler<FlaskEventArgs> FlaskSold;

        public World() {
            Instance = this;
            this.Random = new Random();
            this.NameDatabase = new string[] {
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
            this.FlaskDatabase = new Flask[] {
                new Flask("Brittle Flask", Quality.Poor, 5),
                new Flask("Weak Flask", Quality.Fair, 10),
                new Flask("Strong Flask", Quality.Good, 20),
                new Flask("Alchemist's Flask", Quality.Excellent, 50),
                new Flask("Magical Flask", Quality.Perfect, 100),
            };
            this.SolventDatabase = new Solvent[] {

            };
            this.HerbDatabase = new Herb[] {
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
            this.speed = 1;
            this.time = 0;
            this.timePerHour = 1;
            this.hour = 0;
            this.day = 1;
            this.shop = new Shop();
            this.apothecaries = new List<Apothecary>();
            this.guards = new List<Guard>();
            this.herbalists = new List<Herbalist>();
            this.shopkeepers = new List<Shopkeeper>();
            this.flasks = new List<Flask>();
        }

        public void Start() {
            this.Shop.Start();

            DayChanged += (sender, e) => {
                Employee applicant = null;

                var name = this.NameDatabase[this.Random.Next(this.NameDatabase.Length)];
                var salary = this.Random.Next(1, 100);

                switch (this.Random.Next(4)) {
                    case 0: applicant = new Herbalist(name, salary); break;
                    case 1: applicant = new Guard(name, salary); break;
                    case 2: applicant = new Apothecary(name, salary); break;
                    case 3: applicant = new Shopkeeper(name, salary); break;
                }

                if (applicant != null) {
                    this.ReceiveApplication(applicant);
                }
            };

            DayChanged += (sender, e) => {
                var flask = (Flask)this.FlaskDatabase[this.Random.Next(this.FlaskDatabase.Length)].Clone();

                this.DisplayFlask(flask);
            };

            DayChanged += (sender, e) => {
                this.Shop.Gold += 1000;
            };

        }

        public void ReceiveApplication(Employee applicant) {
            if (applicant is Apothecary) {
                this.Apothecaries.Add((Apothecary)applicant);
            } else if (applicant is Guard) {
                this.Guards.Add((Guard)applicant);
            } else if (applicant is Herbalist) {
                this.Herbalists.Add((Herbalist)applicant);
            } else if (applicant is Shopkeeper) {
                this.Shopkeepers.Add((Shopkeeper)applicant);
            }

            this.OnApplicantCountChanged(this.Applicants.Length);

            this.OnApplicantReceived(applicant);
        }

        public void DismissApplication(Employee applicant) {
            if (applicant is Apothecary) {
                this.Apothecaries.Remove((Apothecary)applicant);
            } else if (applicant is Guard) {
                this.Guards.Remove((Guard)applicant);
            } else if (applicant is Herbalist) {
                this.Herbalists.Remove((Herbalist)applicant);
            } else if (applicant is Shopkeeper) {
                this.Shopkeepers.Remove((Shopkeeper)applicant);
            }

            this.OnApplicantCountChanged(this.Applicants.Length);

            this.OnApplicantDismissed(applicant);
        }

        public void DisplayFlask(Flask flask) {
            this.Flasks.Add(flask);

            this.OnFlaskDisplayed(flask);
        }

        public void SellFlask(Flask flask) {
            if (!this.Shop.PurchaseFlask(flask)) {
                return;
            }

            this.Flasks.Remove(flask);

            this.OnFlaskSold(flask);
        }

        private void OnSpeedChanged(int value) => SpeedChanged?.Invoke(this, new IntEventArgs(value));

        private void OnHourChanged(int value) => HourChanged?.Invoke(this, new IntEventArgs(value));

        private void OnDayChanged(int value) => DayChanged?.Invoke(this, new IntEventArgs(value));

        private void OnApplicantReceived(Employee applicant) => ApplicantReceived?.Invoke(this, new EmployeeEventArgs(applicant));

        private void OnApplicantDismissed(Employee applicant) => ApplicantDismissed?.Invoke(this, new EmployeeEventArgs(applicant));

        private void OnApplicantCountChanged(int value) => ApplicantCountChanged?.Invoke(this, new IntEventArgs(value));

        private void OnFlaskDisplayed(Flask flask) => FlaskDisplayed?.Invoke(this, new FlaskEventArgs(flask));

        private void OnFlaskSold(Flask flask) => FlaskSold?.Invoke(this, new FlaskEventArgs(flask));
    }

    public class StringEventArgs : EventArgs {
        public string Value { get; }

        public StringEventArgs(string value) {
            this.Value = value;
        }
    }

    public class FloatEventArgs : EventArgs {
        public float Value { get; }

        public FloatEventArgs(float value) {
            this.Value = value;
        }
    }

    public class IntEventArgs : EventArgs {
        public int Value { get; }

        public IntEventArgs(int value) {
            this.Value = value;
        }
    }
}
