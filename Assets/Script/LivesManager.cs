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
        gameEvents = GameEvents.Instance;
        GameEvents.OnPlayerDamaged += DecreaseLives;

        currentLives = startingLives;
        InitializeLifeIcons();
        UpdateLifeIcons();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerDamaged -= DecreaseLives;
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

    public void DecreaseLives()
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

}
