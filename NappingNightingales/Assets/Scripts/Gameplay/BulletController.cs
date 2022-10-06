using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Fields

    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;
    private int initialDir;
    private Transform transform;

    [SerializeField]
    private float bulletSpeed = 5.5f;

    #endregion

    #region Methods

    void Awake() 
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
    }

    #endregion
}
