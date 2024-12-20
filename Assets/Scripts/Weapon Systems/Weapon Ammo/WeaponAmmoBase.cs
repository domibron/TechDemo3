using UnityEngine;

public abstract class WeaponAmmoBase : MonoBehaviour
{
    public abstract string AmmoString { get; }

    public abstract bool HasAmmo();

    public abstract bool Reload();

    public abstract void ResetAllAmmo();

    public abstract void StartReducingAmmo();

    public abstract void StopReducingAmmo();
}