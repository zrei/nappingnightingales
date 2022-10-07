using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Fields

    private Transform transform;
    
    [SerializeField]
    private float bulletSpeed = 5.5f;

    private static int currId = 0;
    private int id;
    #endregion

    #region Methods

    private void Awake() 
    {
        transform = GetComponent<Transform>();
        id = BulletController.currId;
        BulletController.currId += 1;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        EventManager.current.onCollideWall += CollideWall;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
    }

    //can be overridden
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Wall"))
        {
            EventManager.current.CollideWall(this.id);
        } else if (otherObject.gameObject.CompareTag("Enemy"))
        {
            //call some method from the enemy controller
        }
    }

    //can be overridden
    private void CollideWall(int bulletId) 
    {
        if (this.id == bulletId) {
            Destroy(this.gameObject);
        }
    }

    #endregion
}
