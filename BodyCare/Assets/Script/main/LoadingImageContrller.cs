using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingImageContrller : MonoBehaviour {

    private void OnEnable()
    {
        can = true;
    }

    private void OnDisable()
    {
        can = false;
    }

    UnityEngine.UI.Image image;
    bool can;

    public float Speed = 30;

    void Start () {
        image = GetComponent<UnityEngine.UI.Image>();
        image.transform.rotation = Quaternion.identity;
	}


    void Update () {

        if (can)
        {
            image.transform.Rotate(new Vector3(0, 0, -Speed*10 * Time.deltaTime));
        }
	}
}
