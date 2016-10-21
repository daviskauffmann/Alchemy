using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class FlaskForSale : MonoBehaviour
    {
        public Flask flask;

        [SerializeField]
        Text _amount = null;
        [SerializeField]
        Text _name = null;
        [SerializeField]
        Text _quality = null;
        [SerializeField]
        Text _value = null;

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
            _value.text = string.Format("{0} Gold", flask.Value);
        }

        void Update()
        {
            _amount.text = flask.Amount.ToString();
        }

        public void Buy()
        {
            World.Instance.SellFlask(World.Instance.GetFlaskPrototype(flask.Name));
        }
    }
}