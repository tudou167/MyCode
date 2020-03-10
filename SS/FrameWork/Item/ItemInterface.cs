using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemInterface  {
    int id { get; }// -id
    string name{get;}// -名称
    string description{get;}// -描述
    Enumclass.Quality quality{get;}// -品质
    //TODO
    // -使用效果委托
}
