using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControlPanelController : MonoBehaviour {

    public static AnimationControlPanelController Instance { get { return _instance; } }
    static AnimationControlPanelController _instance;
    private void Awake() { _instance = this; }


    public Button PlayOrParseButton, StopButton;
    public Sprite[] Sprites;

    public Animator animator;
    AudioSource audioSource;
    void Start () {
        audioSource = GetComponent<AudioSource>();
        _layout();
        _events();
	}

    void _layout()
    {
        animator = GameObject.Find("BodyCare").GetComponent<Animator>();
        var animatorinfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!animatorinfo.IsName("stop"))
            animator.Play("stop");
        var width = (27 / MainController.Mark375f) * Screen.width;
        var height = 2.5f * width;
        var posx = -(71 / MainController.Mark375f) * Screen.width / 2.0f;
        var rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
        rt.anchoredPosition3D = new Vector3(posx, 0,0);
        PlayOrParseButton.GetComponent<RectTransform>().sizeDelta = new Vector2(width, width);
        StopButton.GetComponent<RectTransform>().sizeDelta = new Vector2(width, width);
        gameObject.SetActive(false);
    }
    void _events()
    {
        PlayOrParseButton.onClick.AddListener(() => {
            Debug.Log("===> 播放和暂停按钮被点击了！");
            var animatorinfo = animator.GetCurrentAnimatorStateInfo(0);
            if (PlayOrParseButton.image.sprite == Sprites[0])
            {
                animator.Play("Take 001");
                PlayOrParseButton.image.sprite = Sprites[1];
                audioSource.Play();
            }
            else
            {
                PlayOrParseButton.image.sprite = Sprites[0];
                animator.Play("stop");
                audioSource.Stop();
            }
        });
        StopButton.onClick.AddListener(() => {
            Debug.Log("===> 停止按钮被点击了!");
            var animatorinfo = animator.GetCurrentAnimatorStateInfo(0);
            if(PlayOrParseButton.image.sprite == Sprites[1])
            {
                PlayOrParseButton.image.sprite = Sprites[0];
                animator.Play("stop");
                audioSource.Stop();
            }
            MainController.Instance.animationStop();
        });
    }

    void Update () {
		
	}
}
