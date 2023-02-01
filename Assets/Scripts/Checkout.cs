using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : MonoBehaviour
{
    public static Checkout Instance;

    public Customer customer;

    private void Awake()
    {
        Instance = this;
    }

    public void GetMoney()
    {
        if (customer != null)
        {
            MoneyManeger.Instance.ChangeMoney(customer.productCost);
            customer.shelfTarget = null;
            customer = null;
        }
    }
}
