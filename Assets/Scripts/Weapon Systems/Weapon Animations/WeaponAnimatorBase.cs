using UnityEngine;

public abstract class WeaponAnimatorBase : MonoBehaviour
{
	public abstract void Aim(bool state);
	public abstract void Fire(bool state);

	public abstract void Reload();
	public abstract void SpecialAction();
}