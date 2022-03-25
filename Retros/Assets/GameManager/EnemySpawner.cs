using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] PortalArea PortalEnemyArea;

    Queue<Queue<Step>> stepsToSpawn;
    bool keepCheckingCollisions;

    private void Awake()
    {
        stepsToSpawn = new Queue<Queue<Step>>();
        ChooseSpawningStrategy();
    }

    private void Start()
    {
        keepCheckingCollisions = true;
        StartCoroutine(CheckIfPortalAreaIsEmpty());
    }

    private void OnDestroy()
    {
        keepCheckingCollisions = false;
    }

    void ChooseSpawningStrategy()
    {
        List<Queue<Step>> stepsByRound = GameManager.Instance.GetStepsByRound();
        if(stepsByRound.Count == 0)
            return;

        int currentRound = GameManager.Instance.CurrentRound;

        if((currentRound > 1 && currentRound <= 5) || (currentRound > 11 && currentRound <= 15))
        {
            Queue<Step> previousRound = stepsByRound[stepsByRound.Count - 1];
            this.stepsToSpawn.Enqueue(previousRound);
        }
        else if(currentRound > 5 && currentRound <= 9)
        {
            Queue<Step> previousRound = stepsByRound[stepsByRound.Count - 1];
            Queue<Step> previousToPreviousRound = stepsByRound[stepsByRound.Count - 2];
            this.stepsToSpawn.Enqueue(previousRound);
            this.stepsToSpawn.Enqueue(previousToPreviousRound);
        }
        else if(currentRound % 10 == 0)
        {
            int start = stepsByRound.Count - 1;
            int limit = start - 3;
            if(limit < 0)
                limit = 0;
            
            for (int i = start; i > limit; i--)
                this.stepsToSpawn.Enqueue(stepsByRound[i]);
        }
    }

    void Spawn(Queue<Step> steps)
    {
        GameObject clone = Instantiate(PlayerPrefab, this.transform.position, Quaternion.identity);
        clone.tag = "Enemy";
        PlayerStateManager playerStateManager = clone.GetComponent<PlayerStateManager>();
        playerStateManager.SetState(new PlayerStateEnemy());
        playerStateManager.StepsRecord = steps;
    }

    void SpawnNext()
    {
        if(this.stepsToSpawn.Count == 0)
            return;
        
        Queue<Step> currentSteps = this.stepsToSpawn.Dequeue();
        Spawn(currentSteps);
    }

    IEnumerator CheckIfPortalAreaIsEmpty()
    {
        while(this.stepsToSpawn.Count > 0 && keepCheckingCollisions)
        {
            yield return new WaitForSeconds(3);
            if(PortalEnemyArea.IsEmpty)
                SpawnNext();
        }

        yield break;
    }
}
