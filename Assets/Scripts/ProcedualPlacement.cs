using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualPlacement : MonoBehaviour
{
    [SerializeField] GameObject prefabs;
    [SerializeField] int maxCount;

    int placementTotalCount = 0;

    [SerializeField] int xMapSize;
    [SerializeField] int yMapSize;

    public void Spawn(int totalCount)
    {
        placementTotalCount = 0;

        while (placementTotalCount < totalCount)
        {
            Vector2 randomPos = new Vector3(Random.Range(-xMapSize,xMapSize),Random.Range(-yMapSize, yMapSize));

            GameObject obj = Instantiate(prefabs, randomPos, Quaternion.identity);

            Vector2 size = obj.GetComponent<Collider2D>().bounds.size;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(obj.transform.position,size,0);
            if (colliders.Length > 1)
            {
                Destroy(obj);
            }
            else
            {
                obj.transform.parent = transform;
                CustomerSpawner.instance.extreWaitTime += 0.2f;

                placementTotalCount++;
            }
        }
    }
}
