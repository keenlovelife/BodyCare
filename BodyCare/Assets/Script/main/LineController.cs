using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LineController : MonoBehaviour {

    public bool Enable;
    LineRenderer lineRenderer;
    List<Vector3> points;

    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = new Color(1, 0, 0);
        points = new List<Vector3>();
    }
    public void AddPoint(Vector3 newPoint)
    {
        if (RightBarController.Instance.gameObject.activeSelf)
        {
            RightBarController.Instance.gameObject.SetActive(false);
            TopBarPanelController.Instance.Button.GetComponentInChildren<Text>().color = MainController.NorColor;
        }
        if (SubMenusPanelController.Instance.gameObject.activeSelf)
        {
            SubMenusPanelController.Instance.CanHide = true;
            SubMenusPanelController.Instance.CanShow = false;
            SubMenusPanelController.Instance.MenuButton.GetComponentInChildren<Text>().color = MainController.NorColor;
        }

        bool canadd = true;
        foreach(var p in points)
            if(p == newPoint)
            {
                canadd = false;
                break;
            }
        if(canadd)
        {
            points.Add(newPoint);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }
    public void ClearAllPoint()
    {
        lineRenderer.positionCount = 0;
        points.Clear();
    }

    Vector3 mouseBeginPos;
    private void Update()
    {
        if(Enable && !MainController.Instance.isOnUI)
        {
           
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
                AddPoint(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)));
#else
#if UNITY_IPHONE || UNITY_ANDROID
           if(Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Moved)
                AddPoint(Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 1)));
#endif
#endif
            else
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
#else
#if UNITY_IPHONE || UNITY_ANDROID
           if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
#endif
#endif
            {
                Enable = false;
                var root = GameObject.Find("lineRoot");
                var line = Instantiate(Resources.Load<GameObject>("line"));
                line.name = "line";
                line.transform.SetParent(root.transform);
                var lineController = line.GetComponent<LineController>();
                lineController.Enable = true;
                line.GetComponent<LineRenderer>().material.SetColor("_Color", MainController.Instance.BrushColor);
                line.GetComponent<LineRenderer>().positionCount = 0;

            }
        }
    }
}
