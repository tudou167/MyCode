using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//对Dictory的扩展
public static class DictionaryExtension{
//尝试根据key得到value,得到返回value,没有返回null
	public static Tvalue TryGet<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict,Tkey key){
        Tvalue value;
        dict.TryGetValue(key,out value);
        return value;
    }
	
	
}
