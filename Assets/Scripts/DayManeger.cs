using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManeger : MonoBehaviour
{
    public static DayManeger instance;

    [SerializeField] Text timeText;
    public float dayLengthInSeconds;

    float totalMinutes = 0;
    public int currentDay = 0;
    int currentHour = 0;

    [SerializeField] GameObject reportPanel;
    [SerializeField] GameObject gamePanel;

    [SerializeField] ProcedualPlacement procedualPlacement;
    int tempDay;

    void Awake()
    {
        tempDay = currentDay;
        instance = this;
        gamePanel.SetActive(true);
    }

    void Start()
    {
        totalMinutes = (int)((dayLengthInSeconds / 24) * 9);
        procedualPlacement.Spawn((int)(Mathf.Sqrt(currentDay + 1)));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            reportPanel.SetActive(true);
            gamePanel.SetActive(false);
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        totalMinutes += Time.fixedDeltaTime;

        if (totalMinutes >= dayLengthInSeconds)
        {
            currentDay++;
            totalMinutes = 0;

            reportPanel.SetActive(true);
            gamePanel.SetActive(false);
            MoneyManeger.Instance.DaySkip();
            Time.timeScale = 0;
        }

        currentHour = (int)(totalMinutes / (dayLengthInSeconds/24));

        timeText.text = "Day : " + currentDay + " Hour : " + currentHour + ":00";
    }

    public void Contunie()
    {
        Time.timeScale = 1;
        gamePanel.SetActive(true);
        reportPanel.SetActive(false);

        if (currentDay != tempDay)
        {
            tempDay = currentDay;
            procedualPlacement.Spawn((int)(Mathf.Sqrt(currentDay + 1)));
        }
    }
}
