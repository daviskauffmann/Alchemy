using System;
using UnityEngine;

namespace Alchemy.Controllers
{
    public class UserInterface : MonoBehaviour
    {
        void Start()
        {
            GameManager.World.SpeedChanged += LogSpeedChanged;
            GameManager.World.HourChanged += LogHourChanged;
            GameManager.World.DayChanged += LogDayChanged;
            GameManager.World.FlaskDisplayed += LogFlaskDisplayed;
            GameManager.World.FlaskSold += LogFlaskSold;
            GameManager.World.Shop.GoldChanged += LogGoldChanged;
            GameManager.World.Applicants.CountChanged += LogApplicantsCountChanged;
            GameManager.World.ApplicantReceived += LogApplicantReceived;
            GameManager.World.ApplicantDismissed += LogApplicantDismissed;
            GameManager.World.Shop.EmployeeHired += LogEmployeeHired;
            GameManager.World.Shop.EmployeeFired += LogEmployeeFired;
            GameManager.World.Shop.Ingredients.HerbAdded += LogHerbFound;
            GameManager.World.Shop.Ingredients.HerbRemoved += LogHerbDiscarded;
            GameManager.World.Shop.FlaskBought += LogFlaskBought;
            GameManager.World.Shop.FlaskDiscarded += LogFlaskDiscarded;
            GameManager.World.Shop.EffectDiscovered += LogEffectDiscovered;
            GameManager.World.Shop.PotionResearched += LogPotionResearched;
            GameManager.World.Shop.PotionCreated += LogPotionCreated;
            GameManager.World.Shop.PotionSold += LogPotionSold;
        }

		void LogSpeedChanged(object sender, IntEventArgs e)
		{
			Debug.Log(string.Format("Speed changed to {0}", e.Value));
		}

		void LogHourChanged(object sender, IntEventArgs e)
		{
			Debug.Log(string.Format("Hour changed to {0}", e.Value));
		}

		void LogDayChanged(object sender, IntEventArgs e)
		{
			Debug.Log(string.Format("Day changed to {0}", e.Value));
		}

		void LogGoldChanged(object sender, FloatEventArgs e)
		{
			Debug.Log(string.Format("Gold changed to {0}", e.Value));
		}

		void LogApplicantsCountChanged(object sender, IntEventArgs e)
		{
			Debug.Log(string.Format("Applicants count changed to {0}", e.Value));
		}

		void LogApplicantReceived(object sender, EmployeeEventArgs e)
        {
            Debug.Log(string.Format("{0} the {1} has applied for a job", e.Employee.Name, e.Employee.Title)); 
        }

        void LogApplicantDismissed(object sender, EmployeeEventArgs e)
        {
            Debug.Log(string.Format("Dismissed {0} the {1}", e.Employee.Name, e.Employee.Title));
        }

        void LogEmployeeHired(object sender, EmployeeEventArgs e)
        {
            Debug.Log(string.Format("Hired {0} the {1}", e.Employee.Name, e.Employee.Title));
        }

        void LogEmployeeFired(object sender, EmployeeEventArgs e)
        {
            Debug.Log(string.Format("Fired {0} the {1}", e.Employee.Name, e.Employee.Title));
        }

        void LogHerbFound(object sender, HerbEventArgs e)
        {
            Debug.Log(string.Format("Found some {0}", e.Herb.Name));
        }

        void LogHerbDiscarded(object sender, HerbEventArgs e)
        {
            Debug.Log(string.Format("Lost some {0}", e.Herb.Name));
        }

        void LogFlaskDisplayed(object sender, FlaskEventArgs e)
        {
            Debug.Log(string.Format("{0} is now for sale", e.Flask.Name));
        }

        void LogFlaskSold(object sender, FlaskEventArgs e)
        {
            Debug.Log(string.Format("The world sold a {0}", e.Flask.Name));
        }

        void LogFlaskBought(object sender, FlaskEventArgs e)
        {
            Debug.Log(string.Format("The shop bought a {0}", e.Flask.Name));
        }

        void LogFlaskDiscarded(object sender, FlaskEventArgs e)
        {
            Debug.Log(string.Format("Lost a {0}", e.Flask.Name));
        }

        void LogEffectDiscovered(object sender, EffectEventArgs e)
        {
            Debug.Log(string.Format("Discovered {0} on {1}", e.Effect.Name, e.Ingredient.Name));
        }

        void LogPotionResearched(object sender, PotionEventArgs e)
        {
            Debug.Log(string.Format("Researched {0}", e.Potion.Name));
        }

        void LogPotionCreated(object sender, PotionEventArgs e)
        {
            Debug.Log(string.Format("Apothecary has created {0}", e.Potion.Name));
        }

        void LogPotionSold(object sender, PotionEventArgs e)
        {
            Debug.Log(string.Format("Shopkeeper has sold {0} for {1}", e.Potion.Name, e.Potion.Value));
        }
    }
}