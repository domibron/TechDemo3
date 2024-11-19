using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.WeaponSystems
{
    public interface IProjectileHitLogic
    {
        public bool HitThisObject(GameObject objectThatWasHit, float damageToObject);
    }
}
