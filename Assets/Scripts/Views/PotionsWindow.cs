using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class PotionsWindow : MonoBehaviour
    {
        [SerializeField]CanvasGroup _researchContent = null;
        Flask _flask;
        [SerializeField]Text _flaskText = null;
        Solvent _solvent;
        Ingredient _ingredient1;
        Ingredient _ingredient2;
        [SerializeField]CanvasGroup _prototypesContent = null;
        [SerializeField]PotionPrototypeComponent _potionPrototypePrefab = null;
        [SerializeField]Transform _prototypesArea = null;
        [SerializeField]CanvasGroup _potionShopContent = null;
        [SerializeField]PotionShopComponent _potionShopPrefab = null;
        [SerializeField]Transform _potionShopArea = null;
        Dictionary<Potion, PotionShopComponent> _potionsInShop;

        void Awake()
        {
            _potionsInShop = new Dictionary<Potion, PotionShopComponent>();
        }

        void Start()
        {
            ShowResearchContent();

            World.Instance.Shop.PotionResearched += CreatePotionPrototype;
            World.Instance.Shop.PotionCreated += CreatePotionShop;
            World.Instance.Shop.PotionSold += RemovePotionShop;

            for (int i = 0; i < World.Instance.Shop.PotionPrototypes.Count; i++)
            {
                CreatePotionPrototype(World.Instance.Shop, new PotionEventArgs(World.Instance.Shop.PotionPrototypes[i]));
            }

            for (int i = 0; i < World.Instance.Shop.PotionsForSale.Count; i++)
            {
                CreatePotionShop(World.Instance.Shop, new PotionEventArgs(World.Instance.Shop.PotionsForSale[i]));
            }
        }

        public void ShowResearchContent()
        {
            _researchContent.alpha = 1;
            _researchContent.blocksRaycasts = true;

            _prototypesContent.alpha = 0;
            _prototypesContent.blocksRaycasts = false;

            _potionShopContent.alpha = 0;
            _potionShopContent.blocksRaycasts = false;
        }

        public void ChangeFlask()
        {
            if (World.Instance.Shop.Flasks.Count > 0)
            {
                _flask = World.Instance.Shop.Flasks[0];

                _flaskText.text = _flask.Name;
            }
        }

        public void ChangeSolvent()
        {

        }

        public void ChangeIngredient1()
        {
            
        }

        public void ChangeIngredient2()
        {

        }

        public void Research()
        {
            if (_flask == null || _ingredient1 == null || _ingredient2 == null)
            {
                return;
            }

            World.Instance.Shop.ResearchPotion(_flask, _solvent, new Ingredient[] { _ingredient1, _ingredient2 });
        }

        public void ShowPrototypeContent()
        {
            _prototypesContent.alpha = 1;
            _prototypesContent.blocksRaycasts = true;

            _researchContent.alpha = 0;
            _researchContent.blocksRaycasts = false;

            _potionShopContent.alpha = 0;
            _potionShopContent.blocksRaycasts = false;
        }

        void CreatePotionPrototype(object sender, PotionEventArgs e)
        {
            var potionPrototype = Instantiate<PotionPrototypeComponent>(_potionPrototypePrefab);
            potionPrototype.transform.SetParent(_prototypesArea);
            potionPrototype.potion = e.Potion;
        }

        public void ShowShopContent()
        {
            _potionShopContent.alpha = 1;
            _potionShopContent.blocksRaycasts = true;

            _researchContent.alpha = 0;
            _researchContent.blocksRaycasts = false;

            _prototypesContent.alpha = 0;
            _prototypesContent.blocksRaycasts = false;
        }

        void CreatePotionShop(object sender, PotionEventArgs e)
        {
            var potionShop = Instantiate<PotionShopComponent>(_potionShopPrefab);
            potionShop.transform.SetParent(_potionShopArea);
            potionShop.potion = e.Potion;

            _potionsInShop.Add(e.Potion, potionShop);
        }

        void RemovePotionShop(object sender, PotionEventArgs e)
        {
            Destroy(_potionsInShop[e.Potion].gameObject);

            _potionsInShop.Remove(e.Potion);
        }
    }
}