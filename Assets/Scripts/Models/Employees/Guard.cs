using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Guard : Employee {
        [SerializeField]
        private int strength;
        [SerializeField]
        private int kills;
        [SerializeField]
        private int thievesKilled;
        [SerializeField]
        private int innocentsKilled;

        public Guard(string name, int salary) : base("Guard", name, salary) {

        }
    }
}