using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOptions : MonoBehaviour {

    public BindOption optionPrefab;
    private BindOption[] inSceneOptions;
    public float yOffset;

    void Start () {
        inSceneOptions = new BindOption[InputController.instance.binds.Length];
        for(int i = 0; i < InputController.instance.binds.Length; i++)
        {
            inSceneOptions[i] = Instantiate(optionPrefab,gameObject.transform);
            inSceneOptions[i].transform.position += new Vector3(0, yOffset * i, 0);
            inSceneOptions[i].key = InputController.instance.binds[i];
            inSceneOptions[i].SetUp();
        }
	}
}
