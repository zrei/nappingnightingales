using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    // Start is called before the first frame update

    void OnFire() 
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
