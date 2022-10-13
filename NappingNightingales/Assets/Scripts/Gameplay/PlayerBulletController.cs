using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BulletController
{

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        base.OnTriggerEnter2D(otherObject);
        if (otherObject.gameObject.CompareTag("Enemy"))
        {
            BulletController.DestroyBullet(this.gameObject);
        }
    }
}
