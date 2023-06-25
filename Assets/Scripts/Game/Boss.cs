using System;
using System.Collections;
using DG.Tweening;
using Game;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    private SwipeDirection swipeToKill;
    [SerializeField]
    private Animator anim;

    [SerializeField] 
    private int numberOfAttacks;
    [SerializeField] 
    private int numebrOfShurikens;
    private Transform playerTransform;
    private Transform bossPosition;
    private bool shurikenPhase;
    [SerializeField] private GameObject shuriken;
    [SerializeField] private GameObject shurikenPrefab;
    [SerializeField] private GameObject sword;
    [SerializeField] private Transform shurikenRoot;
    private Transform leftRiver;
    private Transform rightRiver;
    [SerializeField] private Collider2D col;
    private Coroutine cor;
    private bool dead;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource miss;
    [SerializeField] private AudioSource hurtAPlayer;



    private Vector3 lScale;
    private void Awake()
    {
        playerTransform = Player.Instance.transform;
        bossPosition = GameObject.FindGameObjectWithTag("BossPosition").transform;
        leftRiver = GameObject.FindGameObjectWithTag("LeftRiver").transform;
        rightRiver = GameObject.FindGameObjectWithTag("RightRiver").transform;
        lScale = transform.localScale;
    }

    private IEnumerator Attack()
    {
        numberOfAttacks--;

        if (numberOfAttacks < 0)
        {
            cor = StartCoroutine(StartShurikenPhase());
            yield break;
        }
        var rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                swipeToKill = SwipeDirection.Left;
                transform.localScale = new Vector3(-lScale.x, lScale.y, lScale.z);
                break;
            case 1 :
                swipeToKill = SwipeDirection.Right;
                transform.localScale = new Vector3(lScale.x, lScale.y, lScale.z);
                break;
        }
        anim.Play("Taunt");
        yield return new WaitForSeconds(1.5f);
        transform.DOMove(playerTransform.position, 2);
        yield return new WaitForSeconds(1f);
        switch (rnd)
        {
            case 0:
                transform.localScale = new Vector3(lScale.x, lScale.y, lScale.z);
                break;
            case 1:
                transform.localScale = new Vector3(-lScale.x, lScale.y, lScale.z);
                break;
        }
    }

    IEnumerator StartShurikenPhase()
    {
        shurikenPhase = true;
        sword.SetActive(false);
        shuriken.SetActive(true);
        while (true)
        {
            
            yield return new WaitForSeconds(3f);
            anim.Play("Attacking");
        }
    }

    void Update()
    {
        if(dead) return;
        if (numebrOfShurikens <= 0)
        {
            var ginput = Input.gyro.attitude.eulerAngles.z;
            if (ginput > 320 || ginput < 10)
            {
                StopCoroutine(cor);
                transform.DOMove(rightRiver.position, 1).OnComplete(() =>
                {
                    anim.Play("Dying");
                    Player.Instance.OnWin();
                });
                dead = true;
            }
            else if (ginput >= 140 && ginput < 190)
            {
                StopCoroutine(cor);
                transform.DOMove(leftRiver.position, 1).OnComplete(() =>
                {
                    anim.Play("Dying");
                    Player.Instance.OnWin();
                });
                dead = true;
            }
        }

    }


    public void DestroyMe()
    {
        Destroy(gameObject);
        
    }
    private void ThrowShuriken()
    {
        if(!shurikenPhase) return;
        numebrOfShurikens--;
        if (numebrOfShurikens == 0)
        {
            Player.Instance.EndGameHelp();
        }
        Instantiate(shurikenPrefab, shurikenRoot.position, quaternion.identity);
    }
    private void Start()
    {
        StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            anim.Play("Attacking");
            col.enabled = false;
            transform.DOMove(bossPosition.position, 2).OnComplete(() =>
            {
                col.enabled = true;
                StartCoroutine(Attack());
            });
            Player.Instance.Damaged();
            hurtAPlayer.Play();
            return;
        }

        if (other.tag == "Weapon")
        {
            if (swipeToKill == Player.Instance.currentSwipe)
            {
                col.enabled = false;
                anim.Play("Hurt");
                transform.DOMove(bossPosition.position, 2).OnComplete(() =>
                {
                    col.enabled = true;
                    StartCoroutine(Attack());
                });
                hit.Play();
            }
            else
            {
                miss.Play();
            }
        }
    }
}
