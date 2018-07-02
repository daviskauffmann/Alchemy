using Alchemy.Models;
using System.IO;
using UnityEngine;

namespace Alchemy.Controllers {
    public class GameManager : MonoBehaviour {
        public static GameManager instance;

        public World world;

        void Awake() {
            instance = this;

            if (File.Exists("game.json")) {
                world = JsonUtility.FromJson<World>(File.ReadAllText("game.json"));
            } else {
                world = new World();
            }
        }

        void Update() {
            world.Time += world.Speed * Time.deltaTime;
        }
    }
}