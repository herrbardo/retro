using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSoundsManager : MonoBehaviour
{
    public static EnvironmentSoundsManager Instance;

    [SerializeField] AudioSource WindSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        GameEvents.GetInstance().GameExited += GameExited;
    }

    private void OnDestroy()
    {
        GameEvents.GetInstance().GameExited -= GameExited;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void GameExited()
    {
        Destroy(this.gameObject);
    }
}
