using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int leftSideTroopCount = 0;
    public int rightSideTroopCount = 0;
    private List<Troops> leftSideTroops = new List<Troops>();
    private List<Troops> rightSideTroops = new List<Troops>();

    public GameObject gameOverText;
    public GameObject controlUI;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddTroop(Troops troop, bool isLeftSide)
    {
        if (isLeftSide)
        {
            leftSideTroops.Add(troop);
            leftSideTroopCount++;
        }
        else
        {
            rightSideTroops.Add(troop);
            rightSideTroopCount++;
        }

        Debug.Log($"Troops Landed - Left: {leftSideTroopCount}, Right: {rightSideTroopCount}");

        if (leftSideTroopCount == 4)
        {
            StartCoroutine(StartTroopMovement(leftSideTroops, true));
        }
        else if (rightSideTroopCount == 4)
        {
            StartCoroutine(StartTroopMovement(rightSideTroops, false));
        }
    }

    public bool CanSpawnTroops()
    {
        return leftSideTroopCount < 4 && rightSideTroopCount < 4;
    }

    private IEnumerator StartTroopMovement(List<Troops> troops, bool isLeftSide)
    {
        if (troops.Count < 4) yield break;

        Debug.Log("🚶 Moving troops toward their positions in sequence...");

        // Define target positions based on the side
        Vector3[] targetPositions;
        if (isLeftSide)
        {
            targetPositions = new Vector3[]
            {
                new Vector3(-0.68f, -3.276153f, 0f), // 1st
                new Vector3(-0.68f, -2.885f, 0f),   // 2nd (slide directly to this position)
                new Vector3(-0.855f, -3.276153f, 0f), // 3rd
                new Vector3(-1.023f, -3.269f, 0f), // 4th step 1
                new Vector3(-0.855f, -2.885f, 0f), // 4th step 2
                new Vector3(-0.68f, -2.491f, 0f),  // 4th step 3
                new Vector3(-0.43f, -2.519f, 0f)    // 4th step 4
            };
        }
        else
        {
            targetPositions = new Vector3[]
            {
                new Vector3(0.634f, -3.2761f, 0f), // 1st
                new Vector3(0.634f, -2.869f, 0f),   // 2nd (slide directly to this position)
                new Vector3(0.805f, -3.2761f, 0f),  // 3rd
                new Vector3(0.994f, -3.2761f, 0f),  // 4th step 1
                new Vector3(0.805f, -2.869f, 0f),   // 4th step 2
                new Vector3(0.634f, -2.454f, 0f),   // 4th step 3
                new Vector3(0.384f, -2.519f, 0f)    // 4th step 4
            };
        }

        // Move troops to their respective positions
        for (int i = 0; i < targetPositions.Length; i++)
        {
            // Freeze all troops except the one currently moving
            for (int j = 0; j < troops.Count; j++)
            {
                if (j != i)
                {
                    troops[j].FreezeTroop();
                }
            }

            troops[i].UnfreezeTroop();
            yield return StartCoroutine(troops[i].MoveToPosition(targetPositions[i]));

            // If it's the second troop, just move to its position without jumping
            if (i == 1) // Second troop
            {
                // No jump, just ensure it is positioned correctly
                Vector3 aboveFirstTroopPosition = new Vector3(troops[i - 1].transform.position.x, troops[i - 1].transform.position.y + 0.5f, 0f);
                yield return StartCoroutine(troops[i].MoveToPosition(aboveFirstTroopPosition)); // Move directly above the first troop
            }

            // If it's the fourth troop, perform the jumping sequence
            if (i == 3) // Fourth troop
            {
                // Jump on the third troop
                yield return StartCoroutine(troops[i].JumpOnTroop(troops[i - 1])); // Jump on 3rd
                                                                                   // Jump on the second troop
                yield return StartCoroutine(troops[i].JumpOnTroop(troops[i - 2])); // Jump on 2nd
                                                                                   // Move to turret position
                yield return StartCoroutine(troops[i].MoveToPosition(targetPositions[6])); // Move to turret position
            }

            troops[i].FixPosition();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        if (gameOverText != null)
            gameOverText.SetActive(true); // ✅ Show Game Over UI

        if (controlUI != null)
            controlUI.SetActive(false); // ✅ Hide control UI

        Time.timeScale = 0; // ✅ Stop the game
    }
}