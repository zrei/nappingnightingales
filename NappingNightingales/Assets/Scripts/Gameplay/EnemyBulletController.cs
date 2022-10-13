using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
    protected new void OnTriggerEnter2D(Collider2D otherObject)
    {
        base.OnTriggerEnter2D(otherObject);
        if (otherObject.gameObject.CompareTag("Player"))
        {
            BulletController.DestroyBullet(this.gameObject);
            otherObject.GetComponent<PlayerController>().Damage();
        }
    }
}
