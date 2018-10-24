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
            get { return this.world; }
        }

        private void Awake() {
            instance = this;

            if (File.Exists("game.json")) {
                this.world = JsonUtility.FromJson<World>(File.ReadAllText("game.json"));
            } else {
                this.world = new World();
            }
        }

        private void Start() {
            this.World.Start();
        }

        private void Update() {
            this.World.Time += this.World.Speed * Time.deltaTime;
        }
    }
}
