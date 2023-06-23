using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        private static Spawner instance;

        public static Spawner Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Spawner>();
                }

                return instance;
            }
        }
        [SerializeField] private Transform leftSamurai;
        [SerializeField] private Transform rightSamurai;
        [SerializeField] private Transform boss;
        [SerializeField] private Transform log;
        [SerializeField] private AnimationCurve spawningOfEnemies;
        [SerializeField] private float max = 100;

        private Transform _player;
        public int spawned;

        void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        private IEnumerator Start()
        {
            while (true)
            {
                int rnNUmber = Random.Range(0, 3);
                Transform tr;
                if (spawned == 99)
                {
                    yield return new WaitForSeconds(2);
                    tr = Instantiate(boss, transform.position, quaternion.identity);
                    tr.DOMove(_player.position, 2f, false).SetEase(Ease.Linear);
                    yield break;;
                }
                switch (rnNUmber)
                {
                    case 0:
                        tr = Instantiate(leftSamurai, transform.position, quaternion.identity);
                        break;
                    case 1:
                        tr =Instantiate(rightSamurai, transform.position, quaternion.identity);
                        break;
                     default:
                        tr =Instantiate(log, transform.position, quaternion.identity);
                        break;
                    
                }

                tr.DOMove(_player.position, 2f, false).SetEase(Ease.Linear);
                spawned++;
                yield return new WaitForSeconds(spawningOfEnemies.Evaluate(spawned/max));

            }
        }
    }
}
