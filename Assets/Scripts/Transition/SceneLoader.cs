using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 firstPosition;
    public Vector3 menuPosition;


    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO newGameEvent;


    [Header("广播")]
    public VoidEventSO aftersSceneLoadedEvent;

    public FadeEventSO fadeEvent;
    public SceneLoadEventSO unloadedSceneEvent;

    [Header("场景")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;
    [SerializeField]private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    private bool isLoading;
    public float fadeDuration;
    private void Awake()
    {
        //异步加载场景
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        //currentLoadedScene = firstLoadScene;
        //currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
       
    }
    //TODO:做完menu后回来更改
    public void Start()
    {
        //NewGame();
        loadEventSO.RaiseLoadRequestEvent(menuScene, menuPosition, true);
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
        newGameEvent.OnEventRaised += NewGame;
    }

    private void onDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.OnEventRaised -= NewGame;
    }

    private void NewGame() 
    {
        sceneToLoad = firstLoadScene;
        //OnLoadRequestEvent(sceneToLoad, firstPosition, true);
        //加载第一个游戏场景
        loadEventSO.RaiseLoadRequestEvent(sceneToLoad, firstPosition, true);
        
    }

    /// <summary>
    /// 场景加载事件请求    
    /// </summary>
    /// <param name="locationToLoad"></param>
    /// <param name="posToGo"></param>
    /// <param name="fadeScreen"></param>
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        //不希望玩家连续按e切换场景
        if (isLoading)
            return;
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        if (currentLoadedScene != null)
            StartCoroutine(UnLoadPreviousScene());
        else
            LoadNewScene();

        Debug.Log(sceneToLoad.sceneReference.SubObjectName);
    }

    /// <summary>
    /// 场景执行卸载
    /// </summary>
    /// <returns></returns>
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO:实现渐入渐出
            fadeEvent.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);

        //广播事件调整血条显示
        unloadedSceneEvent.RaiseLoadRequestEvent(sceneToLoad,positionToGo,true);


        yield return currentLoadedScene.sceneReference.UnLoadScene();
        //关闭人物
        //playerTrans.gameObject.SetActive(false);
        //加载新场景
        LoadNewScene();
    }
    
    /// <summary>
    /// 加载新的场景
    /// </summary>
    private void LoadNewScene ()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
        //场景加载完毕后执行一些函数方法
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// 场景加载完成
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadedScene = sceneToLoad;
        playerTrans.position = positionToGo;
        //playerTrans.gameObject.SetActive(true);
        Debug.Log("场景加载完成");

        if (fadeScreen)
        {
            fadeEvent.FadeOut(fadeDuration);
        }

        isLoading = false;

        if(currentLoadedScene.sceneType == SceneType.Loaction)
        //场景加载完成之后事件
            aftersSceneLoadedEvent.RaiseEvent();
    }


}
