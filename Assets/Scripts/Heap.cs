﻿using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapItem<T> {

	T[] items;
	int currentItemCount;
	
	public Heap(int maxHeapSpace){
		items = new T[maxHeapSpace];
	}
	
	public void Add(T item){
		item.HeapIndex = currentItemCount;
		items[currentItemCount] = item;
		SortUp(item);
		currentItemCount++;
	}
	
	public T RemoveFirst() {
		T firstItem = items[0];
		currentItemCount--;
		items[0] = items[currentItemCount];
		items[0].HeapIndex = 0;
		SortDown (items[0]);
		return firstItem;
	}
	
	public void UpdateItem(T item){
		SortUp(item);
	}
	
	public int Count{
		get{
			return currentItemCount;
		}
	}
	
	public bool Contains(T item){
		return Equals(items[item.HeapIndex], item);
	}
	
	//not used in path finding but here if you need it
	void SortDown(T item){
		while(true){
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;
			
			if (childIndexLeft < currentItemCount) {
				swapIndex = childIndexLeft;
				
				if (childIndexRight < currentItemCount) {
					if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0){
						swapIndex = childIndexRight;
					}
				}
				
				if(item.CompareTo(items[swapIndex]) < 0){
					Swap(item, items[swapIndex]);
				} 
				else {
					return;
				}
			}
			else{
				return;
			}
		}
		
	}
	
	void SortUp(T item){
		int parentIndex = (item.HeapIndex-1)/2;
		
		while(true){
			T parentItem = items[parentIndex];
			if(item.CompareTo(parentItem) > 0) {
				Swap(item, parentItem);
			}
			else{
				break;
			}
			
			parentIndex = (item.HeapIndex-1)/2;
		}
	}
	
	void Swap(T itemA, T itemB){
		items[itemA.HeapIndex] = itemB;
		items[itemB.HeapIndex] = itemA;
		
		int itemAIndex = itemA.HeapIndex;
		
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
}

/*
By using interfaces, you can, for example, include behavior from multiple sources in a class. 
That capability is important in C# because the language doesn't support multiple inheritance of classes. 
In addition, you must use an interface if you want to simulate inheritance for structs, 
because they can't actually inherit from another struct or class.

https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/
*/

// allows our generic T variable to have a HeapIndex attribute and IComparable stuff
public interface IHeapItem<T> : IComparable<T> {
	int HeapIndex {
		get;
		set;
	}
}
