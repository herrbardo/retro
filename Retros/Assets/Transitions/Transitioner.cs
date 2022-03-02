using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitioner : MonoBehaviour
{
    [SerializeField] GameObject TransitionPrefab;
    [SerializeField] bool CreateStartTransition;
    [SerializeField] GameObject CanvasTarget;
    [SerializeField] float TransitionDuration;
    [SerializeField] float IntervalToLaunch;

    string sceneToLaunch;

    // Start is called before the first frame update
    void Start()
    {
        TransitionEvents.GetInstance().TransitionToScene += TransitionCalled;

        if(CreateStartTransition)
            CreateTransition(TransitionDuration, TransitionMode.FadeToLight);
    }

    void OnDestroy()
    {
        TransitionEvents.GetInstance().TransitionToScene -= TransitionCalled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TransitionCalled(string sceneName)
    {
        this.sceneToLaunch = sceneName;
        CreateTransition(TransitionDuration, TransitionMode.FadeToBlack);
        Invoke("FinallyLaunch", IntervalToLaunch);
    }

    void FinallyLaunch()
    {
        SceneManager.LoadScene(this.sceneToLaunch);
    }

    void CreateTransition(float fadeSpeed, TransitionMode mode)
    {
        GameObject trans = Instantiate(this.TransitionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        trans.transform.SetParent(CanvasTarget.transform, false);
        TransitionManager manager = trans.GetComponent<TransitionManager>();
        manager.Duration = TransitionDuration;
        manager.EnableFade = true;
        manager.Mode = mode;
    }
}
