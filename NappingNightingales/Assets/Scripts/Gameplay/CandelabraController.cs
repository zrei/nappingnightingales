using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelabraController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Bullet")) {
            //Debug.Log("Hit Fire");
            otherObject.gameObject.GetComponent<PlayerBulletController>().ContactFire();
            //Debug.Log(otherObject.gameObject.GetComponent<PlayerBulletController>().GetDamage());
        }
    }
}
