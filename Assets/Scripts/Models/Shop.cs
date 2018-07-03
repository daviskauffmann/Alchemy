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
            get { return gold; }
            set {
                if (gold != value) {
                    gold = value;

                    OnGoldChanged(gold);
                }
            }
        }

        public List<Apothecary> Apothecaries {
            get { return apothecaries; }
        }

        public List<Guard> Guards {
            get { return guards; }
        }

        public List<Herbalist> Herbalists {
            get { return herbalists; }
        }

        public List<Shopkeeper> Shopkeepers {
            get { return shopkeepers; }
        }

        public Employee[] Employees {
            get {
                var employees = new List<Employee>();

                foreach (var apothecary in Apothecaries) {
                    employees.Add(apothecary);
                }

                foreach (var guard in Guards) {
                    employees.Add(guard);
                }

                foreach (var herbalist in Herbalists) {
                    employees.Add(herbalist);
                }

                foreach (var shopkeeper in Shopkeepers) {
                    employees.Add(shopkeeper);
                }

                return employees.ToArray();
            }
        }

        public List<Flask> Flasks {
            get { return flasks; }
        }

        public List<Solvent> Solvents {
            get { return solvents; }
        }

        public List<Herb> Herbs {
            get { return herbs; }
        }

        public Ingredient[] Ingredients {
            get {
                var ingredients = new List<Ingredient>();

                foreach (var herb in Herbs) {
                    ingredients.Add(herb);
                }

                return ingredients.ToArray();
            }
        }

        public List<Potion> PotionPrototypes {
            get { return potionPrototypes; }
        }

        public List<Potion> PotionsForSale {
            get { return potionsForSale; }
        }

        public event EventHandler<FloatEventArgs> GoldChanged;

        public event EventHandler<EmployeeEventArgs> EmployeeHired;

        public event EventHandler<EmployeeEventArgs> EmployeeFired;

        public event EventHandler<IngredientEventArgs> IngredientDelivered;

        public event EventHandler<IngredientEventArgs> IngredientDiscarded;

        public event EventHandler<FlaskEventArgs> FlaskBought;

        public event EventHandler<FlaskEventArgs> FlaskDiscarded;

        public event EventHandler<PotionEventArgs> PotionResearched;

        public event EventHandler<PotionEventArgs> PotionCreated;

        public event EventHandler<PotionEventArgs> PotionSold;

        public Shop() {
            gold = 10000;
            apothecaries = new List<Apothecary>();
            guards = new List<Guard>();
            herbalists = new List<Herbalist>();
            shopkeepers = new List<Shopkeeper>();
            flasks = new List<Flask>();
            solvents = new List<Solvent>();
            herbs = new List<Herb>();
            potionPrototypes = new List<Potion>();
            potionsForSale = new List<Potion>();

            foreach (var employee in Employees) {
                employee.StartWorking();
            }

            World.Instance.DayChanged += (sender, e) => {
                foreach (var employee in Employees) {
                    World.Instance.Shop.Gold -= employee.Salary;
                }
            };
        }

        public void HireEmployee(Employee employee) {
            if (employee is Apothecary) {
                World.Instance.Apothecaries.Remove((Apothecary)employee);

                apothecaries.Add((Apothecary)employee);
            } else if (employee is Guard) {
                World.Instance.Guards.Remove((Guard)employee);

                guards.Add((Guard)employee);
            } else if (employee is Herbalist) {
                World.Instance.Herbalists.Remove((Herbalist)employee);

                herbalists.Add((Herbalist)employee);
            } else if (employee is Shopkeeper) {
                World.Instance.Shopkeepers.Remove((Shopkeeper)employee);

                shopkeepers.Add((Shopkeeper)employee);
            }

            employee.StartWorking();

            OnEmployeeHired(employee);
        }

        public void FireEmployee(Employee employee) {
            if (employee is Apothecary) {
                apothecaries.Remove((Apothecary)employee);
            } else if (employee is Guard) {
                guards.Remove((Guard)employee);
            } else if (employee is Herbalist) {
                herbalists.Remove((Herbalist)employee);
            } else if (employee is Shopkeeper) {
                shopkeepers.Remove((Shopkeeper)employee);
            }

            employee.StopWorking();

            OnEmployeeFired(employee);
        }

        public bool PurchaseFlask(Flask flask) {
            if (Gold < flask.Value) {
                return false;
            }

            Gold -= flask.Value;

            Flasks.Add(flask);

            OnFlaskBought(flask);

            return true;
        }

        public void DiscardFlask(Flask flask) {
            Flasks.Remove(flask);

            OnFlaskDiscarded(flask);
        }

        public void DeliverIngredient(Ingredient ingredient) {
            if (ingredient is Herb) {
                herbs.Add((Herb)ingredient);
            }

            OnIngredientDelivered(ingredient);
        }

        public void DiscardIngredient(Ingredient ingredient) {
            if (ingredient is Herb) {
                herbs.Remove((Herb)ingredient);
            }

            OnIngredientDiscarded(ingredient);
        }

        public void ResearchPotion(Flask flask, Solvent solvent, Ingredient[] ingredients) {
            var potion = new Potion(flask, solvent, ingredients);

            if (!AddPotionPrototype(potion)) {
                return;
            }

            RemovePotionMaterials(flask, solvent, ingredients);

            OnPotionResearched(potion);
        }

        public void CreatePotion(Flask flask, Solvent solvent, Ingredient[] ingredients, Apothecary apothecary) {
            var potion = new Potion(flask, solvent, ingredients);

            PotionsForSale.Add(potion);

            RemovePotionMaterials(flask, solvent, ingredients);

            OnPotionCreated(potion, apothecary);
        }

        public void SellPotion(Potion potion, Shopkeeper shopkeeper) {
            Gold += potion.Value;

            PotionsForSale.Remove(potion);

            OnPotionSold(potion, shopkeeper);
        }

        private bool AddPotionPrototype(Potion potion) {
            foreach (var potionPrototype in PotionPrototypes) {
                if (potion.Name == potionPrototype.Name) {
                    return false;
                }
            }

            PotionPrototypes.Add(potion);

            return true;
        }

        private void RemovePotionMaterials(Flask flask, Solvent solvent, Ingredient[] ingredients) {
            DiscardFlask(flask);

            // DiscardSolvent(solvent);

            foreach (var ingredient in ingredients) {
                DiscardIngredient(ingredient);
            }
        }

        private void OnGoldChanged(float value) {
            if (GoldChanged != null) {
                GoldChanged(this, new FloatEventArgs() { value = value });
            }
        }

        private void OnEmployeeHired(Employee employee) {
            if (EmployeeHired != null) {
                EmployeeHired(this, new EmployeeEventArgs() { employee = employee });
            }
        }

        private void OnEmployeeFired(Employee employee) {
            if (EmployeeFired != null) {
                EmployeeFired(this, new EmployeeEventArgs() { employee = employee });
            }
        }

        private void OnIngredientDelivered(Ingredient ingredient) {
            if (IngredientDelivered != null) {
                IngredientDelivered(this, new IngredientEventArgs() { ingredient = ingredient });
            }
        }

        private void OnIngredientDiscarded(Ingredient ingredient) {
            if (IngredientDiscarded != null) {
                IngredientDiscarded(this, new IngredientEventArgs() { ingredient = ingredient });
            }
        }

        private void OnFlaskBought(Flask flask) {
            if (FlaskBought != null) {
                FlaskBought(this, new FlaskEventArgs() { flask = flask });
            }
        }

        private void OnFlaskDiscarded(Flask flask) {
            if (FlaskDiscarded != null) {
                FlaskDiscarded(this, new FlaskEventArgs() { flask = flask });
            }
        }

        private void OnPotionResearched(Potion potion) {
            if (PotionResearched != null) {
                PotionResearched(this, new PotionEventArgs() { potion = potion });
            }
        }

        private void OnPotionCreated(Potion potion, Apothecary apothecary) {
            if (PotionCreated != null) {
                PotionCreated(this, new PotionEventArgs() { potion = potion, employee = apothecary });
            }
        }

        private void OnPotionSold(Potion potion, Shopkeeper shopkeeper) {
            if (PotionSold != null) {
                PotionSold(this, new PotionEventArgs() { potion = potion, employee = shopkeeper });
            }
        }
    }
}