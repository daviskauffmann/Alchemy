using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class FlasksWindow : MonoBehaviour
    {
        [SerializeField]CanvasGroup _buyContent = null;
        [SerializeField]FlaskBuyComponent _flaskBuyPrefab = null;
        [SerializeField]Transform _buyArea = null;
        Dictionary<Flask, FlaskBuyComponent> _flasksToBuy;
        [SerializeField]CanvasGroup _purchasedContent = null;
        [SerializeField]FlaskPurchasedComponent _flaskPurchasedPrefab = null;
        [SerializeField]Transform _purchasedArea = null;
        Dictionary<Flask, FlaskPurchasedComponent> _flasksPurchased;

        void Awake()
        {
            _flasksToBuy = new Dictionary<Flask, FlaskBuyComponent>();
            _flasksPurchased = new Dictionary<Flask, FlaskPurchasedComponent>();
        }

        void Start()
        {
            ShowBuyContent();

            World.Instance.FlaskDisplayed += CreateFlaskBuy;
            World.Instance.FlaskSold += RemoveFlaskBuy;
            World.Instance.Shop.FlaskBought += CreateFlaskPurchased;
            World.Instance.Shop.FlaskDiscarded += RemoveFlaskPurchased;

            for (int i = 0; i < World.Instance.FlasksForSale.Count; i++)
            {
                CreateFlaskBuy(World.Instance, new FlaskEventArgs(World.Instance.FlasksForSale[i]));
            }

            for (int i = 0; i < World.Instance.Shop.Flasks.Count; i++)
            {
                CreateFlaskPurchased(World.Instance.Shop, new FlaskEventArgs(World.Instance.Shop.Flasks[i]));
            }
        }

        public void ShowBuyContent()
        {
            _buyContent.alpha = 1;
            _buyContent.blocksRaycasts = true;

            _purchasedContent.alpha = 0;
            _purchasedContent.blocksRaycasts = false;
        }

        void CreateFlaskBuy(object sender, FlaskEventArgs e)
        {
            if (!_flasksToBuy.ContainsKey(e.Flask))
            {
                var flaskBuy = Instantiate<FlaskBuyComponent>(_flaskBuyPrefab);
                flaskBuy.transform.SetParent(_buyArea);
                flaskBuy.flask = e.Flask;

                _flasksToBuy.Add(e.Flask, flaskBuy);
            }
        }

        void RemoveFlaskBuy(object sender, FlaskEventArgs e)
        {
            if (e.Flask.Amount < 1)
            {
                Destroy(_flasksToBuy[e.Flask].gameObject);

                _flasksToBuy.Remove(e.Flask);
            }
        }

        public void ShowPurchasedContent()
        {
            _purchasedContent.alpha = 1;
            _purchasedContent.blocksRaycasts = true;

            _buyContent.alpha = 0;
            _buyContent.blocksRaycasts = false;
        }

        void CreateFlaskPurchased(object sender, FlaskEventArgs e)
        {
            if (!_flasksPurchased.ContainsKey(e.Flask))
            {
                var flaskPurchased = Instantiate<FlaskPurchasedComponent>(_flaskPurchasedPrefab);
                flaskPurchased.transform.SetParent(_purchasedArea);
                flaskPurchased.flask = e.Flask;

                _flasksPurchased.Add(e.Flask, flaskPurchased);
            }
        }

        void RemoveFlaskPurchased(object sender, FlaskEventArgs e)
        {
            if (e.Flask.Amount < 1)
            {
                Destroy(_flasksPurchased[e.Flask].gameObject);

                _flasksPurchased.Remove(e.Flask);
            }
        }
    }
}