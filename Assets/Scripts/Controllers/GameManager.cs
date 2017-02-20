using System.IO;
using UnityEngine;
using Alchemy.Models;

namespace Alchemy.Controllers
{
    public class GameManager : MonoBehaviour
    {
		static World _world;

		public static World World
		{
			get { return _world; }
		}

        void Awake()
        {
            if (File.Exists("game.json"))
            {
                _world = JsonUtility.FromJson<World>(File.ReadAllText("game.json"));
            }
            else
            {
                _world = new World();
            }
        }

        void Start()
        {
            for (int i = 0; i < _world.Shop.Employees.Total.Length; i++)
            {
				_world.Shop.Employees.Total[i].World = _world;
				_world.Shop.Employees.Total[i].StartWorking();
            }
        }

        void Update()
        {
            _world.Time += _world.Speed * Time.deltaTime;
        }
    }
}