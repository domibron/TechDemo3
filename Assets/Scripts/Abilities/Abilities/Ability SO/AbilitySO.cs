using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilitySO")]
public class AbilitySO : ScriptableObject
{
	public String Name;
	public Sprite Icon;
	public float CoolDown = 5f;

	public (String, Sprite) GetInfo()
	{
		return (Name, Icon);
	}
}