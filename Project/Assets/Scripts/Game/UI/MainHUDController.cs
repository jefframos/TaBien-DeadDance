using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
public class MainHUDController : MonoBehaviour {
    [Serializable]
    public class PopUpModel
    {
        public RectTransform Container;
        public Animator Animator;
        private bool reseted;
        public void Reset()
        {
            Animator.gameObject.SetActive(false);
            Container.gameObject.SetActive(false);
            Animator.Stop();
            reseted = true;
        }
        public void Show()
        {
            print(this);
            reseted = false;
            Animator.gameObject.SetActive(true);
            Container.gameObject.SetActive(true);
            Animator.Play("TransitionIn");
        }
        public void Update()
        {
            if (reseted)
            {
                return;
            }
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Out"))
            {
                Reset();
            }
        }
        public void Hide()
        {
            Animator.Play("TransitionOut");
        }
        public void Change(PopUpModel next)
        {
            Hide();
            next.Show();
        }
    }
    public PopUpModel PreEndPopUp;
    public PopUpModel EndPopUp;
    
    // Use this for initialization
    void Awake()
    {
        Reset();
    }
    public void Reset()
    {
        PreEndPopUp.Reset();
        EndPopUp.Reset();
    }
    public void Hide()
    {
        PreEndPopUp.Hide();
        EndPopUp.Hide();
    }
    public void ShowPreEndGame()
    {
        PreEndPopUp.Show();
    }
    void Update()
    {
        PreEndPopUp.Update();
        EndPopUp.Update();
    }
    public void ShowEndGame()
    {
        PreEndPopUp.Change(EndPopUp);
    }
}
