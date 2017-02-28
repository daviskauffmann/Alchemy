using Alchemy.Models;
using System.IO;
using UnityEngine;

namespace Alchemy.Controllers
{
    public class GameManager : MonoBehaviour
    {
		public static GameManager instance;

		public World world;
		
        void Awake()
        {
			instance = this;

            if (File.Exists("game.json"))
            {
				world = JsonUtility.FromJson<World>(File.ReadAllText("game.json"));
				for (int i = 0; i < world.Applicants.Total.Length; i++)
				{
					world.Applicants.Total[i].World = world;
				}
				for (int i = 0; i < world.Shop.Employees.Total.Length; i++)
				{
					world.Shop.Employees.Total[i].World = world;
					world.Shop.Employees.Total[i].StartWorking();
				}
			}
			else
            {
                world = new World();
            }
        }

		void Start()
		{
			world.DayChanged += (sender, e) =>
			{
				var alert = UserInterface.CreateAlert(new AlertData()
				{
					title = "Notice",
					message = "Day changed to " + e.value + "\n" +
					"You have " + world.Shop.Gold + " gold",
				});
				alert.AddButton(new ButtonData()
				{
					text = "Ok",
					onClick = () =>
					{
						alert.onClose.Invoke(alert);
					}
				});
			};
		}

		void Update()
        {
            world.Time += world.Speed * Time.deltaTime;
        }
    }
}