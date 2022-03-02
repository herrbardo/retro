using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;

    Queue<Step> currentSteps;

    void Start()
    {
        currentSteps = GameManager.Instance.GetSteps();
        if(currentSteps != null)
            Spawn();
    }

    void Spawn()
    {
        GameObject clone = Instantiate(PlayerPrefab, this.transform.position, Quaternion.identity);
        clone.tag = "Enemy";
        PlayerController controller = clone.GetComponent<PlayerController>();
        controller.IsEnemy = true;
        controller.SetSteps(this.currentSteps);
    }
}
