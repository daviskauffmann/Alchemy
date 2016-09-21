using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class EmployeesWindow : MonoBehaviour
    {
        [SerializeField]EmployeeComponent _employeePrefab = null;
        [SerializeField]Transform _employeeArea = null;
        Dictionary<Employee, EmployeeComponent> _employees;

        void Awake()
        {
            _employees = new Dictionary<Employee, EmployeeComponent>();
        }

        void Start()
        {
            World.Instance.Shop.EmployeeHired += CreateEmployee;
            World.Instance.Shop.EmployeeFired += RemoveEmployee;

            for (int i = 0; i < World.Instance.Shop.Employees.Herbalists.Count; i++)
            {
                CreateEmployee(World.Instance.Shop, new EmployeeEventArgs(World.Instance.Shop.Employees.Herbalists[i]));
            }

            for (int i = 0; i < World.Instance.Shop.Employees.Apothecaries.Count; i++)
            {
                CreateEmployee(World.Instance.Shop, new EmployeeEventArgs(World.Instance.Shop.Employees.Apothecaries[i]));
            }

            for (int i = 0; i < World.Instance.Shop.Employees.Shopkeepers.Count; i++)
            {
                CreateEmployee(World.Instance.Shop, new EmployeeEventArgs(World.Instance.Shop.Employees.Shopkeepers[i]));
            }

            for (int i = 0; i < World.Instance.Shop.Employees.Guards.Count; i++)
            {
                CreateEmployee(World.Instance.Shop, new EmployeeEventArgs(World.Instance.Shop.Employees.Guards[i]));
            }
        }

        void CreateEmployee(object sender, EmployeeEventArgs e)
        {
            var employee = Instantiate<EmployeeComponent>(_employeePrefab);
            employee.transform.SetParent(_employeeArea);
            employee.employee = e.Employee;

            _employees.Add(e.Employee, employee);
        }

        void RemoveEmployee(object sender, EmployeeEventArgs e)
        {
            Destroy(_employees[e.Employee].gameObject);

            _employees.Remove(e.Employee);
        }
    }
}