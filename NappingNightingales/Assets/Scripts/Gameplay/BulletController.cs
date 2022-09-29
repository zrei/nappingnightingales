using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Fields

    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;
    private int initialDir;

    [SerializeField]
    private float bulletSpeed = 5.5f;

    #endregion

    #region Methods

    void Awake() 
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (mySR.flipX) {
            initialDir = -1;
        } else {
            initialDir = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //think of a better way to do this
        rb2D.MovePosition(new Vector2(rb2D.position.x + initialDir * bulletSpeed * Time.fixedDeltaTime, rb2D.position.y));
        /*if (movementInput.x > 0) {
            mySR.flipX = false;
        } else if (movementInput.x < 0) {
            mySR.flipX = true;
        }*/
    }

    #endregion
}
