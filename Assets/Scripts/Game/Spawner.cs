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
        public float max = 100;

        public int spawned;

        private IEnumerator Start()
        {
            while (true)
            {
                int rnNUmber = Random.Range(0, 2);
                if (spawned == 99)
                {
                    yield return new WaitForSeconds(2);
                    Instantiate(boss, transform.position, quaternion.identity);
                 
                    yield break;;
                }
                switch (rnNUmber)
                {
                    case 0:
                        Instantiate(leftSamurai, transform.position, quaternion.identity);
                        break;
                    case 1:
                        Instantiate(rightSamurai, transform.position, quaternion.identity);
                        break;
                     default:
                        Instantiate(log, transform.position, quaternion.identity);
                        break;
                    
                }

                spawned++;
                yield return new WaitForSeconds(spawningOfEnemies.Evaluate(spawned/max));

            }
        }
    }
}
