using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Shop {
        [SerializeField]
        private float gold;
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
        [SerializeField]
        private List<Solvent> solvents;
        [SerializeField]
        private List<Herb> herbs;
        [SerializeField]
        private List<Potion> potionPrototypes;
        [SerializeField]
        private List<Potion> potionsForSale;

        public float Gold {
            get { return this.gold; }
            set {
                if (this.gold != value) {
                    this.gold = value;

                    this.OnGoldChanged(this.gold);
                }
            }
        }

        public List<Apothecary> Apothecaries => this.apothecaries;

        public List<Guard> Guards => this.guards;

        public List<Herbalist> Herbalists => this.herbalists;

        public List<Shopkeeper> Shopkeepers => this.shopkeepers;

        public Employee[] Employees {
            get {
                var employees = new List<Employee>();

                foreach (var apothecary in this.Apothecaries) {
                    employees.Add(apothecary);
                }

                foreach (var guard in this.Guards) {
                    employees.Add(guard);
                }

                foreach (var herbalist in this.Herbalists) {
                    employees.Add(herbalist);
                }

                foreach (var shopkeeper in this.Shopkeepers) {
                    employees.Add(shopkeeper);
                }

                return employees.ToArray();
            }
        }

        public List<Flask> Flasks => this.flasks;

        public List<Solvent> Solvents => this.solvents;

        public List<Herb> Herbs => this.herbs;

        public Ingredient[] Ingredients {
            get {
                var ingredients = new List<Ingredient>();

                foreach (var herb in this.Herbs) {
                    ingredients.Add(herb);
                }

                return ingredients.ToArray();
            }
        }

        public List<Potion> PotionPrototypes => this.potionPrototypes;

        public List<Potion> PotionsForSale => this.potionsForSale;

        public event EventHandler<FloatEventArgs> GoldChanged;

        public event EventHandler<EmployeeEventArgs> EmployeeHired;

        public event EventHandler<EmployeeEventArgs> EmployeeFired;

        public event EventHandler<IngredientEventArgs> IngredientDelivered;

        public event EventHandler<IngredientEventArgs> IngredientDiscarded;

        public event EventHandler<FlaskEventArgs> FlaskBought;

        public event EventHandler<FlaskEventArgs> FlaskDiscarded;

        public event EventHandler<PotionEventArgs> PotionResearched;

        public event EventHandler<PotionCreatedEventArgs> PotionCreated;

        public event EventHandler<PotionSoldEventArgs> PotionSold;

        public Shop() {
            this.gold = 10000;
            this.apothecaries = new List<Apothecary>();
            this.guards = new List<Guard>();
            this.herbalists = new List<Herbalist>();
            this.shopkeepers = new List<Shopkeeper>();
            this.flasks = new List<Flask>();
            this.solvents = new List<Solvent>();
            this.herbs = new List<Herb>();
            this.potionPrototypes = new List<Potion>();
            this.potionsForSale = new List<Potion>();
        }

        public void Start() {
            foreach (var employee in this.Employees) {
                employee.StartWorking();
            }

            World.Instance.DayChanged += (sender, e) => {
                foreach (var employee in this.Employees) {
                    this.Gold -= employee.Salary;
                }
            };
        }

        public void HireEmployee(Employee employee) {
            if (employee is Apothecary) {
                World.Instance.Apothecaries.Remove((Apothecary)employee);

                this.Apothecaries.Add((Apothecary)employee);
            } else if (employee is Guard) {
                World.Instance.Guards.Remove((Guard)employee);

                this.Guards.Add((Guard)employee);
            } else if (employee is Herbalist) {
                World.Instance.Herbalists.Remove((Herbalist)employee);

                this.Herbalists.Add((Herbalist)employee);
            } else if (employee is Shopkeeper) {
                World.Instance.Shopkeepers.Remove((Shopkeeper)employee);

                this.Shopkeepers.Add((Shopkeeper)employee);
            }

            employee.StartWorking();

            this.OnEmployeeHired(employee);
        }

        public void FireEmployee(Employee employee) {
            if (employee is Apothecary) {
                this.Apothecaries.Remove((Apothecary)employee);
            } else if (employee is Guard) {
                this.Guards.Remove((Guard)employee);
            } else if (employee is Herbalist) {
                this.Herbalists.Remove((Herbalist)employee);
            } else if (employee is Shopkeeper) {
                this.Shopkeepers.Remove((Shopkeeper)employee);
            }

            employee.StopWorking();

            this.OnEmployeeFired(employee);
        }

        public bool PurchaseFlask(Flask flask) {
            if (this.Gold < flask.Value) {
                return false;
            }

            this.Gold -= flask.Value;

            this.Flasks.Add(flask);

            this.OnFlaskBought(flask);

            return true;
        }

        public void DiscardFlask(Flask flask) {
            this.Flasks.Remove(flask);

            this.OnFlaskDiscarded(flask);
        }

        public void DeliverIngredient(Ingredient ingredient) {
            if (ingredient is Herb) {
                this.herbs.Add((Herb)ingredient);
            }

            this.OnIngredientDelivered(ingredient);
        }

        public void DiscardIngredient(Ingredient ingredient) {
            if (ingredient is Herb) {
                this.herbs.Remove((Herb)ingredient);
            }

            this.OnIngredientDiscarded(ingredient);
        }

        public void ResearchPotion(Flask flask, Solvent solvent, Ingredient[] ingredients) {
            foreach (var ingredient in ingredients) {
                Debug.Log(ingredient.Name);
            }
            var potion = new Potion(flask, solvent, ingredients);

            if (!this.AddPotionPrototype(potion)) {
                return;
            }

            this.RemovePotionMaterials(flask, solvent, ingredients);

            this.OnPotionResearched(potion);
        }

        public void CreatePotion(Flask flask, Solvent solvent, Ingredient[] ingredients, Apothecary apothecary) {
            var potion = new Potion(flask, solvent, ingredients);

            this.PotionsForSale.Add(potion);

            this.RemovePotionMaterials(flask, solvent, ingredients);

            this.OnPotionCreated(potion, apothecary);
        }

        public void SellPotion(Potion potion, Shopkeeper shopkeeper) {
            this.Gold += potion.Value;

            this.PotionsForSale.Remove(potion);

            this.OnPotionSold(potion, shopkeeper);
        }

        private bool AddPotionPrototype(Potion potion) {
            foreach (var potionPrototype in this.PotionPrototypes) {
                if (potion.Name == potionPrototype.Name) {
                    return false;
                }
            }

            this.PotionPrototypes.Add(potion);

            return true;
        }

        private void RemovePotionMaterials(Flask flask, Solvent solvent, Ingredient[] ingredients) {
            this.DiscardFlask(flask);

            // DiscardSolvent(solvent);

            foreach (var ingredient in ingredients) {
                this.DiscardIngredient(ingredient);
            }
        }

        private void OnGoldChanged(float value) => GoldChanged?.Invoke(this, new FloatEventArgs(value));

        private void OnEmployeeHired(Employee employee) => EmployeeHired?.Invoke(this, new EmployeeEventArgs(employee));

        private void OnEmployeeFired(Employee employee) => EmployeeFired?.Invoke(this, new EmployeeEventArgs(employee));

        private void OnIngredientDelivered(Ingredient ingredient) => IngredientDelivered?.Invoke(this, new IngredientEventArgs(ingredient));

        private void OnIngredientDiscarded(Ingredient ingredient) => IngredientDiscarded?.Invoke(this, new IngredientEventArgs(ingredient));

        private void OnFlaskBought(Flask flask) => FlaskBought?.Invoke(this, new FlaskEventArgs(flask));

        private void OnFlaskDiscarded(Flask flask) => FlaskDiscarded?.Invoke(this, new FlaskEventArgs(flask));

        private void OnPotionResearched(Potion potion) => PotionResearched?.Invoke(this, new PotionEventArgs(potion));

        private void OnPotionCreated(Potion potion, Apothecary apothecary) => PotionCreated?.Invoke(this, new PotionCreatedEventArgs(potion, apothecary));

        private void OnPotionSold(Potion potion, Shopkeeper shopkeeper) => PotionSold?.Invoke(this, new PotionSoldEventArgs(potion, shopkeeper));
    }
}
