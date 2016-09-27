using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class FlaskInShop : MonoBehaviour
    {
        public Flask flask;

        [SerializeField]
        Text _amount = null;
        [SerializeField]
        Text _name = null;
        [SerializeField]
        Text _quality = null;

        void Start()
        {
            _name.text = flask.Name;
            _quality.text = flask.Quality.ToString();

            switch (flask.Quality)
            {
                case Quality.Poor:
                    _quality.color = Color.gray;
                    break;
                case Quality.Fair:
                    _quality.color = Color.white;
                    break;
                case Quality.Good:
                    _quality.color = Color.green;
                    break;
                case Quality.Excellent:
                    _quality.color = Color.blue;
                    break;
                case Quality.Perfect:
                    _quality.color = Color.magenta;
                    break;
                default:
                    _quality.color = Color.white;
                    break;
            }
        }

        void Update()
        {
            _amount.text = flask.Amount.ToString();
        }

        public void Discard()
        {
            World.Instance.Shop.DiscardFlask(World.Instance.GetFlaskPrototype(flask.Name));
        }
    }
}