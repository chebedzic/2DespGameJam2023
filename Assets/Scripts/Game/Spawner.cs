using System.Collections;
using Unity.Mathematics;
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
        [SerializeField] private GameObject leftSamurai;
        [SerializeField] private GameObject rightSamurai;
        [SerializeField] private GameObject boss;
        [SerializeField] private GameObject log;
        [SerializeField] private float interval;
        public int spawned;

        private IEnumerator StartGame()
        {
            while (true)
            {
                int rnNUmber = Random.Range(0, 2);
                switch (rnNUmber)
                {
                    case 0:
                        Instantiate(leftSamurai, transform.position, quaternion.identity);
                        break;
                    case 1:
                        Instantiate(leftSamurai, transform.position, quaternion.identity);
                        break;
                    case 2:
                        //TODO: LOG!;
                        break;
                    
                }

                yield return new WaitForSeconds(interval);

            }
        }
    }
}
