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
    private bool activated = false;

    private SpriteRenderer mySR;
    private Animator animator;
    private new CircleCollider2D collider;

    public Sprite finalState;
    //public Sprite initialDeathState;
    
    private void Awake()
    {
        this.id = currId;
        EnemyController.currId += 1;
        mySR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
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
        if (mySR.sprite == finalState) {
            Destroy(this.gameObject);
        }
        shootCountdown -= Time.deltaTime;
        if (shootCountdown <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDeath")) {
            Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            Instantiate(enemyBullet, transform.position, bulletRotation);
            ResetShootCountdown();
        }
    }

    private void ResetShootCountdown()
    {
        this.shootCountdown = Random.Range(minShootCountdown, maxShootCountdown);
    }

    public void Damage(int damageAmount)
    {
        Debug.Log(damageAmount);
        health -= damageAmount;
        if (health <= 0) {
            //mySR.sprite = initialDeathState;
            animator.SetTrigger("isDead");
            collider.enabled = false;
            this.gameObject.GetComponent<Pathfinding.AIPath>().enabled = false;
            //transform.rotation = new Quaternion(0f, 0f, 0.5f, 1f);
            //transform.localScale = new Vector3(5, 5, 1);
            //while (!animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead")) {
            //    Debug.Log("dying");
            //}
            //Destroy(this.gameObject);
        }
        StartCoroutine("FlashRedOnDamage");
    }

    private IEnumerator FlashRedOnDamage() {
        mySR.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.15f);
        mySR.color = new Color(1, 1, 1, 1);

    }

    private void KillEnemy(int id)
    {
        if (this.id == id)
        {
            Debug.Log("Die");
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead")) {
                Debug.Log("dying");
            }
            Destroy(this.gameObject);
        }
    }

    private void OnBecameVisible() {
        if (!activated) {
            enabled = true;
            this.gameObject.GetComponent<Pathfinding.AIPath>().enabled = true;
            this.gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = 
                GameObject.FindGameObjectWithTag("Player").transform;
            activated = true;
        }
        
    }
    
}
