using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    private float TIME_WEAPON_RELOAD = 0.5f;
    [SerializeField] GameObject weaponObj;
    [SerializeField] BulletType bulletType;

    public bool WeaponAcitve => weaponObj.activeSelf;

    public void SetActive(bool active)
    {
        weaponObj.SetActive(active);
    }

    public void OnEnable()
    {
        SetActive(true);
    }

    public void Throw(Character character, Vector3 target, float size)
    {
        Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, transform.position, Quaternion.identity);
        bullet.OnInit(character, target, size);
        SetActive(false);
        //Invoke(nameof(OnEnable),TIME_WEAPON_RELOAD);
    }
    
}
