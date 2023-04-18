using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState : MonoBehaviour
{
    public enum State { Buy, Equip, Equiped}

    public GameObject[] btnObjs;

    public void SetState(State state)
    {
        for(int i = 0; i < btnObjs.Length; i++)
        {
            btnObjs[i].SetActive(false);
        }
        btnObjs[(int)state].SetActive(true);
    }
}
