using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : GameUnit
{
    public const float TIME_DELAY_THROW = 0.5f;
    public const float ATTACK_RANGE = 5f;
    public const float MAX_SIZE = 4f;
    public const float MIN_SIZE = 1f;

    private string currentAnimName;
    
    public Vector3 targetPoint;

    [SerializeField] GameObject mask;

    public float size = 1f;
    private int score;

    public int Score => score;

    public bool WeaponActive => currentSkin.Weapon.WeaponAcitve;

    public bool IsDead { get; set; }



    [SerializeField] Transform indicatorPoint;
    protected TargetIndicator indicator;


    private List<Character> targets = new List<Character>();
    protected Character target;


    private void Update()
    {
        Debug.Log(GetCharacterInRange());
    }

    public virtual void OnInit()
    {
        IsDead = false;
        score = 0;
        WearClothes();
        ClearTarget();

    }

    public virtual void OnDespawn()
    {
        TakeOffClothes();
    }

    public virtual void OnDeath()
    {
        ChangeAnim(Constant.ANIM_DEAD);
        LevelManager.Ins.CharacterDeath(this);
    }

    public void ResetAnim()
    {
        currentAnimName = "";
    }
    public void OnHit()
    {
        if (!IsDead)
        {
            IsDead = true;
            OnDeath();
        }
    }

    public virtual void OnAttack()
    {
        target = GetCharacterInRange();
        if(WeaponActive && target != null && !target.IsDead)
        {
            targetPoint = target.TF.position;
            TF.LookAt(targetPoint + (TF.position.y - targetPoint.y) * Vector3.up);
            ChangeAnim(Constant.ANIM_ATTACK);
        }
    }

    public void Throw()
    {
        currentSkin.Weapon.Throw(this, targetPoint, size);
    }

    public virtual void AddTarget(Character target)
    {
        targets.Add(target);
    }

    public virtual void RemoveTarget(Character target)
    {
        targets.Remove(target);
        this.target = null;
    }

    public void ClearTarget()
    {
        targets.Clear();
    }

    public Character GetCharacterInRange()
    {
        Character target = null;
        for(int i = 0; i < targets.Count; i++)
        {
            if(targets[i] != null && Vector3.Distance(transform.position, targets[i].transform.position) <= ATTACK_RANGE)
            {
                target = targets[i];
            }
        }
        return target;
    }


    public void ChangeAnim(string animName)
    {
        if(this.currentAnimName != animName)
        {
            currentSkin.Anim.ResetTrigger(this.currentAnimName);
            this.currentAnimName = animName;
            currentSkin.Anim.SetTrigger(this.currentAnimName);
        }
    }

    public void SetMask(bool active)
    {
        mask.SetActive(active);
    }

    public void AddScore(int amount = 1)
    {
        SetScore(score + amount);
    }


    public void SetScore(int score)
    {
        this.score = score > 0 ? score : 0;
        SetSize(1 + this.score * 0.1f);
    }

    public virtual void SetSize(float size)
    {
        size = Mathf.Clamp(size, MIN_SIZE, MAX_SIZE);
        this.size = size;
        TF.localScale = size * Vector3.one;
    }



   

    [SerializeField] protected Skin currentSkin;

    public virtual void WearClothes()
    {

    }

    public void TakeOffClothes()
    {
        currentSkin?.OnDespawn();
        SimplePool.Despawn(currentSkin);
    }

    public void ChangeColor(ColorType colorType)
    {
        currentSkin.ChangeColor(colorType);
    }

    public void ChangePant(PantType pantType)
    {
        currentSkin.ChangePant(pantType);
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        currentSkin.ChangeWeapon(weaponType);
    }

    public void ChangeHat(HatType hatType)
    {
        currentSkin.ChangeHat(hatType);
    }

    public void ChangeAccessory(AccessoryType accessoryType)
    {
        currentSkin.ChangeAccessory(accessoryType);
    }

    public void ChangeSkin(SkinType skinType)
    {
        currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
    }


}
