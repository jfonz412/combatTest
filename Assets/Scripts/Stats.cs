using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	public float attack = 1f;
	public float baseHp = 10f;
	
	//need to find more efficient way to do this
	public float CalculateTotalAttack(string material, string type, float condition){
		float matPoints = 1f;
		float typePoints = 1f;
		float conPoints = 0.2f;
		
		//material
		if(material == "iron"){
			matPoints = 5f;
		}else if(material == "steel"){
			matPoints = 10f;
		}else if(material == "copper"){
			matPoints = 3f;
		}
		
		//type of weapon
		if(type == "dagger"){
			typePoints = 5f;
		}else if(type == "sword"){
			typePoints = 10f;
		}else if(type == "axe"){
			typePoints = 14f;
		}
		
		//condition of the weapon
		if(condition > 80f ){
			conPoints = 1f;
		}else if(condition < 80f && condition > 60f){
			conPoints = 0.9f;
		}else if(condition < 60f && condition > 20f){
			conPoints = 0.5f;
		}
		
		float total = matPoints + typePoints * conPoints;
		return total;
	}
}
