using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shance : MonoBehaviour {

    public static bool Chance(int curChance)
    {
        int[] num = new int[100];

        for (int i = 0; i < 100; i++)
            num[i] = 0;

        for (int i = 0; i < curChance; i++)
            num[i] = 1;
        
        if (num[Random.Range(0, 99)] == 1)
            return true;
        else return false;
    }
}
