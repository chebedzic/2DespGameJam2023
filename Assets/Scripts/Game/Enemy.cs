using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player.Instance.Damaged();
            return;
        }

        if (other.tag == "Weapon")
        {
            Destroy(gameObject);
            Player.Instance.KilledAnEnemy();
        }
    }
}
