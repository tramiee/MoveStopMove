using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField]
    private VariableJoystick Joystick
    {
        get
        {
            if (variableJoystick == null)
            {
                variableJoystick = FindObjectOfType<VariableJoystick>();
            }
            return variableJoystick;
        }
    }

    public float speed;

    public CounterTimer counterTime = new CounterTimer();
    public float TIME_THROW_DELAY = 1.5f;

    private bool isMoving;

    private bool IsCanAttack;

    public int Coin => Score * 10;

    SkinType skinType = SkinType.SKIN_Normal;
    ColorType colorType = ColorType.Blue;
    PantType pantType = PantType.Pant_1;
    HatType hatType = HatType.HAT_Arrow;
    WeaponType weaponType = WeaponType.W_Candy_1;
    AccessoryType accessoryType = AccessoryType.ACC_Book;

    private float time = 0;
    private float times = 1.5f;


    public override void OnInit()
    {
        base.OnInit();
        TF.rotation = Quaternion.Euler(Vector3.up * 180);
        SetSize(MIN_SIZE);

    }
    private void FixedUpdate()
    {
        if (GameManager.Ins.IsState(GameState.GamePlay))
        {
            if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
            {
                IsCanAttack = false;
                rb.velocity = new Vector3(Joystick.Horizontal * speed * Time.fixedDeltaTime, rb.velocity.y, Joystick.Vertical * speed * Time.fixedDeltaTime);
                rb.rotation = Quaternion.LookRotation(rb.velocity);
                ChangeAnim(Constant.ANIM_RUN);
                isMoving = true;
            }
            else
            {
                isMoving = false;
                OnStopMove();
            }

            if (IsCanAttack)
            {
                OnAttack();
                ChangeAnim(Constant.ANIM_ATTACK);
            }
        }
      
        
    }

    public void OnStopMove()
    {
        if (IsCanAttack) return;

        rb.velocity = Vector3.zero;
        ChangeAnim(Constant.ANIM_IDLE);

        // tìm enemy lien tục tring này
        // tìm đc thì iscanattack = true;
        target = GetCharacterInRange();
        if(target != null && !target.IsDead)
        {
            IsCanAttack = true;
            
        }
        else
        {
            IsCanAttack = false;
        }
    }

    public override void OnAttack()
    {
        base.OnAttack();
        if(IsCanAttack && currentSkin.Weapon.WeaponAcitve)
        {
            Throw();
            IsCanAttack = false;

        }

    }

    public override void AddTarget(Character target)
    {
        base.AddTarget(target);
        if (!target.IsDead)
        {
            target.SetMask(true);
        }
    }

    public override void RemoveTarget(Character target)
    {
        base.RemoveTarget(target);
        target.SetMask(false);
    }

    public void OnRevive()
    {
        ChangeAnim(Constant.ANIM_IDLE);
        IsDead = false;
        ClearTarget();
    }

    public override void SetSize(float size)
    {
        base.SetSize(size);
        CameraFollow.Ins.SetRateOffset((this.size - MIN_SIZE) / (MAX_SIZE - MIN_SIZE));
    }


    public override void WearClothes()
    {
        base.WearClothes();
        ChangeSkin(skinType);
        ChangeColor(colorType);
        ChangePant(pantType);
        ChangeWeapon(weaponType);
        ChangeHat(hatType);
        ChangeAccessory(accessoryType);
    }

    public void TryClothes(UIShop.ShopType shopType, Enum type)
    {
        switch (shopType)
        {
            case UIShop.ShopType.Pant:
                ChangePant((PantType)type);
                break;
            case UIShop.ShopType.Hat:
                currentSkin.DespawnHat();
                ChangeHat((HatType)type);
                break;
            case UIShop.ShopType.Accessory:
                currentSkin.DespawnAccessory();
                ChangeAccessory((AccessoryType)type);
                break;
            case UIShop.ShopType.Color:
                ChangeColor((ColorType)type);
                break;
            case UIShop.ShopType.Weapon:
                currentSkin.DespawnWeapon();
                ChangeWeapon((WeaponType)type);
                break;
            default:
                break;
        }
    }

    //take cloth from data
    internal void OnTakeClothsData()
    {
        // take old cloth data
        colorType = UserData.Ins.playerColor;
        weaponType = UserData.Ins.playerWeapon;
        hatType = UserData.Ins.playerHat;
        accessoryType = UserData.Ins.playerAccessory;
        pantType = UserData.Ins.playerPant;
    }
}
