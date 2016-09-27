using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
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
            World.Instance.Applicants.CountChanged += ChangeTitleText;
            World.Instance.ApplicantReceived += CreateApplication;
            World.Instance.ApplicantDismissed += RemoveApplication;
            World.Instance.Shop.EmployeeHired += RemoveApplication;

            ChangeTitleText(World.Instance.Applicants, new IntEventArgs(World.Instance.Applicants.Total.Length));

            for (int i = 0; i < World.Instance.Applicants.Total.Length; i++)
            {
                CreateApplication(World.Instance.Applicants, new EmployeeEventArgs(World.Instance.Applicants.Total[i]));
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