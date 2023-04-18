using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{

    /*ColorType colorType = ColorType.Green;
    PantType pantType = PantType.Pant_4;
    HatType hatType = HatType.HAT_Headphone;
    WeaponType weaponType = WeaponType.W_Candy_4;
    AccessoryType accessoryType = AccessoryType.ACC_Shield;*/

    SkinType skinType = SkinType.SKIN_Normal;
    private CounterTimer counter = new CounterTimer();
    public CounterTimer Counter => counter;

    [SerializeField] protected NavMeshAgent agent;
    Vector3 destination;

    IState<Enemy> currentState;

    private bool IsCanRunning => (GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Finish) );

    private void Update()
    {
        if (IsCanRunning && currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }

    public bool IsDestination()
    {
        return Vector3.Distance(transform.position, destination) - Mathf.Abs(transform.position.y - destination.y) < 0.1f;
    }

    public void SetPosition(Vector3 point)
    {
        destination = point;
        agent.enabled = true;
        agent.SetDestination(destination);
        ChangeAnim(Constant.ANIM_RUN);
    }

    public void ChangeState(IState<Enemy> newState) 
    {
        
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }


    public void StopMove()
    {
        agent.enabled = false;
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public override void OnInit()
    {
        base.OnInit();
        SetMask(false);
        ResetAnim();
    }

    public override void OnAttack()
    {
        base.OnAttack();
        
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
        CancelInvoke();
    }

    public override void OnDeath()
    {
        ChangeState(null);
        StopMove();
        base.OnDeath();
        SetMask(false);
        Invoke(nameof(OnDespawn), 2f);
    }

    public override void AddTarget(Character target)
    {
        base.AddTarget(target);
        if(!IsDead && IsCanRunning && Utilities.Chance(50, 100))
        {
            ChangeState(new AttackState());
        }
    }

    public override void WearClothes()
    {
        base.WearClothes();
        ChangeSkin(skinType);
        ChangeColor(Utilities.RandomEnumValue<ColorType>());
        ChangePant(Utilities.RandomEnumValue<PantType>());
        ChangeWeapon(Utilities.RandomEnumValue<WeaponType>());
        ChangeHat(Utilities.RandomEnumValue<HatType>());
        ChangeAccessory(Utilities.RandomEnumValue<AccessoryType>());
    }
}
