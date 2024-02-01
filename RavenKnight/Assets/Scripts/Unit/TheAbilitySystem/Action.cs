using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public enum Types
	{
		Mana,
		Energy,
		Rage
	}

	[System.Serializable]
	public class Action
    {
		public List<Groups> Groups;
		public List<Types> Type; 



	}
}

