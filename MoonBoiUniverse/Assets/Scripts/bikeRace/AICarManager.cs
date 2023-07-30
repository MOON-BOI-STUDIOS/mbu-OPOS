using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarManager : MonoBehaviour
{
    public List<AICarController> aiCarControllers;
    public List<AICarController> activeAICarControllers;
    public BikeController bikeController;

    [SerializeField] float easyFrequency = 5f;
    [SerializeField] float mediumFrequency = 3f;
    [SerializeField] float hardFrequency = 1f;
    [Space]
    public GameObject BlastPrefab;

    public static AICarManager Inst;
    private void Awake()
    {
        Inst = this;
    }
    private void Start()
    {
        StartCoroutine(PopulateCarsCoroutine());
        StartCoroutine(ChangePathCoroutine());
    }

    public void PopulatingCar()
    {
        if (aiCarControllers.Count > 0)
        {
            int index = Random.Range(0, aiCarControllers.Count);

            aiCarControllers[index].gameObject.SetActive(true);
            aiCarControllers[index].transform.position = new Vector3(aiCarControllers[index].transform.position.x, bikeController.transform.position.y + 20, aiCarControllers[index].transform.position.z);
            aiCarControllers[index].InitializeCar();
            aiCarControllers.RemoveAt(index);
        }
    }

    IEnumerator PopulateCarsCoroutine()
    {
        while (true)
        {
            PopulatingCar();

            switch (RaceGameManager.inst.currentDifficulty)
            {
                case RaceGameManager.Difficulty.Easy:
                    yield return new WaitForSeconds(easyFrequency);
                    break;
                case RaceGameManager.Difficulty.Medium:
                    yield return new WaitForSeconds(mediumFrequency);
                    break;
                case RaceGameManager.Difficulty.Hard:
                    yield return new WaitForSeconds(hardFrequency);
                    break;
            }
        }
    }

    IEnumerator ChangePathCoroutine()
    {
        while (true)
        {
            if (activeAICarControllers.Count > 0)
            {
                switch (RaceGameManager.inst.currentDifficulty)
                {
                    case RaceGameManager.Difficulty.Easy:
                        // No path change for "Easy" difficulty
                        break;
                    case RaceGameManager.Difficulty.Medium:
                        int indexMedium = Random.Range(0, activeAICarControllers.Count);
                        activeAICarControllers[indexMedium].ChangePath();
                        yield return new WaitForSeconds(mediumFrequency);
                        break;
                    case RaceGameManager.Difficulty.Hard:
                        int indexHard = Random.Range(0, activeAICarControllers.Count);
                        activeAICarControllers[indexHard].ChangePath();
                        yield return new WaitForSeconds(hardFrequency);
                        break;
                }
            }
            yield return null;
        }
    }

   
    public void CollisionWithCar(AICarController acc)
    {
        if(!acc.isDisdroyed)
        {
            var blast = Instantiate(BlastPrefab, acc.gameObject.transform);
            blast.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(blast, 0.5f);

            // Add force in the opposite direction of the bike's movement

            // Start fading out
            StartCoroutine(acc.RotateCarRandomly(0.5f));
            acc.isDisdroyed = true;
            acc.StopMovement();
            StartCoroutine(acc.RepairCar());
        }
    }




}
