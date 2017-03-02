﻿using System;
using UnityEngine;

namespace Alchemy.Models
{
	[Serializable]
	public class Guard : Employee
	{
		[SerializeField]
		int strength;
		[SerializeField]
		int kills;
		[SerializeField]
		int thievesKilled;
		[SerializeField]
		int innocentsKilled;

		public Guard(string name, int salary)
			: base("Guard", name, salary)
		{

		}
	}
}