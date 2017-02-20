using System;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Guard : Employee
    {
        [SerializeField]
        int _strength;
        [SerializeField]
        int _kills;
        [SerializeField]
        int _thievesKilled;
        [SerializeField]
        int _innocentsKilled;

        public Guard(World world, string name, int salary)
            : base(world, "Guard", name, salary)
        {
            
        }
    }
}