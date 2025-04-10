using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PointBuyManager : MonoBehaviour
{
    [Header("Point Allocation")]
    public int totalPoints = 10;

    private int availablePoints;
    public static int bodyPoints = 0;
    public static int mindPoints = 0;
    public static int luckPoints = 0;

    [Header("UI References")]
    public TextMeshProUGUI pointsText;

    public TextMeshProUGUI bodyText;
    public Button bodyIncrease;
    public Button bodyDecrease;

    public TextMeshProUGUI mindText;
    public Button mindIncrease;
    public Button mindDecrease;

    public TextMeshProUGUI luckText;
    public Button luckIncrease;
    public Button luckDecrease;

    public Button accept;

    private void Start()
    {
        availablePoints = totalPoints;
        UpdateUI();

        // Assign button listeners
        bodyIncrease.onClick.AddListener(() => ModifyStat(ref bodyPoints, +1));
        bodyDecrease.onClick.AddListener(() => ModifyStat(ref bodyPoints, -1));

        mindIncrease.onClick.AddListener(() => ModifyStat(ref mindPoints, +1));
        mindDecrease.onClick.AddListener(() => ModifyStat(ref mindPoints, -1));

        luckIncrease.onClick.AddListener(() => ModifyStat(ref luckPoints, +1));
        luckDecrease.onClick.AddListener(() => ModifyStat(ref luckPoints, -1));
    }

    private void ModifyStat(ref int statValue, int delta)
    {
        // Check if increasing and there are points available
        if (delta > 0 && availablePoints <= 0) return;

        // Check if decreasing and stat is already at 0
        if (delta < 0 && statValue <= 0) return;

        statValue += delta;
        availablePoints -= delta;
        UpdateUI();
    }

    private void UpdateUI()
    {
        pointsText.text = availablePoints.ToString();
        bodyText.text = bodyPoints.ToString();
        mindText.text = mindPoints.ToString();
        luckText.text = luckPoints.ToString();
    }

    public void Accept()
    {
        if (availablePoints == 0)
        {
            SceneManager.LoadScene("Player and Enemy AI Test");
        }
    }
}
