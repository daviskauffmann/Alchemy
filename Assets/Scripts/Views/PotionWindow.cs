using System.Collections.Generic;
using UnityEngine;
using Alchemy.Models;

namespace Alchemy.Controllers
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

		void Awake()
        {
            _potionForSaleGameObjects = new Dictionary<Potion, PotionForSale>();
		}

        void Start()
        {
            GameManager.World.Shop.PotionResearched += CreatePotionPrototype;
            GameManager.World.Shop.PotionCreated += CreatePotionForSale;
            GameManager.World.Shop.PotionSold += RemovePotionForSale;

			for (int i = 0; i < GameManager.World.Shop.PotionPrototypes.Count; i++)
            {
                CreatePotionPrototype(GameManager.World.Shop, new PotionEventArgs(GameManager.World.Shop.PotionPrototypes[i]));
            }
            for (int i = 0; i < GameManager.World.Shop.PotionsForSale.Count; i++)
            {
                CreatePotionForSale(GameManager.World.Shop, new PotionEventArgs(GameManager.World.Shop.PotionsForSale[i]));
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
	}
}