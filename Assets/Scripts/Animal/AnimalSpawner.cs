using System.Collections;
using System.Collections.Generic;
using Mob;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public AnimalWrapper AnimalTemplate;
    public int Total;

    private List<AnimalWrapper> animals;

    void Start()
    {
        animals = new List<AnimalWrapper>();
        for (int i = 0; i < Total; i++)
        {
            Animal an = AssetsManager.Instance.GetRandom();
            AnimalWrapper a = Instantiate(AnimalTemplate, Vector3.zero, Quaternion.identity, this.transform);
            a.Init(an);
            a.GetComponent<AIMovement>().MovementSpeed = an.speedMultiplier;
            animals.Add(a);
        }
    }
}
