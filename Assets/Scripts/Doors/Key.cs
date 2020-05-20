using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    public enum KeyType
    {
        llave1,
        llave2,
        llave3
    }
    public KeyType GetKeyType()
    {
        return keyType;
    }
}
