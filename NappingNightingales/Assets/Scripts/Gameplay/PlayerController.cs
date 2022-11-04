using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Fields

    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;

    // Movement support
    private Vector2 movementInput;
    [SerializeField]
    private ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // TODO: change to const
    [SerializeField]
    private float movementSpeed = 3f;
    [SerializeField]
    private float collisionOffset = 0.05f;
    
    [SerializeField] 
    private int health;
    [SerializeField] 
    private float bulletCooldown = 3f;
    private float shootCountdown = 0f;
    private bool allowFire = true;

    // animation
    private Animator animator;

    #endregion

    #region Methods

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        castCollisions = new List<RaycastHit2D>();
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            shootCountdown -= Time.deltaTime;
        }
        if (shootCountdown <= 0 && !allowFire) {
            ResetBulletCountdown();
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
        
    }

    void OnFire() 
    {
        if (allowFire && !PauseMenu.GameIsPaused) 
        {
            this.allowFire = false;
            this.shootCountdown = this.bulletCooldown;
            //need a little offset for the position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
            EventManager.current.SpawnBullet(transform.position, bulletRotation);
        }
    }

    private void ResetBulletCountdown() {
        this.allowFire = true;
    }

    void FixedUpdate()  
    {   
        if (movementInput != Vector2.zero)
        
        {
        
            int numCollisions = rb2D.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                movementSpeed * Time.fixedDeltaTime + collisionOffset
                );

            if (numCollisions == 0)
            {
                rb2D.MovePosition(rb2D.position + movementInput * movementSpeed * Time.fixedDeltaTime);
            }

            //check direction
            if (movementInput.x > 0) {
                mySR.flipX = false;
                //transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            } else if (movementInput.x < 0) {
                mySR.flipX = true;
                //transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            //animation paras
            animator.SetFloat("horizontal", movementInput.x);
            animator.SetFloat("vertical", movementInput.y);
            animator.SetBool("moving", true);
        }
        
        else

        {
            animator.SetBool("moving", false);
        }
    }

    public void Damage() {
        //Debug.Log("Ow!");
        EventManager.current.Damage();
        health--;
        if (health <= 0) {
            // Add game over code here
            //Debug.Log("Game Over");
            EventManager.IncrementDeaths();
            Debug.Log(EventManager.GetDeaths());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        StartCoroutine("FlashRedOnDamage");
    }

    private IEnumerator FlashRedOnDamage() {
        mySR.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.15f);
        mySR.color = new Color(1, 1, 1, 1);

    }

    #endregion
}
