using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingController : com.keenlove.unity.c_sharp.ViewController {

    public static bool can;
    Image loadImage,progressImage,LoadingImage;
    Text progressText, titleText;


    public static LoadingController Instance { get { return _instance; } }
    static LoadingController _instance;
    private void Awake() { _instance = this; }

    public override void _Init()
    {
        Debug.Log("===> LoadingController _Init(): 屏幕尺寸：" + Screen.width + "," + Screen.height);

        loadImage = transform.Find("loadImage").GetComponent<Image>();
        LoadingImage = transform.Find("loadingImage").GetComponent<Image>();
        progressImage = loadImage.transform.Find("progressImage").GetComponent<Image>();
        progressText = loadImage.transform.Find("progressText").GetComponent<Text>();
        titleText = transform.Find("titleText").GetComponent<Text>();

        _UI_layout();
    }
    public override void _UI_layout()
    {
        Debug.Log("===> LoadingController _UI_layout()");
        // loadingIamge
        var loadingImage_w = Screen.width;
        var loadingImage_h = (1920.0f / 1080) * loadingImage_w;
        LoadingImage.rectTransform.sizeDelta = new Vector2(loadingImage_w, loadingImage_h);
        Debug.Log("===> LoadingController _UI_layout() loadingIamge");

        // loadImage
        var loadImage_width = (float)(796 / 1080.0f) * Display.main.systemWidth;
        var loadImage_height = (float)(43 / 796.0) * loadImage_width;
        var loadImage_posy = (float)(336.5f / 1920.0f) * Display.main.systemHeight;
        loadImage.rectTransform.sizeDelta = new Vector2(loadImage_width, loadImage_height);
        loadImage.rectTransform.anchoredPosition3D = new Vector3(0, loadImage_posy, 0);
        Debug.Log("===> LoadingController _UI_layout() loadImage");

        // progressImage
        var right = -loadImage_width;
        progressImage.rectTransform.offsetMax = new Vector2(right, 0);
        Debug.Log("===> LoadingController _UI_layout() progressImage");

        // progressText
        var progressText_height = (float)(33 / 1920.0f) * Display.main.systemHeight;
        var progressText_posy = -(float)(20 / 1920.0f) * Display.main.systemHeight;
        progressText.rectTransform.sizeDelta = new Vector2(progressText.rectTransform.sizeDelta.x, progressText_height);
        progressText.rectTransform.anchoredPosition3D = new Vector3(0, progressText_posy, 0);
        Debug.Log("===> LoadingController _UI_layout() progressText");

        // titleText
        var titleText_height = progressText_height;
        var titleText_posy = loadImage_posy + loadImage_height + loadImage_height + (-progressText_posy)+progressText_height;
        titleText.rectTransform.sizeDelta = new Vector2(titleText.rectTransform.sizeDelta.x, titleText_height);
        titleText.rectTransform.anchoredPosition3D = new Vector3(0, titleText_posy, 0);

        Debug.Log("===> LoadingController _UI_layout() 完成");
    }
    public void SetProgress(float progress)
    {
        if(progress <= 1f)
        {
            var right = -(1 - progress) * loadImage.rectTransform.rect.width;
            progressImage.rectTransform.offsetMax = new Vector2(right, 0);
            progressText.text = string.Format("{0}%", System.Math.Round(progress * 100, 0));
        }

    }
    void loadAssetBundle(string path, string who)
    {
        Debug.Log("===> LoadingController loadAssetBundle()");

        var p = Application.streamingAssetsPath + "/" + path;
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.IPhonePlayer)
            p = "file://" + p;
        StartCoroutine(_loadAssetBundle(p, who));
    }

    GameObject bodyCare;
    IEnumerator _loadAssetBundle(string path, string who)
    {
        Debug.Log("===> LoadingController _loadAssetBundle()");
        var www = new WWW(path);
        yield return www;
        var obj = www.assetBundle.LoadAsset<GameObject>(who);
        var o = Instantiate(obj);
        bodyCare = o;
        o.name = "BodyCare";
        o.transform.position = new Vector3(0, 1.4f, -7.5f);
        for (int i = 0; i < o.transform.childCount; ++i)
        {
            var t = o.transform.GetChild(i);
                t.gameObject.SetActive(false);
        }
        DontDestroyOnLoad(o);
        MainController.bodyCare = o;
        www.assetBundle.Unload(false);
        if(MainController.bodyCare)
            Debug.Log("===> LoadingController 加载模型成功："+MainController.bodyCare.name + " " + o.name);
        Debug.Log("===> LoadingController 加载模型完毕：" + MainController.bodyCare.name+" "+o.name);
    }
    void Start () {
        _Init();
        StartCoroutine(loadmodel(0.1f));
    }
    IEnumerator loadmodel(float wait)
    {
        yield return new WaitForSecondsRealtime(wait);
        loadAssetBundle("dissect/body", "quan");
    }
    void Update () {

    }
    float time = 0f;
    float maxTiem = 2f;
    private void FixedUpdate()
    {
        if (!can)
            return;
        if (time < maxTiem)
        {
            time += Time.fixedDeltaTime;
            SetProgress(time / maxTiem);
        }
        else
        {
            Debug.Log("===> 转到main场景！");
            UnityEngine.SceneManagement.SceneManager.LoadScene("main");
        }
    }
}