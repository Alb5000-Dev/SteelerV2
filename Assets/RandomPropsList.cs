using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomPropsList : MonoBehaviour
{
    //List of all props in the game, used to generate the list of props the thief will have to steal 
    public List<GameObject> allProps;
    //Random list of props the thief will have to steal
    public static List<GameObject> propsToSteal;

    //How many props the thief will have to steal 
    public int nbToSteal;
    //How many props the thief has stolen at any given moment
    public int hasStolen;

    //Activate at the start of the game
    public void createList()
    {
        //Shuffle the list 
        allProps = allProps.OrderBy(x => UnityEngine.Random.value).ToList();
        //Create the list of props to steal from the shuffled list
        for (int i = 0; i < nbToSteal; i++)
        {
            propsToSteal.Add(allProps[i]);
        }

        //updateUI();
    }
    
    //Activate when the thief steal a prop
    public void propStolen(GameObject gameObject)
    {
        hasStolen++;
        propsToSteal.Remove(gameObject);
        
        //updateUI();
    }

    public void updateUI()
    {
        //Update the UI at the start of the game(when the prop list is generated) and whenever a prop is stolen
    }

    private void Start()
    {
        createList();
    }
}
