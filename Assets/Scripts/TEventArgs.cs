using System;
using Alchemy.Models;

public class StringEventArgs : EventArgs
{
    string _value;

    public string Value
    { 
        get { return _value; }
    }

    public StringEventArgs(string value)
    {
        _value = value;
    }
}

public class FloatEventArgs : EventArgs
{
    float _value;

    public float Value
    { 
        get { return _value; }
    }


    public FloatEventArgs(float value)
    {
        _value = value;
    }
}

public class IntEventArgs : EventArgs
{
    int _value;

    public int Value
    { 
        get { return _value; }
    }

    public IntEventArgs(int value)
    {
        _value = value;
    }
}

public class EmployeeEventArgs : EventArgs
{
    Employee _employee;

    public Employee Employee
    { 
        get { return _employee; }
    }

    public EmployeeEventArgs(Employee employee)
    {
        _employee = employee;
    }
}

public class IngredientEventArgs : EventArgs
{
    Ingredient _ingredient;

    public Ingredient Ingredient
    { 
        get { return _ingredient; }
    }

    public IngredientEventArgs(Ingredient ingredient)
    {
        _ingredient = ingredient;
    }
}

public class HerbEventArgs : EventArgs
{
    Herb _herb;

    public Herb Herb
    { 
        get { return _herb; }
    }

    public HerbEventArgs(Herb herb)
    {
        _herb = herb;
    }
}

public class FlaskEventArgs : EventArgs
{
    Flask _flask;

    public Flask Flask
    { 
        get { return _flask; }
    }

    public FlaskEventArgs(Flask flask)
    {
        _flask = flask;
    }
}

public class PotionEventArgs : EventArgs
{
    Potion _potion;

    public Potion Potion
    { 
        get { return _potion; }
    }

    public PotionEventArgs(Potion potion)
    {
        _potion = potion;
    }
}

public class EffectEventArgs : EventArgs
{
    Effect _effect;
    Ingredient _ingredient;

    public Effect Effect
    { 
        get { return _effect; }
    }

    public Ingredient Ingredient
    { 
        get { return _ingredient; }
    }

    public EffectEventArgs(Effect effect, Ingredient ingredient)
    {
        _effect = effect;
        _ingredient = ingredient;
    }
}