using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.keenlove.unity.c_sharp
{
    public delegate void Action_1_param<T>(T obj);
    public class ViewController : MonoBehaviour
    {
        public virtual void _Init() { }
        public virtual void _UI_Init() { }
        public virtual void _UI_layout() { }
        public virtual void _UI_event() { }
    }
}