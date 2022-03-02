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
    }

    void Start()
    {
        //TOOD Quizas haya que desactivar el viento cuando el jugador sale del juego.
    }

    
    void Update()
    {
        
    }
}
