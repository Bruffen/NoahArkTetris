using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject AnimalPrefab;
    public int Total;

    private List<GameObject> animals;

    void Start()
    {
        animals = new List<GameObject>();
        for (int i = 0; i < Total; i++)
        {
            GameObject a = Instantiate(AnimalPrefab);
            a.transform.parent = this.transform;
            animals.Add(a);
        }
    }
}
