using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    protected Character character;

    protected bool isRunning;
    public virtual void OnInit(Character character, Vector3 target, float size)
    {
        this.character = character;
        transform.forward = (target - transform.position).normalized;
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public virtual void OnStop()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
        
            Character character = Cache.GetCharacter(other);
            if(character != this.character)
            {
                character.OnHit();
            }
        }
    }
}
