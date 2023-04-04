using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_RUN = "Run";
    public const string ANIM_ATTACK = "Attack";
    public const string ANIM_DEAD = "Dead";
    public const string ANIM_DANCE = "Dance";
    public const string TAG_CHARACTER = "Character";
}

public enum ColorType
{
    Red, 
    Yellow, 
    Pink, 
    Blue, 
    Green, 
    Violet
}

public enum PantType
{
    Pant_1, 
    Pant_2, 
    Pant_3, 
    Pant_4, 
    Pant_5, 
    Pant_6, 
    Pant_7, 
    Pant_8, 
    Pant_9
}

public enum WeaponType
{
    W_Hammer_1 = PoolType.W_Hammer_1,
    W_Hammer_2 = PoolType.W_Hammer_2,
    W_Hammer_3 = PoolType.W_Hammer_3,
    W_Hammer_4 = PoolType.W_Candy_4,
    W_Candy_1 = PoolType.W_Candy_1,
    W_Candy_2 = PoolType.W_Candy_2,
    W_Candy_3 = PoolType.W_Candy_3,
    W_Candy_4 = PoolType.W_Candy_4,
}

public enum BulletType
{
    B_Hammer_1 = PoolType.B_Hammer_1,
    B_Hammer_2 = PoolType.B_Hammer_2,
    B_Hammer_3 = PoolType.B_Hammer_3,
    B_Hammer_4 = PoolType.B_Hammer_4,
    B_Candy_1 = PoolType.B_Candy_1,
    B_Candy_2 = PoolType.B_Candy_2,
    B_Candy_3 = PoolType.B_Candy_3,
    B_Candy_4 = PoolType.B_Candy_4,
}

public enum HatType
{
    HAT_None = 0,
    HAT_Arrow = PoolType.HAT_Arrow,
    HAT_Cap = PoolType.HAT_Cap,
    HAT_Cowboy = PoolType.HAT_Cowboy,
    HAT_Crown = PoolType.HAT_Crown,
    HAT_Ear = PoolType.HAT_Ear,
    HAT_StrawHat = PoolType.HAT_StrawHat,
    HAT_Headphone = PoolType.HAT_Headphone,
    HAT_Horn = PoolType.HAT_Horn,
    HAT_Police = PoolType.HAT_Police,
}

public enum AccessoryType
{
    ACC_None = 0,
    ACC_Book = PoolType.ACC_Book,
    ACC_Captain = PoolType.ACC_Captain,
    ACC_Headphone = PoolType.ACC_Headphone,
    ACC_Shield = PoolType.ACC_Shield,
}
