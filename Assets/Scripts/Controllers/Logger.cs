using UnityEngine;

namespace Alchemy.Controllers
{
    public class Logger : MonoBehaviour
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
			Debug.Log(string.Format("Speed changed to {0}", e.value));
		}

		void LogHourChanged(object sender, IntEventArgs e)
		{
			Debug.Log(string.Format("Hour changed to {0}", e.value));
		}

		void LogDayChanged(object sender, IntEventArgs e)
		{
			Debug.Log(string.Format("Day changed to {0}", e.value));
		}

		void LogGoldChanged(object sender, FloatEventArgs e)
		{
			Debug.Log(string.Format("Gold changed to {0}", e.value));
		}

		void LogApplicantsCountChanged(object sender, IntEventArgs e)
		{
			Debug.Log(string.Format("Applicants count changed to {0}", e.value));
		}

		void LogApplicantReceived(object sender, EmployeeEventArgs e)
        {
            Debug.Log(string.Format("{0} the {1} has applied for a job", e.employee.Name, e.employee.Title)); 
        }

        void LogApplicantDismissed(object sender, EmployeeEventArgs e)
        {
            Debug.Log(string.Format("Dismissed {0} the {1}", e.employee.Name, e.employee.Title));
        }

        void LogEmployeeHired(object sender, EmployeeEventArgs e)
        {
            Debug.Log(string.Format("Hired {0} the {1}", e.employee.Name, e.employee.Title));
        }

        void LogEmployeeFired(object sender, EmployeeEventArgs e)
        {
            Debug.Log(string.Format("Fired {0} the {1}", e.employee.Name, e.employee.Title));
        }

        void LogHerbFound(object sender, HerbEventArgs e)
        {
            Debug.Log(string.Format("Found some {0}", e.herb.Name));
        }

        void LogHerbDiscarded(object sender, HerbEventArgs e)
        {
            Debug.Log(string.Format("Lost some {0}", e.herb.Name));
        }

        void LogFlaskDisplayed(object sender, FlaskEventArgs e)
        {
            Debug.Log(string.Format("{0} is now for sale", e.flask.Name));
        }

        void LogFlaskSold(object sender, FlaskEventArgs e)
        {
            Debug.Log(string.Format("The world sold a {0}", e.flask.Name));
        }

        void LogFlaskBought(object sender, FlaskEventArgs e)
        {
            Debug.Log(string.Format("The shop bought a {0}", e.flask.Name));
        }

        void LogFlaskDiscarded(object sender, FlaskEventArgs e)
        {
            Debug.Log(string.Format("Lost a {0}", e.flask.Name));
        }

        void LogEffectDiscovered(object sender, EffectEventArgs e)
        {
            Debug.Log(string.Format("Discovered {0} on {1}", e.effect.Name, e.ingredient.Name));
        }

        void LogPotionResearched(object sender, PotionEventArgs e)
        {
            Debug.Log(string.Format("Researched {0}", e.potion.Name));
        }

        void LogPotionCreated(object sender, PotionEventArgs e)
        {
            Debug.Log(string.Format("{0} the {1} has created {2}", e.employee.Name, e.employee.Title, e.potion.Name));
        }

        void LogPotionSold(object sender, PotionEventArgs e)
        {
            Debug.Log(string.Format("{0} the {1} has sold {2} for {3}", e.employee.Name, e.employee.Title, e.potion.Name, e.potion.Value));
        }
    }
}