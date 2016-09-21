using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class PotionPrototypeComponent : MonoBehaviour
    {
        public Potion potion;

        [SerializeField]Text _name = null;

        void Start()
        {
            _name.text = potion.Name;
        }
    }
}