using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Shop
    {
        [SerializeField]float _gold;
        [SerializeField]Employees _employees;
        [SerializeField]List<Flask> _flasks;
        [SerializeField]List<Solvent> _solvents;
        [SerializeField]Ingredients _ingredients;
        [SerializeField]List<Potion> _potionPrototypes;
        [SerializeField]List<Potion> _potionsForSale;

        public event EventHandler<FloatEventArgs> GoldChanged;

        public event EventHandler<EmployeeEventArgs> EmployeeHired;

        public event EventHandler<EmployeeEventArgs> EmployeeFired;

        public event EventHandler<IngredientEventArgs> IngredientDelivered;

        public event EventHandler<IngredientEventArgs> IngredientDiscarded;

        public event EventHandler<FlaskEventArgs> FlaskBought;

        public event EventHandler<FlaskEventArgs> FlaskDiscarded;

        public event EventHandler<EffectEventArgs> EffectDiscovered;

        public event EventHandler<PotionEventArgs> PotionResearched;

        public event EventHandler<PotionEventArgs> PotionCreated;

        public event EventHandler<PotionEventArgs> PotionSold;

        public float Gold
        {
            get { return _gold; }
            set
            {
                if (_gold != value)
                {
                    _gold = value;
                    OnGoldChanged(_gold);
                }
            }
        }

        public Employees Employees
        {
            get { return _employees; }
        }

        public List<Flask> Flasks
        {
            get { return _flasks; }
        }

        public List<Solvent> Solvents
        {
            get { return _solvents; }
        }

        public Ingredients Ingredients
        {
            get { return _ingredients; }
        }

        public List<Potion> PotionPrototypes
        {
            get { return _potionPrototypes; }
        }

        public List<Potion> PotionsForSale
        {
            get { return _potionsForSale; }
        }

        public Shop()
        {
            _gold = 10000;
            _employees = new Employees();
            _flasks = new List<Flask>();
            _solvents = new List<Solvent>();
            _ingredients = new Ingredients();
            _potionPrototypes = new List<Potion>();
            _potionsForSale = new List<Potion>();
        }

        public void HireEmployee(Employee employee)
        {
            World.Instance.Applicants.Remove(employee);
            Employees.Add(employee);
            OnEmployeeHired(employee);
        }

        public void FireEmployee(Employee employee)
        {
            Employees.Remove(employee);
            OnEmployeeFired(employee);
        }

        public bool PurchaseFlask(Flask prototype)
        {
            if (Gold < prototype.Value)
            {
                return false;
            }

            Gold -= prototype.Value;
            bool newEntry = true;
            for (int i = 0; i < Flasks.Count; i++)
            {
                if (Flasks[i].Name == prototype.Name)
                {
                    Flasks[i].Amount++;
                    OnFlaskBought(Flasks[i]);
                    newEntry = false;
                    break;
                }
            }
            if (newEntry)
            {
                var flask = (Flask)prototype.Clone();
                flask.Amount = 1;
                Flasks.Add(flask);
                OnFlaskBought(flask);
            }
            return true;
        }

        public void DiscardFlask(Flask prototype)
        {
            for (int i = 0; i < Flasks.Count; i++)
            {
                if (Flasks[i].Name == prototype.Name)
                {
                    Flasks[i].Amount--;
                    if (Flasks[i].Amount < 0)
                    {
                        Flasks[i].Amount = 0;
                    }
                    OnFlaskDiscarded(Flasks[i]);
                    break;
                }
            }
        }

        public void DeliverIngredient(Ingredient prototype)
        {
            Ingredients.Add(prototype);
            OnIngredientDelivered(prototype);
        }

        public void DiscardIngredient(Ingredient prototype)
        {
            Ingredients.Remove(prototype);
            OnIngredientDiscarded(prototype);
        }

        public void ResearchPotion(Flask flask, Solvent solvent, Ingredient[] ingredients)
        {
            var potion = new Potion(flask, solvent, ingredients);   
            if (!AddPotionPrototype(potion))
            {
                return;
            }
            RemovePotionMaterials(flask, solvent, ingredients);
            OnPotionResearched(potion);

            for (int i = 0; i < ingredients.Length; i++)
            {
                for (int j = 0; j < ingredients[i].Effects.Length; j++)
                {
                    if (ingredients[i].Effects[j].Discovered)
                    {
                        OnEffectDiscovered(ingredients[i].Effects[j], ingredients[i]);
                    }
                }
            }
        }

        public void CreatePotion(Flask flask, Solvent solvent, Ingredient[] ingredients)
        {
            var potion = new Potion(flask, solvent, ingredients);
            PotionsForSale.Add(potion);
            RemovePotionMaterials(flask, solvent, ingredients);
            OnPotionCreated(potion);
        }

        public void SellPotion(Potion potion)
        {
            Gold += potion.Value;
            PotionsForSale.Remove(potion);
            OnPotionSold(potion);
        }

        protected virtual void OnGoldChanged(float value)
        {
            if (GoldChanged != null)
            {
                GoldChanged(this, new FloatEventArgs(value));
            }
        }

        protected virtual void OnEmployeeHired(Employee employee)
        {
            if (EmployeeHired != null)
            {
                EmployeeHired(this, new EmployeeEventArgs(employee));
            }
        }

        protected virtual void OnEmployeeFired(Employee employee)
        {
            if (EmployeeFired != null)
            {
                EmployeeFired(this, new EmployeeEventArgs(employee));
            }
        }

        protected virtual void OnIngredientDelivered(Ingredient ingredient)
        {
            if (IngredientDelivered != null)
            {
                IngredientDelivered(this, new IngredientEventArgs(ingredient));
            }
        }

        protected virtual void OnIngredientDiscarded(Ingredient ingredient)
        {
            if (IngredientDiscarded != null)
            {
                IngredientDiscarded(this, new IngredientEventArgs(ingredient));
            }
        }

        protected virtual void OnFlaskBought(Flask flask)
        {
            if (FlaskBought != null)
            {
                FlaskBought(this, new FlaskEventArgs(flask));
            }
        }

        protected virtual void OnFlaskDiscarded(Flask flask)
        {
            if (FlaskDiscarded != null)
            {
                FlaskDiscarded(this, new FlaskEventArgs(flask));
            }
        }

        protected virtual void OnEffectDiscovered(Effect effect, Ingredient ingredient)
        {
            if (EffectDiscovered != null)
            {
                EffectDiscovered(this, new EffectEventArgs(effect, ingredient));
            }
        }

        protected virtual void OnPotionResearched(Potion potion)
        {
            if (PotionResearched != null)
            {
                PotionResearched(this, new PotionEventArgs(potion));
            }
        }

        protected virtual void OnPotionCreated(Potion potion)
        {
            if (PotionCreated != null)
            {
                PotionCreated(this, new PotionEventArgs(potion));
            }
        }

        protected virtual void OnPotionSold(Potion potion)
        {
            if (PotionSold != null)
            {
                PotionSold(this, new PotionEventArgs(potion));
            }
        }

        void RemovePotionMaterials(Flask flask, Solvent solvent, Ingredient[] ingredients)
        {
            DiscardFlask(World.Instance.GetFlaskPrototype(flask.Name));

            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i] is Herb)
                {
                    DiscardIngredient(World.Instance.GetHerbPrototype(ingredients[i].Name));
                }
            }
        }

        bool AddPotionPrototype(Potion potion)
        {
            for (int i = 0; i < PotionPrototypes.Count; i++)
            {
                if (potion.Name == PotionPrototypes[i].Name)
                {
                    return false;
                }
            }

            PotionPrototypes.Add(potion);
            return true;
        }
    }
}