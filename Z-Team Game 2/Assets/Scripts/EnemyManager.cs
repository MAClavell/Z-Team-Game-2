﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bounceEnemyPrefab;
    [SerializeField]
    private GameObject stickEnemyPrefab;
   
    private float time;
    private float spawnTime;
    private AudioSource enemyEnter1;
    private AudioSource enemyEnter2;
    private AudioSource enemyEnter3;
    AudioSource[] audioManager;
    float difficultyModifier;
    float difficultyChange; //used when difficulty is changed mid game
    TMPro.TMP_Dropdown difficultySelect;

    private void Awake()
    {
        enemyEnter1 = GameObject.Find("enemyEnter1").GetComponent<AudioSource>();
        enemyEnter2 = GameObject.Find("enemyEnter2").GetComponent<AudioSource>();
        enemyEnter3 = GameObject.Find("enemyEnter3").GetComponent<AudioSource>();
        audioManager = new AudioSource[3];
        audioManager[0] = enemyEnter1;
        audioManager[1] = enemyEnter2;
        audioManager[2] = enemyEnter3;
        difficultySelect = GameObject.FindGameObjectWithTag("DifficultySelect").GetComponent<TMPro.TMP_Dropdown>();
        difficultyChange = 1.0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        spawnTime = 1.5f;
        difficultyModifier = 1.0f;
        difficultySelect.onValueChanged.AddListener(delegate {
            ChangeDifficulty(difficultySelect.value);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<GameManager>().CurrentState == GameState.Playing)
        {
            time += Time.deltaTime;
            if (time >= spawnTime * difficultyModifier)
            {
                SpawnEnemy();
                time -= spawnTime * difficultyModifier;
            }
        }

        if (transform.GetComponent<GameManager>().CurrentState == GameState.GameOver)
        {
            foreach (var deadEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(deadEnemy.transform.parent.gameObject);
            }
        }
    }

    private void SpawnEnemy()
    {
        int side = Random.Range(0, 2);
        int enterSound = Random.Range(0, 3);

        float xBound = (Camera.main.orthographicSize) * ((float)Screen.width / Screen.height) + 2;

        GameObject prefab = bounceEnemyPrefab;
        if (Random.Range(0, 4) == 0)
            prefab = stickEnemyPrefab;

        if (side == 0)
        {
            GameObject enemySpawn = GameObject.Instantiate(prefab, new Vector3(-xBound, 0.0f, 0.0f), Quaternion.identity);
            enemySpawn.transform.Rotate(Vector3.forward, Random.Range(-90, 90));
            enemySpawn.GetComponentInChildren<Enemy>().Side = 0;
        }
        else
        {
            GameObject enemySpawn = GameObject.Instantiate(prefab, new Vector3(xBound, 0.0f, 0.0f), Quaternion.identity);
            enemySpawn.transform.Rotate(Vector3.forward, Random.Range(-90, 90));
            enemySpawn.GetComponentInChildren<Enemy>().Side = 1;
        }

        audioManager[enterSound].Play();
    }

    public void ChangeDifficulty(int difficulty)
    {
        if (difficulty == 0)
        {
            //Easy
             difficultyChange = 1.25f;
            Debug.Log("EASY");
        }
        else if (difficulty == 1)
        {
            //Medium
            difficultyChange = 1.0f;
            Debug.Log("MEDIUM");
        }
        else if (difficulty == 2)
        {
            //Hard
            difficultyChange = 0.5f;
            Debug.Log("HARD");
        }


    }

    public void SetDifficulty()
    {
        difficultyModifier = difficultyChange;
    }

}
