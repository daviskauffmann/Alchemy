using System;
using System.Collections.Generic;
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
            for (int i = 0; i < World.Instance.Shop.Employees.Herbalists.Count; i++)
            {
                World.Instance.Shop.Employees.Herbalists[i].StartWorking();
            }

            for (int i = 0; i < World.Instance.Shop.Employees.Apothecaries.Count; i++)
            {
                World.Instance.Shop.Employees.Apothecaries[i].StartWorking();
            }

            for (int i = 0; i < World.Instance.Shop.Employees.Shopkeepers.Count; i++)
            {
                World.Instance.Shop.Employees.Shopkeepers[i].StartWorking();
            }

            for (int i = 0; i < World.Instance.Shop.Employees.Guards.Count; i++)
            {
                World.Instance.Shop.Employees.Guards[i].StartWorking();
            }
        }

        void Update()
        {
            _world.Time += _world.Speed * Time.deltaTime;
        }
    }
}