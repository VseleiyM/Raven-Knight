using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public enum CostType
	{
		Mana,
		Energy,
		Rage
	}
	public enum Groups
	{
		Mana,
		Energy,
		Rage
	}

	[System.Serializable]
	public class Ability 
	{

		public List<Groups> Groups;
		public float Cooldown;
		public int CostAmount;
		public List<CostType> costTypes;

		public List<Action> Actions; 

		public bool isCooldown = false;

		public Ability() 
		{
			Actions = new List<Action>();
		}

		public void Activate()
		{
			if (isCooldown)
			{
				Debug.LogWarning("Ability is on cooldown!");
				return;
			}


			foreach (var action in Actions)
			{
				//action(); 
			}
			
		}

		public IEnumerator CooldownRoutine()
		{
			isCooldown = true;
			yield return new WaitForSeconds(Cooldown);
			isCooldown = false;
		}
	}
}
