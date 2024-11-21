using Project;
using Project.StatusEffects;
using UnityEngine;

public class MindBlastEmission : AbilityBase
{


	public float AngleAsDot = .5f;

	public float Range = 15f;

	public float Duration = 10f;

	public override void ActivateAbility()
	{
		if (_currentCoolDownTime > 0f) return;


		Collider[] colliders = Physics.OverlapSphere(transform.position, Range, StaticData.LAYER_WITH_IGNORED_PLAYER_RELATED_LAYERS);

		foreach (Collider collider in colliders)
		{
			if (Vector3.Dot(collider.transform.position, transform.position) > AngleAsDot)
			{
				collider.GetComponent<IConvertable>()?.Convert(Duration);
			}
		}

		_currentCoolDownTime = AbilitySO.CoolDown;
	}

	void Update()
	{
		if (_currentCoolDownTime > 0f) _currentCoolDownTime -= Time.deltaTime;
	}
}