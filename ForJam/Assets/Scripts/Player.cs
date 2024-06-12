using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterData data;

    private void Start()
    {
        data = GetComponent<CharacterData>();
    }
}
