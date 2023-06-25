using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SwipeDirection swipeToKill;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D col;
    [SerializeField] private float timeToPlayer;
    private  TweenerCore<Vector3, Vector3, VectorOptions> tween;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource miss;
    [SerializeField] private AudioSource hurtAPlayer;

    private bool dealtDamage;

    private bool dead;
    private void Awake()
    {
        tween = transform.DOMove(Player.Instance.transform.position, timeToPlayer, false).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(dead ) return;
        
        if (other.tag == "Player")
        {
            tween.Kill();
            anim.Play("Attacking");
            col.enabled = false;
            dealtDamage = true;
            dead = true;
            hurtAPlayer.Play();
            return;
        }

        if (other.tag == "Weapon")
        {
            if (swipeToKill == Player.Instance.currentSwipe)
            {
                col.enabled = false;
                anim.Play("Dying");
                tween.Kill();
                dead = true;
                Player.Instance.KilledAnEnemy();
                hit.Play();
            }
            else
            {
                miss.Play();
            }
        }
    }

    private void ThrowShuriken()
    {
    }

    public void DestroyMe()
    {
        if (dealtDamage)
            Player.Instance.Damaged();
        Destroy(gameObject);
    }
}
