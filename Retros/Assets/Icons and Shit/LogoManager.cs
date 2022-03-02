using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] Image LogoImage;

    
    private void Awake()
    {
        TransitionEvents.GetInstance().TransitionCreated += TransitionCreated;
        TransitionEvents.GetInstance().TransitionFinished += TransitionFinished;
    }

    void Start()
    {
        //LogoImage.enabled = false;
    }

    private void OnDestroy()
    {
        TransitionEvents.GetInstance().TransitionCreated -= TransitionCreated;
        TransitionEvents.GetInstance().TransitionFinished -= TransitionFinished;
    }

    // Update is called once per frame
    void Update()
    {
        LogoImage.enabled = TransitionVariables.GetInstance().IsThereAnyTransition();
        Rotate();
    }

    void Rotate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.Rotate(new Vector3( 0, 0, 45 * Time.deltaTime * Speed));
    }

    void TransitionCreated(TransitionMode mode)
    {
        //LogoImage.enabled = true;
    }

    void TransitionFinished(TransitionMode mode)
    {
        // if(mode != TransitionMode.FadeToBlack)
        //     LogoImage.enabled = false;
    }
}
