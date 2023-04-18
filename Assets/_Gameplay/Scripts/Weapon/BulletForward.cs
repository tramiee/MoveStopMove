using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : Bullet
{
    public const float TIME_ALIVE = 1f;
    public float speed = 7f;
    CounterTimer counterTimer = new CounterTimer();

    public override void OnInit(Character character, Vector3 target, float size)
    {
        base.OnInit(character, target, size);
        counterTimer.Start(OnDespawn, TIME_ALIVE * size);
    }
    private void Update()
    {
        counterTimer.Execute();
        TF.Translate(TF.forward * speed * Time.deltaTime, Space.World);

    }

    public override void OnStop()
    {
        base.OnStop();
    }
}
