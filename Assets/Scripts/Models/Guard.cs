using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Guard : Employee
    {
        [SerializeField]int _strength;
        [SerializeField]int _kills;
        [SerializeField]int _thievesKilled;
        [SerializeField]int _innocentsKilled;

        public Guard(string name, int salary)
            : base("Guard", name, salary)
        {
            
        }
    }
}