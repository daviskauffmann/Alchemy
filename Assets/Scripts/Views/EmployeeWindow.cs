using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Controllers
{
    public class EmployeeWindow : MonoBehaviour
    {
        [SerializeField]
        EmployeeCard _employeeCardPrefab = null;
        [SerializeField]
        Transform _employeeCardArea = null;
        Dictionary<Employee, EmployeeCard> _employeeCardGameObjects;

        void Awake()
        {
            _employeeCardGameObjects = new Dictionary<Employee, EmployeeCard>();
        }

        void Start()
        {
            GameManager.World.Shop.EmployeeHired += CreateEmployeeCard;
            GameManager.World.Shop.EmployeeFired += RemoveEmployeeCard;

            for (int i = 0; i < GameManager.World.Shop.Employees.Total.Length; i++)
            {
                CreateEmployeeCard(GameManager.World.Shop.Employees, new EmployeeEventArgs(GameManager.World.Shop.Employees.Total[i]));
            }
        }

        void CreateEmployeeCard(object sender, EmployeeEventArgs e)
        {
            var employeeCardGameObject = Instantiate<EmployeeCard>(_employeeCardPrefab);
            employeeCardGameObject.transform.SetParent(_employeeCardArea);
            employeeCardGameObject.employee = e.Employee;
            _employeeCardGameObjects.Add(e.Employee, employeeCardGameObject);
        }

        void RemoveEmployeeCard(object sender, EmployeeEventArgs e)
        {
            Destroy(_employeeCardGameObjects[e.Employee].gameObject);
            _employeeCardGameObjects.Remove(e.Employee);
        }
    }
}