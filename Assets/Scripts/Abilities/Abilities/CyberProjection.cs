using UnityEngine;

public class CyberProjection : AbilityBase
{
	public float Distance = 10f;

	private CharacterController _characterController;

	void Start()
	{
		_characterController = GetComponent<CharacterController>();
	}

	public override void ActivateAbility()
	{
		if (_currentCoolDownTime > 0f) return;

		_characterController.Move(transform.forward * Distance);


		_currentCoolDownTime = AbilitySO.CoolDown;
	}

	void Update()
	{
		if (_currentCoolDownTime > 0f) _currentCoolDownTime -= Time.deltaTime;
	}
}