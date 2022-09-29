using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public GameObject projectile;

    #endregion

    #region Methods

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        castCollisions = new List<RaycastHit2D>();
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() 
    {
        //need a little offset for the position
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        bullet.GetComponent<SpriteRenderer>().flipX = mySR.flipX;
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

        }
    }

    #endregion
}
