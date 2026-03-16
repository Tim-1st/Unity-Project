using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using NUnit.Framework;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private IntVariable currentLifePoints;

    [SerializeField]
    private IntVariable maxLifePoints;

    [SerializeField]
    private TextMeshProUGUI currentLifePointsText;

    [SerializeField]
    private VoidEventChannel onTakeDamage;

    [SerializeField]
    private VoidEventChannel onPlayerDeath;

    [SerializeField]
    private SpriteRenderer sr;

    private bool isInvulnerable = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentLifePoints.CurrentValue = maxLifePoints.CurrentValue;
        currentLifePointsText.SetText(maxLifePoints.CurrentValue.ToString());
    }

    public void TakeDamage(int damage = 1)
    {
        if (isInvulnerable)
        {
            return;
        }
        isInvulnerable = true;
        StartCoroutine(InvulnerableFlash());
        currentLifePoints.CurrentValue = Mathf.Clamp(
            currentLifePoints.CurrentValue - damage,
            0,
            maxLifePoints.CurrentValue
        );
        currentLifePointsText.text = currentLifePoints.CurrentValue.ToString();
        onTakeDamage.Raise();
        if (currentLifePoints.CurrentValue == 0)
        {
            onPlayerDeath.Raise();
        }
    }
    IEnumerator InvulnerableFlash()
    {
        isInvulnerable = true;
 
        // Durée de l'invulnerabilité
        float invulnerableDuration = 1.25f;
        // Temps écoulé durant la période d'invulnerabilité
        float timeElapsed = 0;
 
        // Durée durant laquelle le sprite est visible ou invisible
        float flashInvulnerabilityDuration = 0.2f;
        // Temps écoulé durant la période de visibilité ou invisibilité
        float flashTimeElapsed = 0;
        bool isVisible = true;
 
        while (timeElapsed < invulnerableDuration)
        {
            timeElapsed += Time.deltaTime;
            flashTimeElapsed += Time.deltaTime;
 
            if (flashTimeElapsed >= flashInvulnerabilityDuration)
            {
                if (isVisible)
                {
                    sr.color = Color.clear;
                } else
                {
                    sr.color = Color.white;
                }
 
                flashTimeElapsed = 0;
                isVisible = !isVisible;
            }
 
            // Attendre le rafraichissement de l'écran
            yield return null;
        }
 
        sr.color = Color.white;
        isInvulnerable = false;
    }
}