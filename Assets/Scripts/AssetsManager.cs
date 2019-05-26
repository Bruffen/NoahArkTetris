using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance;

    public Animal[] allAnimals;

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

    public Animal GetAnimalOfType(AnimalType type){
        Animal result = null;
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
    public Animal GetRandom(){
        return allAnimals[UnityEngine.Random.Range(0, allAnimals.Length)];
    }
}
