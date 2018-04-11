using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

namespace com.keenlove.unity.c_sharp.tool
{
    public class Tool
    {
        public static void GameObject_eachChild(GameObject gameObject, bool isAll, Action_1_param<GameObject> action)
        {
            if (action == null)
                return;
            if (gameObject == null)
            {
                var all = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
                foreach (var o in all)
                {
                    if (isAll)
                        action(o);
                    else if (o.transform.parent == null)
                        action(o);
                }
            }
            else
            {
                for (int i = 0; i < gameObject.transform.childCount; ++i)
                    action(gameObject.transform.GetChild(i).gameObject);
                if (isAll)
                    for (int i = 0; i < gameObject.transform.childCount; ++i)
                        GameObject_eachChild(gameObject.transform.GetChild(i).gameObject, true, action);
            }
        }

#if UNITY_IPHONE
                [DllImport("__Internal")]
        static extern void __savePhoto_formUnity(string path);
#endif
        public static void Screenshot()
        {
            System.DateTime now = new System.DateTime();
            now = System.DateTime.Now;
            string filename = string.Empty;

            filename = string.Format("Screenshot_{0}-{1}-{2}-{3}-{4}-{5}.png",
                                            now.Year, now.Month, now.Day,
                                            now.Hour, now.Minute, now.Second);

            ScreenCapture.CaptureScreenshot(filename);
            string path = string.Empty;



#if UNITY_EDITOR
            path = Application.dataPath.Replace("Assets", "") + "/" + filename;
            MianController.Instance.Screenshot(path,() =>
            {
                var movedpath = "C:\\Users\\王庆东\\Desktop\\Screenshot";
                if (!File.Exists(movedpath))
                    Directory.CreateDirectory(movedpath);
                var savepath = movedpath + "/" + filename;
                File.Move(path, savepath);
                MianController.Instance.ScreenshotSaved(savepath);
            });
#else
#if UNITY_IPHONE
            __savePhoto_formUnity(path);
#else
#if UNITY_ANDROID
                        path = Application.persistentDataPath + "/" + filename;
            MianController.Instance.Screenshot(path, () =>
            {
                string savepath = "/sdcard/" + Application.productName,
                savepath1 = "/sdcard/Pictures/Screenshots",
                savepath2 = "/sdcard/DCIM/Screenshots";
                if (!Directory.Exists(savepath1))
                {
                    if (!Directory.Exists(savepath2))
                    {
                        if (!Directory.Exists(savepath))
                            Directory.CreateDirectory(savepath);
                    }
                    else
                        savepath = savepath2;
                }
                else
                    savepath = savepath1;
                savepath += "/" + filename;
                File.Move(path, savepath);
                MianController.Instance.ScreenshotSaved(savepath);
                AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaClass classMedia = new AndroidJavaClass("android.media.MediaScannerConnection");
                classMedia.CallStatic("scanFile", new object[4] { objActivity,
                new string[]{ savepath },
                new string[]{"image/png" },
                null});
            });
#endif
#endif
#endif
        }
    }
}
