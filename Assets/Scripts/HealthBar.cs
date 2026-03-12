using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField]
    private Image image;

    [SerializeField]
    private Gradient gradient;
    public void SetHealth(float healthNormalized)
    {
        image.fillAmount = healthNormalized;
        image.color = gradient.Evaluate(healthNormalized);
    }
    
}
