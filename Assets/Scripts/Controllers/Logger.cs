﻿using Alchemy.Models;
using UnityEngine;

namespace Alchemy.Controllers
{
    public class Logger : MonoBehaviour
    {
		void Start()
        {
#if false
			GameManager.instance.world.SpeedChanged += (sender, e) =>
			{
				Debug.Log(string.Format("Speed changed to {0}", e.value));
			};
            GameManager.instance.world.HourChanged += (sender, e) =>
			{
				Debug.Log(string.Format("Hour changed to {0}", e.value));
			};
            GameManager.instance.world.DayChanged += (sender, e) =>
			{
				Debug.Log(string.Format("Day changed to {0}", e.value));
			};
            GameManager.instance.world.Shop.GoldChanged += (sender, e) =>
			{
				Debug.Log(string.Format("Gold changed to {0}", e.value));
			};
            GameManager.instance.world.Applicants.CountChanged += (sender, e) =>
			{
				Debug.Log(string.Format("Applicants count changed to {0}", e.value));
			};
            GameManager.instance.world.ApplicantReceived += (sender, e) =>
			{
				Debug.Log(string.Format("{0} the {1} has applied for a job", e.employee.Name, e.employee.Title));
			};
            GameManager.instance.world.ApplicantDismissed += (sender, e) =>
			{
				Debug.Log(string.Format("Dismissed {0} the {1}", e.employee.Name, e.employee.Title));
			};
            GameManager.instance.world.Shop.EmployeeHired += (sender, e) =>
			{
				Debug.Log(string.Format("Hired {0} the {1}", e.employee.Name, e.employee.Title));
			};
            GameManager.instance.world.Shop.EmployeeFired += (sender, e) =>
			{
				Debug.Log(string.Format("Fired {0} the {1}", e.employee.Name, e.employee.Title));
			};
            GameManager.instance.world.Shop.Ingredients.HerbAdded += (sender, e) =>
			{
				Debug.Log(string.Format("Found some {0}", e.herb.Name));
			};
            GameManager.instance.world.Shop.Ingredients.HerbRemoved += (sender, e) =>
			{
				Debug.Log(string.Format("Lost some {0}", e.herb.Name));
			};
			GameManager.instance.world.FlaskDisplayed += (sender, e) =>
			{
				Debug.Log(string.Format("{0} is now for sale", e.flask.Name));
			};
			GameManager.instance.world.FlaskSold += (sender, e) =>
			{
				Debug.Log(string.Format("The world sold a {0}", e.flask.Name));
			};
			GameManager.instance.world.Shop.FlaskBought += (sender, e) =>
			{
				Debug.Log(string.Format("The shop bought a {0}", e.flask.Name));
			};
            GameManager.instance.world.Shop.FlaskDiscarded += (sender, e) =>
			{
				Debug.Log(string.Format("Lost a {0}", e.flask.Name));
			};
            GameManager.instance.world.Shop.EffectDiscovered += (sender, e) =>
			{
				Debug.Log(string.Format("Discovered {0} on {1}", e.effect.Name, e.ingredient.Name));
			};
            GameManager.instance.world.Shop.PotionResearched += (sender, e) =>
			{
				Debug.Log(string.Format("Researched {0}", e.potion.Name));
			};
            GameManager.instance.world.Shop.PotionCreated += (sender, e) =>
			{
				Debug.Log(string.Format("{0} the {1} has created {2}", e.employee.Name, e.employee.Title, e.potion.Name));
			};
            GameManager.instance.world.Shop.PotionSold += (sender, e) =>
			{
				Debug.Log(string.Format("{0} the {1} has sold {2} for {3}", e.employee.Name, e.employee.Title, e.potion.Name, e.potion.Value));
			};
#endif
		}
	}
}