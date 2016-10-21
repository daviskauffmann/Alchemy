using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class HerbWindow : MonoBehaviour
    {
        [SerializeField]HerbInShop _herbInShopPrefab = null;
        [SerializeField]Transform _herbInShopArea = null;
        Dictionary<Herb, HerbInShop> _herbInShopGameObjects;

        void Awake()
        {
            _herbInShopGameObjects = new Dictionary<Herb, HerbInShop>();
        }

        void Start()
        {
            World.Instance.Shop.Ingredients.HerbAdded += CreateHerbInShop;
            World.Instance.Shop.Ingredients.HerbRemoved += RemoveHerbInShop;

            for (int i = 0; i < World.Instance.Shop.Ingredients.Herbs.Count; i++)
            {
                CreateHerbInShop(World.Instance.Shop.Ingredients, new HerbEventArgs(World.Instance.Shop.Ingredients.Herbs[i]));
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