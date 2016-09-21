using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class ApplicationsWindow : MonoBehaviour
    {
        [SerializeField]Text _title = null;
        [SerializeField]ApplicationComponent _applicationPrefab = null;
        [SerializeField]Transform _applicationArea = null;
        Dictionary<Employee, ApplicationComponent> _applications;

        void Awake()
        {
            _applications = new Dictionary<Employee, ApplicationComponent>();
        }

        void Start()
        {
            World.Instance.Applicants.CountChanged += ChangeTitleText;
            World.Instance.ApplicantReceived += CreateApplication;
            World.Instance.ApplicantDismissed += RemoveApplication;
            World.Instance.Shop.EmployeeHired += RemoveApplication;

            ChangeTitleText(World.Instance.Applicants, new IntEventArgs(World.Instance.Applicants.Count));

            for (int i = 0; i < World.Instance.Applicants.Herbalists.Count; i++)
            {
                CreateApplication(World.Instance.Shop, new EmployeeEventArgs(World.Instance.Applicants.Herbalists[i]));
            }

            for (int i = 0; i < World.Instance.Applicants.Apothecaries.Count; i++)
            {
                CreateApplication(World.Instance.Shop, new EmployeeEventArgs(World.Instance.Applicants.Apothecaries[i]));
            }

            for (int i = 0; i < World.Instance.Applicants.Shopkeepers.Count; i++)
            {
                CreateApplication(World.Instance.Shop, new EmployeeEventArgs(World.Instance.Applicants.Shopkeepers[i]));
            }

            for (int i = 0; i < World.Instance.Applicants.Guards.Count; i++)
            {
                CreateApplication(World.Instance.Shop, new EmployeeEventArgs(World.Instance.Applicants.Guards[i]));
            }
        }

        void ChangeTitleText(object sender, IntEventArgs e)
        {
            _title.text = string.Format("Applications{0}", e.Value == 0 ? string.Empty : string.Format(" ({0})", e.Value));
        }

        void CreateApplication(object sender, EmployeeEventArgs e)
        {
            var application = Instantiate<ApplicationComponent>(_applicationPrefab);
            application.transform.SetParent(_applicationArea);
            application.applicant = e.Employee;

            _applications.Add(e.Employee, application);
        }

        void RemoveApplication(object sender, EmployeeEventArgs e)
        {
            Destroy(_applications[e.Employee].gameObject);

            _applications.Remove(e.Employee);
        }
    }
}