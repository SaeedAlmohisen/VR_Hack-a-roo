using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    //public bool showController = false;
    public GameObject handModelPrefab;

    private GameObject spawnedHandModel;

    // Start is called before the first frame update
    void Start()
    {
        spawnedHandModel = Instantiate(handModelPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        spawnedHandModel.SetActive(true);
    }
}
