﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private const int X_VELOCITY_MIN = 10;
    private const int X_VELOCITY_MAX = 25;
    private const int Y_VELOCITY_MIN = 5;
    private const int Y_VELOCITY_MAX = 20;

    public int Side { get; set; }
    protected Vector3 velocity;
    protected float force;

    protected virtual void Awake()
    {
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Set when enemy is spawned in EnemyManager
        //Left = 0, Right = 1
        switch (Side)
        {
            case 0:
                //Velocity to the right
                velocity = new Vector3(Random.Range(X_VELOCITY_MIN, X_VELOCITY_MAX), Random.Range(Y_VELOCITY_MIN, Y_VELOCITY_MAX), 0.0f);
                force = -250.0f * velocity.x;
                break;
            case 1:
                //Velocity to the left
                velocity = new Vector3(Random.Range(-X_VELOCITY_MIN, -X_VELOCITY_MAX), Random.Range(Y_VELOCITY_MIN, Y_VELOCITY_MAX),0.0f);
                force = -250.0f * velocity.x;
                break;
        }

    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        transform.position += velocity * Time.fixedDeltaTime;

        if (transform.position.x > 12.0f || transform.position.x < -12.0f|| transform.position.y < -3.0f)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);

}
