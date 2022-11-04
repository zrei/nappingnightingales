using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public Sprite newSprite;
    private List<SpriteRenderer> children = new List<SpriteRenderer>();
    private int maxHealth;
    private int currDamage = 0;

    private void Awake() 
    {
        foreach (Transform child in transform)
        {
            //child is your child transform
            children.Add(child.gameObject.GetComponent<SpriteRenderer>());
        }
        maxHealth = children.Count;
        //Debug.Log(maxHealth);
        /*for (int i = 0; i < maxHealth; i++) {
            Debug.Log(i);
        }*/
    }

    private void Start()
    {
        EventManager.current.onDamage += Damage;
    }

    public void Damage()
    {
        if (currDamage < maxHealth)
        {
            SpriteRenderer currHeart = children[currDamage];
            ChangeHeart(currHeart);
            currDamage += 1;
        }
    }

    private void ChangeHeart(SpriteRenderer heart)
    {
        heart.sprite = newSprite;
        Transform heartTransform = heart.gameObject.GetComponent<Transform>();
        heartTransform.localScale = new Vector3(11, 11, 1);
    }
}