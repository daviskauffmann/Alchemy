using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Controllers
{
    public class HerbInShop : MonoBehaviour
    {
        public Herb herb;

        [SerializeField]
        Text _amount = null;
        [SerializeField]
        Text _name = null;
        [SerializeField]
        Text _rarity = null;
		[SerializeField]
		Text[] _effects = null;

        void Start()
        {
            _name.text = herb.Name;
            _rarity.text = herb.Rarity.ToString();
            switch (herb.Rarity)
            {
                case Rarity.Common:
                    _rarity.color = Color.white;
                    break;
                case Rarity.Uncommon:
                    _rarity.color = Color.green;
                    break;
                case Rarity.Rare:
                    _rarity.color = Color.blue;
                    break;
                default:
                    _rarity.color = Color.white;
                    break;
            }
			for (int i = 0; i < herb.Effects.Length; i++)
			{
				_effects[i].text = herb.Effects[i].Name;
			}
        }

        void Update()
        {
            _amount.text = herb.Amount.ToString();
        }

        public void Discard()
        {
            GameManager.World.Shop.DiscardIngredient(GameManager.World.GetHerbPrototype(herb.Name));
        }
    }
}