using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using Alchemy.Models;

namespace Alchemy.Controllers
{
    public class InputHandler : MonoBehaviour
    {
        int _previousGameSpeed;

        void Update()
        {
            if (Input.GetButtonDown("Increase Game Speed"))
            {
                World.Instance.Speed += 1;
            }

            if (Input.GetButtonDown("Decrease Game Speed"))
            {
                World.Instance.Speed -= 1;
            }

            if (Input.GetButtonDown("Jump") && EventSystem.current.currentSelectedGameObject == null)
            {
                if (World.Instance.Speed != 0)
                {
                    _previousGameSpeed = World.Instance.Speed;

                    World.Instance.Speed = 0;
                }
                else
                {
                    World.Instance.Speed = _previousGameSpeed;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                for (int i = 0; i < 100; i++)
                {
                    var herb = World.Instance.HerbDatabase[World.Instance.Random.Next(World.Instance.HerbDatabase.Length)];
                    World.Instance.Shop.DeliverIngredient(herb);
                }

                for (int i = 0; i < 100; i++)
                {
                    var flask = (Flask)World.Instance.FlaskDatabase[World.Instance.Random.Next(World.Instance.FlaskDatabase.Length)].Clone();
                    World.Instance.Shop.PurchaseFlask(flask);
                }
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                File.WriteAllText("game.json", JsonUtility.ToJson(World.Instance));
            }
        }
    }
}