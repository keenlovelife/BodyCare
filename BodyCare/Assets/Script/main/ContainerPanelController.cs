using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPanelController : MonoBehaviour {

	void Start () {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 0);
        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(Screen.width, 0, 0);
        canShow = true;
	}

    bool canShow = false;
    int showRate = 20;
    private void FixedUpdate()
    {
        if (canShow)
        {
            GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(GetComponent<RectTransform>().anchoredPosition3D, Vector3.zero, showRate * Time.fixedDeltaTime);
            if (GetComponent<RectTransform>().anchoredPosition3D.x < 1f)
            {
                if (!RightBarController.Instance.bodyCare.activeSelf)
                    RightBarController.Instance.bodyCare.SetActive(true);
                canShow = false;
                GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            }
        }
    }
}
