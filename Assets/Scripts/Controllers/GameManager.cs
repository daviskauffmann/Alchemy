using Alchemy.Models;
using System.IO;
using UnityEngine;

namespace Alchemy.Controllers
{
    public class GameManager : MonoBehaviour
    {
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

		void Update()
        {
            world.Time += world.Speed * Time.deltaTime;
        }
    }
}