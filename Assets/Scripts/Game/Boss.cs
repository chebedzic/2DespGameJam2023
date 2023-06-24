using System;
using System.Collections;
using DG.Tweening;
using Game;
using Unity.Mathematics;
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
    private Transform playerTransform;
    private Transform bossPosition;
    private bool shurikenPhase;
    [SerializeField] private GameObject shuriken;
    [SerializeField] private GameObject sword;
    [SerializeField] private Transform shurikenRoot;
    private void Awake()
    {
        playerTransform = Player.Instance.transform;
        bossPosition = GameObject.FindGameObjectWithTag("BossPosition").transform;
    }

    private IEnumerator Attack()
    {
        numberOfAttacks--;

        if (numberOfAttacks < 0)
        {
            StartCoroutine(StartShurikenPhase());
            yield break;
        }
        var rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                swipeToKill = SwipeDirection.Left;
                break;
            case 1 :
                swipeToKill = SwipeDirection.Right;
                break;
        }
        anim.Play("Taunt");
        yield return new WaitForSeconds(1.5f);
        transform.DOMove(playerTransform.position, 2);
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
    private void ThrowShuriken()
    {
        if(!shurikenPhase) return;
        var newShuriken = Instantiate(shuriken, shurikenRoot.position, quaternion.identity);

        newShuriken.transform.DOMove(playerTransform.position, 1);
        newShuriken.transform.DORotate(new Vector3(0,0,180), 0.1f).SetLoops(-1).SetEase(Ease.Linear);




    }
    private void Start()
    {
        StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Player.Instance.Damaged();
            transform.DOMove(bossPosition.position, 2).OnComplete(() =>
            {
                StartCoroutine(Attack());
            });
            return;
        }

        if (other.tag == "Weapon")
        {
            if (swipeToKill == Player.Instance.currentSwipe)
            {
                transform.DOMove(bossPosition.position, 2).OnComplete(() => { StartCoroutine(Attack()); });
                // Player.Instance.KilledAnEnemy();
            }
        }
    }
}
