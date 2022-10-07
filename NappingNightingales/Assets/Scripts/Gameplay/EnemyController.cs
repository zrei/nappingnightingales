using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static int currId = 0;
    private int id;
   
    private void Awake()
    {
        this.id = currId;
        EnemyController.currId += 1;
    }

    // Start is called before the first frame update
    private void Start()
    {
        EventManager.current.onKillEnemy += KillEnemy;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Bullet"))
        {
           EventManager.current.KillEnemy(this.id);
            //call some method from the enemy controller
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void KillEnemy(int id)
    {
        if (this.id == id)
        {
            Destroy(this.gameObject);
        }
    }
    
}
