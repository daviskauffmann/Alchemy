using UnityEngine;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField]
        Text _speed = null;
        [SerializeField]
        Text _day = null;
        [SerializeField]
        Text _hour = null;
        [SerializeField]
        Text _gold = null;
        [SerializeField]
        Text _applications = null;
        [SerializeField]
        Text _messagePrefab = null;
        [SerializeField]
        Transform _messageArea = null;
        [SerializeField]
        ScrollRect _messageScrollRect = null;

        void Start()
        {
            GameManager.World.SpeedChanged += ChangeSpeedText;
            GameManager.World.HourChanged += ChangeHourText;
            GameManager.World.DayChanged += ChangeDayText;
            GameManager.World.FlaskDisplayed += LogFlaskDisplayed;
            GameManager.World.FlaskSold += LogFlaskSold;
            GameManager.World.Shop.GoldChanged += ChangeGoldText;
            GameManager.World.Applicants.CountChanged += ChangeApplicationText;
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

            ChangeSpeedText(GameManager.World, new IntEventArgs(GameManager.World.Speed));
            ChangeHourText(GameManager.World, new IntEventArgs(GameManager.World.Hour));
            ChangeDayText(GameManager.World, new IntEventArgs(GameManager.World.Day));
            ChangeGoldText(GameManager.World.Shop, new FloatEventArgs(GameManager.World.Shop.Gold));
            ChangeApplicationText(GameManager.World.Applicants, new IntEventArgs(GameManager.World.Applicants.Total.Length));
        }

        void Log(string value) //FIXME
        {
            bool scrollDown = false;
            if (_messageScrollRect.verticalNormalizedPosition == 0)
            {
                scrollDown = true;
            }
            var message = Instantiate<Text>(_messagePrefab);
            message.transform.SetParent(_messageArea);
            message.text = string.Format("[Day: {0} Hour: {1}] {2}", GameManager.World.Day, GameManager.World.Hour, value);
            if (scrollDown)
            {
                Canvas.ForceUpdateCanvases();
                _messageScrollRect.verticalNormalizedPosition = 0;
            }
            Destroy(message, 30);
        }

        void ChangeSpeedText(object sender, IntEventArgs e)
        {
            _speed.text = string.Format("Speed: {0}", e.Value == 0 ? "PAUSED" : e.Value.ToString());
        }

        void ChangeHourText(object sender, IntEventArgs e)
        {
            _hour.text = string.Format("Hour: {0}", e.Value);
        }

        void ChangeDayText(object sender, IntEventArgs e)
        {
            _day.text = string.Format("Day: {0}", e.Value);
        }

        void ChangeGoldText(object sender, FloatEventArgs e)
        {
            _gold.text = string.Format("Gold: {0}", (int)e.Value);
        }

        void ChangeApplicationText(object sender, IntEventArgs e)
        {
            _applications.text = string.Format("Applications {0}", e.Value == 0 ? string.Empty : string.Format("({0})", e.Value));
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