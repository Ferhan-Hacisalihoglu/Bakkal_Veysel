using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner instance;

    [SerializeField] GameObject customer;

    float waitTime;
    [SerializeField] float countDownTime;

    public float extreWaitTime;

    public List<GameObject> shelfs;

    [SerializeField] RuntimeAnimatorController[] controllers;

    private void Awake()
    {
        extreWaitTime = 0;
        instance = this;
        waitTime = (countDownTime + extreWaitTime) * (DayManeger.instance.dayLengthInSeconds / 24);
    }

    private void FixedUpdate()
    {
        if (waitTime <= 0)
        {
            waitTime = (countDownTime + extreWaitTime) * (DayManeger.instance.dayLengthInSeconds / 24);
            SpawnCustomer();    
        }
        else
        {
            waitTime -= Time.fixedDeltaTime;
        }
    }

    void SpawnCustomer()
    {
        GameObject _customer = Instantiate(customer,transform.position,Quaternion.identity);
        _customer.GetComponent<Animator>().runtimeAnimatorController = controllers[Random.Range(0, controllers.Length)];
        _customer.transform.parent = transform;
        _customer.GetComponent<Customer>().shelfTarget = shelfs[Random.Range(0, shelfs.Count)].transform;
    }
}
