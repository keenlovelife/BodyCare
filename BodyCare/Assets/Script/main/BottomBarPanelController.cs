using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarPanelController : MonoBehaviour {

    public static string TitleButton_Text = "请点击人体中任意位置";
    public static string TitleButton_Text_split = "可以拖拽您感兴趣的部位";
    public static string TitleButton_Text_brush = "可以在您感兴趣的部位坐标记";
    public static string TitleButton_Text_search = "可以在搜素框中输入您感兴趣的部位";

    public GameObject RightBarPanel, TopPanel, ContentPanel;
    public Button ReduceButton, UndoButton, TitleButton;
    public Text ContentPanel_panel_text;
    public Image IndicatorImage;
    public static BottomBarPanelController Instance { get { return _instance; } }
    static BottomBarPanelController _instance;
    private void Awake() { _instance = this; }

    void Start () {
        _layout();
        _events();
	}
    void _layout()
    {
        var bottomBarPanel_posy = (float)(46 / MainController.Mark667f) * Display.main.systemHeight;
        var bottomBarPanel_height = (float)Display.main.systemHeight * (306 / MainController.Mark667f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, bottomBarPanel_height);
        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, bottomBarPanel_posy, 0);
        ContentPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, -bottomBarPanel_posy);
        var topPanel_height = bottomBarPanel_posy;
        TopPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, topPanel_height);

        var reduceButton_width = (float)(71 / MainController.Mark375f) * Screen.width;

        var Button_Text_width = (float)(27 / MainController.Mark375f) * Screen.width;
        var Button_text_height = (float)(17.5 / MainController.Mark667f) * Screen.height;

        ReduceButton.GetComponent<RectTransform>().sizeDelta = new Vector2(reduceButton_width, 0);
        UndoButton.GetComponent<RectTransform>().sizeDelta = new Vector2(reduceButton_width, 0);
        TitleButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - reduceButton_width * 2, 0);

        ReduceButton.GetComponentInChildren<Text>().rectTransform.sizeDelta = new Vector2(0, Button_text_height);
        UndoButton.GetComponentInChildren<Text>().rectTransform.sizeDelta = new Vector2(0, Button_text_height);
        TitleButton.GetComponentInChildren<Text>().rectTransform.sizeDelta = new Vector2(0, Button_text_height-0.5f);

        TitleButton.GetComponentInChildren<Text>().text = TitleButton_Text;

        var width = (float)(11 / MainController.Mark375f) * Display.main.systemWidth;
        var height = (float)(8 / MainController.Mark667f) * Display.main.systemHeight;
        var i_posy = (9 / MainController.Mark667f) * Screen.height;
        IndicatorImage.rectTransform.sizeDelta = new Vector2(width, height);
        IndicatorImage.rectTransform.anchoredPosition3D = new Vector3(0, i_posy, 0);

        var fontsize = (int)(40 * (Screen.width / 1080.0f));
        ContentPanel_panel_text.fontSize = fontsize;

        var contentPanel_panel_width = ((375-30) / 375.0f) * Screen.width;
        ContentPanel.transform.Find("Panel").GetComponent<RectTransform>().sizeDelta = new Vector2(contentPanel_panel_width, 50);
    }
    void _events()
    {
        TitleButton.onClick.AddListener(() => {
            Debug.Log("===> 底部标题按钮触发！");

            if (MainController.Instance.EnableSearch|| MainController.Instance.EnableSplit|| MainController.Instance.EnableBrush)
                return;
            var t = TitleButton.GetComponentInChildren<Text>();
            MainController.Instance.BottomBarPanel_TitleButton_click(t.color.r == 1f, TitleButton);
            if (t.color.r == 1f)
            {
                t.color = MainController.SelectedColor;
                IndicatorImage.color = t.color;
                IndicatorImage.transform.rotation = Quaternion.identity;
            }
            else
            {
                t.color = MainController.NorColor;
                IndicatorImage.color = t.color;
                IndicatorImage.transform.Rotate(new Vector3(0, 0, 180));
            }
        });
        ReduceButton.onClick.AddListener(() => {
            Debug.Log("===> 还原按钮触发！");
            MainController.Instance.BottomBarPanel_ReButton_click();
        });
        UndoButton.onClick.AddListener(() => {
            Debug.Log("===> 撤销按钮触发！");
            MainController.Instance.BottomBarPanel_UndoButton_click();
        });
    }
	
	void Update () {

    }
}
