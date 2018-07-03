using Alchemy.Models;
using UnityEngine;

namespace Alchemy.Controllers {
    public class Logger : MonoBehaviour {
        private void Start() {
            GameManager.Instance.World.SpeedChanged += (sender, e) => {
                Debug.Log(string.Format("Speed changed to {0}", e.value));
            };
            GameManager.Instance.World.HourChanged += (sender, e) => {
                // Debug.Log(string.Format("Hour changed to {0}", e.value));
            };
            GameManager.Instance.World.DayChanged += (sender, e) => {
                Debug.Log(string.Format("Day changed to {0}", e.value));
            };
            GameManager.Instance.World.Shop.GoldChanged += (sender, e) => {
                Debug.Log(string.Format("Gold changed to {0}", e.value));
            };
            GameManager.Instance.World.ApplicantReceived += (sender, e) => {
                Debug.Log(string.Format("{0} the {1} has applied for a job", e.employee.Name, e.employee.Title));
            };
            GameManager.Instance.World.ApplicantDismissed += (sender, e) => {
                Debug.Log(string.Format("Dismissed {0} the {1}", e.employee.Name, e.employee.Title));
            };
            GameManager.Instance.World.ApplicantCountChanged += (sender, e) => {
                Debug.Log(string.Format("Applicant count changed to {0}", e.value));
            };
            GameManager.Instance.World.Shop.EmployeeHired += (sender, e) => {
                Debug.Log(string.Format("Hired {0} the {1}", e.employee.Name, e.employee.Title));
            };
            GameManager.Instance.World.Shop.EmployeeFired += (sender, e) => {
                Debug.Log(string.Format("Fired {0} the {1}", e.employee.Name, e.employee.Title));
            };
            GameManager.Instance.World.Shop.IngredientDelivered += (sender, e) => {
                Debug.Log(string.Format("Delivered some {0} to the shop", e.ingredient.Name));
            };
            GameManager.Instance.World.Shop.IngredientDiscarded += (sender, e) => {
                Debug.Log(string.Format("The shop discarded some {0}", e.ingredient.Name));
            };
            GameManager.Instance.World.FlaskDisplayed += (sender, e) => {
                Debug.Log(string.Format("{0} is now for sale", e.flask.Name));
            };
            GameManager.Instance.World.FlaskSold += (sender, e) => {
                Debug.Log(string.Format("The world sold a {0}", e.flask.Name));
            };
            GameManager.Instance.World.Shop.FlaskBought += (sender, e) => {
                Debug.Log(string.Format("The shop bought a {0}", e.flask.Name));
            };
            GameManager.Instance.World.Shop.FlaskDiscarded += (sender, e) => {
                Debug.Log(string.Format("Lost a {0}", e.flask.Name));
            };
            GameManager.Instance.World.Shop.PotionResearched += (sender, e) => {
                Debug.Log(string.Format("Researched {0}", e.potion.Name));
            };
            GameManager.Instance.World.Shop.PotionCreated += (sender, e) => {
                Debug.Log(string.Format("{0} the {1} has created {2}", e.employee.Name, e.employee.Title, e.potion.Name));
            };
            GameManager.Instance.World.Shop.PotionSold += (sender, e) => {
                Debug.Log(string.Format("{0} the {1} has sold {2} for {3}", e.employee.Name, e.employee.Title, e.potion.Name, e.potion.Value));
            };
        }
    }
}