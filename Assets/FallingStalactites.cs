using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStalactites : MonoBehaviour
{
    [SerializeField] private GameObject stalactite;
    [SerializeField] List<Transform> positions = new List<Transform>();
    private float spawningTime;
    private int listIndex;
    private int randomNumber;
    void Start()
    {
        spawningTime = Random.Range(5f,10f);
    }

    // Update is called once per frame
    void Update()
    {
        spawningTime -= Time.deltaTime;
        if (spawningTime<= 0)
        {

            randomNumber = Random.Range(1,15);
            for (int i = 0; i < randomNumber; i++)
            {
                listIndex = Random.Range(0, positions.Count);
                Instantiate(stalactite, positions[listIndex].position, Quaternion.identity);
            }
            spawningTime = Random.Range(0.2f, 0.7f);
        }
    }
}
