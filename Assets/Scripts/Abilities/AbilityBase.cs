using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
	public AbilitySO AbilitySO;

	public float _currentCoolDownTime { get; protected set; } = 0f;

	public abstract void ActivateAbility();
}