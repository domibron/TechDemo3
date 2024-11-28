using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
    public abstract class ProjectileHitLogicBase : MonoBehaviour
    {
        public abstract bool HitThisObject(GameObject objectThatWasHit, float damageToObject);
    }
}
