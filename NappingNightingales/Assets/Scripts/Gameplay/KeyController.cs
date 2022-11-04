using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private GateAndKeyController parentScript;

    private void Awake()
    {
        parentScript = transform.parent.gameObject.GetComponent<GateAndKeyController>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Player"))
        {
            parentScript.KeyObtain();
            Destroy(this.gameObject);
        }
    }
}