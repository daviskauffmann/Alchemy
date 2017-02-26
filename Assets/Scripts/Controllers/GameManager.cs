using System.IO;
using UnityEngine;
using Alchemy.Models;

namespace Alchemy.Controllers
{
    public class GameManager : MonoBehaviour
    {
		public World __world;

		static World world;

		public static World World
		{
			get { return world; }
		}

        void Awake()
        {
            if (File.Exists("game.json"))
            {
				world = JsonUtility.FromJson<World>(File.ReadAllText("game.json"));
			}
			else
            {
                world = new World();
            }
        }

		void Start()
		{
			for (int i = 0; i < world.Shop.Employees.Total.Length; i++)
			{
				world.Shop.Employees.Total[i].World = world;
				world.Shop.Employees.Total[i].StartWorking();
			}

			// DEBUG
			__world = world;
		}

		void Update()
        {
            world.Time += world.Speed * Time.deltaTime;
        }
    }
}