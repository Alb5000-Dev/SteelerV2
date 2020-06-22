using System.Collections;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public enum playerType
    {
        owner,
        thief
    }

    public playerType currentPlayerType;

    public float transitionTime;

    public Timer timer;
    public RandomPropsList RPL;

    public IEnumerator StartTheGame()
    {
        //Mini Timer avant de commencer pour laisser le temps au propriétaire de se préparer
        timer.StartCoroutine(timer.timerCoroutine(transitionTime));
        yield return new WaitForSeconds(transitionTime);

        //TP player à l'entrée de la maison

        //L'owner commence à jouer
        currentPlayerType = playerType.owner;
        timer.StartCoroutine(timer.timerCoroutine(timer.ownerTime));

        yield return new WaitForSeconds(timer.ownerTime);
        //Switch to Thief
        StartCoroutine(SwitchPlayer());
    }

    public IEnumerator SwitchPlayer()
    {
        //Affichage de la liste de props à voler
        RPL.createList();

        //Mini Timer avant de commencer pour laisser le temps au voleur de se préparer
        timer.StartCoroutine(timer.timerCoroutine(transitionTime));
        yield return new WaitForSeconds(transitionTime);

        //TP player à l'entrée de la maison

        //Le thief commence à jouer
        currentPlayerType = playerType.thief;
        timer.StartCoroutine(timer.timerCoroutine(timer.thiefTime));        
    }

    private void Start()
    {
        StartCoroutine(StartTheGame());
    }
}