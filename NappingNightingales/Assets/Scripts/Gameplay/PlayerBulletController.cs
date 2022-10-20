using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BulletController
{
    new void OnTriggerEnter2D(Collider2D otherObject)
    {
        base.OnTriggerEnter2D(otherObject);
        if (otherObject.gameObject.CompareTag("Enemy"))
        {
            otherObject.GetComponent<EnemyController>().Damage();
            BulletController.DestroyBullet(this.gameObject);
        }
    }
}
