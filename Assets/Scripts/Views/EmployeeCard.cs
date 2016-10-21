using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class EmployeeCard : MonoBehaviour
    {
        public Employee employee;

        [SerializeField]
        Text _name = null;
        [SerializeField]
        Text _title = null;
        [SerializeField]
        Text _salary = null;

        void Start()
        {
            _name.text = employee.Name;
            _title.text = employee.Title;
            switch (employee.Title)
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
            _salary.text = string.Format("{0} gold/day", employee.Salary);
        }

        public void Fire()
        {
            World.Instance.Shop.FireEmployee(employee);
        }
    }
}