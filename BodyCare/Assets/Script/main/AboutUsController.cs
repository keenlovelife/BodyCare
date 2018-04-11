using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AboutUsController : MonoBehaviour {

    public Text TitleText,AppNameText,AppVerText,ComNameText,Tel_disc_Text,E_disc_text,Web_disc_Text,tel_value_text,e_value_text,web_vale_text;
    public Image backImage,IcoImage,tel_icoImae,E_icoImage,web_icoImage;
    public Button BackButton, TelButton,EButton,WebButton;

    public static AboutUsController Instance { get { return _instance; } }
    static AboutUsController _instance;
    private void Awake() { _instance = this; }

    int showRate = 15;

    bool canShow, canHide;
    private void OnEnable()
    {
        canShow = true;
        canHide = false;
    }
    private void OnDisable()
    {
        canShow = false;
        canHide = false;
    }

    void Start () {
        _layout();
        _events();
	}

    void _layout()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        var height = (float)(46 / MainController.Mark667f) * Screen.height;
        BackButton.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);

        var titleText_height = (22 / MainController.Mark667f) * Screen.height;
        var titleText_width = (67 / MainController.Mark375f) * Screen.width;
        TitleText.rectTransform.sizeDelta = new Vector2(titleText_width, titleText_height);

        backImage.rectTransform.sizeDelta = new Vector2(titleText_height, titleText_height);
        var width = (71 / MainController.Mark375f) * Screen.width;
        BackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 0);

        var icoImage_width = (float)((MainController.Mark375f - 144 * 2) / MainController.Mark375f) * Screen.width;
        var icoImage_posy = -(float)(131 / MainController.Mark667f) * Screen.height;
        IcoImage.rectTransform.sizeDelta = new Vector2(icoImage_width, icoImage_width);
        IcoImage.rectTransform.anchoredPosition3D = new Vector3(0, icoImage_posy, 0);

        var appname_width = Application.productName.Length / 4.0f *titleText_width;
        var appname_heigt = titleText_height;
        var appname_text_posy = -(20 / MainController.Mark667f) * Screen.height;
        AppNameText.rectTransform.sizeDelta = new Vector2(appname_width, appname_heigt);
        AppNameText.rectTransform.anchoredPosition3D = new Vector3(0, appname_text_posy, 0);

        var appVerText_width = Application.version.Length/4.0f*titleText_width;
        var appVertext_height = (17 / MainController.Mark667f) * Screen.height;
        var appverText_posy = -(4 / MainController.Mark667f) * Screen.height;
        AppVerText.rectTransform.sizeDelta = new Vector2(appVerText_width, appVertext_height);
        AppVerText.rectTransform.anchoredPosition3D = new Vector3(0, appname_text_posy, 0);
        AppVerText.text = Application.version;

        var conNameText_height = (17 / MainController.Mark667f) * Screen.height;
        var comNameText_posy = (14 / MainController.Mark667f) * Screen.height;
        ComNameText.rectTransform.sizeDelta = new Vector2(0, conNameText_height);
        ComNameText.rectTransform.anchoredPosition3D = new Vector3(0, comNameText_posy, 0);

        var tel_button_heigt = (47 / MainController.Mark667f) * Screen.height;
        var tel_posy = -(334 / MainController.Mark667f) * Screen.height;
        TelButton.GetComponent<RectTransform>().sizeDelta = new Vector2(0, tel_button_heigt);
        TelButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, tel_posy, 0);
        EButton.GetComponent<RectTransform>().sizeDelta = new Vector2(0, tel_button_heigt);
        EButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, tel_posy - tel_button_heigt, 0);
        WebButton.GetComponent<RectTransform>().sizeDelta = new Vector2(0, tel_button_heigt);
        WebButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, tel_posy - 2 * tel_button_heigt, 0);

        var _icoImage_posx = (20 / MainController.Mark375f) * Screen.width;
        var tel_ico_w = (14 / MainController.Mark375f) * Screen.width;
        var e_ico_w = (float)(17.2 / MainController.Mark375f) * Screen.width;
        var e_ico_h = (45.0f/54.0f) * e_ico_w;
        var web_ico_w = (float)(16.1 / MainController.Mark375f) * Screen.width;
        tel_icoImae.rectTransform.sizeDelta = new Vector2(tel_ico_w, tel_ico_w);
        tel_icoImae.rectTransform.anchoredPosition3D = new Vector3(_icoImage_posx, 0, 0);
        E_icoImage.rectTransform.sizeDelta = new Vector2(e_ico_w, e_ico_h);
        E_icoImage.rectTransform.anchoredPosition3D = new Vector3(_icoImage_posx, 0, 0);
        web_icoImage.rectTransform.sizeDelta = new Vector2(web_ico_w, web_ico_w);
        web_icoImage.rectTransform.anchoredPosition3D = new Vector3(_icoImage_posx, 0, 0);

        var _text_w = (60 / MainController.Mark375f) * Screen.width;
        var _text_h = (20 / MainController.Mark667f) * Screen.height;
        var _text_posx = (44 / MainController.Mark375f) * Screen.width;
        Tel_disc_Text.rectTransform.sizeDelta = new Vector2(_text_w, _text_h);
        Tel_disc_Text.rectTransform.anchoredPosition3D = new Vector3(_text_posx, 0, 0);
        E_disc_text.rectTransform.sizeDelta = new Vector2(_text_w, _text_h);
        E_disc_text.rectTransform.anchoredPosition3D = new Vector3(_text_posx, 0, 0);
        Web_disc_Text.rectTransform.sizeDelta = new Vector2(_text_w, _text_h);
        Web_disc_Text.rectTransform.anchoredPosition3D = new Vector3(_text_posx, 0, 0);

        var value_text_posx = -(20 / MainController.Mark375f) * Screen.width;
        var value_text_w = Screen.width + value_text_posx - _text_w - _text_posx;
        var value_text_h = _text_h;
        tel_value_text.rectTransform.sizeDelta = new Vector2(value_text_w, value_text_h);
        tel_value_text.rectTransform.anchoredPosition3D = new Vector3(value_text_posx, 0, 0);
        e_value_text.rectTransform.sizeDelta = new Vector2(value_text_w, value_text_h);
        e_value_text.rectTransform.anchoredPosition3D = new Vector3(value_text_posx, 0, 0);
        web_vale_text.rectTransform.sizeDelta = new Vector2(value_text_w, value_text_h);
        web_vale_text.rectTransform.anchoredPosition3D = new Vector3(value_text_posx, 0, 0);

        gameObject.SetActive(false);
    }
    void _events()
    {
        BackButton.onClick.AddListener(() => {
            canShow = false;
            canHide = true;
        });
    }

    void Update () {
        if (canShow)
        {
            var rt = GetComponent<RectTransform>();
            rt.anchoredPosition3D = Vector3.Lerp(rt.anchoredPosition3D, Vector3.zero, showRate * Time.deltaTime);
            if(rt.anchoredPosition3D.x < 1f)
            {
                canShow = false;
                rt.anchoredPosition3D = Vector3.zero;
            }
        }

        if (canHide)
        {
            var rt = GetComponent<RectTransform>();
            rt.anchoredPosition3D = Vector3.Lerp(rt.anchoredPosition3D, new Vector3(Screen.width,0,0), showRate * Time.deltaTime);
            if (rt.anchoredPosition3D.x > Screen.width - 1f)
            {
                canHide = false;
                rt.anchoredPosition3D = new Vector3(Screen.width, 0, 0);
                gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canHide = true;
            canShow = false;
        }
	}
}
