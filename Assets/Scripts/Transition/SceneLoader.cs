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

    [Header("�¼�����")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    [Header("�㲥")]
    public VoidEventSO aftersSceneLoadedEvent;

    public FadeEventSO fadeEvent;
    [SerializeField]private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    private bool isLoading;
    public float fadeDuration;
    private void Awake()
    {
        //�첽���س���
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        //currentLoadedScene = firstLoadScene;
        //currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        DontDestroyOnLoad(GameObject.Find("dina"));
    }
    //TODO:����menu���������
    public void Start()
    {
        NewGame();
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void onDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }

    private void NewGame() 
    {
        sceneToLoad = firstLoadScene;
        OnLoadRequestEvent(sceneToLoad, firstPosition, true);
    }

    /// <summary>
    /// ���������¼�����    
    /// </summary>
    /// <param name="locationToLoad"></param>
    /// <param name="posToGo"></param>
    /// <param name="fadeScreen"></param>
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        //��ϣ�����������e�л�����
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
    /// ����ִ��ж��
    /// </summary>
    /// <returns></returns>
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO:ʵ�ֽ��뽥��
            fadeEvent.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);

        yield return currentLoadedScene.sceneReference.UnLoadScene();
        //�ر�����
        playerTrans.gameObject.SetActive(false);
        //�����³���
        LoadNewScene();
    }
    
    /// <summary>
    /// �����µĳ���
    /// </summary>
    private void LoadNewScene ()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
        //����������Ϻ�ִ��һЩ��������
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadedScene = sceneToLoad;
        playerTrans.position = positionToGo;
        playerTrans.gameObject.SetActive(true);

        if (fadeScreen)
        {
            //FadeEvent.FadeOut(fadeDuration);
        }

        isLoading = false;

        //�����������֮���¼�
        aftersSceneLoadedEvent.RaiseEvent();
    }


}
