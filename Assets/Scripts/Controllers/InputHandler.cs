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
                GameManager.World.Speed += 1;
            }

            if (Input.GetButtonDown("Decrease Game Speed"))
            {
                GameManager.World.Speed -= 1;
            }

            if (Input.GetButtonDown("Jump") && EventSystem.current.currentSelectedGameObject == null)
            {
                if (GameManager.World.Speed != 0)
                {
                    _previousGameSpeed = GameManager.World.Speed;
                    GameManager.World.Speed = 0;
                }
                else
                {
                    GameManager.World.Speed = _previousGameSpeed;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                for (int i = 0; i < 100; i++)
                {
                    var herb = GameManager.World.HerbDatabase[GameManager.World.Random.Next(GameManager.World.HerbDatabase.Length)];
                    GameManager.World.Shop.DeliverIngredient(herb);
                }
                for (int i = 0; i < 100; i++)
                {
                    var flask = (Flask)GameManager.World.FlaskDatabase[GameManager.World.Random.Next(GameManager.World.FlaskDatabase.Length)].Clone();
                    GameManager.World.Shop.PurchaseFlask(flask);
                }
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                File.WriteAllText("game.json", JsonUtility.ToJson(GameManager.World));
            }
        }
    }
}