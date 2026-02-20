using UnityEngine;
using TMPro;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{

    [SerializeField] 
    private int currentLifePoints;

    [SerializeField] 
    private int maxLifePoints;

    private bool isInvulnerable = false;

    [SerializeField]
    private TextMeshProUGUI currentLifePointsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentLifePoints = maxLifePoints;
        currentLifePointsText.SetText(currentLifePoints.ToString());
    }

    public void TakeDamage(int damage = 1)
    {
        if (isInvulnerable)
        {
            return;
        }
        isInvulnerable = true;
        StartCoroutine(InvulnerableDuration());
        currentLifePoints = Mathf.Clamp(currentLifePoints - damage, 0, maxLifePoints);
        currentLifePointsText.SetText(currentLifePoints.ToString());

        if (currentLifePoints == 0)
        {
           Debug.Log("Game Over");
        }
    }

    IEnumerator InvulnerableDuration()
    {
       
        
        float duration = 5f;
        float timeElasped = 0f;

        yield return new WaitForSeconds(duration);

        //while (timeElasped < duration)
        //{
       //     timeElasped += Time.deltaTime;
      //      yield return null;
      //  }
        isInvulnerable = false;
    }

}
