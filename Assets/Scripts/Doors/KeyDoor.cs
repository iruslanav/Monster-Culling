using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType1;
    [SerializeField] private Key.KeyType keyType2;
    [SerializeField] private Key.KeyType keyType3;

    private Door door;

    private void Awake()
    {
        door = gameObject.GetComponent<Door>();
    }
    public Key.KeyType GetKeyType1()
    {

        return keyType1;
    }
    public Key.KeyType GetKeyType2()
    {

        return keyType2;
    }
    public Key.KeyType GetKeyType3()
    {

        return keyType3;
    }
    public void OpenDoor()
    {
        Debug.Log("door down");
        door.DownDoor();

    }
}
