using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : Bullet
{
    public float speed = 7f;
    CounterTimer counterTimer = new CounterTimer();

    public override void OnInit(Character character, Vector3 target, float size)
    {
        base.OnInit(character, target, size);
        counterTimer.Start(OnDespawn, 1f );
    }
    private void Update()
    {
        counterTimer.Execute();
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    public override void OnStop()
    {
        base.OnStop();
    }
}
