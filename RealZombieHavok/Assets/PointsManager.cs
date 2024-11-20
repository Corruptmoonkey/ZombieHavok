using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance { get; private set; }

    public int points = 0;  // Variable to store the player's points
    public TextMeshProUGUI pointsText;  // Reference to the UI Text component that displays the points

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of PointsManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Update the points display at the start
        UpdatePointsUI();
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;  // Add points to the player's total
        UpdatePointsUI();  // Update the UI
    }
    public void RemovePoints(int pointsToRemove)
    {
        points -= pointsToRemove;
        UpdatePointsUI();
    }
    private void UpdatePointsUI()
    {
        pointsText.text = "Points: " + points.ToString();  // Update the points text
    }
}
