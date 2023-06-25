using UnityEngine;

public class DontDestroyMe : MonoBehaviour
{
    
    void Start()
    {
        if (FindObjectsOfType<DontDestroyMe>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
