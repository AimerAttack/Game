using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

class Test : MonoBehaviour
{
    public List<AssetReference> m_Characters;
    private bool m_AssetsReady;
    private int m_ToLoadCount;
    private int m_CharacterIndex;
    
    void Start()
    {
        m_ToLoadCount = m_Characters.Count;
        foreach (var character in m_Characters)
        {
            character.LoadAssetAsync<GameObject>().Completed += OnCharacterAssetLoaded;
        }
    }

    public void SpawnCharacter(int characterType)
    {
        if (m_AssetsReady)
        {
            var position = Random.insideUnitSphere * 5;
            position.Set(position.x,0,position.z);
            m_Characters[characterType].InstantiateAsync(position, Quaternion.identity);
        }
    }
    
    private void OnCharacterAssetLoaded(AsyncOperationHandle<GameObject> obj)
    {
        m_ToLoadCount--;
        if (m_ToLoadCount == 0)
            m_AssetsReady = true;
    }

    private void OnDestroy()
    {
        foreach (var character in m_Characters)
        {
            character.ReleaseAsset();
        }
    }
}