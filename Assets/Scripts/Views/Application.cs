using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class Application : MonoBehaviour
    {
        public Employee applicant;

        [SerializeField]
        Text _name = null;
        [SerializeField]
        Text _title = null;
        [SerializeField]
        Text _salary = null;

        void Start()
        {
            _name.text = applicant.Name;
            _title.text = applicant.Title;
            _salary.text = string.Format("{0} gold/day", applicant.Salary);

            switch (applicant.Title)
            {
                case "Herbalist":
                    _title.color = Color.green;
                    break;
                case "Guard":
                    _title.color = Color.red;
                    break;
                case "Apothecary":
                    _title.color = Color.cyan;
                    break;
                case "Shopkeeper":
                    _title.color = Color.yellow;
                    break;
                default:
                    _title.color = Color.white;
                    break;
            }
        }

        public void Dismiss()
        {
            World.Instance.DismissApplication(applicant);
        }

        public void Hire()
        {
            World.Instance.Shop.HireEmployee(applicant);
        }
    }
}