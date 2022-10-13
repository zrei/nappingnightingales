using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 moveToPosition;

    [SerializeField]
    private float offsetSpeed;

    // Update is called once per frame
    void Update()
    {
        moveToPosition = new Vector3(player.transform.position.x,
            player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, moveToPosition, offsetSpeed * Time.deltaTime);
    }
}
