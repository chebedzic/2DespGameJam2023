using DG.Tweening;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] private float timeToReachPlayer;
    private void Awake()
    {
        transform.DOMove(Player.Instance.transform.position, timeToReachPlayer);
        transform.DORotate(new Vector3(0,0,180), 0.1f).SetLoops(-1).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Player.Instance.Damaged();
            return;
        }

        if (other.tag == "Weapon")
        {
            Destroy(gameObject);
        }
    }
}