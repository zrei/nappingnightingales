using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float bulletSpeed = 5.5f;

    protected int baseDamage = 1;

    private static int currId = 0;
    private int id;

    #endregion

    #region Methods

    private void Awake() 
    {
        this.id = BulletController.currId;
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
    protected void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Wall"))
        {
            EventManager.current.CollideWall(this.id);
        }
    }

    public void ContactFire()
    {

    }

    public int GetDamage() {
        return this.baseDamage;
    }

    //can be overridden
    private void CollideWall(int bulletId) 
    {
        if (this.id == bulletId) {
            BulletController.DestroyBullet(this.gameObject);
        }
    }

    //TODO: delete this method after adding level walls
    private void OnBecameInvisible()
    {
        DestroyBullet(this.gameObject);
    }

    public static void DestroyBullet(GameObject bullet) {
        Destroy(bullet);
    }

    #endregion
}
