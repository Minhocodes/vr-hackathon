using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Nastavení")]
    public float memorizationTime = 30f;
    public FurnitureItem[] furnitureItems;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    private GamePhase currentPhase = GamePhase.Memorization;

    public enum GamePhase { Memorization, Scrambled, Placing, Results }

    void Start()
    {
        StartCoroutine(RunGame());
    }

    IEnumerator RunGame()
    {
        // FÁZE 1 - Zapamatování
        currentPhase = GamePhase.Memorization;
        UpdatePhaseText("Zapamatuj si místnost!");

        float timer = memorizationTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timerText) timerText.text = Mathf.CeilToInt(timer) + "s";
            yield return null;
        }

        // FÁZE 2 - Chaos
        currentPhase = GamePhase.Scrambled;
        UpdatePhaseText("Rozhazuji nábytek...");
        foreach (var item in furnitureItems)
            item.Scramble();

        yield return new WaitForSeconds(2f);

        // FÁZE 3 - Skládání
        currentPhase = GamePhase.Placing;
        UpdatePhaseText("Dej nábytek zpět!");
        if (timerText) timerText.text = "";

        // Čekáme dokud není vše na místě
        while (!AllPlaced())
        {
            foreach (var item in furnitureItems)
                item.CheckPlacement();
            yield return null;
        }

        // FÁZE 4 - Výsledky
        currentPhase = GamePhase.Results;
        score = furnitureItems.Length;
        UpdatePhaseText("Výborně! Hotovo!");
        if (scoreText) scoreText.text = "Skóre: " + score + "/" + furnitureItems.Length;
    }

    bool AllPlaced()
    {
        foreach (var item in furnitureItems)
            if (!item.IsPlaced()) return false;
        return true;
    }

    void UpdatePhaseText(string text)
    {
        if (phaseText) phaseText.text = text;
        Debug.Log(text);
    }
}