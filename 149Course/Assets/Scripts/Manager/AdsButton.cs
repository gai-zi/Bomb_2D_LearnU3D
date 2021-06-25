//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Advertisements;

//public class AdsButton : MonoBehaviour,IUnityAdsListener
//{
//#if UNITY_IOS
//    private string gameId = "4150074";
//#elif UNITY_ANDROID
//    private string gameId = "4150075";
//#endif
//    Button adsbutton;
//    public string placementID = "rewardedVideo";
//    void Start()
//    {
//        adsbutton = GetComponent<Button>();
//        //adsbutton.interactable = Advertisement.IsReady(placementID);    //查看是否加载好了

//        if (adsbutton)
//            adsbutton.onClick.AddListener(ShowRewardAds);

//        Advertisement.AddListener(this);
//        Advertisement.Initialize(gameId, true);
//    }
//    public void ShowRewardAds()
//    {
//        Advertisement.Show(placementID);
//    }
//    public void OnUnityAdsDidError(string message)
//    {
        
//    }

//    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
//    {
//        switch (showResult)
//        {
//            case ShowResult.Failed:
//                break;
//            case ShowResult.Skipped:
//                break;
//            case ShowResult.Finished:
//                FindObjectOfType<PlayerController>().health = 3;
//                FindObjectOfType<PlayerController>().isDead = false;
//                UIManager.instance.UpdateHealth(FindObjectOfType<PlayerController>().health);
//                FindObjectOfType<PlayerController>().transform.tag = "Player";
//                break;
            
//        }
//    }

//    public void OnUnityAdsDidStart(string placementId)
//    {
        
//    }

//    public void OnUnityAdsReady(string placementId)
//    {

//    }


//}
