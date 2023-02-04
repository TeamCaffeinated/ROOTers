// https://github.com/herbou/Unity_ShuffleArraysAndLists

using System.Collections.Generic ;
using UnityEngine ;

public static class ShuffleExtension {
   //shuffle arrays:
   public static void Shuffle<T> (this T[] array, int shuffleAccuracy) {
        if(array.Length <= 1) return;    
        for (int i = 0; i < shuffleAccuracy; i++) {
            int randomIndex = Random.Range (1, array.Length - 1) ;

            T temp = array [ randomIndex ] ;
            array [ randomIndex ] = array [ 0 ] ;
            array [ 0 ] = temp ;
        }
   }
   //shuffle lists:
   public static void Shuffle<T> (this List<T> list, int shuffleAccuracy) {
        if(list.Count <= 1) return;    
        for(int i = 0; i < shuffleAccuracy; i++) {
            int randomIndex = Random.Range (1, list.Count - 1) ;

            T temp = list [ randomIndex ] ;
            list [ randomIndex ] = list [ 0 ] ;
            list [ 0 ] = temp ;
        }
   }
}