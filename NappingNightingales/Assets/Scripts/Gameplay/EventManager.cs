using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;
    public GameObject playerBullet;

    void Awake() 
    {
        current = this;
    }

    void Start()
    {

    }
        
    public void SpawnBullet(Vector3 position, Quaternion rotation) { //can overload this later
        Instantiate(playerBullet, position, rotation);
    }

    public event Action<int> onCollideWall; //needs to be set in the class
    //if you want different behaviours of the coliision for different bullets you can have different classes
    //inherit from the bullet controller and override the collision method
    public void CollideWall(int id) 
    {
        if (onCollideWall != null) {
            onCollideWall(id);
        }
    }

    public event Action<int> onKillEnemy;
    public void KillEnemy(int id)
    {
        if (onKillEnemy != null) {
            onKillEnemy(id);
        }
    }

}
