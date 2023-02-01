using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    int products;

    [SerializeField] int cost;

    void Start()
    {
        CustomerSpawner.instance.shelfs.Add(gameObject);
    }

    public void AddProducts()
    {
        if (MoneyManeger.Instance.ChechMoney(cost))
        {
            if (products <= 3)
            {
                products += 3;

                for (int i = products - 3; i < products; i++)
                {
                    if (!transform.GetChild(i).gameObject.activeSelf)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }

                MoneyManeger.Instance.ChangeMoney(cost * -1);
            }
        }
    }

    public int UseProducts()
    {
        if (products < 1)
        {
            return 0;
        }

        products--;

        transform.GetChild(products).gameObject.SetActive(false);
        
        return cost;
    }

}
