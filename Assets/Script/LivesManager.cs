using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public int startingLives = 3;
    public GameObject lifeIconPrefab;
    public Transform lifeIconContainer;

    private int currentLives;
    private Image[] lifeIcons;
    private GameEvents gameEvents;

    private void Start()
    {
        //Subscribe to Game Events
        gameEvents = GameEvents.Instance;
        GameEvents.OnPlayerDamaged += DecreaseLives;
        GameEvents.OnGameRestart += ResetLives;

        //Initializing values and Life icons
        currentLives = startingLives;
        InitializeLifeIcons();
        UpdateLifeIcons();
    }

    private void OnDestroy()
    {
        //Unsubscribe to Game Events
        GameEvents.OnPlayerDamaged -= DecreaseLives;
        GameEvents.OnGameRestart -= ResetLives;
    }

    private void InitializeLifeIcons()
    {
        lifeIcons = new Image[startingLives];

        float iconSpacing = lifeIconPrefab.GetComponent<RectTransform>().sizeDelta.x;

        for (int i = 0; i < startingLives; i++)
        {
            GameObject lifeIconObject = Instantiate(lifeIconPrefab, lifeIconContainer);
            lifeIconObject.transform.localPosition = new Vector3(i * iconSpacing, lifeIconContainer.position.y , 0f);

            lifeIcons[i] = lifeIconObject.GetComponent<Image>();
        }
    }

    private void DecreaseLives()
    {
        currentLives--;
        UpdateLifeIcons();

        if (currentLives <= 0)
        {
            gameEvents.TriggerGameOver();
        }
    }

    private void UpdateLifeIcons()
    {
        for (int i = 0; i < startingLives; i++)
        {
            if (i < currentLives)
            {
                lifeIcons[i].enabled = true;
            }
            else
            {
                lifeIcons[i].enabled = false;
            }
        }
    }

    private void ResetLives()
    {
        currentLives = startingLives;
        UpdateLifeIcons();
    }

}
