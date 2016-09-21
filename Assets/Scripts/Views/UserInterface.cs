using System;
using UnityEngine;
using UnityEngine.UI;
using Alchemy.Models;

namespace Alchemy.Views
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField]Text _speed = null;
        [SerializeField]Text _day = null;
        [SerializeField]Text _hour = null;
        [SerializeField]Text _gold = null;
        [SerializeField]Text _applications = null;
        [SerializeField]Text _messagePrefab = null;
        [SerializeField]Transform _messageArea = null;
        [SerializeField]ScrollRect _messageScrollRect = null;

        void Start()
        {
            World.Instance.SpeedChanged += ChangeSpeedText;
            World.Instance.HourChanged += ChangeHourText;
            World.Instance.DayChanged += ChangeDayText;
            World.Instance.FlaskDisplayed += LogFlaskDisplayed;
            World.Instance.FlaskSold += LogFlaskSold;
            World.Instance.Shop.GoldChanged += ChangeGoldText;
            World.Instance.Applicants.CountChanged += ChangeApplicationText;
            World.Instance.ApplicantReceived += LogApplicantReceived;
            World.Instance.ApplicantDismissed += LogApplicantDismissed;
            World.Instance.Shop.EmployeeHired += LogEmployeeHired;
            World.Instance.Shop.EmployeeFired += LogEmployeeFired;
            World.Instance.Shop.Ingredients.HerbAdded += LogHerbFound;
            World.Instance.Shop.Ingredients.HerbRemoved += LogHerbDiscarded;
            World.Instance.Shop.FlaskBought += LogFlaskBought;
            World.Instance.Shop.FlaskDiscarded += LogFlaskDiscarded;
            World.Instance.Shop.EffectDiscovered += LogEffectDiscovered;
            World.Instance.Shop.PotionResearched += LogPotionResearched;
            World.Instance.Shop.PotionCreated += LogPotionCreated;
            World.Instance.Shop.PotionSold += LogPotionSold;

            ChangeSpeedText(World.Instance, new IntEventArgs(World.Instance.Speed));
            ChangeHourText(World.Instance, new IntEventArgs(World.Instance.Hour));
            ChangeDayText(World.Instance, new IntEventArgs(World.Instance.Day));
            ChangeGoldText(World.Instance.Shop, new FloatEventArgs(World.Instance.Shop.Gold));
            ChangeApplicationText(World.Instance.Applicants, new IntEventArgs(World.Instance.Applicants.Count));
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
            message.text = string.Format("[Day: {0} Hour: {1}] {2}", World.Instance.Day, World.Instance.Hour, value);

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