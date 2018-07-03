using Alchemy.Models;
using System.IO;
using UnityEngine;

namespace Alchemy.Controllers {
    public class GameManager : MonoBehaviour {
        private static GameManager instance;

        [SerializeField]
        private World world;

        public static GameManager Instance {
            get { return instance; }
        }

        public World World {
            get { return world; }
        }

        private void Awake() {
            instance = this;

            if (File.Exists("game.json")) {
                world = JsonUtility.FromJson<World>(File.ReadAllText("game.json"));
            } else {
                world = new World();
            }
        }

        private void Update() {
            World.Time += World.Speed * Time.deltaTime;
        }
    }
}