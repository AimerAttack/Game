using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

class AddressableTest : MonoBehaviour
{
    public List<AssetReference> m_Characters;
    private bool m_AssetsReady;
    private int m_ToLoadCount;
    private int m_CharacterIndex;

    public string path;
    private AddressableLoader loader;
    
    private List<object> updateKeys = new List<object>();
    
    void Start()
    {
        loader = new AddressableLoader(path, OnLoadOK);
        loader.Start();
    }

    async void UpdateCatalog()
    {
        await Addressables.InitializeAsync().Task;
        var handle = Addressables.CheckForCatalogUpdates(false);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var catalogs = handle.Result;
            if (catalogs != null && catalogs.Count > 0)
            {
                var updateHandle = Addressables.UpdateCatalogs(catalogs,false);   
                await updateHandle.Task;
                foreach (var item in updateHandle.Result)
                {
                    updateKeys.AddRange(item.Keys);
                }

                Download();
            }
            else
            {
                //Don't need update catalogs
            }
        }

        Addressables.Release(handle);
    }

    void Download()
    {
        StartCoroutine(DownAssetImpl());
    }

    IEnumerator DownAssetImpl()
    {
        var downloadSize = Addressables.GetDownloadSizeAsync(updateKeys);
        yield return downloadSize;
        Debug.Log($"need download size={downloadSize.Result}");
        if (downloadSize.Result > 0)
        {
            var download = Addressables.DownloadDependenciesAsync(updateKeys, Addressables.MergeMode.Union);
            yield return download;
            // foreach (var item in download.Result as
            //     List<UnityEngine.ResourceManagement.ResourceProviders.IAssetBundleResource>)
            // {
            //     var ab = item.GetAssetBundle();
            // }
            Addressables.Release(download);
        }

        Addressables.Release(downloadSize);
    }
    
    private void OnLoadOK(Object obj)
    {
        Instantiate(obj);
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            loader.Release();   
        }
    }
}

class AddressableLoader
{
    private string path;
    private Action<Object> complete;
    private AsyncOperationHandle<Object> handle;
    private AsyncOperationHandle<Object> loader;
    public AddressableLoader(string _path,Action<Object> _complete)
    {
        path = _path;
        complete = _complete;
    }

    void OnComplete(AsyncOperationHandle<Object> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            handle = obj;
            if (complete != null)
                complete(obj.Result);
        }
        else
        {
            //
        }
    }

    public void Start()
    {
        loader = Addressables.LoadAssetAsync<Object>(path);
        loader.Completed += OnComplete;
    }

    public void Release()
    {
        // Addressables.Release(handle);
        Addressables.Release(loader);
    }
    
    
    
}