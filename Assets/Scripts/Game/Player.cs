using System;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Player instance;
    private int killed = 0;
    private int health = 3;
    [SerializeField] private TextMeshProUGUI scoreboard;
    [SerializeField] private List<GameObject> hearts;
    public SwipeDirection currentSwipe;
    private bool dead;
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
        var tr = transform;
        _animator = GetComponent<Animator>();

        if (Input.GetMouseButtonDown(0))
        {
            _animator.Play("Attacking");
        }

        SwipeController.Instance.OnSwipe += direction =>
        {
            if(dead) return;
            currentSwipe = direction;
            switch (direction)
            {
                case SwipeDirection.Left:
                    tr.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                    _animator.Play("Attacking");
                    break;
                case SwipeDirection.Right:
                    tr.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    _animator.Play("Attacking");
                    break;
                case SwipeDirection.Down:
                case SwipeDirection.Up:
                    break;
            }
        };
    }

    public void KilledAnEnemy()
    {
        killed++;
        scoreboard.text = $"Enemies:{Environment.NewLine}{(Spawner.Instance.max - killed)}";
    }

    public void OnDeath()
    {
        SceneManager.LoadScene("Main");
    }

    public void Damaged()
    {
        if (--health == 0)
        {
            dead = true;
            _animator.Play("Dying");
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