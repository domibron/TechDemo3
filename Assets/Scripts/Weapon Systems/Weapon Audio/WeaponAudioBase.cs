using Project.WeaponSystems;
using UnityEngine;

public abstract class WeaponAudioBase : MonoBehaviour
{
	public abstract void Aim();
	public abstract void Fire(bool state);
	public abstract void Reload();
	public abstract void SpecialAction();
	public abstract void UnAim();
}