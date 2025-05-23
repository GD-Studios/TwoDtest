using System.Collections;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;
    int currentAction;
    int currentMove;
    
    void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle() 
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Pokemon);
        enemyHud.SetData(enemyUnit.Pokemon);

        dialogBox.SetMoveNames(playerUnit.Pokemon.Moves);

        yield return dialogBox.TypeDialog($"wild {enemyUnit.Pokemon.Base.Name} pulled up ");
        yield return new WaitForSeconds(1f);

        PlayerAction();

    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an Action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove() 
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    public void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }
    void HandleActionSelection() 
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
            {
                ++currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
            {
                --currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(currentAction== 0)
            {
                PlayerMove();   
            }
            else if(currentAction==1)
            {

            }
        }
        
    }
    void HandleMoveSelection()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentMove < playerUnit.Pokemon.Moves.Count-1)
            {
                ++currentMove;
            }
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentMove > 0)
            {
                --currentMove;
            }
        }

         else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentMove < playerUnit.Pokemon.Moves.Count-2)
            {
                currentMove+=2;
            }
        }
            
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentMove > 1)
            {
                currentMove-=2;
            }
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);

    }
}
