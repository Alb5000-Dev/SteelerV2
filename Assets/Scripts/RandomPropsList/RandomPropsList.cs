using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RandomPropsList : MonoBehaviour
{
    public List<PropScriptableObject> propScriptableObjects;
    //List of all props in the game, used to generate the list of props the thief will have to steal 
    public List<GameObject> allProps;
    //Random list of props the thief will have to steal -> to be static
    public List<GameObject> propsToSteal;
    //List of props the thief have already stolen
    public List<GameObject> propsStolen;

    //How many props the thief will have to steal 
    public int nbToSteal;
    //How many props the thief has stolen at any given moment
    public int hasStolen;
    public int valueStolen;

    public GameObject propsListText;


    //Activate at the start of the game
    public void createList()
    {
        //Shuffle the list 
        allProps = allProps.OrderBy(x => Random.value).ToList();
        //Create the list of props to steal from the shuffled list
        for (int i = 0; i < nbToSteal; i++)
        {
            propsToSteal.Add(allProps[i]);
        }

        updateUI();
    }
    
    //Activate when the thief steal a prop
    public void propStolen(GameObject gameObject)
    {
        if (propsToSteal.Contains(gameObject))
        {
            hasStolen++;
            foreach (var item in propScriptableObjects)
            {
                if (gameObject.GetComponent<propID>().id == item.id)
                {
                    valueStolen += item.value;
                }
            }
            propsStolen.Add(gameObject);
            updateUI();
            gameObject.SetActive(false);
        }
    }

    public void updateUI()
    {
        //Update the UI at the start of the game(when the prop list is generated) and whenever a prop is stolen
        propsListText.GetComponent<TextMeshProUGUI>().text = "Props to Steal : \n";
        foreach (var item in propsToSteal)
        {
            if (propsStolen.Contains(item))
            {
                propsListText.GetComponent<TextMeshProUGUI>().text += "<s>" + item.GetComponent<propID>().name + "</s>" + "\n";
            }
            else
            {
                propsListText.GetComponent<TextMeshProUGUI>().text += item.GetComponent<propID>().name + "\n";
            }  
        }
        if (propsStolen.Count() == nbToSteal)
        {
            propsListText.GetComponent<TextMeshProUGUI>().text = "Get out of the house before the owner comes back !";
        }
    }
}
