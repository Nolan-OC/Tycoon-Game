using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    aggro,
    reason,
    kindness
}
public class BattleManager : MonoBehaviour
{
    enum State
    {
        idle,   //waiting for battle to begin, available for a battle to start
        paused, //Battle has been paused, either swapping out characters or going on break
        employeeTurn,
        customerTurn,
        victory,
        defeat,
    }
    //This class takes the one Employee and one Customer and battles them, determines who wins and then sends them off
    [SerializeField] private State currentState;

    public Employee employee;
    public Customer customer;

    [SerializeField]
    private BattlePos EPos, CPos;
    public MovementPosBase customerExitPos;
    public MovementPosBase employeeExitPos; //where defeated npc's go on defeat
    
    //TODO QOL make so you can drop any npc anywhere on gameobject and it will properly place them
    public void Start()
    {
        StartBattle();
        if (customerExitPos == null)
            Debug.LogError("ERROR BATTLE HAS NO EXIT POS FOR DEFEATED CUSTOMERS");
        if (employeeExitPos == null)
            Debug.LogError("ERROR BATTLE HAS NO EXIT POS FOR DEFEATED EMPLOYEES");
    }

    public void StartBattle()
    {
        if(customer!=null && employee != null)
        {
            Debug.Log("Battle has begun at counter" + gameObject.name);
            //Customer stops being draggable when battle begins until they leave
            customer.GetComponent<NPCStates>().state = NPCStates.NPCState.notDraggable;
            StartCoroutine(EmployeeTurn());
        }
    }

    IEnumerator EmployeeTurn()
    {
        currentState = State.employeeTurn;
        yield return new WaitForSeconds(2f);
        //check if should take break
        //determine attack or blunder
        float hitRoll = Random.Range(0, 100);
        if(hitRoll <= employee.accuracy)
        {
            //calculate optimal attack style, and damage
            DamageType optimalAttack = CalculateOptimalAttack(employee, customer);
            float optimalDamage = CalculateOptimalDamage(optimalAttack, employee, customer);
            //Debug.Log("EMPLOYEE OPTIMAL: Attacking with = " + optimalAttack);
            //send damageType and damage float to target NPC_COMBAT
            customer.TakeDamage(optimalDamage);
        }
        else
        {
            //choose random attack
            int attackRoll = Random.Range(0, 2);
            float blunderDamage = CalculateOptimalDamage((DamageType)attackRoll,employee,customer);
            //Debug.Log("EMPLOYEE BLUNDER: Attacking with " + (DamageType)attackRoll);
            customer.TakeDamage(blunderDamage);
        }

        if(customer.patience>0)
            StartCoroutine(CustomerTurn());
        else
        {
            currentState = State.victory;
            //TODO change state back to idle at some point
            customer.GetComponent<Navigation>().SetDestination(customerExitPos);
        }
    }
    IEnumerator CustomerTurn()
    {
        currentState = State.customerTurn;
        yield return new WaitForSeconds(2f);
        //determine attack or blunder
        float hitRoll = Random.Range(0, 100);
        if (hitRoll <= customer.accuracy)
        {
            //calculate optimal attack style, and damage
            DamageType optimalAttack = CalculateOptimalAttack(customer, employee);
            float optimalDamage = CalculateOptimalDamage(optimalAttack, customer, employee);
            //Debug.Log("CUSTOMER OPTIMAL: Attacking with = " + optimalAttack);
            //send damageType and damage float to target NPC_COMBAT
            employee.TakeDamage(optimalDamage);
        }
        else
        {
            //choose random attack
            int attackRoll = Random.Range(0, 2);
            float blunderDamage = CalculateOptimalDamage((DamageType)attackRoll, customer, employee);
            //Debug.Log("CUSTOMER BLUNDER: Attacking with " + (DamageType)attackRoll);
            employee.TakeDamage(blunderDamage);
        }

        if (employee.patience > 0)
            StartCoroutine(EmployeeTurn());
        else
        {
            currentState = State.defeat;
            //
            //TODO change state back to idle at some point
            employee.GetComponent<Navigation>().SetDestination(employeeExitPos);
        }
    }
    public void PauseBattle()
    {
        //TODO Pause battle when dragging npc to heal in break room or any other reason like swapping employees
        StopAllCoroutines();
        currentState = State.idle;
    }
    private DamageType CalculateOptimalAttack(NPC_Combat attacker, NPC_Combat defender)
    {
        DamageType optimalType = DamageType.aggro;
        float optimalDamage = CalculateDamage(attacker.aggro, defender.reason);

        if (CalculateDamage(attacker.reason, defender.kindness) > optimalDamage)
        {
            optimalDamage = CalculateDamage(attacker.reason, defender.kindness);
            optimalType = DamageType.reason;
        }
        if (CalculateDamage(attacker.kindness, defender.aggro) > optimalDamage)
        {
            //optimalDamage = CalculateDamage(attacker.kindness, defender.aggro);
            optimalType = DamageType.kindness;
        }
        return (optimalType);
    }
    private float CalculateOptimalDamage(DamageType attackType,NPC_Combat attacker, NPC_Combat defender)
    {
        float damage = 0;
        if (attackType == DamageType.aggro)
            damage = CalculateDamage(attacker.aggro, defender.reason);
        else if (attackType == DamageType.reason)
            damage = CalculateDamage(attacker.reason, defender.kindness);
        else
            damage = CalculateDamage(attacker.kindness, defender.aggro);

        return damage;
    }
    private float CalculateDamage(int att, int def)
    {
        float damage = att/(def*.5f);

        return damage;
    }
    private void TestDamage()
    {
        Debug.Log(CalculateDamage(employee.aggro, customer.aggro));
    }
}
