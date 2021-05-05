﻿using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;

namespace BTD_Mod_Helper.Extensions
{
    public static partial class ArrayExt
    {
        /// <summary>
        /// (Cross-Game compatible) Return as Il2CppSystem.List
        /// </summary>
        public static Il2CppSystem.Collections.Generic.List<T> ToIl2CppList<T>(this T[] array) where T : Il2CppSystem.Object
        {
            Il2CppSystem.Collections.Generic.List<T> il2CppList = new Il2CppSystem.Collections.Generic.List<T>();
            foreach (T item in array)
                il2CppList.Add(item);

            return il2CppList;
        }

        /// <summary>
        /// (Cross-Game compatible) Return as Il2CppReferenceArray
        /// </summary>
        public static Il2CppReferenceArray<T> ToIl2CppReferenceArray<T>(this T[] array) where T : Il2CppSystem.Object
        {
            Il2CppReferenceArray<T> il2cppArray = new Il2CppReferenceArray<T>(array.Length);

            for (int i = 0; i < array.Length; i++)
                il2cppArray[i] = array[i];

            return il2cppArray;
        }

        /// <summary>
        /// (Cross-Game compatible) Return as LockList
        /// </summary>
        public static LockList<T> ToLockList<T>(this T[] array)
        {
            LockList<T> lockList = new LockList<T>();
            foreach (T item in array)
                lockList.Add(item);

            return lockList;
        }

        /// <summary>
        /// (Cross-Game compatible) Return a duplicate of this Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] Duplicate<T>(this T[] array)
        {
            T[] newArray = new T[] { };
            foreach (T item in array)
            {
                Array.Resize(ref newArray, newArray.Length + 1);
                newArray[newArray.Length - 1] = item;
            }

            return newArray;
        }

        /// <summary>
        /// (Cross-Game compatible) Return a duplicate of this array as type TCast
        /// </summary>
        /// <typeparam name="TSource">The original Array Type</typeparam>
        /// <typeparam name="TCast">The Cast type</typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static TCast[] DuplicateAs<TSource, TCast>(this TSource[] array)
            where TSource : Il2CppSystem.Object where TCast : Il2CppSystem.Object
        {
            TCast[] newArray = new TCast[] { };
            foreach (TSource item in array)
            {
                Array.Resize(ref newArray, newArray.Length + 1);
                newArray[newArray.Length - 1] = item.TryCast<TCast>();
            }

            return newArray;
        }

        /// <summary>
        /// (Cross-Game compatible) Return this with an Item added to it
        /// </summary>
        /// <typeparam name="T">The Type of the Item you want to add</typeparam>
        /// <param name="array"></param>
        /// <param name="objectToAdd">Item to add to this</param>
        /// <returns></returns>
        public static T[] AddTo<T>(this T[] array, T objectToAdd) where T : Il2CppSystem.Object
        {
            if (array is null)
                array = new T[0];

            List<T> list = array.ToList();
            list.Add(objectToAdd);
            return list.ToArray();
        }

        /// <summary>
        /// (Cross-Game compatible) Return this with Items added to it
        /// </summary>
        /// <typeparam name="T">The Type of the Items you want to add</typeparam>
        /// <param name="array"></param>
        /// <param name="objectsToAdd">Items you want to add</param>
        /// <returns></returns>
        public static T[] AddTo<T>(this T[] array, T[] objectsToAdd) where T : Il2CppSystem.Object
        {
            if (array is null)
                array = new T[0];

            int size = array.Length + objectsToAdd.Length;
            T[] newReference = new T[size];

            List<T> tempList = new List<T>(array);
            tempList.AddRange(objectsToAdd);

            for (int i = 0; i < tempList.Count; i++)
            {
                T item = tempList[i];
                newReference[i] = item;
            }

            return newReference;
        }

        /// <summary>
        /// (Cross-Game compatible) Return this with Items added to it
        /// </summary>
        /// <typeparam name="T">The Type of the Items you want to add</typeparam>
        /// <param name="array"></param>
        /// <param name="objectsToAdd">Items you want to add</param>
        /// <returns></returns>
        public static T[] AddTo<T>(this T[] array, List<T> objectsToAdd) where T : Il2CppSystem.Object
        {
            return array.AddTo(objectsToAdd.ToArray());
        }


        /// <summary>
        /// (Cross-Game compatible) Check if this has any items of type TCast
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCast">The Type you're checking for</typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool HasItemsOfType<TSource, TCast>(this TSource[] array) where TSource : Il2CppSystem.Object
            where TCast : Il2CppSystem.Object
        {
            // Doing this the ugly way to guarantee no errors. Had a couple of bizarre errors in testing when using LINQ
            for (int i = 0; i < array.Length; i++)
            {
                TSource item = array[i];
                try
                {
                    if (item.IsType<TCast>())
                        return true;
                }
                catch (Exception) { }
            }

            return false;
        }

        /// <summary>
        /// (Cross-Game compatible) Return the first Item of type TCast
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCast">The Type of the Item you want</typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static TCast GetItemOfType<TSource, TCast>(this TSource[] array) where TCast : Il2CppSystem.Object
            where TSource : Il2CppSystem.Object
        {
            if (!HasItemsOfType<TSource, TCast>(array))
                return null;

            TSource result = array.FirstOrDefault(item => item.IsType<TCast>());
            return result.TryCast<TCast>();
        }

        /// <summary>
        /// (Cross-Game compatible) Return all Items of type TCast
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCast">The Type of the Items you want</typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<TCast> GetItemsOfType<TSource, TCast>(this TSource[] array)
            where TSource : Il2CppSystem.Object where TCast : Il2CppSystem.Object
        {
            if (!HasItemsOfType<TSource, TCast>(array))
                return null;

            IEnumerable<TSource> results = array.Where(item => item.TryCast<TCast>() != null);
            return results.DuplicateAs<TSource, TCast>().ToList();
        }

        /// <summary>
        /// (Cross-Game compatible) Return this with the first Item of type TCast removed
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCast">The Type of the Item you want to remove</typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static TSource[] RemoveItemOfType<TSource, TCast>(this TSource[] array)
            where TSource : Il2CppSystem.Object
            where TCast : Il2CppSystem.Object
        {
            TCast behavior = GetItemOfType<TSource, TCast>(array);
            return RemoveItem(array, behavior);
        }

        /// <summary>
        /// (Cross-Game compatible) Return this with the first Item of type TCast removed
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCast">The Type of the Item you want to remove</typeparam>
        /// <param name="array"></param>
        /// <param name="itemToRemove">The specific Item to remove</param>
        /// <returns></returns>
        public static TSource[] RemoveItem<TSource, TCast>(this TSource[] array, TCast itemToRemove)
            where TSource : Il2CppSystem.Object where TCast : Il2CppSystem.Object
        {
            if (!HasItemsOfType<TSource, TCast>(array))
                return array;

            List<TSource> arrayList = array.ToList();

            for (int i = 0; i < array.Length; i++)
            {
                TSource item = array[i];
                if (item is null || !item.Equals(itemToRemove.TryCast<TCast>()))
                    continue;

                arrayList.RemoveAt(i);
                break;
            }

            return arrayList.ToArray();
        }

        /// <summary>
        /// (Cross-Game compatible) Return this with all Items of type TCast removed
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCast">The Type of the Items that you want to remove</typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static TSource[] RemoveItemsOfType<TSource, TCast>(this TSource[] array) where TSource : Il2CppSystem.Object
            where TCast : Il2CppSystem.Object
        {
            if (!HasItemsOfType<TSource, TCast>(array))
                return array;

            int numRemoved = 0;
            List<TSource> arrayList = array.ToList();
            for (int i = 0; i < array.Length; i++)
            {
                TSource item = array[i];
                if (item is null || !item.IsType<TCast>())
                    continue;

                arrayList.RemoveAt(i - numRemoved);
                numRemoved++;
            }

            return arrayList.ToArray();
        }
    }
}
