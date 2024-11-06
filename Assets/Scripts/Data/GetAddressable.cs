using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class GetAddressable : MonoBehaviour
{
    static GetAddressable _instance;

    public static GetAddressable Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GetAddressable>();
            return _instance;
        }
    }

    public void LoadIcons(string labelName, Action<Dictionary<string, AudioClip>> callback)
    {
        Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
        AsyncOperationHandle<IList<IResourceLocation>> labelOperation = Addressables.LoadResourceLocationsAsync(labelName);
        labelOperation.Completed += (labelResponse) => {
            int totalCount = labelResponse.Result.Count;
            foreach (IResourceLocation item in labelResponse.Result)
            {
                AsyncOperationHandle<AudioClip> resourceOperation = Addressables.LoadAssetAsync<AudioClip>(item.PrimaryKey);
                resourceOperation.Completed += (result) =>
                {
                    totalCount--;
                    switch (labelResponse.Status)
                    {
                        case AsyncOperationStatus.Succeeded:
                            clips.Add(result.Result.name, result.Result);
                            Addressables.Release(resourceOperation);
                            break;
                        case AsyncOperationStatus.Failed:
                            Debug.LogError("Failed to load audio clips.");
                            break;
                        default:
                            break;
                    }
                    // When we've finished loading all items in the directory, let's continue
                    if (totalCount == 0)
                    {
                        callback(clips);
                    }
                };
            }
        };
    }
}
