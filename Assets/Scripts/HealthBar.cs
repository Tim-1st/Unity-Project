using UnityEngine;
using UnityEngine.UI;
 
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private VoidEventChannel onPlayerTakeDamage;

    [SerializeField]
    private IntVariable playerCurrentLifePoints;

    [SerializeField]
    private IntVariable playerMaxLifePoints;

        void Update()
        {
            SetHealth();
        }
    private void SetHealth()
    {
        float healthNormalized = (float)playerCurrentLifePoints.CurrentValue / playerMaxLifePoints.CurrentValue; //permet de calculer le pourcentage de vie restant
        fillImage.fillAmount = healthNormalized; //permet de faire varier la taille de la barre de vie en fonction du pourcentage de vie restant
        fillImage.color = gradient.Evaluate(healthNormalized); //permet de faire varier la couleur de la barre de vie en fonction du pourcentage de vie restant
    }
}
 