using System;
using Alchemy.Models;

public class StringEventArgs : EventArgs
{
	public string value;
}

public class FloatEventArgs : EventArgs
{
	public float value;
}

public class IntEventArgs : EventArgs
{
	public int value;
}

public class EmployeeEventArgs : EventArgs
{
	public Employee employee;
}

public class IngredientEventArgs : EventArgs
{
	public Ingredient ingredient;
}

public class HerbEventArgs : EventArgs
{
	public Herb herb;
}

public class FlaskEventArgs : EventArgs
{
	public Flask flask;
}

public class PotionEventArgs : EventArgs
{
	public Potion potion;
	public Employee employee;
}

public class EffectEventArgs : EventArgs
{
	public Effect effect;
	public Ingredient ingredient;
}