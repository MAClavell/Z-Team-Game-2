﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int side;
    private Vector3 velocity;
    private bool hit;
    BoxCollider2D collider;
    GameObject player;
    float force;



    private void Awake()
    {
        collider = transform.GetComponent<BoxCollider2D>();
        player = GameObject.Find("Pole");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set when enemy is spawned in EnemyManager
        //Left = 0, Right = 1
        switch (side)
        {
            case 0:
                //Velocity to the right
                velocity = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.25f),0.0f);
                force = -10000.0f * velocity.x;
                break;
            case 1:
                //Velocity to the left
                velocity = new Vector3(Random.Range(-0.5f, -0.1f), Random.Range(0.1f, 0.25f),0.0f);
                force = -10000.0f * velocity.x;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity;
    
    }



    private void Stick()
    {

    }

    private void Bounce()
    {
        velocity.x = -velocity.x;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<Pole>().AddForce(force);
            Bounce();
        }
    }


}
