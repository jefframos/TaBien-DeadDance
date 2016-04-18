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
            if(Animator != null)
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


    public GameObject GameHUD;
    public GameObject TopHUD;
    public GameObject InitHUD;

    public Animator GameHUDAnimator;
    public Animator ClosetUIAnimator;
    public Animator GameScreenAnimator;

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
        HideGameHUD();
    }
    public void HideGameHUD()
    {
        GameHUDAnimator.SetTrigger("ToOutGame");
        Invoke("disableGameHUD", 0.5f);

    }
    private void disableGameHUD()
    {
        print("disableGameHUD");
        GameHUD.SetActive(false);
    }
    public void AcceptMoreLife()
    {
        PreEndPopUp.Hide();
    }
    public void ToGame()
    {
        GameHUD.SetActive(true);
        TopHUD.SetActive(true);

        
        GameScreenAnimator.SetTrigger("ToGame");        
        ClosetUIAnimator.SetTrigger("TransitionOut");
        

    }
    public void ShowGameplayUI()
    {
        GameHUDAnimator.SetTrigger("ToGame");
    }
    public void ToShop()
    {
        InitHUD.SetActive(true);
        GameHUD.SetActive(true);
        GameScreenAnimator.SetTrigger("ToEditMode");
        //GameHUDAnimator.SetTrigger("ToOutGame");
        ClosetUIAnimator.SetTrigger("TransitionIn");

        Hide();

    }
    public void ToSectionSelected()
    {
        print("ToSectionSelected");
        GameScreenAnimator.SetTrigger("ToSectionSelected");
        ClosetUIAnimator.SetTrigger("ToSectionSelected");
    }
    public void ToHome()
    {
        print("ToHome");
        InitHUD.SetActive(true);
        GameHUD.SetActive(true);
        GameScreenAnimator.SetTrigger("ToStandard");
        //GameHUDAnimator.SetTrigger("ToOutGame");
        ClosetUIAnimator.SetTrigger("TransitionOut");

        Hide();

    }
}
