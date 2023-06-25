using System;
using System.Collections;
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
    [SerializeField] private GameObject rootBtn;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private DialogueBox dialogue;
    [SerializeField] private AudioSource audio;
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
        if (scoreboard != null)
            scoreboard.text = $"Enemies:{Environment.NewLine}{(Spawner.max - killed)}";
        var tr = transform;
        _animator = GetComponent<Animator>();

        if (Input.GetMouseButtonDown(0))
        {
            _animator.Play("Attacking");
        }

        if (SwipeController.Instance != null)
            SwipeController.Instance.OnSwipe += direction =>
            {
                if (dead) return;
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
        scoreboard.text = $"Enemies:{Environment.NewLine}{(Spawner.max - killed)}";
    }

    private void ThrowShuriken()
    {
    }
    public void OnDeath()
    {
        rootBtn.SetActive(true);

        looseScreen.SetActive(true);
        var enemies = FindObjectsOfType<Enemy>();
        var boss = FindObjectOfType<Boss>();
        if (boss != null)
            Destroy(boss.gameObject);
        if (enemies != null)
            foreach (var enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }

        Spawner.Instance.gameObject.SetActive(false);
    }
    public void OnWin()
    {
        StartCoroutine(DoWin());
    }

    IEnumerator DoWin()
    {
        yield return new WaitForSeconds(2);
        
        rootBtn.SetActive(true);
        
        Spawner.Instance.gameObject.SetActive(false);
        winScreen.SetActive(true);
    }

    public void EndGameHelp()
    {
        Input.gyro.enabled = true;
        StartCoroutine(dialogue.EndingText());
    }

    public void PLaySwoosh()
    {
        audio.Play();
    }
    public void Damaged()
    {
        if (--health == 0)
        {
            dead = true;
            _animator.Play("Dying");
        }
        else
        {
            _animator.Play("Hurt");
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