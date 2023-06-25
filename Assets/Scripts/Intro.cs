using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject playersDialogue;
    [SerializeField] private GameObject bossDialogue;
    [SerializeField] private Transform boss;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform bossPosition;
    [SerializeField] private GameObject spawner;
    private void Awake()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(player.DOMove(playerPosition.position, 1));
        sequence.AppendCallback(() =>
        {
            playersDialogue.SetActive(true);
        });
        sequence.AppendInterval(3);
        sequence.AppendCallback(() =>
        {
            bossDialogue.SetActive(true);
            playersDialogue.SetActive(false);
        });        
        sequence.AppendInterval(3);
        sequence.AppendCallback(() =>
        {
            bossDialogue.SetActive(false);
        });
        sequence.Append(boss.DOMove(bossPosition.position, 2).OnComplete(() =>
        {
            spawner.SetActive(true);
            Destroy(boss.gameObject);
        }));
    }
}
