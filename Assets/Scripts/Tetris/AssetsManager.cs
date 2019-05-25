using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance;

    public Item[] allAnimals;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }        
        else{
            Destroy(this.gameObject);
            return;
        }
    }

    public Item GetAnimalOfType(AnimalType type){
        Item result = null;
        foreach (var a in allAnimals)
        {
            if(a.animalType == type)
            {
                result = a;
                break;
            }
        }
        return result;
    }
    public Item GetRandom(){
        return allAnimals[UnityEngine.Random.Range(0, allAnimals.Length)];
    }
}
