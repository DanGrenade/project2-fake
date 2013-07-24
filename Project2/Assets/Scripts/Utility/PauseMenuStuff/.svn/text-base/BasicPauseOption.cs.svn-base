using UnityEngine;
using System.Collections;

public class PauseMainOption
{
	FLabel _text;
	
	PauseMenu.PauseState _next;
	
	
	public PauseMainOption(string option, PauseMenu.PauseState state)
	{
		_text = new FLabel("", option);
		_next = state;
	}
	
	public void SetPosition(int x, int y)
	{
		_text.SetPosition(x, y);
	}
	
	public PauseMenu.PauseState ReturnNext()
	{
		return _next;
	}
	
	public void SetToSelected()
	{
		_text.color = Color.red;
	}
	
	public void Deselect()
	{
		_text.color = Color.white;
	}
	
	public void SetToOn()
	{
		_text.isVisible = true;
	}
	
	public void SetToOff()
	{
		_text.isVisible = false;
	}
	
}
