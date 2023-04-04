using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{

    ColorType colorType = ColorType.Green;
    PantType pantType = PantType.Pant_4;
    HatType hatType = HatType.HAT_Headphone;
    WeaponType weaponType = WeaponType.W_Candy_4;
    AccessoryType accessoryType = AccessoryType.ACC_Shield;

    private CounterTimer counter = new CounterTimer();
    public CounterTimer Counter => counter;


    [SerializeField] protected NavMeshAgent agent;
    Vector3 destination;

    IState<Enemy> currentState;

    private bool IsCanRunning => (GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.MainMenu) || GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Finish) );

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

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void OnDeath()
    {
        base.OnDeath();
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
        ChangeColor(colorType);
        ChangePant(pantType);
        ChangeWeapon(weaponType);
        ChangeHat(hatType);
        ChangeAccessory(accessoryType);
    }
}
