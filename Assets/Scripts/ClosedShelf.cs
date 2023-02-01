using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosedShelf : MonoBehaviour
{
    [SerializeField] int cost;
    Text costText;

    [SerializeField] GameObject prefabs;

    void Awake()
    {
        costText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        costText.text = "Cost : " + cost;
    }

    public void OpenShelf()
    {
        if (MoneyManeger.Instance.ChechMoney(cost))
        {
            MoneyManeger.Instance.ChangeMoney(cost * -1);
            Instantiate(prefabs, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
