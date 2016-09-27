using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class FlaskWindow : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup _flaskForSaleContent = null;
        [SerializeField]
        FlaskForSale _flaskForSalePrefab = null;
        [SerializeField]
        Transform _flaskForSaleArea = null;
        Dictionary<Flask, FlaskForSale> _flaskForSaleGameObjects;
        [SerializeField]
        CanvasGroup _flaskInShopContent = null;
        [SerializeField]
        FlaskInShop _flaskInShopPrefab = null;
        [SerializeField]
        Transform _flaskInShopArea = null;
        Dictionary<Flask, FlaskInShop> _flaskInShopGameObjects;

        void Awake()
        {
            _flaskForSaleGameObjects = new Dictionary<Flask, FlaskForSale>();
            _flaskInShopGameObjects = new Dictionary<Flask, FlaskInShop>();
        }

        void Start()
        {
            ShowFlasksForSale();

            World.Instance.FlaskDisplayed += CreateFlaskForSale;
            World.Instance.FlaskSold += RemoveFlaskForSale;
            World.Instance.Shop.FlaskBought += CreateFlaskInShop;
            World.Instance.Shop.FlaskDiscarded += RemoveFlaskInShop;

            for (int i = 0; i < World.Instance.FlasksForSale.Count; i++)
            {
                CreateFlaskForSale(World.Instance, new FlaskEventArgs(World.Instance.FlasksForSale[i]));
            }

            for (int i = 0; i < World.Instance.Shop.Flasks.Count; i++)
            {
                CreateFlaskInShop(World.Instance.Shop, new FlaskEventArgs(World.Instance.Shop.Flasks[i]));
            }
        }

        public void ShowFlasksForSale()
        {
            _flaskForSaleContent.alpha = 1;
            _flaskForSaleContent.blocksRaycasts = true;

            _flaskInShopContent.alpha = 0;
            _flaskInShopContent.blocksRaycasts = false;
        }

        void CreateFlaskForSale(object sender, FlaskEventArgs e)
        {
            if (!_flaskForSaleGameObjects.ContainsKey(e.Flask))
            {
                var flaskForSaleGameObject = Instantiate<FlaskForSale>(_flaskForSalePrefab);
                flaskForSaleGameObject.transform.SetParent(_flaskForSaleArea);
                flaskForSaleGameObject.flask = e.Flask;

                _flaskForSaleGameObjects.Add(e.Flask, flaskForSaleGameObject);
            }
        }

        void RemoveFlaskForSale(object sender, FlaskEventArgs e)
        {
            if (e.Flask.Amount < 1)
            {
                Destroy(_flaskForSaleGameObjects[e.Flask].gameObject);

                _flaskForSaleGameObjects.Remove(e.Flask);
            }
        }

        public void ShowFlasksInShop()
        {
            _flaskInShopContent.alpha = 1;
            _flaskInShopContent.blocksRaycasts = true;

            _flaskForSaleContent.alpha = 0;
            _flaskForSaleContent.blocksRaycasts = false;
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
    }
}