using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameobjectExtension
{
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }
    
    public static Transform FindChildByName(this GameObject gameObject, string name)
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == name)
            {
                return child;
            }
        }
        return null;
    }
    
    public static void DestroyChildren(this GameObject gameObject)
    {
        foreach (Transform child in gameObject.transform)
        {
            Object.Destroy(child.gameObject);
        }
    }
    
    public static void SetActiveRecursively(this GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
    
    public static void SetLayerRecursively(this GameObject gameObject, int layer)
    {
        gameObject.layer = layer;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetLayerRecursively(layer);
        }
    }
    
    public static GameObject FindWithTagInChildren(this GameObject gameObject, string tag)
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }
    
    public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponentsInChildren<T>().Where(c => c.gameObject != gameObject).ToArray();
    }
    
    public static bool HasComponentInChildren<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponentInChildren<T>() != null;
    }
    
    
    //This extension method returns an array of all components of the specified type in the direct children of the GameObject.
    public static T[] GetComponentsInDirectChildren<T>(this GameObject gameObject) where T : Component
    {
        T[] components = new T[gameObject.transform.childCount];
        int index = 0;

        foreach (Transform child in gameObject.transform)
        {
            T component = child.GetComponent<T>();
            if (component != null)
            {
                components[index++] = component;
            }
        }

        System.Array.Resize(ref components, index);
        return components;
    }
}
