using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Controllers
{
    public class ApplicationWindow : MonoBehaviour
    {
        [SerializeField]
        Text _title = null;
        [SerializeField]
        Application _applicationPrefab = null;
        [SerializeField]
        Transform _applicationArea = null;
        Dictionary<Employee, Application> _applicationGameObjects;

        void Awake()
        {
            _applicationGameObjects = new Dictionary<Employee, Application>();
        }

        void Start()
        {
            GameManager.World.Applicants.CountChanged += ChangeTitleText;
            GameManager.World.ApplicantReceived += CreateApplication;
            GameManager.World.ApplicantDismissed += RemoveApplication;
            GameManager.World.Shop.EmployeeHired += RemoveApplication;

            ChangeTitleText(GameManager.World.Applicants, new IntEventArgs(GameManager.World.Applicants.Total.Length));
            for (int i = 0; i < GameManager.World.Applicants.Total.Length; i++)
            {
                CreateApplication(GameManager.World.Applicants, new EmployeeEventArgs(GameManager.World.Applicants.Total[i]));
            }
        }

        void ChangeTitleText(object sender, IntEventArgs e)
        {
            _title.text = string.Format("Applications{0}", e.Value == 0 ? string.Empty : string.Format(" ({0})", e.Value));
        }

        void CreateApplication(object sender, EmployeeEventArgs e)
        {
            var _applicationGameObject = Instantiate<Application>(_applicationPrefab);
            _applicationGameObject.transform.SetParent(_applicationArea);
            _applicationGameObject.applicant = e.Employee;
            _applicationGameObjects.Add(e.Employee, _applicationGameObject);
        }

        void RemoveApplication(object sender, EmployeeEventArgs e)
        {
            Destroy(_applicationGameObjects[e.Employee].gameObject);
            _applicationGameObjects.Remove(e.Employee);
        }
    }
}