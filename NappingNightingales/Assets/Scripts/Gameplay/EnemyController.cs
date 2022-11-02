using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static int currId = 0;
    private int id;
    private GameObject player;
    [SerializeField] private int health;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float minShootCountdown;
    [SerializeField] private float maxShootCountdown;
    private float shootCountdown;
   
    private void Awake()
    {
        this.id = currId;
        EnemyController.currId += 1;
    }

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.current.onKillEnemy += KillEnemy;
        ResetShootCountdown();
    }

    // Update is called once per frame
    private void Update()
    {
        shootCountdown -= Time.deltaTime;
        if (shootCountdown <= 0) {
            Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            Instantiate(enemyBullet, transform.position, bulletRotation);
            ResetShootCountdown();
        }
    }

    private void ResetShootCountdown()
    {
        this.shootCountdown = Random.Range(minShootCountdown, maxShootCountdown);
    }

    public void Damage()
    {
        health--;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    private void KillEnemy(int id)
    {
        if (this.id == id)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnBecameVisible() {
        enabled = true;
        this.gameObject.GetComponent<Pathfinding.AIPath>().enabled = true;
        this.gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = 
            GameObject.FindGameObjectWithTag("Player").transform;
    }
    
}
