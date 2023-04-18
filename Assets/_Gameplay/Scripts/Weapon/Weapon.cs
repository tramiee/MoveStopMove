using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    private float TIME_WEAPON_RELOAD = 2f;
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

    public void Update()
    {
    }

    public void Throw(Character character, Vector3 target, float size)
    {
        Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, TF.position, Quaternion.identity);
        bullet.OnInit(character, target, size);
        bullet.TF.localScale = size * Vector3.one;
        SetActive(false);
        Invoke(nameof(OnEnable), TIME_WEAPON_RELOAD);
    }
    
}
