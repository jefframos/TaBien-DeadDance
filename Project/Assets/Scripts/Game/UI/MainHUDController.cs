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
            if(Animator != null && Animator.runtimeAnimatorController != null)
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
    public LogoController LogoController;

    public Animator GameHUDAnimator;
    public Animator ClosetUIAnimator;
    public Animator GameScreenAnimator;

    public ShopController ShopController;
    public PlayButtonController PlayButtonController;
    private bool inGame;

    enum CurrentStateHUD {ENVIRONMENT, SHOP };

    private CurrentStateHUD currentStateHUD;
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
        inGame = true;
        GameHUD.SetActive(true);
        TopHUD.SetActive(true);
        
        GameScreenAnimator.SetTrigger("ToGame");        
        ClosetUIAnimator.SetTrigger("TransitionOut");
        

    }
    public void UpdateTurboMode()
    {
        LogoController.UpdateLogoStatus();
        PlayButtonController.UpdateShape();
    }
    public void ShowGameplayUI()
    {
        GameHUDAnimator.SetTrigger("ToGame");
    }
    public void ToShop()
    {
        currentStateHUD = CurrentStateHUD.SHOP;
        InitHUD.SetActive(true);
        //GameHUD.SetActive(true);
        GameScreenAnimator.SetTrigger("ToEditMode");
        //GameHUDAnimator.SetTrigger("ToOutGame");
        ClosetUIAnimator.SetTrigger("TransitionIn");

        ShopController.Reset();
        if(inGame)
            Hide();

        inGame = false;

    }
    public void ToEnvironment()
    {
        currentStateHUD = CurrentStateHUD.ENVIRONMENT;
        InitHUD.SetActive(true);
        //GameHUD.SetActive(true);
        GameScreenAnimator.SetTrigger("ToEnvironment");

        ShopController.Reset();
        if (inGame)
            Hide();

        inGame = false;

    }
    public void ToSectionSelected()
    {
        //print("ToSectionSelected");
        GameScreenAnimator.SetTrigger("ToSectionSelected");
        ClosetUIAnimator.SetTrigger("ToSectionSelected");
        inGame = false;
    }
    public void ToHome()
    {
        print("ToHome");

        InitHUD.SetActive(true);
        GameHUD.SetActive(true);
        GameScreenAnimator.SetTrigger("ToStandard");
        //GameHUDAnimator.SetTrigger("ToOutGame");
        ClosetUIAnimator.SetTrigger("TransitionOut");

        if (inGame)
            Hide();

        inGame = false;

    }
    public void Back()
    {
        switch (currentStateHUD)
        {
            case CurrentStateHUD.ENVIRONMENT:
                GameScreenAnimator.SetTrigger("EndEnvironment");
                break;
            case CurrentStateHUD.SHOP:
                GameScreenAnimator.SetTrigger("EndEditMode");
                ClosetUIAnimator.SetTrigger("TransitionOut");
                break;
            default:
                GameScreenAnimator.SetTrigger("ToStandard");
                break;
        }
    }
}
