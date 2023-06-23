using System;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player instance;
    private int killed = 0;
    private int health = 3;
    [SerializeField] private TextMeshProUGUI scoreboard;
    [SerializeField] private List<GameObject> hearts;
    
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            return instance;
        }
    }
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.Play("Attacking");
        }
    }

    public void KilledAnEnemy()
    {
        killed++;
        scoreboard.text = $"Enemies:{Environment.NewLine}{(Spawner.Instance.max - killed)}";
    }
    public void Damaged()
    {
        if (--health == 0)
        {
            SceneManager.LoadScene("Game");
        }
        
        for (int i = 0; i < 3; i++)
        {
            if (health <= i)
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
