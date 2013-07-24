using UnityEngine;
using System.Collections;
using System.Text;

public class ControlOption
{
	FLabel keyFunction;
	FLabel keyChange;
	KeyCode theKey;
	string newKey;
	
	public ControlOption(KeyCode key, string function)
	{
		keyFunction = new FLabel("",function);
		keyChange = new FLabel("", key.ToString());
		newKey = key.ToString();
		theKey = key;
	}
	
	public void SetKey(KeyCode key)
	{
		theKey = key;
		ChangeKey(theKey.ToString());
	}
	
	public void ChangeKey(string key)
	{
		newKey = key;
	}
	
	public void TurnOn()
	{
		keyFunction.isVisible = true;
		keyChange.isVisible = true;
	}
	
	public void TurnOff()
	{
		keyFunction.isVisible = false;
		keyChange.isVisible = false;
	}
	
	public void EditOption()
	{
		keyChange.color = Color.blue;
	}
	
	public void FinishEdit()
	{
		keyChange.color = Color.white;
	}
	
	public void SetToSelected()
	{
		keyFunction.color = Color.red;
	}
	
	public void Deselect()
	{
		keyFunction.color = Color.white;
	}
	
	public string GetKey()
	{
		return newKey;
	}
	
	

}
