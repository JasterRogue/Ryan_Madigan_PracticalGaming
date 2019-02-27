using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    private string name;

    private string type;



    ////no argument constructor 

    public Item()
    {

        // this("Unknown", "UnknownType"); 
    }


    ////3 argument constructor 

    public Item(string name, string type)
    {
        setType(type);

        setName(name);
    }

    public string getName()
    {
        return name;
    }


    public string getType()
    {
        return type;
    }

    public void setType(string Type)
    {
        this.type = Type;
    }


    public void setName(string name)
    {
        this.name = name;
    }
}