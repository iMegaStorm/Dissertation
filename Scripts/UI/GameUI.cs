using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Team 1")]
    public TextMeshProUGUI team1UnitCountText;
    public TextMeshProUGUI team1FoodText;
    public TextMeshProUGUI team1WoodText;

    [Header("Team 2")]
    public TextMeshProUGUI team2UnitCountText;
    public TextMeshProUGUI team2FoodText;
    public TextMeshProUGUI team2WoodText;

    public static GameUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateTeam1UnitCountText(int value)
    {
        team1UnitCountText.text = value.ToString();
    }

    public void UpdateTeam1FoodText(int value)
    {
        team1FoodText.text = value.ToString();
    }

    public void UpdateTeam1WoodText(int value)
    {
        team1WoodText.text = value.ToString();
    }

    public void UpdateTeam2UnitCountText(int value)
    {
        team2UnitCountText.text = value.ToString();
    }

    public void UpdateTeam2FoodText(int value)
    {
        team2FoodText.text = value.ToString();
    }

    public void UpdateTeam2WoodText(int value)
    {
        team2WoodText.text = value.ToString();
    }
}
