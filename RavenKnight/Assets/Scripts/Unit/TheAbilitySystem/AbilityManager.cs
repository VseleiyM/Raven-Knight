using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Project
{
	public class AbilityManager : MonoBehaviour
	{

		public List<Ability> Abilities; // Список способностей


		public AbilityManager()
		{
			Abilities = new List<Ability>();

		}

		// Добавление способности
		public void AddAbility(Ability ability)
		{
			Abilities.Add(ability);
		}

		// Удаление способности
		public void RemoveAbility(Ability ability)
		{
			Abilities.Remove(ability);
		}

		// Активация способности по номеру в списке
		public void ActivateAbility(int index)
		{
			if (index >= 0 && index < Abilities.Count)
			{
				Ability abilityToActivate = Abilities[index];
				if (!abilityToActivate.isCooldown)
				{
					abilityToActivate.Activate();
					StartCoroutine(abilityToActivate.CooldownRoutine());
				}
				else
				{
					Debug.LogWarning($"Ability {index} is on cooldown!");
				}
			}
			else
			{
				Debug.LogWarning("No ability at the specified index!");
			}
		}
	}
}

