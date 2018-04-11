using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftPanelController : MonoBehaviour {

    const int brush_nor_sprite = 6;
    const int brush_selected_sprite = 7;
    const int split_nor_sprite = 4;
    const int split_selected_sprite = 5;
    const int search_nor_sprite = 2;
    const int search_selected_sprite = 3;

    public GameObject TopBarPanel;
    public Button SearchButton, SplitButton, ScreenshotButton, BrushButton;

    public static LeftPanelController Instance { get { return _instance; } }
    static LeftPanelController _instance;
    private void Awake() { _instance = this; }
    void Start () {
        _layout();
        _events();
	}
    void _layout()
    {
        var item_image_width = (40 / MainController.Mark375f) * Screen.width;
        var b_itemImage_ItemText_tmp = (10 / MainController.Mark667f) * Screen.height;
        var item_text_width = (27 / MainController.Mark375f) * Screen.width;
        var item_text_height = (20 / MainController.Mark667f) * Screen.height;

        var item_height = (70 / MainController.Mark667f) * Screen.height;
        var b_tmp = (30 / MainController.Mark667f) * Screen.height;

        var width = (float)(55 / MainController.Mark375f) * Screen.width;
        var height = item_height * 4 + b_tmp * 3;
        var posx = (float)((71 / 2.0f) / MainController.Mark375f) * Screen.width - item_image_width / 2.0f;
        GetComponent<RectTransform>().sizeDelta = new Vector2(item_image_width, height);
        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(posx, 0, 0);

        float leftBarPanel_item_posy = 0;
        var item_posy = (100 / MainController.Mark667f) * Screen.height;
        for (int i = 0; i < transform.childCount; i++) {
            var item = transform.GetChild(i);
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(item_image_width, item_image_width);
            item.GetComponentInChildren<Text>().rectTransform.sizeDelta = new Vector2(item_text_width, item_text_height);
            item.GetComponentInChildren<Text>().rectTransform.anchoredPosition3D = new Vector3(0, -b_itemImage_ItemText_tmp, 0);
            if (i == 1)
            {
                leftBarPanel_item_posy = -item_posy;
                item.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(item.GetComponent<RectTransform>().anchoredPosition3D.x, leftBarPanel_item_posy, 0);
            }
            else if (i == 2)
            {
                leftBarPanel_item_posy = item_posy;
                item.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(item.GetComponent<RectTransform>().anchoredPosition3D.x, leftBarPanel_item_posy+ b_itemImage_ItemText_tmp + item_text_height, 0);
            }else if (i == 3)
            {
                item.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0,b_itemImage_ItemText_tmp+item_text_height, 0);
            }
        }

        gameObject.SetActive(false);
    }
    void _events()
    {
        SearchButton.onClick.AddListener(() => {
            Debug.Log("===> 点击了搜索按钮！");
            var t = SearchButton.GetComponentInChildren<Text>();
            if (t.color.r == 1f)
            {
                t.color = MainController.SelectedColor;
                MainController.Instance.EnableSearch = true;
                SearchPanelController.Instance.gameObject.SetActive(true);

                if (LongPressPanelController.Instance.gameObject.activeSelf)
                {
                    LongPressPanelController.Instance.gameObject.SetActive(false);
                    var list = new List<GameObject>();
                    foreach (var o in MainController.Instance.MultipleChoiceObjects)
                        if (!o.activeSelf)
                            list.Add(o);
                    foreach (var o in list)
                        MainController.Instance.MultipleChoiceObjects.Remove(o);
                }

                foreach (var obj in MainController.Instance.MultipleChoiceObjects)
                {
                    var mr = obj.GetComponent<MeshRenderer>();
                    foreach (var material in mr.materials)
                        material.SetColor("_Color", new Color(1f, 1f, 1f, material.GetColor("_Color").a));
                }
                MainController.Instance.MultipleChoiceObjects.Clear();
                string name = string.Empty;
                var isOnly = MainController.Instance.isOnlySystemModelShow(out name);
                if (isOnly)
                    TopBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = name;
                else
                    TopBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = "人体";
                BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = BottomBarPanelController.TitleButton_Text_search;
                if(BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().color.r != 1f)
                {
                    MainController.Instance.BottomBarPanel_TitleButton_click(false, BottomBarPanelController.Instance.TitleButton);
                    BottomBarPanelController.Instance.ContentPanel_panel_text.text = "";
                    BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().color = MainController.NorColor;
                    BottomBarPanelController.Instance.IndicatorImage.color = MainController.NorColor;
                }
                if (RightBarController.Instance.gameObject.activeSelf)
                {
                    RightBarController.Instance.gameObject.SetActive(false);
                    TopBarPanelController.Instance.Button.GetComponentInChildren<Text>().color = MainController.NorColor;
                }
                if (MainController.Instance.EnableBrush)
                {
                    MainController.Instance.EnableBrush = false;
                    BrushButton.GetComponentInChildren<Text>().color = MainController.NorColor;
                }
                if (MainController.Instance.EnableSplit)
                {
                    MainController.Instance.EnableSplit = false;
                    SplitButton.GetComponentInChildren<Text>().color = MainController.NorColor;
                }
            }
            else
            {
                t.color = MainController.NorColor;
                MainController.Instance.EnableSearch = false;
                SearchPanelController.Instance.gameObject.SetActive(false);
                BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = BottomBarPanelController.TitleButton_Text;
            }
        });
        SplitButton.onClick.AddListener(() => {
            Debug.Log("===> 点击了拆分按钮！");
            var t = SplitButton.GetComponentInChildren<Text>();
            if (t.color.r == 1f)
            {
                t.color = MainController.SelectedColor;
                MainController.Instance.EnableSplit = true;
                if (LongPressPanelController.Instance.gameObject.activeSelf)
                {
                    LongPressPanelController.Instance.gameObject.SetActive(false);
                    var list = new List<GameObject>();
                    foreach (var o in MainController.Instance.MultipleChoiceObjects)
                        if (!o.activeSelf)
                            list.Add(o);
                    foreach (var o in list)
                        MainController.Instance.MultipleChoiceObjects.Remove(o);
                }

                foreach (var obj in MainController.Instance.MultipleChoiceObjects)
                {
                    var mr = obj.GetComponent<MeshRenderer>();
                    foreach (var material in mr.materials)
                        material.SetColor("_Color", new Color(1f, 1f, 1f, material.GetColor("_Color").a));
                }
                MainController.Instance.MultipleChoiceObjects.Clear();
                string name = string.Empty;
                var isOnly = MainController.Instance.isOnlySystemModelShow(out name);
                if (isOnly)
                    TopBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = name;
                else
                    TopBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = "人体";
                BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = BottomBarPanelController.TitleButton_Text_split;
                if (BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().color.r != 1f)
                {
                    MainController.Instance.BottomBarPanel_TitleButton_click(false, BottomBarPanelController.Instance.TitleButton);
                    BottomBarPanelController.Instance.ContentPanel_panel_text.text = "";
                    BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().color = MainController.NorColor;
                    BottomBarPanelController.Instance.IndicatorImage.color = MainController.NorColor;
                }
                if (RightBarController.Instance.gameObject.activeSelf)
                {
                    RightBarController.Instance.gameObject.SetActive(false);
                    TopBarPanelController.Instance.Button.GetComponentInChildren<Text>().color = MainController.SelectedColor;
                }
                if (MainController.Instance.EnableBrush)
                {
                    MainController.Instance.EnableBrush = false;
                    BrushButton.GetComponentInChildren<Text>().color = MainController.NorColor;
                }
                if (MainController.Instance.EnableSearch)
                {
                    MainController.Instance.EnableSearch = false;
                    SearchButton.GetComponentInChildren<Text>().color = MainController.NorColor;
                    SearchPanelController.Instance.gameObject.SetActive(false);
                }
            }
            else
            {
                t.color = MainController.NorColor;
                MainController.Instance.EnableSplit = false;
                MainController.Instance.rayobj = null;
                BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = BottomBarPanelController.TitleButton_Text;
            }
        });
        ScreenshotButton.onClick.AddListener(() => {
            Debug.Log("===> 点击了截屏按钮！");

            var t = ScreenshotButton.GetComponentInChildren<Text>();
            if(t.color.r == 1f)
            {
                t.color = MainController.SelectedColor;
            }else
            {
                t.color = MainController.NorColor;
            }

        });
        BrushButton.onClick.AddListener(() => {
            Debug.Log("===> 点击了画笔按钮！");
            var t = BrushButton.GetComponentInChildren<Text>();
            if (t.color.r == 1f)
            {
                t.color = MainController.SelectedColor;
                MainController.Instance.EnableBrush = true;

                foreach (var obj in MainController.Instance.MultipleChoiceObjects)
                {
                    var mr = obj.GetComponent<MeshRenderer>();
                    foreach (var material in mr.materials)
                        material.SetColor("_Color", new Color(1f, 1f, 1f, material.GetColor("_Color").a));
                }
                MainController.Instance.MultipleChoiceObjects.Clear();
                string name = string.Empty;
                var isOnly = MainController.Instance.isOnlySystemModelShow(out name);
                if (isOnly)
                    TopBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = name;
                else
                    TopBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = "人体";
                BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = BottomBarPanelController.TitleButton_Text_brush;
                if (BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().color.r != 1f)
                {
                    MainController.Instance.BottomBarPanel_TitleButton_click(false, BottomBarPanelController.Instance.TitleButton);
                    BottomBarPanelController.Instance.ContentPanel_panel_text.text = "";
                    BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().color = MainController.NorColor;
                    BottomBarPanelController.Instance.IndicatorImage.color = MainController.NorColor;
                }
                if (MainController.Instance.EnableSplit)
                {
                    SplitButton.GetComponentInChildren<Text>().color = MainController.NorColor;    
                    MainController.Instance.EnableSplit = false;
                }
                if (MainController.Instance.EnableSearch)
                {
                    MainController.Instance.EnableSearch = false;
                    SearchButton.GetComponentInChildren<Text>().color = MainController.NorColor;
                    SearchPanelController.Instance.gameObject.SetActive(false);
                }
            }
            else
            {
                t.color = MainController.NorColor;
                MainController.Instance.EnableBrush = false;
                BottomBarPanelController.Instance.TitleButton.GetComponentInChildren<Text>().text = BottomBarPanelController.TitleButton_Text;
            }
        });
    }


    void Update () {
		
	}
}
