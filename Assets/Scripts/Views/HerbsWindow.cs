using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class HerbsWindow : MonoBehaviour
    {
        [SerializeField]HerbComponent _herbPrefab = null;
        [SerializeField]Transform _herbArea = null;
        Dictionary<Herb, HerbComponent> _herbs;

        void Awake()
        {
            _herbs = new Dictionary<Herb, HerbComponent>();
        }

        void Start()
        {
            World.Instance.Shop.Ingredients.HerbAdded += CreateHerb;
            World.Instance.Shop.Ingredients.HerbRemoved += RemoveHerb;

            for (int i = 0; i < World.Instance.Shop.Ingredients.Herbs.Count; i++)
            {
                CreateHerb(World.Instance.Shop.Ingredients, new HerbEventArgs(World.Instance.Shop.Ingredients.Herbs[i]));
            }
        }

        void CreateHerb(object sender, HerbEventArgs e)
        {
            if (!_herbs.ContainsKey(e.Herb))
            {
                var herb = Instantiate<HerbComponent>(_herbPrefab);
                herb.transform.SetParent(_herbArea);
                herb.herb = e.Herb;

                _herbs.Add(e.Herb, herb);
            }
        }

        void RemoveHerb(object sender, HerbEventArgs e)
        {
            if (e.Herb.Amount < 1)
            {
                Destroy(_herbs[e.Herb].gameObject);

                _herbs.Remove(e.Herb);
            }
        }
    }
}