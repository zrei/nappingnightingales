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
            otherObject.GetComponent<EnemyController>().Damage(base.GetDamage());
            BulletController.DestroyBullet(this.gameObject);
        }
    }

    public new void ContactFire()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.6f, 0, 1);
        base.baseDamage = 2;
    }
}
