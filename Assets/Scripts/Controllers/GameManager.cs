using System.IO;
using UnityEngine;
using Alchemy.Models;

namespace Alchemy.Controllers
{
    public class GameManager : MonoBehaviour
    {
        World _world;

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
            for (int i = 0; i < World.Instance.Shop.Employees.Total.Length; i++)
            {
                World.Instance.Shop.Employees.Total[i].StartWorking();
            }
        }

        void Update()
        {
            _world.Time += _world.Speed * Time.deltaTime;
        }
    }
}