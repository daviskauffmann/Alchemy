using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class PotionWindow : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup _potionResearchContent = null;
        [SerializeField]
        CanvasGroup _potionPrototypeContent = null;
        [SerializeField]
        PotionPrototype _potionPrototypePrefab = null;
        [SerializeField]
        Transform _potionPrototypeArea = null;
        [SerializeField]
        CanvasGroup _potionForSaleContent = null;
        [SerializeField]
        PotionForSale _potionForSalePrefab = null;
        [SerializeField]
        Transform _potionForSaleArea = null;
        Dictionary<Potion, PotionForSale> _potionForSaleGameObjects;
		[SerializeField]
		FlaskInShop _flaskInShopPrefab = null;
		[SerializeField]
		Transform _flaskInShopArea = null;
		Dictionary<Flask, FlaskInShop> _flaskInShopGameObjects;
		[SerializeField]
		HerbInShop _herbInShopPrefab = null;
		[SerializeField]
		Transform _herbInShopArea = null;
		Dictionary<Herb, HerbInShop> _herbInShopGameObjects;

		void Awake()
        {
            _potionForSaleGameObjects = new Dictionary<Potion, PotionForSale>();
			_flaskInShopGameObjects = new Dictionary<Flask, FlaskInShop>();
			_herbInShopGameObjects = new Dictionary<Herb, HerbInShop>();
		}

        void Start()
        {
            World.Instance.Shop.PotionResearched += CreatePotionPrototype;
            World.Instance.Shop.PotionCreated += CreatePotionForSale;
            World.Instance.Shop.PotionSold += RemovePotionForSale;

			for (int i = 0; i < World.Instance.Shop.PotionPrototypes.Count; i++)
            {
                CreatePotionPrototype(World.Instance.Shop, new PotionEventArgs(World.Instance.Shop.PotionPrototypes[i]));
            }
            for (int i = 0; i < World.Instance.Shop.PotionsForSale.Count; i++)
            {
                CreatePotionForSale(World.Instance.Shop, new PotionEventArgs(World.Instance.Shop.PotionsForSale[i]));
            }

			World.Instance.Shop.FlaskBought += CreateFlaskInShop;
			World.Instance.Shop.FlaskDiscarded += RemoveFlaskInShop;

			for (int i = 0; i < World.Instance.Shop.Flasks.Count; i++)
			{
				CreateFlaskInShop(World.Instance.Shop, new FlaskEventArgs(World.Instance.Shop.Flasks[i]));
			}

			World.Instance.Shop.Ingredients.HerbAdded += CreateHerbInShop;
			World.Instance.Shop.Ingredients.HerbRemoved += RemoveHerbInShop;

			for (int i = 0; i < World.Instance.Shop.Ingredients.Herbs.Count; i++)
			{
				CreateHerbInShop(World.Instance.Shop.Ingredients, new HerbEventArgs(World.Instance.Shop.Ingredients.Herbs[i]));
			}

			ShowPotionResearch();
        }

        public void ShowPotionResearch()
        {
            _potionResearchContent.alpha = 1;
            _potionResearchContent.blocksRaycasts = true;
            _potionPrototypeContent.alpha = 0;
            _potionPrototypeContent.blocksRaycasts = false;
            _potionForSaleContent.alpha = 0;
            _potionForSaleContent.blocksRaycasts = false;
        }

        public void ShowPotionPrototypes()
        {
            _potionPrototypeContent.alpha = 1;
            _potionPrototypeContent.blocksRaycasts = true;
            _potionResearchContent.alpha = 0;
            _potionResearchContent.blocksRaycasts = false;
            _potionForSaleContent.alpha = 0;
            _potionForSaleContent.blocksRaycasts = false;
        }

        void CreatePotionPrototype(object sender, PotionEventArgs e)
        {
            var potionPrototypeGameObject = Instantiate<PotionPrototype>(_potionPrototypePrefab);
            potionPrototypeGameObject.transform.SetParent(_potionPrototypeArea);
            potionPrototypeGameObject.potion = e.Potion;
        }

        public void ShowPotionsForSale()
        {
            _potionForSaleContent.alpha = 1;
            _potionForSaleContent.blocksRaycasts = true;
            _potionResearchContent.alpha = 0;
            _potionResearchContent.blocksRaycasts = false;
            _potionPrototypeContent.alpha = 0;
            _potionPrototypeContent.blocksRaycasts = false;
        }

        void CreatePotionForSale(object sender, PotionEventArgs e)
        {
            var potionForSaleGameObject = Instantiate<PotionForSale>(_potionForSalePrefab);
            potionForSaleGameObject.transform.SetParent(_potionForSaleArea);
            potionForSaleGameObject.potion = e.Potion;
            _potionForSaleGameObjects.Add(e.Potion, potionForSaleGameObject);
        }

        void RemovePotionForSale(object sender, PotionEventArgs e)
        {
            Destroy(_potionForSaleGameObjects[e.Potion].gameObject);
            _potionForSaleGameObjects.Remove(e.Potion);
        }

		void CreateFlaskInShop(object sender, FlaskEventArgs e)
		{
			if (!_flaskInShopGameObjects.ContainsKey(e.Flask))
			{
				var flaskInShopGameObject = Instantiate<FlaskInShop>(_flaskInShopPrefab);
				flaskInShopGameObject.transform.SetParent(_flaskInShopArea);
				flaskInShopGameObject.flask = e.Flask;
				_flaskInShopGameObjects.Add(e.Flask, flaskInShopGameObject);
			}
		}

		void RemoveFlaskInShop(object sender, FlaskEventArgs e)
		{
			if (e.Flask.Amount < 1)
			{
				Destroy(_flaskInShopGameObjects[e.Flask].gameObject);
				_flaskInShopGameObjects.Remove(e.Flask);
			}
		}

		void CreateHerbInShop(object sender, HerbEventArgs e)
		{
			if (!_herbInShopGameObjects.ContainsKey(e.Herb))
			{
				var herbInShopGameObject = Instantiate<HerbInShop>(_herbInShopPrefab);
				herbInShopGameObject.transform.SetParent(_herbInShopArea);
				herbInShopGameObject.herb = e.Herb;
				_herbInShopGameObjects.Add(e.Herb, herbInShopGameObject);
			}
		}

		void RemoveHerbInShop(object sender, HerbEventArgs e)
		{
			if (e.Herb.Amount < 1)
			{
				Destroy(_herbInShopGameObjects[e.Herb].gameObject);
				_herbInShopGameObjects.Remove(e.Herb);
			}
		}
	}
}