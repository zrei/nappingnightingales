using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Fields

    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;
    private Transform transform;

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
    
    #endregion

    #region Methods

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        EventManager.current.SpawnBullet(transform.position, bulletRotation);
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
